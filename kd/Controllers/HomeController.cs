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
        private List<string> name;

        public ActionResult Dashboard()
        {
            return View();
        }
        
        public ActionResult Index(string ps="10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }

        public ActionResult Sites(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] sites = new List<string>[7];
            List<string>[] list = new List<string>[9];
            sites = obj.sites_show();

            list = obj.flats_show(sites[1][0], Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.sites = sites;            
            ViewBag.total_site = sites[0].Count();

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }

        public ActionResult Executive(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }

        public ActionResult Franchies(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }

        public ActionResult Customer(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }
        public ActionResult Booking(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[21];

            list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }
        public ActionResult PaymentCommit(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }
        public ActionResult PaymentDetails(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }
        public ActionResult Agreement(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }
        public ActionResult Finance(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[21];

            list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }
        public ActionResult FileStatus(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[10];

            list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }

        public ActionResult CustomerCostSheet(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }

        public ActionResult BuilderCostSheet(string ps = "10")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);

            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult First(string page, string ps)
        {
            try
            {
                List<string>[] list = new List<string>[21];
                int page_size = Int32.Parse(ps);
                ViewBag.total = 0;

                if (page == "Index")
                {
                    list = obj.enquiry_show(0, page_size);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(sites[1][0], 0, page_size);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                }
                else if(page == "Executive")
                {                                        
                    list = obj.executive_show(0, page_size);
                }
                else if(page == "Franchies")
                {
                    list = obj.franchies_show(0, page_size);
                }
                else if(page == "Customer")
                {
                    list = obj.customer_show(0, page_size);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(0, page_size);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(0, page_size);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(0, page_size);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(0, page_size);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(0, page_size);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(0, page_size);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", 0, page_size);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", 0, page_size);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;

                return View(page);
            }
            catch (Exception ex)
            {
                return View(page);
            }
        }

        public ActionResult Previous(string page, string ps)
        {
            try
            {
                List<string>[] list = new List<string>[21];
                int page_size = Int32.Parse(ps);

                HttpContext.Session.Add("offset", (Int32.Parse(HttpContext.Session["offset"].ToString()) - page_size));
                if (Int32.Parse(HttpContext.Session["offset"].ToString()) <= (page_size - 1))
                {
                    HttpContext.Session.Add("offset", 0);
                }                
                
                if (page == "Index")
                {
                    list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(sites[1][0], Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Franchies")
                {
                    list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;

                return View(page);
            }
            catch (Exception ex)
            {
                return View(page);
            }
        }

        public ActionResult Next(string page, string ps)
        {
            try
            {
                List<string>[] list = new List<string>[21];
                int page_size = Int32.Parse(ps);
                int cnt = 0;

                if (page == "Index")
                {
                    cnt = obj.get_count("daily_enquiry");
                }
                else if (page == "Sites")
                {
                    cnt = obj.get_count("flats where Site_Id = 0");
                }
                else if (page == "Executive")
                {
                    cnt = obj.get_count("executive");
                }
                else if (page == "Franchies")
                {
                    cnt = obj.get_count("franchies");
                }
                else if (page == "Customer")
                {
                    cnt = obj.get_count("applicant");
                }
                else if (page == "Booking")
                {
                    cnt = obj.get_count("bookings");
                }
                else if (page == "PaymentCommit")
                {
                    cnt = obj.get_count("payment_commitment");
                }
                else if (page == "PaymentDetails")
                {
                    cnt = obj.get_count("payment_details");
                }
                else if (page == "Agreement")
                {
                    cnt = obj.get_count("aggrement");
                }
                else if (page == "Finance")
                {
                    cnt = obj.get_count("finance_details");
                }
                else if (page == "FileStatus")
                {
                    cnt = obj.get_count("file_details");
                }
                else if (page == "CustomerCostSheet")
                {
                    cnt = obj.get_count("cost_sheet where Cost_Sheet_Type = 'customer'");
                }
                else if (page == "BuilderCostSheet")
                {
                    cnt = obj.get_count("cost_sheet where Cost_Sheet_Type = 'builder'");
                }

                HttpContext.Session.Add("offset", (Int32.Parse(HttpContext.Session["offset"].ToString()) + page_size));
                if (Int32.Parse(HttpContext.Session["offset"].ToString()) > cnt)
                {
                    HttpContext.Session.Add("offset", (cnt - (cnt % page_size)));
                }

                if (page == "Index")
                {
                    list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(sites[1][0], Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Franchies")
                {
                    list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;

                return View(page);
            }
            catch (Exception ex)
            {
                return View(page);
            }
        }
        
        public ActionResult Last(string page, string ps)
        {
            try
            {
                List<string>[] list = new List<string>[21];
                int page_size = Int32.Parse(ps);
                int cnt = 0;

                if (page == "Index")
                {
                    cnt = obj.get_count("daily_enquiry");
                }
                else if (page == "Sites")
                {
                    cnt = obj.get_count("flats where Site_Id = 0");
                }
                else if (page == "Executive")
                {
                    cnt = obj.get_count("executive");
                }
                else if (page == "Franchies")
                {
                    cnt = obj.get_count("franchies");
                }
                else if (page == "Customer")
                {
                    cnt = obj.get_count("applicant");
                }
                else if (page == "Booking")
                {
                    cnt = obj.get_count("bookings");
                }
                else if (page == "PaymentCommit")
                {
                    cnt = obj.get_count("payment_commitment");
                }
                else if (page == "PaymentDetails")
                {
                    cnt = obj.get_count("payment_details");
                }
                else if (page == "Agreement")
                {
                    cnt = obj.get_count("aggrement");
                }
                else if (page == "Finance")
                {
                    cnt = obj.get_count("finance_details");
                }
                else if (page == "FileStatus")
                {
                    cnt = obj.get_count("file_details");
                }
                else if (page == "CustomerCostSheet")
                {
                    cnt = obj.get_count("cost_sheet where Cost_Sheet_Type = 'customer'");
                }
                else if (page == "BuilderCostSheet")
                {
                    cnt = obj.get_count("cost_sheet where Cost_Sheet_Type = 'builder'");
                }

                if (cnt > 0)
                {
                    if (cnt % page_size == 0)
                    {
                        HttpContext.Session.Add("offset", (cnt - page_size));
                    }
                    else
                    {
                        HttpContext.Session.Add("offset", (cnt - (cnt % page_size)));
                    }
                }

                if (page == "Index")
                {
                    list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(sites[1][0], Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Franchies")
                {
                    list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;

                return View(page);
            }
            catch (Exception ex)
            {
                return View(page);
            }
        }

        public ActionResult add_customer_cost_sheet(string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyreg, string gst, string otheramt, string grandtotal)
        {
            try
            {
                obj.insert_cost_sheet("customer", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal);
                return RedirectToAction("CustomerCostSheet", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("CustomerCostSheet", "Home");
            }
        }

        public ActionResult add_builder_cost_sheet(string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyreg, string gst, string otheramt, string grandtotal)
        {
            try
            {
                obj.insert_cost_sheet("builder", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal);
                return RedirectToAction("BuilderCostSheet", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("BuilderCostSheet", "Home");
            }
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
                System.Windows.Forms.MessageBox.Show(flatsitename);
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
            string apploccu, string applbirth, string applage, string coapplname, string coapplpan, string coapplaadhar, string coapploccu, string coapplbirth, string applstatus)
        {
            try
            {
                obj.insert_applicant(applname, applemail, applmob, appladdr, applpan, applaadhar,
             apploccu, applbirth, applage, coapplname, coapplpan, coapplaadhar, coapploccu, coapplbirth, applstatus);
                return RedirectToAction("Customer", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Customer", "Home");
            }
        }

        public ActionResult add_booking(string bno, string breferred, string bincentive, string bincome, string bcancel, string btamount,
            string bramount, string bblder, string bparking, string bcharges, string bfollowup, string bstatus, string bremark, string bsite, string bflats, string bapplicant, string bexecutive, string bfranchies)
        {
            try
            {
                obj.insert_booking(bno, breferred, bincentive, bincome, bcancel, btamount,
             bramount, bblder, bparking, bcharges, bfollowup, bstatus, bremark, bsite, bflats, bapplicant, bexecutive, bfranchies);
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
        
        public ActionResult add_agreement(string ano, string aamount, string astatus, string bid, string adate)
        {
            try
            {
                obj.insert_agreement(ano, aamount, astatus, bid, adate);
                return RedirectToAction("Agreement", "Home");
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Agreement", "Home");
            }
        }

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

        /* Drop Down list for site name on page load*/
        [HttpGet]
        public ActionResult get_site(string date)
        {
            List<string>[] sites = new List<string>[7];
            sites = obj.sites_show();
            var result = new { id = sites[0],
                name = sites[1]};
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}