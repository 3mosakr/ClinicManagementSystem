using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Implementations;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Appointment;

namespace ClinicManagementSystem.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateAppointment(Appointment appointment)
        {
            _unitOfWork.AppointmentRepositry.Add(appointment);
            _unitOfWork.Save();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _unitOfWork.AppointmentRepositry.Update(appointment);
            _unitOfWork.Save();

        }

        public void DeleteAppointment(Appointment appointment)
        {
            _unitOfWork.AppointmentRepositry.Delete(appointment);
            _unitOfWork.Save();

        }


        public List<AppointmentViewModel> GetAppointmentsByDoctor(string docId)
        {
            var appointments= _unitOfWork.AppointmentRepositry.GetAppointmentsByDoctorId(docId);
            return appointments.Select(a => new AppointmentViewModel
            {
                AppointmentId = a.Id,
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor.FullName,
                PatientId = a.PatientId,
                PatientName = a.Patient.FullName,
                AppointmentDateOnly = a.Date.Date,
                AppointmentTimeOnly = a.Date.TimeOfDay,
                Status = a.Status,
                Notes = a.Notes
            }).ToList();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _unitOfWork.AppointmentRepositry.GetById(id);
        }

        public List<AppointmentViewModel> GetAllAppointments()
        {
            var appointments = _unitOfWork.AppointmentRepositry.GetAllWithIncludes();

            return appointments.Select(a => new AppointmentViewModel
            {
                AppointmentId = a.Id,
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor.FullName ,
                PatientId = a.PatientId,
                PatientName = a.Patient.FullName ,
                AppointmentDateOnly = a.Date.Date,
                AppointmentTimeOnly = a.Date.TimeOfDay,
                Status = a.Status,
                Notes = a.Notes
            }).ToList();

            
        }

        public AppointmentViewModel GetAppointment(int id)
        {
            var appointment = _unitOfWork.AppointmentRepositry.GetAppointmentWithIncludes(id);
            return new AppointmentViewModel
            {
                AppointmentId = appointment.Id,
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor.FullName,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient.FullName,
                AppointmentDateOnly = appointment.Date.Date,
                AppointmentTimeOnly = appointment.Date.TimeOfDay,
                Status = appointment.Status,
                Notes = appointment.Notes
            };
        }
    }
}
