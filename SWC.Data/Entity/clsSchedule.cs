using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsSchedule
    {
        public clsSchedule()
        {
            ITEMS = new List<clsScheduleItem>();
        }
        public int ID { get; set; }

        public string NAME { get; set; }
        public string PERIOD_NAME { get; set; }
        public List<clsScheduleItem> ITEMS { get; set; }

        public bool ACTIVE { get; set; }

    }
}