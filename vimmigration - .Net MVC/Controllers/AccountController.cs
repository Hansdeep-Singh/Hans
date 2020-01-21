using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VeraCityImmigration.Models;
using System.IO;
using System.Collections.Generic;
using Channel.Logic;
//ID IS ALWAYS INTEGER
namespace VeraCityImmigration.Controllers
{
    [Authorize(Roles="User")]
    public class AccountController : Controller
    {
        private ClientDetailRepository cdr = new ClientDetailRepository();
        private DocumentRepository dr = new DocumentRepository();
        private AdminClientDetailRepository acdr = new AdminClientDetailRepository();
        private ApplicationSignInManager _signInManager;
        private ClientManager _userManager;
        private Logic l = new Logic();
       
        public AccountController()
        {
        }

        public AccountController(ClientManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }
       [HttpGet]
        public ActionResult EditInfo(string id)
        {
            ClientDetailViewModel cdvm = new ClientDetailViewModel();
            ClientDetail cd = new ClientDetail();
            cd = cdr.OneRow(id);
            cdvm.ClientDetailId = cd.ClientDetailId;
            cdvm.ClientId =  cd.ClientId;
            cdvm.FirstName = cd.FirstName;
            cdvm.Lastname = cd.Lastname;
            cdvm.dob = cd.dob;
            cdvm.address = cd.address;
            cdvm.phone = cd.phone;
            cdvm.visatype = cd.visatype;
            cdvm.visaduedate = cd.visaduedate;
            cdvm.passportnumber = cd.passportnumber;
            cdvm.passportcountry = cd.passportcountry;
            cdvm.passportexpirydate = cd.passportexpirydate;
            cdvm.policeclearanceexpiry = cd.policeclearanceexpiry;
            cdvm.medicalexpiry = cd.medicalexpiry;
            cdvm.photo = cd.photo;
            cdvm.photothumb = cd.photothumb;
            cdvm.photoraw = cd.photoraw;
            return View(cdvm);
        }
        [HttpPost]
        public ActionResult EditInfo(ClientDetailViewModel cdvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cdvm);
            }
            ClientDetail cd = new ClientDetail();
            //cd = cdr.OneRow(cdvm.ClientId);
            //   cdr.UpdateClient(cd, cdvm);

            cd.ClientDetailId = cdvm.ClientDetailId;
            cd.ClientId = cdvm.ClientId;
            cd.FirstName = cdvm.FirstName;
            cd.Lastname = cdvm.Lastname;
            cd.dob = cdvm.dob;
            cd.address = cdvm.address;
            cd.phone = cdvm.phone;
            cd.visatype = cdvm.visatype;
            cd.visaduedate = cdvm.visaduedate;
            cd.passportcountry = cdvm.passportcountry;
            cd.passportexpirydate = cdvm.passportexpirydate;
            cd.passportnumber = cdvm.passportnumber;
            cd.policeclearanceexpiry = cdvm.policeclearanceexpiry;
            cd.medicalexpiry = cdvm.medicalexpiry;
            cd.photo = cdvm.photo;
            cd.photoraw = cdvm.photoraw;
            cd.photothumb = cdvm.photothumb;
            cdr.EditClient(cd);
            return RedirectToAction("Welcome", new { id = cd.ClientId });
        }

        [HttpGet]
        public ActionResult ChangePhoto(string id)
        {
            ClientDetailViewModel cdvm = new ClientDetailViewModel();
            ClientDetail cd = new ClientDetail();
            cd = cdr.OneRow(id);
            cdvm.ClientDetailId = cd.ClientDetailId;
            cdvm.ClientId = cd.ClientId;
            cdvm.FirstName = cd.FirstName;
            cdvm.Lastname = cd.Lastname;
            cdvm.dob = cd.dob;
            cdvm.address = cd.address;
            cdvm.phone = cd.phone;
            cdvm.visatype = cd.visatype;
            cdvm.visaduedate = cd.visaduedate;
            cdvm.passportnumber = cd.passportnumber;
            cdvm.passportcountry = cd.passportcountry;
            cdvm.passportexpirydate = cd.passportexpirydate;
            cdvm.policeclearanceexpiry = cd.policeclearanceexpiry;
            cdvm.medicalexpiry = cd.medicalexpiry;
            return View(cdvm);
        }
        [HttpPost]
        public ActionResult ChangePhoto(ClientDetailViewModel cdvm)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                cdvm.file.InputStream.CopyTo(ms);
                ResizeImage ri = new ResizeImage();
                cdvm.photoraw = ri.ResizeFromStream(750, ms, "Raw750");
                cdvm.photo = ri.ResizeFromStream(450, ms, "Photo450");
                cdvm.photothumb = ri.ResizeFromStream(200, ms, "Photo200");
            }

            ClientDetail cd = new ClientDetail();

            cd.ClientDetailId = cdvm.ClientDetailId;
            cd.ClientId = cdvm.ClientId;
            cd.FirstName = cdvm.FirstName;
            cd.Lastname = cdvm.Lastname;
            cd.dob = cdvm.dob;
            cd.address = cdvm.address;
            cd.phone = cdvm.phone;
            cd.visatype = cdvm.visatype;
            cd.visaduedate = cdvm.visaduedate;
            cd.passportcountry = cdvm.passportcountry;
            cd.passportexpirydate = cdvm.passportexpirydate;
            cd.passportnumber = cdvm.passportnumber;
            cd.policeclearanceexpiry = cdvm.policeclearanceexpiry;
            cd.medicalexpiry = cdvm.medicalexpiry;
            cd.photoraw = cdvm.photoraw;
            cd.photo = cdvm.photo;
            cd.photothumb = cdvm.photothumb;
            

        cdr.EditClient(cd);
           // cdr.UpdateClient(cd, cdvm);
            return RedirectToAction("Welcome", new { id = cd.ClientId });


           
        }

        public ActionResult EditDocument(string id)
        {
            List<DocumentViewModel> Documents = new List<DocumentViewModel>();
            foreach (var d in dr.Documents(id).OrderBy(n=>n.DocumentName))
            {
             DocumentViewModel dvml = new DocumentViewModel();
             dvml.ClientId = d.ClientId;
             dvml.DocumentId = d.DocumentId;
             dvml.DocumentName = d.DocumentName;
             dvml.DocumentType = d.DocumentType;
             dvml.document = d.document;
             Documents.Add(dvml);
            }

            DocumentViewModel dvm = new DocumentViewModel();
            dvm.ClientId = id;
            ViewBag.Documents = Documents;
            return View(dvm);
        }
        [HttpPost]
        public ActionResult EditDocument(DocumentViewModel dvm)
        {


            if (dvm.file == null)
            {
                PhotoString ps = new PhotoString();
                byte[] myByte = Convert.FromBase64String(ps.stringforbyte);
                
                dvm.document = myByte;
            }

            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    dvm.file.InputStream.CopyTo(ms);
                    dvm.document = ms.ToArray();
                }
            }
            string ext = Path.GetExtension(dvm.file.FileName);
            Document d = new Document();
            d.ClientId = dvm.ClientId;
            d.DocumentId = dvm.DocumentId;
            d.DocumentName = dvm.DocumentName;
            d.DocumentType = ext;
            d.document = dvm.document;
            dr.AddDocument(d);
            return RedirectToAction("EditDocument", new { id = dvm.ClientId });
        }

        public ActionResult RenderDocument(int id)
        {
            Document d = new Document();
            d = dr.OneRow(id);
            if (d.DocumentType == ".pdf")
            {
                return File(d.document, "application/pdf");
            }
            else if (d.DocumentType == ".jpg")
            {
                return File(d.document, "image/jpeg");
            }

            else
              return  RedirectToAction("NotFound");
        }

        public ActionResult NotFound()
        {
            return View();
        }


        [HttpGet]
    
        public ActionResult Welcome(string id)
        {
            PhotoString ps = new PhotoString();
            ClientDetailViewModel cdvm = new ClientDetailViewModel();
            ClientDetail cd = new ClientDetail();
            cd = cdr.OneRow(id);
            cdvm.ClientId = cd.ClientId;
            cdvm.FirstName = cd.FirstName;
            cdvm.Lastname = cd.Lastname;
            cdvm.dob = cd.dob;
            cdvm.address = cd.address;
            cdvm.phone = cd.phone;
            cdvm.visatype = cd.visatype;
            cdvm.visaduedate = cd.visaduedate;
            cdvm.passportcountry = cd.passportcountry;
            cdvm.passportnumber = cd.passportnumber;
            cdvm.passportexpirydate = cd.passportexpirydate;
            cdvm.policeclearanceexpiry = cd.policeclearanceexpiry;
            cdvm.medicalexpiry = cd.medicalexpiry;
            if(cd.photothumb == null)
            { cdvm.thumbstring = ps.thumb; }
            else { 
            cdvm.thumbstring = ps.PhotoByteToString(cd.photothumb);
            }
            List<DocumentViewModel> Documents = new List<DocumentViewModel>();
            foreach (var d in dr.Documents(id).OrderBy(n => n.DocumentName))
            {
                DocumentViewModel dvml = new DocumentViewModel();
                dvml.ClientId = d.ClientId;
                dvml.DocumentId = d.DocumentId;
                dvml.DocumentName = d.DocumentName;
                dvml.DocumentType = d.DocumentType;
                dvml.document = d.document;
                Documents.Add(dvml);
            }
            ViewBag.Documents = Documents;
            return View(cdvm);
        }
        [HttpPost]
        public ActionResult DeleteDocument(int id)
        {
            Document doct = dr.OneRow(id);
            string cid = doct.ClientId;
            dr.DeleteDocument(doct);
            return RedirectToAction("EditDocument", new { id = cid });
        }


        [HttpGet]
        public ActionResult Crop(string id)
        {
            TempData["ClientId"] = id;
            PhotoString ps = new PhotoString();
            ViewBag.RawPhoto = ps.PhotoByteToString(cdr.OneRow(id).photoraw);
            return View();
        }
        [HttpPost]
        public ActionResult Crop(ImageCoordinates i)
        {
            string ClientId = TempData["ClientId"].ToString();
            ResizeImage ri = new ResizeImage();
            int x1 = Convert.ToInt16(i.x1);
            int x2 = Convert.ToInt16(i.x2);
            int y1 = Convert.ToInt16(i.y1);
            int y2 = Convert.ToInt16(i.y2);
            int w = Convert.ToInt16(i.w);
            int h = Convert.ToInt16(i.h);
            byte[] CroppedPhoto;
            if (w != 0 || h != 0)
            {
                CroppedPhoto = ImageHelper.CropImage(cdr.OneRow(ClientId).photoraw, x1, y1, w, h);
                Stream str = new MemoryStream(CroppedPhoto);
                ClientDetail cd = new ClientDetail();
                cd = cdr.OneRow(ClientId);
                cd.ClientId = TempData["ClientId"].ToString();
                cd.photo = ri.ResizeFromStream(400, str, "CroppedPhoto");
                cd.photothumb = ri.ResizeFromStream(200, str, "CroppedThumb");
                cdr.EditClient(cd);

            }

            
            return RedirectToAction("Welcome", new { id = TempData["ClientId"].ToString() });
        }




        public ClientManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ClientManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginUser(LoginViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Email or Password is Wrong";
                return RedirectToAction("Index", "Home");
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    string cid = UserManager.FindByEmail(model.Email).Id;

                    if (UserManager.IsInRole(cid, "User"))
                    {
                        return RedirectToAction("Welcome", "Account", new { id = cid });
                    }
                    else if (UserManager.IsInRole(cid, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Welcome","Account", new {id = cid });
                    }
                case SignInStatus.LockedOut:
                    TempData["LogInFail"] = "Email or Password is Wrong";
                    return RedirectToAction("Index", "Home");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
               
                case SignInStatus.Failure:
                default:
               // ModelState.AddModelError("", "Invalid login attempt.");
                    TempData["Error"] = "Email or Password is Wrong";
                   
                    return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Client { UserName = model.Email, Email = model.Email };
                
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    ClientDetail cd = new ClientDetail();
                    AdminClientDetail acd = new AdminClientDetail();
                   
                    string role = "User";
                    acd.ClientId = cd.ClientId = UserManager.FindByEmail(model.Email).Id;
                    UserManager.AddToRole(cd.ClientId,role);
                    cdr.AddRow(cd);
                    acdr.AddRow(acd);
                    TempData["Error"] = "Your secure account has been created, please login to continue";
                    return RedirectToAction("Index", "Home");
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new Client { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}