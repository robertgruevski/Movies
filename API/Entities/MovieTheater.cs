namespace API.Entities;

public class MovieTheater
{
    public int MovieId { get; set; }
    public int TheaterId { get; set; }

    // Navigation Properties
    public Movie Movie { get; set; } = null!;
    public Theater Theater { get; set; } = null!;
}
