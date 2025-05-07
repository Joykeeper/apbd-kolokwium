using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IAppointmentsService
{
    
    Task<Boolean> IsAppointmentExists(int id);
    
    Task<AppointmentDTO> GetAppointmentById(int id);
    
    Task<List<ServiceDTO>> GetServicesByAppointmentsId(int id);
    
    Task<Boolean> IsPatientExists(int id);
    Task<Boolean> IsDoctorExists(string pwz);
    
    Task<Int32> AddVisit(VisitDTO visit);

}