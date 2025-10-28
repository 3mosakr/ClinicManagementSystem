using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.DoctorAvailability;

namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IDoctorAvailabilityRepository : IGenericRepository<DoctorAvailability>
    {
        public List<DoctorAvailability> GetByDoctorId(string id);
    }
}
