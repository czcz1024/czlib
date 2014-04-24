namespace CZLib.Cache
{
    using System.Web;

    /// <summary>
    /// HttpContext2级缓存
    /// </summary>
    public class HttpContextLev2Cache : ILev2Cache
    {
        public object Get(string key)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                return context.Items[key];
            }
            return null;
        }

        public bool Set(string key, object obj)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                context.Items[key] = obj;
            }
            return true;
        }

        public bool Remove(string key)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                context.Items.Remove(key);
            }
            return true;
        }
    }
}