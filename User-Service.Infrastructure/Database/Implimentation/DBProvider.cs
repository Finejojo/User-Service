using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using UserMicroservices.Entity;

public class DBProvider: IDBProvider
{
    readonly IConfiguration _configuration;
    readonly DBConnection _connection;
    readonly IMongoDatabase _database; // database instance


    public DBProvider(IConfiguration configuration, DBConnection connection)
    {
        Log.Information("Establishing connection");
        _connection = connection;
        Log.Information("Establishing configuration");
        _configuration = configuration;
        _configuration.GetSection(nameof(DBConnection)).Bind(_connection = connection);
        Log.Information("Connecting to MongoDB database");
        var client = new MongoClient(_connection.ConnectionString);

      
        Log.Information("Retrieving database");
        _database = client.GetDatabase(_connection.DatabaseName);

        Log.Information("MongoDB database connection successfully established");
    }

    public IMongoDatabase Connect()
    {
        return _database;
    }

}
