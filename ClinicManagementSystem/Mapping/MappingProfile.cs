using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.Doctor;
using ClinicManagementSystem.ViewModel.Receptionist;
using ClinicManagementSystem.ViewModel.Visit;

namespace ClinicManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            // Add Mapping
            CreateMap<ApplicationUser, DoctorViewModel>().ReverseMap();

            CreateMap<ApplicationUser, ReceptionistViewModel>().ReverseMap();

			// Add Mapping Visits

			CreateMap<Visit, VisitViewModel>()
			.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FullName))
			.ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.Doctor.FullName))
			.ForMember(dest => dest.VisitDateDate, opt => opt.MapFrom(src => src.VisitDate.HasValue ? src.VisitDate.Value.ToString("yyyy-MM-dd") : null))
			.ForMember(dest => dest.VisitDateTime, opt => opt.MapFrom(src => src.VisitDate.HasValue ? src.VisitDate.Value.ToString("HH:mm") : null));

			CreateMap<Visit, VisitDetailsViewModel>()
			.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Appointment.Patient.FullName))
			.ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.Doctor.FullName));

			CreateMap<VisitViewModel, Visit>()
			.ForMember(dest => dest.Appointment, opt => opt.Ignore())
			.ForMember(dest=>dest.VisitDate, opt=>opt.MapFrom(src=>
			(!string.IsNullOrEmpty(src.VisitDateDate) && !string.IsNullOrEmpty(src.VisitDateTime)) 
				? DateTime.ParseExact($"{src.VisitDateDate} {src.VisitDateTime}", "yyyy-MM-dd HH:mm", null) 
				: (DateTime?)DateTime.Now
			));
		}

	}
}
