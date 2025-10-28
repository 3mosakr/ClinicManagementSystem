using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.Appointment;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IAppointmentService
    {
        List<AppointmentViewModel> GetAppointmentsByDoctor(string id);
        Appointment GetAppointmentById(int id);
        void CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);

        List<AppointmentViewModel> GetAllAppointments();

        AppointmentViewModel GetAppointment(int id);
    }
}
