using ClinicManagementSystem.ViewModel.Doctor;
using ClinicManagementSystem.ViewModel.Receptionist;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IReceptionistService
    {
        Task<List<ReceptionistViewModel>> GetAllAsync();
        Task<ReceptionistViewModel> GetByIdAsync(string id);
        Task<bool> CreateAsync(CreateReceptionistViewModel model, string password);
        Task<bool> UpdateAsync(ReceptionistViewModel model);
        Task<bool> DeleteAsync(string id);
    }
}
