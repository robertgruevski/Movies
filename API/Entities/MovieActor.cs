using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class MovieActor
{
    public int MovieId { get; set; }
    public int ActorId { get; set; }
    public int Order { get; set; }
    [StringLength(300)]
    public required string Character { get; set; }

    // Navigation Properties
    public Actor Actor { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}
