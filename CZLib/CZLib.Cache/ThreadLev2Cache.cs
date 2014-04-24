namespace CZLib.Cache
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// 线程内二级缓存
    /// </summary>
    public class ThreadLev2Cache:ILev2Cache
    {
        static ThreadLocal<Dictionary<string,object>> local=new ThreadLocal<Dictionary<string,object>>(()=>new Dictionary<string,object>());
        public object Get(string key)
        {
            if(local.Value.ContainsKey(key))
                return local.Value[key];
            return null;
        }

        public bool Set(string key, object obj)
        {
            local.Value[key] = obj;
            return true;
        }

        public bool Remove(string key)
        {
            if (local.Value.ContainsKey(key))
            {
                local.Value.Remove(key);
            }
            return true;
        }
    }
}