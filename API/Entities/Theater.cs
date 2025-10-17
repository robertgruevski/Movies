using System.ComponentModel.DataAnnotations;
using API.DTOs;
using NetTopologySuite.Geometries;

namespace API.Entities;

public class Theater : IId
{
    public int Id { get; set; }
    [Required]
    [StringLength(75)]
    public required string Name { get; set; }
    public required Point Location { get; set; }
}
