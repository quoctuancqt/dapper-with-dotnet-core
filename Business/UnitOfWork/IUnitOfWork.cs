namespace Core.UnitOfWork
{
    using Core.Entities;
    using Core.Repositories;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<User> UserRepository { get; }

        void Commit();
    }
}
