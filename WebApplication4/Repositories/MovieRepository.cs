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
        // Add updated parameters for updating a Movie (directorId, genreList)
        // Directors will be associated with directors via create and update movie
        public bool CreateMovie(int directorId, List<int> genreIds, Movie movie)
        {
            var director = _context.Directors.Where(d => d.Id == directorId).FirstOrDefault();

            if (director == null)
                return false;
            
            movie.Director = director;

            foreach (var genreId in genreIds)
            {
                var genereEntity = _context.Genres.Where(x => x.Id == genreId).FirstOrDefault();

                if (genereEntity == null)
                    return false;

                var movieGenre = new MovieGenre
                {
                    Movie = movie,
                    Genre = genereEntity
                };

                // Can posit that movie genres is only when updated within the Movie  

                _context.Add(movieGenre);
            }

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

        public bool UpdateMovie(int directorId, List<int> genreIds, Movie updatedMovie)
        {
            var director = _context.Directors.Where(d => d.Id == directorId).FirstOrDefault();

            if (director == null)
                return false;

            updatedMovie.Director = director;

            var currentMovieGenres = _context.MoviesGenres.Where(mg => mg.MovieId == updatedMovie.Id);
            _context.MoviesGenres.RemoveRange(currentMovieGenres);


            foreach (var genreId in genreIds)
            {
                var genereEntity = _context.Genres.Where(x => x.Id == genreId).FirstOrDefault();

                if (genereEntity == null)
                    return false;

                var movieGenre = new MovieGenre
                {
                    Movie = updatedMovie,
                    Genre = genereEntity
                };

                _context.Update(movieGenre);
            }

            _context.Update(updatedMovie);

            return Save();
        }

        public double GetMovieRating(int movieId)
        {
            var count = _context.Reviews.Count();
            var stars = _context.Reviews.Where(r => r.Movie.Id == movieId).Select(r => r.Stars).FirstOrDefault();

            return (stars / count);
        }

        public ICollection<Genre> GetGenresByMovie(int movieId)
        {
            return _context.MoviesGenres.Where(m => m.MovieId == movieId).Select(mg => mg.Genre).ToList();
        }
    }
}