using System;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Utilities;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        ConfigureGenres();
    }
    
    private void ConfigureGenres()
    {
        CreateMap<GenreCreationDTO, Genre>();
        CreateMap<Genre, GenreDTO>();
    }
}
