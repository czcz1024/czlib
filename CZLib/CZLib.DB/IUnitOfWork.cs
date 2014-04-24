namespace CZLib.DB
{
    using System;

    public interface IUnitOfWork:IDisposable
    {
        int SaveChanges();

        IRepository<T> GetRepository<T>() where T : class;
    }
}