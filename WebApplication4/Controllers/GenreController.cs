using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApplication4.Dto;
using WebApplication4.Interfaces;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        // GetGenre, GetGenre by id, GetMoviesByGenre (supposedly in the Genre url "path", Create genre

        // 

        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Genre>))]
        [ProducesResponseType(400)]
        public IActionResult GetGenres()
        {
            var genres = _genreRepository.GetGenres();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(genres);

        }

        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Genre>))]
        [ProducesResponseType(400)]
        public IActionResult GetGenreById(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            var genre = _genreRepository.GetGenre(genreId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(genre);

        }

        [HttpGet("movie/{genreId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMoviesByGenreId(int genreId)
        {

            if (!_genreRepository.GenreExists(genreId))
                return NotFound();


            var movies = _mapper.Map<List<MovieDto>>(
                _genreRepository.GetMoviesByGenreId(genreId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_genreRepository.GetMoviesByGenreId(genreId));

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre(GenreDto genreCreate)
        {
            if (genreCreate == null)
                return BadRequest(ModelState);

            var genre = _genreRepository.GetGenres()
                .Where(g => g.Title.Trim().ToUpper() == genreCreate.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (genre != null)
            {
                ModelState.AddModelError("", "Genre already exists..");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(genreCreate);

            if (!_genreRepository.CreateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving..");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");

        }

        [HttpPut("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre([FromQuery] int genreId, GenreDto updatedGenre)
        {
            // Null check
            if (updatedGenre == null)
                return BadRequest(ModelState);

            // **** **** **** **
            if (genreId != updatedGenre.Id)
                return BadRequest(ModelState);

            // "Exists check" *
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            // Sequencing logic for this
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // This, wonder if course will cover it
            var genreMap = _mapper.Map<Genre>(updatedGenre);

            if (!_genreRepository.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating..");  // Adding this to the "ModelState"
                return StatusCode(500, ModelState);
            }

            return NoContent(); // Why "NoContent" for Update

        }

        [HttpDelete("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGenre(int genreId)
        {
            if (_genreRepository.GenreExists(genreId))
                return NotFound();

            var genreToDelete = _genreRepository.GetGenre(genreId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_genreRepository.DeleteGenre(genreToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting..");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
        



    }
}
