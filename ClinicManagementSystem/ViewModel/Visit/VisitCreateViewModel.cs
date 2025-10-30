using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel.Visit
{
    public class VisitCreateViewModel
    {
		[Required]
		public int AppointmentId { get; set; }
		[MaxLength(200)]
		public string? Diagnosis { get; set; }
		[MaxLength(1000)]
		public string? Prescription { get; set; }
        public DateTime? VisitDate { get; set; }
		[MaxLength(500)]
		public string? DoctorNotes { get; set; }

		[DataType(DataType.Date)]
		public string? VisitDateDate { get; set; }   // format: "yyyy-MM-dd"

		[DataType(DataType.Time)]
		public string? VisitDateTime { get; set; }   // format: "HH:mm"


	}
}
