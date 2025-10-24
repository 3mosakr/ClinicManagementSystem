using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel.Visit
{
    public class VisitViewModel
    {
		public int Id { get; set; }


		[Required]
		public int AppointmentId { get; set; }


		[MaxLength(200)]
		public string? Diagnosis { get; set; }


		[MaxLength(1000)]
		public string? Prescription { get; set; }


		[DataType(DataType.DateTime)]
		public DateTime? VisitDate { get; set; } 


		[MaxLength(500)]
		public string? DoctorNotes { get; set; }

		public string? PatientName { get; set; }

		public string? DoctorName { get; set; }

		[DataType(DataType.Date)]
		public string? VisitDateDate { get; set; }   // format: "yyyy-MM-dd"

		[DataType(DataType.Time)]
		public string? VisitDateTime { get; set; }   // format: "HH:mm"

	}
}
