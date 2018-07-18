using Microsoft.EntityFrameworkCore;
using Ship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ship.Infrastructure.Services
{
    /// <summary>
    /// 服务基类
    /// <remarks>创建：2014.02.03
    /// 修改：2014.03.04</remarks>
    /// </summary>
    public abstract class BaseService<T> where T : class
    {
        private DefaultDbContext context;

        public BaseService(DefaultDbContext cxt) => context = cxt;

        public virtual IQueryable<T> GetEntities()
        {
            return context.Set<T>();
        }

        public virtual T Add(T entity, bool isSave = true)
        {
            context.Set<T>().Add(entity);
            if (isSave) context.SaveChanges();
            return entity;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return GetEntities().Count(predicate);
        }

        public virtual bool Update(T entity, bool isSave = true)
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
            return isSave ? context.SaveChanges() > 0 : true;
        }

        public bool Delete(T entity, bool isSave = true)
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = EntityState.Deleted;
            return isSave ? context.SaveChanges() > 0 : true;
        }

        public bool Delete(int ID, bool isSave = true)
        {
            var entity = Find(ID);
            context.Set<T>().Remove(entity);
            return isSave ? context.SaveChanges() > 0 : true;
        }

        public bool DeleteRange(Expression<Func<T, bool>> whereLambda, bool isSave = true)
        {
            var entities = GetEntities().Where(whereLambda);
            context.Set<T>().RemoveRange(entities);
            return isSave ? context.SaveChanges() > 0 : true;
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return GetEntities().Any(anyLambda);
        }

        public virtual T Find(int? ID)
        {
            return context.Set<T>().Find(ID);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = GetEntities().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        public int Save() { return context.SaveChanges(); }

        public virtual IQueryable<T> PageList(IQueryable<T> entities, int pageIndex, int pageSize)
        {
            return entities.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
