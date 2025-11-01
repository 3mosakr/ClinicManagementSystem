using ClinicManagementSystem.ViewModel.DoctorAvailability;

namespace ClinicManagementSystem.ViewModel.Appointment
{
    public class TotalCountOfDataViewModel
    {
        public int TotalCurrentAppointments { get; set; }
        public int TotalCancelAppointments { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalPatients { get; set; }

        public List<AppointmentViewModel> CurrentAppointments;

        public List<DoctorAvailabilityViewModel> doctorAvailabilities;

    }
}
