using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    
    public class PatientsController : Controller
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        public IActionResult Index(string search)
        {
            var patients = _service.GetAllPatients(search);
            ViewBag.Search = search;
            return View(patients);
        }

        public IActionResult Details(int id)
        {
            var patient = _service.GetPatientById(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var patient = new Patient
            {
                FullName = vm.FullName,
                PhoneNumber = vm.PhoneNumber,
                Address = vm.Address,
                Gender = vm.Gender,
                DateOfBirth = vm.DateOfBirth
            };

            _service.AddPatient(patient);
            TempData["Message"] = "Patient created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var patient = _service.GetPatientById(id);
            if (patient == null) return NotFound();

            var vm = new PatientViewModel
            {
                Id = patient.Id,
                FullName = patient.FullName,
                PhoneNumber = patient.PhoneNumber,
                Address = patient.Address,
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var patient = new Patient
            {
                Id = vm.Id,
                FullName = vm.FullName,
                PhoneNumber = vm.PhoneNumber,
                Address = vm.Address,
                Gender = vm.Gender,
                DateOfBirth = vm.DateOfBirth
            };

            _service.UpdatePatient(patient);
            TempData["Message"] = "Patient updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var patient = _service.GetPatientById(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.DeletePatient(id);
            TempData["Message"] = "Patient deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
