namespace GameStore.Interfaces.DAL.Data.MongoSettings
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}