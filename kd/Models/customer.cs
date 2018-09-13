using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kd.Models
{
    public class customer
    {
        public DailyFollowup _DailyFollowup { get; set; }
        public List<DailyVM> _DailyVM { get; set; }
    }

    public class DailyFollowup1
    {
        public int ID { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
    }
    public class DailyVM1
    {
        public int ID { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
        public string Email_ID { get; set; }
        public string Requirement { get; set; }
    }
}