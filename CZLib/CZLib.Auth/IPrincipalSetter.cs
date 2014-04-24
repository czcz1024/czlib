namespace CZLib.Auth
{
    /// <summary>
    /// 设置身份信息到上下文的接口
    /// </summary>
    public interface IPrincipalSetter
    {
        /// <summary>
        /// 设置
        /// </summary>
        void SetToPrincipal();
    }
}