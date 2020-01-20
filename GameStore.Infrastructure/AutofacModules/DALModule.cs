using Autofac;
using GameStore.DAL.Data;
using GameStore.DAL.MongoLogger;
using GameStore.DAL.Repositories.MongoRepositories;
using GameStore.DAL.Repositories.RepositoryFacades;
using GameStore.DAL.Repositories.SqlRepositories;
using GameStore.Domain.Models;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.MongoModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.MongoRepositories;
using GameStore.Interfaces.DAL.RepositorySql;

namespace GameStore.Infrastructure.AutofacModules
{
    public class DALModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<GameContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MongoContext>().AsSelf();

            builder.RegisterGeneric(typeof(RepositoryFacade<>)).As(typeof(ICrudRepository<>));
            builder.RegisterType<GameRepositoryFacade>().As<IGameRepository>();

            builder.RegisterGeneric(typeof(CrudRepository<>))
                   .Keyed(RepositoryType.SQL, typeof(ICrudRepository<>));
            builder.RegisterType<CrudGameRepository>().Keyed<IGameRepository>(RepositoryType.SQL);

            builder.RegisterType<CrudRepository<Platform>>().As<ICrudRepository<Platform>>();
            builder.RegisterType<CrudRepository<Comment>>().As<ICrudRepository<Comment>>();
            builder.RegisterType<CrudRepository<Language>>().As<ICrudRepository<Language>>();
            builder.RegisterType<CrudRepository<PublisherTranslation>>().As<ICrudRepository<PublisherTranslation>>();
            builder.RegisterType<CrudRepository<PlatformTranslation>>().As<ICrudRepository<PlatformTranslation>>();
            builder.RegisterType<CrudRepository<GenreTranslation>>().As<ICrudRepository<GenreTranslation>>();
            builder.RegisterType<CrudRepository<GameTranslation>>().As<ICrudRepository<GameTranslation>>();
            builder.RegisterType<CrudRepository<GameState>>().As<ICrudRepository<GameState>>();
            builder.RegisterType<CrudRepository<Genre>>().As<ICrudRepository<Genre>>();
            builder.RegisterType<CrudRepository<Publisher>>().As<ICrudRepository<Publisher>>();
            builder.RegisterType<CrudRepository<UserRole>>().As<ICrudRepository<UserRole>>();

            builder.RegisterType<MongoCategoryRepository>().As<IMongoReadOnlyRepository<Genre>>();
            builder.RegisterType<MongoGameRepository>()
                   .Keyed<IMongoReadOnlyRepository<Game>>(RepositoryType.Mongo);
            builder.RegisterType<MongoPublisherRepository>().As<IMongoReadOnlyRepository<Publisher>>();
            builder.RegisterType<MongoOrderDetailsRepository>()
                   .Keyed<IMongoReadOnlyRepository<OrderDetail>>(RepositoryType.Mongo);
            builder.RegisterType<MongoOrderRepository>()
                   .Keyed<IMongoReadOnlyRepository<Order>>(RepositoryType.Mongo);
            builder.RegisterType<MongoShipperRepository>().As<IMongoReadOnlyRepository<ShipperModel>>();
            builder.RegisterType<MongoLogger>().As<IMongoLogger>();
        }
    }
}