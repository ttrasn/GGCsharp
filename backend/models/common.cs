namespace backend.models;

public class AppSettings
{
    public string JwtSecret { get; set; }
    public string JwtIssuer { get; set; }

    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string GamesCollectionName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
}
