namespace CZLib.DB.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using CZLib.DB;

    public class DatabaseContext:DbContext,IUnitOfWork
    {
        
        public IRepository<T> GetRepository<T>() where T : class
        {
            object obj;
            if (this.dic.TryGetValue(typeof(T), out obj))
            {
                if (obj is EFRepositoryAdapter<T>)
                {
                    return obj as EFRepositoryAdapter<T>;
                }
            }
            var rp= new EFRepositoryAdapter<T>(this,Set<T>());
            this.dic[typeof(T)] = rp;
            return rp;
        }

        private Dictionary<Type, object> dic = new Dictionary<Type, object>();

    }
}