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
        public List<string>[] list_show = new List<string>[75];

        public List<string>[] list_alarms_show = new List<string>[75];
        public List<string>[] list_executive_show_name = new List<string>[2];
        public List<string>[] list_customer_show_name = new List<string>[2];
        public List<string>[] list_finance_name_show = new List<string>[2];
        public List<string>[] list_customer_booking_show = new List<string>[2];
        public List<string>[] list_daily_customer_name_show = new List<string>[2];
        public List<string>[] list_wing_name_show = new List<string>[2];
        public List<string>[] list_booking_details_show = new List<string>[7];        
        public List<string>[] list_flat_no_show = new List<string>[3];
        public List<string>[] list_sitewise_booking_show = new List<string>[21];
        public List<DailyFollowup> list_enquiry_followup_show = new List<DailyFollowup>();
        public List<string>[] masterlist = new List<string>[75];
        public List<string>[] masterlist1 = new List<string>[75];
        public List<string>[] masterlist2 = new List<string>[75];
        public List<string>[] masterlist3 = new List<string>[75];
        public List<string>[] masterlist4 = new List<string>[75];

        public string cipher_text = "4804";
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

        private bool OpenConnection()
        {
            string connetionString = null;
            connetionString = "server=182.50.133.77;database=kolhedeveloper1;uid=kolheadmin1;pwd=Kolhe@123;Allow User Variables=True;SslMode=none";
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
            for (int i = 0; i < 75; i++)
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
                    query = "UPDATE daily_customer SET " +
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
                    query = "INSERT INTO daily_customer (Customer_Name, Address, Mobile_No, Second_Mobile_No, Email_ID, " +
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
                    cmd.Parameters.AddWithValue("@exe1", (enqexename1 != "" ? enqexename1 : null));
                    cmd.Parameters.AddWithValue("@exe2", (enqexename2 != "" ? enqexename2 : null));
                    cmd.Parameters.AddWithValue("@exe3", (enqexename3 != "" ? enqexename3 : null));

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
                        " Followup_Date = @follow," +
                        " Next_Followup_Date = @nextfollow," +
                        " Followup_Details = @followdetail," +
                        " Executive1_ID = @exe1," +
                        " Executive2_ID = @exe2," +
                        " Executive3_ID = @exe3 where id=@id";
                }
                else
                {
                    query = "INSERT INTO daily_followup (Daily_Customer_ID, Followup_Date, Next_Followup_Date, Followup_Details, " +
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
                    cmd.Parameters.AddWithValue("@exe1", (enqexename1 != "" ? enqexename1 : null));
                    cmd.Parameters.AddWithValue("@exe2", (enqexename2 != "" ? enqexename2 : null));
                    cmd.Parameters.AddWithValue("@exe3", (enqexename3 != "" ? enqexename3 : null));

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
                    query = "UPDATE flat_plot SET " +
                        "Number = @flatno," +
                        " Floor = @floor," +
                        " Area = @area," +
                        " Type = @flat_type," +
                        " Wing = @wing," +
                        " Status = @status," +
                        " Site_Id = @siteid where id=@id";
                }
                else
                {
                    query = "INSERT INTO flat_plot (Number, Floor, Area, Type, Wing, Date, Status, Site_Id) " +
                    "VALUES(@flatno, @floor, @area, @flat_type, @wing, NOW(), @status, @siteid)";
                }
                if (flatfloor == "")
                {
                    flatfloor = "0";
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
        
        public int insert_executive(string exetype, string exename, string execode, string exeemail, string exemob, string exeadd, string exejoin, string exebirth, string exestatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE executive_franchies SET " +
                        "Name = @name," +
                        " Code = @code," +
                        " Email_Id = @email," +
                        " Phone = @phone," +
                        " Address = @addr," +
                        " Birth_Date = @birth," +
                        " Joining_Date = @join," +
                        " Executive_type = @type," +
                        " Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO executive_franchies (Name, Code, Email_Id, Phone, Address, Birth_Date, Joining_Date, Date, Status, Executive_type) " +
                    "VALUES(@name, @code, @email, @phone, @addr, @birth, @join, NOW(), @status, @type)";
                }

                if (this.OpenConnection() == true)
                {
                    string birthdate = exebirth;
                    if (exebirth == "" || exebirth == null)
                    {
                        birthdate = null;
                    }
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", exename);
                    cmd.Parameters.AddWithValue("@code", execode);
                    cmd.Parameters.AddWithValue("@email", exeemail);
                    cmd.Parameters.AddWithValue("@phone", exemob);
                    cmd.Parameters.AddWithValue("@addr", exeadd);
                    cmd.Parameters.AddWithValue("@birth", birthdate);
                    cmd.Parameters.AddWithValue("@join", exejoin);
                    cmd.Parameters.AddWithValue("@status", exestatus);
                    cmd.Parameters.AddWithValue("@type", exetype);

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

        public int insert_users(string uname, string utype, string username, string upass, string uemail, string uphone, string ustatus, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE user SET " +
                        "Name = @name," +
                        " User_Name = @username," +
                        " Password = @pass," +
                        " Email_Id = @email," +
                        " Phone = @phone," +
                        " User_Type = @type," +                        
                        " Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO user (Name, User_Name, Password, Email_Id, Phone, User_Type, Date, Status) " +
                    "VALUES(@name, @username, @pass, @email, @phone, @type, NOW(), @status)";
                }

                string password_string = upass + cipher_text;
                string phash = getHash(password_string);

                if (this.OpenConnection() == true)
                {                    
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", uname);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@pass", phash);
                    cmd.Parameters.AddWithValue("@phone", uphone);
                    cmd.Parameters.AddWithValue("@email", uemail);
                    cmd.Parameters.AddWithValue("@type", utype);
                    cmd.Parameters.AddWithValue("@status", Int32.Parse(ustatus));

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
                    query = "UPDATE executive_franchies SET " +
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
                    query = "INSERT INTO executive_franchies (Name, Code, Email_Id, Phone, Address, Joining_Date, Date, Status, Executive_type) " +
                    "VALUES(@name, @code, @email, @phone, @addr, @join, NOW(), @status, 'Franchies')";
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
            string apploccu, string applbirth, string applstatus, string type = "insert", int id = 0)
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
                        " Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO applicant (Applicant_Name, Applicant_Email_Id, Applicant_Phone, Applicant_Address, Applicant_Pan_No, " +
                    "Applicant_Adhar_No, Applicant_Occupation, Applicant_DOB, Date, Status) " +
                    "VALUES(@name, @email, @phone, @addr, @pan, @aadhar, @occu, @birth, NOW(), @status)";
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

        public int insert_notes(string notesummary, string notedesc, string notedate, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE notes SET " +
                        " Note_Summary = @summary," +
                        " Note_Details = @desc," +
                        " Date = @date where id=@id";
                }
                else
                {
                    query = "INSERT INTO notes (Note_Summary, Note_Details, Date) " +
                    "VALUES(@summary, @desc, @date)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@summary", notesummary);
                    cmd.Parameters.AddWithValue("@desc", notedesc);
                    cmd.Parameters.AddWithValue("@date", notedate);

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

        public int insert_exe_franc_incentive(string ename, string bapplicant, string bsite, string bwing, string bflats,string paidamt, string type = "insert", int id = 0)
        {
            try
            {
                //string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Site_Id=" + bsite + ")";
                string que = "(select ID from execu_fran_audit where Booking_ID = (select ID from bookings where Applicant_Id = " + bapplicant  + " and Flat = " + bflats + " and Site_Id = " + bsite + ")" + " and Executive_ID = " + ename + ")";
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE execu_fran_incentive SET " +
                        "Total_Paid_Amount = @paidamt," +
                        " Exe_Fran_Audit_ID = " + que + " where id=@id";
                }
                else
                {
                    query = "INSERT INTO execu_fran_incentive (Total_Paid_Amount, Date, Exe_Fran_Audit_ID) " +
                    "VALUES(@paidamt, NOW(), " + que + ")";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);                    
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

        public int insert_exe_franc_audit(string ename, string bapplicant, string bsite, string bwing, string bflats, string incentive, string share, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Site_Id=" + bsite + ")";
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE execu_fran_audit SET " +                        
                        " Executive_ID = @ename," +
                        " Total_Incentive = @incentive," +
                        " Total_Share = @share," +
                        " Booking_ID = " + que + " where id=@id";
                }
                else
                {
                    query = "INSERT INTO execu_fran_audit (Executive_ID, Total_Incentive, Total_Share, Date, Booking_ID) " +
                    "VALUES(@ename, @incentive, @share, NOW(), " + que + ")";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@bno", bapplicant);
                    cmd.Parameters.AddWithValue("@ename", ename);
                    cmd.Parameters.AddWithValue("@incentive", incentive);
                    cmd.Parameters.AddWithValue("@share", share);                    

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

        public int insert_booking(string breferred, string bapplicant, string btamount, string bramount, string bblder,
            string bsite, string bwing, string bflats, string bcharges, string other, string bparking, string bcancel,
            string bfollowup, string bstatus, string bremark, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE bookings SET " +
                        " Referenceby = @bref," +
                        " Applicant_Id = @bappl," +
                        " Total_Flat_Amount = @btamt," +
                        " Received_Amount = @bramt," +
                        " Total_Builder_Received = @bbldr," +
                        " Site_Id = @bsite," +
                        " Wing = @bwing," +
                        " Flat = @bflat," +
                        " Internal_Charges = @bchrg," +
                        " Other = @other," +
                        " Reserved_Parking = @bpark," +
                        " Flat_Cancled_By = @bcan," +
                        " Follow_Up_Date = @bflp," +
                        " Status = @bsts," +
                        " Remark = @bremark where id=@id";
                }
                else
                {
                    query = "INSERT INTO bookings (Referenceby, Applicant_Id, Total_Flat_Amount, " +
                        "Received_Amount, Total_Builder_Received, Site_Id, Wing, Flat, Internal_Charges, Other, Reserved_Parking, " +
                        "Flat_Cancled_By, Follow_Up_Date, Remark, Status, Date) " +
                    "VALUES(@bref, @bappl, @btamt, @bramt, @bbldr, @bsite, @bwing, @bflat, @bchrg, @other, @bpark, @bcan," +
                    " @bflp, @bremark, @bsts, NOW())";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@bref", breferred);
                    cmd.Parameters.AddWithValue("@bappl", bapplicant);
                    cmd.Parameters.AddWithValue("@btamt", btamount);
                    cmd.Parameters.AddWithValue("@bramt", bramount);
                    cmd.Parameters.AddWithValue("@bbldr", bblder);
                    cmd.Parameters.AddWithValue("@bsite", bsite);
                    cmd.Parameters.AddWithValue("@bwing", bwing);
                    cmd.Parameters.AddWithValue("@bflat", bflats);
                    cmd.Parameters.AddWithValue("@bchrg", bcharges);
                    cmd.Parameters.AddWithValue("@other", other);
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

        public int insert_paymentcommit(string ctype, string camount, string cdate, string cremark, string bapplicant, string bsite, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Site_Id=" + bsite + ")";
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
            string ptype, string bldpay, string bnkpay, string sts, string bapplicant, string bsite, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Site_Id=" + bsite + ")";
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

        public int insert_agreement(string ano, string adate, string anotary, string aamount, string aadjustment, string aextra, string gst, string astatus, string bapplicant, string bsite, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Site_Id=" + bsite + ")";
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE agreement SET " +
                        "Agreement_Amount = @aamount," +
                        " Agreement_Date = @adate," +
                        " Agreement_No = @ano," +
                        " Status = @astatus," +
                        " Notary_Amount = @notary," +
                        " Adjustment_Amount = @adjust," +
                        " Extra_Amount = @extra," +
                        " GST_Amount = @gst, " +
                        " Booking_Id = " + que + " where id=@id";
                }
                else
                {
                    query = "INSERT INTO agreement (Agreement_Amount, Agreement_Date, Agreement_No, Status, Notary_Amount, Adjustment_Amount, Extra_Amount, GST_Amount, Date, Booking_Id) " +
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
            string filesta, string filesanctdate, string reqloanamt, string sanctloanamt, string disburseamt, string actloanamt, string diffloanamt, string recddamt, string remddamt, string rateofinter, string emiamt, string emimonths, string finstat, string bapplicant, string bsite, string bflats, string type = "insert", int id = 0)
        {
            try
            {
                string que = "(select ID from bookings where Applicant_Id=" + bapplicant + " and Flat=" + bflats + " and Site_Id=" + bsite + ")";
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
                        " Difference_Loan_Amount = @dlamt," +
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
                    "Required_Loan_Amount, Sanctioned_Loan_Amount, Total_Disbursed_Amount, Actual_Loan_Amount, Difference_Loan_Amount, Received_DD_Amount, " +
                    "Remaining_DD_Amount, Rate_Of_Interest, EMI_Amount, EMI_Total_Months, Status, Date, Booking_Id) " +
                    "VALUES(@ftype, @fname, @fexename, @fexemob, @fexemail, @fhdate, @fsts, @fdate, @rlamt, @slamt, @dbrmnt," +
                    " @alamt, @dlamt, @ramt, @remamt, @rointr, @emiamt, @emimonths, @finstat, NOW(), " + que + ")";
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
                    cmd.Parameters.AddWithValue("@dlamt", diffloanamt);
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
                    cmd.Parameters.AddWithValue("@fid", Int32.Parse(fid));
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
                    query = "SELECT * FROM daily_customer ORDER BY ID DESC LIMIT @lim OFFSET @off";
                    //query = "SELECT * FROM daily ORDER BY ID DESC LIMIT @lim OFFSET @off";
                    //query = "daily_enquiry_sp";
                }
                else
                {
                    query = "SELECT * FROM daily_customer where CONCAT(" + daily_customer_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @lim OFFSET @off";
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
                    query = "SELECT * FROM v_daily_sitevisit where CONCAT(" + daily_sitevisit_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @lim OFFSET @off";
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
                    query = "SELECT * FROM v_daily_followup where CONCAT(" + daily_followup_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @lim OFFSET @off";
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

        public List<string>[] notes_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM notes ORDER BY ID DESC LIMIT @lim OFFSET @off";
                }
                else
                {
                    query = "SELECT * FROM notes where CONCAT(" + notes_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @lim OFFSET @off";
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
                DateTime defaultDate = Convert.ToDateTime("1967-01-01");
                if (enqStartDate == defaultDate) {
                    enqStartDate = defaultDate;
                }
                if (enqEndDate == defaultDate)
                {
                    enqEndDate = todayDate;
                }
                
                string query = "select * from daily_customer_followup_view2 where " +
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
                        if (dataReader["followup_Date"] != DBNull.Value)
                        {
                            DailyFollowupobj.folloup_Date = Convert.ToDateTime(dataReader["followup_Date"]);
                        }
                        if (dataReader["Next_followup_Date"] != DBNull.Value)
                        {
                            DailyFollowupobj.Next_folloup_Date = Convert.ToDateTime(dataReader["Next_followup_Date"]);
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
                //list_enquiry_followup_show = new List<DailyFollowup>();
                string query = ""; 
                DateTime todayDate = DateTime.Now;
                DateTime defaultDate = Convert.ToDateTime("1967-01-01");
                if (startDate == defaultDate)
                {
                    startDate = defaultDate;
                }
                if (endDate == defaultDate)
                {
                    endDate = todayDate;
                }

                query = "select * from sitewise_booking_flat_plot where " +
                               "( Booking_Date between '" + startDate + "' and '" + endDate + "')"
                               + " and Site_Id =  " + siteName;
                
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
                    query = "SELECT * FROM v_cost_sheet where Cost_Sheet_Type = @sheet_type and CONCAT(" + cost_sheet_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                    query = "SELECT * FROM applicant where CONCAT(" + applicant_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                    query = "SELECT * FROM v_co_applicant where CONCAT(" + co_applicant_column +") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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

        /*
         *Get customer and co-applicant report 
         * */
        public List<string>[] customer_report(string customerName)
        {
            try
            {
                clear_list_show();
                
                string query = "SELECT * FROM v_co_applicant where Applicant_ID=@customer";
                

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@customer", customerName);
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
                    query = "SELECT * FROM v_finance_details where CONCAT(" + finanace_column +") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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

        /*
         * Get Finance report
         * */
        public List<string>[] finance_report(DateTime startDate, DateTime endDate,
                                             string financeName, string siteName,
                                             string filesta)
        {
            try
            {
                clear_list_show();
                string query = "";
                DateTime todayDate = DateTime.Now;
                DateTime defaultDate = Convert.ToDateTime("1967-01-01");
                if (startDate == defaultDate)
                {
                    startDate = defaultDate;
                }
                if (endDate == defaultDate)
                {
                    endDate = todayDate;
                }
                
                query = "select * from v_finance_details where " +
                            "( finance_details_date between '" + startDate + "' and '" + endDate + "')";
                                   
                if (financeName != null && financeName != "")
                {
                    query = query + " and (Finance_Name = '" + financeName + "')";
                }
                
                if (siteName != null && siteName != "")
                {
                    //query = query + " and (Site_Id = '" + siteName + "')";
                }

                if (filesta != null && filesta != "")
                {
                    //query = query + " and (File_Status = '" + filesta + "')";
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

        /*
         * Get Profit Loss report
         * */
        public List<string>[] profit_loss_report(DateTime startDate, DateTime endDate,
                                             string siteName)
        {
            try
            {
                clear_list_show();
                string query = "";
                DateTime todayDate = DateTime.Now;
                DateTime defaultDate = Convert.ToDateTime("1967-01-01");
                if (startDate == defaultDate)
                {
                    startDate = defaultDate; //todayDate;
                }
                if (endDate == defaultDate)
                {
                    endDate = todayDate;
                }

                query = "select * from v_commission where " +
                            "( Booking_Date between '" + startDate + "' and '" + endDate + "')";

               
                if (siteName != null && siteName != "")
                {
                    query = query + " and (Site_Id = '" + siteName + "')";
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

        /*
         * Get Executive Franchies report
         * */
        public List<string>[] execu_fran_report(DateTime startDate, DateTime endDate,
                                             string siteName, string ename)
        {
            try
            {
                clear_list_show();
                string query = "";
                DateTime todayDate = DateTime.Now;
                DateTime defaultDate = Convert.ToDateTime("1967-01-01");
                if (startDate == defaultDate)
                {
                    startDate = defaultDate;
                }
                if (endDate == defaultDate)
                {
                    endDate = todayDate;
                }

                query = "select * from v_exec_fran where " +
                            "( Date between '" + startDate + "' and '" + endDate + "')";

                if (ename != null && ename != "")
                {
                    query = query + " and (E_ID = '" + ename + "')";
                }

                if (siteName != null && siteName != "")
                {
                    //query = query + " and (Site_Id = '" + siteName + "')";
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


        /*
         * Get Master report
         * */
        public List<string>[] master_report(int applid,int siteName, int flatno)
        {
            //List<string>[] list = new List<string>[75];

            try
            {
                for (int i = 0; i < 75; i++)
                {
                    masterlist[i] = new List<string>();
                }
                //clear_list_show();

                string query = "usp_Get_MasterReportDetails";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Applicant_Id", applid);
                    cmd.Parameters.AddWithValue("@Site_Id", siteName);
                    cmd.Parameters.AddWithValue("@Flat_No", flatno);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        masterlist[i] = new List<string>();
                    }

                    //get_list_show(dataReader);

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            masterlist[i].Add(dataReader[i].ToString());
                        }
                    }

                    dataReader.Close();

                    this.CloseConnection();

                    return masterlist;
                }
                else
                {
                    return masterlist;
                }
            }
            catch (MySqlException ex)
            {
                return masterlist;
            }
        }

        /*
         * Get Master report
         * */
        public List<string>[] master_report1(int applid, int siteName, int flatno)
        {
            //List<string>[] list = new List<string>[75];

            try
            {
                //clear_list_show();
                string query = "usp_Get_FranchiseDetails";

                for (int i = 0; i < 75; i++)
                {
                    masterlist1[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Applicant_Id", applid);
                    cmd.Parameters.AddWithValue("@Site_Id", siteName);
                    cmd.Parameters.AddWithValue("@Flat_No", flatno);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        masterlist1[i] = new List<string>();
                    }

                    //get_list_show(dataReader);
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            masterlist1[i].Add(dataReader[i] + "");
                        }
                    }

                    dataReader.Close();

                    this.CloseConnection();

                    return masterlist1;
                }
                else
                {
                    return masterlist1;
                }
            }
            catch (MySqlException ex)
            {
                return masterlist1;
            }
        }

        /*
         * Get Master report
         * */
        public List<string>[] master_report2(int applid, int siteName, int flatno)
        {
            //List<string>[] list = new List<string>[75];

            try
            {
                //clear_list_show();
                for (int i = 0; i < 75; i++)
                {
                    masterlist2[i] = new List<string>();
                }
                string query = "usp_Get_PaymentCommDetails";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Applicant_Id", applid);
                    cmd.Parameters.AddWithValue("@Site_Id", siteName);
                    cmd.Parameters.AddWithValue("@Flat_No", flatno);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        masterlist2[i] = new List<string>();
                    }

                    //get_list_show(dataReader);
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            masterlist2[i].Add(dataReader[i] + "");
                        }
                    }

                    dataReader.Close();

                    this.CloseConnection();

                    return masterlist2;
                }
                else
                {
                    return masterlist2;
                }
            }
            catch (MySqlException ex)
            {
                return masterlist2;
            }
        }

        /*
        * Get Master report
        * */
        public List<string>[] master_report3(int applid, int siteName, int flatno)
        {
            //List<string>[] list = new List<string>[75];
            try
            {
                //clear_list_show();
                for (int i = 0; i < 75; i++)
                {
                    masterlist3[i] = new List<string>();
                }
                string query = "usp_Get_PaymentDetails";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Applicant_Id", applid);
                    cmd.Parameters.AddWithValue("@Site_Id", siteName);
                    cmd.Parameters.AddWithValue("@Flat_No", flatno);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        masterlist3[i] = new List<string>();
                    }

                    //get_list_show(dataReader);
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            masterlist3[i].Add(dataReader[i] + "");
                        }
                    }

                    dataReader.Close();

                    this.CloseConnection();

                    return masterlist3;
                }
                else
                {
                    return masterlist3;
                }
            }
            catch (MySqlException ex)
            {
                return masterlist3;
            }
        }

        /*
        * Get Master report
        **/
        public List<string>[] master_report4(string financeID)
        {
            //List<string>[] list = new List<string>[75];
            try
            {
                //clear_list_show();
                for (int i = 0; i < 75; i++)
                {
                    masterlist4[i] = new List<string>();
                }
                string query = "SELECT * FROM v_file_details where Finance_Id = @financeid";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@financeid", financeID);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        masterlist4[i] = new List<string>();
                    }

                    //get_list_show(dataReader);
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            masterlist4[i].Add(dataReader[i] + "");
                        }
                    }

                    dataReader.Close();

                    this.CloseConnection();

                    return masterlist4;
                }
                else
                {
                    return masterlist4;
                }
            }
            catch (MySqlException ex)
            {
                return masterlist4;
            }
        }

        /*
         * Get File Process report
         * */
        public List<string>[] fileprocess_report(string financeName)
        {
            try
            {
                clear_list_show();
                string query = "";
                
                query = "select * from v_file_details";

                if (financeName != null && financeName != "")
                {
                    query = query + " where (Finance_Name LIKE '%" + financeName + "%')";
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
                    query = "SELECT * FROM v_bookings where CONCAT(" + bookings_column +") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                    query = "SELECT * FROM executive_franchies ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM executive_franchies where CONCAT(" + executive_franchies_column +") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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

        public List<string>[] user_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM user ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM user where CONCAT(" + user_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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

        public List<string>[] executive_incentive_show(int offset, int limit, string search = "")
        {
            try
            {
                clear_list_show();
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM v_ef_audit_incentive ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_ef_audit_incentive where CONCAT(" + executive_audit_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                string query = "SELECT ID, Name FROM executive_franchies ORDER BY ID DESC";

                list_executive_show_name[0] = new List<string>();
                list_executive_show_name[1] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_executive_show_name[0].Add(dataReader["ID"] + "");
                        list_executive_show_name[1].Add(dataReader["Name"] + "");
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
                    query = "SELECT * FROM v_file_details where CONCAT(" + file_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                    query = "SELECT * FROM v_payment_commitment where CONCAT(" + payment_commitment_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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

        /*
         * Get Payment Commit report
         * */
        public List<string>[] paycommit_report(DateTime startDate, DateTime endDate)
        {
            try
            {
                clear_list_show();
                string query = "";
                DateTime todayDate = DateTime.Now;
                DateTime defaultDate = Convert.ToDateTime("1967-01-01");
                if (startDate == defaultDate)
                {
                    startDate = defaultDate;
                }
                if (endDate == defaultDate)
                {
                    endDate = todayDate;
                }

                query = "select * from v_payment_commitment where " +
                            "( Commitment_Date between '" + startDate + "' and '" + endDate + "')";

                
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

        /*
         * Get cost sheet report
         * */
        public List<string>[] costsheet_report(string costSheet, string siteName)
        {
            try
            {
                clear_list_show();
                string query = "";
                

                query = "select * from v_cost_sheet where Cost_Sheet_Type = '" + costSheet + "' and (Site_Id = '" + siteName + "')";
                
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
                    query = "SELECT * FROM v_payment_details where CONCAT(" + payment_details_column +") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                    query = "SELECT * FROM v_agreement ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM v_agreement where CONCAT(" + agreement_column +") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                    query = "SELECT * FROM executive_franchies ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM executive_franchies where CONCAT(" + executive_franchies_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                        query = "SELECT * FROM sites where concat(" + sites_column + ") LIKE '%" + search + "%' ORDER BY ID DESC ";
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
                        query = "SELECT * FROM sites where Site_Type = '" + site_type + "' and concat(" + sites_column + ") LIKE '%" + search + "%' ORDER BY ID DESC ";
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
                        query = "SELECT * FROM v_flat_plot ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_flat_plot WHERE CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }
                else if (site_name == "All Flats")
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM v_flat_plot where Site_Type='Flat' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_flat_plot WHERE Site_Type='Flat' and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }
                else if (site_name == "All Plots")
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM v_flat_plot where Site_Type='Plot' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_flat_plot WHERE Site_Type='Plot' and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        query = "SELECT * FROM v_flat_plot WHERE Site_Name = @site_name ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                    else
                    {
                        query = "SELECT * FROM v_flat_plot WHERE Site_Name = @site_name and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                    }
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@site_name", site_name);
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
                        query = "SELECT * FROM v_plot WHERE CONCAT(" + flat_plot_column +") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                        query = "SELECT * FROM v_plot WHERE Site_ID = @site_id and CONCAT(" + flat_plot_column + ") LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
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
                string query = "SELECT ID, Customer_Name FROM daily_customer ORDER BY ID DESC";

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
         * Show Daily finance name for page load
         */
        public List<string>[] finance_name(string appid, string siteid, string flatid)
        {
            try
            {
                string query = "select ID, Finance_Name from finance_details where Booking_Id = (select ID from bookings where Applicant_Id = " + appid + " and Site_Id = " + siteid + " and Flat = " + flatid + ") ORDER BY ID DESC";

                list_finance_name_show[0] = new List<string>();
                list_finance_name_show[1] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_finance_name_show[0].Add(dataReader["ID"] + "");
                        list_finance_name_show[1].Add(dataReader["Finance_Name"] + "");
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_finance_name_show;
                }
                else
                {
                    return list_finance_name_show;
                }                
            }
            catch (MySqlException ex)
            {
                return list_finance_name_show;
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

                query = "SELECT ID, Wing FROM flat_plot where Site_Id = '" + site_id + "' order by ID DESC";
                
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
                string query = "SELECT * FROM v_bookings where Applicant_Id = '" + applicant_id + "' ORDER BY Applicant_Id DESC";
                
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
                        list_booking_details_show[0].Add(dataReader["Applicant_Id"] + "");
                        list_booking_details_show[1].Add(dataReader["Site_Id"] + "");
                        list_booking_details_show[2].Add(dataReader["Flat"] + "");
                        list_booking_details_show[3].Add(dataReader["Applicant_Name"] + "");                        
                        list_booking_details_show[4].Add(dataReader["Site_Name"] + "");
                        list_booking_details_show[5].Add(dataReader["Wing"] + "");                        
                        list_booking_details_show[6].Add(dataReader["Number"] + "");
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

                    query = "SELECT ID, Number as NUM, Wing FROM flat_plot where Site_Id = '" + site_id + "' order by ID DESC";
                    
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
                    "FROM (SELECT Count( id ) AS count FROM daily_followup where Followup_Date = Date(NOW()) or Next_Followup_Date = Date(NOW()) UNION ALL " +
                    "SELECT Count( id ) AS count FROM payment_commitment where Commitment_Date = Date(NOW()) UNION ALL  " +
                    "SELECT Count( id ) AS count FROM payment_details where Cheque_Date = Date(NOW()) ) a;";

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
                    query = "SELECT * FROM v_daily_followup where Followup_Date=Date(NOW()) or Next_Followup_Date=Date(NOW()) ORDER BY ID DESC";
                }
                else if (page == "paycommit")
                {
                    query = "SELECT * FROM v_payment_commitment where Commitment_Date=Date(NOW()) ORDER BY ID DESC";
                }
                else
                {
                    query = "SELECT * FROM v_payment_details where Cheque_Date=Date(NOW()) ORDER BY ID DESC";
                }

                for (int i = 0; i < 75; i++)
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
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                list_alarms_show[i].Add(dataReader[i] + "");
                            }
                        }
                        else if (page == "paycommit")
                        {
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                list_alarms_show[i].Add(dataReader[i] + "");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dataReader.FieldCount; i++)
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

        public List<string> get_showcase_from_bookingID(int id)
        {
            try
            {
                string query = "select Applicant_Id, Site_Id, Flat from v_bookings where ID = " + id;
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

        public List<string> get_showcase_from_efaID(int id)
        {
            try
            {
                string query = "select Executive_ID, Booking_ID FROM execu_fran_audit where ID = " + id;
                List<string> list_edit = new List<string>();
                List<string> list_edit1 = new List<string>();
                List<string> list_edit2 = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int count = dataReader.FieldCount;
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < count; i++)
                        {
                            list_edit1.Add(dataReader.GetValue(i).ToString());
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    list_edit2 = get_showcase_from_bookingID(Int32.Parse(list_edit1[1]));
                    list_edit1.RemoveAt(1);
                    list_edit.AddRange(list_edit1);
                    list_edit.AddRange(list_edit2);
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

        public List<string> get_showcase_from_financeID(int id)
        {
            try
            {                
                string query = "select Booking_Id from finance_details where ID = " + id;
                List<string> list_edit = new List<string>();
                List<string> list_edit1 = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int count = dataReader.FieldCount;
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < count; i++)
                        {
                            list_edit1.Add(dataReader.GetValue(i).ToString());
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    list_edit = get_showcase_from_bookingID(Int32.Parse(list_edit1[0]));
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
                var pass = password + cipher_text;
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