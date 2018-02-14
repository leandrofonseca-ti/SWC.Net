using SWC.Data.Entity;
using System.Collections.Generic;

namespace SWC.Data.Interface
{
    public interface IUserRepository
    {
        clsUser ExistUserPhone(string Phone);
        clsUser ValidateUserPhone(string Phone, string Password);

        clsUser ValidateUser(string Email, string Password);

        void UpdatePasswordMD5(int usuarioid, string password);

        List<clsUser> List(out int pageTotal, int pageIndex, int pageSize, Dictionary<string, object> dicFilter);

        List<clsUser> ListUsuario();

        int Remove(long[] ids);

        clsUser Get(int ID);

        List<clsUserPhone> GetPhones(int id);

        clsUser GetByEmail(string email);

        clsUser Save(clsUser entidade, out string message);

    }
}
