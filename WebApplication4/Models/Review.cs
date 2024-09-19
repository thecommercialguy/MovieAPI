using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required][Range(1,5)] public int Stars { get; set; }  // When to set the min and max for these...
        public string? Description { get; set; }
        [Required] public Movie Movie { get; set; }
        [Required] public Reviewer Reviewer { get; set; }

    }
}
