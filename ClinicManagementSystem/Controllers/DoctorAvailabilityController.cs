using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services.Implementations;
using ClinicManagementSystem.Services.Interfaces;
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

        public DoctorAvailabilityController(IDoctorAvailabilityService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        //GET: /DoctorAvailability/

        public IActionResult Index()
        {
            var list = _service.GetAll();
            return View(list);
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
