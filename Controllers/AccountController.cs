using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models.ViewModels;
using TechStore.Models.Entities;
using TechStore.Data;

public class AccountController : Controller
{
    // Assuming you have a DbContext to interact with your database
    private readonly TechStoreDbContext _context;

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = HashPassword(model.Password) // Make sure to hash the password
            };

            _context.User.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        return View(model);
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == model.Email);
            // Verify the hashed password. The VerifyHashedPassword method is a placeholder for whatever hash verification method you use.
            if (user != null && VerifyHashedPassword(user.PasswordHash, model.Password))
            {
                // Implement your session or authentication mechanism here
                // For example, FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
        }

        return View(model);
    }

    public ActionResult Logout()
    {
        // Implement your logout mechanism here
        // For example, FormsAuthentication.SignOut();
        return RedirectToAction("Index", "Home");
    }

    private string HashPassword(string password)
    {
        // Implement password hashing here
        return password; // Placeholder
    }

    private bool VerifyHashedPassword(string hashedPassword, string password)
    {
        // Implement password verification here
        return true; // Placeholder
    }
}