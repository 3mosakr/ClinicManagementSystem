using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.Visit;

namespace ClinicManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
			// Add Mapping Visits

			CreateMap<Visit, VisitViewModel>()
			.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FullName))
			.ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.Doctor.FullName));

			CreateMap<Visit, VisitDetailsViewModel>()
			.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FullName))
			.ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.Doctor.FullName));

			CreateMap<VisitViewModel, Visit>()
			.ForMember(dest => dest.Appointment, opt => opt.Ignore()); 


		}

	}
}
