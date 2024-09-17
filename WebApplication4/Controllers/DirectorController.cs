using AutoMapper;
//using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
// using System.Web.Mvc;

// using System.Web.Mvc;
using WebApplication4.Dto;
using WebApplication4.Interfaces;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMovieRepository _movieRepository;

        private readonly IMapper _mapper;

        public DirectorController(IDirectorRepository directorRepository, IMovieRepository movieRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Director>))]
        public IActionResult GetDirector()
        {
            var directors = _mapper.Map<List<DirectorDto>>(_directorRepository.GetDirectors());  // Getting objects

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(directors);
        }

        [HttpGet("{directorId}")]
        [ProducesResponseType(200, Type = typeof(Director))]
        [ProducesResponseType(400)]
        public IActionResult GetDirector(int directorId)
        {
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();

            var director = _mapper.Map<DirectorDto>(_directorRepository.GetDirector(directorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(director);
        }

        [HttpGet("movies/{directorId}")]
        [ProducesResponseType(200, Type = typeof(Director))]
        [ProducesResponseType(400)]
        public IActionResult GetMoviesByDirector(int directorId)
        {
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();

            var movies = _directorRepository.GetMoviesByDictor(directorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDirector([FromBody] DirectorDto directorCreate)
        {
            if (directorCreate == null)
                return BadRequest(ModelState);

            var directors = _directorRepository.GetDirectors()
                .Where(d => d.LastName.Trim().ToUpper() == directorCreate.LastName.ToUpper())
                .FirstOrDefault();

            if (directors != null)
            {
                ModelState.AddModelError("", "Director already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var directorMap = _mapper.Map<Director>(directorCreate);

            if (!_directorRepository.CreateDirector(directorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            //  var movieExists = _movieRepository.MovieExists(movieId);

            //  if (movieExists == false)
            //  {

            //  }

            // directorMap.


            return Ok("Successfully created!");
            // Quentin Tarantino is an iconic American filmmaker known for his stylized violence, sharp dialogue, and non-linear storytelling. His influential films include Pulp Fiction, Reservoir Dogs, and Django Unchained.
        }

        [HttpPut("{directorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDirector(int directorId,[FromBody] DirectorDto updatedDirector)
        {
            if (updatedDirector == null)
                return BadRequest(ModelState);

            if (directorId != updatedDirector.Id)
                return BadRequest(ModelState);

            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var directorMap = _mapper.Map<Director>(updatedDirector);  // Of type Director DTO, (establishing mapping between director dto obj (param) and the director class
            
            if (!_directorRepository.UpdateDirector(directorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating director..");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{directorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(400)]
        [ProducesResponseType(400)]
        public IActionResult DeleteDirector(int directorId)
        {
            if (!_directorRepository.DirectorExists(directorId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var directorToDelete = _directorRepository.GetDirector(directorId);

            if (!_directorRepository.DeleteDirector(directorToDelete))
            {
                ModelState.AddModelError("", "Error deleting director..");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted!");
        }

    }
}
