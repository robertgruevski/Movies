using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isadmin")]
public class TheatersController : CustomBaseController
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IOutputCacheStore outputCacheStore;
    private const string cacheTag = "theaters";

    public TheatersController(ApplicationDbContext context, IMapper mapper, IOutputCacheStore outputCacheStore) : base(context, mapper, outputCacheStore, cacheTag)
    {
        this.context = context;
        this.mapper = mapper;
        this.outputCacheStore = outputCacheStore;
    }

    [HttpGet]
    [OutputCache(Tags = [cacheTag])]
    public async Task<ActionResult<List<TheaterDTO>>> Get([FromQuery] PaginationDTO pagination)
    {
        return await Get<Theater, TheaterDTO>(pagination, orderBy: g => g.Name);
    }

    [HttpGet("{id:int}", Name = "GetTheaterById")]
    [OutputCache(Tags = [cacheTag])]
    public async Task<ActionResult<TheaterDTO>> Get(int id)
    {
        return await Get<Theater, TheaterDTO>(id);
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] TheaterCreationDTO theaterCreationDTO)
    {
        return await Post<TheaterCreationDTO, Theater, TheaterDTO>(theaterCreationDTO, routeName: "GetTheaterById");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] TheaterCreationDTO theaterCreationDTO)
    {
        return await Put<TheaterCreationDTO, Theater>(id, theaterCreationDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await Delete<Theater>(id);
    }
}
