using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.Doctor;
using ClinicManagementSystem.ViewModel.Receptionist;

namespace ClinicManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            // Add Mapping
            CreateMap<ApplicationUser, DoctorViewModel>().ReverseMap();

            CreateMap<ApplicationUser, ReceptionistViewModel>().ReverseMap();
        }

    }
}
