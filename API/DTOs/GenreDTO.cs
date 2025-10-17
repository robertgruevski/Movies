namespace API.DTOs;

public class GenreDTO : IId
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
