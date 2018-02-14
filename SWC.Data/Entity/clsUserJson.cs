using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsUserJson
    {
        public int ID { get; set; }

        public string PHONE { get; set; }
        
        public string NAME { get; set; }
        public string OCCUPATION { get; set; }

        public string LASTNAME { get; set; }
        public int PROFILEID { get; set; }
    }
}