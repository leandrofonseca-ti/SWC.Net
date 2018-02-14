using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsUserPhone
    {
        public int? ID { get; set; }

        public int ID_USER { get; set; }

        public string NUMBER { get; set; }

        public string DDD { get; set; }

        public string DDI { get; set; }

        public string NUMBER_TYPE { get; set; }

        public bool WHATSAPP { get; set; }
        
    }
}