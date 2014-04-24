namespace CZLib.Auth
{
    /// <summary>
    /// 身份信息在客户端的存储
    /// </summary>
    public interface IAuthInfoStorage
    {
        /// <summary>
        /// 保存到客户端
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void Save(string name, string value);
        /// <summary>
        /// 从客户端读取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string Load(string name);
        /// <summary>
        /// 清除客户端保存的数据
        /// </summary>
        /// <param name="name"></param>
        void Clear(string name);
    }
}