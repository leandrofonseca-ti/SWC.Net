using System;
using System.Collections.Generic;
using System.Web;

namespace SWC.Data.Entity
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            MESSAGE = string.Empty;
        }
        public string MESSAGE { get; set; }
        public bool STATUS { get; set; }
        public object DATA { get; set; }
    }
}