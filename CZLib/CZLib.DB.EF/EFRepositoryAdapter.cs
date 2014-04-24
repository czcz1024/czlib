namespace CZLib.DB.EF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class EFRepositoryAdapter<T> :IRepository<T>
        where T : class
    {
        private DbContext context;
        private DbSet<T> set;

        public EFRepositoryAdapter(DbContext context, DbSet<T> set)
        {
            this.context = context;
            this.set = set;
        }

        public T Add(T entity)
        {
            return this.set.Add(entity);
        }

        public T Remove(T entity)
        {
            return this.set.Remove(entity);
        }

        public T Find(object key)
        {
            return this.set.Find(key);
        }

        public T Update(T obj)
        {
            var entry = this.context.Entry(obj);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(obj);
                entry.State = EntityState.Modified;
            }
            return obj;
        }

        public T Update(T obj, params Expression<Func<T, object>>[]property)
        {
            var entry = this.context.Entry(obj);

            foreach (var item in property)
            {
                entry.Property(item).IsModified = true;
            }

            return obj;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IQueryable<T>)this.set).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Expression Expression {
            get
            {
                return ((IQueryable<T>)this.set).Expression;
            }
        }

        public Type ElementType
        {
            get
            {
                return ((IQueryable<T>)this.set).ElementType;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return ((IQueryable<T>)this.set).Provider;
            }
        }
    }
}