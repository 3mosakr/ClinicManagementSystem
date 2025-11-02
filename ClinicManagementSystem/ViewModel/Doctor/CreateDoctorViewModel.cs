using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel.Doctor
{
    public class CreateDoctorViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Specialty { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
