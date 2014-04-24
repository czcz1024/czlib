namespace CZLib.Auth
{
    /// <summary>
    /// 身份操作基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AuthOperator<T>:IAuthOperator<T> where T:IAuthInfo
    {
        private IAuthInfoEncryptor encryptor;

        private IAuthInfoSerializer<T> serializer;

        private IAuthInfoStorage storage;

        public AuthOperator(IAuthInfoEncryptor encryptor, IAuthInfoSerializer<T> serializer, IAuthInfoStorage storage)
        {
            this.encryptor = encryptor;
            this.serializer = serializer;
            this.storage = storage;
        }

        public AuthOperator(IAuthInfoSerializer<T> serializer)
            : this(new DesEncryptor(), serializer, new CookieAuthInfoStorage())
        {
        }

        public IAuthInfoEncryptor Encryptor()
        {
            return this.encryptor;
        }

        public IAuthInfoSerializer<T> Serializer()
        {
            return this.serializer;
        }

        public IAuthInfoStorage Storager()
        {
            return this.storage;
        }

        public abstract string StorageName();
    }
}