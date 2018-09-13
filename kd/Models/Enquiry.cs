using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kd.Models
{
    public class Enquiry
    {
        public DailyFollowup _DailyFollowup { get; set; }
        public List<DailyVM> _DailyVM { get; set; }
    }

    public class DailyFollowup
    {
        public int ID { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
        public string Mobile_No { get; set; }
        public string Second_Mobile_No { get; set; }
        public string Email_ID { get; set; }
        public string Requirement { get; set; }
        public string Occupation { get; set; }
        public Int32 Income { get; set; }
        public Double Budget { get; set; }
        public Double Down_Payment { get; set; }
        public string Visit { get; set; }
        public string Current_Status { get; set; }
        public string Source { get; set; }
        public string Source_Details { get; set; }
        public DateTime? Enquiry_Date { get; set; }
        public DateTime? folloup_Date { get; set; }
        public DateTime? Next_folloup_Date { get; set; }
        public string folloup_Details { get; set; }
        public Int32 Executive1_ID { get; set; }
        public Int32 Executive2_ID { get; set; }
        public Int32 Executive3_ID { get; set; }
    }
    public class DailyVM
    {
        public Int32 ID { get; set; }
        public string Customer_Name { get; set; }
        public Int32 Daily_Customer_ID { get; set; }
        public Int32 Site_ID { get; set; }
        public string Wing { get; set; }
        public string Flat { get; set; }
        public Int32 Executive1_ID { get; set; }
        public Int32 Executive2_ID { get; set; }
        public Int32 Executive3_ID { get; set; }
        public DateTime? Date { get; set; }
        public string Site_Name { get; set; }
        public Int32 Flat_No { get; set; }
        public string Executive1_Name { get; set; }
        public string Executive2_Name { get; set; }
        public string Executive3_Name { get; set; }
    }
}