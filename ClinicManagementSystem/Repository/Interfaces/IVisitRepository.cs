using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Implementations;

namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IVisitRepository : IGenericRepository<Visit>
	{
			List<Visit> GetAllWithDetails();
			public Visit GetVisitWithAppointment(int id);
			List<Appointment> GetAppointmentsWithDetails(int? Appointment = null);
			List<Appointment> GetAppointmentsForEdit(int currentAppointmentId);

			
			void UpdateAppointmentStatus(int appointmentId, string status);


    }
}
