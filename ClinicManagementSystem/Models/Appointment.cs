namespace ClinicManagementSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Scheduled"; // e.g. Scheduled, Completed, Canceled
        public string? Notes { get; set; }

        // Foreign Keys
        public int PatientId { get; set; }
        public string DoctorId { get; set; }
        public string ReceptionistId { get; set; }
        public int? DoctorAvailabilityId { get; set; }

        // Navigation
        public virtual Patient Patient { get; set; } 
        public virtual ApplicationUser Doctor { get; set; }
        public virtual ApplicationUser Receptionist { get; set; }
        public virtual DoctorAvailability? DoctorAvailability { get; set; }
        public virtual Visit Visit { get; set; }
    }

}
