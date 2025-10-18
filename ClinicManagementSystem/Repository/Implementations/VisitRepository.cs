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
			return GetAllWithDetails().FirstOrDefault(v => v.Id == id);
		}

		public List<Appointment> GetAppointmentsWithDetails(int? appointmentId = null)
		{
			var query = _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).AsQueryable().Where(a => a.Visit == null);

			if (appointmentId.HasValue)
				query = query.Where(a => a.Id == appointmentId);

			return query.ToList();
		}

		public List<Appointment> GetAppointmentsForEdit(int currentAppointmentId)
		{
			var query = _context.Appointments
				.Include(a => a.Patient)
				.Include(a => a.Doctor)
				.Include(a => a.Visit)
				.AsQueryable();

			var appointments = query.Where(a => a.Visit == null).ToList();

			var currentAppointment = query.FirstOrDefault(a => a.Id == currentAppointmentId);
			if (currentAppointment != null && !appointments.Any(a => a.Id == currentAppointment.Id))
			{
				appointments.Add(currentAppointment);
			}

			return appointments;
		}

	}
}
