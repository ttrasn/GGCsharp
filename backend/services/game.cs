using System.Reflection;
using backend.components.pagination;
using backend.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.services;

public class GameService
{
    private readonly IMongoCollection<Game> _gamesCollection;

    public GameService(
        IOptions<AppSettings> setting)
    {
        var mongoClient = new MongoClient(
            setting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            setting.Value.DatabaseName);

        _gamesCollection = mongoDatabase.GetCollection<Game>(
            setting.Value.GamesCollectionName);
    }

    public Pagination<Game> GetList(GameRequest request)
    {
        Pagination<Game> pagination = new Pagination<Game>(this._gamesCollection, request);
        var filter = Builders<Game>.Filter.Eq("Status", GameStatus.Active);
        if (request.Studio != null && request.Studio.Length > 0)
        {
            filter = Builders<Game>.Filter.And(filter, Builders<Game>.Filter.Eq("StudioName", request.Studio));
        }

        return pagination.GetList(filter);
    }

    public async Task<Game?> GetAsync(string id) =>
        await _gamesCollection.Find(x => x.Id == id && x.Status == GameStatus.Active).FirstOrDefaultAsync();

    public Studio[] GetStudios()
    {
        var data = _gamesCollection.Aggregate().Group(x => x.StudioName, y => new Studio
        {
            Title = y.Key,
            Games = y.Count()
        }).ToList();

        return data.ToArray();
    }

    public async Task<Game> CreateAsync(CreateGame g)
    {
        Game newGame = new Game();
        PropertyInfo[] request = typeof(CreateGame).GetProperties();
        PropertyInfo?[] gameClass = typeof(Game).GetProperties();

        foreach (PropertyInfo p in request)
        {
            PropertyInfo? dProperty = Array.Find(gameClass, prop => prop.Name == p.Name);
            if (dProperty != null && dProperty.PropertyType == p.PropertyType)
            {
                object value = p.GetValue(g);
                dProperty.SetValue(newGame, value);
            }
        }

        newGame.Status = GameStatus.Active;
        newGame.Id = null;
        await _gamesCollection.InsertOneAsync(newGame);

        return newGame;
    }

    public async Task UpdateAsync(string id, UpdateGame g)
    {
        var update = Builders<Game>.Update.Set("UpdatedAt", DateTime.Now);
        if (g.GameTitle != null) update = update.Set("GameTitle", g.GameTitle);
        if (g.Genre != null) update = update.Set("Genre", g.Genre);
        if (g.Description != null) update = update.Set("Description", g.Description);
        if (g.Rating != null) update = update.Set("Rating", g.Rating);
        if (g.Price != null) update = update.Set("Price", g.Price);
        if (g.Platform != null) update = update.Set("Platform", g.Platform);
        if (g.StudioName != null) update = update.Set("StudioName", g.StudioName);
        if (g.ImageUrl != null) update = update.Set("ImageUrl", g.ImageUrl);

        await _gamesCollection.UpdateOneAsync(x => x.Id == id && x.Status == GameStatus.Active, update);
    }

    public async Task RemoveAsync(string id)
    {
        var update = Builders<Game>.Update.Set(x => x.Status, GameStatus.Deleted);
        await _gamesCollection.UpdateOneAsync(x => x.Id == id && x.Status == GameStatus.Active, update);
        return;
    }

    public void BunchInsert(Game[] games) => _gamesCollection.InsertManyAsync(games);
}