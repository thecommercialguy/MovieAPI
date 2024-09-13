using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using WebApplication4.Dto;
using WebApplication4.Interfaces;
using System.IO;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        {
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieById(int movieId)
        {
            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovieById(movieId)); // Obtaining the object

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        // bbeessttppoossiibbllee
        public IActionResult CreateMovie([FromQuery] int genreId, [FromQuery] int directorId, MovieDto movieCreate)
        {
            if (movieCreate == null)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovies()
                .Where(m => m.Title.Trim().ToUpper() == movieCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (movie != null)
            {
                ModelState.AddModelError("", "Movie already exists..");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap = _mapper.Map<Movie>(movieCreate);

            if (!_movieRepository.CreateMovie(genreId, movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            // CLI concepts, learn to make a server
            // Node is a runtime, Java is a language
            // Solving things that annoy me..

            return Ok("Successfully created");
        }
                
    }
}
