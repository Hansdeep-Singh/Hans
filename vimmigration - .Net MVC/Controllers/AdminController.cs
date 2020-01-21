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
using System.Data.Entity.Validation;

namespace VeraCityImmigration.Controllers
{
    public class AdminController : Controller
    {
        private ClientDetailRepository cdr = new ClientDetailRepository();
        private AdminClientDetailRepository acdr = new AdminClientDetailRepository();
        private AdminClientFeeRepository acfr = new AdminClientFeeRepository();
        private DocumentRepository dr = new DocumentRepository();
        //private ApplicationSignInManager _signInManager;
        //private ClientManager _userManager;
        private Logic l = new Logic();
        // GET: Admin
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            List<ClientDetail> ClientDetails = cdr.GetAll();
            List<AdminClientDetail> AdminClientDetails = acdr.GetAll();
            List<AdminClientDetailViewModel> AdminClientDetailViewModel = new List<Models.AdminClientDetailViewModel>();
            var clientviewfull = from c in ClientDetails
                             join ac in AdminClientDetails on c.ClientId equals ac.ClientId into ac2
                             from ac in ac2.DefaultIfEmpty()
                             select new AdminClientDetailFull { ClientDetail = c, AdminClientDetail = ac };
            foreach(var Client in clientviewfull)
            {
                AdminClientDetailViewModel acdvm = new AdminClientDetailViewModel();
                acdvm.FirstName = Client.ClientDetail.FirstName;
                acdvm.Lastname = Client.ClientDetail.Lastname;
                acdvm.ClientId = Client.ClientDetail.ClientId;
                acdvm.TotalFees = Client.AdminClientDetail.TotalFees;
                acdvm.Balance = Client.AdminClientDetail.Balance;
                acdvm.Paid = Client.AdminClientDetail.Paid;
                acdvm.visaduedate = Client.ClientDetail.visaduedate;
                acdvm.Status = Client.AdminClientDetail.Status;
                acdvm.ApplicationNumber = Client.AdminClientDetail.ApplicationNumber;
                AdminClientDetailViewModel.Add(acdvm);
            }

            ViewBag.ClientDetailFull = AdminClientDetailViewModel;

            //List<AdminClientDetail> AdminClientDetailToView;

            //var query = ClientDetails.AsQueryable().Join(AdminClientDetails,
            //        client => client,
            //        clientdetail => clientdetail.ClientDetail,
            //        (client, clientdetail) =>
            //            new { FullClient = client.ClientId, Clientdetail = clientdetail.ClientId });
            //AdminClientDetailViewModel acdf = new AdminClientDetailViewModel();


          
          
                return View();
        }
        [HttpGet]
        public ActionResult EditClient(string id)
        {
           
            AdminClientDetailViewModel acdvm = new AdminClientDetailViewModel();
            AdminClientDetail acd = new AdminClientDetail();
            acd =  acdr.OneRow(id);
            acdvm.AdminClientDetailId = acd.AdminClientDetailId;
            acdvm.ClientId = acd.ClientId;
            acdvm.ApplicationDetail = acd.ApplicationDetail;
            acdvm.ApplicationNumber = acd.ApplicationNumber;
            acdvm.TotalFees = acd.TotalFees;
            return View(acdvm);
        }
        [HttpPost]
        public ActionResult EditClient(AdminClientDetailViewModel acdvm)
        {
            AdminClientDetail acd = new AdminClientDetail();
            acd.AdminClientDetailId = acdvm.AdminClientDetailId;
            acd.ClientId = acdvm.ClientId;
            acd.ApplicationNumber = acdvm.ApplicationNumber;
            acd.ApplicationDetail = acdvm.ApplicationDetail;
            acd.TotalFees = acdvm.TotalFees;
            acd.Status = acdvm.Status;
            acdr.EditClient(acd);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditFees(string id)
        {
            List<AdminClientFeeViewModel> AdminClientFees = new List<Models.AdminClientFeeViewModel>();
            foreach(var f in acfr.AdminClientFees(id).OrderBy(n=>n.InstallmentDate))
            {
                AdminClientFeeViewModel acfvm = new AdminClientFeeViewModel();
                acfvm.AdminClientFeeId = f.AdminClientFeeId;
                acfvm.ClientId = f.ClientId;
                acfvm.Installment = f.Installment;
                acfvm.InstallmentDate = f.InstallmentDate;
                acfvm.InstallmentType = f.InstallmentType;
                AdminClientFees.Add(acfvm);
            }
            AdminClientFeeViewModel acfvmm = new AdminClientFeeViewModel();
            acfvmm.ClientId = id;

            ViewBag.Sum = acfr.AdminClientFees(id).Sum(s=>s.Installment);
            ViewBag.Installments = AdminClientFees;
            return View(acfvmm);
        }


        public ActionResult UpdateBill(string id)
        {
            decimal? sum = acfr.AdminClientFees(id).Sum(s => s.Installment);
            AdminClientDetail acd = new AdminClientDetail();
           
            acd= acdr.OneRow(id);
            acd.Paid = sum;
            acd.Balance = acd.TotalFees - sum;
            acdr.EditClient(acd);
            
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult EditFees(AdminClientFeeViewModel acfvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AdminClientFee acf = new AdminClientFee();
            acf.ClientId = acfvm.ClientId;
            acf.AdminClientFeeId = acfvm.AdminClientFeeId;
            acf.Installment = acfvm.Installment;
            acf.InstallmentDate = acfvm.InstallmentDate;
            acf.InstallmentType = acfvm.InstallmentType;
            acfr.AddFee(acf);
            
            
            return RedirectToAction("EditFees", new { id = acf.ClientId });
        }

        public ActionResult DeleteInstallment(int id)
        {
            AdminClientFee acf = acfr.OneRow(id);
            string cid = acf.ClientId;
            acfr.DeleteInstallment(acf);
            return RedirectToAction("EditFees", new { id = cid });
        }

        


        public ActionResult AdminClientDetail(string id)
        {
            PhotoString ps = new PhotoString();
            ClientDetail cd = new ClientDetail();
            AdminClientDetail acd = new AdminClientDetail();
            AdminClientDetailViewModel acdvm = new AdminClientDetailViewModel();
            acd = acdr.OneRow(id);
            cd = cdr.OneRow(id);
            acdvm.address = cd.address;
            acdvm.ApplicationDetail = acd.ApplicationDetail;
            acdvm.ApplicationNumber = acd.ApplicationNumber;
            acdvm.dob = cd.dob;
            acdvm.FirstName = cd.FirstName;
            acdvm.Lastname = cd.Lastname;
            acdvm.medicalexpiry = cd.medicalexpiry;
            acdvm.passportcountry = cd.passportcountry;
            acdvm.passportexpirydate = cd.passportexpirydate;
            acdvm.passportnumber = cd.passportnumber;
            acdvm.phone = cd.phone;
            acdvm.photothumb = cd.photothumb;
            acdvm.policeclearanceexpiry = cd.policeclearanceexpiry;
            acdvm.visaduedate = cd.visaduedate;
            acdvm.visatype = cd.visatype;
            if (cd.photothumb == null)
            { acdvm.thumbstring = ps.thumb; }
            else
            {
                acdvm.thumbstring = ps.PhotoByteToString(cd.photothumb);
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



            return View(acdvm);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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
                return RedirectToAction("NotFound");
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}