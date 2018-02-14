using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsUser
    {
        public clsUser()
        {
            PHONES = new List<clsUserPhone>();
        }
        public int? USERID { get; set; }

        public string NAME { get; set; }

        public int PROFILEID { get; set; }

        public string PROFILENAME { get; set; }

        public string PASSWORD { get; set; }

        public string PHONE { get; set; }

        public string PICTURE { get; set; }

        public string EMAIL { get; set; }

        public string ADDRESS { get; set; }

        public List<clsUserPhone> PHONES { get; set; }

        public bool ACTIVE { get; set; }

        

        public DateTime CREATEDATE { get; set; }

        public string LASTNAME { get; set; }

        public string FULLNAME
        {
            get
            {
                if (!String.IsNullOrEmpty(LASTNAME))
                    return string.Format("{0} {1} ({2})", NAME, LASTNAME, EMAIL);
                else
                    return string.Format("{0} ({1})", NAME, EMAIL);
            }
        }
    }
}