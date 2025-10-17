using System.ComponentModel.DataAnnotations;
using API.DTOs;
using API.Validations;

namespace API.Entities;

public class Genre : IId
{
    public int Id { get; set; }

    [Required]
    [StringLength(maximumLength: 50)]
    [FirstLetterUppercase]
    public required string Name { get; set; }
}
