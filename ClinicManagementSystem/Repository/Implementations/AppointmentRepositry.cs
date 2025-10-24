using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Data;
using ClinicManagementSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repository.Implementations
{
    public class AppointmentRepositry : GenericRepository<Appointment>,IAppointmentRepositry
    {
        private readonly ApplicationDbContext context;

        public AppointmentRepositry(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<Appointment> GetAppointmentsByDoctorId(string doctorId)
        {
            return context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.DoctorAvailability)
                .Where(a => a.DoctorId == doctorId)
                .ToList();
        }
    }
}
