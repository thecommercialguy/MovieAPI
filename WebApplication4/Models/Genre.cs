﻿namespace WebApplication4.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }   
    }
}
