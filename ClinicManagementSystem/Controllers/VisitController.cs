using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.ViewModel.Visit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			var visits = _unitOfWork.VisitRepository.GetAllWithDetails();
			var vm = visits.Select(v => new VisitViewModel
			{
				Id = v.Id,
				AppointmentId = v.AppointmentId,
				Diagnosis = v.Diagnosis,
				Prescription = v.Prescription,
				VisitDate = v.VisitDate,
				DoctorNotes = v.DoctorNotes,
				PatientName = v.Appointment?.Patient?.FullName,
				DoctorName = v.Appointment?.Doctor?.FullName
			}).ToList();
			return View(vm);
        }

		// GET: Visit/Details
		public IActionResult Details(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetVisitWithAppointment(id);
			if (visit == null) return NotFound();


			var vm = new VisitDetailsViewModel
			{
				Id = visit.Id,
				AppointmentId = visit.AppointmentId,
				Diagnosis = visit.Diagnosis,
				Prescription = visit.Prescription,
				VisitDate = visit.VisitDate,
				DoctorNotes = visit.DoctorNotes,
				PatientName = visit.Appointment?.Patient?.FullName,
				DoctorName = visit.Appointment?.Doctor?.FullName,
			};


			return View(vm);
		}

		public IActionResult Create()
		{
			var appointments = _unitOfWork.VisitRepository.GetAppointmentsWithDetails();

			ViewBag.Appointments = appointments.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = $"{a.Patient.FullName} - {a.Doctor.FullName} ({a.Date:yyyy-MM-dd HH:mm})"
			}).ToList();

			return View();
		}


		// POST: Visit/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(VisitViewModel vm)
		{
			if (ModelState.IsValid)
			{
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

			var appointments = _unitOfWork.VisitRepository.GetAppointmentsWithDetails();

			ViewBag.Appointments = appointments.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = $"{a.Patient.FullName} - {a.Doctor.FullName} ({a.Date:yyyy-MM-dd HH:mm})"
			}).ToList();

			return View(vm);


		}

		// GET: Visit/Edit
		public IActionResult Edit(int id)
		{
			var visit = _unitOfWork.VisitRepository.GetVisitWithAppointment(id);
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
			var appointments = _unitOfWork.VisitRepository.GetAppointmentsWithDetails(visit.AppointmentId);
			ViewBag.Appointments = appointments.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = $"{a.Patient.FullName} - {a.Doctor.FullName} ({a.Date:yyyy-MM-dd HH:mm})"
			}).ToList();


			return View(vm);
		}

		// POST: Visit/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(VisitViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var visit = _unitOfWork.VisitRepository.GetById(vm.Id);
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

			var appointments = _unitOfWork.VisitRepository.GetAppointmentsWithDetails();
			ViewBag.Appointments = appointments.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = $"{a.Patient.FullName} - {a.Doctor.FullName} ({a.Date:yyyy-MM-dd HH:mm})"
			}).ToList();

			return View(vm);

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
