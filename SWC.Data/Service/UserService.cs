using MySql.Data.MySqlClient;
using SWC.Data.Entity;
using SWC.Data.Helper;
using SWC.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWC.Data.Service
{
    public class UserService : ProviderConnection, IUserRepository
    {
        #region PUBLIC METHODS
       
        public clsUser ExistUserPhone(string Phone)
        {
            clsUser entity = new clsUser();
            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine("SELECT user.ID, user.name, user.profile_id, user.phone, user.email, profile.profile_name FROM user ");
                    sbSQL.AppendLine("join profile on profile.profile_id = user.profile_id ");
                    sbSQL.AppendLine("WHERE phone = @phone AND ACTIVE = 1 ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar));
                    dataProc.Parameters["@phone"].Value = Phone;


                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {
                        entity = new clsUser();
                        entity.USERID = Int32.Parse(dr["ID"].ToString());
                        entity.NAME = dr["name"].ToString();
                        entity.PROFILEID = Int32.Parse(dr["profile_id"].ToString());

                        if (dr["phone"] != DBNull.Value)
                            entity.PHONE = dr["phone"].ToString();

                        if (dr["email"] != DBNull.Value)
                            entity.EMAIL = dr["email"].ToString();

                        if (dr["profile_name"] != DBNull.Value)
                            entity.PROFILENAME = dr["profile_name"].ToString();

                        entity.ACTIVE = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return entity;
        }

        public clsUser ValidateUserPhone(string Phone, string Password)
        {
            clsUser entity = new clsUser();
            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine("SELECT user.ID, user.name, user.profile_id, user.phone, user.email, profile.profile_name FROM user ");
                    sbSQL.AppendLine("join profile on profile.profile_id = user.profile_id ");
                    sbSQL.AppendLine("WHERE phone = @phone AND password = @password AND ACTIVE = 1 ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar));
                    dataProc.Parameters["@phone"].Value = Phone;

                    dataProc.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar));
                    dataProc.Parameters["@password"].Value = MD5Hash.GerarHashMd5(Password);

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {
                        entity = new clsUser();
                        entity.USERID = Int32.Parse(dr["ID"].ToString());
                        entity.NAME = dr["name"].ToString();
                        entity.PROFILEID = Int32.Parse(dr["profile_id"].ToString());

                        if (dr["phone"] != DBNull.Value)
                            entity.PHONE = dr["phone"].ToString();

                        if (dr["email"] != DBNull.Value)
                            entity.EMAIL = dr["email"].ToString();

                        if (dr["profile_name"] != DBNull.Value)
                            entity.PROFILENAME = dr["profile_name"].ToString();

                        entity.ACTIVE = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return entity;
        }

        public clsUser ValidateUser(string Email, string Password)
        {
            clsUser entity = new clsUser();
            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine("SELECT * FROM user ");
                    sbSQL.AppendLine("WHERE email = @email AND password = @password AND ACTIVE = 1 ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar));
                    dataProc.Parameters["@email"].Value = Email;

                    dataProc.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar));
                    dataProc.Parameters["@password"].Value = MD5Hash.GerarHashMd5(Password);

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {
                        entity = new clsUser();
                        entity.USERID = Int32.Parse(dr["ID"].ToString());
                        entity.NAME = dr["name"].ToString();
                        entity.PROFILEID = Int32.Parse(dr["PROFILE_ID"].ToString());
                        entity.PASSWORD = dr["password"].ToString();
                        entity.EMAIL = dr["email"].ToString();
                        entity.ACTIVE = true;
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return entity;
        }

        public void UpdatePasswordMD5(int usuarioid, string password)
        {
            
            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.AppendLine("UPDATE USER SET PASSWORD = @PASSWORD ");
                    sbSQL.AppendLine("WHERE ID = @ID ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.Int32));
                    dataProc.Parameters["@ID"].Value = usuarioid;

                    dataProc.Parameters.Add(new MySqlParameter("@PASSWORD", MySqlDbType.VarChar));
                    dataProc.Parameters["@PASSWORD"].Value = MD5Hash.GerarHashMd5(password);

                    // executa comando.
                    dataProc.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public List<clsUser> List(out int pageTotal, int pageIndex, int pageSize, Dictionary<string, object> dicFilter)
        {
            List<clsUser> listagem = new List<clsUser>();
            pageTotal = 0;

            
            try
            {
                using (var connection = CreateConnection())
                {



                    StringBuilder clauseWhere = new StringBuilder();
                    if (dicFilter != null)
                        if (dicFilter.Count > 0)
                        {
                            foreach (var item in dicFilter)
                            {
                                if (clauseWhere.Length > 0)
                                {
                                    if (item.Value.GetType() == typeof(int))
                                        clauseWhere.AppendFormat(" AND {0} = {1}", item.Key, item.Value);

                                    if (item.Value.GetType() == typeof(string))
                                        clauseWhere.AppendFormat(" AND {0} like '%{1}%'", item.Key, item.Value);
                                }
                                else
                                {
                                    if (item.Value.GetType() == typeof(int))
                                        clauseWhere.AppendFormat(" {0} = {1}", item.Key, item.Value);

                                    if (item.Value.GetType() == typeof(string))
                                        clauseWhere.AppendFormat(" {0} like '%{1}%'", item.Key, item.Value);
                                }
                            }
                        }

                    StringBuilder sbSQLPaged = new StringBuilder();

                    sbSQLPaged.AppendLine(" select distinct id, name, password, email, active, profile.profile_id, profile.profile_name as perfilnome ");
                    sbSQLPaged.AppendLine(" from user left join profile on profile.profile_id = user.profile_id ");

                    if (clauseWhere.Length > 0)
                    {
                        sbSQLPaged.AppendFormat("WHERE {0} ", clauseWhere.ToString());
                    }
                    pageIndex = pageIndex - 1;
                    sbSQLPaged.AppendFormat(" ORDER BY Id ");
                    sbSQLPaged.AppendFormat(" LIMIT {0},{1} ", pageIndex * pageSize, pageSize);

                    MySqlCommand dataProc = new MySqlCommand(sbSQLPaged.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        clsUser entidade = new clsUser();
                        entidade.USERID = Int32.Parse(dr["Id"].ToString());
                        entidade.NAME = dr["Name"].ToString();

                        entidade.PASSWORD = dr["password"].ToString();
                        entidade.EMAIL = dr["Email"].ToString();
                        entidade.PROFILENAME = dr["PerfilNome"].ToString();

                        if (dr["Active"] != DBNull.Value)
                        {
                            entidade.ACTIVE = false;
                            switch (dr["Active"].ToString().ToLower())
                            {
                                case "1":
                                case "true":
                                    entidade.ACTIVE = true;
                                    break;

                            }
                        }
                        listagem.Add(entidade);
                    }



                    #region TOTAL

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine(" select count(*) as total from user left join profile on profile.profile_id = user.profile_id ");

                    if (clauseWhere.Length > 0)
                    {
                        sbSQL.AppendFormat(" WHERE {0} ", clauseWhere.ToString());
                    }

                    dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    // executa comando.
                    dr.Close();
                    dr = dataProc.ExecuteReader();
                    if (dr.Read())
                    {
                        pageTotal = Int32.Parse(dr["total"].ToString());
                    }

                    #endregion

                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            




            return listagem;
        }

        public List<clsUser> ListUsuario()
        {
            List<clsUser> listagem = new List<clsUser>();

            
            try
            {
                using (var connection = CreateConnection())
                {


                    StringBuilder sbSQLPaged = new StringBuilder();

                    sbSQLPaged.AppendLine(" SELECT distinct Id, Name, Lastname, Email  FROM USER ");
                    sbSQLPaged.AppendFormat(" WHERE Active = 1 ");
                    sbSQLPaged.AppendFormat(" ORDER BY Name ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQLPaged.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        clsUser entidade = new clsUser();
                        entidade.USERID = Int32.Parse(dr["Id"].ToString());
                        entidade.NAME = string.Format("{0} {1} ({2})", dr["Name"].ToString(), dr["Lastname"].ToString(), dr["Email"].ToString());
                        listagem.Add(entidade);
                    }



                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return listagem;
        }

        public int Remove(long[] ids)
        {
            int ret = 0;
            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    string strIds = "";
                    foreach (var item in ids)
                    {
                        strIds = strIds.Length == 0 ? item.ToString() : string.Concat(strIds, ",", item.ToString());
                    }
                    sbSQL.AppendLine("DELETE FROM USER WHERE ID in (" + strIds + ")");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    // executa comando.
                    ret = dataProc.ExecuteNonQuery();

                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return ret;
        }

        public clsUser Get(int ID)
        {
            clsUser entidade = new clsUser();
            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("select id, name, lastname, password, email, address, active, corretorimovel, profile_id from user ");
                    sbSQL.AppendLine("where id = @id ");


                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.Int32));
                    dataProc.Parameters["@ID"].Value = ID;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {


                        entidade = new clsUser();
                        entidade.USERID = Int32.Parse(dr["Id"].ToString());
                        entidade.NAME = dr["Name"].ToString();

                        if (dr["LastName"] != DBNull.Value)
                            entidade.LASTNAME = dr["LastName"].ToString();

                        entidade.PASSWORD = dr["Password"].ToString();


                        if (dr["PROFILE_ID"] != DBNull.Value)
                            entidade.PROFILEID = Int32.Parse(dr["PROFILE_ID"].ToString());

                        entidade.EMAIL = dr["Email"].ToString();

                        if (dr["Address"] != DBNull.Value)
                            entidade.ADDRESS = dr["Address"].ToString();

                        if (dr["Active"] != DBNull.Value)
                        {
                            entidade.ACTIVE = false;
                            switch (dr["Active"].ToString().ToLower())
                            {
                                case "1":
                                case "true":
                                    entidade.ACTIVE = true;
                                    break;

                            }
                        }

                        //if (dr["CorretorImovel"] != DBNull.Value)
                        //{
                        //    entidade.CorretorImovel = false;
                        //    switch (dr["CorretorImovel"].ToString().ToLower())
                        //    {
                        //        case "1":
                        //        case "true":
                        //            entidade.CorretorImovel = true;
                        //            break;

                        //    }
                        //} 
                        //entidade.DataCadastro = DateTime.Parse(dr["DataCadastro"].ToString());
                    }
                    dr.Close();
                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (entidade.USERID > 0)
                entidade.PHONES = GetPhones(entidade.USERID.Value);

            return entidade;
        }

        public List<clsUserPhone> GetPhones(int id)
        {
            List<clsUserPhone> entidades = new List<clsUserPhone>();

            
            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.Append(" select id, ddi, ddd, number, number_type, whatsapp from user_phone where id_user = @ID_USER ");
                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;
                    dataProc.Parameters.Add(new MySqlParameter("@ID_USER", MySqlDbType.Int32));
                    dataProc.Parameters["@ID_USER"].Value = id;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        clsUserPhone entidade = new clsUserPhone();
                        entidade.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["DDI"] != DBNull.Value)
                            entidade.DDI = dr["DDI"].ToString();

                        if (dr["DDD"] != DBNull.Value)
                            entidade.DDD = dr["DDD"].ToString();

                        if (dr["NUMBER"] != DBNull.Value)
                            entidade.NUMBER = dr["NUMBER"].ToString();

                        if (dr["NUMBER_TYPE"] != DBNull.Value)
                            entidade.NUMBER_TYPE = dr["NUMBER_TYPE"].ToString();

                        if (dr["WHATSAPP"] != DBNull.Value)
                        {
                            switch (dr["WHATSAPP"].ToString().ToLower())
                            {
                                case "1":
                                case "true":
                                    entidade.WHATSAPP = true;
                                    break;

                            }
                        }
                        entidades.Add(entidade);
                    }

                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return entidades;
        }

        public clsUser GetByEmail(string email)
        {
            clsUser entidade = new clsUser();
            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT ID, EMAIL, PASSWORD, NAME FROM USER ");
                    sbSQL.AppendLine("WHERE ACTIVE = 1 AND EMAIL = @Email  ");


                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar));
                    dataProc.Parameters["@Email"].Value = email;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {
                        entidade = new clsUser();
                        entidade.USERID = Int32.Parse(dr["ID"].ToString());
                        entidade.NAME = dr["NAME"].ToString();
                        entidade.EMAIL = dr["EMAIL"].ToString();
                        entidade.PASSWORD = dr["PASSWORD"].ToString();
                    }
                    dr.Close();
                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return entidade;
        }

        public clsUser Save(clsUser entidade, out string message)
        {

            var validacao = ValidacaoUser(entidade.USERID, entidade.EMAIL);

            if (validacao == 1) // Email ja existe
            {
                message = "E-mail já cadastrado";
                return entidade;
            }

            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    if (entidade.USERID > 0)
                    {
                        if (!String.IsNullOrEmpty(entidade.PASSWORD))
                        {
                            sbSQL.AppendLine(" UPDATE user SET PROFILE_ID = @PROFILE_ID, PASSWORD = @PASSWORD, NAME = @NAME, LASTNAME = @LASTNAME, EMAIL = @EMAIL, ADDRESS = @ADDRESS, ACTIVE = @ACTIVE, UPDATEDDATE = @UPDATEDDATE ");
                            sbSQL.AppendLine(" WHERE ID = @ID ");
                        }
                        else
                        {
                            sbSQL.AppendLine(" UPDATE user SET PROFILE_ID = @PROFILE_ID, NAME = @NAME, LASTNAME = @LASTNAME, EMAIL = @EMAIL, ADDRESS = @ADDRESS, ACTIVE = @ACTIVE, UPDATEDDATE = @UPDATEDDATE ");
                            sbSQL.AppendLine(" WHERE ID = @ID ");
                        }
                    }
                    else
                    {
                        sbSQL.AppendLine(" INSERT INTO user (PROFILE_ID, NAME, LASTNAME, EMAIL, ADDRESS, PASSWORD, ACTIVE, UPDATEDDATE, CREATEDDATE ) VALUES (@PROFILE_ID, @NAME, @LASTNAME, @EMAIL, @ADDRESS, @PASSWORD, @ACTIVE, @UPDATEDDATE, @CREATEDDATE ) "); // LASTNAME, GENDER, BIRTHDAY
                    }

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);


                    if (entidade.USERID > 0)
                    {

                        dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.Int32));
                        dataProc.Parameters["@ID"].Value = entidade.USERID;
                    }
                    else
                    {
                        dataProc.Parameters.Add(new MySqlParameter("@CREATEDDATE", MySqlDbType.DateTime));
                        dataProc.Parameters["@CREATEDDATE"].Value = Util.GetDateTimeNow();

                    }
                    dataProc.Parameters.Add(new MySqlParameter("@UPDATEDDATE", MySqlDbType.DateTime));
                    dataProc.Parameters["@UPDATEDDATE"].Value = Util.GetDateTimeNow();

                    dataProc.Parameters.Add(new MySqlParameter("@NAME", MySqlDbType.VarChar));
                    dataProc.Parameters["@NAME"].Value = entidade.NAME;

                    dataProc.Parameters.Add(new MySqlParameter("@LASTNAME", MySqlDbType.VarChar));
                    dataProc.Parameters["@LASTNAME"].Value = entidade.LASTNAME;

                    dataProc.Parameters.Add(new MySqlParameter("@ADDRESS", MySqlDbType.VarChar));
                    dataProc.Parameters["@ADDRESS"].Value = entidade.ADDRESS;

                    if (!String.IsNullOrEmpty(entidade.PASSWORD))
                    {
                        dataProc.Parameters.Add(new MySqlParameter("@PASSWORD", MySqlDbType.VarChar));
                        dataProc.Parameters["@PASSWORD"].Value = MD5Hash.GerarHashMd5(entidade.PASSWORD);
                    }


                    dataProc.Parameters.Add(new MySqlParameter("@PROFILE_ID", MySqlDbType.Int32));
                    dataProc.Parameters["@PROFILE_ID"].Value = entidade.PROFILEID;

                    dataProc.Parameters.Add(new MySqlParameter("@EMAIL", MySqlDbType.VarChar));
                    dataProc.Parameters["@EMAIL"].Value = entidade.EMAIL;


                    dataProc.Parameters.Add(new MySqlParameter("@ACTIVE", MySqlDbType.Bit));
                    dataProc.Parameters["@ACTIVE"].Value = entidade.ACTIVE ? "1" : "0";

                    //dataProc.Parameters.Add(new MySqlParameter("@CORRETORIMOVEL", MySqlDbType.Bit));
                    //dataProc.Parameters["@CORRETORIMOVEL"].Value = entidade.CorretorImovel @ "1" : "0";

                    // executa comando.
                    dataProc.ExecuteNonQuery();
                    dataProc.Dispose();


                    if (entidade.USERID == 0)
                    {
                        dataProc = new MySqlCommand("SELECT LAST_INSERT_ID() AS ID", connection);
                        IDataReader obj = dataProc.ExecuteReader();
                        if (obj.Read())
                        {
                            entidade.USERID = Int32.Parse(obj["ID"].ToString());
                        }

                        obj.Close();
                        dataProc.Dispose();
                    }

                }
                message = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            if (entidade.USERID.HasValue)
                SaveUserPhones(entidade.USERID.Value, entidade.PHONES);

            return entidade;
        }

        #endregion PUBLIC METHODS

        #region PRIVATE METHODS

        private int ValidacaoUser(int? id, string email)
        {
            int result = 0;
            
            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.Append(" select  ");
                    sbSQL.Append(" IF(email = @EMAIL, 1, 0) as ExistEmail ");
                    sbSQL.Append(" from user ");
                    sbSQL.Append(" where (email = @EMAIL) ");
                    if (id.HasValue)
                    {
                        sbSQL.Append(" and ID <> @ID ");
                    }


                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@EMAIL", MySqlDbType.VarChar));
                    dataProc.Parameters["@EMAIL"].Value = email;

                    if (id.HasValue)
                    {
                        dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.Int32));
                        dataProc.Parameters["@ID"].Value = id.Value;
                    }

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read() && dr["ExistEmail"] != DBNull.Value)
                    {
                        result = Int32.Parse(dr["ExistEmail"].ToString());
                    }

                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                // new Helper().EventLog(ex);
                throw ex;
            }
            
            return result;
        }

        public int ValidateLoginEmail(string id, string email)
        {
            int result = 0;
            
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();

                    int usuarioid = 0;
                    Int32.TryParse(id, out usuarioid);
                    if (usuarioid > 0)
                    {
                        sbSQL.AppendLine(" select 1 as validate from user where email = @Email and ID <> @ID ");

                    }
                    else
                    {
                        sbSQL.AppendLine(" select 1 as validate from user where email = @Email ");
                    }


                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar));
                    dataProc.Parameters["@Email"].Value = email;


                    //dataProc.Parameters.Add(new MySqlParameter("@EmpresaId", MySqlDbType.Int32));
                    //dataProc.Parameters["@EmpresaId"].Value = empresaid;

                    if (usuarioid > 0)
                    {
                        dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.Int32));
                        dataProc.Parameters["@ID"].Value = usuarioid;
                    }


                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {

                        result = Int32.Parse(dr["validate"].ToString());
                    }
                    dr.Close();
                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return result;
        }

        private void SaveUserPhones(int id, List<clsUserPhone> phones)
        {

            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.Append(" DELETE FROM user_phone WHERE ID_USER = @ID_USER ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;
                    dataProc.Parameters.Add(new MySqlParameter("@ID_USER", MySqlDbType.Int32));
                    dataProc.Parameters["@ID_USER"].Value = id;

                    // executa comando.
                    dataProc.ExecuteNonQuery();


                    foreach (var item in phones)
                    {
                        sbSQL = new StringBuilder();
                        sbSQL.Append(" INSERT INTO user_phone ( ID_USER, DDI, DDD, NUMBER, NUMBER_TYPE, WHATSAPP ) VALUES (@ID_USER, @DDI, @DDD, @NUMBER, @NUMBER_TYPE, @WHATSAPP  ) ");
                        using (dataProc = new MySqlCommand(sbSQL.ToString(), connection))
                        {

                            dataProc.CommandType = CommandType.Text;

                            dataProc.Parameters.Add(new MySqlParameter("@ID_USER", MySqlDbType.Int32));
                            dataProc.Parameters["@ID_USER"].Value = id;


                            dataProc.Parameters.Add(new MySqlParameter("@DDI", MySqlDbType.VarChar));
                            dataProc.Parameters["@DDI"].Value = item.DDI;

                            dataProc.Parameters.Add(new MySqlParameter("@DDD", MySqlDbType.VarChar));
                            dataProc.Parameters["@DDD"].Value = item.DDD;

                            dataProc.Parameters.Add(new MySqlParameter("@NUMBER", MySqlDbType.VarChar));
                            dataProc.Parameters["@NUMBER"].Value = item.NUMBER;


                            dataProc.Parameters.Add(new MySqlParameter("@NUMBER_TYPE", MySqlDbType.VarChar));
                            dataProc.Parameters["@NUMBER_TYPE"].Value = item.NUMBER_TYPE;


                            dataProc.Parameters.Add(new MySqlParameter("@WHATSAPP", MySqlDbType.Bit));
                            dataProc.Parameters["@WHATSAPP"].Value = item.WHATSAPP;

                            // executa comando.
                            dataProc.ExecuteNonQuery();


                        }
                    }


                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion PRIVATE METHODS

    }
}
