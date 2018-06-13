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

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sites()
        {
           
            return View();
        }

        public ActionResult Executive()
        {

            return View();
        }

        public ActionResult Franchies()
        {

            return View();
        }

        public ActionResult Customer()
        {

            return View();
        }
        public ActionResult Booking()
        {

            return View();
        }
        public ActionResult PaymentCommit()
        {

            return View();
        }
        public ActionResult PaymentDetails()
        {

            return View();
        }
        public ActionResult Aggrement()
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
    }
}