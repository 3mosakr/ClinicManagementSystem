using ClinicManagementSystem.ViewModel.Doctor;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel.Appointment
{
    public class AppointmentViewModel
    {
        public int? AppointmentId { get; set; }

        [Required(ErrorMessage = "Doctor is required")]
        public string DoctorId { get; set; }

        [Required(ErrorMessage = "Patient is required")]
        public int PatientId { get; set; }

        public string? DoctorName { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [Remote(action: "CheckDateNotPast", controller: "Appointment", ErrorMessage = "Appointment date cannot be in the past")]
        public DateTime? AppointmentDateOnly { get; set; }

        [Required(ErrorMessage = "Time is required")]
        [DataType(DataType.Time)]
        [Remote(action: "CheckAvailableTime", controller: "Appointment", AdditionalFields = "DoctorId,AppointmentDateOnly")]
        public TimeSpan? AppointmentTimeOnly { get; set; }

        [MaxLength(200, ErrorMessage = "Notes cannot exceed 200 characters")]
        public string? Notes { get; set; }

        public string? Status { get; set; }

        public List<DoctorViewModel> Doctors { get; set; } = new List<DoctorViewModel>();
    }
}
