using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.ViewModel.Visit;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class VisitController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		public VisitController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
        {
			var visits = _unitOfWork.VisitRepository.GetAll();
			var vm = visits.Select(v => new VisitViewModel
			{
				Id = v.Id,
				AppointmentId = v.AppointmentId,
				Diagnosis = v.Diagnosis,
				Prescription = v.Prescription,
				VisitDate = v.VisitDate,
				DoctorNotes = v.DoctorNotes
			}).ToList();
			return View(vm);
        }

		// GET: Visit/Details
		public IActionResult Details(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return NotFound();


			var vm = new VisitViewModel
			{
				Id = visit.Id,
				AppointmentId = visit.AppointmentId,
				Diagnosis = visit.Diagnosis,
				Prescription = visit.Prescription,
				VisitDate = visit.VisitDate,
				DoctorNotes = visit.DoctorNotes
			};


			return View(vm);
		}

		public IActionResult Create()
		{
			return View(new VisitCreateViewModel());
		}


		// POST: Visit/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(VisitViewModel vm)
		{
			if (!ModelState.IsValid) return View(vm);


			var visit = new Visit
			{
				AppointmentId = vm.AppointmentId,
				Diagnosis = vm.Diagnosis,
				Prescription = vm.Prescription,
				VisitDate = vm.VisitDate,
				DoctorNotes = vm.DoctorNotes
			};


			_unitOfWork.VisitRepository.Add(visit);
			_unitOfWork.Save();


			return RedirectToAction(nameof(Index));
		}

		// GET: Visit/Edit
		public IActionResult Edit(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return NotFound();


			var vm = new VisitViewModel
			{
				Id = visit.Id,
				AppointmentId = visit.AppointmentId,
				Diagnosis = visit.Diagnosis,
				Prescription = visit.Prescription,
				VisitDate = visit.VisitDate,
				DoctorNotes = visit.DoctorNotes
			};


			return View(vm);
		}

		// POST: Visit/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, VisitViewModel vm)
		{
			if (id != vm.Id) return BadRequest();
			if (!ModelState.IsValid) return View(vm);


			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return NotFound();


			visit.AppointmentId = vm.AppointmentId;
			visit.Diagnosis = vm.Diagnosis;
			visit.Prescription = vm.Prescription;
			visit.VisitDate = vm.VisitDate;
			visit.DoctorNotes = vm.DoctorNotes;


			_unitOfWork.VisitRepository.Update(visit);
			_unitOfWork.Save();


			return RedirectToAction(nameof(Index));
		}

		// GET: Visit/Delete
		public IActionResult Delete(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return NotFound();


			var vm = new VisitViewModel
			{
				Id = visit.Id,
				AppointmentId = visit.AppointmentId,
				Diagnosis = visit.Diagnosis,
				Prescription = visit.Prescription,
				VisitDate = visit.VisitDate,
				DoctorNotes = visit.DoctorNotes
			};


			return View(vm);
		}

		// POST: Visit/Delete
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetById(id);
			if (visit == null) return NotFound();


			_unitOfWork.VisitRepository.Delete(visit);
			_unitOfWork.Save();


			return RedirectToAction(nameof(Index));
		}
	}
}
