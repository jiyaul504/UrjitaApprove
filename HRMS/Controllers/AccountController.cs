﻿//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using HRMS.Models;

//namespace HRMS.Controllers
//{
//    public class AccountController : Controller
//    {

//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;

//        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }

//        [HttpGet]
//        public IActionResult Login(string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            if (ModelState.IsValid)
//            {
//                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
//                if (result.Succeeded)
//                {
//                    return RedirectToLocal(returnUrl);
//                }
//                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                return View(model);
//            }
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToAction(nameof(HomeController.Index), "Home");
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction(nameof(HomeController.Index), "Home");
//            }
//        }
//    }

//}

