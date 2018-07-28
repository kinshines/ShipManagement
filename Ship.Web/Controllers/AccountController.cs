using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Ship.Infrastructure.Services;
using Ship.Infrastructure.Utility;
using Ship.Web.Models;
using Ship.Web.ViewModels;

namespace Ship.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly SysCompanyService _companyService;
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly ILogger<AccountController> logger;
        readonly IHostingEnvironment _env;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            SysCompanyService companyService, ILogger<AccountController> logger, IHostingEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _companyService = companyService;
            this.logger = logger;
            _env = env;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "无效的用户名或密码");
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await _userManager.GenerateTwoFactorTokenAsync(user, provider);
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, false, model.RememberBrowser);
            if (result.Succeeded)
            {
                return RedirectToLocal(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            ModelState.AddModelError("", "Invalid code.");
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);
                    //await _emailSender.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    ViewBag.Link = callbackUrl;
                    return View("DisplayEmail");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);
                //await _emailSender.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return RedirectToLocal(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        public async Task<ActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        public async Task<ActionResult> Lock()
        {
            string email = User.Identity.Name;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _signInManager.SignOutAsync();
            return RedirectToAction("LockLogin", new { Email = email, UserId = userId });
        }

        [AllowAnonymous]
        public ActionResult LockLogin(string Email, string UserId)
        {
            ViewBag.Email = Email;
            ViewBag.UserId = UserId;
            return View();
        }

        public async Task<ActionResult> UserCenter(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "密码修改成功."
                : message == ManageMessageId.Error ? "出错啦."
                : message == ManageMessageId.ChangeEmailPhoneSuccess ? "账号修改成功."
                : message == ManageMessageId.AddAvatarSuccess ? "头像上传成功."
                : await GetCompanyInfo();

            ViewBag.CompanyName = GetCompanyName();
            return View();
        }
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            ViewBag.CompanyName = GetCompanyName();
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("UserCenter", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            ViewBag.CompanyName = GetCompanyName();
            return View(model);
        }

        public ActionResult UploadAvatar()
        {
            ViewBag.CompanyName = GetCompanyName();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAvatar(IFormFile avatarFile)
        {
            try
            {
                string uploadFolder = "~/Files/avatar/";
                string imgName = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string uploadPath = _env.ContentRootFileProvider.GetFileInfo(uploadFolder).PhysicalPath;

                try
                {
                    //等比例缩放图片
                    string zoomedPicFullPath = uploadPath + imgName + "_small.jpg";

                    // 获取等比例缩放 UploadedImgUrl 后的图片路径
                    Image newImg = ImgHandler.ZoomPicture(Image.FromStream(avatarFile.OpenReadStream()), 40, 40);
                    newImg.Save(zoomedPicFullPath);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "等比例缩放40*40图片错误：");
                }
                try
                {
                    //等比例缩放图片
                    string zoomedPicFullPath = uploadPath + imgName + "_medium.jpg";

                    // 获取等比例缩放 UploadedImgUrl 后的图片路径
                    Image newImg = ImgHandler.ZoomPicture(Image.FromStream(avatarFile.OpenReadStream()), 125, 125);
                    newImg.Save(zoomedPicFullPath);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "等比例缩放125*125图片错误：");
                }
                try
                {
                    //等比例缩放图片
                    string zoomedPicFullPath = uploadPath + imgName + "_large.jpg";

                    // 获取等比例缩放 UploadedImgUrl 后的图片路径
                    Image newImg = ImgHandler.ZoomPicture(Image.FromStream(avatarFile.OpenReadStream()), 200, 200);
                    newImg.Save(zoomedPicFullPath);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "等比例缩放200*200图片错误：");
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "头像保存错误：");
                return RedirectToAction("UserCenter", new { Message = ManageMessageId.Error });
            }
            return RedirectToAction("UserCenter", new { Message = ManageMessageId.AddAvatarSuccess });
        }

        public async Task<ActionResult> AccountSetting()
        {
            var user = await GetCurrentUserAsync();
            var model = new AddPhoneNumberViewModel()
            {
                Email = user.Email,
                Number = user.PhoneNumber
            };
            ViewBag.CompanyName = user.CompanyName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AccountSetting(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.Number;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                user = await GetCurrentUserAsync();
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("UserCenter", new { Message = ManageMessageId.ChangeEmailPhoneSuccess });
            }
            AddErrors(result);
            ViewBag.CompanyName = GetCompanyName();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult GetAvatar(string id, string size)
        {
            if (String.IsNullOrWhiteSpace(id))
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string path = "/Files/avatar/" + id + "_" + size + ".jpg";
            string diskPath = _env.ContentRootFileProvider.GetFileInfo(path).PhysicalPath;
            if (System.IO.File.Exists(diskPath))
            {
                return File(diskPath, "image/jpeg");
            }
            var noimage = _env.ContentRootFileProvider.GetFileInfo("/Files/avatar/noimage.gif").PhysicalPath;
            return PhysicalFile(noimage, "image/gif");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task<string> GetCompanyName()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                return user.CompanyName;
            }
            return String.Empty;
        }
        private async Task<string> GetCompanyInfo()
        {
            var user = await GetCurrentUserAsync();
            var company = _companyService.Find(user.CompanyId);
            if (company == null)
                return "";
            DateTime now = DateTime.Now;
            DateTime open = company.OpenTime;
            TimeSpan opents = now.Subtract(open);
            int opendays = opents.Days;
            DateTime expire = company.ExpireTime;
            TimeSpan expirets = expire.Subtract(now);
            int expiredays = expirets.Days;
            string info = "船员管理系统已持续为 " + company.Name + " 提供船员管理服务 " + opendays + " 天，剩余服务期限 " + expiredays + " 天";
            return info;
        }

        internal class ChallengeResult : UnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ActionContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Items[XsrfKey] = UserId;
                }
                //context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        public enum ManageMessageId
        {
            ChangeEmailPhoneSuccess,
            ChangePasswordSuccess,
            AddAvatarSuccess,
            Error
        }
        #endregion

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}