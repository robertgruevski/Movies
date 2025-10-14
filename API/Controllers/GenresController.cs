using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IRepository repository;
    private readonly IOutputCacheStore outputCacheStore;

    public GenresController(IRepository repository, IOutputCacheStore outputCacheStore)
    {
        this.repository = repository;
        this.outputCacheStore = outputCacheStore;
    }

    [HttpGet]
    [OutputCache(Tags = ["genres"])]
    public ActionResult<List<Genre>> Get()
    {
        var genres = repository.GetAllGenres();
        return genres;
    }

    [HttpGet("{id:int}")]
    [OutputCache(Tags = ["genres"])]
    public async Task<ActionResult<Genre>> Get(int id)
    {
        var genre = await repository.GetById(id);

        if (genre is null)
        {
            return NotFound();
        }

        return genre;
    }

    [HttpPost]
    public ActionResult<Genre> Post([FromBody] Genre genre)
    {
        
        return genre;
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
