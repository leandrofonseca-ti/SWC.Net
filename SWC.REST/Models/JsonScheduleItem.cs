using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWC.REST.Models
{
    public class JsonScheduleItem
    {
        public int ID { get; set; }
        public int CUSTOMERID { get; set; }
        public int PERIODTYPEID { get; set; }

        public string TIME  { get; set; }
        public string DATE { get; set; }
    }
}