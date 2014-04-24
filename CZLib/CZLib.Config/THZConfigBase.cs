namespace CZLib.Config
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// 自定义配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class THZConfigBase<T> : Singleton<T>
    {
        /// <summary>
        /// 检测是否已经创建配置
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has config; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool HasConfig();

        /// <summary>
        /// 从已经创建的配置中读取成配置对象
        /// </summary>
        /// <returns></returns>
        public abstract T Load();

        /// <summary>
        /// 新建一个配置对象
        /// </summary>
        /// <returns></returns>
        public abstract T Create();

        /// <summary>
        /// 保存配置对象
        /// </summary>
        public abstract void Save(T obj);

        /// <summary>
        /// 保存默认值
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void SaveDefault(T obj);

        /// <summary>
        /// 重置为默认
        /// </summary>
        /// <param name="field"></param>
        public abstract void ResetToDefault(Expression<Func<T, object>> field);

    }
}