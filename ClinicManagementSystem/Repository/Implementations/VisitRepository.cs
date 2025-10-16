using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Data;
using ClinicManagementSystem.Repository.Interfaces;

namespace ClinicManagementSystem.Repository.Implementations
{
    public class VisitRepository : GenericRepository<Visit>, IVisitRepository
    {
        private readonly ApplicationDbContext _context;
		public VisitRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

		public List<Visit> GetByAppointmentId(int appointmentId)
		{
			return _context.Visits.Where(v => v.AppointmentId == appointmentId).ToList();
		}
	}
}
