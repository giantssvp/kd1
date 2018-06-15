using kd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kd.Controllers
{
    public class HomeController : Controller
    {
        public static db_connect obj = new db_connect();

        public int enquiry_page_size = 10;
        public int sites_page_size = 10;
        public int executive_page_size = 10;
        public int franchies_page_size = 10;
        public int customer_page_size = 10;
        public int paycommit_page_size = 10;
        public int paydetails_page_size = 10;

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset_enquiry", 0);
            List<string>[] list = new List<string>[14];
            list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset_enquiry"].ToString()), enquiry_page_size);
            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            return View();
        }

        public ActionResult Sites()
        {
            
            ViewBag.total = 0;
            HttpContext.Session.Add("offset_sites", 0);
            List<string>[] list = new List<string>[11];
            list = obj.sites_show(Int32.Parse(HttpContext.Session["offset_sites"].ToString()), sites_page_size);
            ViewBag.list = list;
            //ViewBag.total = list[0].Count();
            
            return View();
        }

        public ActionResult Executive()
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset_executive", 0);
            List<string>[] list = new List<string>[14];
            list = obj.executive_show(Int32.Parse(HttpContext.Session["offset_executive"].ToString()), executive_page_size);
            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            return View();
        }

        public ActionResult Franchies()
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset_franchies", 0);
            List<string>[] list = new List<string>[14];
            list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset_franchies"].ToString()), franchies_page_size);
            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            return View();
        }

        public ActionResult Customer()
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset_customer", 0);
            List<string>[] list = new List<string>[14];
            list = obj.customer_show(Int32.Parse(HttpContext.Session["offset_customer"].ToString()), customer_page_size);
            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            return View();
        }
        public ActionResult Booking()
        {

            return View();
        }
        public ActionResult PaymentCommit()
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset_paycommit", 0);
            List<string>[] list = new List<string>[14];
            list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset_paycommit"].ToString()), paycommit_page_size);
            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            return View();
        }
        public ActionResult PaymentDetails()
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset_paydetails", 0);
            List<string>[] list = new List<string>[14];
            list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset_paydetails"].ToString()), paydetails_page_size);
            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            return View();
        }
        public ActionResult Agreement()
        {

            return View();
        }
        public ActionResult Finance()
        {

            return View();
        }
        public ActionResult FileStatus()
        {

            return View();
        }
        public ActionResult Report()
        {

            return View();
        }

        public ActionResult add_enquiry(string enqname, string enqaddress, string enqmob, string enqdate, string enqsite, string enqrequirement, string enqoccu, string enqvisit, string enqinterest,
            string enqbudget, string enqdown, string enqbooking, string enqremark)
        {
            try
            {
                obj.insert_enquiry(enqname, enqaddress, enqmob, enqdate, enqsite, enqrequirement, enqoccu, enqvisit,
                    enqinterest, enqbudget, enqdown, enqbooking, enqremark);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult add_sites(string sitename, string siteaddress, string sitephone, string siteemail, string sitestatus)
        {
            try
            {
                obj.insert_sites(sitename, siteaddress, sitephone, siteemail, sitestatus);
                return RedirectToAction("Sites", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Sites", "Home");
            }
        }

        public ActionResult add_flats(string flatsitename, string flatwing, string flatfloor, string flatno, string flattype, string flatarea, string flatstatus)
        {
            try
            {
                obj.insert_flats(flatsitename, flatwing, flatfloor, flatno, flattype, flatarea, flatstatus);
                return RedirectToAction("Sites", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Sites", "Home");
            }
        }

        public ActionResult add_executive(string exename, string execode, string exeemail, string exemob, string exeadd, string exejoin, string exebirth, string exestatus)
        {
            try
            {
                obj.insert_executive(exename, execode, exeemail, exemob, exeadd, exejoin, exebirth, exestatus);
                return RedirectToAction("Executive", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Executive", "Home");
            }
        }

        public ActionResult add_franchies(string francname, string franccode, string francemail, string francmob, string francadd, string francjoin, string francstatus)
        {
            try
            {
                obj.insert_franchies(francname, franccode, francemail, francmob, francadd, francjoin, francstatus);
                return RedirectToAction("Franchies", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Franchies", "Home");
            }
        }

        public ActionResult add_applicant(string applname, string applemail, string applmob, string appladdr, string applpan, string applaadhar,
            string apploccu, string applbirth, string applage, string coapplname, string coapplpan, string coapplaadhar, string coapploccu, string coapplbirth)
        {
            try
            {
                obj.insert_applicant(applname, applemail, applmob, appladdr, applpan, applaadhar,
             apploccu, applbirth, applage, coapplname, coapplpan, coapplaadhar, coapploccu, coapplbirth);
                return RedirectToAction("Customer", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Customer", "Home");
            }
        }

        public ActionResult add_booking(string bno, string breferred, string bincentive, string bincome, string bcancel, string btamount,
            string bramount, string bblder, string bparking, string bcharges, string bfollowup, string bstatus, string bremark, string psgst, string bflats, string bapplicant, string bexecutive, string bfranchies)
        {
            try
            {
                obj.insert_booking(bno, breferred, bincentive, bincome, bcancel, btamount,
             bramount, bblder, bparking, bcharges, bfollowup, bstatus, bremark, psgst, bflats, bapplicant, bexecutive, bfranchies);
                return RedirectToAction("Booking", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Booking", "Home");
            }
        }

        public ActionResult add_paymentcommit(string ctype, string camount, string cstatus, string cremark)
        {
            try
            {
                obj.insert_paymentcommit(ctype, camount, cstatus, cremark);
                return RedirectToAction("PaymentCommit", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("PaymentCommit", "Home");
            }
        }

        public ActionResult add_paymentdetails(string pamt, string pdate, string pmode, string chkid, string chkdate, string bname,
            string ptype, string bldpay, string bnkpay, string sts)
        {
            try
            {
                obj.insert_paymentdetails(pamt, pdate, pmode, chkid, chkdate, bname, ptype, bldpay, bnkpay, sts);
                return RedirectToAction("PaymentDetails", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("PaymentDetails", "Home");
            }
        }

        /*public ActionResult add_agreement(string pcode, string pname, string phsn, string pcgst, string psgst, string pigst, string prate)
        {
            try
            {
                obj.insert_agreement(pcode, pname, phsn, pcgst, psgst, pigst, prate);
                return RedirectToAction("Agreement", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Agreement", "Home");
            }
        }*/

        public ActionResult add_finance(string fintype, string finname, string finexe, string finexemob, string finexeemail, string filehanddate,
            string filesta, string filesanctdate, string reqloanamt, string sanctloanamt, string disburseamt, string actloanamt, string recddamt, string remddamt, string rateofinter, string emiamt, string emimonths, string bookid, string finstat)
        {
            try
            {
                obj.insert_finance(fintype, finname, finexe, finexemob, finexeemail, filehanddate,
             filesta, filesanctdate, reqloanamt, sanctloanamt, disburseamt, actloanamt, recddamt, remddamt, rateofinter, emiamt, emimonths, bookid, finstat);
                return RedirectToAction("Finance", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Finance", "Home");
            }
        }

        public ActionResult add_filestatus(string chrg, string lfee, string chid, string chdate, string bnknm, string figst,
            string lfamt, string fid, string fstatus)
        {
            try
            {
                obj.insert_filestatus(chrg, lfee, chid, chdate, bnknm, figst, lfamt, fid, fstatus);
                return RedirectToAction("FileStatus", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("FileStatus", "Home");
            }
        }
    }
}