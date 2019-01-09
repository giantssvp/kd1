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

        //Column names for search in table
        public string daily_customer_column = "Customer_Name, Current_Status, Sanction_Type, Enquiry_Date";
        public string daily_sitevisit_column = "Customer_Name, Site_Name, Site_Type, Exe_franc1_Name, Exe_franc2_Name, Exe_franc3_Name";
        public string daily_followup_column = "Customer_Name, Followup_Date, Next_Followup_Date, Exe_franc1_Name, Exe_franc2_Name, Exe_franc3_Name";
        public string sites_column = "Site_Name, Site_Type, Address, Sanction_Type";
        public string flat_plot_column = "Site_Name, Site_Type, Address, Sanction_Type, Number, Area";
        public string notes_column = "Note_Summary, Note_Details, Date";
        public string executive_franchies_column = "Name, Code, Joining_Date, Executive_Type";
        public string user_column = "Name, User_Name, Email_Id, Phone, User_Type, Status";
        public string executive_audit_column = "efi_date, efa_date, Exe_franc1_Name, Exe_franc1_Code, Exe_franc1_Phone, Site_Name, Site_Type, Number, Wing, Applicant_Name";
        public string applicant_column = "Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No, Applicant_Address";
        public string co_applicant_column = "Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No, Co_Applicant_Name, Co_Applicant_Pan_No, Co_Applicant_Adhar_No";
        public string bookings_column = "Referenceby, bookings_date, Site_Name, Site_Type, Number, Type, Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No";
        public string payment_commitment_column = "Commitment_Type, Commitment_Date, Booking_No, bookings_date, Site_Name, Site_Type, Number, Type, Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No";
        public string payment_details_column = "Payment_Mode, Cheque_Date, Cheque_Id, Bank_Name, Payment_Type, Booking_No, Referenceby, bookings_date, Site_Name, Site_Type, Number, Type, Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No";
        public string agreement_column = "Agreement_date, Agreement_No, agreement_record_date, Booking_No, Referenceby, bookings_date, Site_Name, Site_Type, Number, Type, Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No";
        public string finanace_column = "Finance_Type, Finance_Name, Finance_Executive_Name, File_Handover_Date, Booking_No, Referenceby, bookings_date, Site_Name, Site_Type, Number, Type, Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No";
        public string file_column = "Cheque_Date, Cheque_Id, Bank_Name, Finance_Type, Finance_Name, Finance_Executive_Name, File_Handover_Date, Booking_No, Referenceby, bookings_date, Site_Name, Site_Type, Number, Type, Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No";
        public string cost_sheet_column = "Basic_Rate, Type, Cost_Sheet_Type, Site_Name, Site_Type, Sanction_Type";

        protected bool isUserAuthenticated()
        {
            //if (!this.User.Identity.IsAuthenticated)
            if (!Request.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
                return false;
            }
            else
            {
                return true;
            }
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        public ActionResult ResetPassword()
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
            List<string>[] list = new List<string>[17];

            list = obj.enquiry_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search:search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        [Authorize]
        public ActionResult SiteVisit(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[8];

            list = obj.sitevisit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        [Authorize]
        public ActionResult Followup(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[8];

            list = obj.followup_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        [Authorize]
        public ActionResult Sites(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[9];

            list = obj.sites_show("All", Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        [Authorize]
        public ActionResult Flats(string ps = "10", string site = "All", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] sites = new List<string>[9];
            List<string>[] list = new List<string>[9];
            sites = obj.sites_show(site_type:"All");
            
            sites[0].Insert(0, "AllPlots");
            sites[1].Insert(0, "All Plots");
            sites[0].Insert(0, "AllFlats");
            sites[1].Insert(0, "All Flats");
            sites[0].Insert(0, "All");
            sites[1].Insert(0, "All");

            ViewBag.sites = sites.ToList();
            ViewBag.total_site = sites[0].Count();
            ViewBag.site = site;

            list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);
            
            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        [Authorize]
        public ActionResult Plots(string ps = "10", string site = "All", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] sites = new List<string>[6];
            List<string>[] list = new List<string>[6];
            sites = obj.sites_show(site_type: "Plot");
            sites[0].Insert(0, "AllPlots");
            sites[1].Insert(0, "All Plots");
            sites[0].Insert(0, "AllFlats");
            sites[1].Insert(0, "All Flats");
            sites[0].Insert(0, "All");
            sites[1].Insert(0, "All");

            ViewBag.sites = sites.ToList();
            ViewBag.total_site = sites[0].Count();
            ViewBag.site = site;

            list = obj.plots_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);
            
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
        public ActionResult Users(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.user_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

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
            List<string>[] list = new List<string>[12];

            list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        [Authorize]
        public ActionResult Customer_Sec(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[8];

            list = obj.customer_sec_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

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
        public ActionResult Alarms(string page = "followup")
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[14];

            list = obj.alarms_show(page);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.Page = page;

            return View();
        }

        [Authorize]
        public ActionResult Notes(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[14];

            list = obj.notes_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        [Authorize]
        public ActionResult ExecutiveIncentives(string ps = "10", string filter = "", string search = "")
        {
            ViewBag.total = 0;
            HttpContext.Session.Add("offset", 0);
            List<string>[] list = new List<string>[8];

            list = obj.executive_incentive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), Int32.Parse(ps), search: search);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            ViewBag.pageSize = Int32.Parse(ps);
            ViewBag.search = search;

            return View();
        }

        public ActionResult delete_record(string page, string ps, string del_id, string filter = "", string search = "", string site = "")
        {
            try
            {
                int id = Int32.Parse(del_id);
                int pass = 0;

                if (isUserAuthenticated())
                {
                    if (page == "Index")
                    {
                        if (obj.Delete_Record("daily_customer", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "SiteVisit")
                    {
                        if (obj.Delete_Record("daily_sitevisit", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "Followup")
                    {
                        if (obj.Delete_Record("daily_followup", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "Sites")
                    {
                        if (obj.Delete_Record("sites", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "Flats")
                    {
                        if (obj.Delete_Record("flat_plot", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "Executive")
                    {
                        if (obj.Delete_Record("executive_franchies", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "Users")
                    {
                        if (obj.Delete_Record("user", id) == 0)
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
                    else if (page == "Customer_Sec")
                    {
                        if (obj.Delete_Record("co_applicant", id) == 0)
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
                        if (obj.Delete_Record("agreement", id) == 0)
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
                    else if (page == "BuilderCostSheet")
                    {
                        if (obj.Delete_Record("cost_sheet", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "Notes")
                    {
                        if (obj.Delete_Record("notes", id) == 0)
                        {
                            pass = 1;
                        }
                    }
                    else if (page == "ExecutiveIncentives")
                    {
                        if (obj.Delete_Record("execu_fran_incentive", id) == 0)
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

                if (isUserAuthenticated())
                {
                    if (page == "Index")
                    {
                        edit_list = obj.get_edit_record("daily_customer", id);
                    }
                    else if (page == "SiteVisit")
                    {
                        edit_list = obj.get_edit_record("daily_sitevisit", id);
                    }
                    else if (page == "Followup")
                    {
                        edit_list = obj.get_edit_record("daily_followup", id);
                    }
                    else if (page == "Sites")
                    {
                        edit_list = obj.get_edit_record("sites", id);
                    }
                    else if (page == "Flats")
                    {
                        edit_list = obj.get_edit_record("flat_plot", id);
                    }
                    else if (page == "Executive")
                    {
                        edit_list = obj.get_edit_record("executive_franchies", id);
                    }
                    else if (page == "Users")
                    {
                        edit_list = obj.get_edit_record("user", id);
                    }
                    else if (page == "Customer")
                    {
                        edit_list = obj.get_edit_record("applicant", id);
                    }
                    else if (page == "Customer_Sec")
                    {
                        edit_list = obj.get_edit_record("co_applicant", id);
                    }
                    else if (page == "FileStatus")
                    {
                        edit_list = obj.get_edit_record("file_details", id);
                        List<string> edit_list1 = new List<string>();
                        edit_list1 = obj.get_showcase_from_financeID(Int32.Parse(edit_list[8]));
                        edit_list.Insert(7, edit_list1[0]);
                        edit_list.Insert(8, edit_list1[1]);
                        edit_list.Insert(9, edit_list1[2]);
                    }
                    else if (page == "Finance")
                    {
                        edit_list = obj.get_edit_record("finance_details", id);
                        List<string> edit_list1 = new List<string>();
                        edit_list1 = obj.get_showcase_from_bookingID(Int32.Parse(edit_list[20]));
                        edit_list.Insert(7, edit_list1[0]);
                        edit_list.Insert(8, edit_list1[1]);
                        edit_list.Insert(9, edit_list1[2]);
                    }
                    else if (page == "Agreement")
                    {
                        edit_list = obj.get_edit_record("agreement", id);
                        List<string> edit_list1 = new List<string>();
                        edit_list1 = obj.get_showcase_from_bookingID(Int32.Parse(edit_list[5]));
                        edit_list.Insert(7, edit_list1[0]);
                        edit_list.Insert(8, edit_list1[1]);
                        edit_list.Insert(9, edit_list1[2]);
                    }
                    else if (page == "Booking")
                    {
                        edit_list = obj.get_edit_record("bookings", id);
                    }
                    else if (page == "PaymentCommit")
                    {
                        edit_list = obj.get_edit_record("payment_commitment", id);
                        List<string> edit_list1 = new List<string>();
                        edit_list1 = obj.get_showcase_from_bookingID(Int32.Parse(edit_list[6]));
                        edit_list.AddRange(edit_list1);
                    }
                    else if (page == "PaymentDetails")
                    {
                        edit_list = obj.get_edit_record("payment_details", id);
                        List<string> edit_list1 = new List<string>();
                        edit_list1 = obj.get_showcase_from_bookingID(Int32.Parse(edit_list[11]));
                        edit_list.Insert(7, edit_list1[0]);
                        edit_list.Insert(8, edit_list1[1]);
                        edit_list.Insert(9, edit_list1[2]);
                    }
                    else if (page == "ExecutiveIncentives")
                    {
                        edit_list = obj.get_edit_record("execu_fran_incentive", id);
                        List<string> edit_list1 = new List<string>();
                        edit_list1 = obj.get_showcase_from_efaID(Int32.Parse(edit_list[1]));
                        edit_list.Insert(4, edit_list1[0]); //Executive ID This is just to align applicant ID and other values at index 7,8,9.
                        edit_list.Insert(5, edit_list1[0]); //Executive ID
                        edit_list.Insert(6, edit_list1[0]); //Executive ID
                        edit_list.Insert(7, edit_list1[1]); //Applicant ID
                        edit_list.Insert(8, edit_list1[2]); //Site ID
                        edit_list.Insert(9, edit_list1[3]); //Flat ID
                    }
                    else if (page == "Notes")
                    {
                        edit_list = obj.get_edit_record("notes", id);
                    }                    
                    else if (page == "CustomerCostSheet")
                    {
                        edit_list = obj.get_edit_record("cost_sheet", id);
                    }
                    else if (page == "BuilderCostSheet")
                    {
                        edit_list = obj.get_edit_record("cost_sheet", id);
                    }

                    edit_list = edit_list.ToList().ConvertAll(s => s.Replace(" ", "\u00A0"));
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

        public ActionResult First(string page, string ps, string filter = "", string search = "", string site = "All")
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
                else if (page == "SiteVisit")
                {
                    list = obj.sitevisit_show(0, page_size, search: search);
                }
                else if (page == "Followup")
                {
                    list = obj.followup_show(0, page_size, search: search);
                }
                else if (page == "Sites")
                {
                    list = obj.sites_show(offset: 0, limit: page_size, search: search);
                }
                else if (page == "Flats")
                {
                    List<string>[] sites = new List<string>[9];
                    sites = obj.sites_show(site_type: "All");
                    sites[0].Insert(0, "AllPlots");
                    sites[1].Insert(0, "All Plots");
                    sites[0].Insert(0, "AllFlats");
                    sites[1].Insert(0, "All Flats");
                    sites[0].Insert(0, "All");
                    sites[1].Insert(0, "All");

                    ViewBag.sites = sites.ToList();
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;

                    list = obj.flats_show(site, 0, page_size, search: search);                    
                }
                else if(page == "Executive")
                {                                        
                    list = obj.executive_show(0, page_size, search: search);
                }
                else if (page == "Users")
                {
                    list = obj.user_show(0, page_size, search: search);
                }
                else if(page == "Customer")
                {
                    list = obj.customer_show(0, page_size, search: search);
                }
                else if (page == "Customer_Sec")
                {
                    list = obj.customer_sec_show(0, page_size, search: search);
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
                else if (page == "Notes")
                {
                    list = obj.notes_show(0, page_size, search: search);
                }
                else if (page == "ExecutiveIncentives")
                {
                    list = obj.executive_incentive_show(0, page_size, search: search);
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

        public ActionResult Previous(string page, string ps, string filter = "", string search = "", string site = "All")
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
                if (page == "SiteVisit")
                {
                    list = obj.sitevisit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                if (page == "Followup")
                {
                    list = obj.followup_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Sites")
                {
                    list = obj.sites_show(offset: Int32.Parse(HttpContext.Session["offset"].ToString()), limit:page_size, search: search);
                }
                else if (page == "Flats")
                {
                    List<string>[] sites = new List<string>[9];
                    sites = obj.sites_show(site_type: "All");
                    sites[0].Insert(0, "AllPlots");
                    sites[1].Insert(0, "All Plots");
                    sites[0].Insert(0, "AllFlats");
                    sites[1].Insert(0, "All Flats");
                    sites[0].Insert(0, "All");
                    sites[1].Insert(0, "All");

                    ViewBag.sites = sites.ToList();
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;

                    list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);                    
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Users")
                {
                    list = obj.user_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer_Sec")
                {
                    list = obj.customer_sec_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
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
                else if (page == "Notes")
                {
                    list = obj.notes_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "ExecutiveIncentives")
                {
                    list = obj.executive_incentive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
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

        public ActionResult Next(string page, string ps, string filter = "", string search = "", string site = "All")
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
                        query = "daily_customer";
                    }
                    else
                    {
                        query = "daily_customer where CONCAT(" + daily_customer_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "SiteVisit")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_daily_sitevisit";
                    }
                    else
                    {
                        query = "v_daily_sitevisit where CONCAT(" + daily_sitevisit_column +") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Followup")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_daily_followup";
                    }
                    else
                    {
                        query = "v_daily_followup where CONCAT(" + daily_followup_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Sites")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "sites";
                    }
                    else
                    {
                        query = "sites where CONCAT(" + sites_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Flats")
                {
                    string query = "";
                    if (site == "All")
                    {
                        if (search == "")
                        {
                            query = "v_flat_plot";
                        }
                        else
                        {
                            query = "v_flat_plot where CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }
                    else if (site == "AllFlats")
                    {
                        if (search == "")
                        {
                            query = "v_flat_plot where Site_Type = 'Flat'";
                        }
                        else
                        {
                            query = "v_flat_plot where Site_Type = 'Flat' and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }
                    else if (site == "AllPlots")
                    {
                        if (search == "")
                        {
                            query = "v_flat_plot where Site_Type = 'Plot'";
                        }
                        else
                        {
                            query = "v_flat_plot where Site_Type = 'Plot' and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }
                    else
                    {
                        //int id = obj.get_site_id_by_name(site);
                        if (search == "")
                        {
                            query = "v_flat_plot where Site_Name = " + site;
                        }
                        else
                        {
                            query = "v_flat_plot where Site_Name = " + site + " and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }

                    cnt = obj.get_count(query);
                }
                else if (page == "Executive")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "executive_franchies";
                    }
                    else
                    {
                        query = "executive_franchies where CONCAT(" + executive_franchies_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Users")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "user";
                    }
                    else
                    {
                        query = "user where CONCAT(" + user_column + ") LIKE '%" + search + "%'";
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
                        query = "applicant where CONCAT(" + applicant_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Customer_Sec")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_co_applicant";
                    }
                    else
                    {
                        query = "v_co_applicant where CONCAT(" + co_applicant_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Booking")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_bookings";
                    }
                    else
                    {
                        query = "v_bookings where CONCAT(" + bookings_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentCommit")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_payment_commitment";
                    }
                    else
                    {
                        query = "v_payment_commitment where CONCAT(" + payment_commitment_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentDetails")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_payment_details";
                    }
                    else
                    {
                        query = "v_payment_details where CONCAT(" + payment_details_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Agreement")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_agreement";
                    }
                    else
                    {
                        query = "v_agreement where CONCAT(" + agreement_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Finance")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_finance_details";
                    }
                    else
                    {
                        query = "v_finance_details where CONCAT(" + finanace_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "FileStatus")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_file_details";
                    }
                    else
                    {
                        query = "v_file_details where CONCAT(" + file_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "CustomerCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'customer'";
                    }
                    else
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'customer' and CONCAT(" + cost_sheet_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "BuilderCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'builder'";
                    }
                    else
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'builder' and CONCAT(" + cost_sheet_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Notes")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "notes";
                    }
                    else
                    {
                        query = "notes where CONCAT(" + notes_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "ExecutiveIncentives")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_ef_audit_incentive";
                    }
                    else
                    {
                        query = "v_ef_audit_incentive where CONCAT(" + executive_audit_column + ") LIKE '%" + search + "%'";
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
                else if (page == "SiteVisit")
                {
                    list = obj.sitevisit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Followup")
                {
                    list = obj.followup_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Sites")
                {
                    list = obj.sites_show(offset: Int32.Parse(HttpContext.Session["offset"].ToString()), limit: page_size, search: search);
                }                
                else if (page == "Flats")
                {
                    List<string>[] sites = new List<string>[9];
                    sites = obj.sites_show(site_type: "All");
                    sites[0].Insert(0, "AllPlots");
                    sites[1].Insert(0, "All Plots");
                    sites[0].Insert(0, "AllFlats");
                    sites[1].Insert(0, "All Flats");
                    sites[0].Insert(0, "All");
                    sites[1].Insert(0, "All");

                    ViewBag.sites = sites.ToList();
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;

                    list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);                                      
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Users")
                {
                    list = obj.user_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer_Sec")
                {
                    list = obj.customer_sec_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
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
                else if (page == "Notes")
                {
                    list = obj.notes_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "ExecutiveIncentives")
                {
                    list = obj.executive_incentive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
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
        
        public ActionResult Last(string page, string ps, string filter = "", string search = "", string site = "All")
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
                        query = "daily_customer";
                    }
                    else
                    {
                        query = "daily_customer where CONCAT(" + daily_customer_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "SiteVisit")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_daily_sitevisit";
                    }
                    else
                    {
                        query = "v_daily_sitevisit where CONCAT(" + daily_sitevisit_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Followup")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_daily_followup";
                    }
                    else
                    {
                        query = "v_daily_followup where CONCAT(" + daily_followup_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Sites")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "sites";
                    }
                    else
                    {
                        query = "sites where CONCAT(" + sites_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Flats")
                {
                    string query = "";
                    if (site == "All")
                    {
                        if (search == "")
                        {
                            query = "v_flat_plot";
                        }
                        else
                        {
                            query = "v_flat_plot where CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }
                    else if (site == "AllFlats")
                    {
                        if (search == "")
                        {
                            query = "v_flat_plot where Site_Type = 'Flat'";
                        }
                        else
                        {
                            query = "v_flat_plot where Site_Type = 'Flat' and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }
                    else if (site == "AllPlots")
                    {
                        if (search == "")
                        {
                            query = "v_flat_plot where Site_Type = 'Plot'";
                        }
                        else
                        {
                            query = "v_flat_plot where Site_Type = 'Plot' and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }
                    else
                    {
                        //int id = obj.get_site_id_by_name(site);
                        if (search == "")
                        {
                            query = "v_flat_plot where Site_Name = " + site;
                        }
                        else
                        {
                            query = "v_flat_plot where Site_Name = " + site + " and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%'";
                        }
                    }                    
                    
                    cnt = obj.get_count(query);
                }
                else if (page == "Executive")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "executive_franchies";
                    }
                    else
                    {
                        query = "executive_franchies where CONCAT(" + executive_franchies_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Users")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "user";
                    }
                    else
                    {
                        query = "user where CONCAT(" + user_column + ") LIKE '%" + search + "%'";
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
                        query = "applicant where CONCAT(" + applicant_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Customer_Sec")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_co_applicant";
                    }
                    else
                    {
                        query = "v_co_applicant where CONCAT(" + co_applicant_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Booking")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_bookings";
                    }
                    else
                    {
                        query = "v_bookings where CONCAT(" + bookings_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentCommit")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_payment_commitment";
                    }
                    else
                    {
                        query = "v_payment_commitment where CONCAT(" + payment_commitment_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "PaymentDetails")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_payment_details";
                    }
                    else
                    {
                        query = "v_payment_details where CONCAT(" + payment_details_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Agreement")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_agreement";
                    }
                    else
                    {
                        query = "v_agreement where CONCAT(" + agreement_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Finance")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_finance_details";
                    }
                    else
                    {
                        query = "v_finance_details where CONCAT(" + finanace_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "FileStatus")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_file_details";
                    }
                    else
                    {
                        query = "v_file_details where CONCAT(" + file_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "CustomerCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'customer'";
                    }
                    else
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'customer' and CONCAT(" + cost_sheet_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "BuilderCostSheet")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'builder'";
                    }
                    else
                    {
                        query = "v_cost_sheet where Cost_Sheet_Type = 'builder' and CONCAT(" + cost_sheet_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "Notes")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "notes";
                    }
                    else
                    {
                        query = "notes where CONCAT(" + notes_column + ") LIKE '%" + search + "%'";
                    }
                    cnt = obj.get_count(query);
                }
                else if (page == "ExecutiveIncentives")
                {
                    string query = "";
                    if (search == "")
                    {
                        query = "v_ef_audit_incentive";
                    }
                    else
                    {
                        query = "v_ef_audit_incentive where CONCAT(" + executive_audit_column + ") LIKE '%" + search + "%'";
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
                else if (page == "SiteVisit")
                {
                    list = obj.sitevisit_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Followup")
                {
                    list = obj.followup_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Sites")
                {
                    list = obj.sites_show(offset: Int32.Parse(HttpContext.Session["offset"].ToString()), limit: page_size, search: search);
                }
                else if (page == "Flats")
                {
                    List<string>[] sites = new List<string>[9];
                    sites = obj.sites_show(site_type: "All");
                    sites[0].Insert(0, "AllPlots");
                    sites[1].Insert(0, "All Plots");
                    sites[0].Insert(0, "AllFlats");
                    sites[1].Insert(0, "All Flats");
                    sites[0].Insert(0, "All");
                    sites[1].Insert(0, "All");

                    ViewBag.sites = sites.ToList();
                    ViewBag.total_site = sites[0].Count();
                    ViewBag.site = site;

                    list = obj.flats_show(site, Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);                                        
                }
                else if (page == "Executive")
                {
                    list = obj.executive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Users")
                {
                    list = obj.user_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer")
                {
                    list = obj.customer_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "Customer_Sec")
                {
                    list = obj.customer_sec_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
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
                else if (page == "Notes")
                {
                    list = obj.notes_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
                }
                else if (page == "ExecutiveIncentives")
                {
                    list = obj.executive_incentive_show(Int32.Parse(HttpContext.Session["offset"].ToString()), page_size, search: search);
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
                string req = null;
                if (enqrequirement != null)
                {
                    req = string.Join(" ", enqrequirement);
                }
                if (submit_btn == "Save" && isUserAuthenticated())
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
                else if (submit_btn == "Update" && isUserAuthenticated())
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
        
        public ActionResult add_de_sitevisit(string enqnamevisit, string enqsitename, string enqwing, string enqflatno,
            string enqexename1, string enqexename2, string enqexename3, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_de_sitevisit(enqnamevisit, enqsitename, enqwing, enqflatno, enqexename1, 
                        enqexename2, enqexename3) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                                                                                                                                                                                                                                                                                            }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_de_sitevisit(enqnamevisit, enqsitename, enqwing, enqflatno, enqexename1,
                        enqexename2, enqexename3, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
                }
                return RedirectToAction("SiteVisit", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("SiteVisit", "Home");
            }
        }

        public ActionResult add_de_followup(string enqnamefollowup, string enqfollow, string enqnextfollow, string enqfollowdetails, 
            string enqexenamefollowup1, string enqexenamefollowup2, string enqexenamefollowup3, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
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
                else if (submit_btn == "Update" && isUserAuthenticated())
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
                return RedirectToAction("Followup", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Followup", "Home");
            }
        }

        public ActionResult add_sites(string sitename, string sitetype, string siteaddress, string sitephone, string siteemail, string sitestatus, string sitesanctiontype, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_sites(sitename, sitetype, siteaddress, sitephone, siteemail, sitestatus, sitesanctiontype) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_sites(sitename, sitetype, siteaddress, sitephone, siteemail, sitestatus, sitesanctiontype, "edit", id) == 1)
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

        public ActionResult add_flats(string flatsitename, string flatwing, string flatfloor, string flatno, string flattype, string flatarea, string flatstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
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
                else if (submit_btn == "Update" && isUserAuthenticated())
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
                    return RedirectToAction("Flats", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Flats", "Home");
            }
        }

        public ActionResult add_plots(string plotsitename, string plotno, string plotarea, string plotstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
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
                else if (submit_btn == "Update" && isUserAuthenticated())
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
                return RedirectToAction("Plots", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Plots", "Home");
            }
        }

        public ActionResult add_executive(string exetype, string exename, string execode, string exeemail, string exemob, string exeadd, string exejoin, string exebirth, string exestatus, string submit_btn, string edit_id = "0")
        {
            try
            {

                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_executive(exetype, exename, execode, exeemail, exemob, exeadd, exejoin, exebirth, exestatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_executive(exetype, exename, execode, exeemail, exemob, exeadd, exejoin, exebirth, exestatus, "edit", id) == 1)
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

        public ActionResult add_users(string uname, string utype, string username, string upass, string uemail, string uphone, string ustatus, string submit_btn, string edit_id = "0")
        {
            try
            {

                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_users(uname, utype, username, upass, uemail, uphone, ustatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_users(uname, utype, username, upass, uemail, uphone, ustatus, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
                }
                return RedirectToAction("Users", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Users", "Home");
            }
        }

        public ActionResult add_franchies(string francname, string franccode, string francemail, string francmob, string francadd, string francjoin, string francstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
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
                else if (submit_btn == "Update" && isUserAuthenticated())
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
            string apploccu, string applbirth, string applstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_applicant(applname, applemail, applmob, appladdr, applpan, applaadhar,
                                             apploccu, applbirth, applstatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_applicant(applname, applemail, applmob, appladdr, applpan, applaadhar,
                                             apploccu, applbirth, applstatus, "edit", id) == 1)
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

        public ActionResult add_co_applicant(string coapplname, string coapplpan, string coapplaadhar, string coapploccu, string coapplbirth, string applid, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_co_applicant(coapplname, coapplpan, coapplaadhar,
                                             coapploccu, coapplbirth, applid) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_co_applicant(coapplname, coapplpan, coapplaadhar,
                                             coapploccu, coapplbirth, applid, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
                }
                return RedirectToAction("Customer_Sec", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Customer_Sec", "Home");
            }
        }

        public ActionResult add_booking(string breferred, string bapplicant, string btamount, string bramount, string bblder,
            string bsite, string bwing, string bflats, string bcharges, string other, string bparking, string bcancel,
            string bfollowup, string bstatus, string bremark, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_booking(breferred, bapplicant, btamount, bramount, bblder, bsite, bwing, bflats, 
                        bcharges, other, bparking, bcancel, bfollowup, bstatus, bremark) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_booking(breferred, bapplicant, btamount, bramount, bblder, bsite, bwing, bflats, 
                        bcharges, other, bparking, bcancel, bfollowup, bstatus, bremark, "edit", id) == 1)
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

        public ActionResult add_exe_franc_audit(string ename, string bapplicant, string bsite, string bwing, string bflats, string incentive, string share, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_exe_franc_audit(ename, bapplicant, bsite, bwing, bflats, incentive, share) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_exe_franc_audit(ename, bapplicant, bsite, bwing, bflats, incentive, share, "edit", id) == 1)
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

        public ActionResult add_exe_franc_incentive(string ename, string bapplicant, string bsite, string bwing, string bflats, string paidamt, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_exe_franc_incentive(ename, bapplicant, bsite, bwing, bflats, paidamt) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_exe_franc_incentive(ename, bapplicant, bsite, bwing, bflats, paidamt, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
                }
                return RedirectToAction("ExecutiveIncentives", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("ExecutiveIncentives", "Home");
            }
        }

        public ActionResult add_notes(string notesummary, string notedesc, string notedate, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_notes(notesummary, notedesc, notedate) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_notes(notesummary, notedesc, notedate, "edit", id) == 1)
                    {
                        TempData["AlertMessage"] = "All the details updated successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while updating the details please do it again.";
                    }
                }
                return RedirectToAction("Notes", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction("Notes", "Home");
            }
        }

        public ActionResult add_paymentcommit(string ctype, string camount, string cdate, string cremark, string bapplicant, string bsite, string bflats, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_paymentcommit(ctype, camount, cdate, cremark, bapplicant, bsite, bflats) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_paymentcommit(ctype, camount, cdate, cremark, bapplicant, bsite, bflats, "edit", id) == 1)
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
            string ptype, string bldpay, string bnkpay, string sts, string bapplicant, string bsite, string bflats, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_paymentdetails(pamt, pmode, chkid, chkdate, bname, ptype, bldpay, bnkpay, sts, bapplicant, bsite, bflats) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_paymentdetails(pamt, pmode, chkid, chkdate, bname, ptype, bldpay, bnkpay, sts, bapplicant, bsite, bflats, "edit", id) == 1)
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
        
        public ActionResult add_agreement(string ano, string adate, string anotary, string aamount, string aadjustment, string aextra, string gst, string astatus, string bapplicant, string bsite, string bflats, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_agreement(ano, adate, anotary, aamount, aadjustment, aextra, gst, astatus, bapplicant, bsite, bflats) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_agreement(ano, adate, anotary, aamount, aadjustment, aextra, gst, astatus, bapplicant, bsite, bflats, "edit", id) == 1)
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
            string filesta, string filesanctdate, string reqloanamt, string sanctloanamt, string disburseamt, string actloanamt, string diffloanamt, string recddamt, 
            string remddamt, string rateofinter, string emiamt, string emimonths, string finstat, string bapplicant, string bsite, string bflats, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_finance(fintype, finname, finexe, finexemob, finexeemail, filehanddate,
                                           filesta, filesanctdate, reqloanamt, sanctloanamt, disburseamt, 
                                           actloanamt, diffloanamt, recddamt, remddamt, rateofinter, emiamt, emimonths, 
                                           finstat, bapplicant, bsite, bflats) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_finance(fintype, finname, finexe, finexemob, finexeemail, filehanddate,
                                           filesta, filesanctdate, reqloanamt, sanctloanamt, disburseamt, 
                                           actloanamt, diffloanamt, recddamt, remddamt, rateofinter, emiamt, emimonths, 
                                           finstat, bapplicant, bsite, bflats, "edit", id) == 1)
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
            string lfamt, string financename, string fstatus, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_filestatus(chrg, lfee, chid, chdate, bnknm, figst, lfamt, financename, fstatus) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_filestatus(chrg, lfee, chid, chdate, bnknm, figst, lfamt, financename, fstatus, "edit", id) == 1)
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

        public ActionResult add_customer_cost_sheet(string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyregpercent, string stampdutyreg, string gst, string gstpercent, string otheramt, string grandtotal, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_cost_sheet("customer", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyregpercent, stampdutyreg, gst, gstpercent, otheramt, grandtotal) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_cost_sheet("customer", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyregpercent, stampdutyreg, gst, gstpercent, otheramt, grandtotal, "edit", id) == 1)
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

        public ActionResult add_builder_cost_sheet(string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyregpercent, string stampdutyreg, string gst, string gstpercent, string otheramt, string grandtotal, string submit_btn, string edit_id = "0")
        {
            try
            {
                if (submit_btn == "Save" && isUserAuthenticated())
                {
                    if (obj.insert_cost_sheet("builder", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyregpercent, stampdutyreg, gst, gstpercent, otheramt, grandtotal) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }
                else if (submit_btn == "Update" && isUserAuthenticated())
                {
                    int id = Int32.Parse(edit_id);
                    if (obj.insert_cost_sheet("builder", site, type, area, rr_rate, basic_rate, basic_cost, legal_charge, devcharge, mseb, stampdutyregpercent, stampdutyreg, gst, gstpercent, otheramt, grandtotal, "edit", id) == 1)
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

        public ActionResult change_password(string page, string uname, string newpass, string confirmnewpass, string oldpass="")
        {
            try
            {
                if (isUserAuthenticated())
                {
                    if (page == "ChangePassword")
                    {
                        if (obj.Login(uname, oldpass) == "false")
                        {
                            TempData["AlertMessage"] = "Invalid username or Password.";
                            return RedirectToAction(page, "Home");
                        }
                    }
                    if(newpass != confirmnewpass)
                    {
                        TempData["AlertMessage"] = "New password and confirm password did not match.";
                        return RedirectToAction(page, "Home");
                    }
                    if (oldpass == newpass)
                    {
                        TempData["AlertMessage"] = "New password can not be same as old password.";
                        return RedirectToAction(page, "Home");
                    }
                    if (obj.update_password(uname, newpass) == 1)
                    {
                        TempData["AlertMessage"] = "All the details saved successfully.";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "There is some issue while saving the details please do it again.";
                    }
                }                
                return RedirectToAction(page, "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "There is exception while saving the details please do it again.";
                System.Web.HttpContext.Current.Response.Write("<script>alert('There is some issue while saving the details, please try again, Thanks.')</script>");
                return RedirectToAction(page, "Home");
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
            sites[0].Insert(0, "");
            sites[1].Insert(0, "");
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
         * Drop Down list for applicant name on page load
         */
        [HttpGet]
        public ActionResult get_finance(string appid, string siteid, string flatid)
        {
            List<string>[] sites = new List<string>[2];
            sites = obj.finance_name(appid, siteid, flatid);
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
            sites[0].Insert(0, "");
            sites[1].Insert(0, "");
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
            List<string>[] wing = new List<string>[2];
            wing = obj.wing_show_name(site_id);
            var result = new
            {
                id = wing[0],
                name = wing[1]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /** 
         * Drop Down list for wing name on page load
         */
        [HttpPost]
        public ActionResult get_booking_details(string applicant_id)
        {
            List<string>[] bdetails = new List<string>[7];
            bdetails = obj.booking_details(applicant_id);
            var result = new
            {
                applicant_id = bdetails[0],
                sites_id = bdetails[1],
                flats_id = bdetails[2],
                appl_name = bdetails[3],
                site = bdetails[4],
                wing = bdetails[5],
                flat = bdetails[6]
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /** 
         * Drop Down list for flat no on page load
         */
        [HttpPost]
        public ActionResult get_flat_no(string site_id)
        {
            List<string>[] flat = new List<string>[3];
            flat = obj.flat_show_no(site_id);
            var result = new
            {
                id = flat[0],
                name = flat[1],
                wing = flat[2]
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

        public static int get_alarms()
        {
            int cnt = 0;
            cnt = obj.get_alarm_count();            
            return cnt;
        }

        [Authorize]
        public ActionResult DailyReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult DisplayDailyReport(DateTime enqStartDate, DateTime enqEndDate,
                                              string enqName, string enqSite, string enqRequirement,
                                              string enqVisit, Double enqBudget, Double enqDown,
                                              string enqCurrentStatus, string enqMob)
        {
            
            List<DailyFollowup> list = new List<DailyFollowup>();
            List<DailyVM> list1 = new List<DailyVM>();
            List<Enquiry> lstDailyEnquiry = new List<Enquiry>();
            //list = obj.Daily_enquiry_report(enqStartDate, enqEndDate, enqName, enqSite, enqRequirement,
            //                        enqVisit,enqIneterest,enqBudget,enqDown,enqMob);

            list = obj.Daily_enquiry_followup_report(
                   enqStartDate, enqEndDate, enqName, enqSite, 
                   enqRequirement, enqVisit, enqCurrentStatus,
                   enqBudget, enqDown, enqMob
                   );

            foreach (DailyFollowup DailyFollowupobj in list)
            {
                list1 = obj.Daily_enquiry_sitevisit_report();

                Enquiry _Enquiry = new Enquiry();
                _Enquiry._DailyFollowup = DailyFollowupobj;
                _Enquiry._DailyVM = list1;

                lstDailyEnquiry.Add(_Enquiry);
            }
            
            return View(lstDailyEnquiry); 
        }
        
        [Authorize]
        public ActionResult DailyEnquiryPDF(
                                            DateTime enqStartDate, DateTime enqEndDate,
                                            string enqName, string enqSite, string enqRequirement,
                                            string enqVisit, Double enqBudget, Double enqDown,
                                            string enqCurrentStatus, string enqMob
                                            )
        {
            List<DailyFollowup> list = new List<DailyFollowup>();
            List<DailyVM> list1 = new List<DailyVM>();
            List<Enquiry> lstDailyEnquiry = new List<Enquiry>();
            //list = obj.Daily_enquiry_report(enqStartDate, enqEndDate, enqName, enqSite, enqRequirement,
            //                        enqVisit,enqIneterest,enqBudget,enqDown,enqMob);

            list = obj.Daily_enquiry_followup_report(
                   enqStartDate, enqEndDate, enqName, enqSite,
                   enqRequirement, enqVisit, enqCurrentStatus,
                   enqBudget, enqDown, enqMob
                   );

            foreach (DailyFollowup DailyFollowupobj in list)
            {
                list1 = obj.Daily_enquiry_sitevisit_report();

                Enquiry _Enquiry = new Enquiry();
                _Enquiry._DailyFollowup = DailyFollowupobj;
                _Enquiry._DailyVM = list1;

                lstDailyEnquiry.Add(_Enquiry);
            }

            return View(lstDailyEnquiry);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Generate_pdf(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 0f, 0f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
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
        public ActionResult FinanceReportPDF(DateTime startDate, DateTime endDate,
                                             string financeName, string siteName, 
                                             string filesta)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[62];
            list = obj.finance_report(startDate, endDate, financeName, siteName,
                                        filesta);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }

        [Authorize]
        public ActionResult SiteReport()
        {
            
            
            return View();
        }

        [Authorize]
        public ActionResult SiteReportPDF(DateTime startDate, DateTime endDate,
                                       string siteName)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[20];
            list = obj.Sitewise_bookings(
                   startDate, endDate, siteName
                   );

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }

        [Authorize]
        public ActionResult CustomerReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult CustomerReportPDF(string applid)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[19];
            list = obj.customer_report(applid);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }

        [Authorize]
        public ActionResult FileStatusReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult FileStatusReportPDF(string financeName)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[71];
            list = obj.fileprocess_report(financeName);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }

        [Authorize]
        public ActionResult ExecutiveReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult ExecutiveReportPDF(DateTime startDate, DateTime endDate,
                                       string siteName, string ename)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[17];
            list = obj.execu_fran_report(
                   startDate, endDate, siteName, ename);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }

        [Authorize]
        public ActionResult ProfitLossReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult ProfitLossReportPDF(DateTime startDate, DateTime endDate,
                                       string siteName)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[17];
            list = obj.profit_loss_report(
                   startDate, endDate, siteName);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }

        [Authorize]
        public ActionResult MasterReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult MasterReportPDF(int bapplicant, int bsite, string bwing, int bflats)
        {
            ViewBag.total = 0;
            ViewBag.total1 = 0;
            ViewBag.total2 = 0;
            ViewBag.total3 = 0;
            ViewBag.total4 = 0;
            ViewBag.total5 = 0;
            ViewBag.total6 = 0;
            ViewBag.total7 = 0;

            List<string>[] list = new List<string>[75];
            List<string>[] list1 = new List<string>[75];
            List<string>[] list2 = new List<string>[75];
            List<string>[] list3 = new List<string>[75];
            List<string>[] list4 = new List<string>[75];
            List<string>[] list5 = new List<string>[6];
            List<string>[] list6 = new List<string>[71];
            List<string>[] list7 = new List<string>[23];

            list = obj.master_report(
                   bapplicant, bsite, bflats);
            ViewBag.list = list;
            ViewBag.total = list[0].Count();

            list1 = obj.master_report1(
                   bapplicant, bsite, bflats);
            ViewBag.list1 = list1;
            ViewBag.total1 = list1[0].Count();

            list2 = obj.master_report2(
                   bapplicant, bsite, bflats);
            ViewBag.list2 = list2;
            ViewBag.total2 = list2[0].Count();

            list3 = obj.master_report3(
                   bapplicant, bsite, bflats);
            ViewBag.list3 = list3;
            ViewBag.total3 = list3[0].Count();

            list4 = obj.master_report4(
                      bapplicant, bsite, bflats);
            ViewBag.list4 = list4;
            ViewBag.total4 = list4[0].Count();

            list5 = obj.co_applicant_details(
                      bapplicant);
            ViewBag.list5 = list5;
            ViewBag.total5 = list5[0].Count();

            
            list6 = obj.master_report5(
                    bapplicant, bsite, bflats);

            ViewBag.list6 = list6;
            ViewBag.total6 = list6[0].Count();

            list7 = obj.master_report6(
                    bapplicant, bsite, bflats);

            ViewBag.list7 = list7;
            ViewBag.total7 = list7[0].Count();

            return View();
        }

        [Authorize]
        public ActionResult ExecutiveAudits()
        {
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

        public ActionResult PaymentCommitReportPDF(DateTime startDate, DateTime endDate)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[48];
            list = obj.paycommit_report(startDate, endDate);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }

        public ActionResult CostSheetReport()
        {
            return View();
        }

        public ActionResult CostSheetReportPDF(string costSheet, string siteName)
        {
            ViewBag.total = 0;
            List<string>[] list = new List<string>[26];
            for (int i = 0; i < 26; i++)
            {
                list[i] = new List<string>();
            }

            list = obj.costsheet_report(costSheet, siteName);

            ViewBag.list = list;
            ViewBag.total = list[0].Count();
            return View();
        }
    }
}