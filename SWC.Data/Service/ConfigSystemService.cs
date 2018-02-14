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
    public class ConfigSystemService : ProviderConnection, IConfigSystemRepository
    {
        #region PUBLIC METHODS

        public List<clsConfigSystem> List()
        {
            List<clsConfigSystem> listagem = new List<clsConfigSystem>();

            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQLPaged = new StringBuilder();
                    sbSQLPaged.AppendLine(" SELECT ID, NAME_KEY, NAME_VALUE, DESCRIPTION ");
                    sbSQLPaged.AppendLine(" FROM config_system ");



                    MySqlCommand dataProc = new MySqlCommand(sbSQLPaged.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        clsConfigSystem entidade = new clsConfigSystem();

                        entidade.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["NAME_KEY"] != DBNull.Value)
                            entidade.NAME_KEY = dr["NAME_KEY"].ToString();

                        if (dr["NAME_VALUE"] != DBNull.Value)
                            entidade.NAME_VALUE = dr["NAME_VALUE"].ToString();

                        if (dr["DESCRIPTION"] != DBNull.Value)
                            entidade.DESCRIPTION = dr["DESCRIPTION"].ToString();

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
        #endregion PUBLIC METHODS


    }
}
