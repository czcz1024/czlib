namespace CZLib.Cache
{
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// 缓存服务器获取接口
    /// </summary>
    public interface ICacheServerList
    {
        /// <summary>
        /// 获取缓存服务器地址
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPEndPoint> GetCacheServers();
    }
}