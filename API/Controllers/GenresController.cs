using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace API.Controllers;

public class GenresController : CustomBaseController
{
    private readonly IOutputCacheStore outputCacheStore;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private const string cacheTag = "genres";

    public GenresController(IOutputCacheStore outputCacheStore, ApplicationDbContext context, IMapper mapper) : base(context, mapper, outputCacheStore, cacheTag)
    {

        this.outputCacheStore = outputCacheStore;
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    [OutputCache(Tags = [cacheTag])]
    public async Task<ActionResult<List<GenreDTO>>> Get([FromQuery] PaginationDTO pagination)
    {
        return await Get<Genre, GenreDTO>(pagination, orderBy: g => g.Name);
    }

    [HttpGet("all")]
    [OutputCache(Tags = [cacheTag])]
    public async Task<ActionResult<List<GenreDTO>>> Get()
    {
        return await Get<Genre, GenreDTO>(orderBy: g => g.Name);
    }

    [HttpGet("{id:int}", Name = "GetGenreById")]
    [OutputCache(Tags = [cacheTag])]
    public async Task<ActionResult<GenreDTO>> Get(int id)
    {
        return await Get<Genre, GenreDTO>(id);
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
    {
        return await Post<GenreCreationDTO, Genre, GenreDTO>(genreCreationDTO, routeName: "GetGenreById");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
    {
        return await Put<GenreCreationDTO, Genre>(id, genreCreationDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await Delete<Genre>(id);
    }
}
