using kd.Models;
using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Collections.Generic;
using iTextSharp.text.html.simpleparser;

namespace kd.Controllers
{
    public class HomeController : Controller
    {
        public static db_connect obj = new db_connect();

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            this.Session["role"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                string role = user.IsValid(user.UserName, user.Password);

                if (role != "false")
                {                   
                    this.Session["role"] = role;
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                    return RedirectToAction("Login", "Home");
                }
            }
            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
                
        public ActionResult Index(string ps="10", string filter="", string search="")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search:search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult Sites(string ps = "10", string site = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] sites = new List<string>[7];
            List<string>[] list = new List<string>[9];
            sites = obj.sites_show();

            if (site == "")
            {
                site = sites[1][0];
                list = obj.flats_show(sites[1][0], Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps));
            }
            else
            {
                list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);
            }

            ViewBag.sites = sites;            
            ViewBag.total_site = sites[0].Count();
            ViewBag.site = site;
            
            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult Executive(string ps = "10", string filter="", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult Franchies(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult Customer(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }
        public ActionResult Booking(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[21];

            list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }
        public ActionResult PaymentCommit(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }
        public ActionResult PaymentDetails(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }
        public ActionResult Agreement(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }
        public ActionResult Finance(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[21];

            list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }
        public ActionResult FileStatus(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[10];

            list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult CustomerCostSheet(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult BuilderCostSheet(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult delete_record(string page, string ps, string del_id, string filter = "", string search = "", string site = "")
        {
            try
            {
                int id = Int32.Parse(del_id);
                int pass = 0;

                if (page == "Index")
                {
                    if(obj.Delete_Record("daily_enquiry", id)  == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "Sites")
                {
                    if (obj.Delete_Record("flats", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "Executive")
                {
                    if (obj.Delete_Record("executive", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "Franchies")
                {
                    if (obj.Delete_Record("franchies", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "Customer")
                {
                    if (obj.Delete_Record("applicant", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "FileStatus")
                {
                    if (obj.Delete_Record("file_details", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "Finance")
                {
                    if (obj.Delete_Record("finance_details", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "Agreement")
                {
                    if (obj.Delete_Record("aggrement", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "Booking")
                {
                    if (obj.Delete_Record("bookings", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "PaymentCommit")
                {
                    if (obj.Delete_Record("payment_commitment", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "PaymentDetails")
                {
                    if (obj.Delete_Record("payment_details", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "CustomerCostSheet")
                {
                    if (obj.Delete_Record("cost_sheet", id) == 0)
                    {
                        pass = 1;
                    }
                }
                else if (page == "builderCostSheet")
                {
                    if (obj.Delete_Record("cost_sheet", id) == 0)
                    {
                        pass = 1;
                    }
                }

                if (pass == 1)
                {
                    TempData["AlertMessage"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while deleting the details please do it again.";
                }

                return RedirectToAction(page, "Home", new { ps=ps, search=search, site=site});
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while deleting the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction(page, "Home");
            }
        }

        public ActionResult First(string page, string ps, string filter = "", string search = "", string site = "")
        {
            try
            {
                List<string>[] list = new List<string>[21];
                int page_size = Int32.Parse(ps);
                ViewBag.total = 0;

                if (page == "Index")
                {
                    list = obj.enquiry_show(0, page_size, search: search);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(site, 0, page_size, search: search);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;
                }
                else if(page == "Executive")
                {                                        
                    list = obj.executive_show(0, page_size, search: search);
                }
                else if(page == "Franchies")
                {
                    list = obj.franchies_show(0, page_size, search: search);
                }
                else if(page == "Customer")
                {
                    list = obj.customer_show(0, page_size, search: search);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(0, page_size, search: search);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(0, page_size, search: search);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(0, page_size, search: search);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(0, page_size, search: search);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(0, page_size, search: search);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(0, page_size, search: search);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", 0, page_size, search: search);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", 0, page_size, search: search);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;
                ViewBag.search = search;

                return View(page);
            }
            catch (Exception ex)
            {
                return View(page);
            }
        }

        public ActionResult Previous(string page, string ps, string filter = "", string search = "", string site = "")
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
                    list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Franchies")
                {
                    list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;
                ViewBag.search = search;

                return View(page);
            }
            catch (Exception ex)
            {
                return View(page);
            }
        }

        public ActionResult Next(string page, string ps, string filter = "", string search = "", string site = "")
        {
            try
            {
                List<string>[] list = new List<string>[21];
                int page_size = Int32.Parse(ps);
                int cnt = 0;

                if (page == "Index")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "daily_enquiry";
                    }
                    else
                    {
                        query = "daily_enquiry where CONCAT(Customer_Name, Requirement) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Sites")
                {
                    string query = "";
                    int id = obj.get_site_id_by_name(site);
                    
                    if (search == "")
                    {
                        query = "flats where Site_Id = " + id;
                    }
                    else
                    {
                        query = "flats where Site_Id = " + id + " and CONCAT(Status, Flat_No) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Executive")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "executive";
                    }
                    else
                    {
                        query = "executive where CONCAT(Executive_Name, Executive_Code) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Franchies")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "franchies";
                    }
                    else
                    {
                        query = "franchies where CONCAT(Francies_Name, Address) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Customer")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "applicant";
                    }
                    else
                    {
                        query = "applicant where CONCAT(Applicant_Name, Co_Applicant_Name) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Booking")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "bookings";
                    }
                    else
                    {
                        query = "bookings where CONCAT(Booking_No, Referenceby) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentCommit")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "payment_commitment";
                    }
                    else
                    {
                        query = "payment_commitment where CONCAT(Commitment_Type, Amount) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentDetails")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "payment_details";
                    }
                    else
                    {
                        query = "payment_details where CONCAT(Cheque_Id, Payment_Type) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Agreement")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "aggrement";
                    }
                    else
                    {
                        query = "aggrement where CONCAT(Aggrement_No, Aggrement_Amount) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Finance")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "finance_details";
                    }
                    else
                    {
                        query = "finance_details where CONCAT(Finance_Name, Finance_Executive_Name) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "FileStatus")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "file_details";
                    }
                    else
                    {
                        query = "file_details where CONCAT(Cheque_Id, Bank_Name) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "CustomerCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'customer'";
                    }
                    else
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'customer' and CONCAT(Basic_Rate, Type) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "BuilderCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'builder'";
                    }
                    else
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'builder' and CONCAT(Basic_Rate, Type) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }

                HttpContext.Session.Add("offset", (Int32.Parse(HttpContext.Session["offset"].ToString()) + page_size));
                if (Int32.Parse(HttpContext.Session["offset"].ToString()) > cnt)
                {
                    HttpContext.Session.Add("offset", (cnt - (cnt % page_size)));
                }

                if (page == "Index")
                {
                    list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;                    
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Franchies")
                {
                    list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;
                ViewBag.search = search;

                return View(page);
            }
            catch (Exception ex)
            {
                return View(page);
            }
        }
        
        public ActionResult Last(string page, string ps, string filter = "", string search = "", string site = "")
        {
            try
            {
                List<string>[] list = new List<string>[21];
                int page_size = Int32.Parse(ps);
                int cnt = 0;

                if (page == "Index")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "daily_enquiry";
                    }
                    else
                    {
                        query = "daily_enquiry where CONCAT(Customer_Name, Requirement) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Sites")
                {
                    string query = "";
                    int id = obj.get_site_id_by_name(site);
                    if (search == "")
                    {
                        query = "flats where Site_Id = " + id;
                    }
                    else
                    {
                        query = "flats where Site_Id = " + id + " and CONCAT(Status, Flat_No) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Executive")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "executive";
                    }
                    else
                    {
                        query = "executive where CONCAT(Executive_Name, Executive_Code) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Franchies")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "franchies";
                    }
                    else
                    {
                        query = "franchies where CONCAT(Francies_Name, Address) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Customer")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "applicant";
                    }
                    else
                    {
                        query = "applicant where CONCAT(Applicant_Name, Co_Applicant_Name) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Booking")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "bookings";
                    }
                    else
                    {
                        query = "bookings where CONCAT(Booking_No, Referenceby) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentCommit")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "payment_commitment";
                    }
                    else
                    {
                        query = "payment_commitment where CONCAT(Commitment_Type, Amount) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentDetails")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "payment_details";
                    }
                    else
                    {
                        query = "payment_details where CONCAT(Cheque_Id, Payment_Type) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Agreement")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "aggrement";
                    }
                    else
                    {
                        query = "aggrement where CONCAT(Aggrement_No, Aggrement_Amount) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Finance")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "finance_details";
                    }
                    else
                    {
                        query = "finance_details where CONCAT(Finance_Name, Finance_Executive_Name) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "FileStatus")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "file_details";
                    }
                    else
                    {
                        query = "file_details where CONCAT(Cheque_Id, Bank_Name) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "CustomerCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'customer'";
                    }
                    else
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'customer' and CONCAT(Basic_Rate, Type) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "BuilderCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'builder'";
                    }
                    else
                    {
                        query = "cost_sheet where Cost_Sheet_Type = 'builder' and CONCAT(Basic_Rate, Type) LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
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
                    list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Sites")
                {
                    List<string>[] sites = new List<string>[7];
                    sites = obj.sites_show();
                    list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                    ViewBag.sites = sites;
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;                    
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Franchies")
                {
                    list = obj.franchies_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Booking")
                {
                    list = obj.booking_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "PaymentCommit")
                {
                    list = obj.paycommit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "PaymentDetails")
                {
                    list = obj.paydetails_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Agreement")
                {
                    list = obj.agreement_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Finance")
                {
                    list = obj.finance_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "FileStatus")
                {
                    list = obj.file_status_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "CustomerCostSheet")
                {
                    list = obj.cost_sheet_show("customer", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "BuilderCostSheet")
                {
                    list = obj.cost_sheet_show("builder", Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }

                ViewBag.list = list;
                ViewBag.total = list[0].Count();
                ViewBag.pageSize = page_size;
                ViewBag.search = search;

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
                if (obj.insert_cost_sheet("customer", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("CustomerCostSheet", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("CustomerCostSheet", "Home");
            }
        }

        public ActionResult add_builder_cost_sheet(string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyreg, string gst, string otheramt, string grandtotal)
        {
            try
            {
                if (obj.insert_cost_sheet("builder", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("BuilderCostSheet", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("BuilderCostSheet", "Home");
            }
        }

        public ActionResult add_enquiry(string enqname, string enqaddress, string enqmob, string enqdate, string enqsite, List<string> enqrequirement, string enqoccu, string enqvisit, string enqinterest,
            string enqbudget, string enqdown, string enqbooking, string enqremark)
        {
            try
            {
                string req = string.Join(" ", enqrequirement);

                if (obj.insert_enquiry(enqname, enqaddress, enqmob, enqdate, enqsite, req, enqoccu, enqvisit, 
                    enqinterest, enqbudget, enqdown, enqbooking, enqremark) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult add_sites(string sitename, string siteaddress, string sitephone, string siteemail, string sitestatus)
        {
            try
            {
                if (obj.insert_sites(sitename, siteaddress, sitephone, siteemail, sitestatus) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Sites", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Sites", "Home");
            }
        }

        public ActionResult add_flats(string flatsitename, string flatwing, string flatfloor, string flatno, string flattype, string flatarea, string flatstatus)
        {
            try
            {
                if (obj.insert_flats(flatsitename, flatwing, flatfloor, flatno, flattype, flatarea, flatstatus) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Sites", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Sites", "Home");
            }
        }

        public ActionResult add_executive(string exename, string execode, string exeemail, string exemob, string exeadd, string exejoin, string exebirth, string exestatus)
        {
            try
            {
                if (obj.insert_executive(exename, execode, exeemail, exemob, exeadd, exejoin, exebirth, exestatus) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Executive", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Executive", "Home");
            }
        }

        public ActionResult add_franchies(string francname, string franccode, string francemail, string francmob, string francadd, string francjoin, string francstatus)
        {
            try
            {
                if (obj.insert_franchies(francname, franccode, francemail, francmob, francadd, francjoin, francstatus) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Franchies", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Franchies", "Home");
            }
        }

        public ActionResult add_applicant(string applname, string applemail, string applmob, string appladdr, string applpan, string applaadhar,
            string apploccu, string applbirth, string applage, string coapplname, string coapplpan, string coapplaadhar, string coapploccu, string coapplbirth, string applstatus)
        {
            try
            {
                if (obj.insert_applicant(applname, applemail, applmob, appladdr, applpan, applaadhar,
             apploccu, applbirth, applage, coapplname, coapplpan, coapplaadhar, coapploccu, coapplbirth, applstatus) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Customer", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Customer", "Home");
            }
        }

        public ActionResult add_booking(string bno, string breferred, string bincentive, string bincome, string bcancel, string btamount,
            string bramount, string bblder, string bparking, string bcharges, string bfollowup, string bstatus, string bremark, string bsite, string bflats, string bapplicant, string bexecutive, string bfranchies)
        {
            try
            {
                if (obj.insert_booking(bno, breferred, bincentive, bincome, bcancel, btamount,
             bramount, bblder, bparking, bcharges, bfollowup, bstatus, bremark, bsite, bflats, bapplicant, bexecutive, bfranchies) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Booking", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Booking", "Home");
            }
        }

        public ActionResult add_paymentcommit(string ctype, string camount, string cstatus, string bid, string cremark)
        {
            try
            {
                if (obj.insert_paymentcommit(ctype, camount, cstatus, cremark, bid) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("PaymentCommit", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("PaymentCommit", "Home");
            }
        }

        public ActionResult add_paymentdetails(string pamt, string pdate, string pmode, string chkid, string chkdate, string bname,
            string ptype, string bldpay, string bnkpay, string sts, string bid)
        {
            try
            {
                if (obj.insert_paymentdetails(pamt, pdate, pmode, chkid, chkdate, bname, ptype, bldpay, bnkpay, sts, bid) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("PaymentDetails", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("PaymentDetails", "Home");
            }
        }
        
        public ActionResult add_agreement(string ano, string adate, string anotary, string aamount, string aadjustment, string aextra, string astatus, string bid)
        {
            try
            {
                if (obj.insert_agreement(ano, adate, anotary, aamount, aadjustment, aextra, astatus, bid) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Agreement", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Agreement", "Home");
            }
        }

        public ActionResult add_finance(string fintype, string finname, string finexe, string finexemob, string finexeemail, string filehanddate,
            string filesta, string filesanctdate, string reqloanamt, string sanctloanamt, string disburseamt, string actloanamt, string recddamt, 
            string remddamt, string rateofinter, string emiamt, string emimonths, string bid, string finstat)
        {
            try
            {
                if (obj.insert_finance(fintype, finname, finexe, finexemob, finexeemail, filehanddate,
             filesta, filesanctdate, reqloanamt, sanctloanamt, disburseamt, actloanamt, recddamt, remddamt, rateofinter, emiamt, emimonths, bid, finstat) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("Finance", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Finance", "Home");
            }
        }

        public ActionResult add_filestatus(string chrg, string lfee, string chid, string chdate, string bnknm, string figst,
            string lfamt, string fid, string fstatus)
        {
            try
            {
                if (obj.insert_filestatus(chrg, lfee, chid, chdate, bnknm, figst, lfamt, fid, fstatus) == 1)
                {
                    TempData["AlertMessage"] = "All the details saved successfully.";
                }
                else
                {
                    TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                }
                return RedirectToAction("FileStatus", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("FileStatus", "Home");
            }
        }

        /**
         * Drop Down list for site name on page load
         */
        [HttpGet]
        public ActionResult get_site(string data)
        {
            List<string>[] sites = new List<string>[7];
            sites = obj.sites_show();
            var result = new { id = sites[0],
                name = sites[1]};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /**
         * Drop Down list for applicant name on page load
         */
        [HttpGet]
        public ActionResult get_applicant(string data)
        {
            List<string>[] sites = new List<string>[7];
            sites = obj.customer_show_name();
            var result = new
            {
                id = sites[0],
                name = sites[1]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /** 
         * Drop Down list for executive name on page load
         */
        [HttpGet]
        public ActionResult get_executive(string data)
        {
            List<string>[] sites = new List<string>[7];
            sites = obj.executive_show_name();
            var result = new
            {
                id = sites[0],
                name = sites[1]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /** 
         * Drop Down list for franchies name on page load
         */
        [HttpGet]
        public ActionResult get_franchies(string data)
        {
            List<string>[] sites = new List<string>[7];
            sites = obj.franchies_show_name();
            var result = new
            {
                id = sites[0],
                name = sites[1]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /** 
         * Drop Down list for booking wise customer name on page load
         */
        [HttpGet]
        public ActionResult get_customer_booking_name(string data)
        {
            List<string>[] sites = new List<string>[7];
            sites = obj.customer_booking_show();
            var result = new
            {
                id = sites[0],
                name = sites[1]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyReport()
        {
            return View();
        }

        public ActionResult Display_pdf()
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.enquiry_show(0, 10, "");

            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            return View("DailyEnquiryPDF");
        }

        public ActionResult DailyEnquiryPDF()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Generate_pdf(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.AddHeader("sdasd","weqweqw");
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }
    }
}