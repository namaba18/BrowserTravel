namespace Infrastructure.Persistence.Mongo
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = default!;
        public string MongoDatabase { get; set; } = default!;
    }
}
