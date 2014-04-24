namespace CZLib.Config
{
    using System;
    using System.Linq.Expressions;

    internal static class XmlConfigExtension
    {
        public static Expression<Func<T, object>> GetExpression<T>(string src) where T:XmlFileConfigBase<T>
        {
            var props = src.Split('.');
            var baseParam = Expression.Parameter(typeof(T), "x");
            MemberExpression mem = Expression.Property(baseParam, props[0]);
            for (var i = 1; i < props.Length; i++)
            {
                mem = Expression.Property(mem, props[i]);

            }
            var exp = Expression.Lambda<Func<T, object>>(Expression.Convert(mem, typeof(object)), baseParam);
            return exp;
        }
    }
}