using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsPlan
    {
        public int ID { get; set; }
        public string NAME { get; set; }
		public List<clsTask> TASKS { get; set; }
		
		public clsPlan(){
			TASKS = new List<clsTask>();
		}
    }
}