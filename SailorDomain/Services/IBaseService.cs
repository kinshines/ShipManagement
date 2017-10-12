using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace SailorDomain.Services
{
    /// <summary>
    /// 接口基类
    /// <remarks>创建：2014.02.03
    /// 修改：2014.03.04</remarks>
    /// </summary>
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 数据集
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetEntities();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        T Add(T entity, bool isSave = true);

        /// <summary>
        /// 计数
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>数量</returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        bool Update(T entity, bool isSave = true);

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        bool Delete(T entity, bool isSave = true);

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="ID">主键</param>
        /// <param name="isSave"></param>
        /// <returns>是否成功</returns>
        bool Delete(int ID, bool isSave = true);

        /// <summary>
        /// 范围删除
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <param name="isSave"></param>
        /// <returns>是否成功</returns>
        bool DeleteRange(Expression<Func<T, bool>> whereLambda, bool isSave = true);

        /// <summary>
        ///  记录是否存在
        /// </summary>
        /// <param name="anyLambda">条件</param>
        /// <returns>是否存在</returns>
        bool Exist(Expression<Func<T, bool>> anyLambda);

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>实体</returns>
        T Find(int? ID);

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns>实体</returns>
        T Find(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 获取指定页数据
        /// </summary>
        /// <param name="entitys">数据实体集</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        IQueryable<T> PageList(IQueryable<T> entitys, int pageIndex, int pageSize);
    }
}