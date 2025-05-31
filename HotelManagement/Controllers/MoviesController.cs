using Microsoft.AspNetCore.Mvc;
using HotelManagement.Services;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
    {
        _movieService = movieService;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMovies([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty");
        }

        var movies = await _movieService.SearchMoviesAsync(query);
        return Ok(movies);
    }

    [HttpGet("{imdbId}")]
    public async Task<IActionResult> GetMovieDetails(string imdbId)
    {
        if (string.IsNullOrWhiteSpace(imdbId))
        {
            return BadRequest("IMDB ID cannot be empty");
        }

        var movie = await _movieService.GetMovieDetailsAsync(imdbId);
        if (movie == null)
        {
            return NotFound($"Movie with IMDB ID {imdbId} not found");
        }

        return Ok(movie);
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedMovies()
    {
        var movies = await _movieService.GetFeaturedMoviesAsync();
        return Ok(movies);
    }
} 