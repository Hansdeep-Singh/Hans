using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatnyou.Data;
using chatnyou.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace chatnyou.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var id = _userManager.GetUserId(User);
            ChatUser cu = new ChatUser();
            cu = _db.ChatUsers.Where(cuid => cuid.Id == id).FirstOrDefault();
            cu.IsLoggedIn = false;
            cu.LoggedOn = DateTime.Now;
            _db.ChatUsers.Update(cu);
            await _db.SaveChangesAsync();
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
