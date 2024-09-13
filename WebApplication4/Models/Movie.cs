using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        public Director? Director { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        // public ICollection<MovieDirector> MovieDirectors { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
