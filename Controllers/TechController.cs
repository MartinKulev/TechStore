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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }

        public IActionResult Laptops(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult Smartphones(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult Tablets(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult TVs(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult Monitors(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult Keyboards(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult Mice(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult Headphones(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult Speakers(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        public IActionResult ShoppingCart(string optionButton)
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
            else if (optionButton == "shoppingCartButton")
            {
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
    }
}
