using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    
    [HttpGet("{id}/trips")]
    public async Task<IActionResult> GetClientTrips([FromRoute] int id)
    {   
        var clientExists = await _clientsService.ClientExists(id);

        if (!clientExists) return NotFound("Client not found");
        
        
        var trips = await _clientsService.GetClientTripsWithDetails(id);
        
        
        if (trips.Count == 0) return NotFound("Client has not been registered to any of the trips");
        
        return Ok(trips);
    }
}
