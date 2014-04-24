namespace CZLib.Auth
{
    /// <summary>
    /// 身份信息的序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAuthInfoSerializer<T> where T : IAuthInfo
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize(T obj);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        T Deserialize(string src);
    }
}