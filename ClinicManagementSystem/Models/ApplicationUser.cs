using Microsoft.AspNetCore.Identity;

namespace ClinicManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        // Navigation
        public ICollection<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();
        public ICollection<Appointment> ReceptionistAppointments { get; set; } = new List<Appointment>();
    }


}
