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
    public async Task<ActionResult<GenreDTO>> Get(int id)
    {
        var genre = await context.Genres
            .ProjectTo<GenreDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre is null) return NotFound();

        return genre;
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
    {
        var genre = mapper.Map<Genre>(genreCreationDTO);
        
        await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();

        await outputCacheStore.EvictByTagAsync(cacheTag, default);
        
        var genreDto = mapper.Map<GenreDTO>(genre);
        return CreatedAtRoute("GetGenreById", new { id = genre.Id }, genreDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
    {
        var genreExists = await context.Genres.AnyAsync(g => g.Id == id);
        
        if (!genreExists) return NotFound();
        
        var genre = mapper.Map<Genre>(genreCreationDTO);
        genre.Id = id;

        context.Genres.Update(genre);
        await context.SaveChangesAsync();
        await outputCacheStore.EvictByTagAsync(cacheTag, default);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        int deletedRecords = await context.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
        
        if (deletedRecords == 0) return NotFound();

        await outputCacheStore.EvictByTagAsync(cacheTag, default);
        return NoContent();
    }
}
