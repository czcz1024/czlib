namespace CZLib.Auth
{
    /// <summary>
    /// 标识为一个身份信息
    /// </summary>
    public interface IAuthInfo
    {
        /// <summary>
        /// 此种身份代表的角色
        /// </summary>
        /// <returns></returns>
        string AuthRole();
    }
}