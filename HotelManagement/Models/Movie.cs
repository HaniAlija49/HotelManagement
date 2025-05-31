using System.Text.Json.Serialization;

namespace HotelManagement.Models;

public class Movie
{
    [JsonPropertyName("#TITLE")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("#YEAR")]
    public int Year { get; set; }

    [JsonPropertyName("#IMDB_ID")]
    public string ImdbId { get; set; } = string.Empty;

    [JsonPropertyName("#RANK")]
    public int Rank { get; set; }

    [JsonPropertyName("#ACTORS")]
    public string Actors { get; set; } = string.Empty;

    [JsonPropertyName("#AKA")]
    public string Aka { get; set; } = string.Empty;

    [JsonPropertyName("#IMDB_URL")]
    public string ImdbUrl { get; set; } = string.Empty;

    [JsonPropertyName("#IMDB_IV")]
    public string ImdbIv { get; set; } = string.Empty;

    [JsonPropertyName("#IMG_POSTER")]
    public string Poster { get; set; } = string.Empty;

    [JsonPropertyName("photo_width")]
    public int PhotoWidth { get; set; }

    [JsonPropertyName("photo_height")]
    public int PhotoHeight { get; set; }
} 