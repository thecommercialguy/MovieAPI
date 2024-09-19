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
    // GetGenresOfAMovie
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
        [ProducesResponseType(404)]
        public IActionResult GetMovieById(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();

            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovieById(movieId)); // Obtaining the object

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movie);
        }

        [HttpGet("{movieId}/genres")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        [ProducesResponseType(400)]
        public IActionResult GetGenresByMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();

            var genres = _movieRepository.GetGenresByMovie(movieId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(genres);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        // bbeessttppoossiibbllee
        public IActionResult CreateMovie([FromQuery] int directorId, [FromQuery] List<int> genreIds, MovieDto movieCreate)
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

            if (!_movieRepository.CreateMovie(directorId, genreIds, movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            // CLI concepts, learn to make a server
            // Node is a runtime, Java is a language
            // Solving things that annoy me..

            return Ok("Successfully created");
        }

        [HttpPut("{movieId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateMovie(int movieId, [FromQuery] int directorId, [FromQuery] List<int> genreIds, [FromBody] MovieDto updatedMovie)
        {
            if (updatedMovie == null) 
                return BadRequest(ModelState);

            if (movieId != updatedMovie.Id)
                return BadRequest(ModelState);

            if (!_movieRepository.MovieExists(movieId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieUpdateMap = _mapper.Map<Movie>(updatedMovie);


            if (!_movieRepository.UpdateMovie(directorId, genreIds, movieUpdateMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating..");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{movieId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();

            var movieToDelete = _movieRepository.GetMovieById(movieId);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_movieRepository.DeleteMovie(movieToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting..");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
