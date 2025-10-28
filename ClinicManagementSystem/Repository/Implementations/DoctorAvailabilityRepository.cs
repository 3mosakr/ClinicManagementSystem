using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Data;
using ClinicManagementSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repository.Implementations
{
    public class DoctorAvailabilityRepository : GenericRepository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorAvailabilityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override List<DoctorAvailability> GetAll()
        {
            return _context.DoctorAvailabilities
                           .Include(a => a.Doctor)
                           .ToList();
        }

        public List<DoctorAvailability> GetByDoctorId(string id)
        {
            var availabilities = _context.DoctorAvailabilities.Include(a => a.Doctor).Where(a => a.DoctorId == id).ToList();

            return availabilities;
        }
    }
}
