using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Length(3,150)]
        public string FullName { get; set; }

        // Navigation
        public ICollection<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();
        public ICollection<Appointment> ReceptionistAppointments { get; set; } = new List<Appointment>();
    }


}
