using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel.Receptionist
{
    public class ReceptionistViewModel
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Not a valid phone number")]
        public string? PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}
