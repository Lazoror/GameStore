using Autofac;
using GameStore.Infrastructure.Factory;
using GameStore.Interfaces.DAL.RepositorySql;

namespace GameStore.Infrastructure.AutofacModules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RepositoryFactory>().As<IRepositoryFactory>();
        }
    }
}