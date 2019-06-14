namespace Core.IoC
{
    using Core.Common;
    using Core.Entities;
    using Core.Services;
    using Core.UnitOfWork;
    using MicroOrm.Pocos.SqlGenerator;
    using Microsoft.Extensions.DependencyInjection;

    public class IoCConfiguration
    {
        public static void RegisterIoC(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Services
            services.AddScoped<IUserService, UserService>();
            //SQL Generator
            services.AddScoped<ISqlGenerator<User>, SqlGenerator<User>>();

            ResolverFactory.SetProvider(services.BuildServiceProvider());
        }
    }
}
