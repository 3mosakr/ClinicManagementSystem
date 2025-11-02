using AutoMapper;
using ClinicManagementSystem.Enums;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Doctor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClinicManagementSystem.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public DoctorService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<DoctorViewModel>> GetAllAsync()
        {
            var doctors = await _userManager.GetUsersInRoleAsync(UserRoles.Doctor);
            // mapping 
            var doctorsVM = _mapper.Map<List<DoctorViewModel>>(doctors);

            // get each doctor's specialty claim
            foreach (var doctor in doctors)
            {
                var doctorVM = doctorsVM.FirstOrDefault(d => d.Id == doctor.Id);
                if (doctorVM != null)
                {
                    var claims = await _userManager.GetClaimsAsync(doctor);
                    var specialtyClaim = claims.FirstOrDefault(c => c.Type == StaticData.Specialty);
                    if (specialtyClaim != null)
                    {
                        doctorVM.Specialty = specialtyClaim.Value;
                    }
                }
            }

            return doctorsVM;

        }

        public async Task<DoctorViewModel> GetByIdAsync(string id)
        {
            var doctor = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (doctor == null)
            {
                return null;
            }
           
            // mapping
            var doctorVM = _mapper.Map<DoctorViewModel>(doctor);

            // get doctor claims
            var claims = await _userManager.GetClaimsAsync(doctor);
            var specialtyClaim = claims.FirstOrDefault(c => c.Type == StaticData.Specialty);
            if (specialtyClaim != null)
            {
                doctorVM.Specialty = specialtyClaim.Value;
            }

            return doctorVM;
        }

        // Create new doctor
        public async Task<bool> CreateAsync(CreateDoctorViewModel model, string password)
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
            // Create ApplicationUser instance (Mapping the user data)
            var user = new ApplicationUser
            {
                UserName = model.UserName?.Trim(),
                FullName = model.FullName?.Trim(),
                Email = model.Email?.Trim(),
                PhoneNumber = model.PhoneNumber
            };
            // Create user
            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                return false;
            }
            // Add Doctor role
            var addToRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.Doctor);
            if (!addToRoleResult.Succeeded)
            {
                // Roll back user if role assignment fails
                await _userManager.DeleteAsync(user);
                return false;
            }
            // Add Doctor specialization claim
            var addSpecializationClaimToDoctor = await _userManager.AddClaimAsync(user, new Claim(StaticData.Specialty, model.Specialty));
            if (!addSpecializationClaimToDoctor.Succeeded)
            {
                // Roll back user and role if claim assignment fails
                await _userManager.RemoveFromRoleAsync(user, UserRoles.Doctor);
                await _userManager.DeleteAsync(user);
                return false;
            }

            return true;
        }

        // update
        public async Task<bool> UpdateAsync(DoctorViewModel model)
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

            //Update lockout enabled
            user.LockoutEnabled = model.LockoutEnabled;

            // Update specialization claim
            var updateClaimsResult = await UpdateDoctorClaims(user, model.Specialty!);
            if (!updateClaimsResult)
            {
                return false;
            }

            // Persist other property changes
            var updateResult = await _userManager.UpdateAsync(user);
            return updateResult.Succeeded;
        }

        public async Task<bool> UpdateDoctorClaims(ApplicationUser user, string specialty)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            var specClaim = claims.FirstOrDefault(c => c.Type == StaticData.Specialty);
            if (specClaim != null)
            {
                // Replace existing claim
                var removeResult = await _userManager.RemoveClaimAsync(user, specClaim);
                if (!removeResult.Succeeded)
                {
                    return false;
                }
            }

            var addClaimResult = await _userManager.AddClaimAsync(user, new Claim(StaticData.Specialty, specialty));
            if (!addClaimResult.Succeeded)
            {
                return false;
            }
            //await _signInManager.RefreshSignInAsync(user);
            return true;
        }


        // delete
        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            // include appointments to check for related data that would block deletion
            var user = await _userManager.Users
                .Include(u => u.DoctorAppointments)
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
