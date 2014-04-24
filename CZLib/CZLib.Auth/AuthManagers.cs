namespace CZLib.Auth
{
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// 用户身份管理入口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AuthManagers<T> where T : class,IAuthInfo
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="obj"></param>
        public static void Login(T obj)
        {
            var op = ServiceLocator.Current.GetInstance<IAuthOperator<T>>();
            var ser = op.Serializer();
            var value = ser.Serialize(obj);
            var enc = op.Encryptor().Encrypt(value);
            op.Storager().Save(op.StorageName(), enc);
        }

        /// <summary>
        /// 退出
        /// </summary>
        public static void Logout()
        {
            var op = ServiceLocator.Current.GetInstance<IAuthOperator<T>>();
            op.Storager().Clear(op.StorageName());
        }

        /// <summary>
        /// 是否登陆
        /// </summary>
        /// <returns></returns>
        public static bool IsLog()
        {
            return GetNowUser()!=null;
        }

        /// <summary>
        /// 获取当前登陆信息
        /// </summary>
        /// <returns></returns>
        public static T GetNowUser()
        {
            var op = ServiceLocator.Current.GetInstance<IAuthOperator<T>>();
            var enc = op.Storager().Load(op.StorageName());
            if (string.IsNullOrEmpty(enc)) return null;
            var value = op.Encryptor().Decrypt(enc);
            return op.Serializer().Deserialize(value);
        }
    }
}