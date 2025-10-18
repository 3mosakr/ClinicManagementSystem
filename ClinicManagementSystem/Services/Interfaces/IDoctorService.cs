using ClinicManagementSystem.ViewModel.Doctor;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IDoctorService
    {

        Task<List<DoctorViewModel>> GetAll();
    }
}
