using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class clsTask
    {
        public int ID { get; set; }
        public int QTY { get; set; }
        public string NAME { get; set; }
        public float PRICE { get; set; }
        public string FULL { get; set; }
        public string DESCRIPTION { get; set; }
        public bool EXTRA { get; set; }
    }
}