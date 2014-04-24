namespace CZLib.Cache
{
    using System;
    using System.Runtime.Caching;

    public class DefaultCache:ICache
    {
        public object Get(string key)
        {
            return MemoryCache.Default.Get(key);
        }

        public bool Set(string key, object obj, DateTime expire)
        {
            try
            {
                MemoryCache.Default.Set(key, obj, new DateTimeOffset(expire));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string key)
        {
            try
            {
                MemoryCache.Default.Remove(key);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}