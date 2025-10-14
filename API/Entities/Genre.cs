using System.ComponentModel.DataAnnotations;
using API.Validations;

namespace API.Entities;

public class Genre
{
    public int Id { get; set; }

    [Required]
    [FirstLetterUppercase]
    public required string Name { get; set; }
}
