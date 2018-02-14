using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsScheduleItemTask
    {
        public clsScheduleItemTask()
        {
        }
        public int ID { get; set; }
        public int TASK_ID { get; set; }
        public float PRICE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string TASK_DESC { get; set; }


    }
}