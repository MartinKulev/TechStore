﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Data.ViewModels;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailSender;
        private ICartService cartService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailSender,
            ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            this.cartService = cartService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await SendConfirmationEmail(model.Email, user);

                    TempData["Message"] = "A verification link was sent to your email!";
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        private async Task SendConfirmationEmail(string? email, ApplicationUser? user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
            new { userID = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(ConfirmationLink)}'>clicking here</a>.", true);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userID, string token)
        {
            if (userID == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userID}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Account", "Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!User.IsInRole("Admin"))
                    {
                        string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        foreach (int productID in productIDs)
                        {
                            cartService.AddProductToCart(userID, productID);
                        }
                    }
                    TempData["Products"] = new List<int>();

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        TempData["Message"] = "Welcome back!";
                        return RedirectToAction("Profile", "Tech");
                    }
                }
                else
                {
                    TempData["Products"] = productIDs;
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}