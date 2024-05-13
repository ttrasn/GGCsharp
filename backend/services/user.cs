using backend.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(
        IOptions<AppSettings> setting)
    {
        var mongoClient = new MongoClient(
            setting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            setting.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            setting.Value.UsersCollectionName);
        Console.WriteLine(_usersCollection);
    }


    public User? FindByEmail(string email) =>
        _usersCollection.Find(x => x.Email == email).FirstOrDefault();

    public User? GetById(string id) => _usersCollection.Find(x => x.Id == id).FirstOrDefault();

    public void InsertUser(User user)
    {
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        user.UserType = UserType.User;
        _usersCollection.InsertOne(user);
    }
}