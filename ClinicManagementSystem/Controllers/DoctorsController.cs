using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Doctor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllAsync();
            return View(doctors);
        }

        public async Task<IActionResult> Details(string id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return View(doctor);
        }

        // Create 
        // GET: Doctors/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(nameof(Create), new CreateDoctorViewModel());
        }

        // POST: Doctors/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorViewModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var succeeded = await _doctorService.CreateAsync(model, model.Password);
            if (!succeeded)
            {
                ModelState.AddModelError(string.Empty, "Unable to create doctor. Check inputs and try again.");
                return View(model);
            }

			TempData["Message"] = "Doctor's Email Created successfully!";

			return RedirectToAction(nameof(Index));
        }



        // GET: Doctors/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        // POST: Doctors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorViewModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var succeeded = await _doctorService.UpdateAsync(model);
            if (!succeeded)
            {
                ModelState.AddModelError(string.Empty, "Unable to update doctor. Please check the inputs and try again.");
                return View(model);
            }
			TempData["Message"] = "Doctor's Email updated successfully!";
			return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        // GET: Doctors/Delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
                return NotFound();

            return View(doctor); // show confirmation view
        }

        // POST: Doctors/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var succeeded = await _doctorService.DeleteAsync(id);
            if (!succeeded)
            {
                // If deletion failed, try to reload the entity to show details/error on the same view.
                var doctor = await _doctorService.GetByIdAsync(id);
                if (doctor == null)
                    return NotFound();

                ModelState.AddModelError(string.Empty, "Unable to delete doctor. There may be dependent data or an internal error.");
                return View("Delete", doctor);
            }
			TempData["ShowDeleteToast"] = true;
			return RedirectToAction(nameof(Index));
        }

    }
}
