using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.models;

public class Game
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string GameTitle { get; set; }

    public string Genre { get; set; }
    public string Platform { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime ReleaseDate { get; set; }

    public float Rating { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string StudioName { get; set; }
    public DateTime UpdatedAt { get; set; }

    [EnumDataType(typeof(GameStatus))] public GameStatus Status { get; set; }
}

public class Studio
{
    public string Title { get; set; }
    public int Games { get; set; }
}

public enum GameStatus
{
    Active = 0,
    Deleted = 1,
}

