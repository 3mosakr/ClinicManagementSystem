using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Doctor;
using Microsoft.AspNetCore.Identity;

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

        public async Task<List<DoctorViewModel>> GetAll()
        {
            var doctors = await _userManager.GetUsersInRoleAsync("Doctor");
            // mapping 
            var doctorsVM = _mapper.Map<List<DoctorViewModel>>(doctors);

            return doctorsVM;

        }
    }
}
