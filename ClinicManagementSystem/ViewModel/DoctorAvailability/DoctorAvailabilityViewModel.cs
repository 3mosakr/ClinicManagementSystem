using System.ComponentModel.DataAnnotations;
using ClinicManagementSystem.Validations;

namespace ClinicManagementSystem.ViewModel.DoctorAvailability
{
    public class DoctorAvailabilityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Doctor")]
        public string DoctorId { get; set; } 

        public string? DoctorName { get; set; } 

        [Display(Name = "Day of Week")]
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required(ErrorMessage = "End time is required")]
        [EndTimeAfterStartTime]
        public TimeSpan EndTime { get; set; }
    }
}