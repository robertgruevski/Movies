using API.Entities;

namespace API;

public interface IRepository
{
    List<Genre> GetAllGenres();
    Task<Genre?> GetById(int id);
    bool Exists(string name);
}
