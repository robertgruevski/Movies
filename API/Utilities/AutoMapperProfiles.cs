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
        ConfigureActors();
    }

    private void ConfigureActors()
    {
        CreateMap<ActorCreationDTO, Actor>()
            .ForMember(x => x.Picture, options => options.Ignore());
        CreateMap<Actor, ActorDTO>();
    }
    
    private void ConfigureGenres()
    {
        CreateMap<GenreCreationDTO, Genre>();
        CreateMap<Genre, GenreDTO>();
    }
}
