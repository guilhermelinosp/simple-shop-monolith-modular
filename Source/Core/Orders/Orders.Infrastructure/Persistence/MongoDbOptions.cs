namespace Order.Infrastructure.Persistence;

public class MongoDbOptions(string database, string connectionString)
{
	public string Database { get; set; } = database;
	public string ConnectionString { get; set; } = connectionString;
}