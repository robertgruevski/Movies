using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class EditClaimDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
