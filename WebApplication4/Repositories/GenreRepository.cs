using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Interfaces;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LiteDbContext _context;
        

        public GenreRepository(LiteDbContext context)
        {
            _context = context;
        }

        public bool CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);

            return Save();
        }

        public bool DeleteGenre(Genre genre)
        {
            _context.Remove(genre);

            return Save();
        }

        public bool GenreExists(int genreId)
        {
            var exists = _context.Genres.Any(g => g.Id == genreId);

            return exists;
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.Where(g => g.Id == genreId).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public ICollection<Movie> GetMoviesByGenreId(int genreId)
        {
            return _context.MoviesGenres.Where(mg => mg.GenreId == genreId)
                .Select(mg => mg.Movie)
                .ToList();  // Selecting the pokemon objects
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Update(genre);

            return Save();
        }
    }
}
