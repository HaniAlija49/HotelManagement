using System.Text.Json;
using HotelManagement.Models;
using System.Text.Json.Serialization;

namespace HotelManagement.Services;

public class MovieService : IMovieService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MovieService> _logger;
    private const string BaseUrl = "https://imdb.iamidiotareyoutoo.com";

    public MovieService(HttpClient httpClient, ILogger<MovieService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Movie>> SearchMoviesAsync(string query)
    {
        try
        {
            var url = $"{BaseUrl}/search?q={Uri.EscapeDataString(query)}";
            _logger.LogInformation("Searching movies with URL: {Url}", url);

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            _logger.LogInformation("API Response Status: {StatusCode}", response.StatusCode);
            _logger.LogInformation("API Response Content: {Content}", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("API request failed with status code: {StatusCode}", response.StatusCode);
                return new List<Movie>();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content, options);
            
            if (apiResponse?.Description == null)
            {
                _logger.LogWarning("API response or description is null");
                return new List<Movie>();
            }

            _logger.LogInformation("Found {Count} movies", apiResponse.Description.Count);
            return apiResponse.Description;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for movies with query: {Query}", query);
            return new List<Movie>();
        }
    }

    public async Task<Movie?> GetMovieDetailsAsync(string imdbId)
    {
        try
        {
            var url = $"{BaseUrl}/title/{imdbId}";
            _logger.LogInformation("Getting movie details with URL: {Url}", url);

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            _logger.LogInformation("API Response Status: {StatusCode}", response.StatusCode);
            _logger.LogInformation("API Response Content: {Content}", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("API request failed with status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content, options);
            
            if (apiResponse?.Description == null || !apiResponse.Description.Any())
            {
                _logger.LogWarning("API response or description is null or empty");
                return null;
            }

            return apiResponse.Description.First();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting movie details for IMDB ID: {ImdbId}", imdbId);
            return null;
        }
    }

    public async Task<IEnumerable<Movie>> GetFeaturedMoviesAsync()
    {
        // For featured movies, we'll search for popular movies
        return await SearchMoviesAsync("popular");
    }
}

public class ApiResponse
{
    [JsonPropertyName("ok")]
    public bool Ok { get; set; }

    [JsonPropertyName("description")]
    public List<Movie> Description { get; set; } = new();

    [JsonPropertyName("error_code")]
    public int ErrorCode { get; set; }
} 