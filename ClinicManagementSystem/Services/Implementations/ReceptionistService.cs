using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Doctor;
using ClinicManagementSystem.ViewModel.Receptionist;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Services.Implementations
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ReceptionistService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<ReceptionistViewModel>> GetAllAsync()
        {
            var receptionists = await _userManager.GetUsersInRoleAsync("Receptionist");
            // mapping 
            var receptionistsVM = _mapper.Map<List<ReceptionistViewModel>>(receptionists);

            return receptionistsVM;
        }

        public async Task<ReceptionistViewModel> GetByIdAsync(string id)
        {
            var receptionist = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (receptionist == null)
            {
                return null;
            }
            // mapping
            var receptionistVM = _mapper.Map<ReceptionistViewModel>(receptionist);

            return receptionistVM;
        }

        public async Task<bool> CreateAsync(CreateReceptionistViewModel model, string password)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password is required", nameof(password));

            // Prevent duplicates
            if (!string.IsNullOrWhiteSpace(model.UserName)
                && await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(model.Email)
                && await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return false;
            }

            var receptionist = new ApplicationUser
            {
                UserName = model.UserName?.Trim(),
                FullName = model.FullName?.Trim(),
                Email = model.Email?.Trim(),
                PhoneNumber = model.PhoneNumber
            };

            var createResult = await _userManager.CreateAsync(receptionist, password);
            if (!createResult.Succeeded)
            {
                return false;
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(receptionist, "Receptionist");
            if (!addToRoleResult.Succeeded)
            {
                // Roll back user if role assignment fails
                await _userManager.DeleteAsync(receptionist);
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(ReceptionistViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return false;
            }

            // Update simple fields directly
            user.FullName = string.IsNullOrWhiteSpace(model.FullName) ? user.FullName : model.FullName.Trim();
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;

            // Update user name if changed (uses UserManager to keep normalization and checks)
            if (!string.IsNullOrWhiteSpace(model.UserName) &&
                !string.Equals(user.UserName, model.UserName, StringComparison.OrdinalIgnoreCase))
            {
                var userNameResult = await _userManager.SetUserNameAsync(user, model.UserName);
                if (!userNameResult.Succeeded)
                {
                    return false;
                }
            }

            // Update email if changed (uses UserManager to keep normalization and checks)
            if (!string.IsNullOrWhiteSpace(model.Email) &&
                !string.Equals(user.Email, model.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!emailResult.Succeeded)
                {
                    return false;
                }
            }

            // Persist other property changes
            var updateResult = await _userManager.UpdateAsync(user);
            return updateResult.Succeeded;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            // include appointments to check for related data that would block deletion
            var user = await _userManager.Users
                .Include(u => u.ReceptionistAppointments)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return false;

            // Prevent deleting a doctor that still has appointments to avoid FK constraint errors.
            // If you prefer to cascade or reassign appointments, handle that here using your DbContext.
            if (user.DoctorAppointments != null && user.DoctorAppointments.Any())
            {
                return false;
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            return deleteResult.Succeeded;
        }

        
    }
}
