using System.ComponentModel.DataAnnotations;
using backend.components.pagination;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.services;

public class CreateGame
{
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
}

public class UpdateGame
{
    public string? GameTitle { get; set; }

    public string? Genre { get; set; }
    public string? Platform { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime? ReleaseDate { get; set; }

    public float? Rating { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? StudioName { get; set; }
}

public class GameRequest : Request
{
    public string? Studio { get; set; }
}