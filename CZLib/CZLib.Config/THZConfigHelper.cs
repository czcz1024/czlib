namespace CZLib.Config
{
    using System;

    public class THZConfigHelper<T> where T : THZConfigBase<T>
    {
        static T _instance;

        public static T Instance
        {
            get { return _instance ?? (_instance = Get()); }
        }

        private static T Get()
        {
            var gen = THZConfigBase<T>.Instance;
            if (gen.HasConfig())
            {
                return gen.Load();
            }
            var obj = gen.Create();
            gen.Save(obj);
            gen.SaveDefault(obj);
            return obj;
        }

        public static void Reload()
        {
            var gen = THZConfigBase<T>.Instance;
            if (gen.HasConfig())
            {
                _instance=gen.Load();
                return;
            }
            throw new Exception("未找到配置文件");
        }
    }
}