namespace CZLib.Cache
{
    using System;

    /// <summary>
    /// 缓存功能接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool Set(string key, object obj, DateTime expire);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(string key);
    }
}