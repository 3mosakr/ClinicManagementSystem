using ClinicManagementSystem.Services.Implementations;
using ClinicManagementSystem.Services.Interfaces;
using ClinicManagementSystem.ViewModel.Doctor;
using ClinicManagementSystem.ViewModel.Receptionist;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Controllers
{
    public class ReceptionistsController : Controller
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistsController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }


        public async Task<IActionResult> Index()
        {
            var receptionists = await _receptionistService.GetAllAsync();
            return View(receptionists);    
        }

        public async Task<IActionResult> Details(string id)
        {
            var receptionist = await _receptionistService.GetByIdAsync(id);
            return View(receptionist);
        }

        // Create 
        // GET: Receptionists/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(nameof(Create), new CreateReceptionistViewModel());
        }

        // POST: Receptionists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReceptionistViewModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var succeeded = await _receptionistService.CreateAsync(model, model.Password);
            if (!succeeded)
            {
                ModelState.AddModelError(string.Empty, "Unable to create doctor. Check inputs and try again.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Receptionists/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var receptionist = await _receptionistService.GetByIdAsync(id);
            if (receptionist == null)
                return NotFound();

            return View(receptionist);
        }

        // POST: Receptionists/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReceptionistViewModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var succeeded = await _receptionistService.UpdateAsync(model);
            if (!succeeded)
            {
                ModelState.AddModelError(string.Empty, "Unable to update doctor. Please check the inputs and try again.");
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        // GET: Receptionists/Delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var receptionist = await _receptionistService.GetByIdAsync(id);
            if (receptionist == null)
                return NotFound();

            return View(receptionist); // show confirmation view
        }

        // POST: Receptionists/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var succeeded = await _receptionistService.DeleteAsync(id);
            if (!succeeded)
            {
                // If deletion failed, try to reload the entity to show details/error on the same view.
                var receptionist = await _receptionistService.GetByIdAsync(id);
                if (receptionist == null)
                    return NotFound();

                ModelState.AddModelError(string.Empty, "Unable to delete receptionist. There may be dependent data or an internal error.");
                return View("Delete", receptionist);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
