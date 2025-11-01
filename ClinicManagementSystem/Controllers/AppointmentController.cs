using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Implementations;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Appointment;
using ClinicManagementSystem.ViewModel.Doctor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IDoctorAvailabilityService _availabilityService;
        private readonly IPatientService _patientService;

        public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService, IDoctorAvailabilityService availabilityService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _availabilityService = availabilityService;
            _patientService = patientService;
        }


        //public async Task<IActionResult> Index()
        //{
        //    var todaysDate = DateTime.Today;
        //    var doctorsCount= (await _doctorService.GetAllAsync()).ToList().Count();
        //    var patientsCount= _patientService.GetAllPatients().Count();
        //    var curreentAppointmentsCount= _appointmentService.GetAllAppointments().Where(a=>a.AppointmentDateOnly == todaysDate).Count();
        //    var cancelledAppointmentsCount= _appointmentService.GetAllAppointments().Where(a=>a.Status=="Canceled").Count();

        //    TotalCountOfDataViewModel totalCountOfData = new TotalCountOfDataViewModel
        //    {
        //        TotalDoctors = doctorsCount,
        //        TotalPatients = patientsCount,
        //        TotalCurrentAppointments = curreentAppointmentsCount,
        //        TotalCancelAppointments = cancelledAppointmentsCount
        //    };
        //    return View("Index",totalCountOfData);
        //}

        public async Task<IActionResult> ShowAllDoctors()
        {
            var doctors = await _doctorService.GetAllAsync(); /////////////////////////    محتاجة اعمل mapping خاص بيا
            return View("ShowAllDoctors", doctors);
        }

        public IActionResult ViewAppointmentsOfDoctor(string id)
        {
            var availabilitiesOfDoctor = _availabilityService.GetByDoctorId(id);
            if (availabilitiesOfDoctor == null)
                return NotFound();

            return View("ViewAppointmentsOfDoctor", availabilitiesOfDoctor);
        }

        public IActionResult ViewAppointmentsOfDoctorForPatients(string id)
        {
            var appointments = _appointmentService.GetAppointmentsByDoctor(id);
            return View("ViewAppointmentsOfDoctorForPatients", appointments);

        }

        public IActionResult ShowAllPatients()
        {
            var patients = _patientService.GetAllPatients();
            return View("ShowAllPatients");
        }

        public IActionResult Index()
        {
            var appointments = _appointmentService.GetAllAppointments();
            return View("ShowAllAppointments", appointments);
        }
        public async Task<IActionResult> CreateAppointment(string? id)
        {
            var appointmentWithDoctor = new AppointmentViewModel
            {
                Doctors = new List<DoctorViewModel>()
            };

            if (string.IsNullOrEmpty(id))
            {
                var model = await _doctorService.GetAllAsync();
                appointmentWithDoctor.Doctors = model;

            }
            else
            {
                var doctor = await _doctorService.GetByIdAsync(id);
                if (doctor == null)
                {
                    return NotFound();
                }
                else
                {
                    appointmentWithDoctor.Doctors.Add(doctor);
                }
            }
            return View("CreateAppointment", appointmentWithDoctor);
        }



        [HttpGet]
        public JsonResult SearchPatients(string term)
        {
            var patients = _patientService.GetPatientByName(term);
            var result = patients.Select(p => new
            {
                id = p.Id,
                name = p.FullName
            }).ToList();

            return Json(result);
        }




        [HttpGet]
        public JsonResult GetDoctorAvailability(string doctorId)
        {
            var availabilities = _availabilityService.GetByDoctorId(doctorId)
                .Select(a => new
                {
                    day = a.DayOfWeek.ToString(),
                    start = a.StartTime.ToString(@"hh\:mm"),
                    end = a.EndTime.ToString(@"hh\:mm")
                })
                .ToList();

            return Json(availabilities);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckDateNotPast(DateTime appointmentDateOnly)
        {
            if (appointmentDateOnly.Date < DateTime.Today)
            {
                return Json("Appointment date cannot be in the past.");
            }

            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckAvailableTime(DateTime? appointmentDateOnly, TimeSpan? appointmentTimeOnly, string doctorId)
        {
            if (appointmentDateOnly == null || appointmentTimeOnly == null || string.IsNullOrEmpty(doctorId))
            {
                return Json("Please select doctor, date and time.");
            }

            var availabilities = _availabilityService.GetByDoctorId(doctorId);
            if (availabilities == null || availabilities.Count == 0)
            {
                return Json("This doctor has no availability set.");
            }

            var selectedDay = appointmentDateOnly.Value.DayOfWeek;
            var availability = availabilities.FirstOrDefault(a => a.DayOfWeek == selectedDay);

            if (availability == null)
            {
                return Json("Doctor is not available on this day.");
            }

            var start = availability.StartTime;
            var end = availability.EndTime;
            var selectedTime = appointmentTimeOnly.Value;

            if (selectedTime < start || selectedTime > end)
            {
                return Json($"Doctor works from {start} to {end}.");
            }

            return Json(true);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppointment(AppointmentViewModel appointmentView)
        {

            var patient = _patientService.GetPatientById(appointmentView.PatientId);


            if (!ModelState.IsValid)
            {
                appointmentView.Doctors = (await _doctorService.GetAllAsync()).ToList();
                appointmentView.PatientName = patient.FullName;
                return View("CreateAppointment", appointmentView);
            }



            var appointment = new Appointment
            {
                ReceptionistId = "910334b1-3b13-4787-9fec-fc818d8611d0",
                DoctorId = appointmentView.DoctorId,
                PatientId = appointmentView.PatientId,
                Date = appointmentView.AppointmentDateOnly.Value.Date + appointmentView.AppointmentTimeOnly.Value,
                Status = "Scheduled"
            };

            try
            {
                _appointmentService.CreateAppointment(appointment);
                return RedirectToAction("ShowAllAppointments");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the appointment: " + ex.InnerException?.Message);
                appointmentView.Doctors = (await _doctorService.GetAllAsync()).ToList();
                appointmentView.PatientName = patient.FullName;
                return View("CreateAppointment", appointmentView);
            }
        }



        public async Task<IActionResult> EditAppointment(int id)
        {
            var appointmentView = _appointmentService.GetAppointment(id);
            appointmentView.Doctors = (await _doctorService.GetAllAsync()).ToList();
            if (appointmentView == null)
            {
                return NotFound();
            }
            return View("EditAppointment", appointmentView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAppointment(AppointmentViewModel appointmentView)
        {
            if (!ModelState.IsValid)
            {
                appointmentView.Doctors = (await _doctorService.GetAllAsync()).ToList();
                return View("EditAppointment", appointmentView);
            }

            if (appointmentView.AppointmentId == null)
            {
                ModelState.AddModelError("", "Invalid appointment ID.");
                appointmentView.Doctors = (await _doctorService.GetAllAsync()).ToList();
                return View("EditAppointment", appointmentView);
            }

            try
            {
                var appointment = new Appointment
                {
                    Id = appointmentView.AppointmentId.Value,
                    ReceptionistId = "e16e76bf-f757-4c93-871a-3db69a87c2aa", 
                    DoctorId = appointmentView.DoctorId,
                    PatientId = appointmentView.PatientId,
                    Date = appointmentView.AppointmentDateOnly.Value.Date + appointmentView.AppointmentTimeOnly.Value,
                    Status = appointmentView.Status,
                    Notes = appointmentView.Notes
                };

                _appointmentService.UpdateAppointment(appointment);

                TempData["SuccessMessage"] = "Appointment updated successfully!";
                return RedirectToAction("ShowAllAppointments");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error while updating appointment: {ex.Message}");
                appointmentView.Doctors = (await _doctorService.GetAllAsync()).ToList();
                return View("EditAppointment", appointmentView);
            }
        }

        public async Task<IActionResult> ViewAppointment(int id)
        {
            var appointmentView = _appointmentService.GetAppointment(id);
            appointmentView.Doctors = (await _doctorService.GetAllAsync()).ToList();
            if (appointmentView == null)
            {
                return NotFound();
            }
            return View("ViewAppointment", appointmentView);
        }

        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _appointmentService.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            try
            {
                _appointmentService.DeleteAppointment(appointment);
                TempData["SuccessMessage"] = "Appointment deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error while deleting appointment: {ex.Message}";
            }
            return RedirectToAction("ShowAllAppointments");
        }



    }
}
