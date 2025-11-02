using AutoMapper;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Implementations;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel;
using ClinicManagementSystem.ViewModel.DoctorAvailability;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagementSystem.Controllers
{
    public class DoctorAvailabilityController : Controller
    {
        private readonly IDoctorAvailabilityService _service;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;


		public DoctorAvailabilityController(IDoctorAvailabilityService service, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;

        }

        //GET: /DoctorAvailability/

        public IActionResult Index(string search , int page = 1)
        {
            var list = _service.GetAll();

			int pageSize = 10;

            	if (!string.IsNullOrEmpty(search))
			    {
			    	list = list.Where(p =>p.DoctorName!.ToLower().Contains(search.ToLower())).ToList();
			    }

			    var DoctorVM = _mapper.Map<List<DoctorAvailabilityViewModel>>(list);


			var paged = DoctorVM
				.Skip((page - 1) * pageSize)
		        .Take(pageSize)
		        .ToList();

			    ViewBag.Search = search;
			    ViewBag.CurrentPage = page;
			    ViewBag.TotalPages = (int)Math.Ceiling((double)DoctorVM.Count / pageSize);



			return View(paged);
        }
        // GET: /DoctorAvailability/
        //public async Task<IActionResult> Index()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (user == null)
        //        return Challenge(); 

        //    if (await _userManager.IsInRoleAsync(user, "Admin"))
        //    {
        //        var allAvailabilities = _service.GetAll();
        //        return View(allAvailabilities);
        //    }

        //    if (await _userManager.IsInRoleAsync(user, "Doctor"))
        //    {
        //        var doctorAvailabilities = _service.GetByDoctorId(user.Id);
        //        return View(doctorAvailabilities);
        //    }

        //    // If other roles exist
        //    return Forbid();
        //}


        // GET: /DoctorAvailability/Details/id
        public IActionResult Details(int id)
        {
            var vm = _service.GetById(id);
            if (vm == null)
                return NotFound();

            return View(vm);
        }

        
        public IActionResult Create()
        {
            var doctors = _userManager.GetUsersInRoleAsync("Doctor").Result;

            ViewBag.DoctorList = new SelectList(doctors, "Id", "FullName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorAvailabilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.Add(model);
				TempData["Message"] = "Doctor Availability created successfully!";
				return RedirectToAction(nameof(Index));
            }

            var doctors = _userManager.GetUsersInRoleAsync("Doctor").Result;
            ViewBag.DoctorList = new SelectList(doctors, "Id", "FullName");

            return View(model);
        }


        // GET: /DoctorAvailability/Edit/{id}
        public IActionResult Edit(int id)
        {
            var vm = _service.GetById(id);
            if (vm == null)
                return NotFound();

            var doctors = _userManager.GetUsersInRoleAsync("Doctor").Result;
            ViewBag.DoctorList = new SelectList(doctors, "Id", "FullName", vm.DoctorId);

            return View(vm);
        }

        // POST: /DoctorAvailability/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DoctorAvailabilityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var doctors = _userManager.GetUsersInRoleAsync("Doctor").Result;
                ViewBag.DoctorList = new SelectList(doctors, "Id", "FullName", model.DoctorId);
				return View(model);
            }

            _service.Update(model);
			TempData["Message"] = "Doctor Availability Updated successfully!";
			return RedirectToAction(nameof(Index));
        }

        
        // POST: /DoctorAvailability/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var record = _service.GetById(id);
            if (record == null)
                return NotFound();

            _service.Delete(id);

            TempData["ShowDeleteToast"] = true;

            return RedirectToAction(nameof(Index));
        }

    }
}
