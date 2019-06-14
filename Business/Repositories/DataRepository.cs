namespace Core.Repositories
{
    using Dapper;
    using MicroOrm.Pocos.SqlGenerator;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class DataRepository<TEntity> : DataConnection, IDataRepository<TEntity> where TEntity : new()
    {
        #region Constructors

        public DataRepository(IDbTransaction transaction, ISqlGenerator<TEntity> sqlGenerator)
            : base(transaction.Connection)
        {
            SqlGenerator = sqlGenerator;
            Transaction = transaction;
        }

        #endregion

        #region Properties

        protected ISqlGenerator<TEntity> SqlGenerator { get; private set; }

        protected IDbTransaction Transaction { get; private set; }

        #endregion

        #region Repository sync base actions

        public virtual IEnumerable<TEntity> GetAll()
        {
            var sql = SqlGenerator.GetSelectAll();
            return Connection.Query<TEntity>(sql, null, Transaction);
        }

        public virtual IEnumerable<TEntity> GetWhere(object filters)
        {
            var sql = SqlGenerator.GetSelect(filters);
            return Connection.Query<TEntity>(sql, filters, Transaction);
        }

        public virtual TEntity GetFirst(object filters)
        {
            return GetWhere(filters).FirstOrDefault();
        }

        public virtual bool Insert(TEntity instance)
        {
            bool added = false;
            var sql = SqlGenerator.GetInsert();

            if (SqlGenerator.IsIdentity)
            {
                var newId = Connection.Query<decimal>(sql, instance, Transaction).Single();
                added = newId > 0;

                if (added)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentityProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentityProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                added = Connection.Execute(sql, instance, Transaction) > 0;
            }

            return added;
        }

        public virtual bool Delete(object key)
        {
            var sql = SqlGenerator.GetDelete();
            return Connection.Execute(sql, key, Transaction) > 0;
        }

        public virtual bool Update(TEntity instance)
        {
            var sql = SqlGenerator.GetUpdate();
            return Connection.Execute(sql, instance, Transaction) > 0;
        }

        #endregion

        #region Repository async base action

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = SqlGenerator.GetSelectAll();
            return await Connection.QueryAsync<TEntity>(sql, null, Transaction);
        }

        public virtual async Task<IEnumerable<TEntity>> GetWhereAsync(object filters)
        {
            var sql = SqlGenerator.GetSelect(filters);
            return await Connection.QueryAsync<TEntity>(sql, filters, Transaction);
        }

        public virtual async Task<TEntity> GetFirstAsync(object filters)
        {
            var sql = SqlGenerator.GetSelect(filters);
            Task<IEnumerable<TEntity>> queryTask = Connection.QueryAsync<TEntity>(sql, filters, Transaction);
            IEnumerable<TEntity> data = await queryTask;
            return data.FirstOrDefault();
        }

        public virtual async Task<bool> InsertAsync(TEntity instance)
        {
            bool added = false;
            var sql = SqlGenerator.GetInsert();

            if (SqlGenerator.IsIdentity)
            {
                Task<IEnumerable<decimal>> queryTask = Connection.QueryAsync<decimal>(sql, instance, Transaction);
                IEnumerable<decimal> result = await queryTask;
                var newId = result.Single();
                added = newId > 0;

                if (added)
                {
                    var newParsedId = Convert.ChangeType(newId, SqlGenerator.IdentityProperty.PropertyInfo.PropertyType);
                    SqlGenerator.IdentityProperty.PropertyInfo.SetValue(instance, newParsedId);
                }
            }
            else
            {
                Task<IEnumerable<int>> queryTask = Connection.QueryAsync<int>(sql, instance, Transaction);
                IEnumerable<int> result = await queryTask;
                added = result.Single() > 0;
            }

            return added;
        }

        public virtual async Task<bool> DeleteAsync(object key)
        {
            var sql = SqlGenerator.GetDelete();
            Task<IEnumerable<int>> queryTask = Connection.QueryAsync<int>(sql, key, Transaction);
            IEnumerable<int> result = await queryTask;
            return result.SingleOrDefault() > 0;
        }

        public virtual async Task<bool> UpdateAsync(TEntity instance)
        {
            var sql = SqlGenerator.GetUpdate();
            Task<IEnumerable<int>> queryTask = Connection.QueryAsync<int>(sql, instance, Transaction);
            IEnumerable<int> result = await queryTask;
            return result.SingleOrDefault() > 0;
        }

        #endregion
    }
}
