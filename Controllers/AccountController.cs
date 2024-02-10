namespace TechStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models;

    public class AccountController : Controller
    {
     
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {

            return RedirectToAction("Index", "Home");
        }
    }