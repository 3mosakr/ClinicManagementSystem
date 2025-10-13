namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Navigation
        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
    }

}
