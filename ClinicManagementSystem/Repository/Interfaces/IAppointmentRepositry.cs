using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IAppointmentRepositry : IGenericRepository<Appointment>
    {
        public List<Appointment> GetAppointmentsByDoctorId(string doctorId);
    }
}
