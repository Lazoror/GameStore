using System.Linq;
using AutoMapper;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.MongoModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using GameStore.Web.ViewModels.Account;
using GameStore.Web.ViewModels.Comment;
using GameStore.Web.ViewModels.Game;
using GameStore.Web.ViewModels.Genre;
using GameStore.Web.ViewModels.Payment;
using GameStore.Web.ViewModels.Platform;
using GameStore.Web.ViewModels.Publisher;

namespace GameStore.Web.MapperModules
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameViewModel, Game>()
                .ForMember(a => a.Publisher,
                    act => act.MapFrom(x => new Publisher { CompanyName = x.Publisher }))
                .ForMember(x => x.GamePlatforms, act => act.Ignore())
                .ForMember(x => x.GameGenres, act => act.Ignore());

            CreateMap<Game, GameViewModel>()
                .ForMember(x => x.Publisher, act => act.MapFrom(y => y.Publisher.CompanyName))
                .ForMember(x => x.NameRu,
                    act => act.MapFrom(y => y.Languages.FirstOrDefault(x => x.Language.Code == "ru")
                                             .Name))
                .ForMember(x => x.Name, act => act.MapFrom(y => y.Name))
                .ForMember(x => x.Description, act => act.MapFrom(y => y.Description))
                .ForMember(x => x.DescriptionRu,
                    act => act.MapFrom(y => y.Languages.FirstOrDefault(x => x.Language.Code == "ru")
                                             .Description))
                .ForMember(x => x.GameGenres, act => act.MapFrom(y => y.GameGenres.Select(x => x.Genre.Name)))
                .ForMember(x => x.GamePlatforms,
                    act => act.MapFrom(y => y.GamePlatforms.Select(z => z.PlatformType.Name)))
                .ForMember(x => x.PublishDate,
                    act => act.MapFrom(y => new string(y.PublishDate.ToString("dd MMMM yyyy"))));

            CreateMap<Publisher, PublisherViewModel>()
                .ForMember(x => x.CompanyNameRu,
                    act => act.MapFrom(y => y.Languages.FirstOrDefault(x => x.Language.Code == "ru")
                                             .CompanyName))
                .ForMember(x => x.HomePageRu,
                    act => act.MapFrom(y => y.Languages.FirstOrDefault(x => x.Language.Code == "ru")
                                             .HomePage))
                .ForMember(x => x.DescriptionRu,
                    act => act.MapFrom(y => y.Languages.FirstOrDefault(x => x.Language.Code == "ru")
                                             .Description));

            CreateMap<Comment, DisplayCommentModel>()
                .ForMember(x => x.GameKey, act => act.MapFrom(y => y.Game.Key))
                .ForMember(x => x.Quote, act => act.MapFrom(y => y.Quote))
                .ForMember(x => x.CommentId, act => act.MapFrom(y => y.Id));

            CreateMap<EditRoleViewModel, Role>()
                .ReverseMap()
                .ForMember(x => x.NewRoleName, act => act.MapFrom(r => r.Name))
                .ForMember(x => x.RoleId, act => act.MapFrom(r => r.Id));

            CreateMap<OrderModel, Order>()
                .ForMember(x => x.OrderDate, act => act.MapFrom(om => om.OrderDate))
                .ForMember(x => x.Id, act => act.MapFrom(om => om.OrderId))
                .ForMember(x => x.OrderStatus, act => act.MapFrom(om => OrderStatus.Shipped));

            CreateMap<Genre, GenreViewModel>()
                .ForMember(x => x.ParentGenre, act => act.MapFrom(x => x.ParentGenre.Name))
                .ForMember(y => y.NameRu,
                    act => act.MapFrom(x => x.Languages.FirstOrDefault(y => y.Language.Code == "ru")
                                             .Name));

            CreateMap<Platform, PlatformTypeViewModel>()
                .ForMember(x => x.NameRu,
                    act => act.MapFrom(y => y.Languages.FirstOrDefault(x => x.Language.Code == "ru")
                                             .Name));

            CreateMap<GenreModel, Genre>()
                .ForMember(x => x.Id, act => act.MapFrom(y => y.GenreId));

            CreateMap<PublisherModel, Publisher>()
                .ForMember(x => x.Id, act => act.MapFrom(y => y.PublisherId));

            CreateMap<PublisherTranslation, Publisher>()
                .ForMember(x => x.Id, act => act.MapFrom(pl => pl.EntityId));

            CreateMap<OrderDetailModel, OrderDetail>()
                .ForMember(x => x.Id, act => act.MapFrom(odm => odm.OrderDetailId));

            CreateMap<GenreModel, Genre>()
                .ForMember(g => g.Id, act => act.MapFrom(gm => gm.GenreId));

            CreateMap<GameModel, Game>()
                .ForMember(g => g.Id, act => act.MapFrom(gm => gm.GameId));

            CreateMap<Game, GameModel>()
                .ForMember(gm => gm.GameId, act => act.MapFrom(g => g.Id));

            CreateMap<PublisherModel, Publisher>()
                .ForMember(p => p.Id, act => act.MapFrom(gm => gm.PublisherId));

            CreateMap<User, DeleteUserViewModel>().ReverseMap();
            CreateMap<CommentViewModel, Comment>().ReverseMap();
            CreateMap<CreateCommentViewModel, Comment>();
            CreateMap<PublisherViewModel, Publisher>();
            CreateMap<GenreViewModel, Genre>();
            CreateMap<PlatformTypeViewModel, Platform>();
            CreateMap<OrderDetail, OrderDetailViewModel>();
            CreateMap<CreateCommentViewModel, Comment>();
        }
    }
}