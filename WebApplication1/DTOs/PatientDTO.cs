using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class PatientDTO
{
    public int? Id { get; set; }
    [MinLength(3), StringLength(100)]
    public required string FirstName { get; set; }
    [MinLength(3), StringLength(100)]
    public required string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
}