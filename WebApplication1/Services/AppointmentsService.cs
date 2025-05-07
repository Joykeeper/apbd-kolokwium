using Microsoft.Data.SqlClient;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

public class AppointmentsService : IAppointmentsService
{
    private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";

    public async Task<bool> IsAppointmentExists(int id)
    {
        bool exists = false;
        
        string command = """
                         SELECT 1 FROM Appointment WHERE Appointment_Id = @AppointmentId
                         """;
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("AppointmentId", id);


            object result = await cmd.ExecuteScalarAsync();

            if (result != null)
            {
                exists = true;
            }
        }

        return exists;
    }

    public async Task<AppointmentDTO> GetAppointmentById(int id)
    {
        AppointmentDTO appointment;

        string command = """
                         SELECT A.DATE,  P.First_name, P.Last_name, P.Date_of_birth, D.Doctor_id, D.Pwz
                         FROM Appointment AS A
                         INNER JOIN Patient AS P ON A.Patient_Id=P.Patient_Id
                         INNER JOIN Doctor AS D ON A.Doctor_Id=D.Doctor_Id
                         WHERE A.Appointment_Id = @AppointmentId;
                         """;
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("AppointmentId", id);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                appointment = new AppointmentDTO()
                {
                    Date = reader.GetDateTime(0),
                    Patient = new PatientDTO()
                    {
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        DateOfBirth = reader.GetDateTime(3),
                    },
                    Doctor = new DoctorDTO()
                    {
                        DoctorId = reader.GetInt32(4),
                        Pwz = reader.GetString(5),
                    }
                }; 
            }
        }

        return appointment;
    }

    public async Task<List<ServiceDTO>> GetServicesByAppointmentsId(int id)
    {
        List<ServiceDTO> services = new List<ServiceDTO>();

        string command = """
                         SELECT SER.NAME, SER.SERVICE_FEE
                         FROM Appointment_Service AS SER
                         INNER JOIN Appointment AS A ON A.Appointment_Id = SER.Appointment_Id
                         """;
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("AppointmentId", id);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    services.Add(new ServiceDTO()
                    {
                        Name = reader.GetString(0),
                        ServiceFee = reader.GetInt32(1),
                    });
                }
            }
        }

        return services;
    }

    public async Task<bool> IsPatientExists(int id)
    {
        bool exists = false;
        
        string command = """
                         SELECT 1 FROM Patient WHERE Patient_Id = @Patient_Id
                         """;
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("Patient_Id", id);


            object result = await cmd.ExecuteScalarAsync();

            if (result != null)
            {
                exists = true;
            }
        }

        return exists;
    }

    public async Task<bool> IsDoctorExists(string pwz)
    {
        bool exists = false;
        
        string command = """
                         SELECT 1 FROM Doctor WHERE PWZ = @pwz
                         """;
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("pwz", pwz);


            object result = await cmd.ExecuteScalarAsync();

            if (result != null)
            {
                exists = true;
            }
        }

        return exists;
    }

    public Task<int> AddVisit(VisitDTO visit)
    {
        throw new NotImplementedException();
    }
}