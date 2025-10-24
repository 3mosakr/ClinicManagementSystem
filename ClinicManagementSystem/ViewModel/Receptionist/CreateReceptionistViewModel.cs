using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel.Receptionist
{
    public class CreateReceptionistViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
