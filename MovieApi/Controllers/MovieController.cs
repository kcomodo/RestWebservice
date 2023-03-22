using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers;
using MovieApi.Models;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static readonly List<Movie> movies = new List<Movie>(10){
        new Movie{Name = "Citizen Kane", Genre = "Drama", Year = 1941},
        new Movie{Name = "The Wizard of Oz", Genre = "Fantasy", Year = 1939},
        new Movie{Name = "The Godfather", Genre = "Crime", Year =1972}
    };

    private readonly ILogger<MovieController> _logger;

    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
    }

    
    public IActionResult GetMovies(){
        if(movies != null)
            return Ok(movies);
        
        else
            return BadRequest();

        
    }
    [HttpGet("{name}", Name = "GetMovies")]
    public IActionResult GetMovieByName(string name)
    {   
        foreach(Movie m in movies){
            if(m.Name.Equals(name)){
                return Ok(m);
            }
        }
        return BadRequest();
    }
    [HttpGet("year/")]
    public IActionResult GetMovieByYear(int year){
        foreach(Movie m in movies){
            if(m.Year.Equals(year)){
                return Ok(m);
            };
        }
        return BadRequest();

    }
    [HttpPost]
    public IActionResult CreateMovie(Movie m){
        try{
             movies.Add(m);
             return CreatedAtRoute("GetMovies", new {name = m.Name}, m);
        }catch(Exception e){
            return StatusCode(500);
        }
       
    }
    [HttpPut("{name}")]
    public IActionResult UpdateMovie(string name, Movie movieIn){
        try{
             //movies.Add(m);
             foreach(Movie m in movies){
                if(m.Name.Equals(name)){
                    m.Name = movieIn.Name;
                    m.Genre = movieIn.Genre;
                    m.Year = movieIn.Year;
                    return NoContent();
                }
             }
             return BadRequest();
        }catch(Exception e){
            StatusCode(500);
        }
        return BadRequest();
       
    }
    

}
