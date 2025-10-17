namespace ClinicManagementSystem.ViewModel.Visit
{
    public class VisitDetailsViewModel
    {
		public int Id { get; set; }
		public int AppointmentId { get; set; }
		public string? Diagnosis { get; set; }
		public string? Prescription { get; set; }
		public DateTime? VisitDate { get; set; }
		public string? DoctorNotes { get; set; }
		public string? PatientName { get; set; }
		public string? DoctorName { get; set; }

	}
}
