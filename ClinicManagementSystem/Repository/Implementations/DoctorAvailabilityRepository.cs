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

        public override DoctorAvailability GetById(object id)
        {
            var intId = Convert.ToInt32(id);
            return _context.DoctorAvailabilities
                           .Include(a => a.Doctor)
                           .FirstOrDefault(a => a.Id == intId);
        }
    }
}
