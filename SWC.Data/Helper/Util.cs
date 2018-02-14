using SWC.Data.Interface;
using SWC.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWC.Data.Helper
{
    public class Util
    {
        public static DateTime GetDateTimeNow()
        {
            // Listando os fusos horários existentes (apenas para observar os valores na collection)
            System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> collection = TimeZoneInfo.GetSystemTimeZones();
            // Mesmo estando o servidor configurado para qualquer fuso horário, o código abaixo obtém o horário de Brasília
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"); // Brasilia/BRA
            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);
        }


        public bool SendMailEsqueciSenha(int usuarioid, string nome, string email)
        {
            try
            {
                ICryptographyRepository _serviceCryptographyRepository = new Cryptography();
                var listSettings = GetDefaultDefaultSettings();
                var _EmailEsqueciSenha = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("EmailSubject_EsqueciSenha", StringComparison.InvariantCultureIgnoreCase));
                var _Domain_Admin = listSettings.SingleOrDefault(x => x.NAME_KEY.Equals("Domain_Service", StringComparison.InvariantCultureIgnoreCase));
                var Body = string.Format("Hello <strong>{0}</strong>, <br/><br/> To reset your password, click the link below. <br/><br/> {1}", nome, string.Format("{0}NewPassword.aspx?uid={1}", _Domain_Admin.NAME_VALUE, _serviceCryptographyRepository.Encrypt(usuarioid.ToString())));
                TemplateEmail.SendEmailPadrao(Body, _EmailEsqueciSenha.NAME_VALUE, email);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public clsConfigSystem[] GetDefaultDefaultSettings()
        {
            IConfigSystemRepository _serviceConfigSystemRepository = new ConfigSystemService();
            return _serviceConfigSystemRepository.List().ToArray();

        }

    }
}
