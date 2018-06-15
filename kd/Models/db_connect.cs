using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace kd.Models
{
    public class db_connect
    {
        private MySqlConnection connection;
        public List<string>[] list_feedback_show = new List<string>[3];
        public List<string>[] list_time_show = new List<string>[1];
        public List<string>[] list_gallery_show = new List<string>[2];
        public List<string>[] list_events_show = new List<string>[4];
        public List<string>[] list_bookings_show = new List<string>[14];
        public List<string>[] list_enquiry_show = new List<string>[14];
        public List<string>[] list_sites_show = new List<string>[11];
        public List<string>[] list_executive_show = new List<string>[10];
        public List<string>[] list_franchies_show = new List<string>[7];
        public List<string>[] list_customer_show = new List<string>[17];
        public List<string>[] list_paycommit_show = new List<string>[7];
        public List<string>[] list_paydetails_show = new List<string>[12];

        private bool OpenConnection()
        {
            string connetionString = null;
            connetionString = "server=182.50.133.77;database=kolhedeveloper;uid=kolheadmin;pwd=Kolhe@123;Allow User Variables=True;";
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

        public int insert_enquiry(string enqname, string enqaddress, string enqmob, string enqdate, string enqsite, string enqrequirement, string enqoccu, string enqvisit, string enqinterest,
            string enqbudget, string enqdown, string enqbooking, string enqremark)
        {
            try
            {
                string query = "INSERT INTO daily_enquiry (Enquiry_Date, Customer_Name, Mobile_No, Requirement, Down_Payment, Budget, Address," +
                    " Occupation, Visit, Interested, Booking_no, Remarks, Site_Id) " +
                    "VALUES(NOW(), @name, @mob, @req, @down_pay, @budget, @addr, @occu, @visit, @interested, @booking, @remark, @site_id)";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", enqname);
                    cmd.Parameters.AddWithValue("@mob", enqmob);
                    cmd.Parameters.AddWithValue("@req", enqrequirement);
                    cmd.Parameters.AddWithValue("@down_pay", enqdown);
                    cmd.Parameters.AddWithValue("@budget", enqbudget);
                    cmd.Parameters.AddWithValue("@addr", enqaddress);
                    cmd.Parameters.AddWithValue("@occu", enqoccu);
                    cmd.Parameters.AddWithValue("@visit", enqvisit);
                    cmd.Parameters.AddWithValue("@interested", enqinterest);
                    cmd.Parameters.AddWithValue("@booking", enqbooking);
                    cmd.Parameters.AddWithValue("@remark", enqremark);
                    cmd.Parameters.AddWithValue("@site_id", enqsite);

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
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
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public int insert_flats(string flatsitename, string flatwing, string flatfloor, string flatno, string flattype, string flatarea, string flatstatus)
        {
            try
            {
                string query = "INSERT INTO flats (Flat_No, Floor, Area, Flat_Type, Wing, Date, Status, Site_Id) " +
                    "VALUES(@flatno, @floor, @area, @flat_type, @wing, NOW(), @status, @siteid)";

                string query1 = "select id from sites where Site_Name = '" + flatsitename + "'";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd1 = new MySqlCommand(query1, connection);
                    int id = Convert.ToInt32(cmd1.ExecuteScalar());

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@flatno", flatno);
                    cmd.Parameters.AddWithValue("@floor", flatfloor);
                    cmd.Parameters.AddWithValue("@area", flatarea);
                    cmd.Parameters.AddWithValue("@flat_type", flattype);
                    cmd.Parameters.AddWithValue("@wing", flatwing);
                    cmd.Parameters.AddWithValue("@status", flatstatus);
                    cmd.Parameters.AddWithValue("@siteid", id);

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }        

        public int insert_executive(string exename, string execode, string exeemail, string exemob, string exeadd, string exejoin, string exebirth, string exestatus)
        {
            try
            {
                string query = "INSERT INTO executive (Executive_Name, Executive_Code, Email_Id, Phone, Address, Birth_Date, Joining_Date, Date, Status) " +
                    "VALUES(@name, @code, @email, @phone, @addr, @birth, @join, NOW(), @status)";

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
                    cmd.Parameters.AddWithValue("@status", 1); //To be changed

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }
                
        public int insert_franchies(string francname, string franccode, string francemail, string francmob, string francadd, string francjoin, string francstatus)
        {
            try
            {
                string query = "INSERT INTO franchies (Francies_Name, Email_Id, Phone, Address, Date, Status) " +
                    "VALUES(@name, @email, @phone, @addr, NOW(), @status)";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", francname);
                    //cmd.Parameters.AddWithValue("@code", franccode);
                    cmd.Parameters.AddWithValue("@email", francemail);
                    cmd.Parameters.AddWithValue("@phone", francmob);
                    cmd.Parameters.AddWithValue("@addr", francadd);
                    cmd.Parameters.AddWithValue("@join", francjoin);
                    cmd.Parameters.AddWithValue("@status", 1); //To be changed

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }        

        public int insert_applicant(string applname, string applemail, string applmob, string appladdr, string applpan, string applaadhar,
            string apploccu, string applbirth, string applage, string coapplname, string coapplpan, string coapplaadhar, string coapploccu, string coapplbirth)
        {
            try
            {
                string query = "INSERT INTO applicant (Applicant_Name, Applicant_Email_Id, Applicant_Phone, Applicant_Address, Applicant_Pan_No, " +
                    "Applicant_Adhar_No, Applicant_Occupation, Applicant_DOB, Applicant_Age, Co_Applicant_Name, Co_Applicant_Pan_No, " +
                    "Co_Applicant_Adhar_No, Co_Applicant_Occupation, Co_Applicant_DOB, Date, Status) " +
                    "VALUES(@name, @email, @phone, @addr, @pan, @aadhar, @occu, @birth, @age, @cname, @cpan, @caadhar, @coccu, @cbirth, NOW(), @status)";

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
                    cmd.Parameters.AddWithValue("@status", 1); //To be changed

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public int insert_booking(string bno, string breferred, string bincentive, string bincome, string bcancel, string btamount,
            string bramount, string bblder, string bparking, string bcharges, string bfollowup, string bstatus, string bremark, string psgst, string bflats, string bapplicant, string bexecutive, string bfranchies)
        {
            try
            {
                string query = "INSERT INTO bookings (Booking_No, Referenceby, Incentive_Paid, Total_Incentive, Flat_Cancled_By, Total_Flat_Amount, " +
                    "Received_Amount, Total_Builder_Received, Reserved_Parking, Internal_Charges, Follow_Up_Date, Date, Status, Remark, Site_Id," +
                    " Applicant_Id, Executive_Id, Franchies_Id, Flat_Id) " +
                    "VALUES(@bno, @bref, @bince, @bin, @bcan, @btamt, @bramt, @bbldr, @bpark, @bchrg, @bflp, NOW(), @bsts, @bremark, @bsgst, @bflts, @bappl, @bexe, @bfrn)";

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
                    cmd.Parameters.AddWithValue("@bsts", 1);//To be changed
                    cmd.Parameters.AddWithValue("@bremark", bremark);
                    cmd.Parameters.AddWithValue("@bsgst", 1);//To be changed
                    cmd.Parameters.AddWithValue("@bflts", 1);//To be changed
                    cmd.Parameters.AddWithValue("@bappl", 1);//To be changed
                    cmd.Parameters.AddWithValue("@bexe", 1);//To be changed
                    cmd.Parameters.AddWithValue("@bfrn", 1);//To be changed
                   

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public int insert_paymentcommit(string ctype, string camount, string cstatus, string cremark)
        {
            try
            {
                string query = "INSERT INTO payment_commitment (Commitment_Type, Amount, Date, Status, Remark, Booking_Id) " +
                    "VALUES(@ctype, @camt, NOW(), @csts, @crmrk, @bid)";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ctype", ctype);
                    cmd.Parameters.AddWithValue("@camt", camount);
                    cmd.Parameters.AddWithValue("@csts", 1); //to be changed
                    cmd.Parameters.AddWithValue("@crmrk", cremark);
                    cmd.Parameters.AddWithValue("@bid", 1); //to be changed
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public int insert_paymentdetails(string pamt, string pdate, string pmode, string chkid, string chkdate, string bname,
            string ptype, string bldpay, string bnkpay, string sts)
        {
            try
            {
                string query = "INSERT INTO payment_details (Amount, Date, Payment_Mode, Cheque_Id, Cheque_Date, Bank_Name, " +
                    "Payment_Type, Builder_Pay, Bank_Pay, Status, Booking_Id) " +
                    "VALUES(@pamt, NOW(), @pmode, @chkid, @chkdate, @bname, @ptype, @bldpay, @bnkpay, @sts, @bid)";

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
                    cmd.Parameters.AddWithValue("@sts", 1);
                    cmd.Parameters.AddWithValue("@bid", 1);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        /*public int insert_agreement(string pcode, string pname, string phsn, string pcgst, string psgst, string pigst, string prate)
        {
            try
            {
                string query = "INSERT INTO aggrement (Applicant_Name, Applicant_Email_Id, Applicant_Phone, Applicant_Address, Applicant_Pan_No, " +
                    "Applicant_Adhar_No, Applicant_Occupation, Applicant_DOB, Applicant_Age, Co_Applicant_Name, Co_Applicant_Pan_No, " +
                    "Co_Applicant_Adhar_No, Co_Applicant_Occupation, Co_Applicant_DOB, Date, Status) " +
                    "VALUES(@name, @email, @phone, @addr, @pan, @aadhar, @occu, @birth, @age, @cname, @cpan, @caadhar, @coccu, @cbirth, NOW(), @status)";

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
                    cmd.Parameters.AddWithValue("@status", 1); //To be changed

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }*/

        public int insert_finance(string fintype, string finname, string finexe, string finexemob, string finexeemail, string filehanddate,
            string filesta, string filesanctdate, string reqloanamt, string sanctloanamt, string disburseamt, string actloanamt, string recddamt, string remddamt, string rateofinter, string emiamt, string emimonths, string bookid, string finstat)
        {
            try
            {
                string query = "INSERT INTO finance_details (Finance_Type, Finance_Name, Finance_Executive_Name, " +
                    "Finance_Executive_Mobile, Finance_Executive_Email, File_Handover_Date, File_Status, File_Sanction_Date, " +
                    "Required_Loan_Amount, Sanctioned_Loan_Amount, Total_Disbursed_Amount, Actual_Loan_Amount, Received_DD_Amount, " +
                    "Remaining_DD_Amount, Rate_Of_Interest, EMI_Amount, EMI_Total_Months, Status, Booking_Id) " +
                    "VALUES(@ftype, @fname, @fexename, @fexemob, @fexemail, @fhdate, @fsts, @fdate, @rlamt, @slamt, @dbrmnt," +
                    " @alamt, @ramt, @remamt, @rointr, @emiamt, @emimonths, @finstat, @bookid)";

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
                    cmd.Parameters.AddWithValue("@finstat", 1);//To be changed

                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public int insert_filestatus(string chrg, string lfee, string chid, string chdate, string bnknm, string figst,
            string lfamt, string fid, string fstatus)
        {
            try
            {
                string query = "INSERT INTO file_details (Service_Charge, Loan_Fees_Amount," +
                    " Cheque_Id, Cheque_Date, Bank_Name, Loan_fees, status, Finance_Id) " +
                    "VALUES(@chrg, @lfamt, @chid, @chdate, @bnknm, @lfee, @fstatus, @fid)";

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
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public List<string>[] feedback_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM feedback ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_feedback_show[0] = new List<string>();
                list_feedback_show[1] = new List<string>();
                list_feedback_show[2] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_feedback_show[0].Add(dataReader["subject"] + "");
                        list_feedback_show[1].Add(dataReader["message"] + "");
                        list_feedback_show[2].Add(dataReader["date"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_feedback_show;
                }
                else
                {
                    return list_feedback_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_feedback_show;
            }
        }

        public List<string>[] time_slot_show(string date)
        {
            try
            {
                string query = "SELECT start_time FROM booking where date = @date";

                list_time_show[0] = new List<string>();
                //list_feedback_show[1] = new List<string>();
                //list_feedback_show[2] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@date", date);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_time_show[0].Add(dataReader["start_time"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_time_show;
                }
                else
                {
                    return list_time_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_time_show;
            }
        }

        /*News Section */
        public List<string>[] events_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM events ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_events_show[0] = new List<string>();
                list_events_show[1] = new List<string>();
                list_events_show[2] = new List<string>();
                list_events_show[3] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_events_show[0].Add(dataReader["Heading"] + "");
                        list_events_show[1].Add(dataReader["Description"] + "");
                        list_events_show[2].Add(dataReader["Date"] + "");
                        list_events_show[3].Add(dataReader["ID"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_events_show;
                }
                else
                {
                    return list_events_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_events_show;
            }
        }

        public List<string>[] bookings_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM booking_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_bookings_show[0] = new List<string>();
                list_bookings_show[1] = new List<string>();
                list_bookings_show[2] = new List<string>();
                list_bookings_show[3] = new List<string>();
                list_bookings_show[4] = new List<string>();
                list_bookings_show[5] = new List<string>();
                list_bookings_show[6] = new List<string>();
                list_bookings_show[7] = new List<string>();
                list_bookings_show[8] = new List<string>();
                list_bookings_show[9] = new List<string>();
                list_bookings_show[10] = new List<string>();
                list_bookings_show[11] = new List<string>();
                list_bookings_show[12] = new List<string>();
                list_bookings_show[13] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_bookings_show[0].Add(dataReader["ID"] + "");
                        list_bookings_show[1].Add(dataReader["transaction_id"] + "");
                        list_bookings_show[2].Add(dataReader["transaction_status"] + "");
                        list_bookings_show[3].Add(dataReader["transaction_date"] + "");
                        list_bookings_show[4].Add(dataReader["product_info"] + "");
                        list_bookings_show[5].Add(dataReader["name"] + "");
                        list_bookings_show[6].Add(dataReader["email"] + "");
                        list_bookings_show[7].Add(dataReader["phone"] + "");
                        list_bookings_show[8].Add(dataReader["booking_date"] + "");
                        list_bookings_show[9].Add(dataReader["adults"] + "");
                        list_bookings_show[10].Add(dataReader["children"] + "");
                        list_bookings_show[11].Add(dataReader["total_amount"] + "");
                        list_bookings_show[12].Add(dataReader["part_payment"] + "");
                        list_bookings_show[13].Add(dataReader["paid_amount"] + "");
                    }

                    dataReader.Close();
                    this.CloseConnection();
                    return list_bookings_show;
                }
                else
                {
                    return list_bookings_show;
                }
            }
            catch (MySqlException ex)
            {
                return list_bookings_show;
            }
        }

        public List<string>[] enquiry_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM daily_enquiry ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_enquiry_show[0] = new List<string>();
                list_enquiry_show[1] = new List<string>();
                list_enquiry_show[2] = new List<string>();
                list_enquiry_show[3] = new List<string>();
                list_enquiry_show[4] = new List<string>();
                list_enquiry_show[5] = new List<string>();
                list_enquiry_show[6] = new List<string>();
                list_enquiry_show[7] = new List<string>();
                list_enquiry_show[8] = new List<string>();
                list_enquiry_show[9] = new List<string>();
                list_enquiry_show[10] = new List<string>();
                list_enquiry_show[11] = new List<string>();
                list_enquiry_show[12] = new List<string>();
                list_enquiry_show[13] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_enquiry_show[0].Add(dataReader["ID"] + "");
                        list_enquiry_show[1].Add(dataReader["Enquiry_date"] + "");
                        list_enquiry_show[2].Add(dataReader["Customer_Name"] + "");
                        list_enquiry_show[3].Add(dataReader["Mobile_No"] + "");
                        list_enquiry_show[4].Add(dataReader["Requirement"] + "");
                        list_enquiry_show[5].Add(dataReader["Down_Payment"] + "");
                        list_enquiry_show[6].Add(dataReader["Budget"] + "");
                        list_enquiry_show[7].Add(dataReader["Address"] + "");
                        list_enquiry_show[8].Add(dataReader["Occupation"] + "");
                        list_enquiry_show[9].Add(dataReader["Visit"] + "");
                        list_enquiry_show[10].Add(dataReader["Interested"] + "");
                        list_enquiry_show[11].Add(dataReader["Booking_no"] + "");
                        list_enquiry_show[12].Add(dataReader["Remarks"] + "");
                        list_enquiry_show[13].Add(dataReader["Site_Id"] + "");
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

        public List<string>[] customer_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM applicant ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_customer_show[0] = new List<string>();
                list_customer_show[1] = new List<string>();
                list_customer_show[2] = new List<string>();
                list_customer_show[3] = new List<string>();
                list_customer_show[4] = new List<string>();
                list_customer_show[5] = new List<string>();
                list_customer_show[6] = new List<string>();
                list_customer_show[7] = new List<string>();
                list_customer_show[8] = new List<string>();
                list_customer_show[9] = new List<string>();
                list_customer_show[10] = new List<string>();
                list_customer_show[11] = new List<string>();
                list_customer_show[12] = new List<string>();
                list_customer_show[13] = new List<string>();
                list_customer_show[14] = new List<string>();
                list_customer_show[15] = new List<string>();
                list_customer_show[16] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_customer_show[0].Add(dataReader["ID"] + "");
                        list_customer_show[1].Add(dataReader["Applicant_Name"] + "");
                        list_customer_show[2].Add(dataReader["Applicant_Email_Id"] + "");
                        list_customer_show[3].Add(dataReader["Applicant_Phone"] + "");
                        list_customer_show[4].Add(dataReader["Applicant_Address"] + "");
                        list_customer_show[5].Add(dataReader["Applicant_Pan_No"] + "");
                        list_customer_show[6].Add(dataReader["Applicant_Adhar_No"] + "");
                        list_customer_show[7].Add(dataReader["Applicant_Occupation"] + "");
                        list_customer_show[8].Add(dataReader["Applicant_DOB"] + "");
                        list_customer_show[9].Add(dataReader["Applicant_Age"] + "");
                        list_customer_show[10].Add(dataReader["Co_Applicant_Name"] + "");
                        list_customer_show[11].Add(dataReader["Co_Applicant_Pan_No"] + "");
                        list_customer_show[12].Add(dataReader["Co_Applicant_Adhar_No"] + "");
                        list_customer_show[13].Add(dataReader["Co_Applicant_Occupation"] + "");
                        list_customer_show[14].Add(dataReader["Co_Applicant_DOB"] + "");
                        list_customer_show[15].Add(dataReader["Date"] + "");
                        list_customer_show[16].Add(dataReader["Status"] + "");
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

        public List<string>[] executive_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM executive ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_executive_show[0] = new List<string>();
                list_executive_show[1] = new List<string>();
                list_executive_show[2] = new List<string>();
                list_executive_show[3] = new List<string>();
                list_executive_show[4] = new List<string>();
                list_executive_show[5] = new List<string>();
                list_executive_show[6] = new List<string>();
                list_executive_show[7] = new List<string>();
                list_executive_show[8] = new List<string>();
                list_executive_show[9] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_executive_show[0].Add(dataReader["ID"] + "");
                        list_executive_show[1].Add(dataReader["Executive_Name"] + "");
                        list_executive_show[2].Add(dataReader["Executive_Code"] + "");
                        list_executive_show[3].Add(dataReader["Email_Id"] + "");
                        list_executive_show[4].Add(dataReader["Phone"] + "");
                        list_executive_show[5].Add(dataReader["Address"] + "");
                        list_executive_show[6].Add(dataReader["Birth_Date"] + "");
                        list_executive_show[7].Add(dataReader["Joining_Date"] + "");
                        list_executive_show[8].Add(dataReader["Date"] + "");
                        list_executive_show[9].Add(dataReader["Status"] + "");
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

        public List<string>[] paycommit_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM payment_commitment ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_paycommit_show[0] = new List<string>();
                list_paycommit_show[1] = new List<string>();
                list_paycommit_show[2] = new List<string>();
                list_paycommit_show[3] = new List<string>();
                list_paycommit_show[4] = new List<string>();
                list_paycommit_show[5] = new List<string>();
                list_paycommit_show[6] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_paycommit_show[0].Add(dataReader["ID"] + "");
                        list_paycommit_show[1].Add(dataReader["Commitment_Type"] + "");
                        list_paycommit_show[2].Add(dataReader["Amount"] + "");
                        list_paycommit_show[3].Add(dataReader["Date"] + "");
                        list_paycommit_show[4].Add(dataReader["Status"] + "");
                        list_paycommit_show[5].Add(dataReader["Remark"] + "");
                        list_paycommit_show[6].Add(dataReader["Booking_Id"] + "");
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

        public List<string>[] paydetails_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM payment_details ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_paydetails_show[0] = new List<string>();
                list_paydetails_show[1] = new List<string>();
                list_paydetails_show[2] = new List<string>();
                list_paydetails_show[3] = new List<string>();
                list_paydetails_show[4] = new List<string>();
                list_paydetails_show[5] = new List<string>();
                list_paydetails_show[6] = new List<string>();
                list_paydetails_show[7] = new List<string>();
                list_paydetails_show[8] = new List<string>();
                list_paydetails_show[9] = new List<string>();
                list_paydetails_show[10] = new List<string>();
                list_paydetails_show[11] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_paydetails_show[0].Add(dataReader["ID"] + "");
                        list_paydetails_show[1].Add(dataReader["Amount"] + "");
                        list_paydetails_show[2].Add(dataReader["Date"] + "");
                        list_paydetails_show[3].Add(dataReader["Payment_Mode"] + "");
                        list_paydetails_show[4].Add(dataReader["Cheque_Id"] + "");
                        list_paydetails_show[5].Add(dataReader["Cheque_Date"] + "");
                        list_paydetails_show[6].Add(dataReader["Bank_Name"] + "");
                        list_paydetails_show[7].Add(dataReader["Payment_Type"] + "");
                        list_paydetails_show[8].Add(dataReader["Builder_Pay"] + "");
                        list_paydetails_show[9].Add(dataReader["Bank_Pay"] + "");
                        list_paydetails_show[10].Add(dataReader["Status"] + "");
                        list_paydetails_show[11].Add(dataReader["Booking_Id"] + "");
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

        public List<string>[] franchies_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM franchies ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_franchies_show[0] = new List<string>();
                list_franchies_show[1] = new List<string>();
                list_franchies_show[2] = new List<string>();
                list_franchies_show[3] = new List<string>();
                list_franchies_show[4] = new List<string>();
                list_franchies_show[5] = new List<string>();
                list_franchies_show[6] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_franchies_show[0].Add(dataReader["ID"] + "");
                        list_franchies_show[1].Add(dataReader["Francies_Name"] + "");
                        list_franchies_show[2].Add(dataReader["Email_Id"] + "");
                        list_franchies_show[3].Add(dataReader["Phone"] + "");
                        list_franchies_show[4].Add(dataReader["Address"] + "");
                        list_franchies_show[5].Add(dataReader["Date"] + "");
                        list_franchies_show[6].Add(dataReader["Status"] + "");
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

        public List<string>[] sites_show(int offset, int limit)
        {
            try
            {
                string query = "SELECT * FROM sites a, flats b WHERE a.id = b.Site_Id ORDER BY ID DESC LIMIT @limit OFFSET @offset";

                list_enquiry_show[0] = new List<string>();
                list_enquiry_show[1] = new List<string>();
                list_enquiry_show[2] = new List<string>();
                list_enquiry_show[3] = new List<string>();
                list_enquiry_show[4] = new List<string>();
                list_enquiry_show[5] = new List<string>();
                list_enquiry_show[6] = new List<string>();
                list_enquiry_show[7] = new List<string>();
                list_enquiry_show[8] = new List<string>();
                list_enquiry_show[9] = new List<string>();
                list_enquiry_show[10] = new List<string>();



                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    MessageBox.Show("tesitng");
                    while (dataReader.Read())
                    {
                        MessageBox.Show(dataReader["Site_Name"].ToString().Trim());
                        list_sites_show[0].Add(dataReader["ID"] + "");
                        list_sites_show[1].Add(dataReader["Site_Name"] + "");
                        list_sites_show[2].Add(dataReader["Email_Id"] + "");
                        list_sites_show[3].Add(dataReader["Phone"] + "");
                        list_sites_show[4].Add(dataReader["Address"] + "");
                        list_sites_show[5].Add(dataReader["Date"] + "");
                        list_sites_show[6].Add(dataReader["Status"] + "");
                       /* list_enquiry_show[7].Add(dataReader["Address"] + "");
                        list_enquiry_show[8].Add(dataReader["Occupation"] + "");
                        list_enquiry_show[9].Add(dataReader["Visit"] + "");
                        list_enquiry_show[10].Add(dataReader["Interested"] + "");
                        list_enquiry_show[11].Add(dataReader["Booking_no"] + "");
                        list_enquiry_show[12].Add(dataReader["Remarks"] + "");
                        list_enquiry_show[13].Add(dataReader["Site_Id"] + "");*/
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

        public int booking_count()
        {
            try
            {
                int count = 0;
                string query = "select count(id) from booking_details";
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

        public int feedback_count()
        {
            try
            {
                int count = 0;
                string query = "select count(id) from feedback";
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

        /**/
        public int event_count()
        {
            try
            {
                int count = 0;
                string query = "select count(id) from pawna_camping.events";
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

        public int get_rates(string age_grp, string type)
        {
            try
            {
                int count = 0;
                string query = "select amount from pawna_camping.rates where age_group = \"" + age_grp + "\" and package_type=@type";
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@type", Int32.Parse(type));
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

        public int update_rates(string base_adult, string base_child, int type)
        {
            try
            {
                int id = -1;
                string query1 = "UPDATE rates SET amount=@base_adult WHERE age_group=\"adult\" and package_type=@type";
                string query2 = "UPDATE rates SET amount=@base_child WHERE age_group=\"child\" and package_type=@type1";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query1, connection);
                    cmd.Parameters.AddWithValue("@base_adult", Int32.Parse(base_adult));
                    cmd.Parameters.AddWithValue("@type", type);
                    MySqlCommand cmd1 = new MySqlCommand(query2, connection);
                    cmd1.Parameters.AddWithValue("@base_child", Int32.Parse(base_child));
                    cmd1.Parameters.AddWithValue("@type1", type);

                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();

                    this.CloseConnection();
                }
                return id;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public int Insert_Event(string heading, string description)
        {
            try
            {
                int id = -1;
                string query = "INSERT INTO events (Heading, Description, Date) VALUES(@heading, @description, NOW())";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@heading", heading);
                    cmd.Parameters.AddWithValue("@description", description);

                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                }
                return id;
            }
            catch (MySqlException ex)
            {
                return -1;
            }
        }

        public void Delete_Event(int id)
        {
            try
            {
                string query = "DELETE FROM events where ID = @id";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                }
            }
            catch (MySqlException ex)
            {

            }
        }
        /*News Section*/

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
        public Boolean Login(string name, string password)
        {
            try
            {
                var pass = password + "4804";
                var hash = getHash(pass);
                MySqlDataReader rdr;
                string query = "select password from login where username = @name and password = @password";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", hash);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        this.CloseConnection();
                        return true;
                    }
                }
                this.CloseConnection();
                return false;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
        /*Login Section*/

    } //db_connect class

} // namespace