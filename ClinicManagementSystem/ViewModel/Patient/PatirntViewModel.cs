
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(150, MinimumLength = 3)]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(7)]
        public string? Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
