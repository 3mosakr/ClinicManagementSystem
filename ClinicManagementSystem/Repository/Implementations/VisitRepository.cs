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
				.AsNoTracking()
				.Include(v => v.Appointment)
					.ThenInclude(a => a.Patient)
				.Include(v => v.Appointment)
					.ThenInclude(a => a.Doctor)
				.ToList();
		}

		public List<Appointment> GetAppointmentsWithDetails(int? Appointment = null)
		{
			var query = _context.Appointments
				.Include(a => a.Patient)
				.Include(a => a.Doctor)
				.Include(a => a.Visit)
				.AsNoTracking().AsNoTracking();

			if (Appointment.HasValue)
			{
				int? appointmentId = Appointment.Value;
				query = query.Where(a => a.Id == Appointment.Value);
			}
			else
			{
				query = query.Where(a => a.Visit == null);
			}
			return query.ToList();
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

	}
}
