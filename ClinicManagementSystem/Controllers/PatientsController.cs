using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    
    public class PatientsController : Controller
    {
        private readonly IPatientService _service;
		private readonly IMapper _mapper;

		public PatientsController(IPatientService service, IMapper mapper)
        {
            _service = service;
			_mapper = mapper;
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

        public IActionResult Create()
        {
            return View(nameof(Create),new PatientViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var patient = _mapper.Map<Patient>(vm);
			_service.AddPatient(patient);

            TempData["Message"] = "Patient created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var patient = _service.GetPatientById(id);
            if (patient == null) return NotFound();

            var vm = _mapper.Map<PatientViewModel>(patient);
			return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

			var patient = _mapper.Map<Patient>(vm);
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
