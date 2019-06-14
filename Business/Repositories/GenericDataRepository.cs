namespace Core.Repositories
{
    using MicroOrm.Pocos.SqlGenerator;
    using System.Data;

    public sealed class GenericDataRepository<TEntity> : DataRepository<TEntity>, IDataRepository<TEntity>
        where TEntity : new()
    {
        public GenericDataRepository(IDbTransaction transaction, ISqlGenerator<TEntity> sqlGenerator) : base(transaction, sqlGenerator)
        {
        }
    }
}
