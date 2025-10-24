using ClinicManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;


        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }


        public IActionResult Index() {
            return View("Index");
        }

        public IActionResult ShowAllDoctors()
        {
            return View("ShowAllDoctors");
        }

        public IActionResult ShowAllPatients() 
        { 
            return View("ShowAllPatients");
        }

        public IActionResult ShowAllAppointments() 
        { 
            return View("ShowAllAppointments");
        }
        public IActionResult CreateAppointment()
        {
            return View("CreateAppointment");
        }


        //[HttpGet("appointments/{id}")]
        //public IActionResult ShowAllAppointments(string id)
        //{
        //    var appointments = appointmentService.GetAppointmentsByDoctor(id);
        //    return View("ShowAllAppointments", appointments);
        //}


        //[HttpGet("appointment/{id}")]
        //public IActionResult ShowAppointmentDetails(int id)
        //{
        //    var appointment = appointmentService.GetAppointmentById(id);
        //    return View("ShowAppointmentDetails", appointment);
        //}

    }
}
