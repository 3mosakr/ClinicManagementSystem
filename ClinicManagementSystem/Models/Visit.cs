namespace ClinicManagementSystem.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime VisitDate { get; set; }

        // Navigation
        public virtual Appointment Appointment { get; set; }
    }

}
