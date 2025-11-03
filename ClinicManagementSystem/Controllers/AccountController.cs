using ClinicManagementSystem.Enums;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        #region Login
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel userFromReq)
        {
            if (ModelState.IsValid)
            {
                //check 
                ApplicationUser userFromDb = await _userManager.FindByNameAsync(userFromReq.UserName);
                if (userFromDb != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userFromDb, userFromReq.Password);
                    if (found)
                    {
                        //create cookie(id,name,role,email)
                        //await _signInManager.SignInAsync(userFromDb, userFromReq.RememberMe);
                        // Get user Claims
                        var userClaims = await _userManager.GetClaimsAsync(userFromDb);
                        var userRoles = await _userManager.GetRolesAsync(userFromDb);
                        // Add specialty claim if role is doctor
                        var Claims = new List<Claim>(userClaims)
                        {
                            new Claim(ClaimTypes.NameIdentifier, userFromDb.Id),
                            new Claim(ClaimTypes.Name, userFromDb.UserName),
                            new Claim(ClaimTypes.Role, userRoles[0]),
                            new Claim(ClaimTypes.Email, userFromDb.Email ?? ""),
                            new Claim("FullName", userFromDb.FullName)
                        };

                        await _signInManager.SignInWithClaimsAsync(userFromDb, userFromReq.RememberMe, Claims);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return View("Login", userFromReq);
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        // change password
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.OldPassword, changePasswordVM.NewPassword);
                    if (result.Succeeded)
                    {
                        await _signInManager.RefreshSignInAsync(user);
                        ViewBag.IsSuccess = true;
                        ModelState.Clear();
                        //return View();
                        return RedirectToAction("Logout", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return View(changePasswordVM);

        }
    }
}
