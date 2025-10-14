using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IOutputCacheStore outputCacheStore;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private const string cacheTag = "genres";

    public GenresController(IOutputCacheStore outputCacheStore, ApplicationDbContext context, IMapper mapper)
    {

        this.outputCacheStore = outputCacheStore;
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    [OutputCache(Tags = [cacheTag])]
    public async Task<ActionResult<List<GenreDTO>>> Get([FromQuery] PaginationDTO pagination)
    {
        var queryable = context.Genres;
        await HttpContext.InsertPaginationParametersInHeader(queryable);
        return await queryable
            .OrderBy(g => g.Name)
            .Paginate(pagination)
            .ProjectTo<GenreDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    [HttpGet("{id:int}", Name = "GetGenreById")]
    [OutputCache(Tags = [cacheTag])]
    public Task<ActionResult<Genre>> Get(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
    {
        var genre = mapper.Map<Genre>(genreCreationDTO);
        
        await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();
        
        await outputCacheStore.EvictByTagAsync(cacheTag, default);
        
        var genreDto = mapper.Map<GenreDTO>(genre);
        return CreatedAtRoute("GetGenreById", new { id = genre.Id }, genre);
    }

    [HttpPut]
    public void Put()
    {

    }

    [HttpDelete]
    public void Delete()
    {

    }
}
