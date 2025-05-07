using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class DoctorDTO
{
    public int? DoctorId { get; set; }
    [StringLength(7)]
    public required string Pwz { get; set; }
}

