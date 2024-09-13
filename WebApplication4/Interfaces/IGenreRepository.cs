using WebApplication4.Models;

namespace WebApplication4.Interfaces
{
    public interface IGenreRepository
    {
        public ICollection<Genre> GetGenres();
        public Genre GetGenre(int genreId);
        public ICollection<Movie> GetMoviesByGenreId(int genreId);
        public bool CreateGenre(Genre genre);
        public bool UpdateGenre(Genre genre);
        public bool DeleteGenre(Genre genre);
        public bool GenreExists(int genreId);
        public bool Save();
    }
}
