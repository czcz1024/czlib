namespace CZLib.Cache
{
    /// <summary>
    /// 二级缓存接口
    /// </summary>
    public interface ILev2Cache
    {
        object Get(string key);

        bool Set(string key, object obj);

        bool Remove(string key);
    }
}