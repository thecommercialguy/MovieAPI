using WebApplication4.Models;

namespace WebApplication4.Interfaces
{
    public interface IMovieRepository
    {
        public ICollection<Movie> GetMovies();
        public Movie GetMovieById(int movieId);
        public double GetMovieRating(int movieId);
        public ICollection<Genre> GetGenresByMovie(int movieId);
        public bool CreateMovie(int directorId, List<int> genreIds, Movie movie);
        public bool UpdateMovie(int directorId, List<int> genreIds, Movie movie);
        public bool DeleteMovie(Movie movie);
        public bool MovieExists(int movieId);
        public bool Save();
    }
}
