using HotelManagement.Models;

namespace HotelManagement.Services;

public interface IMovieService
{
    Task<IEnumerable<Movie>> SearchMoviesAsync(string query);
    Task<Movie?> GetMovieDetailsAsync(string imdbId);
    Task<IEnumerable<Movie>> GetFeaturedMoviesAsync();
} 