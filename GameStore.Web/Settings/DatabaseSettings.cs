using GameStore.Interfaces.DAL.Data.MongoSettings;

namespace GameStore.Web.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}