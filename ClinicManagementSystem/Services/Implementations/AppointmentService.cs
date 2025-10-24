using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Implementations;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.Services.Interfaces;

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
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _unitOfWork.AppointmentRepositry.Update(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _unitOfWork.AppointmentRepositry.Delete(appointment);
        }


        public List<Appointment> GetAppointmentsByDoctor(string docId)
        {
            return _unitOfWork.AppointmentRepositry.GetAppointmentsByDoctorId(docId);
        }

        public Appointment GetAppointmentById(int id)
        {
            return _unitOfWork.AppointmentRepositry.GetById(id);
        }

    }
}
