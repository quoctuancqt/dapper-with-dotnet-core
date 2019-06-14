namespace Core.Services
{
    using Core.Common;
    using Core.Repositories;
    using Core.UnitOfWork;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : new()
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected readonly IDataRepository<TEntity> _reponsitory;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _reponsitory = unitOfWork.GetPropValue<IDataRepository<TEntity>>(typeof(TEntity).Name + "Repository");

            _unitOfWork = unitOfWork;
        }

        public virtual bool Delete(object key)
        {
            var result = _reponsitory.Delete(key);

            _unitOfWork.Commit();

            return result;
        }

        public virtual async Task<bool> DeleteAsync(object key)
        {
            var result = await _reponsitory.DeleteAsync(key);

            _unitOfWork.Commit();

            return result;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var result = _reponsitory.GetAll();

            _unitOfWork.Commit();

            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _reponsitory.GetAllAsync();

            _unitOfWork.Commit();

            return result;
        }

        public virtual TEntity GetFirst(object filters)
        {
            return _reponsitory.GetFirst(filters);
        }

        public virtual async Task<TEntity> GetFirstAsync(object filters)
        {
            return await _reponsitory.GetFirstAsync(filters);
        }

        public virtual IEnumerable<TEntity> GetWhere(object filters)
        {
            return _reponsitory.GetWhere(filters);
        }

        public virtual async Task<IEnumerable<TEntity>> GetWhereAsync(object filters)
        {
            return await _reponsitory.GetWhereAsync(filters);
        }

        public virtual bool Insert(TEntity instance)
        {
            var result = _reponsitory.Insert(instance);

            _unitOfWork.Commit();

            return result;
        }

        public virtual async Task<bool> InsertAsync(TEntity instance)
        {
            var result = await _reponsitory.InsertAsync(instance);

            _unitOfWork.Commit();

            return result;
        }

        public virtual bool Update(TEntity instance)
        {
            var result = _reponsitory.Update(instance);

            _unitOfWork.Commit();

            return result;
        }

        public virtual async Task<bool> UpdateAsync(TEntity instance)
        {
            var result = await _reponsitory.UpdateAsync(instance);

            _unitOfWork.Commit();

            return result;
        }
    }
}
