using API.Controllers;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MoviesAPITests.Doubles;
using NSubstitute;

namespace MoviesAPITests.Controllers;

[TestClass]
public class GenresControllerTests : TestBase
{
    [TestMethod]
    public async Task Get_ReturnsAllOfTheGenres()
    {
        // Preparation
        var nameDb = Guid.NewGuid().ToString();
        var context = BuildContext(nameDb);
        var mapper = ConfigureAutoMapper();
        IOutputCacheStore outputCacheStore = null!;

        context.Genres.Add(new Genre() { Name = "Genre 1" });
        context.Genres.Add(new Genre() { Name = "Genre 2" });
        await context.SaveChangesAsync();

        var context2 = BuildContext(nameDb);

        var controller = new GenresController(outputCacheStore, context2, mapper);

        // Testing
        var response = await controller.Get();

        // Verificiation
        Assert.HasCount(expected: 2, response.Value!);
    }

    [TestMethod]
    public async Task Get_ShouldReturn404_WhenGenreWithIdDoesNotExist()
    {
        // Preparation
        var nameDb = Guid.NewGuid().ToString();
        var context = BuildContext(nameDb);
        var mapper = ConfigureAutoMapper();
        IOutputCacheStore outputCacheStore = null!;

        var id = 1;
        var controller = new GenresController(outputCacheStore, context, mapper);

        // Testing
        var response = await controller.Get(id);

        // Verificiation
        Assert.IsInstanceOfType<NotFoundResult>(response.Result);
    }

    [TestMethod]
    public async Task Get_ShouldReturnTheGenre_WhenGenreWithIdExists()
    {
        // Preparation
        var nameDb = Guid.NewGuid().ToString();
        var context = BuildContext(nameDb);
        var mapper = ConfigureAutoMapper();
        IOutputCacheStore outputCacheStore = null!;

        context.Genres.Add(new Genre() { Name = "Genre 1" });
        context.Genres.Add(new Genre() { Name = "Genre 2" });
        await context.SaveChangesAsync();

        var context2 = BuildContext(nameDb);

        var id = 1;
        var controller = new GenresController(outputCacheStore, context2, mapper);

        // Testing
        var response = await controller.Get(id);

        // Verificiation
        var result = response.Value;
        Assert.AreEqual(expected: id, actual: result!.Id);
    }

    [TestMethod]
    public async Task Post_ShouldCreateGenre_WhenWeSendGenre()
    {
        // Preparation
        var nameDb = Guid.NewGuid().ToString();
        var context = BuildContext(nameDb);
        var mapper = ConfigureAutoMapper();
        IOutputCacheStore outputCacheStore = new OutputCacheStoreFake();

        var controller = new GenresController(outputCacheStore, context, mapper);

        // Testing
        var response = await controller.Post(new GenreCreationDTO { Name = "new genre" });

        // Verification
        Assert.IsInstanceOfType<CreatedAtRouteResult>(response);

        var context2 = BuildContext(nameDb);
        var count = await context2.Genres.CountAsync();
        Assert.AreEqual(expected: 1, actual: count);
    }

    private const string cacheTag = "genres";

    [TestMethod]
    public async Task Post_ShouldInvokeEvictByTagAsync_WhenWeSendAGenre()
    {
        // Preparation
        var nameDb = Guid.NewGuid().ToString();
        var context = BuildContext(nameDb);
        var mapper = ConfigureAutoMapper();
        IOutputCacheStore outputCacheStore = Substitute.For<IOutputCacheStore>();

        var controller = new GenresController(outputCacheStore, context, mapper);

        // Testing
        var response = await controller.Post(new GenreCreationDTO { Name = "new genre" });

        // Verification
        await outputCacheStore.Received(1).EvictByTagAsync(cacheTag, default);
    }
}
