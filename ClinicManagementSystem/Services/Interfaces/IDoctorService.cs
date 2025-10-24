using ClinicManagementSystem.ViewModel.Doctor;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorViewModel>> GetAllAsync();
        Task<DoctorViewModel> GetByIdAsync(string id);
        Task<bool> CreateAsync(CreateDoctorViewModel model, string password);
        Task<bool> UpdateAsync(DoctorViewModel model);
        Task<bool> DeleteAsync(string id);

    }
}
