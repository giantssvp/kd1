using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace kd.Models
{
    public class db_connect
    {
        private MySqlConnection connection;        
        public List<string>[] list_show = new List<string>[70];

        public List<string>[] list_alarms_show = new List<string>[12];
        public List<string>[] list_executive_show_name = new List<string>[2];
        public List<string>[] list_franchies_show_name = new List<string>[2];
        public List<string>[] list_customer_show_name = new List<string>[2];
        public List<string>[] list_customer_booking_show = new List<string>[2];
        public List<string>[] list_daily_customer_name_show = new List<string>[2];
        public List<string>[] list_wing_name_show = new List<string>[2];
        public List<string>[] list_booking_details_show = new List<string>[7];        
        public List<string>[] list_flat_no_show = new List<string>[3];
        public List<string>[] list_sitewise_booking_show = new List<string>[21];
        public List<DailyFollowup> list_enquiry_followup_show = new List<DailyFollowup>();

        private bool OpenConnection()
        {
            string connetionString = null;
            connetionString = "server=182.50.133.77;database=kolhedeveloper;uid=kolheadmin;pwd=Kolhe@123;Allow User Variables=True;SslMode=none";
            connection = new MySqlConnection(connetionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private void clear_list_show()
        {
            for (int i = 0; i < 70; i++)
            {
                list_show[i] = new List<string>();
            }
        }

        private void get_list_show(MySqlDataReader dataReader)
        {
            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    list_show[i].Add(dataReader[i] + "");
                }
            }
        }        

public int insert_enquiry(string enqname, string enqaddress, string enqmob, string enqaltmob, string enqemail,
            string enqrequirement, string enqoccu, string enqincome, string enqbudget, string enqdown, string enqcurstatus,
            string enqvisit, string enqsource, string enqsourcedetails, string enqsanctiontype, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE daily_enquiry SET " +
                        " Customer_Name = @name," +
                        " Address = @addr," +
                        " Mobile_No = @mob," +
                        " Second_Mobile_No = @altmob," +
                        " Email_ID = @email," +
                        " Requirement = @req," +
                        " Occupation = @occu," +
                        " Income = @income," +
                        " Budget = @budget," +
                        " Down_Payment = @down_pay," +
                        " Visit = @visit," +
                        " Current_Status = @cur_status," +
                        " Source = @source," +
                        " Source_Details = @source_detail," +
                        " Sanction_Type = @sanction_type where id=@id";
                }
                else
                {
                    query = "INSERT INTO daily_enquiry (Customer_Name, Address, Mobile_No, Second_Mobile_No, Email_ID, " +
                        "Requirement, Occupation, Income, Budget, Down_Payment, Visit, Current_Status, Source, Source_Details, Sanction_Type, Enquiry_Date) " +
                        "VALUES(@name, @addr, @mob, @altmob, @email, @req, @occu, @income, @budget, @down_pay, @visit, " +
                        "@cur_status, @source, @source_detail, @sanction_type, NOW())";
                }
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", enqname);
                    cmd.Parameters.AddWithValue("@addr", enqaddress);
                    cmd.Parameters.AddWithValue("@mob", enqmob);
                    cmd.Parameters.AddWithValue("@altmob", enqaltmob);
                    cmd.Parameters.AddWithValue("@email", enqemail);
                    cmd.Parameters.AddWithValue("@req", enqrequirement);
                    cmd.Parameters.AddWithValue("@occu", enqoccu);
                    cmd.Parameters.AddWithValue("@income", enqincome);
                    cmd.Parameters.AddWithValue("@budget", enqbudget);
                    cmd.Parameters.AddWithValue("@down_pay", enqdown);
                    cmd.Parameters.AddWithValue("@visit", enqvisit);
                    cmd.Parameters.AddWithValue("@cur_status", enqcurstatus);
                    cmd.Parameters.AddWithValue("@source", enqsource);
                    cmd.Parameters.AddWithValue("@source_detail", enqsourcedetails);
                    cmd.Parameters.AddWithValue("@sanction_type", enqsanctiontype);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_de_sitevisit(string enqname, string enqsitename, string enqwing, string enqflatno,
            string enqexename1, string enqexename2, string enqexename3, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE daily_sitevisit SET " +
                        " Daily_Customer_ID = @name," +
                        " Site_ID = @site," +
                        " Wing = @wing," +
                        " Flat = @flat," +
                        " Executive1_ID = @exe1," +
                        " Executive2_ID = @exe2," +
                        " Executive3_ID = @exe3 where id=@id";
                }
                else
                {
                    query = "INSERT INTO daily_sitevisit (Daily_Customer_ID, Site_ID, Wing, Flat, Executive1_ID, " +
                        "Executive2_ID, Executive3_ID, Date) " +
                        "VALUES(@name, @site, @wing, @flat, @exe1, @exe2, @exe3, NOW())";
                }
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", enqname);
                    cmd.Parameters.AddWithValue("@site", enqsitename);
                    cmd.Parameters.AddWithValue("@wing", enqwing);
                    cmd.Parameters.AddWithValue("@flat", enqflatno);
                    cmd.Parameters.AddWithValue("@exe1", enqexename1);
                    cmd.Parameters.AddWithValue("@exe2", enqexename2);
                    cmd.Parameters.AddWithValue("@exe3", enqexename3);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_de_followup(string enqname, string enqfollow, string enqnextfollow, string enqfollowdetails,
            string enqexename1, string enqexename2, string enqexename3, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE daily_followup SET " +
                        " Daily_Customer_ID = @name," +
                        " Folloup_Date = @follow," +
                        " Next_Folloup_Date = @nextfollow," +
                        " Folloup_Details = @followdetail," +
                        " Executive1_ID = @exe1," +
                        " Executive2_ID = @exe2," +
                        " Executive3_ID = @exe3 where id=@id";
                }
                else
                {
                    query = "INSERT INTO daily_followup (Daily_Customer_ID, Folloup_Date, Next_Folloup_Date, Folloup_Details, " +
                        "Executive1_ID, Executive2_ID, Executive3_ID, Date) " +
                        "VALUES(@name, @follow, @nextfollow, @followdetail, @exe1, @exe2, @exe3, NOW())";
                }
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", enqname);
                    cmd.Parameters.AddWithValue("@follow", enqfollow);
                    cmd.Parameters.AddWithValue("@nextfollow", enqnextfollow);
                    cmd.Parameters.AddWithValue("@followdetail", enqfollowdetails);
                    cmd.Parameters.AddWithValue("@exe1", enqexename1);
                    cmd.Parameters.AddWithValue("@exe2", enqexename2);
                    cmd.Parameters.AddWithValue("@exe3", enqexename3);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_sites(string sitename, string sitetype, string siteaddress, string sitephone, string siteemail, string sitestatus, string sitesanctiontype, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE sites SET " +
                        " Site_Name = @name," +
                        " Site_Type = @site_type," +
                        " Email_Id = @email," +
                        " Phone = @mob," +
                        " Address = @addr," +
                        " Status = @status," +
                        " Sanction_Type = @sact_type where id=@id";
                }
                else
                {
                    query = "INSERT INTO sites (Site_Name, Site_Type, Email_Id, Phone, Address, Date, Status, Sanction_Type) " +
                    "VALUES(@name, @site_type, @email, @mob, @addr, NOW(), @status, @sact_type)";
                }
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", sitename);
                    cmd.Parameters.AddWithValue("@site_type", sitetype);
                    cmd.Parameters.AddWithValue("@email", siteemail);
                    cmd.Parameters.AddWithValue("@mob", sitephone);
                    cmd.Parameters.AddWithValue("@addr", siteaddress);
                    cmd.Parameters.AddWithValue("@status", sitestatus);
                    cmd.Parameters.AddWithValue("@sact_type", sitesanctiontype);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_flats(string flatsitename, string flatwing, string flatfloor, string flatno, string flattype, string flatarea, string flatstatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE flats SET " +
                        "Flat_No = @flatno," +
                        " Floor = @floor," +
                        " Area = @area," +
                        " Flat_Type = @flat_type," +
                        " Wing = @wing," +
                        " Status = @status," +
                        " Site_Id = @siteid where id=@id";
                }
                else
                {
                    query = "INSERT INTO flats (Flat_No, Floor, Area, Flat_Type, Wing, Date, Status, Site_Id) " +
                    "VALUES(@flatno, @floor, @area, @flat_type, @wing, NOW(), @status, @siteid)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@flatno", flatno);
                    cmd.Parameters.AddWithValue("@floor", flatfloor);
                    cmd.Parameters.AddWithValue("@area", flatarea);
                    cmd.Parameters.AddWithValue("@flat_type", flattype);
                    cmd.Parameters.AddWithValue("@wing", flatwing);
                    cmd.Parameters.AddWithValue("@status", flatstatus);
                    cmd.Parameters.AddWithValue("@siteid", Int32.Parse(flatsitename));

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_plots(string plotsitename, string plotno, string plotarea, string plotstatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE plot SET " +
                        "Site_ID = @site," +
                        " Plot_NO = @plotno," +
                        " Plot_Area = @area," +
                        " Plot_Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO plot (Site_ID, Plot_NO, Plot_Area, Plot_Status, Wing, Date) " +
                    "VALUES(@site, @plotno, @area, @status, @wing, NOW())";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@site", plotsitename);
                    cmd.Parameters.AddWithValue("@plotno", plotno);
                    cmd.Parameters.AddWithValue("@area", plotarea);
                    cmd.Parameters.AddWithValue("@status", plotstatus);
                    cmd.Parameters.AddWithValue("@wing", "None");

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_executive(string exename, string execode, string exeemail, string exemob, string exeadd, string exejoin, string exebirth, string exestatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE executive SET " +
                        "Executive_Name = @name," +
                        " Executive_Code = @code," +
                        " Email_Id = @email," +
                        " Phone = @phone," +
                        " Address = @addr," +
                        " Birth_Date = @birth," +
                        " Joining_Date = @join," +
                        " Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO executive (Executive_Name, Executive_Code, Email_Id, Phone, Address, Birth_Date, Joining_Date, Date, Status) " +
                    "VALUES(@name, @code, @email, @phone, @addr, @birth, @join, NOW(), @status)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", exename);
                    cmd.Parameters.AddWithValue("@code", execode);
                    cmd.Parameters.AddWithValue("@email", exeemail);
                    cmd.Parameters.AddWithValue("@phone", exemob);
                    cmd.Parameters.AddWithValue("@addr", exeadd);
                    cmd.Parameters.AddWithValue("@birth", exebirth);
                    cmd.Parameters.AddWithValue("@join", exejoin);
                    cmd.Parameters.AddWithValue("@status", exestatus);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_franchies(string francname, string franccode, string francemail, string francmob, string francadd, string francjoin, string francstatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE franchies SET " +
                        "Francies_Name = @name," +
                        " Email_Id = @email," +
                        " Phone = @phone," +
                        " Address = @addr," +
                        " Joining_Date = @join," +
                        " Francies_Code = @code," +
                        " Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO franchies (Francies_Name, Email_Id, Phone, Address, Date, Status, Joining_Date, Francies_Code) " +
                    "VALUES(@name, @email, @phone, @addr, NOW(), @status, @join, @code)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", francname);
                    cmd.Parameters.AddWithValue("@code", franccode);
                    cmd.Parameters.AddWithValue("@email", francemail);
                    cmd.Parameters.AddWithValue("@phone", francmob);
                    cmd.Parameters.AddWithValue("@addr", francadd);
                    cmd.Parameters.AddWithValue("@join", francjoin);
                    cmd.Parameters.AddWithValue("@status", francstatus);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_applicant(string applname, string applemail, string applmob, string appladdr, string applpan, string applaadhar,
            string apploccu, string applbirth, string applage, string applstatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE applicant SET " +
                        "Applicant_Name = @name," +
                        " Applicant_Email_Id = @email," +
                        " Applicant_Phone = @phone," +
                        " Applicant_Address = @addr," +
                        " Applicant_Pan_No = @pan," +
                        " Applicant_Adhar_No = @aadhar," +
                        " Applicant_Occupation = @occu," +
                        " Applicant_DOB = @birth," +
                        " Applicant_Age = @age," +
                        " Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO applicant (Applicant_Name, Applicant_Email_Id, Applicant_Phone, Applicant_Address, Applicant_Pan_No, " +
                    "Applicant_Adhar_No, Applicant_Occupation, Applicant_DOB, Applicant_Age, Date, Status) " +
                    "VALUES(@name, @email, @phone, @addr, @pan, @aadhar, @occu, @birth, @age, NOW(), @status)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", applname);
                    cmd.Parameters.AddWithValue("@email", applemail);
                    cmd.Parameters.AddWithValue("@phone", applmob);
                    cmd.Parameters.AddWithValue("@addr", appladdr);
                    cmd.Parameters.AddWithValue("@pan", applpan);
                    cmd.Parameters.AddWithValue("@aadhar", applaadhar);
                    cmd.Parameters.AddWithValue("@occu", apploccu);
                    cmd.Parameters.AddWithValue("@birth", applbirth);
                    cmd.Parameters.AddWithValue("@age", applage);
                    cmd.Parameters.AddWithValue("@status", applstatus);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_co_applicant(string coapplname, string coapplpan, string coapplaadhar, string coapploccu,
            string coapplbirth, string applid, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE co_applicant SET " +
                        " Co_Applicant_Name = @cname," +
                        " Co_Applicant_Pan_No = @cpan," +
                        " Co_Applicant_Adhar_No = @caadhar," +
                        " Co_Applicant_Occupation = @coccu," +
                        " Co_Applicant_DOB = @cbirth," +
                        " Applicant_ID = @appid where id=@id";
                }
                else
                {
                    query = "INSERT INTO co_applicant (Co_Applicant_Name, Co_Applicant_Pan_No, " +
                    "Co_Applicant_Adhar_No, Co_Applicant_Occupation, Co_Applicant_DOB, Date, Applicant_ID) " +
                    "VALUES(@cname, @cpan, @caadhar, @coccu, @cbirth, NOW(), @appid)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@cname", coapplname);
                    cmd.Parameters.AddWithValue("@cpan", coapplpan);
                    cmd.Parameters.AddWithValue("@caadhar", coapplaadhar);
                    cmd.Parameters.AddWithValue("@coccu", coapploccu);
                    cmd.Parameters.AddWithValue("@cbirth", coapplbirth);
                    cmd.Parameters.AddWithValue("@appid", applid);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_exe_franc_audit(string ename, string fname, string bno, string incentive, string share, string paidamt, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE execu_fran_audit SET " +
                        "Booking_ID = @bno," +
                        " Executive_ID = @ename," +
                        " Franchies_ID = @fname," +
                        " Total_Incentive = @incentive," +
                        " Total_Share = @share," +
                        " Total_Paid = @paidamt where id=@id";
                }
                else
                {
                    query = "INSERT INTO execu_fran_audit (Booking_ID, Executive_ID, Franchies_ID, Total_Incentive, Total_Share, Total_Paid, Date) " +
                    "VALUES(@bno, @ename, @fname, @incentive, @share, @paidamt, NOW())";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@bno", bno);
                    cmd.Parameters.AddWithValue("@ename", ename);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@incentive", incentive);
                    cmd.Parameters.AddWithValue("@share", share);
                    cmd.Parameters.AddWithValue("@paidamt", paidamt);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_booking(string bno, string breferred, string bapplicant, string btamount, string bramount, string bblder,
            string bsite, string bwing, string bflats, string bcharges, string bparking, string bcancel,
            string bfollowup, string bstatus, string bremark, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE bookings SET " +
                        "Booking_No = @bno," +
                        " Referenceby = @bref," +
                        " Applicant_Id = @bappl," +
                        " Total_Flat_Amount = @btamt," +
                        " Received_Amount = @bramt," +
                        " Total_Builder_Received = @bbldr," +
                        " Site_Id = @bsite," +
                        " Wing = @bwing," +
                        " Flat = @bflat," +
                        " Internal_Charges = @bchrg," +
                        " Reserved_Parking = @bpark," +
                        " Flat_Cancled_By = @bcan," +
                        " Follow_Up_Date = @bflp," +
                        " Status = @bsts," +
                        " Remark = @bremark where id=@id";
                }
                else
                {
                    query = "INSERT INTO bookings (Booking_No, Referenceby, Applicant_Id, Total_Flat_Amount, " +
                        "Received_Amount, Total_Builder_Received, Site_Id, Wing, Flat, Internal_Charges, Reserved_Parking, " +
                        "Flat_Cancled_By, Follow_Up_Date, Remark, Status, Date) " +
                    "VALUES(@bno, @bref, @bappl, @btamt, @bramt, @bbldr, @bsite, @bwing, @bflat, @bchrg, @bpark, @bcan," +
                    " @bflp, @bremark, @bsts, NOW())";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@bno", bno);
                    cmd.Parameters.AddWithValue("@bref", breferred);
                    cmd.Parameters.AddWithValue("@bappl", bapplicant);
                    cmd.Parameters.AddWithValue("@btamt", btamount);
                    cmd.Parameters.AddWithValue("@bramt", bramount);
                    cmd.Parameters.AddWithValue("@bbldr", bblder);
                    cmd.Parameters.AddWithValue("@bsite", bsite);
                    cmd.Parameters.AddWithValue("@bwing", bwing);
                    cmd.Parameters.AddWithValue("@bflat", bflats);
                    cmd.Parameters.AddWithValue("@bchrg", bcharges);
                    cmd.Parameters.AddWithValue("@bpark", bparking);
                    cmd.Parameters.AddWithValue("@bcan", bcancel);
                    cmd.Parameters.AddWithValue("@bflp", bfollowup);
                    cmd.Parameters.AddWithValue("@bsts", bstatus);
                    cmd.Parameters.AddWithValue("@bremark", bremark);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_paymentcommit(string ctype, string camount, string cdate, string cremark, string bapplicant, string bsite, string bwing, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Wing='" + bwing + "' and Site_Id=" + bsite + ")";
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE payment_commitment SET " +
                        "Commitment_Type = @ctype," +
                        " Amount = @camt," +
                        " Commitment_Date = @cdate," +
                        " Remark = @crmrk," +
                        " Booking_Id = " + que + " where id=@id";
                }
                else
                {
                    query = "INSERT INTO payment_commitment (Commitment_Type, Amount, Commitment_Date, Date, Remark, Booking_Id) " +
                    "VALUES(@ctype, @camt, @cdate, NOW(), @crmrk, " + que + ")";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ctype", ctype);
                    cmd.Parameters.AddWithValue("@camt", camount);
                    cmd.Parameters.AddWithValue("@cdate", cdate);
                    cmd.Parameters.AddWithValue("@crmrk", cremark);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_paymentdetails(string pamt, string pmode, string chkid, string chkdate, string bname,
            string ptype, string bldpay, string bnkpay, string sts, string bapplicant, string bsite, string bwing, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Wing='" + bwing + "' and Site_Id=" + bsite + ")";
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE payment_details SET " +
                        "Amount = @pamt," +
                        " Payment_Mode = @pmode," +
                        " Cheque_Id = @chkid," +
                        " Cheque_Date = @chkdate," +
                        " Bank_Name = @bname," +
                        " Payment_Type = @ptype," +
                        " Builder_Pay = @bldpay," +
                        " Bank_Pay = @bnkpay," +
                        " Status = @sts," +
                        " Booking_Id = " + que + " where id=@id";
                }
                else
                {
                    query = "INSERT INTO payment_details (Amount, Date, Payment_Mode, Cheque_Id, Cheque_Date, Bank_Name, " +
                    "Payment_Type, Builder_Pay, Bank_Pay, Status, Booking_Id) " +
                    "VALUES(@pamt, NOW(), @pmode, @chkid, @chkdate, @bname, @ptype, @bldpay, @bnkpay, @sts, "+ que + ")";
                }

                if (this.OpenConnection() == true)
                {                    
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@pamt", pamt);
                    cmd.Parameters.AddWithValue("@pmode", pmode);
                    cmd.Parameters.AddWithValue("@chkid", chkid);
                    cmd.Parameters.AddWithValue("@chkdate", chkdate);
                    cmd.Parameters.AddWithValue("@bname", bname);
                    cmd.Parameters.AddWithValue("@ptype", ptype);
                    cmd.Parameters.AddWithValue("@bldpay", bldpay);
                    cmd.Parameters.AddWithValue("@bnkpay", bnkpay);
                    cmd.Parameters.AddWithValue("@sts", sts);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_agreement(string ano, string adate, string anotary, string aamount, string aadjustment, string aextra, string gst, string astatus, string bapplicant, string bsite, string bwing, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Wing='" + bwing + "' and Site_Id=" + bsite + ")";
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE aggrement SET " +
                        "Aggrement_Amount = @aamount," +
                        " Aggrement_Date = @adate," +
                        " Aggrement_No = @ano," +
                        " Status = @astatus," +
                        " Notary_Amount = @notary," +
                        " Adjustment_Amount = @adjust," +
                        " Extra_Amount = @extra," +
                        " GST_Amount = @gst, " +
                        " Booking_Id = " + que + " where id=@id";
                }
                else
                {
                    query = "INSERT INTO aggrement (Aggrement_Amount, Aggrement_Date, Aggrement_No, Status, Notary_Amount, Adjustment_Amount, Extra_Amount, GST_Amount, Date, Booking_Id) " +
                    "VALUES(@aamount, @adate, @ano, @astatus, @notary, @adjust, @extra, @gst, NOW(), " + que + ")";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ano", ano);
                    cmd.Parameters.AddWithValue("@aamount", aamount);
                    cmd.Parameters.AddWithValue("@astatus", astatus);
                    cmd.Parameters.AddWithValue("@adate", adate);
                    cmd.Parameters.AddWithValue("@notary", anotary);
                    cmd.Parameters.AddWithValue("@adjust", aadjustment);
                    cmd.Parameters.AddWithValue("@extra", aextra);
                    cmd.Parameters.AddWithValue("@gst", gst);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_finance(string fintype, string finname, string finexe, string finexemob, string finexeemail, string filehanddate,
            string filesta, string filesanctdate, string reqloanamt, string sanctloanamt, string disburseamt, string actloanamt, string recddamt, string remddamt, string rateofinter, string emiamt, string emimonths, string finstat, string bapplicant, string bsite, string bwing, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Wing='" + bwing + "' and Site_Id=" + bsite + ")";
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE finance_details SET " +
                        "Finance_Type = @ftype," +
                        " Finance_Name = @fname," +
                        " Finance_Executive_Name = @fexename," +
                        " Finance_Executive_Mobile = @fexemob," +
                        " Finance_Executive_Email = @fexemail," +
                        " File_Handover_Date = @fhdate," +
                        " File_Status = @fsts," +
                        " File_Sanction_Date = @fdate," +
                        " Required_Loan_Amount = @rlamt," +
                        " Sanctioned_Loan_Amount = @slamt," +
                        " Total_Disbursed_Amount = @dbrmnt," +
                        " Actual_Loan_Amount = @alamt," +
                        " Received_DD_Amount = @ramt," +
                        " Remaining_DD_Amount = @remamt," +
                        " Rate_Of_Interest = @rointr," +
                        " EMI_Amount = @emiamt," +
                        " EMI_Total_Months = @emimonths," +
                        " Status = @finstat," +
                        " Booking_Id = " + que + " where id=@id";
                }
                else
                {
                    query = "INSERT INTO finance_details (Finance_Type, Finance_Name, Finance_Executive_Name, " +
                    "Finance_Executive_Mobile, Finance_Executive_Email, File_Handover_Date, File_Status, File_Sanction_Date, " +
                    "Required_Loan_Amount, Sanctioned_Loan_Amount, Total_Disbursed_Amount, Actual_Loan_Amount, Received_DD_Amount, " +
                    "Remaining_DD_Amount, Rate_Of_Interest, EMI_Amount, EMI_Total_Months, Status, Date, Booking_Id) " +
                    "VALUES(@ftype, @fname, @fexename, @fexemob, @fexemail, @fhdate, @fsts, @fdate, @rlamt, @slamt, @dbrmnt," +
                    " @alamt, @ramt, @remamt, @rointr, @emiamt, @emimonths, @finstat, NOW(), " + que + ")";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ftype", fintype);
                    cmd.Parameters.AddWithValue("@fname", finname);
                    cmd.Parameters.AddWithValue("@fexename", finexe);
                    cmd.Parameters.AddWithValue("@fexemob", finexemob);
                    cmd.Parameters.AddWithValue("@fexemail", finexeemail);
                    cmd.Parameters.AddWithValue("@fhdate", filehanddate);
                    cmd.Parameters.AddWithValue("@fsts", filesta);
                    cmd.Parameters.AddWithValue("@fdate", filesanctdate);
                    cmd.Parameters.AddWithValue("@rlamt", reqloanamt);
                    cmd.Parameters.AddWithValue("@slamt", sanctloanamt);
                    cmd.Parameters.AddWithValue("@dbrmnt", disburseamt);
                    cmd.Parameters.AddWithValue("@alamt", actloanamt);
                    cmd.Parameters.AddWithValue("@ramt", recddamt);
                    cmd.Parameters.AddWithValue("@remamt", remddamt);
                    cmd.Parameters.AddWithValue("@rointr", rateofinter);
                    cmd.Parameters.AddWithValue("@emiamt", emiamt);
                    cmd.Parameters.AddWithValue("@emimonths", emimonths);
                    cmd.Parameters.AddWithValue("@finstat", finstat);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_filestatus(string chrg, string lfee, string chid, string chdate, string bnknm, string figst,
            string lfamt, string fid, string fstatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE file_details SET " +
                        "Service_Charge = @chrg," +
                        " Loan_Fees_Amount = @lfamt," +
                        " Cheque_Id = @chid," +
                        " Cheque_Date = @chdate," +
                        " Bank_Name = @bnknm," +
                        " Loan_fees = @lfee," +
                        " status = @fstatus," +
                        " Finance_Id = @fid where id=@id";
                }
                else
                {
                    query = "INSERT INTO file_details (Service_Charge, Loan_Fees_Amount," +
                    " Cheque_Id, Cheque_Date, Bank_Name, Loan_fees, status, Finance_Id, Date) " +
                    "VALUES(@chrg, @lfamt, @chid, @chdate, @bnknm, @lfee, @fstatus, @fid, NOW())";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@chrg", chrg);
                    cmd.Parameters.AddWithValue("@lfee", lfee);
                    cmd.Parameters.AddWithValue("@chid", chid);
                    cmd.Parameters.AddWithValue("@chdate", chdate);
                    cmd.Parameters.AddWithValue("@bnknm", bnknm);
                    cmd.Parameters.AddWithValue("@figst", figst);
                    cmd.Parameters.AddWithValue("@lfamt", lfamt);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@fstatus", fstatus);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int insert_cost_sheet(string sheet_type, string site, string type1, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyregpercent, string stampdutyreg, string gst, string gstpercent, string otheramt, string grandtotal, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE cost_sheet SET " +
                        "RR_Rate = @rr_rate," +
                        " Type = @type," +
                        " Area = @area," +
                        " Basic_Rate = @bas_rate," +
                        " Basic_Cost = @bas_cost," +
                        " Legal_Charges = @legal_charge," +
                        " MSEB_Charges = @mseb," +
                        " Development_Charges = @devcharge," +
                        " Stamp_Duty_Percent = @stamppercent," +
                        " Stamp_Duty_Registration = @stamp," +
                        " GST = @gst," +
                        " GST_Percent = @gst_percent," +                        
                        " Other_Amount = @otheramt," +
                        " Grand_Total = @grandtotal," +
                        " Cost_Sheet_Type = @sheet_type," +
                        " Site_Id = @site_id where id=@id";
                }
                else
                {
                    query = "INSERT INTO cost_sheet (RR_Rate, Type, Basic_Rate, Basic_Cost, Legal_Charges, MSEB_Charges, " +
                    "Development_Charges, Stamp_Duty_Percent, Stamp_Duty_Registration, GST, GST_Percent, Other_Amount, Grand_Total, Cost_Sheet_Type, Site_Id, Area, Date) " +
                    "VALUES(@rr_rate, @type, @bas_rate, @bas_cost, @legal_charge, @mseb, @devcharge, @stamppercent, @stamp, @gst, @gst_percent, @otheramt, @grandtotal, @sheet_type," +
                    " @site_id, @area, NOW())";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@sheet_type", sheet_type);
                    cmd.Parameters.AddWithValue("@type", type1);
                    cmd.Parameters.AddWithValue("@area", area);
                    cmd.Parameters.AddWithValue("@rr_rate", rr_rate);
                    cmd.Parameters.AddWithValue("@bas_cost", basic_cost);
                    cmd.Parameters.AddWithValue("@bas_rate", basic_rate);
                    cmd.Parameters.AddWithValue("@legal_charge", legal_charge);
                    cmd.Parameters.AddWithValue("@mseb", mseb);
                    cmd.Parameters.AddWithValue("@gst", gst);
                    cmd.Parameters.AddWithValue("@gst_percent", gstpercent);
                    cmd.Parameters.AddWithValue("@devcharge", devcharge);
                    cmd.Parameters.AddWithValue("@stamppercent", stampdutyregpercent);                    
                    cmd.Parameters.AddWithValue("@stamp", stampdutyreg);
                    cmd.Parameters.AddWithValue("@otheramt", otheramt);
                    cmd.Parameters.AddWithValue("@grandtotal", grandtotal);
                    cmd.Parameters.AddWithValue("@site_id", site);

                    if (type == "edit")
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        /* Show Queries */
        public List<string>[] enquiry_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM daily_enquiry ORDER BY ID DESC LIMIT @lim OFFSET @off";
                    //query = "SELECT * FROM daily ORDER BY ID DESC LIMIT @lim OFFSET @off";
                    //query = "daily_enquiry_sp";
                }
                else
                {
                    query = "SELECT * FROM daily_enquiry where CONCAT(Customer_Name, Requirement) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @lim OFFSET @off";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@off", offset);
                    cmd.Parameters.AddWithValue("@lim", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] sitevisit_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_daily_sitevisit ORDER BY ID DESC LIMIT @lim OFFSET @off";
                }
                else
                {
                    query = "SELECT * FROM v_daily_sitevisit where CONCAT(Wing, Flat) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @lim OFFSET @off";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@off", offset);
                    cmd.Parameters.AddWithValue("@lim", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);

                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] followup_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_daily_followup ORDER BY ID DESC LIMIT @lim OFFSET @off";
                }
                else
                {
                    query = "SELECT * FROM v_daily_followup where CONCAT(Folloup_Details, Folloup_Date, Next_Folloup_Date) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @lim OFFSET @off";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@off", offset);
                    cmd.Parameters.AddWithValue("@lim", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);

                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        /* Show Queries */
        public List<DailyVM> Daily_enquiry_sitevisit_report()
        {
            List<DailyVM> list_followup_show1 = new List<DailyVM>();

            try
            {
                ////cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@startDate", enqStartDate);
                //cmd.Parameters.AddWithValue("@endDate", enqEndDate);
                //cmd.Parameters.AddWithValue("@mobile", Convert.ToInt64(enqMob));
                //cmd.Parameters.AddWithValue("@custName", enqName);
                //cmd.Parameters.AddWithValue("@siteID", enqSite);
                //cmd.Parameters.AddWithValue("@requirement", enqRequirement);
                //cmd.Parameters.AddWithValue("@visit", enqVisit);
                //cmd.Parameters.AddWithValue("@interest", enqIneterest);
                //cmd.Parameters.AddWithValue("@budget", enqBudget);
                //cmd.Parameters.AddWithValue("@downPayment", enqDown);

                //cmd.CommandType = CommandType.StoredProcedure;
                string query = "select * from daily_sitevisit_view";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        DailyVM DailyVMObj = new DailyVM();
                        DailyVMObj.ID = (int)dataReader["ID"];
                        DailyVMObj.Customer_Name = dataReader["Customer_Name"].ToString();
                        DailyVMObj.Daily_Customer_ID = (int)dataReader["Daily_Customer_ID"];
                        DailyVMObj.Site_ID = (int)dataReader["Site_ID"];
                        if (dataReader["Wing"] != DBNull.Value)
                        {
                            DailyVMObj.Wing = dataReader["Wing"].ToString();
                        }
                        if (dataReader["Flat"] != DBNull.Value)
                        {
                            DailyVMObj.Flat = dataReader["Flat"].ToString();
                        }
                        if (dataReader["Executive1_ID"] != DBNull.Value)
                        {
                            DailyVMObj.Executive1_ID = (int)dataReader["Executive1_ID"];
                        }
                        if (dataReader["Executive2_ID"] != DBNull.Value)
                        {
                            DailyVMObj.Executive2_ID = (int)dataReader["Executive2_ID"];
                        }
                        if (dataReader["Executive3_ID"] != DBNull.Value)
                        {
                            DailyVMObj.Executive3_ID = (int)dataReader["Executive3_ID"];
                        }
                        if (dataReader["Date"] != DBNull.Value)
                        {
                            DailyVMObj.Date = Convert.ToDateTime(dataReader["Date"]);
                        }
                        DailyVMObj.Site_Name = dataReader["Site_Name"].ToString();
                        if (dataReader["Flat_No"] != DBNull.Value)
                        {
                            DailyVMObj.Flat_No = (int)dataReader["Flat_No"];
                        }
                        DailyVMObj.Executive1_Name = dataReader["Executive1_Name"].ToString();
                        DailyVMObj.Executive2_Name = dataReader["Executive2_Name"].ToString();
                        DailyVMObj.Executive3_Name = dataReader["Executive3_Name"].ToString();

                        list_followup_show1.Add(DailyVMObj);
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    
                }
               
            }
            catch (MySqlException ex)
            {
              
            }
            return list_followup_show1;
        }


        /* Show daily enquiry followup based on view2 */
        public List<DailyFollowup> Daily_enquiry_followup_report(
                                    DateTime enqStartDate, DateTime enqEndDate,
                                    string enqName, string enqSite, string enqRequirement,
                                    string enqVisit, string enqCurrentStatus, Double enqBudget,
                                    Double enqDown, string enqMob
                                   )
        {
            try
            {
                list_enquiry_followup_show = new List<DailyFollowup>();

                DateTime todayDate = DateTime.Now;
                DateTime defaulDate = Convert.ToDateTime("1967-01-01");
                if (enqStartDate == defaulDate) {
                    enqStartDate = todayDate;
                }
                if (enqEndDate == defaulDate)
                {
                    enqEndDate = todayDate;
                }
                
                string query = "select * from daily_enquiry_followup_view2 where " +
                               "( Enquiry_Date between '" + enqStartDate + "' and '" + enqEndDate + "')";

                if (enqName != null && enqName != "")
                {
                    query = query + " and (Customer_Name = '" + enqName + "')";
                }

                if (enqSite != null && enqSite != "" ) {
                    //query = query + " and (Site_ID = '" + enqSite + "')";
                }

                if (enqRequirement != null && enqRequirement != "") {
                    query = query + " and (Requirement LIKE '%" + enqRequirement + "%')";
                }

                if (enqVisit != null && enqVisit != "") {
                    query = query + " and (Visit = '" + enqVisit + "')";
                }

                if (enqBudget > 0.0) {
                    query = query + " and (Budget = '" + enqBudget + "')";
                }
                if (enqDown > 0.0) {
                    query = query + " and (Down_Payment = '" + enqDown + "')";
                }
                if (enqCurrentStatus != null && enqCurrentStatus != "") {
                    query = query + " and (Current_Status = '" + enqCurrentStatus + "')";
                }
                if (enqMob != null && enqMob != "") {
                    query = query + " and (Mobile_No = '" + enqMob + "')";
                }

                if (this.OpenConnection() == true)

                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        DailyFollowup DailyFollowupobj = new DailyFollowup();
                        DailyFollowupobj.ID = (int)dataReader["ID"];
                        DailyFollowupobj.Customer_Name  = dataReader["Customer_Name"].ToString();
                        DailyFollowupobj.Address  = dataReader["Address"].ToString();
                        DailyFollowupobj.Mobile_No = dataReader["Mobile_No"].ToString();
                        DailyFollowupobj.Second_Mobile_No = dataReader["Second_Mobile_No"].ToString();
                        DailyFollowupobj.Email_ID = dataReader["Email_ID"].ToString();
                        DailyFollowupobj.Requirement = dataReader["Requirement"].ToString();
                        DailyFollowupobj.Occupation = dataReader["Occupation"].ToString();
                        DailyFollowupobj.Income = (int)dataReader["Income"];
                        DailyFollowupobj.Budget = (double)dataReader["Budget"];
                        DailyFollowupobj.Down_Payment = (double)dataReader["Down_Payment"];
                        DailyFollowupobj.Visit = dataReader["Visit"].ToString();
                        DailyFollowupobj.Current_Status = dataReader["Current_Status"].ToString();
                        DailyFollowupobj.Source = dataReader["Source"].ToString();
                        DailyFollowupobj.Source_Details = dataReader["Source_Details"].ToString();

                        if (dataReader["Enquiry_Date"] != DBNull.Value)
                        {
                            DailyFollowupobj.Enquiry_Date = Convert.ToDateTime(dataReader["Enquiry_Date"]);
                        }
                        if (dataReader["folloup_Date"] != DBNull.Value)
                        {
                            DailyFollowupobj.folloup_Date = Convert.ToDateTime(dataReader["folloup_Date"]);
                        }
                        if (dataReader["Next_folloup_Date"] != DBNull.Value)
                        {
                            DailyFollowupobj.Next_folloup_Date = Convert.ToDateTime(dataReader["Next_folloup_Date"]);
                        }
                        DailyFollowupobj.folloup_Details = dataReader["folloup_Details"].ToString();
                        if (dataReader["Executive1_ID"] != DBNull.Value)
                        {
                            DailyFollowupobj.Executive1_ID = (int)dataReader["Executive1_ID"];
                        }
                        if (dataReader["Executive2_ID"] != DBNull.Value)
                        {
                            DailyFollowupobj.Executive2_ID = (int)dataReader["Executive2_ID"];
                        }
                        if (dataReader["Executive3_ID"] != DBNull.Value)
                        {
                            DailyFollowupobj.Executive3_ID = (int)dataReader["Executive3_ID"];
                        }

                        list_enquiry_followup_show.Add(DailyFollowupobj);
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    
                }
                
            }
            catch (MySqlException ex)
            {  
            }
            return list_enquiry_followup_show;
        }

        /* Show sitewise bookingd of flats and plot */
        public List<string>[] Sitewise_bookings(
                                    DateTime startDate, DateTime endDate,
                                    string siteName, string siteType)
        {
            try
            {
                list_enquiry_followup_show = new List<DailyFollowup>();
                string query = ""; 
                DateTime todayDate = DateTime.Now;
                DateTime defaulDate = Convert.ToDateTime("1967-01-01");
                if (startDate == defaulDate)
                {
                    startDate = todayDate;
                }
                if (endDate == defaulDate)
                {
                    endDate = todayDate;
                }
                if (siteType == "Flat")
                {
                     query = "select * from sitewise_booking_flats where " +
                               "( Booking_Date between '" + startDate + "' and '" + endDate + "')"
                               + " and Site_Id =  " + siteName;

                }
                else {
                     query = "select * from sitewise_booking_plot where " +
                               "( Booking_Date between '" + startDate + "' and '" + endDate + "')"
                               + " and Site_Id = " + siteName;
                }
                for (int i = 0; i < 21; i++)
                {
                    list_sitewise_booking_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 21; i++)
                        {
                            list_sitewise_booking_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_sitewise_booking_show;
                }
                else
                {
                    return list_sitewise_booking_show;
                }

            }
            catch (MySqlException ex)
            {
                return list_sitewise_booking_show;
            }
            
        }

        public List<string>[] cost_sheet_show(string sheet_type, int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_cost_sheet where Cost_Sheet_Type = @sheet_type ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_cost_sheet where Cost_Sheet_Type = @sheet_type and CONCAT(Basic_Rate, Type) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@sheet_type", sheet_type);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] customer_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM applicant ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM applicant where CONCAT(Applicant_Name, Applicant_Pan_No, Applicant_Adhar_No) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] customer_sec_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_co_applicant ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_co_applicant where CONCAT(Co_Applicant_Name, Co_Applicant_Pan_No, Co_Applicant_Adhar_No) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        /* Get the customer name */
        public List<string>[] customer_show_name()
        {
            try
            {
                string query = "SELECT ID, Applicant_Name FROM applicant ORDER BY ID DESC";

                list_customer_show_name[0] = new List<string>();
                list_customer_show_name[1] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_customer_show_name[0].Add(dataReader["ID"] + "");
                        list_customer_show_name[1].Add(dataReader["Applicant_Name"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_customer_show_name;
                }
                else
                {
                    return list_customer_show_name;
                }
            }
            catch (MySqlException ex)
            {
                return list_customer_show_name;
            }
        }

        public List<string>[] finance_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_finance_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_finance_details where CONCAT(Finance_Name, Finance_Executive_Name) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] booking_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_bookings ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_bookings where CONCAT(Booking_No, Referenceby) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] executive_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM executive ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM executive where CONCAT(Executive_Name, Executive_Code) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        /* Get the executive name */
        public List<string>[] executive_show_name()
        {
            try
            {
                string query = "SELECT ID,Executive_Name FROM executive ORDER BY ID DESC";

                list_executive_show_name[0] = new List<string>();
                list_executive_show_name[1] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_executive_show_name[0].Add(dataReader["ID"] + "");
                        list_executive_show_name[1].Add(dataReader["Executive_Name"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_executive_show_name;
                }
                else
                {
                    return list_executive_show_name;
                }
            }
            catch (MySqlException ex)
            {
                return list_executive_show_name;
            }
        }

        public List<string>[] file_status_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_file_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_file_details where CONCAT(Cheque_Id, Bank_Name) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] paycommit_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_payment_commitment ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_payment_commitment where CONCAT(Commitment_Type, Amount) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] paydetails_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_payment_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_payment_details where CONCAT(Cheque_Id, Payment_Type) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] agreement_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_aggrement ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_aggrement where CONCAT(Aggrement_No, Aggrement_Amount) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] franchies_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM franchies ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM franchies where CONCAT(Francies_Name, Address) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        /* Get Franchies name */
        public List<string>[] franchies_show_name()
        {
            try
            {
                string query = "SELECT ID,Francies_Name FROM franchies ORDER BY ID DESC";

                list_franchies_show_name[0] = new List<string>();
                list_franchies_show_name[1] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_franchies_show_name[0].Add(dataReader["ID"] + "");
                        list_franchies_show_name[1].Add(dataReader["Francies_Name"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_franchies_show_name;
                }
                else
                {
                    return list_franchies_show_name;
                }
            }
            catch (MySqlException ex)
            {
                return list_franchies_show_name;
            }
        }

        public List<string>[] sites_show(string site_type = "All", int offset = 0, int limit = 0, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (site_type == "All")
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM sites ORDER BY ID DESC ";
                    }
                    else
                    {
                        query = "SELECT * FROM sites where concat(Site_Name, Site_type) LIKE '%" + search + "%' ORDER BY ID DESC ";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM sites where Site_Type = '" + site_type + "' ORDER BY ID DESC ";
                    }
                    else
                    {
                        query = "SELECT * FROM sites where Site_Type = '" + site_type + "' and concat(Site_Name, Site_type) LIKE '%" + search + "%' ORDER BY ID DESC ";
                    }
                }

                if (limit != 0)
                {
                    query = query + " limit " + limit.ToString();
                }
                if (offset != 0)
                {
                    query = query + " offset " + offset.ToString();
                }
                
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] flats_show(string site_name, int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (site_name == "All")
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM v_flats ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_flats WHERE CONCAT(Status, Flat_No) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM v_flats WHERE Site_Id = @site_id ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_flats WHERE Site_Id = @site_id and CONCAT(Status, Flat_No) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }

                if (this.OpenConnection() == true)
                {
                    int id = get_site_id_by_name(site_name);

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@site_id", id);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        public List<string>[] plots_show(string site_name, int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (site_name == "All")
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM v_plot ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_plot WHERE CONCAT(Plot_Status, Plot_NO) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM v_plot WHERE Site_ID = @site_id ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_plot WHERE Site_ID = @site_id and CONCAT(Plot_Status, Plot_NO) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }

                if (this.OpenConnection() == true)
                {
                    int id = get_site_id_by_name(site_name);

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@site_id", id);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    get_list_show(dataReader);
                    dataReader.Close();
                    this.CloseConnection();
                    return list_show;
                }
                else
                {
                    return list_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_show;
            }
        }

        /**
         * Show Daily customer name for page load
         */
        public List<string>[] daily_customer_name_show()
        {
            try
            {
                string query = "SELECT ID, Customer_Name FROM daily_enquiry ORDER BY ID DESC";

                list_daily_customer_name_show[0] = new List<string>();
                list_daily_customer_name_show[1] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_daily_customer_name_show[0].Add(dataReader["ID"] + "");
                        list_daily_customer_name_show[1].Add(dataReader["Customer_Name"] + "");
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_daily_customer_name_show;
                }
                else
                {
                    return list_daily_customer_name_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_daily_customer_name_show;
            }
        }

        /**
         * Show site type
         */
        public string show_site_type(string site_id)
        {
            try
            {
                string query = "SELECT Site_Type FROM sites where ID = '" + site_id + "'";


                string site_type = "";


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        site_type = dataReader[0].ToString();

                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return site_type;
                }
                else
                {
                    return site_type;
                }
            }
            catch (MySqlException ex)
            {
                return "Failed";
            }
        }

        /**
         * Show Daily wing name for page load
         */
        public List<string>[] wing_show_name(string site_id)
        {
            try
            {
                string query = "";
                string site_type = show_site_type(site_id);
                if (site_type == "Flat")
                {
                    query = "SELECT ID, Wing FROM flats where Site_Id = '" + site_id + "' order by ID DESC";
                }
                else
                {
                    query = "SELECT ID, Wing FROM plot where Site_ID = '" + site_id + "' order by ID DESC";
                }

                list_wing_name_show[0] = new List<string>();
                list_wing_name_show[1] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_wing_name_show[0].Add(dataReader["ID"] + "");
                        list_wing_name_show[1].Add(dataReader["Wing"] + "");

                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_wing_name_show;
                }
                else
                {
                    return list_wing_name_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_wing_name_show;
            }
        }
        /**
         * Show Daily wing name for page load
         */
        public List<string>[] booking_details(string applicant_id)
        {
            try
            {
                string query = "SELECT * FROM booking_details where applicant_ID = '" + applicant_id + "' ORDER BY applicant_ID DESC";
                
                list_booking_details_show[0] = new List<string>();
                list_booking_details_show[1] = new List<string>();
                list_booking_details_show[2] = new List<string>();
                list_booking_details_show[3] = new List<string>();
                list_booking_details_show[4] = new List<string>();
                list_booking_details_show[5] = new List<string>();
                list_booking_details_show[6] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_booking_details_show[0].Add(dataReader["applicant_ID"] + "");
                        list_booking_details_show[1].Add(dataReader["sites_ID"] + "");
                        list_booking_details_show[2].Add(dataReader["flats_ID"] + "");
                        list_booking_details_show[3].Add(dataReader["Applicant_Name"] + "");                        
                        list_booking_details_show[4].Add(dataReader["Site_Name"] + "");
                        list_booking_details_show[5].Add(dataReader["Wing"] + "");                        
                        list_booking_details_show[6].Add(dataReader["Flat_No"] + "");
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_booking_details_show;
                }
                else
                {
                    return list_booking_details_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_booking_details_show;
            }
        }

        /**
         * Show Daily flat no for page load
         */
        public List<string>[] flat_show_no(string site_id)
        {
            try
            {
                string query;
                string que = "select Site_Type from sites where ID = " + site_id;
                if (this.OpenConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(que, connection);
                    string site_type = command.ExecuteScalar() as string;
                    if (site_type == "Plot")
                    {
                        query = "SELECT ID, Plot_NO as NUM, Wing FROM plot where Site_ID = '" + site_id + "' order by ID DESC";
                    }
                    else
                    {
                        query = "SELECT ID, Flat_No as NUM, Wing FROM flats where Site_Id = '" + site_id + "' order by ID DESC";
                    }

                    list_flat_no_show[0] = new List<string>();
                    list_flat_no_show[1] = new List<string>();
                    list_flat_no_show[2] = new List<string>();
                
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_flat_no_show[0].Add(dataReader["ID"] + "");
                        list_flat_no_show[1].Add(dataReader["NUM"] + "");
                        if (site_type == "Flat")
                        {
                            list_flat_no_show[2].Add(dataReader["Wing"] + "");
                        }
                        else
                        {
                            list_flat_no_show[2].Add("");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_flat_no_show;
                }
                else
                {
                    return list_flat_no_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_flat_no_show;
            }
        }

        public int get_site_id_by_name(string site)
        {
            try
            {
                string query = "select id from sites where Site_Name = '" + site + "'";
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    return id;
                }
                else
                {
                    return 0;
                }
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public List<string>[] customer_booking_show()
        {
            try
            {
                string query = "select b.ID as ID,a.Applicant_Name as Applicant_Name from applicant a inner join bookings b on a.Id = b.Applicant_Id ORDER BY b.ID DESC";

                list_customer_booking_show[0] = new List<string>();
                list_customer_booking_show[1] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_customer_booking_show[0].Add(dataReader["ID"] + "");
                        list_customer_booking_show[1].Add(dataReader["Applicant_Name"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_customer_booking_show;
                }
                else
                {
                    return list_customer_booking_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_customer_booking_show;
            }
        }

        public int get_count(string table)
        {
            try
            {
                int count = 0;
                string query = "select count(id) from " + table;
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    this.CloseConnection();
                }
                return count;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public int get_alarm_count()
        {
            try
            {
                int count = 0;
                string query = "SELECT Sum( a.count ) " +
                    "FROM (SELECT Count( id ) AS count FROM kolhedeveloper.daily_followup where Folloup_Date = Date(NOW()) or Next_Folloup_Date = Date(NOW()) UNION ALL " +
                    "SELECT Count( id ) AS count FROM kolhedeveloper.payment_commitment where Commitment_Date = Date(NOW()) UNION ALL  " +
                    "SELECT Count( id ) AS count FROM kolhedeveloper.payment_details where Cheque_Date = Date(NOW()) ) a;";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    this.CloseConnection();
                }
                return count;
            }
            catch (MySqlException ex)
            {
                return 0;
            }
        }

        public List<string>[] alarms_show(string page)
        {
            try
            {
                string query = "";
                if (page == "followup")
                {
                    query = "SELECT * FROM daily_followup where Folloup_Date=Date(NOW()) or Next_Folloup_Date=Date(NOW()) ORDER BY ID DESC";
                }
                else if (page == "paycommit")
                {
                    query = "SELECT * FROM payment_commitment where Commitment_Date=Date(NOW()) ORDER BY ID DESC";
                }
                else
                {
                    query = "SELECT * FROM payment_details where Cheque_Date=Date(NOW()) ORDER BY ID DESC";
                }

                for (int i = 0; i < 12; i++)
                {
                    list_alarms_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {                        
                        if (page == "followup")
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                list_alarms_show[i].Add(dataReader[i] + "");
                            }
                        }
                        else if (page == "paycommit")
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                list_alarms_show[i].Add(dataReader[i] + "");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                list_alarms_show[i].Add(dataReader[i] + "");
                            }
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_alarms_show;
                }
                else
                {
                    return list_alarms_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_alarms_show;
            }
        }

        public int Delete_Record(string table, int id)
        {
            try
            {
                string query = "DELETE FROM " + table + " where ID=" + id;

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return 0;
                }
                return -1;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public List<string> get_edit_record(string table, int id)
        {
            try
            {
                string query = "select * FROM " + table + " where ID=" + id;
                List<string> list_edit = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int count = dataReader.FieldCount;
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < count; i++)
                        {
                            list_edit.Add(dataReader.GetValue(i).ToString());
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_edit;
                }
                else
                {
                    return list_edit;
                }
            }
            catch (MySqlException ex)
            {
                return new List<string>();
            }
        }

        /*Password Hashing*/
        private static string getHash(string text)
        {
            // SHA512 is disposable by inheritance. 
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash. 
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string. 
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        /*Login Section*/
        public string Login(string name, string password)
        {
            try
            {
                var pass = password + "4804";
                var hash = getHash(pass);
                MySqlDataReader rdr;
                string query = "select User_Type from user where User_Name = @name and Password = @password";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", hash);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        string role = rdr["User_Type"].ToString();
                        this.CloseConnection();
                        return role;
                    }
                }
                return "false";
            }
            catch (MySqlException ex)
            {
                return "false";
            }
        }
        /*Login Section*/

    } //db_connect class

} // namespace