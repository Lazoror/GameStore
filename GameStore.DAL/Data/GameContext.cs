using GameStore.DAL.EntityConfigurations;
using GameStore.DAL.EntityConfigurations.Account;
using GameStore.DAL.EntityConfigurations.Game;
using GameStore.DAL.EntityConfigurations.LanguageEntities;
using GameStore.DAL.EntityConfigurations.Order;
using GameStore.Domain.Models.LanguageModels;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Domain.Models.SqlModels.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL.Data
{
    public sealed class GameContext : DbContext
    {
        public GameContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<PublisherTranslation> PublisherTranslations { get; set; }
        public DbSet<PlatformTranslation> PlatformTranslations { get; set; }
        public DbSet<GenreTranslation> GenreTranslations { get; set; }
        public DbSet<GameTranslation> GameTranslations { get; set; }
        public DbSet<GameState> GameState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new GameGenreConfiguration());
            modelBuilder.ApplyConfiguration(new GamePlatformConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new GenreTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new GameTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new GameStateConfiguration());
        }
    }
}