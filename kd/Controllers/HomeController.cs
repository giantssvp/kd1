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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult PaymentCommitReport()
        {
            return View();
        }

        public ActionResult CostSheetReport()
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

        public ActionResult edit_record(string page, string ps, string edit_id, string filter = "", string search = "", string site = "")
        {
            try
            {
                int id = Int32.Parse(edit_id);
                List<string> edit_list = new List<string>();
                
                if (page == "Index")
                {
                    edit_list = obj.get_edit_record("daily_sitevisit", id);
                    
                    List<string> edit_list1 = new List<string>();
                    id = Int32.Parse(edit_list[1]);
                    edit_list1 = obj.get_edit_record("daily_enquiry", id);
                    edit_list.AddRange(edit_list1);                        
                    id = Int32.Parse("2");
                    edit_list1 = obj.get_edit_record("daily_followup", id);
                    edit_list.AddRange(edit_list1);                        
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "Sites")
                {
                    edit_list = obj.get_edit_record("flats", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "Executive")
                {
                    edit_list = obj.get_edit_record("executive", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "Franchies")
                {
                    edit_list = obj.get_edit_record("franchies", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "Customer")
                {
                    edit_list = obj.get_edit_record("applicant", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "FileStatus")
                {
                    edit_list = obj.get_edit_record("file_details", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "Finance")
                {
                    edit_list = obj.get_edit_record("finance_details", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "Agreement")
                {
                    edit_list = obj.get_edit_record("aggrement", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "Booking")
                {
                    edit_list = obj.get_edit_record("bookings", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "PaymentCommit")
                {
                    edit_list = obj.get_edit_record("payment_commitment", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "PaymentDetails")
                {
                    edit_list = obj.get_edit_record("payment_details", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "CustomerCostSheet")
                {
                    edit_list = obj.get_edit_record("cost_sheet", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }
                else if (page == "BuilderCostSheet")
                {
                    edit_list = obj.get_edit_record("cost_sheet", id);
                    ViewBag.edit_list = edit_list;
                    ViewBag.edit_str = "edit";
                }

                return View(page);
            }
            catch (Exception ex)
            {
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
                
        public ActionResult add_enquiry(string enqname, string enqaddress, string enqmob, string enqaltmob, string enqemail,
            List<string> enqrequirement, string enqoccu, string enqincome, string enqbudget, string enqdown, 
            string enqcurstatus, string enqvisit, string enqsource, string enqsourcedetails, string enqsanctiontype, 
            string submit_btn, string edit_id="0")
        {
            try
            {
                string req = string.Join(" ", enqrequirement);
                if (submit_btn == "Save")
                {
                    if (obj.insert_enquiry(enqname, enqaddress, enqmob, enqaltmob, enqemail, req, enqoccu, enqincome, 
                        enqbudget, enqdown, enqcurstatus, enqvisit, enqsource, enqsourcedetails, enqsanctiontype) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_enquiry(enqname, enqaddress, enqmob, enqaltmob, enqemail, req, enqoccu, enqincome,
                        enqbudget, enqdown, enqcurstatus, enqvisit, enqsource, enqsourcedetails, enqsanctiontype, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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
        
        public ActionResult add_de_sitevisit(string enqnamevisit, string enqsitename, string enqtype, string enqwing, string enqflatno,
            string enqsize, string enqexename1, string enqexename2, string enqexename3, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_de_sitevisit(enqnamevisit, enqsitename, enqtype, enqwing, enqflatno, enqsize, enqexename1, 
                        enqexename2, enqexename3) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_de_sitevisit(enqnamevisit, enqsitename, enqtype, enqwing, enqflatno, enqsize, enqexename1,
                        enqexename2, enqexename3, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_de_followup(string enqnamefollowup, string enqfollow, string enqnextfollow, string enqfollowdetails, 
            string enqexenamefollowup1, string enqexenamefollowup2, string enqexenamefollowup3, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_de_followup(enqnamefollowup, enqfollow, enqnextfollow, enqfollowdetails, enqexenamefollowup1, enqexenamefollowup2,
                        enqexenamefollowup3) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_de_followup(enqnamefollowup, enqfollow, enqnextfollow, enqfollowdetails, enqexenamefollowup1, enqexenamefollowup2,
                        enqexenamefollowup3, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_sites(string sitename, string sitetype, string siteaddress, string sitephone, string siteemail, string sitestatus, string sitesanctiontype)
        {
            try
            {
                if (obj.insert_sites(sitename, sitetype, siteaddress, sitephone, siteemail, sitestatus, sitesanctiontype) == 1)
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

        public ActionResult add_flats(string flatsitename, string flatwing, string flatfloor, string flatno, string flattype, string flatarea, string flatstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_flats(flatsitename, flatwing, flatfloor, flatno, flattype, flatarea, flatstatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_flats(flatsitename, flatwing, flatfloor, flatno, flattype, flatarea, flatstatus, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_plots(string plotsitename, string plotno, string plotarea, string plotstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_plots(plotsitename, plotno, plotarea, plotstatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_plots(plotsitename, plotno, plotarea, plotstatus, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_executive(string exename, string execode, string exeemail, string exemob, string exeadd, string exejoin, string exebirth, string exestatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_executive(exename, execode, exeemail, exemob, exeadd, exejoin, exebirth, exestatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_executive(exename, execode, exeemail, exemob, exeadd, exejoin, exebirth, exestatus, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_franchies(string francname, string franccode, string francemail, string francmob, string francadd, string francjoin, string francstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_franchies(francname, franccode, francemail, francmob, francadd, francjoin, francstatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_franchies(francname, franccode, francemail, francmob, francadd, francjoin, francstatus, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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
            string apploccu, string applbirth, string applage, string coapplname, string coapplpan, string coapplaadhar, string coapploccu, string coapplbirth, string applstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_applicant(applname, applemail, applmob, appladdr, applpan, applaadhar,
                                             apploccu, applbirth, applage, coapplname, coapplpan, coapplaadhar, 
                                             coapploccu, coapplbirth, applstatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_applicant(applname, applemail, applmob, appladdr, applpan, applaadhar,
                                             apploccu, applbirth, applage, coapplname, coapplpan, coapplaadhar, 
                                             coapploccu, coapplbirth, applstatus, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_booking(string bno, string breferred, string bapplicant, string btamount, string bramount, string bblder,
            string bsite, string bwing, string bflats, string bcharges, string bparking, string bcancel,
            string bfollowup, string bstatus, string bremark, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_booking(bno, breferred, bapplicant, btamount, bramount, bblder, bsite, bwing, bflats, 
                        bcharges, bparking, bcancel, bfollowup, bstatus, bremark) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_booking(bno, breferred, bapplicant, btamount, bramount, bblder, bsite, bwing, bflats, 
                        bcharges, bparking, bcancel, bfollowup, bstatus, bremark, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_exe_franc_audit(string ename, string fname, string bno, string incentive, string share, string paidamt, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_exe_franc_audit(ename, fname, bno, incentive, share, paidamt) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_exe_franc_audit(ename, fname, bno, incentive, share, paidamt, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
                }
                return RedirectToAction("ExecutiveAudits", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("ExecutiveAudits", "Home");
            }
        }

        public ActionResult add_paymentcommit(string ctype, string camount, string cstatus, string bid, string cremark, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_paymentcommit(ctype, camount, cstatus, cremark, bid) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_paymentcommit(ctype, camount, cstatus, cremark, bid, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_paymentdetails(string pamt, string pmode, string chkid, string chkdate, string bname,
            string ptype, string bldpay, string bnkpay, string sts, string bid, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_paymentdetails(pamt, pmode, chkid, chkdate, bname, ptype, bldpay, bnkpay, sts, bid) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_paymentdetails(pamt, pmode, chkid, chkdate, bname, ptype, bldpay, bnkpay, sts, bid, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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
        
        public ActionResult add_agreement(string ano, string adate, string anotary, string aamount, string aadjustment, string aextra, string gst, string astatus, string bid, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_agreement(ano, adate, anotary, aamount, aadjustment, aextra, gst, astatus, bid) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_agreement(ano, adate, anotary, aamount, aadjustment, aextra, gst, astatus, bid, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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
            string remddamt, string rateofinter, string emiamt, string emimonths, string bid, string finstat, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_finance(fintype, finname, finexe, finexemob, finexeemail, filehanddate,
                                           filesta, filesanctdate, reqloanamt, sanctloanamt, disburseamt, 
                                           actloanamt, recddamt, remddamt, rateofinter, emiamt, emimonths, 
                                           bid, finstat) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_finance(fintype, finname, finexe, finexemob, finexeemail, filehanddate,
                                           filesta, filesanctdate, reqloanamt, sanctloanamt, disburseamt, 
                                           actloanamt, recddamt, remddamt, rateofinter, emiamt, emimonths, 
                                           bid, finstat, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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
            string lfamt, string fid, string fstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_filestatus(chrg, lfee, chid, chdate, bnknm, figst, lfamt, fid, fstatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_filestatus(chrg, lfee, chid, chdate, bnknm, figst, lfamt, fid, fstatus, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_customer_cost_sheet(string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyreg, string gst, string otheramt, string grandtotal, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_cost_sheet("customer", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_cost_sheet("customer", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        public ActionResult add_builder_cost_sheet(string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyreg, string gst, string otheramt, string grandtotal, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save")
                {
                    if (obj.insert_cost_sheet("builder", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update")
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_cost_sheet("builder", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyreg, gst, otheramt, grandtotal, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
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

        /**
         * Drop Down list for site name on page load
         */
        [HttpGet]
        public ActionResult get_site(string data, string site_type = "All")
        {
            List<string>[] sites = new List<string>[7];
            sites = obj.sites_show(site_type);
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
         * Drop Down list for wing name on page load
         */
        [HttpPost]
        public ActionResult get_wing_name(string site_id)
        {
            List<string>[] wing = new List<string>[7];
            wing = obj.wing_show_name(site_id);
            var result = new
            {
                id = wing[0],
                name = wing[5]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /** 
         * Drop Down list for flat no on page load
         */
        [HttpPost]
        public ActionResult get_flat_no(string wing_name, string site_id)
        {
            List<string>[] flat = new List<string>[7];
            flat = obj.flat_show_no(wing_name, site_id);
            var result = new
            {
                id = flat[0],
                name = flat[1]
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

        /**
         * Drop Down list for site customer name on page load
         */
        [HttpGet]
        public ActionResult get_daily_customer_name(string data)
        {
            List<string>[] dcname = new List<string>[21];
            dcname = obj.daily_customer_name_show();
            var result = new
            {
                id = dcname[0],
                name = dcname[1]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult DailyReport()
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        public ActionResult DailyEnquiryPDF()
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        public ActionResult FinanceReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult FinanceReportPDF()
        {
            return View();
        }

        [Authorize]
        public ActionResult SiteReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult SiteReportPDF()
        {
            return View();
        }

        [Authorize]
        public ActionResult CustomerReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult CustomerReportPDF()
        {
            return View();
        }

        [Authorize]
        public ActionResult FileStatusReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult FileStatusReportPDF()
        {
            return View();
        }

        [Authorize]
        public ActionResult ExecutiveReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult ExecutiveReportPDF()
        {
            return View();
        }

        [Authorize]
        public ActionResult ProfitLossReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult ProfitLossReportPDF()
        {
            return View();
        }

        [Authorize]
        public ActionResult MasterReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult MasterReportPDF()
        {
            return View();
        }

        [Authorize]
        public ActionResult ExecutiveAudits()
        {
            return View();
        }
    }
}