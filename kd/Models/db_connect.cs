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
        public List<string>[] list_enquiry_show = new List<string>[21];
        public List<string>[] list_sites_show = new List<string>[9];
        public List<string>[] list_executive_show = new List<string>[10];
        public List<string>[] list_executive_show_name = new List<string>[2];
        public List<string>[] list_franchies_show = new List<string>[9];
        public List<string>[] list_franchies_show_name = new List<string>[2];
        public List<string>[] list_customer_show = new List<string>[17];
        public List<string>[] list_customer_show_name= new List<string>[2];
        public List<string>[] list_paycommit_show = new List<string>[7];
        public List<string>[] list_paydetails_show = new List<string>[12];
        public List<string>[] list_flats_show = new List<string>[9];
        public List<string>[] list_booking_show = new List<string>[17];
        public List<string>[] list_finance_show = new List<string>[21];
        public List<string>[] list_file_status_show = new List<string>[10];
        public List<string>[] list_agreement_show = new List<string>[9];
        public List<string>[] list_cost_sheet_show = new List<string>[14];
        public List<string>[] list_customer_booking_show = new List<string>[2];
        public List<string>[] list_daily_customer_name_show = new List<string>[21];
        public List<string>[] list_wing_name_show = new List<string>[9];
        public List<string>[] list_flat_no_show = new List<string>[9];

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

        public int insert_enquiry(string enqname, string enqaddress, string enqmob, string enqaltmob, string enqemail,
            string enqrequirement, string enqoccu, string enqincome, string enqbudget, string enqdown, string enqcurstatus, 
            string enqvisit, string enqsource, string enqsourcedetails, string enqsanctiontype, string type="insert", int id = 0)
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
                        "Requirement, Occupation, Income, Budget, Down_Payment, Visit, Current_Status, Source, Source_Details, Sanction_Type) " +
                        "VALUES(@name, @addr, @mob, @altmob, @email, @req, @occu, @income, @budget, @down_pay, @visit, " +
                        "@cur_status, @source, @source_detail, @sanction_type)";
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

        public int insert_de_sitevisit(string enqname, string enqsitename, string enqtype, string enqwing, string enqflatno,
            string enqsize, string enqexename1, string enqexename2, string enqexename3, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE daily_sitevisit SET " +
                        " Daily_Customer_ID = @name," +
                        " Site_ID = @site," +
                        " Type = @enqtype," +
                        " Wing = @wing," +                        
                        " Flat = @flat," +
                        " Size = @size," +
                        " Executive1_ID = @exe1," +
                        " Executive2_ID = @exe2," +
                        " Executive3_ID = @exe3 where id=@id";
                }
                else
                {
                    query = "INSERT INTO daily_sitevisit (Daily_Customer_ID, Site_ID, Type, Wing, Flat, Size, Executive1_ID, " +
                        "Executive2_ID, Executive3_ID) " +
                        "VALUES(@name, @site, @enqtype, @wing, @flat, @size, @exe1, @exe2, @exe3)";
                }
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", enqname);
                    cmd.Parameters.AddWithValue("@site", enqsitename);
                    cmd.Parameters.AddWithValue("@enqtype", enqtype);
                    cmd.Parameters.AddWithValue("@wing", enqwing);
                    cmd.Parameters.AddWithValue("@flat", enqflatno);
                    cmd.Parameters.AddWithValue("@size", enqsize);
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
                        "Executive1_ID, Executive2_ID, Executive3_ID) " +
                        "VALUES(@name, @follow, @nextfollow, @followdetail, @exe1, @exe2, @exe3)";
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

        public int insert_sites(string sitename, string siteaddress, string sitephone, string siteemail, string sitestatus)
        {
            try
            {
                string query = "INSERT INTO sites (Site_Name, Email_Id, Phone, Address, Date, Status) " +
                    "VALUES(@name, @email, @mob, @addr, NOW(), @status)";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", sitename);
                    cmd.Parameters.AddWithValue("@email", siteemail);
                    cmd.Parameters.AddWithValue("@mob", sitephone);
                    cmd.Parameters.AddWithValue("@addr", siteaddress);
                    cmd.Parameters.AddWithValue("@status", sitestatus);

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
            string apploccu, string applbirth, string applage, string coapplname, string coapplpan, string coapplaadhar, string coapploccu, 
            string coapplbirth, string applstatus, string type = "insert", int id = 0)
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
                        " Co_Applicant_Name = @cname," +
                        " Co_Applicant_Pan_No = @cpan," +
                        " Co_Applicant_Adhar_No = @caadhar," +
                        " Co_Applicant_Occupation = @coccu," +
                        " Co_Applicant_DOB = @cbirth," +
                        " Status = @status where id=@id";
                }
                else
                {
                    query = "INSERT INTO applicant (Applicant_Name, Applicant_Email_Id, Applicant_Phone, Applicant_Address, Applicant_Pan_No, " +
                    "Applicant_Adhar_No, Applicant_Occupation, Applicant_DOB, Applicant_Age, Co_Applicant_Name, Co_Applicant_Pan_No, " +
                    "Co_Applicant_Adhar_No, Co_Applicant_Occupation, Co_Applicant_DOB, Date, Status) " +
                    "VALUES(@name, @email, @phone, @addr, @pan, @aadhar, @occu, @birth, @age, @cname, @cpan, @caadhar, @coccu, @cbirth, NOW(), @status)";
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

                    cmd.Parameters.AddWithValue("@cname", coapplname);
                    cmd.Parameters.AddWithValue("@cpan", coapplpan);
                    cmd.Parameters.AddWithValue("@caadhar", coapplaadhar);
                    cmd.Parameters.AddWithValue("@coccu", coapploccu);
                    cmd.Parameters.AddWithValue("@cbirth", coapplbirth);
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

        public int insert_booking(string bno, string breferred, string bincentive, string bincome, string bcancel, string btamount,
            string bramount, string bblder, string bparking, string bcharges, string bfollowup, string bstatus, string bremark, string bsite, string bflats, string bapplicant, string bexecutive, string bfranchies, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE bookings SET " +
                        "Booking_No = @bno," +
                        " Referenceby = @bref," +
                        " Incentive_Paid = @bince," +
                        " Total_Incentive = @bin," +
                        " Flat_Cancled_By = @bcan," +
                        " Total_Flat_Amount = @btamt," +
                        " Received_Amount = @bramt," +
                        " Total_Builder_Received = @bbldr," +
                        " Reserved_Parking = @bpark," +
                        " Internal_Charges = @bchrg," +
                        " Follow_Up_Date = @bflp," +
                        " Status = @bsts," +
                        " Remark = @bremark," +
                        " Site_Id = @bsite," +
                        " Applicant_Id = @bappl," +
                        " Executive_Id = @bexe," +
                        " Franchies_Id = @bfrn," +
                        " Flat_Id = @bflts where id=@id";
                }
                else
                {
                    query = "INSERT INTO bookings (Booking_No, Referenceby, Incentive_Paid, Total_Incentive, Flat_Cancled_By, Total_Flat_Amount, " +
                    "Received_Amount, Total_Builder_Received, Reserved_Parking, Internal_Charges, Follow_Up_Date, Date, Status, Remark, Site_Id," +
                    " Applicant_Id, Executive_Id, Franchies_Id, Flat_Id) " +
                    "VALUES(@bno, @bref, @bince, @bin, @bcan, @btamt, @bramt, @bbldr, @bpark, @bchrg, @bflp, NOW(), @bsts, " +
                    "@bremark, @bsite, @bappl, @bexe, @bfrn, @bflts)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@bno", bno);
                    cmd.Parameters.AddWithValue("@bref", breferred);
                    cmd.Parameters.AddWithValue("@bince", bincentive);
                    cmd.Parameters.AddWithValue("@bin", bincome);
                    cmd.Parameters.AddWithValue("@bcan", bcancel);
                    cmd.Parameters.AddWithValue("@btamt", btamount);
                    cmd.Parameters.AddWithValue("@bramt", bramount);
                    cmd.Parameters.AddWithValue("@bbldr", bblder);
                    cmd.Parameters.AddWithValue("@bpark", bparking);
                    cmd.Parameters.AddWithValue("@bchrg", bcharges);
                    cmd.Parameters.AddWithValue("@bflp", bfollowup);
                    cmd.Parameters.AddWithValue("@bsts", bstatus);
                    cmd.Parameters.AddWithValue("@bremark", bremark);
                    cmd.Parameters.AddWithValue("@bsite", bsite);
                    cmd.Parameters.AddWithValue("@bflts", bflats);
                    cmd.Parameters.AddWithValue("@bappl", bapplicant);
                    cmd.Parameters.AddWithValue("@bexe", bexecutive);
                    cmd.Parameters.AddWithValue("@bfrn", bfranchies);

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

        public int insert_paymentcommit(string ctype, string camount, string cstatus, string cremark, string bid, string type = "insert", int id = 0)
        {
            try
            {
                string query = "";
                if (type == "edit")
                {
                    query = "UPDATE payment_commitment SET " +
                        "Commitment_Type = @ctype," +
                        " Amount = @camt," +
                        " Status = @csts," +
                        " Remark = @crmrk," +
                        " Booking_Id = @bid where id=@id";
                }
                else
                {
                    query = "INSERT INTO payment_commitment (Commitment_Type, Amount, Date, Status, Remark, Booking_Id) " +
                    "VALUES(@ctype, @camt, NOW(), @csts, @crmrk, @bid)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ctype", ctype);
                    cmd.Parameters.AddWithValue("@camt", camount);
                    cmd.Parameters.AddWithValue("@csts", cstatus);
                    cmd.Parameters.AddWithValue("@crmrk", cremark);
                    cmd.Parameters.AddWithValue("@bid", Int32.Parse(bid));

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

        public int insert_paymentdetails(string pamt, string pdate, string pmode, string chkid, string chkdate, string bname,
            string ptype, string bldpay, string bnkpay, string sts, string bid, string type = "insert", int id = 0)
        {
            try
            {
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
                        " Booking_Id = @bid where id=@id";
                }
                else
                {
                    query = "INSERT INTO payment_details (Amount, Date, Payment_Mode, Cheque_Id, Cheque_Date, Bank_Name, " +
                    "Payment_Type, Builder_Pay, Bank_Pay, Status, Booking_Id) " +
                    "VALUES(@pamt, NOW(), @pmode, @chkid, @chkdate, @bname, @ptype, @bldpay, @bnkpay, @sts, @bid)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@pamt", pamt);
                    cmd.Parameters.AddWithValue("@pdate", pdate);
                    cmd.Parameters.AddWithValue("@pmode", pmode);
                    cmd.Parameters.AddWithValue("@chkid", chkid);
                    cmd.Parameters.AddWithValue("@chkdate", chkdate);
                    cmd.Parameters.AddWithValue("@bname", bname);
                    cmd.Parameters.AddWithValue("@ptype", ptype);
                    cmd.Parameters.AddWithValue("@bldpay", bldpay);
                    cmd.Parameters.AddWithValue("@bnkpay", bnkpay);
                    cmd.Parameters.AddWithValue("@sts", sts);
                    cmd.Parameters.AddWithValue("@bid", Int32.Parse(bid));

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

        public int insert_agreement(string ano, string adate, string anotary, string aamount, string aadjustment, string aextra, string astatus, string bid, string type = "insert", int id = 0)
        {
            try
            {
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
                        " Booking_Id = @bid where id=@id";
                }
                else
                {
                    query = "INSERT INTO aggrement (Aggrement_Amount, Aggrement_Date, Aggrement_No, Status, Booking_Id, Notary_Amount, Adjustment_Amount, Extra_Amount) " +
                    "VALUES(@aamount, @adate, @ano, @astatus, @bid, @notary, @adjust, @extra)";
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ano", ano);
                    cmd.Parameters.AddWithValue("@aamount", aamount);
                    cmd.Parameters.AddWithValue("@astatus", astatus);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@adate", adate);
                    cmd.Parameters.AddWithValue("@notary", anotary);
                    cmd.Parameters.AddWithValue("@adjust", aadjustment);
                    cmd.Parameters.AddWithValue("@extra", aextra);

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
            string filesta, string filesanctdate, string reqloanamt, string sanctloanamt, string disburseamt, string actloanamt, string recddamt, string remddamt, string rateofinter, string emiamt, string emimonths, string bookid, string finstat, string type = "insert", int id = 0)
        {
            try
            {
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
                        " Booking_Id = @bookid where id=@id";
                }
                else
                {
                    query = "INSERT INTO finance_details (Finance_Type, Finance_Name, Finance_Executive_Name, " +
                    "Finance_Executive_Mobile, Finance_Executive_Email, File_Handover_Date, File_Status, File_Sanction_Date, " +
                    "Required_Loan_Amount, Sanctioned_Loan_Amount, Total_Disbursed_Amount, Actual_Loan_Amount, Received_DD_Amount, " +
                    "Remaining_DD_Amount, Rate_Of_Interest, EMI_Amount, EMI_Total_Months, Status, Booking_Id) " +
                    "VALUES(@ftype, @fname, @fexename, @fexemob, @fexemail, @fhdate, @fsts, @fdate, @rlamt, @slamt, @dbrmnt," +
                    " @alamt, @ramt, @remamt, @rointr, @emiamt, @emimonths, @finstat, @bookid)";
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
                    cmd.Parameters.AddWithValue("@bookid", bookid);
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
                    " Cheque_Id, Cheque_Date, Bank_Name, Loan_fees, status, Finance_Id) " +
                    "VALUES(@chrg, @lfamt, @chid, @chdate, @bnknm, @lfee, @fstatus, @fid)";
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

        public int insert_cost_sheet(string sheet_type, string site, string type1, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyreg, string gst, string otheramt, string grandtotal, string type = "insert", int id = 0)
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
                        " Stamp_Duty_Registration = @stamp," +
                        " GST = @gst," +
                        " Other_Amount = @otheramt," +
                        " Grand_Total = @grandtotal," +
                        " Cost_Sheet_Type = @sheet_type," +
                        " Site_Id = @site_id where id=@id";
                }
                else
                {
                    query = "INSERT INTO cost_sheet (RR_Rate, Type, Basic_Rate, Basic_Cost, Legal_Charges, MSEB_Charges, " +
                    "Development_Charges, Stamp_Duty_Registration, GST, Other_Amount, Grand_Total, Cost_Sheet_Type, Site_Id, Area) " +
                    "VALUES(@rr_rate, @type, @bas_rate, @bas_cost, @legal_charge, @mseb, @devcharge, @stamp, @gst, @otheramt, @grandtotal, @sheet_type," +
                    " @site_id, @area)";
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
                    cmd.Parameters.AddWithValue("@devcharge", devcharge);
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
                string query = "";
                if (search == "")
                {
                    //query = "SELECT * FROM daily_enquiry ORDER BY ID DESC LIMIT @lim OFFSET @off";
                    query = "SELECT * FROM daily ORDER BY ID DESC LIMIT @lim OFFSET @off";
                    //query = "daily_enquiry_sp";
                }
                else
                {
                    query = "SELECT * FROM daily_enquiry where CONCAT(Customer_Name, Requirement) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 21; i++)
                {
                    list_enquiry_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@off", offset);
                    cmd.Parameters.AddWithValue("@lim", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 21; i++)
                        {
                            list_enquiry_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_enquiry_show;
                }
                else
                {
                    return list_enquiry_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_enquiry_show;
            }
        }

        /* Show Queries */
        public List<string>[] Daily_enquiry_report(DateTime enqStartDate, DateTime enqEndDate,
                                                string enqName, string enqSite,
                                                string enqRequirement,
                                                string enqVisit, string enqIneterest,
                                                string enqBudget, string enqDown,
                                                string enqMob)
        {
            try
            {              
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand("usp_daily_enquiry_report", connection);
                   
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@startDate", enqStartDate);
                    cmd.Parameters.AddWithValue("@endDate", enqEndDate);
                    cmd.Parameters.AddWithValue("@mobile", Convert.ToInt64(enqMob));
                    cmd.Parameters.AddWithValue("@custName", enqName);
                    cmd.Parameters.AddWithValue("@siteID", enqSite);
                    cmd.Parameters.AddWithValue("@requirement", enqRequirement);
                    cmd.Parameters.AddWithValue("@visit", enqVisit);
                    cmd.Parameters.AddWithValue("@interest", enqIneterest);
                    cmd.Parameters.AddWithValue("@budget", enqBudget);
                    cmd.Parameters.AddWithValue("@downPayment", enqDown);

                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    bool hasrows = dataReader.HasRows;

                    DataTable dt = new DataTable();
                    dt.Load(dataReader);

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 21; i++)
                        {
                            list_enquiry_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_enquiry_show;
                }
                else
                {
                    return list_enquiry_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_enquiry_show;
            }
        }

        public List<string>[] cost_sheet_show(string sheet_type, int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM cost_sheet where Cost_Sheet_Type = @sheet_type ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM cost_sheet where Cost_Sheet_Type = @sheet_type and CONCAT(Basic_Rate, Type) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 14; i++)
                {
                    list_cost_sheet_show[i] = new List<string>();
                }
                
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@sheet_type", sheet_type);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 14; i++)
                        {
                            list_cost_sheet_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_cost_sheet_show;
                }
                else
                {
                    return list_cost_sheet_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_cost_sheet_show;
            }
        }

        public List<string>[] customer_show(int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM applicant ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM applicant where CONCAT(Applicant_Name, Co_Applicant_Name) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 17; i++)
                {
                    list_customer_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 17; i++)
                        {
                            list_customer_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_customer_show;
                }
                else
                {
                    return list_customer_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_customer_show;
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
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM finance_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM finance_details where CONCAT(Finance_Name, Finance_Executive_Name) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 20; i++)
                {
                    list_finance_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            list_finance_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_finance_show;
                }
                else
                {
                    return list_finance_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_finance_show;
            }
        }

        public List<string>[] booking_show(int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM bookings ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM bookings where CONCAT(Booking_No, Referenceby) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 17; i++)
                {
                    list_booking_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 17; i++)
                        {
                            list_booking_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_booking_show;
                }
                else
                {
                    return list_booking_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_booking_show;
            }
        }

        public List<string>[] executive_show(int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM executive ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM executive where CONCAT(Executive_Name, Executive_Code) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 10; i++)
                {
                    list_executive_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            list_executive_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_executive_show;
                }
                else
                {
                    return list_executive_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_executive_show;
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
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM file_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM file_details where CONCAT(Cheque_Id, Bank_Name) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 9; i++)
                {
                    list_file_status_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            list_file_status_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_file_status_show;
                }
                else
                {
                    return list_file_status_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_file_status_show;
            }
        }

        public List<string>[] paycommit_show(int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM payment_commitment ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM payment_commitment where CONCAT(Commitment_Type, Amount) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 7; i++)
                {
                    list_paycommit_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            list_paycommit_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_paycommit_show;
                }
                else
                {
                    return list_paycommit_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_paycommit_show;
            }
        }

        public List<string>[] paydetails_show(int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM payment_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM payment_details where CONCAT(Cheque_Id, Payment_Type) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 12; i++)
                {
                    list_paydetails_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            list_paydetails_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_paydetails_show;
                }
                else
                {
                    return list_paydetails_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_paydetails_show;
            }
        }

        public List<string>[] agreement_show(int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM aggrement ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM aggrement where CONCAT(Aggrement_No, Aggrement_Amount) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 9; i++)
                {
                    list_agreement_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            list_agreement_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_agreement_show;
                }
                else
                {
                    return list_agreement_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_agreement_show;
            }
        }

        public List<string>[] franchies_show(int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM franchies ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM franchies where CONCAT(Francies_Name, Address) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 9; i++)
                {
                    list_franchies_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            list_franchies_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_franchies_show;
                }
                else
                {
                    return list_franchies_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_franchies_show;
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
        
        public List<string>[] sites_show()
        {
            try
            {
                string query = "SELECT * FROM sites ORDER BY ID DESC";

                for (int i = 0; i < 8; i++)
                {
                    list_sites_show[i] = new List<string>();
                }
                
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            list_sites_show[i].Add(dataReader[i] + "");
                        }                     
                    }                    
                    dataReader.Close();
                    this.CloseConnection();
                    return list_sites_show;
                }
                else
                {
                    return list_sites_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_sites_show;
            }
        }

        public List<string>[] flats_show(string site_name, int offset, int limit, string search = "")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM flats WHERE Site_Id = @site_id ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM flats WHERE Site_Id = @site_id and CONCAT(Status, Flat_No) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

                for (int i = 0; i < 9; i++)
                {
                    list_flats_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    int id = get_site_id_by_name(site_name);

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@site_id", id);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            list_flats_show[i].Add(dataReader[i] + "");
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list_flats_show;
                }
                else
                {
                    return list_flats_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_flats_show;
            }
        }

        /**
         * Show Daily customer name for page load
         */
        public List<string>[] daily_customer_name_show()
        {
            try
            {
                string query = "SELECT * FROM daily ORDER BY ID DESC";

                for (int i = 0; i < 21; i++)
                {
                   list_daily_customer_name_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 21; i++)
                        {
                            list_daily_customer_name_show[i].Add(dataReader[i] + "");
                        }
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
         * Show Daily wing name for page load
         */
        public List<string>[] wing_show_name(string site_id)
        {
            try
            {
                string query = "SELECT * FROM flats where Site_Id = '" + site_id + "'";

                for (int i = 0; i < 9; i++)
                {
                    list_wing_name_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            list_wing_name_show[i].Add(dataReader[i] + "");
                        }
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
                return list_daily_customer_name_show;
            }
        }

        /**
         * Show Daily flat no for page load
         */
        public List<string>[] flat_show_no(string wing_name, string site_id)
        {
            try
            {
                string query = "SELECT * FROM flats where Site_Id = '" + site_id + "' and wing = '" + wing_name + "'";

                for (int i = 0; i < 9; i++)
                {
                    list_flat_no_show[i] = new List<string>();
                }

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            list_flat_no_show[i].Add(dataReader[i] + "");
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
                return list_daily_customer_name_show;
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
        
        public int Delete_Record(string table, int id)
        {
            try
            {
                string query = "DELETE FROM " + table +" where ID="+id;
                
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
                        for(int i=0; i<count; i++)
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

                this.CloseConnection();
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