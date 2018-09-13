using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kd.Models
{
    public class DailyFollowup
    {
        public int ID { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
    }
    public class DailyVM
    {
        public int ID { get; set; }
        public string Customer_Name { get; set; }
        public string Address { get; set; }
        public string Email_ID { get; set; }
        public string Requirement { get; set; }
    } 
}