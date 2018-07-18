using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Ship.Core.SharedKernel;
using Ship.Infrastructure.Data;
using Ship.Infrastructure.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Ship.Infrastructure.Services
{
    public class AuthorizeBaseService<T> : BaseService<T> where T : BaseEntity
    {
        private readonly ILogger logger;
        private readonly ClaimsPrincipal principal;
        public AuthorizeBaseService(DefaultDbContext cxt,ILogger<AuthorizeBaseService<T>> logger):base(cxt)
        {
            this.logger = logger;
            this.principal = ServiceLocator.Instance.GetCurrentUser();
        }
        public string SysUserId => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public int SysCompanyId
        {
            get
            {
                var value = principal.FindFirst(ClaimTypes.GroupSid)?.Value;
                if (value == null)
                {
                    return 0;
                }
                return Convert.ToInt32(value);
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
            catch (DbUpdateException ex)
            {
                foreach (var result in ex.Entries)
                {
                    logger.LogError("update error");
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
            context.Entry<T>(entity).State = EntityState.Modified;
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
