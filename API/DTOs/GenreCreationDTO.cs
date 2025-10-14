using System.ComponentModel.DataAnnotations;
using API.Validations;

namespace API.DTOs;

public class GenreCreationDTO
{
    [Required]
    [StringLength(maximumLength: 50)]
    [FirstLetterUppercase]
    public required string Name { get; set; }
}
