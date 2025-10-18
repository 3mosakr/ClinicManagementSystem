using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Data;
using ClinicManagementSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repository.Implementations
{
	public class VisitRepository : GenericRepository<Visit>, IVisitRepository
	{
		private readonly ApplicationDbContext _context;

		public VisitRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public List<Visit> GetAllWithDetails()
		{
			return _context.Visits
				.Include(v => v.Appointment)
					.ThenInclude(a => a.Patient)
				.Include(v => v.Appointment)
					.ThenInclude(a => a.Doctor)
				.ToList();
		}

		public Visit GetVisitWithAppointment(int id)
		{
			return _context.Visits
				.Include(v => v.Appointment)
					.ThenInclude(a => a.Patient)
				.Include(v => v.Appointment)
					.ThenInclude(a => a.Doctor)
				.FirstOrDefault(v => v.Id == id);
		}

		public List<Appointment> GetAppointmentsWithDetails(int? appointmentId = null)
		{
			var query = _context.Appointments
				.Include(a => a.Patient)
				.Include(a => a.Doctor)
				.AsQueryable();

			if (appointmentId.HasValue)
				query = query.Where(a => a.Id == appointmentId);

			return query.ToList();
		}
	}
}
