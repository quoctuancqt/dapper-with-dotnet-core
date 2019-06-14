namespace Core.UnitOfWork
{
    using Core.Common;
    using Core.Entities;
    using Core.Repositories;
    using MicroOrm.Pocos.SqlGenerator;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DapperContext").Value);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            InitRepository();
        }

        public IDataRepository<User> UserRepository { get; private set; }

        private void ResetRepositories()
        {
            UserRepository = null;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }

                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }

                _disposed = true;
            }
        }

        private void InitRepository()
        {
            UserRepository = new GenericDataRepository<User>(_transaction, ResolverFactory.GetService<ISqlGenerator<User>>());
        }
    }
}
