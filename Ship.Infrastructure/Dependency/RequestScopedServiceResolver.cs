using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ship.Infrastructure.Dependency
{
    /// <summary>
    /// Request的<see cref="ServiceLifetime.Scoped"/>服务解析器
    /// </summary>
    public class RequestScopedServiceResolver : IScopedServiceResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 初始化一个<see cref="RequestScopedServiceResolver"/>类型的新实例
        /// </summary>
        public RequestScopedServiceResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取 是否可解析
        /// </summary>
        public bool ResolveEnabled => _httpContextAccessor.HttpContext != null;

        /// <summary>
        /// 获取 <see cref="ServiceLifetime.Scoped"/>生命周期的服务提供者
        /// </summary>
        public IServiceProvider ScopedProvider
        {
            get { return _httpContextAccessor.HttpContext.RequestServices; }
        }

        /// <summary>
        /// 获取指定服务类型的实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetService<T>();
        }

        /// <summary>
        /// 获取指定服务类型的实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetService(serviceType);
        }

        /// <summary>
        /// 获取指定服务类型的所有实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetServices<T>()
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetServices<T>();
        }

        /// <summary>
        /// 获取指定服务类型的所有实例
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _httpContextAccessor.HttpContext.RequestServices.GetServices(serviceType);
        }
    }
}
