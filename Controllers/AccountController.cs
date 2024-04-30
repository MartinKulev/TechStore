﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using Microsoft.AspNetCore.Identity;
using TechStore.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Data.ViewModels;

namespace TechStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISenderEmail _emailSender;
        private CartService cartService;
        private CategoryService categoryService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ISenderEmail emailSender, 
            CartService cartService,
            CategoryService categoryService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            this.cartService = cartService;
            this.categoryService = categoryService;
        }

        public IActionResult Register()
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.ItemsList = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.ItemsList = categories;

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
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    foreach (int productID in productIDs)
                    {
                        cartService.AddItemToCart(userId, productID);
                    }
                    TempData["Products"] = new List<int>();

                    await _userManager.AddToRoleAsync(user, "User");
                    await SendConfirmationEmail(model.Email, user);

                    return RedirectToAction("VerificationLinkWasSentToYourEmail", "Tech");
                }
                else
                {
                    TempData["Products"] = productIDs;
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

            //Build the Email Confirmation Link which must include the Callback URL
            var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
            new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

            //Send the Confirmation Email to the User Email Id
            await _emailSender.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(ConfirmationLink)}'>clicking here</a>.", true);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Find the User By Id
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            //Call the ConfirmEmailAsync Method which will mark the Email as Confirmed
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Profile", "Tech");
        }

        public IActionResult Login()
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.ItemsList = categories;

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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.ItemsList = categories;

            if (ModelState.IsValid)
            {
                // Sign in the user with SignInManager
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    foreach (int productID in productIDs)
                    {                        
                        cartService.AddItemToCart(userId, productID);
                    }
                    TempData["Products"] = new List<int>();

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
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
            // Sign out the user
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}