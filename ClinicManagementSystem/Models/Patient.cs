using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Length(3, 150)]
        public string FullName { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }
        [MaxLength(7)]
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Navigation
        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<ApplicationUser>? Users { get; set; } = new List<ApplicationUser>();
    }

}
