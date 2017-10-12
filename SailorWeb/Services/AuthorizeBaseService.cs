using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq.Expressions;
using System.Security.Claims;
using SailorDomain.Entities;
using SailorDomain.Services;
using System.Data.Entity.Validation;
using NLog;

namespace SailorWeb.Services
{
    public class AuthorizeBaseService<T> : BaseService<T> where T : class,IEntity
    {
        private string _sysUserId;
        private int _sysCompanyId;
        private ApplicationUserManager _userManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string SysUserId
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
            set
            {
                _sysUserId = value;
            }
        }
        public int SysCompanyId
        {
            get
            {
                var user = UserManager.FindById(SysUserId);
                if (user == null)
                    return 0;
                else
                    return user.CompanyId;
            }
            private set
            {
                _sysCompanyId = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public override IQueryable<T> GetEntities()
        {
            return context.Set<T>().Where(e => e.SysCompanyId == SysCompanyId);
        }
        public override T Add(T entity, bool isSave = true)
        {
            entity.SysUserId = SysUserId;
            entity.SysCompanyId = SysCompanyId;
            context.Set<T>().Add(entity);
            try
            {
                if (isSave) context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach(var result in ex.EntityValidationErrors)
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        logger.Error(error.ErrorMessage);
                    }
                }
                throw ex;
            }
            
            return entity;
        }

        public override bool Update(T entity, bool isSave = true)
        {
            entity.SysUserId = SysUserId;
            entity.SysCompanyId = SysCompanyId;
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return isSave ? context.SaveChanges() > 0 : true;
        }

        public override T Find(int? ID)
        {
            var T = context.Set<T>().Find(ID);
            if (T != null && T.SysCompanyId == SysCompanyId)
                return T;
            return null;
        }

        public override IQueryable<T> PageList(IQueryable<T> entities, int pageIndex, int pageSize)
        {
            return entities.Where(e => e.SysCompanyId == SysCompanyId).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}