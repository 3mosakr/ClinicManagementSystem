using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.ViewModel.DoctorAvailability;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Interfaces;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace ClinicManagementSystem.Services.Implementations
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorAvailabilityService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // Get all as ViewModel
        public List<DoctorAvailabilityViewModel> GetAll(string? doctorId)
        {

            var availabilities = _unitOfWork.DoctorAvailabilityRepository.GetAll();
            if (!string.IsNullOrEmpty(doctorId))
            {
                availabilities = availabilities.Where(a => a.DoctorId == doctorId).ToList();
            }

            var result = availabilities.Select(a => new DoctorAvailabilityViewModel
            {
                Id = a.Id,
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.FullName,
                DayOfWeek = a.DayOfWeek, 
                StartTime = a.StartTime,
                EndTime = a.EndTime
            }).ToList();

            return result;
        }

        public DoctorAvailabilityViewModel GetById(int id)
        {
            var entity = _unitOfWork.DoctorAvailabilityRepository.GetById(id);
            if (entity == null) return null;

            return new DoctorAvailabilityViewModel
            {
                Id = entity.Id,
                DoctorId = entity.DoctorId,
                DoctorName = entity.Doctor?.FullName ?? "Unknown",
                DayOfWeek = entity.DayOfWeek, 
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }
        public void Add(DoctorAvailabilityViewModel model)
        {
            var availability = new DoctorAvailability
            {
                DoctorId = model.DoctorId,
                Doctor = model.DoctorName != null ? new ApplicationUser { FullName = model.DoctorName } : null,
                DayOfWeek = model.DayOfWeek,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };

            _unitOfWork.DoctorAvailabilityRepository.Add(availability);
            _unitOfWork.Save();
        }

        public void Update(DoctorAvailabilityViewModel vm)
        {
            var existing = _unitOfWork.DoctorAvailabilityRepository.GetById(vm.Id);
            if (existing == null) throw new KeyNotFoundException("DoctorAvailability not found.");

            // Use vm.DayOfWeek.ToString() to get the string representation for parsing
            if (!Enum.TryParse<DayOfWeek>(vm.DayOfWeek.ToString(), true, out var day))
            {
                throw new ArgumentException("Invalid DayOfWeek value.");
            }

            existing.DoctorId = vm.DoctorId;
            existing.Doctor = vm.DoctorName != null ? new ApplicationUser { FullName = vm.DoctorName } : null;
            existing.DayOfWeek = day;
            existing.StartTime = vm.StartTime; 
            existing.EndTime = vm.EndTime;     

            _unitOfWork.DoctorAvailabilityRepository.Update(existing);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var existing = _unitOfWork.DoctorAvailabilityRepository.GetById(id);
            if (existing == null) return;
            _unitOfWork.DoctorAvailabilityRepository.Delete(existing);
            _unitOfWork.Save();
        }

        public List<DoctorAvailabilityViewModel> GetByDoctorId(string doctorId)
        {
            var availabilities = _unitOfWork.DoctorAvailabilityRepository.GetByDoctorId(doctorId);

            if (availabilities == null || !availabilities.Any())
                return new List<DoctorAvailabilityViewModel>();

            return availabilities.Select(a => new DoctorAvailabilityViewModel
            {
                Id = a.Id,
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.FullName ?? "Unknown",
                DayOfWeek = a.DayOfWeek,
                StartTime = a.StartTime,
                EndTime = a.EndTime
            }).ToList();
        }
    }
}
