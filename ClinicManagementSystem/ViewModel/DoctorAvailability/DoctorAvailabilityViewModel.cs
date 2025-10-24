using System.ComponentModel.DataAnnotations;

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
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }
    }
}