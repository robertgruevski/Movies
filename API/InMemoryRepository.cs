using API.Entities;

namespace API;

public class InMemoryRepository : IRepository
{
    private List<Genre> _genres;

    public InMemoryRepository()
    {
        _genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Comedy"},
            new Genre { Id = 2, Name = "Action"},
            new Genre { Id = 3, Name = "Drama"}
        };
    }

    public List<Genre> GetAllGenres()
    {
        return _genres;
    }

    public async Task<Genre?> GetById(int id)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        return _genres.FirstOrDefault(g => g.Id == id);
    }

    public bool Exists(string name)
    {
        return _genres.Any(g => g.Name == name);
    }
}
