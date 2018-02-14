using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsScheduleItem
    {
        public clsScheduleItem()
        {
            TASKS = new List<clsTask>();
            IMAGES = new List<clsImage>();
        }
        public int ID { get; set; }

        public int SCHEDULE_ID { get; set; }

        

        public IList<clsTask> TASKS { get; set; }
        public IList<clsImage> IMAGES { get; set; } 


        public string SCHEDULE_NAME { get; set; }

        public int PLAN_TASK_ID { get; set; }

        public string PLAN_NAME { get; set; }

        public int PERIOD_TYPE_ID { get; set; }

        public string PERIOD_TYPE_NAME { get; set; }

        public string TIME_BEGIN { get; set; }

        public string DATE_BEGIN { get; set; }
    }
}