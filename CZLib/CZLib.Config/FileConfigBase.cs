namespace CZLib.Config
{
    using System.IO;

    /// <summary>
    /// 基于文件的自定义配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FileTHZConfigBase<T> : THZConfigBase<T> where T:THZConfigBase<T>
    {
        /// <summary>
        /// 保存路径
        /// </summary>
        /// <returns></returns>
        public abstract string FilePath();
        /// <summary>
        /// 默认值保存路径
        /// </summary>
        /// <returns></returns>
        public abstract string DefaultFilePath();
        /// <summary>
        /// 序列化方式
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public abstract string Serialize(T obj);
        /// <summary>
        /// 反序列化方式
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public abstract T Deserialize(string str);

        public override bool HasConfig()
        {
            return File.Exists(this.FilePath());
        }

        public override T Load()
        {
            var str = File.ReadAllText(this.FilePath());
            return this.Deserialize(str);
        }


        public override void Save(T obj)
        {
            var fileInfo = new FileInfo(this.FilePath());
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            File.WriteAllText(this.FilePath(), this.Serialize(obj));
        }

        public override void SaveDefault(T obj)
        {
            var fileInfo = new FileInfo(this.DefaultFilePath());
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            File.WriteAllText(this.DefaultFilePath(), this.Serialize(obj));
        }

    }
}