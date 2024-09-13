using WebApplication4.Data;
using WebApplication4.Interfaces;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly LiteDbContext _context;

        public MovieRepository(LiteDbContext context)
        {
            _context = context;
        }
        public bool CreateMovie(int genreId, Movie movie)
        {
            var genereEntity = _context.Genres.Where(x => x.Id == genreId).FirstOrDefault();

            if (genereEntity == null)
                return false;
            
            var movieGenre = new MovieGenre
            {
                Movie = movie,
                Genre = genereEntity
            };

            _context.Add(movieGenre);

            _context.Add(movie);

            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _context.Remove(movie);

            return Save();
        }

        public bool MovieExists(int movieId)
        {
            var exists = _context.Movies.Any(m => m.Id == movieId);

            return exists ? true : false;
        }

        public Movie GetMovieById(int movieId)
        {
            return _context.Movies.Where(m => m.Id == movieId).FirstOrDefault();
        }

       
        public ICollection<Movie> GetMovies()
        {
            return _context.Movies.ToList();
        }

        public ICollection<Review> GetReviewsByMovie(int movieId)
        {
            return _context.Reviews.Where(r => r.Movie.Id == movieId).ToList();
        }
        

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateMovie(Movie updatedMovie)
        {
            _context.Update(updatedMovie);

            return Save();
        }

        public double GetMovieRating(int movieId)
        {
            var count = _context.Reviews.Count();
            var stars = _context.Reviews.Where(r => r.Movie.Id == movieId).Select(r => r.Stars).FirstOrDefault();

            return (stars / count);
        }
    }
}