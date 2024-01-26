using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers
{
    public class TechController : Controller
    {
        public IActionResult Homepage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Homepage(string optionButton)
        {
            if (optionButton == "laptopsButton")
            {
                return RedirectToAction("Laptops");
            }
            else if (optionButton == "smartphonesButton")
            {
                return RedirectToAction("Smartphones");
            }
            else if (optionButton == "tabletsButton")
            {
                return RedirectToAction("Tablets");
            }
            else if (optionButton == "tvsButton")
            {
                return RedirectToAction("TVs");
            }
            else if (optionButton == "monitorsButton")
            {
                return RedirectToAction("Monitors");
            }
            else if (optionButton == "keyboardsButton")
            {
                return RedirectToAction("Keyboards");
            }
            else if (optionButton == "miceButton")
            {
                return RedirectToAction("Mice");
            }
            else if (optionButton == "headphonesButton")
            {
                return RedirectToAction("Headphones");
            }
            else if (optionButton == "speakersButton")
            {
                return RedirectToAction("Speakers");
            }
            return View();
        }

        public IActionResult Laptops()
        {
            return View();
        }
        public IActionResult Smartphones()
        {
            return View();
        }
        public IActionResult Tablets()
        {
            return View();
        }
        public IActionResult TVs()
        {
            return View();
        }
        public IActionResult Monitors()
        {
            return View();
        }
        public IActionResult Keyboards()
        {
            return View();
        }
        public IActionResult Mice()
        {
            return View();
        }
        public IActionResult Headphones()
        {
            return View();
        }
        public IActionResult Speakers()
        {
            return View();
        }
    }
}
