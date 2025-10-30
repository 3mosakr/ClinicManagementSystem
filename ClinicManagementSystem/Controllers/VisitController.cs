using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Visit;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
	public class VisitController : Controller
	{
		private readonly IVisitService _visitService;
		private readonly IMapper _mapper;

		public VisitController(IVisitService visitService, IMapper mapper)
		{
			_visitService = visitService;
			_mapper = mapper;
		}

		// =======================
		// GET: Visit/Index
		// =======================
		public IActionResult Index(string? searchTerm, int page = 1)
		{
			//added pagination and search
			var vm = _visitService.GetAllVisits();
			if (!string.IsNullOrEmpty(searchTerm))
			{
				vm = vm.Where(v => v.PatientName!.ToLower().Contains(searchTerm.ToLower())
								|| v.Diagnosis!.ToLower().Contains(searchTerm.ToLower())
								|| v.Prescription!.ToLower().Contains(searchTerm.ToLower())
								|| v.DoctorName!.ToLower().Contains(searchTerm.ToLower()))
					   .ToList();
			}
			int pageSize = 10;
			var pagedVisits = vm.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = (int)Math.Ceiling((double)vm.Count / pageSize);
			ViewBag.SearchTerm = searchTerm;
			return View(pagedVisits);
		}

		// =======================
		// GET: Visit/Details
		// =======================
		public IActionResult Details(int id)
		{
			var vm = _visitService.GetVisitDetails(id);
			if (vm == null) return NotFound();

			return View(vm);
		}

		// =======================
		// GET: Visit/Create
		// =======================
		public IActionResult Create()
		{
			ViewBag.Appointments = _visitService.GetAppointmentsSelectList();
			return View();
		}

		// =======================
		// POST: Visit/Create
		// =======================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(VisitCreateViewModel vm)
		{
			if (ModelState.IsValid)
			{
				_visitService.CreateVisit(vm);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Appointments = _visitService.GetAppointmentsSelectList();
			return View(vm);
		}

		// =======================
		// GET: Visit/Edit
		// =======================
		public IActionResult Edit(int id)
		{
			var visit = _visitService.GetVisitForEdit(id);
			if (visit == null) return NotFound();
			var vm = _mapper.Map<VisitViewModel>(visit);

			ViewBag.Appointments = _visitService.GetAppointmentsSelectListForEdit(vm.AppointmentId);
			return View(vm);

		}

		// =======================
		// POST: Visit/Edit
		// =======================
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

		// =======================
		// POST: Visit/Delete
		// =======================
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			_visitService.DeleteVisit(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
