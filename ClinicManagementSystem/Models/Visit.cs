using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        [MaxLength(200)]
        public string? Diagnosis { get; set; }
        [MaxLength(1000)]
        public string? Prescription { get; set; }
        public DateTime? VisitDate { get; set; }
        [MaxLength(500)]
        public string? DoctorNotes { get; set; }

        // Navigation
        public virtual Appointment? Appointment { get; set; }
    }

}
