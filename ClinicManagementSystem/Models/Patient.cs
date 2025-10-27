using System.ComponentModel.DataAnnotations;
using ClinicManagementSystem.Enums;

namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Length(3, 150)]
        public string FullName { get; set; }
		[Phone(ErrorMessage = "Invalid phone number")]
		[StringLength(15, MinimumLength = 11, ErrorMessage = "Phone number must be 11 digits at least.")]
		public string? PhoneNumber { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }
        [MaxLength(7)]
        public Gender Gender { get; set; } = Gender.Unspecified;
		public DateTime? DateOfBirth { get; set; }

        // Navigation
        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<ApplicationUser>? Users { get; set; } = new List<ApplicationUser>();
    }

}
