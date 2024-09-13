using AutoMapper;
using WebApplication4.Dto;
using WebApplication4.Models;

namespace WebApplication4.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, MovieDto>();  // "mapping" to our pokemon Dto
            CreateMap<MovieDto, Movie>();
            CreateMap<Director, DirectorDto>();  
            CreateMap<DirectorDto, Director>();
            CreateMap<Genre, GenreDto>();  
            CreateMap<GenreDto, Genre>();
        }
    }
}
