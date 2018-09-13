using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kd.Models
{
    public class DailyReport
    {
        public DailyFollowup _DailyFollowup { get; set; }
        public List<DailyVM> _DailyVM { get; set; }
    }
}