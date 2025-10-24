using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IAppointmentService
    {
        List<Appointment> GetAppointmentsByDoctor(string id);
        Appointment GetAppointmentById(int id);
        void CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);


    }
}
