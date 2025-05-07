using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;
    
[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentsService _appointmentsService;

    public AppointmentsController(IAppointmentsService service)
    {
        _appointmentsService = service;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById([FromRoute] int id)
    {   
        var clientExists = await _appointmentsService.IsAppointmentExists(id);

        if (!clientExists) return NotFound("Appointment not found");
        
        
        var appointment = await _appointmentsService.GetAppointmentById(id);
        
        appointment.appointmentServices = await _appointmentsService.GetServicesByAppointmentsId(id);
        
        return Ok(appointment);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddVisit([FromBody] Object v)
    {

        VisitDTO visit;
        try
        {
            visit = v as VisitDTO;
        }
        catch (Exception e)
        {
            return BadRequest("Wrong data format");
        }
        
        var appointmentExists = await _appointmentsService.IsAppointmentExists(visit.AppointmentId);

        if (!appointmentExists) return Conflict("Appointment already exists");
        
        var patientExists = await _appointmentsService.IsPatientExists(visit.PatientId);
        if (!patientExists) return NotFound("Patient not exists");
        
        var doctorExists = await _appointmentsService.IsDoctorExists(visit.Pwz);
        if (!doctorExists) return NotFound("Doctor not exists");
        
        
        var status = await _appointmentsService.AddVisit(visit);
        
        
        if (status == 0)
            return Ok();
        else
        {
            return StatusCode(500, "Server Error");
        }
    }
}
