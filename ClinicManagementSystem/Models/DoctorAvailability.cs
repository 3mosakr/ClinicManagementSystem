namespace ClinicManagementSystem.Models
{
    public class DoctorAvailability
    {
        public int Id { get; set; }

        public string DoctorId { get; set; } // FK → ApplicationUser.Id (Role = Doctor)
        public ApplicationUser Doctor { get; set; }

        public DayOfWeek DayOfWeek { get; set; } // مثلاً Monday, Tuesday...
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Appointment> Appointments { get; set; }
    }


}
