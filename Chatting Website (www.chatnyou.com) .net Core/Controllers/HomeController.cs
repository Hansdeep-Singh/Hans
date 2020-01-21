using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using chatnyou.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using chatnyou.Data;
using System.Timers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace chatnyou.Controllers
{
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _db;


        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }


        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LognReg lng, string returnUrl)
        {

            returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = lng.Register.UserName, Email = lng.Register.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, lng.Register.Password);
                if (result.Succeeded)
                {
                    var uid = user.Id;
                    ChatUser cu = new ChatUser();
                    Guid g = Guid.NewGuid();
                    cu.Id = uid;
                    cu.ChatUserId = g.ToString();
                    cu.UserName = lng.Register.UserName;
                    cu.IsLoggedIn = true;
                    cu.LoggedOn = DateTime.Now;
                    cu.Age = lng.Register.Age;
                    cu.Country = lng.Register.Country;
                    cu.Sex = lng.Register.Sex;
                    cu.State = lng.Register.State;
                    _db.ChatUsers.Add(cu);
                    await _db.SaveChangesAsync();


                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    //}
                    //else
                    //{
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LognReg lng, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(lng.Login.Email, lng.Login.Password, lng.Login.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var cuser = await _userManager.FindByNameAsync(lng.Login.Email);
                    ChatUser cu = new ChatUser();
                    cu = _db.ChatUsers.Where(cuid => cuid.Id == cuser.Id).FirstOrDefault();
                    cu.IsLoggedIn = true;
                    cu.LoggedOn = DateTime.Now;
                    _db.ChatUsers.Update(cu);
                    await _db.SaveChangesAsync();
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = lng.Login.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    TempData["Error"] = "Incorrect username or password";
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index");
                }
            }
            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index");

        }


        public IActionResult Privacy()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task UpdateUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatUser cu = _db.ChatUsers.Where(id => id.Id == userId).FirstOrDefault();
            cu.IsLoggedIn = false;
            _db.ChatUsers.Update(cu);
            await _db.SaveChangesAsync();
        }
    }
}