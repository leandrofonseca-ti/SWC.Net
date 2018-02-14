using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;

namespace SWC.Data.Helper
{
    public class TemplateEmail
    {

        public static bool SendEmailPadrao(string body, string subject, string emailTo)
        {

            var listSettings = new Util().GetDefaultDefaultSettings();

            var _templatePadrao = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("Email_TemplateBase_Default", StringComparison.InvariantCultureIgnoreCase));

            string _to = string.Format("{0} <{1}>", "swc.com", emailTo);
   
            string readContents = _templatePadrao.DESCRIPTION;

            readContents = readContents.Replace("##TEXT##", body);


            return SendEmail(subject, _to, readContents, listSettings);

        }


        private static bool SendEmail(string subject, string to, string body, clsConfigSystem[] listSettings)
        {
            bool result = false;
            try
            {

                var _EmailFromGeneric = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("EmailFrom_Generic", StringComparison.InvariantCultureIgnoreCase));

                var _Email_UserName = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("Email_UserName", StringComparison.InvariantCultureIgnoreCase));

                var _Email_Password = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("Email_Password", StringComparison.InvariantCultureIgnoreCase));

                var _Email_Host = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("Email_Host", StringComparison.InvariantCultureIgnoreCase));

                var _Email_Port = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("Email_Port", StringComparison.InvariantCultureIgnoreCase));

                if (_EmailFromGeneric != null &&
                    _Email_UserName != null &&
                    _Email_Password != null &&
                        _Email_Host != null &&
                        _Email_Port != null)
                {
                    MailMessage msg = new MailMessage();
                    msg.Subject = subject;
                    msg.From = new MailAddress(_EmailFromGeneric.NAME_VALUE, "SWC");
                    msg.Body = body;
                    msg.IsBodyHtml = true;

                    string[] emailsTo = to.Split(';');
                    foreach (var item in emailsTo)
                    {
                        msg.To.Add(new MailAddress(item));
                    }
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = _Email_Host.NAME_VALUE;
                    smtp.Port = Int32.Parse(_Email_Port.NAME_VALUE);
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    NetworkCredential nc = new NetworkCredential(_Email_UserName.NAME_VALUE, _Email_Password.NAME_VALUE);
                    smtp.Credentials = nc;
                    smtp.Send(msg);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}