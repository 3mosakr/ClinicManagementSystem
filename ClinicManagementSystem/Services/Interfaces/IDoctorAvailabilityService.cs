using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.DoctorAvailability;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IDoctorAvailabilityService
    {
        List<DoctorAvailabilityViewModel> GetAll(string? doctorId);
        List<DoctorAvailabilityViewModel> GetByDoctorId(string id); 
        DoctorAvailabilityViewModel GetById(int id);
        void Add(DoctorAvailabilityViewModel doctorAvailability);
        void Update(DoctorAvailabilityViewModel doctorAvailability);
        void Delete(int id);
    }
}
