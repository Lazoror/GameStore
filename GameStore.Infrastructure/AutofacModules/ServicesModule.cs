using Autofac;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Interfaces.Services;
using GameStore.Interfaces.Services.Translation;
using GameStore.Services.Services;
using GameStore.Services.Translation;
using GameStore.Services.Translation.TranslationProvider;
using GameStore.Services.Translation.Translator;

namespace GameStore.Infrastructure.AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<PublisherService>().As<IPublisherService>();
            builder.RegisterType<PlatformService>().As<IPlatformService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ShipperService>().As<IShipperService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<LanguageService>().As<ILanguageService>();

            builder.RegisterGeneric(typeof(TranslateProvider<,>)).As(typeof(ITranslateProvider<,>));
            builder.RegisterType<GameTranslationProvider>().As<ITranslationProvider<Game>>();
            builder.RegisterType<GenreTranslationProvider>().As<ITranslationProvider<Genre>>();
            builder.RegisterType<PlatformTranslationProvider>().As<ITranslationProvider<Platform>>();
            builder.RegisterType<PublisherTranslationProvider>().As<ITranslationProvider<Publisher>>();
            builder.RegisterType<GameTranslator>().As<ITranslator<GameTranslation>>();
            builder.RegisterType<GenreTranslator>().As<ITranslator<GenreTranslation>>();
            builder.RegisterType<PlatformTranslator>().As<ITranslator<PlatformTranslation>>();
            builder.RegisterType<PublisherTranslator>().As<ITranslator<PublisherTranslation>>();
        }
    }
}