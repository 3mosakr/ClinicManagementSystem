using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.Doctor;

namespace ClinicManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            // Add Mapping
            CreateMap<ApplicationUser, DoctorViewModel>().ReverseMap();
        }

    }
}
