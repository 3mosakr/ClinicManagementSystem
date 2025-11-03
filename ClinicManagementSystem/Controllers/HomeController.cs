using System.Diagnostics;
using ClinicManagementSystem.Enums;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.Services.Implementations;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Appointment;
using ClinicManagementSystem.ViewModel.DoctorAvailability;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IDoctorAvailabilityService _availabilityService;

        public HomeController(ILogger<HomeController> logger, IPatientService patientService, IAppointmentService appointmentService, IDoctorService doctorService, IDoctorAvailabilityService availabilityService)
        {
            _logger = logger;
            _patientService = patientService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _availabilityService = availabilityService;
        }

        public async Task<IActionResult> Index()
        {
            var todaysDate = DateTime.Today;
            var doctorsCount = (await _doctorService.GetAllAsync()).ToList().Count();
            var patientsCount = _patientService.GetAllPatients().Count();
            var curreentAppointmentsCount = _appointmentService.GetAllAppointments().Where(a => a.AppointmentDateOnly == todaysDate).Count();
            var cancelledAppointmentsCount = _appointmentService.GetAllAppointments().Where(a => a.Status == "Canceled").Count();
            var TodayAppointments = _appointmentService.GetAllAppointments().Where(a => a.AppointmentDateOnly == todaysDate).ToList();
            var DoctorAvalibalityToday = _availabilityService.GetAll().Where(a => a.DayOfWeek == todaysDate.DayOfWeek).ToList();
            // get doctor claims for speciality and add it to doctor 
            List<AvailableDoctorWithSpecialityViewModel> doctorWithSpeciality = new List<AvailableDoctorWithSpecialityViewModel>();
            foreach (var availability in DoctorAvalibalityToday)
            {
                var doctor = await _doctorService.GetByIdAsync(availability.DoctorId);
                if (doctor != null)
                {
                    AvailableDoctorWithSpecialityViewModel doctorWithSpec = new AvailableDoctorWithSpecialityViewModel
                    {
                        Id = availability.Id,
                        DoctorId = availability.DoctorId,
                        DoctorName = doctor.FullName,
                        DayOfWeek = availability.DayOfWeek,
                        StartTime = availability.StartTime,
                        EndTime = availability.EndTime,
                        Speciality = doctor.Specialty
                    };
                    doctorWithSpeciality.Add(doctorWithSpec);
                }

            }


            TotalCountOfDataViewModel totalCountOfData = new TotalCountOfDataViewModel
            {
                TotalDoctors = doctorsCount,
                TotalPatients = patientsCount,
                TotalCurrentAppointments = curreentAppointmentsCount,
                TotalCancelAppointments = cancelledAppointmentsCount,
                CurrentAppointments = TodayAppointments,
                doctorAvailabilities = doctorWithSpeciality
            };
            return View("Index", totalCountOfData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
