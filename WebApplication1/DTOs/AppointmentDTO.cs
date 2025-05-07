using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class AppointmentDTO
{
    public int? Id { get; set; }
    public int? PatientId { get; set; }
    public int? DoctorId { get; set; }
    public DateTime? Date { get; set; }
    public PatientDTO? Patient { get; set; }
    public DoctorDTO? Doctor { get; set; }
    public List<ServiceDTO>? appointmentServices { get; set; }
}

public class VisitDTO
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    [MaxLength(7)]
    public required string Pwz { get; set; }
    public List<ServiceOnlyDTO>? Services { get; set; }
}