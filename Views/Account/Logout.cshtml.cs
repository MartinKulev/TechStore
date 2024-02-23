// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using TechStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechStore.Views.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPost(string returnUrl = "")
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage(returnUrl);
        }
    }
}
