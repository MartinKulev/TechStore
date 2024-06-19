using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Services.Interfaces;
using TechStore.Data.ViewModels;

namespace TechStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService emailSender;
        private ICartService cartService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailSender,
            ICartService cartService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.cartService = cartService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser(model.FirstName, model.LastName, model.Email, model.PhoneNumber);

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
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
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
            new { userID = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

            await this.emailSender.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(ConfirmationLink)}'>clicking here</a>.", true);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userID, string token)
        {
            if (userID == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userID}'.");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
            }

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!User.IsInRole("Admin"))
                    {
                        string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        foreach (int productID in productIDs)
                        {
                            await cartService.AddProductToCartAsync(userID, productID);
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
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}