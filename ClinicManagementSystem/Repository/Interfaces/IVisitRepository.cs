using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Implementations;

namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IVisitRepository : IGenericRepository<Visit>
	{
			List<Visit> GetByAppointmentId(int appointmentId);

	}
}
