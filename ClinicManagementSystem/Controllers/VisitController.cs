using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Visit;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
	public class VisitController : Controller
	{
		private readonly IVisitService _visitService;

		public VisitController(IVisitService visitService)
		{
			_visitService = visitService;
		}

		public IActionResult Index()
		{
			var vm = _visitService.GetAllVisits();
			return View(vm);
		}

		public IActionResult Details(int id)
		{
			var vm = _visitService.GetVisitDetails(id);
			if (vm == null) return NotFound();
			return View(vm);
		}

		public IActionResult Create()
		{
			ViewBag.Appointments = _visitService.GetAppointmentsSelectList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(VisitViewModel vm)
		{
			if (ModelState.IsValid)
			{
				_visitService.CreateVisit(vm);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Appointments = _visitService.GetAppointmentsSelectList();
			return View(vm);
		}

		public IActionResult Edit(int id)
		{
			var vm = _visitService.GetVisitForEdit(id);
			if (vm == null) return NotFound();

			ViewBag.Appointments = _visitService.GetAppointmentsSelectList(vm.AppointmentId);
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(VisitViewModel vm)
		{
			if (ModelState.IsValid)
			{
				_visitService.UpdateVisit(vm);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Appointments = _visitService.GetAppointmentsSelectList();
			return View(vm);
		}

		public IActionResult Delete(int id)
		{
			var vm = _visitService.GetVisitForDelete(id);
			if (vm == null) return NotFound();
			return View(vm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			_visitService.DeleteVisit(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
