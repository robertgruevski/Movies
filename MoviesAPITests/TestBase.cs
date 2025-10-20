using API.Data;
using API.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;

namespace MoviesAPITests;

public class TestBase
{
    protected ApplicationDbContext BuildContext(string nameDb)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(nameDb).Options;

        return new ApplicationDbContext(options);
    }

    protected IMapper ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(options =>
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            options.AddProfile(new AutoMapperProfiles(geometryFactory));
        });
        
        return config.CreateMapper();
    }
}
