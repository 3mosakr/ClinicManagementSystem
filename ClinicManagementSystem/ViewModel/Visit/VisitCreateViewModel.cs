using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel.Visit
{
    public class VisitCreateViewModel
    {
        public int AppointmentId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? DoctorNotes { get; set; }

		[DataType(DataType.Date)]
		public string? VisitDateDate { get; set; }   // format: "yyyy-MM-dd"

		[DataType(DataType.Time)]
		public string? VisitDateTime { get; set; }   // format: "HH:mm"


	}
}
