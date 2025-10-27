
using System;
using System.ComponentModel.DataAnnotations;
using ClinicManagementSystem.Enums;

namespace ClinicManagementSystem.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(150, MinimumLength = 3)]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
		[StringLength(15, MinimumLength = 11, ErrorMessage = "Phone number must be 11 digits at least.")]
		public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [Required]
        public Gender Gender { get; set; } = Gender.Unspecified;

		[DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
