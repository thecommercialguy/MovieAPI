namespace WebApplication4.Models
{
    public class MovieGenre
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }  // Forgeign keys
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
