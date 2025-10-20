using API.Controllers;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.OutputCaching;

namespace MoviesAPITests.Controllers;

[TestClass]
public sealed class MoviesControllerTests : TestBase
{
    [TestMethod]
    public async Task Get_ShouldReturnTwoGenres_GivenAMovieWithTwoGenres()
    {
        // Preparation
        var nameDb = Guid.NewGuid().ToString();
        var context = BuildContext(nameDb);
        var mapper = ConfigureAutoMapper();
        IOutputCacheStore outputCacheStore = null!;
        IFileStorage fileStorage = null!;
        IUsersService usersService = null!;

        var genre1 = new Genre { Name = "Genre 1" };
        var genre2 = new Genre { Name = "Genre 2" };

        var movie = new Movie
        {
            Title = "Movie 1",
            MoviesGenres = new List<MovieGenre>
            {
                new MovieGenre{Genre = genre1},
                new MovieGenre{Genre = genre2}
            }
        };

        context.Add(movie);

        await context.SaveChangesAsync();

        var context2 = BuildContext(nameDb);

        var moviesController = new MoviesController(context2, mapper, outputCacheStore, fileStorage, usersService);

        // Testing
        var result = await moviesController.Get(movie.Id);

        // Verification
        var movieFromwebAPI = result.Value!;
        Assert.HasCount(expected: 2, movieFromwebAPI.Genres);
    }
}
