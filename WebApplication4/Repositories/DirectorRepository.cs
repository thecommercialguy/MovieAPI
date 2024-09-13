using WebApplication4.Data;
using WebApplication4.Interfaces;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly LiteDbContext _context;

        public DirectorRepository(LiteDbContext context) 
        {
            _context = context;
        }
        // Edit director name, oh yeah, update director
        public bool CreateDirector(Director createDirector)
        {
            _context.Add(createDirector);

            return Save();
        }

        public bool DeleteDirector(Director director)
        {
            _context.Remove(director);

            return Save();
        }

        public bool DirectorExists(int directorId)
        {
            var exists = _context.Directors.Any(d => d.Id == directorId);

            return exists ? true : false;
        }

        public Director GetDirector(int directorId)
        { 
            return _context.Directors.Where(d => d.Id == directorId).FirstOrDefault();
        }

        public ICollection<Director> GetDirectors()
        {
            return _context.Directors.ToList();
        }

        public ICollection<Movie> GetMoviesByDictor(int directorId)
        {
            return (ICollection<Movie>)_context.Movies.Where(m => m.Director.Id == directorId).Select(m => m).ToList();

            // var t = _context.Movies.Where(d => d.Director.Id == directorId).Select(c => c).ToList();

            
                
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ?  true : false;
        }

        public bool UpdateDirector(Director updatedDirector)
        {
            _context.Update(updatedDirector);

            return Save();
        }
    }
}
