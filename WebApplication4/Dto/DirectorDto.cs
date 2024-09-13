using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Dto
{
    public class DirectorDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
    }
}
