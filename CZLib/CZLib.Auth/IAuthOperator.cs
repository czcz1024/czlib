namespace CZLib.Auth
{
    /// <summary>
    /// 身份验证流程需要用到的操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAuthOperator<T> where T : IAuthInfo
    {
        /// <summary>
        /// 加密器
        /// </summary>
        /// <returns></returns>
        IAuthInfoEncryptor Encryptor();

        /// <summary>
        /// 序列化器
        /// </summary>
        /// <returns></returns>
        IAuthInfoSerializer<T> Serializer();

        /// <summary>
        /// 存储器
        /// </summary>
        /// <returns></returns>
        IAuthInfoStorage Storager();

        /// <summary>
        /// 存储时，使用的key 或 name
        /// </summary>
        /// <returns></returns>
        string StorageName();
    }
}