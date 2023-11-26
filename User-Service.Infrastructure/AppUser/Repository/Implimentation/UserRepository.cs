using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using User_Service.Domain.AppUser.Models;
using UserMicroservices.Exceptions.Implimentation;


public class UserRepository : IUserRepository
{


    readonly IDBProvider _dbProvider;
    readonly IMongoCollection<User> _user;


    public UserRepository(IDBProvider dbProvider)
    {

        _dbProvider = dbProvider;
      
      _user = _dbProvider.Connect().GetCollection<User>(nameof(User).ToLower());

    }
    public async Task<string> CreateUser(User user)
    {
        try
        {
            Log.Information("Inserting User Data");
            await _user.InsertOneAsync(user);
            Log.Information("Data Inserted");
            return user.Reference;
        }
        catch (Exception e)
        {
            Log.Error("Error Creating User: {0}", e.Message);
            throw DatabaseExceptionHandler.HandleException(e);
        }
    }
    public async Task<string> UpdateUser(string reference, User user)
    {
        try
        {

            Log.Information("Updating Data");
            await _user.ReplaceOneAsync(user => user.Reference == reference, user);
            Log.Information("Data Updated");
            return reference;
        }
        catch (Exception e)
        {
            Log.Error("Error Updating User: {0}", e.Message);
            throw DatabaseExceptionHandler.HandleException(e);
        }
    }
    public async Task<string> DeleteUser(string reference)
    {
        try
        {

            Log.Information("Deleting data");
            var result = await _user.DeleteOneAsync(data => data.Reference == reference);
            Log.Information("Data Deleted");
            return reference;
        }
        catch (Exception e)
        {
            Log.Error("Error Deleting User: {0}", e.Message);
            throw DatabaseExceptionHandler.HandleException(e);
        }
    }
    public async Task<User> GetUserByReference(string reference)
    {
        try
        {
            Log.Information("Getting data by reference {0}", reference);

            var result = await _user.Find(user => user.Reference == reference).FirstOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            Log.Error("Error Getting User: {0}", e.Message);
            throw DatabaseExceptionHandler.HandleException(e);
        }
    }

  
   
    public async Task<User> GetUserByUserName(string username)
    {
        try
        {
            Log.Information("Searching user by username: {0}", username);

            var filter = Builders<User>.Filter.Eq(user => user.UserName, username);

            return await _user.Find(filter).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Log.Error("Error retrieving user by username: {0}", e.Message);
            throw DatabaseExceptionHandler.HandleException(e);
        }
    }

}
