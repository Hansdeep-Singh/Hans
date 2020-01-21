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
using HeroBlaze.Models;
using System.Collections.Generic;
using System.IO;
using Business.Logic;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HeroBlaze.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
       
        private UserInitialInfoRepository uiir = new UserInitialInfoRepository();
        private UserTalentRepository utr = new UserTalentRepository();
        private AudioRepository ar = new AudioRepository();
        private VideoRepository vr = new VideoRepository();
        private DocumentRepository dr = new DocumentRepository();
        private ImageRepository ir = new ImageRepository();
        private ApplicationSignInManager _signInManager;
        private HeroBlazeUserManager _userManager;
        private Transfer t = new Transfer();
        private ResizeImage ri = new ResizeImage();
        private ImageHelper ih = new ImageHelper();

        public AccountController()
        {
        }

        public AccountController(HeroBlazeUserManager userManager, ApplicationSignInManager signInManager)
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

        public HeroBlazeUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HeroBlazeUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // New Actions
        public ActionResult Secure(string id)
        {
            List<UserTalentViewModel> UserTalents = new List<UserTalentViewModel>();
            foreach (var ut in utr.UserTalentsList(id).OrderBy(n => n.TalentName))
            {
                UserTalentViewModel utvm = new UserTalentViewModel();
                t.UtToUtvm(utvm, ut);
                UserTalents.Add(utvm);
            }
            ViewBag.TalentNames = UserTalents;
            PageSecureViewModel Psvm = new PageSecureViewModel();
            Psvm.Udvm = t.UdToUdvm(uiir.OneRow(id));

            return View(Psvm);
        }

        public ActionResult UpdateInfo(string id)
        {
            return View(t.UdToUdvm(uiir.OneRow(id)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateInfo(UserDetailViewModel udvm)
        {
            if (ModelState.IsValid)
            {
                UserDetail ud = new UserDetail();
                ud = uiir.OneRow(udvm.UserID);
                uiir.EditUser(t.UdvmToUd(udvm, ud));
                return RedirectToAction("Secure", new {id = udvm.UserID });
            }
            else
            {
                return View(udvm);
            }
        }


        public ActionResult Crop(string id)
        {
            PhotoString ps = new PhotoString();
            ImageCoordinates ic = new ImageCoordinates();
            ic.PhotoString = ps.PhotoByteToString(uiir.OneRow(id).ProfilePhoto);
            ic.UserID = id;
            return View(ic);
        }
        [HttpPost]
        public ActionResult Crop(ImageCoordinates i)
        {
            UserDetail ud = new UserDetail();
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
                CroppedPhoto = ImageHelper.CropImage(uiir.OneRow(i.UserID).ProfilePhoto, x1, y1, w, h);
                Stream MemStr = new MemoryStream(CroppedPhoto);
                ud = uiir.OneRow(i.UserID);
                ud.ProfilePhoto = ri.ResizeFromStream(400, MemStr, "CroppedPhoto");
                ud.ProfilePhotoThumb = ri.ResizeFromStream(200, MemStr, "CroppedPhoto");
                uiir.EditUser(ud);
            }
               
            return RedirectToAction("Secure", new {id = i.UserID });
        }

        public ActionResult FillPhoto(UserDetailViewModel udvm)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                udvm.File.InputStream.CopyTo(ms);
                UserDetail ud = new UserDetail();
                ud = uiir.OneRow(udvm.UserID);
                ud.PhotoRaw = ri.ResizeFromStream(750, ms, "Raw750");
                ud.ProfilePhoto = ri.ResizeFromStream(450, ms, "Raw450");
                uiir.EditUser(ud);
            }
            return RedirectToAction("Crop", new {id = udvm.UserID });
        }

        public ActionResult UpdateTalent(PageSecureViewModel psvm)
        {

            UserTalentViewModel utvm = new UserTalentViewModel();
            utvm = psvm.Utvm;
            utvm.UserID = psvm.Udvm.UserID;
            UserTalent ut = new UserTalent();
            utr.AddRow(t.UtvmToUt(utvm, ut));
            return RedirectToAction("Secure", new { id =utvm.UserID });
        }


        //public ActionResult DeleteImage(int id)
        //{
        //    ImageMedia im = ir.OneRow(id);
        //    ir.DeleteImage(im);
        //    return RedirectToAction("Talent", new { id = im.UserTalentID });
        //}
        public ActionResult DeleteTalent(int id)
        {
            UserTalent ut = utr.OneRowTalent(id);
            TempData["UserID"] = ut.UserID;
            utr.DeleteTalent(ut);
            ar.DeleteMultipleAudios(id);
            return RedirectToAction("Secure", new { id = TempData["UserID"] });
        }

        public ActionResult Talent(int id)
        {
            PageTalentViewModel ptvm = new PageTalentViewModel();
            UserTalentViewModel utvm = new UserTalentViewModel();
            //Section Fetching All Audio Rows on UserTalent Id
            List<AudioMediaViewModel> AudioMedias = new List<AudioMediaViewModel>();
            int x = -1;
            foreach (var am in ar.AudioMediasList(id).OrderBy(n => n.AudioTitle))
            {
                
                AudioMediaViewModel amvm = new AudioMediaViewModel();
                t.AmToAmvm(am, amvm);
                x += 1;
                amvm.SongIndex = x;
                AudioMedias.Add(amvm);
            }
            ViewBag.Audios = AudioMedias;
            //Section End

            //Section Fetching All Document Rows on UserTalent Id
            List<DocumentMediaViewModel> DocumentMedias = new List<DocumentMediaViewModel>();
            foreach( var dm in dr.DocumentMediasList(id).OrderBy(d=>d.DocumentName))
            {
                DocumentMediaViewModel dmvm = new DocumentMediaViewModel();
                t.DmToDmvm(dm, dmvm);
                DocumentMedias.Add(dmvm);
            }
            ViewBag.DocumentMedias = DocumentMedias;
            //End Section
            //Section Fetching All Video Rows on UserTalent Id

            List<VideoMediaViewModel> VideoMedias = new List<VideoMediaViewModel>();
            foreach (var vm in vr.VideoMediasList(id).OrderBy(v => v.VideoTitle))
            {
                VideoMediaViewModel vmvm = new VideoMediaViewModel();
                t.VmToVmvm(vm, vmvm);
                VideoMedias.Add(vmvm);
            }
            ViewBag.VideoMedias = VideoMedias;
            //End Section
            //Section Fetching All Photo Rows on UserTalent Id
            List<ImageMediaViewModel> ImageMedias = new List<ImageMediaViewModel>();
            foreach(var im in ir.ImageMediasList(id).OrderBy(i=>i.ImageName))
            {
                ImageMediaViewModel imvm = new ImageMediaViewModel();
                t.ImtoImvm(im, imvm);
                ImageMedias.Add(imvm);
            }
           ViewBag.ImageMedias = ImageMedias;
            //End Section
            //ut.UserTalentID = id;
            ptvm.utvm = t.UtToUtvm(utvm, utr.GetOneIntId(id)); 
            return View(ptvm);
        }

        

        public ActionResult AddAudio(PageTalentViewModel ptvm)
        {
            if (ModelState.IsValid)
            {
                AudioMediaViewModel amvm = new AudioMediaViewModel();
                UserTalentViewModel utvm = new UserTalentViewModel();
                AudioMedia am = new AudioMedia();
                utvm = ptvm.utvm;
                amvm = ptvm.amvm;
                amvm.UserTalentID = utvm.UserTalentID;
                am = t.AmvmToAm(am, amvm);
                Stream stream = amvm.File.InputStream;
                BinaryReader br = new BinaryReader(stream);
                am.AudioByte = br.ReadBytes((Int32)stream.Length);
                ar.AddRow(am);
                return RedirectToAction("Talent", new { id = am.UserTalentID });
            }
            return RedirectToAction("Talent", new { id= ptvm.utvm.UserTalentID});



        }

        public ActionResult DeleteAudio(int id)
        {
            AudioMedia am = ar.OneRow(id);
            ar.DeleteAudio(am);
            return RedirectToAction("Talent", new { id = am.UserTalentID});
        }
        public ActionResult AddImage(PageTalentViewModel ptvm)
        {
            if(ModelState.IsValid)
            { 
            ImageMediaViewModel imvm = new ImageMediaViewModel();
            UserTalentViewModel utvm = new UserTalentViewModel();
            ImageMedia im = new ImageMedia();
            utvm = ptvm.utvm;
            imvm = ptvm.imvm;
            imvm.UserTalentID = utvm.UserTalentID;
            t.ImvmtoIm(im, imvm);
            Stream stream = imvm.File.InputStream;
            BinaryReader br = new BinaryReader(stream);

            using (MemoryStream ms = new MemoryStream())
            {
                imvm.File.InputStream.CopyTo(ms);
                im.ImageByte = ri.ResizeFromStream(750, ms, "Raw750");
                im.ImageByteThumb = ri.ResizeFromStream(250, ms, "Raw450");
            }
            ir.AddRow(im);
            return RedirectToAction("Talent", new { id =im.UserTalentID});
            }
            return RedirectToAction("Talent", new { id = ptvm.utvm.UserTalentID});

        }

        public ActionResult EditTalent(PageTalentViewModel ptvm)
        {
            if (ModelState.IsValid)
            {
                UserTalentViewModel utvm = new UserTalentViewModel();
                UserTalent ut = new UserTalent();
                utvm = ptvm.utvm;
                t.UtvmToUt(utvm, ut);
                utr.EditTalent(ut);
                return RedirectToAction("Talent", new { id = utvm.UserTalentID });
            }
            return RedirectToAction("Talent", new { id = ptvm.utvm.UserTalentID });
        }



        public ActionResult DeleteImage(int id)
        {
            ImageMedia im = ir.OneRow(id);
            ir.DeleteImage(im);
            return RedirectToAction("Talent", new { id = im.UserTalentID });
        }

        public ActionResult AddDocument(PageTalentViewModel ptvm)
        {
            if (ModelState.IsValid) { 
            DocumentMediaViewModel dmvm = new DocumentMediaViewModel();
            UserTalentViewModel utvm = new UserTalentViewModel();
            DocumentMedia dm = new DocumentMedia();
            utvm = ptvm.utvm;
            dmvm = ptvm.dmvm;
            dmvm.UserTalentID = utvm.UserTalentID;
            dm = t.DmvmToDm(dm, dmvm);
            Stream stream = dmvm.File.InputStream;
            BinaryReader br = new BinaryReader(stream);
            dm.DocumentByte = br.ReadBytes((Int32)stream.Length);
            dr.AddRow(dm);
            return RedirectToAction("Talent", new { id = dm.UserTalentID });
            }
            return RedirectToAction("Talent", new { id = ptvm.utvm.UserTalentID });
        }

        public ActionResult DeleteDocument(int id)
        {
            DocumentMedia dm = dr.OneRow(id);
            dr.DeleteDocument(dm);
            return RedirectToAction("Talent", new { id = dm.UserTalentID });
        }

        public FileResult RenderDocument(int id)
        {
            return File(dr.OneRow(id).DocumentByte, "application/pdf");
        }


        public ActionResult AddVideo(PageTalentViewModel ptvm)
        {
            if (ModelState.IsValid)
            { 
            VideoMediaViewModel vmvm = new VideoMediaViewModel();
            UserTalentViewModel utvm = new UserTalentViewModel();
            VideoMedia vm = new VideoMedia();
            utvm = ptvm.utvm;
            vmvm = ptvm.vmvm;
            vmvm.UserTalentID = utvm.UserTalentID;
            t.VmvmToVm(vm, vmvm);
            vr.AddRow(vm);
            return RedirectToAction("Talent", new { id = vm.UserTalentID });
            }
            return RedirectToAction("Talent", new { id = ptvm.utvm.UserTalentID });

        }

        public ActionResult DeleteVideo(int id)
        {
            VideoMedia vm = vr.OneRow(id);
            vr.DeleteVideo(vm);
            return RedirectToAction("Talent", new { id = vm.UserTalentID });
        }



        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
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
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
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
                var user = new HeroBlazeUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    populateUserId(UserManager.FindByEmail(model.Email).Id);
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void populateUserId(string id)
        {
            UserDetail ud = new UserDetail();
            ud.UserID = id;
            uiir.AddRow(ud);
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
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    // Don't reveal that the user does not exist or is not confirmed
                //    return View("ForgotPasswordConfirmation");
                //}


               

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                 string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                 var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
               
                var message = new SendGridMessage();
                message.SetFrom(new EmailAddress("hansdeep.singh@hotmail.com", "Hansdeep Singh"));
                var recipients = new List<EmailAddress>
                {new EmailAddress(model.Email, "Name")};
                message.AddTos(recipients);
                message.SetSubject("Reset Password");
                message.AddContent(MimeType.Text, callbackUrl);
                message.AddContent(MimeType.Html, "<p>" + callbackUrl + "</p>");


                var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
                var client = new SendGridClient(apiKey);
                var response = await client.SendEmailAsync(message);
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
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
                var user = new HeroBlazeUser { UserName = model.Email, Email = model.Email };
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
        public ActionResult LogOff()
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

        //Custom
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginUser(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    string uid = UserManager.FindByEmail(model.Email).Id;
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("Secure", new { id = uid });
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:

                default:
                    ViewBag.LoginFail = "Login Has Failed";
                    return RedirectToAction("Index", "Home");
                    //ModelState.AddModelError("", "Invalid login attempt.");
                    //return View(model);
            }
        }
        //Custom


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


