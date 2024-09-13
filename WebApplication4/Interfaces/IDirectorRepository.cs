using WebApplication4.Models;

namespace WebApplication4.Interfaces
{
    public interface IDirectorRepository
    {
        public ICollection<Director> GetDirectors();
        public Director GetDirector(int directorId);
        public ICollection<Movie> GetMoviesByDictor(int directorId);
        public bool CreateDirector(Director createDirector);  // So like, a director doesnt have an owner or a category
        public bool UpdateDirector(Director updateDirector); 
        public bool DeleteDirector(Director director);
        public bool DirectorExists(int directorId);
        public bool Save();
    }
}
// C:\Users\willi\source\repos\WebApplication4\WebApplication4\Interfaces\