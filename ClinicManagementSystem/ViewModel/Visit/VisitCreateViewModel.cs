namespace ClinicManagementSystem.ViewModel.Visit
{
    public class VisitCreateViewModel
    {
        public int AppointmentId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? DoctorNotes { get; set; }

	}
}
