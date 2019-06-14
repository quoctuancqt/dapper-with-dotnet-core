namespace Core.Services
{
    using Core.Entities;
    using Core.UnitOfWork;

    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
