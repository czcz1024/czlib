namespace CZLib.DB
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>:IQueryable<T>
        where T : class
    {
        T Add(T obj);

        T Remove(T obj);

        T Update(T obj);

        T Update(T obj,params Expression<Func<T, object>>[]property);

        T Find(object key);
    }
}