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
        public List<string>[] list_executive_show_name = new List<string>[2];
        public List<string>[] list_franchies_show = new List<string>[7];
        public List<string>[] list_franchies_show_name = new List<string>[2];
        public List<string>[] list_customer_show = new List<string>[17];
        public List<string>[] list_customer_show_name= new List<string>[2];
        public List<string>[] list_paycommit_show = new List<string>[7];
        public List<string>[] list_paydetails_show = new List<string>[12];
        public List<string>[] list_flats_show = new List<string>[9];
        public List<string>[] list_booking_show = new List<string>[21];
        public List<string>[] list_finance_show = new List<string>[21];
        public List<string>[] list_file_status_show = new List<string>[10];
        public List<string>[] list_agreement_show = new List<string>[6];
        public List<string>[] list_cost_sheet_show = new List<string>[14];
        public List<string>[] list_customer_booking_show = new List<string>[2];

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

        public int insert_cost_sheet(string sheet_type, string site, string type, string area, string rr_rate, string basic_rate, string basic_cost, string legal_charge, string devcharge, string mseb, string stampdutyreg, string gst, string otheramt, string grandtotal)
        {
            try
            {
                string query = "INSERT INTO cost_sheet (RR_Rate, Type, Basic_Rate, Basic_Cost, Legal_Charges, MSEB_Charges, " +
                    "Development_Charges, Stamp_Duty_Registration, GST, Other_Amount, Grand_Total, Cost_Sheet_Type, Site_Id) " +
                    "VALUES(@rr_rate, @type, @bas_rate, @bas_cost, @legal_charge, @mseb, @devcharge, @stamp, @gst, @otheramt, @grandtotal, @sheet_type," +
                    " @site_id)";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@sheet_type", sheet_type);
                    cmd.Parameters.AddWithValue("@type", type);
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

        public int insert_flats(string flatsitename, string flatwing, string flatfloor, string flatno, string flattype, string flatarea, string flatstatus)
        {
            try
            {
                string query = "INSERT INTO flats (Flat_No, Floor, Area, Flat_Type, Wing, Date, Status, Site_Id) " +
                    "VALUES(@flatno, @floor, @area, @flat_type, @wing, NOW(), @status, @siteid)";

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
                    cmd.Parameters.AddWithValue("@status", exestatus); 

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
                    cmd.Parameters.AddWithValue("@status", francstatus); 

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
            string coapplbirth, string applstatus)
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
                    cmd.Parameters.AddWithValue("@status", applstatus);

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
            string bramount, string bblder, string bparking, string bcharges, string bfollowup, string bstatus, string bremark, string bsite, string bflats, string bapplicant, string bexecutive, string bfranchies)
        {
            try
            {
                string query = "INSERT INTO bookings (Booking_No, Referenceby, Incentive_Paid, Total_Incentive, Flat_Cancled_By, Total_Flat_Amount, " +
                    "Received_Amount, Total_Builder_Received, Reserved_Parking, Internal_Charges, Follow_Up_Date, Date, Status, Remark, Site_Id," +
                    " Applicant_Id, Executive_Id, Franchies_Id, Flat_Id) " +
                    "VALUES(@bno, @bref, @bince, @bin, @bcan, @btamt, @bramt, @bbldr, @bpark, @bchrg, @bflp, NOW(), @bsts, " +
                    "@bremark, @bsite, @bappl, @bexe, @bfrn, @bflts)";

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

        public int insert_paymentcommit(string ctype, string camount, string cstatus, string cremark, string bid)
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
                    cmd.Parameters.AddWithValue("@csts", cstatus);
                    cmd.Parameters.AddWithValue("@crmrk", cremark);
                    cmd.Parameters.AddWithValue("@bid", Int32.Parse(bid));
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
            string ptype, string bldpay, string bnkpay, string sts, string bid)
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
                    cmd.Parameters.AddWithValue("@sts", sts);
                    cmd.Parameters.AddWithValue("@bid", Int32.Parse(bid));
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

        public int insert_agreement(string ano, string adate, string anotary, string aamount, string aadjustment, string aextra, string astatus, string bid)
        {
            try
            {
                string query = "INSERT INTO aggrement (Aggrement_Amount, Aggrement_Date, Aggrement_No, Status, Booking_Id) " +
                    "VALUES(@aamount, @adate, @ano, @astatus, @bid)";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ano", ano);
                    cmd.Parameters.AddWithValue("@aamount", aamount);
                    cmd.Parameters.AddWithValue("@astatus", astatus);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@adate", adate);

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
                    cmd.Parameters.AddWithValue("@finstat", finstat);

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
                    return 1;
                }
                return 0;
            }
            catch (MySqlException ex)
            {
                return 0;
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

        public List<string>[] bookings_show(int offset, int limit, string search = "")
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

        public List<string>[] enquiry_show(int offset, int limit, string search="")
        {
            try
            {
                string query = "";
                if (search == "")
                {
                    query = "SELECT * FROM daily_enquiry ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }
                else
                {
                    query = "SELECT * FROM daily_enquiry where CONCAT(Customer_Name, Requirement) LIKE '%" + search + "%' ORDER BY ID DESC LIMIT @limit OFFSET @offset";
                }

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

                list_cost_sheet_show[0] = new List<string>();
                list_cost_sheet_show[1] = new List<string>();
                list_cost_sheet_show[2] = new List<string>();
                list_cost_sheet_show[3] = new List<string>();
                list_cost_sheet_show[4] = new List<string>();
                list_cost_sheet_show[5] = new List<string>();
                list_cost_sheet_show[6] = new List<string>();
                list_cost_sheet_show[7] = new List<string>();
                list_cost_sheet_show[8] = new List<string>();
                list_cost_sheet_show[9] = new List<string>();
                list_cost_sheet_show[10] = new List<string>();
                list_cost_sheet_show[11] = new List<string>();
                list_cost_sheet_show[12] = new List<string>();
                list_cost_sheet_show[13] = new List<string>();
                
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@sheet_type", sheet_type);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_cost_sheet_show[0].Add(dataReader["ID"] + "");
                        list_cost_sheet_show[1].Add(dataReader["RR_Rate"] + "");
                        list_cost_sheet_show[2].Add(dataReader["Type"] + "");
                        list_cost_sheet_show[3].Add(dataReader["Basic_Rate"] + "");
                        list_cost_sheet_show[4].Add(dataReader["Basic_Cost"] + "");
                        list_cost_sheet_show[5].Add(dataReader["Legal_Charges"] + "");
                        list_cost_sheet_show[6].Add(dataReader["MSEB_Charges"] + "");
                        list_cost_sheet_show[7].Add(dataReader["Development_Charges"] + "");
                        list_cost_sheet_show[8].Add(dataReader["Stamp_Duty_Registration"] + "");
                        list_cost_sheet_show[9].Add(dataReader["GST"] + "");
                        list_cost_sheet_show[10].Add(dataReader["Other_Amount"] + "");
                        list_cost_sheet_show[11].Add(dataReader["Grand_Total"] + "");
                        list_cost_sheet_show[12].Add(dataReader["Cost_Sheet_Type"] + "");
                        list_cost_sheet_show[13].Add(dataReader["Site_Id"] + "");
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

        /** 
         * Get the customer name
         */
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

                list_finance_show[0] = new List<string>();
                list_finance_show[1] = new List<string>();
                list_finance_show[2] = new List<string>();
                list_finance_show[3] = new List<string>();
                list_finance_show[4] = new List<string>();
                list_finance_show[5] = new List<string>();
                list_finance_show[6] = new List<string>();
                list_finance_show[7] = new List<string>();
                list_finance_show[8] = new List<string>();
                list_finance_show[9] = new List<string>();
                list_finance_show[10] = new List<string>();
                list_finance_show[11] = new List<string>();
                list_finance_show[12] = new List<string>();
                list_finance_show[13] = new List<string>();
                list_finance_show[14] = new List<string>();
                list_finance_show[15] = new List<string>();
                list_finance_show[16] = new List<string>();
                list_finance_show[17] = new List<string>();
                list_finance_show[18] = new List<string>();
                list_finance_show[19] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_finance_show[0].Add(dataReader["ID"] + "");
                        list_finance_show[1].Add(dataReader["Finance_Type"] + "");
                        list_finance_show[2].Add(dataReader["Finance_Name"] + "");
                        list_finance_show[3].Add(dataReader["Finance_Executive_Name"] + "");
                        list_finance_show[4].Add(dataReader["Finance_Executive_Mobile"] + "");
                        list_finance_show[5].Add(dataReader["Finance_Executive_Email"] + "");
                        list_finance_show[6].Add(dataReader["File_Handover_Date"] + "");
                        list_finance_show[7].Add(dataReader["File_Status"] + "");
                        list_finance_show[8].Add(dataReader["File_Sanction_Date"] + "");
                        list_finance_show[9].Add(dataReader["Required_Loan_amount"] + "");
                        list_finance_show[10].Add(dataReader["Sanctioned_Loan_Amount"] + "");
                        list_finance_show[11].Add(dataReader["Total_Disbursed_Amount"] + "");
                        list_finance_show[12].Add(dataReader["Actual_Loan_amount"] + "");
                        list_finance_show[13].Add(dataReader["Received_DD_Amount"] + "");
                        list_finance_show[14].Add(dataReader["Remaining_DD_Amount"] + "");
                        list_finance_show[15].Add(dataReader["Rate_Of_Interest"] + "");
                        list_finance_show[16].Add(dataReader["EMI_Amount"] + "");
                        list_finance_show[17].Add(dataReader["EMI_Total_Months"] + "");
                        list_finance_show[18].Add(dataReader["Status"] + "");
                        list_finance_show[19].Add(dataReader["Booking_Id"] + "");
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

                list_booking_show[0] = new List<string>();
                list_booking_show[1] = new List<string>();
                list_booking_show[2] = new List<string>();
                list_booking_show[3] = new List<string>();
                list_booking_show[4] = new List<string>();
                list_booking_show[5] = new List<string>();
                list_booking_show[6] = new List<string>();
                list_booking_show[7] = new List<string>();
                list_booking_show[8] = new List<string>();
                list_booking_show[9] = new List<string>();
                list_booking_show[10] = new List<string>();
                list_booking_show[11] = new List<string>();
                list_booking_show[12] = new List<string>();
                list_booking_show[13] = new List<string>();
                list_booking_show[14] = new List<string>();
                list_booking_show[15] = new List<string>();
                list_booking_show[16] = new List<string>();
                list_booking_show[17] = new List<string>();
                list_booking_show[18] = new List<string>();
                list_booking_show[19] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_booking_show[0].Add(dataReader["ID"] + "");
                        list_booking_show[1].Add(dataReader["Booking_No"] + "");
                        list_booking_show[2].Add(dataReader["Referenceby"] + "");
                        list_booking_show[3].Add(dataReader["Incentive_Paid"] + "");
                        list_booking_show[4].Add(dataReader["Total_Incentive"] + "");
                        list_booking_show[5].Add(dataReader["Flat_Cancled_By"] + "");
                        list_booking_show[6].Add(dataReader["Total_Flat_Amount"] + "");
                        list_booking_show[7].Add(dataReader["Received_Amount"] + "");
                        list_booking_show[8].Add(dataReader["Total_Builder_Received"] + "");
                        list_booking_show[9].Add(dataReader["Reserved_Parking"] + "");
                        list_booking_show[10].Add(dataReader["Internal_Charges"] + "");
                        list_booking_show[11].Add(dataReader["Follow_Up_Date"] + "");
                        list_booking_show[12].Add(dataReader["Date"] + "");
                        list_booking_show[13].Add(dataReader["Status"] + "");
                        list_booking_show[14].Add(dataReader["Remark"] + "");
                        list_booking_show[15].Add(dataReader["Site_Id"] + "");
                        list_booking_show[16].Add(dataReader["Applicant_Id"] + "");
                        list_booking_show[17].Add(dataReader["Executive_Id"] + "");
                        list_booking_show[18].Add(dataReader["Franchies_Id"] + "");
                        list_booking_show[19].Add(dataReader["Flat_Id"] + "");
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

        /**
         * Get the executive name
         */

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

                list_file_status_show[0] = new List<string>();
                list_file_status_show[1] = new List<string>();
                list_file_status_show[2] = new List<string>();
                list_file_status_show[3] = new List<string>();
                list_file_status_show[4] = new List<string>();
                list_file_status_show[5] = new List<string>();
                list_file_status_show[6] = new List<string>();
                list_file_status_show[7] = new List<string>();
                list_file_status_show[8] = new List<string>();


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_file_status_show[0].Add(dataReader["ID"] + "");
                        list_file_status_show[1].Add(dataReader["Service_Charge"] + "");
                        list_file_status_show[2].Add(dataReader["Loan_Fees_amount"] + "");
                        list_file_status_show[3].Add(dataReader["Cheque_Id"] + "");
                        list_file_status_show[4].Add(dataReader["Cheque_Date"] + "");
                        list_file_status_show[5].Add(dataReader["Bank_Name"] + "");
                        list_file_status_show[6].Add(dataReader["Loan_Fees"] + "");
                        list_file_status_show[7].Add(dataReader["Status"] + "");
                        list_file_status_show[8].Add(dataReader["Finance_Id"] + "");
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

                list_agreement_show[0] = new List<string>();
                list_agreement_show[1] = new List<string>();
                list_agreement_show[2] = new List<string>();
                list_agreement_show[3] = new List<string>();
                list_agreement_show[4] = new List<string>();
                list_agreement_show[5] = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@offset", offset);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_agreement_show[0].Add(dataReader["ID"] + "");
                        list_agreement_show[1].Add(dataReader["Aggrement_Amount"] + "");
                        list_agreement_show[2].Add(dataReader["Aggrement_Date"] + "");
                        list_agreement_show[3].Add(dataReader["Aggrement_No"] + "");
                        list_agreement_show[4].Add(dataReader["Status"] + "");
                        list_agreement_show[5].Add(dataReader["Booking_Id"] + "");
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

        /** 
         * Get Franchies name
         */

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

                list_sites_show[0] = new List<string>();
                list_sites_show[1] = new List<string>();
                list_sites_show[2] = new List<string>();
                list_sites_show[3] = new List<string>();
                list_sites_show[4] = new List<string>();
                list_sites_show[5] = new List<string>();
                list_sites_show[6] = new List<string>();
                list_sites_show[7] = new List<string>();
                list_sites_show[8] = new List<string>();
                list_sites_show[9] = new List<string>();
                list_sites_show[10] = new List<string>();
                
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list_sites_show[0].Add(dataReader["ID"] + "");
                        list_sites_show[1].Add(dataReader["Site_Name"] + "");
                        list_sites_show[2].Add(dataReader["Email_Id"] + "");
                        list_sites_show[3].Add(dataReader["Phone"] + "");
                        list_sites_show[4].Add(dataReader["Address"] + "");
                        list_sites_show[5].Add(dataReader["Date"] + "");
                        list_sites_show[6].Add(dataReader["Status"] + "");                      
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
                
                list_flats_show[0] = new List<string>();
                list_flats_show[1] = new List<string>();
                list_flats_show[2] = new List<string>();
                list_flats_show[3] = new List<string>();
                list_flats_show[4] = new List<string>();
                list_flats_show[5] = new List<string>();
                list_flats_show[6] = new List<string>();
                list_flats_show[7] = new List<string>();
                list_flats_show[8] = new List<string>();

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
                        list_flats_show[0].Add(dataReader["ID"] + "");
                        list_flats_show[1].Add(dataReader["Flat_No"] + "");
                        list_flats_show[2].Add(dataReader["Floor"] + "");
                        list_flats_show[3].Add(dataReader["Area"] + "");
                        list_flats_show[4].Add(dataReader["Flat_Type"] + "");
                        list_flats_show[5].Add(dataReader["Wing"] + "");
                        list_flats_show[6].Add(dataReader["Date"] + "");
                        list_flats_show[7].Add(dataReader["Status"] + "");
                        list_flats_show[8].Add(dataReader["Site_Id"] + "");
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
                    cmd.Parameters.AddWithValue("@password", password);
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