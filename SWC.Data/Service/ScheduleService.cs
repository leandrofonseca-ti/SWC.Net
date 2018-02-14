using MySql.Data.MySqlClient;
using SWC.Data.Entity;
using SWC.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SWC.Data.Service
{
    public class ScheduleService : ProviderConnection, IScheduleRepository
    {
        #region PUBLIC METHODS


        public List<clsPlan> LoadPlan()
        {
            List<clsPlan> entities = new List<clsPlan>();
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine(" select T.* from plan T ");
                    sbSQL.AppendLine(" where T.ACTIVE = 1 ORDER BY T.NAME ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        clsPlan entidade = new clsPlan();

                        entidade.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["NAME"] != DBNull.Value)
                            entidade.NAME = dr["NAME"].ToString();


                        entities.Add(entidade);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].TASKS = LoadNormalTasks(entities[i].ID);
            }
            return entities;
        }

        private List<clsTask> LoadNormalTasks(int planid)
        {
            List<clsTask> entities = new List<clsTask>();
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine(" select task.* from task ");
                    sbSQL.AppendLine(" join plan_task on plan_task.TASK_ID = task.ID ");
                    sbSQL.AppendLine(" where task.EXTRA = 0 and plan_task.PLAN_ID = " + planid.ToString());

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;


                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {

                        clsTask entidade = new clsTask();

                        entidade.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["NAME"] != DBNull.Value)
                            entidade.NAME = dr["NAME"].ToString();

                        if (dr["PRICE"] != DBNull.Value)
                            entidade.PRICE = float.Parse(dr["PRICE"].ToString());

                        if (dr["DESCRIPTION"] != DBNull.Value)
                            entidade.DESCRIPTION = dr["DESCRIPTION"].ToString();

                        if (dr["NAME"] != DBNull.Value && dr["PRICE"] != DBNull.Value)
                        {
                            entidade.FULL = string.Format("{0} ${1}", entidade.NAME, dr["PRICE"].ToString());

                            if (dr["DESCRIPTION"] != DBNull.Value)
                                entidade.FULL = string.Concat(entidade.FULL, " ", dr["DESCRIPTION"].ToString());
                        }

                        entidade.QTY = 1;
                        if (dr["EXTRA"] != DBNull.Value)
                        {
                            entidade.EXTRA = false;
                            switch (dr["EXTRA"].ToString().ToLower())
                            {
                                case "1":
                                case "true":
                                    entidade.EXTRA = true;
                                    break;

                            }
                        }

                        entities.Add(entidade);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entities;
        }

        public List<clsTask> LoadExtraTasks()
        {
            List<clsTask> entities = new List<clsTask>();
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine(" select * from task ");
                    sbSQL.AppendLine(" where EXTRA = 1");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;


                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {

                        clsTask entidade = new clsTask();

                        entidade.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["NAME"] != DBNull.Value)
                            entidade.NAME = dr["NAME"].ToString();

                        if (dr["PRICE"] != DBNull.Value)
                            entidade.PRICE = float.Parse(dr["PRICE"].ToString());


                        if (dr["DESCRIPTION"] != DBNull.Value)
                            entidade.DESCRIPTION = dr["DESCRIPTION"].ToString();



                        if (dr["NAME"] != DBNull.Value && dr["PRICE"] != DBNull.Value)
                        {
                            entidade.FULL = string.Format("{0} ${1}", entidade.NAME, dr["PRICE"].ToString());

                            if (dr["DESCRIPTION"] != DBNull.Value)
                                entidade.FULL = string.Concat(entidade.FULL, " ", dr["DESCRIPTION"].ToString());
                        }


                        if (dr["EXTRA"] != DBNull.Value)
                        {
                            entidade.EXTRA = false;
                            switch (dr["EXTRA"].ToString().ToLower())
                            {
                                case "1":
                                case "true":
                                    entidade.EXTRA = true;
                                    break;

                            }
                        }

                        entities.Add(entidade);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entities;
        }


        public bool SaveScheduleItem(int id, int userid, int periodtypeid, string time, string date)
        {
            /*
            float total = 0;
           Int32.Parse(id), Int32.Parse(customerid), periodtype, time, date, plantaskid, description

            int ret = 0;
            MySqlConnection connection = new MySqlConnection();
            try
            {
                connection = AbrirBanco();

                StringBuilder sbSQL = new StringBuilder();

                sbSQL.AppendLine("INSERT INTO schedule_payment (SCHEDULE_ITEM_ID, DESCRIPTION, STAFF_NAME, TOTAL, CREATED, DT_PAYMENT, STATUS_ID) VALUES (?SCHEDULE_ITEM_ID, ?DESCRIPTION, ?STAFF_NAME, ?TOTAL, NOW(), ?DT_PAYMENT, ?STATUS_ID);");

                sbSQL.AppendFormat("UPDATE schedule_item_response SET STATUS_ID = 1, DESCRIPTION = ?DESCRIPTION  WHERE STATUS_ID = 0 AND SCHEDULE_ITEM_ID = {0} AND STAFF_ID = {1}; ", scheduleitemid, userid);

                MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                dataProc.CommandType = CommandType.Text;

                dataProc.Parameters.Add(new MySqlParameter("?DESCRIPTION", MySqlDbType.VarChar));
                dataProc.Parameters["?DESCRIPTION"].Value = description;

                // executa comando.
                ret = dataProc.ExecuteNonQuery();

                dataProc.Dispose();
                connection.Close();

            }
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {
                FecharBanco(connection);
            }
            */
            return true;
        }


        public IList<clsTask> LoadTasksByPlan(int id)
        {
            IList<clsTask> entities = new List<clsTask>();
            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine(" select T.* from task T ");
                    sbSQL.AppendLine(" join plan_task PT on PT.TASK_ID = T.ID ");
                    sbSQL.AppendLine(" where PT.PLAN_ID = @ID ");

                    var dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;
                    dataProc.CommandText = sbSQL.ToString();

                    dataProc.Parameters.Add(NewParameter(dataProc, "@ID", id));


                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        clsTask entidade = new clsTask();

                        entidade.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["NAME"] != DBNull.Value)
                            entidade.NAME = dr["NAME"].ToString();

                        if (dr["PRICE"] != DBNull.Value)
                            entidade.PRICE = float.Parse(dr["PRICE"].ToString());


                        if (dr["DESCRIPTION"] != DBNull.Value)
                            entidade.DESCRIPTION = dr["DESCRIPTION"].ToString();



                        if (dr["NAME"] != DBNull.Value && dr["PRICE"] != DBNull.Value)
                        {
                            entidade.FULL = string.Format("{0} ${1}", entidade.NAME, dr["PRICE"].ToString());

                            if (dr["DESCRIPTION"] != DBNull.Value)
                                entidade.FULL = string.Concat(entidade.FULL, " ", dr["DESCRIPTION"].ToString());
                        }

                        entidade.QTY = 1;
                        if (dr["EXTRA"] != DBNull.Value)
                        {
                            entidade.EXTRA = false;
                            switch (dr["EXTRA"].ToString().ToLower())
                            {
                                case "1":
                                case "true":
                                    entidade.EXTRA = true;
                                    break;

                            }
                        }

                        entities.Add(entidade);
                    }
                    dr.Close();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entities;
        }

        public bool UpdateScheduleStaff(int userid, int scheduleitemid, string description)
        {
            float total = 0;


            int ret = 0;

            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendFormat("UPDATE schedule_item_response SET STATUS_ID = 1, DESCRIPTION = @DESCRIPTION  WHERE STATUS_ID = 0 AND SCHEDULE_ITEM_ID = {0} AND STAFF_ID = {1}; ", scheduleitemid, userid);

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar));
                    dataProc.Parameters["@DESCRIPTION"].Value = description;

                    // executa comando.
                    ret = dataProc.ExecuteNonQuery();

                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }



            if (ret > 0)
            {
                var text = LoadInvoice(scheduleitemid, out total);
                var name = LoadUser(userid);
                InsertSchedulePaymentStaff(scheduleitemid, name, text, total);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertSchedulePaymentStaff(int scheduleitemid, string staff_name, string description, float total)
        {
            var text = LoadInvoice(scheduleitemid, out total);

            int ret = 0;

            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine("INSERT INTO schedule_payment (SCHEDULE_ITEM_ID, DESCRIPTION, STAFF_NAME, TOTAL, CREATED, DT_PAYMENT, STATUS_ID) VALUES (@SCHEDULE_ITEM_ID, @DESCRIPTION, @STAFF_NAME, @TOTAL, NOW(), @DT_PAYMENT, @STATUS_ID);");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@SCHEDULE_ITEM_ID", MySqlDbType.Int32));
                    dataProc.Parameters["@SCHEDULE_ITEM_ID"].Value = scheduleitemid;

                    dataProc.Parameters.Add(new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar));
                    dataProc.Parameters["@DESCRIPTION"].Value = description;

                    dataProc.Parameters.Add(new MySqlParameter("@STAFF_NAME", MySqlDbType.VarChar));
                    dataProc.Parameters["@STAFF_NAME"].Value = staff_name;

                    dataProc.Parameters.Add(new MySqlParameter("@TOTAL", MySqlDbType.Float));
                    dataProc.Parameters["@TOTAL"].Value = total;

                    dataProc.Parameters.Add(new MySqlParameter("@DT_PAYMENT", MySqlDbType.DateTime));
                    dataProc.Parameters["@DT_PAYMENT"].Value = DBNull.Value;

                    dataProc.Parameters.Add(new MySqlParameter("@STATUS_ID", MySqlDbType.Int32));
                    dataProc.Parameters["@STATUS_ID"].Value = 0;

                    // executa comando.
                    ret = dataProc.ExecuteNonQuery();

                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public clsScheduleItem GetScheduleItem(int id)
        {
            clsScheduleItem entity = new clsScheduleItem();

            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.AppendLine(" select  ");
                    sbSQL.AppendLine(" schedule_item.ID, ");
                    sbSQL.AppendLine(" schedule_base.ID as SCHEDULE_ID, ");
                    sbSQL.AppendLine(" schedule_base.NAME as SCHEDULE_NAME, ");
                    sbSQL.AppendLine(" schedule_item.PLAN_TASK_ID, ");
                    sbSQL.AppendLine(" plan.NAME as PLAN_NAME, ");
                    sbSQL.AppendLine(" schedule_item.PERIOD_TYPE_ID, ");
                    sbSQL.AppendLine(" period_type.DESCRIPTION as PERIOD_TYPE_NAME, ");
                    sbSQL.AppendLine(" schedule_item.TIME_BEGIN, ");
                    sbSQL.AppendLine(" schedule_item.DATE_BEGIN ");
                    sbSQL.AppendLine(" from schedule_item ");
                    sbSQL.AppendLine(" join schedule_base on schedule_base.ID = schedule_item.SCHEDULE_ID ");
                    sbSQL.AppendLine(" join plan on plan.ID = schedule_item.PLAN_TASK_ID ");
                    sbSQL.AppendLine(" join period_type on period_type.ID = schedule_item.PERIOD_TYPE_ID ");
                    sbSQL.AppendLine(" where schedule_item.ID = @ID ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.VarChar));
                    dataProc.Parameters["@ID"].Value = id;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {

                        entity.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["SCHEDULE_ID"] != DBNull.Value)
                            entity.SCHEDULE_ID = Int32.Parse(dr["SCHEDULE_ID"].ToString());

                        if (dr["SCHEDULE_NAME"] != DBNull.Value)
                            entity.SCHEDULE_NAME = dr["SCHEDULE_NAME"].ToString();

                        if (dr["PLAN_TASK_ID"] != DBNull.Value)
                            entity.PLAN_TASK_ID = Int32.Parse(dr["PLAN_TASK_ID"].ToString());

                        if (dr["PLAN_NAME"] != DBNull.Value)
                            entity.PLAN_NAME = dr["PLAN_NAME"].ToString();

                        if (dr["PERIOD_TYPE_ID"] != DBNull.Value)
                            entity.PERIOD_TYPE_ID = Int32.Parse(dr["PERIOD_TYPE_ID"].ToString());

                        if (dr["PERIOD_TYPE_NAME"] != DBNull.Value)
                            entity.PERIOD_TYPE_NAME = dr["PERIOD_TYPE_NAME"].ToString();

                        if (dr["TIME_BEGIN"] != DBNull.Value)
                        {
                            string[] valores = dr["TIME_BEGIN"].ToString().Split(':');

                            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Int32.Parse(valores[0]), Int32.Parse(valores[1]), 0);

                            entity.TIME_BEGIN = string.Format("{0:hh:mm tt}", dt).ToLower();
                        }

                        if (dr["DATE_BEGIN"] != DBNull.Value)
                            entity.DATE_BEGIN = DateTime.Parse(dr["DATE_BEGIN"].ToString()).ToString("yyyy/MM/dd");



                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }



            if (id > 0)
            {
                entity.TASKS = LoadTasksByItemId(id);

                entity.IMAGES = LoadImagesByItemId(id);
            }
            return entity;
        }

        public IList<clsImage> LoadImagesByItemId(int itemid)
        {
            IList<clsImage> entities = new List<clsImage>();

            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine("  select   ");
                    sbSQL.AppendLine("  picture_name  ");
                    sbSQL.AppendLine("  from schedule_item_image ");
                    sbSQL.AppendLine("  where schedule_item_id = " + itemid.ToString());
                    sbSQL.AppendLine("  order by picture_order ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;


                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        entities.Add(new clsImage() { PICTURE = dr["picture_name"].ToString() });
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return entities;
        }

        public IList<clsTask> LoadTasksByItemId(int itemid)
        {
            IList<clsTask> entities = new List<clsTask>();

            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();


                    sbSQL.AppendLine(" select  ");
                    sbSQL.AppendLine(" task.ID, task.NAME, ");
                    sbSQL.AppendLine(" task.PRICE, ");
                    sbSQL.AppendLine(" task.DESCRIPTION, ");
                    sbSQL.AppendLine(" task.EXTRA, ");
                    sbSQL.AppendLine(" schedule_item_task.TASK_ID, ");
                    sbSQL.AppendLine(" schedule_item_task.QUANTITY  ");
                    sbSQL.AppendLine(" from schedule_item_task ");
                    sbSQL.AppendLine(" join task on task.ID = schedule_item_task.TASK_ID ");
                    sbSQL.AppendLine(" where schedule_item_task.schedule_item_id = " + itemid.ToString());
                    sbSQL.AppendLine(" order by extra  ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;


                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {

                        clsTask entidade = new clsTask();

                        entidade.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["NAME"] != DBNull.Value)
                            entidade.NAME = dr["NAME"].ToString();

                        if (dr["PRICE"] != DBNull.Value)
                            entidade.PRICE = float.Parse(dr["PRICE"].ToString());

                        if (dr["DESCRIPTION"] != DBNull.Value)
                            entidade.DESCRIPTION = dr["DESCRIPTION"].ToString();

                        if (dr["NAME"] != DBNull.Value && dr["PRICE"] != DBNull.Value)
                        {
                            entidade.FULL = string.Format("{0} ${1}", entidade.NAME, dr["PRICE"].ToString());

                            if (dr["DESCRIPTION"] != DBNull.Value)
                                entidade.FULL = string.Concat(entidade.FULL, " ", dr["DESCRIPTION"].ToString());
                        }

                        if (dr["QUANTITY"] != DBNull.Value)
                            entidade.QTY = Int32.Parse(dr["QUANTITY"].ToString());

                        if (dr["EXTRA"] != DBNull.Value)
                        {
                            entidade.EXTRA = false;
                            switch (dr["EXTRA"].ToString().ToLower())
                            {
                                case "1":
                                case "true":
                                    entidade.EXTRA = true;
                                    break;

                            }
                        }

                        entities.Add(entidade);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return entities;
        }

        public IList<clsSchedule> LoadScheduleByUser(int userid)
        {
            IList<clsSchedule> entities = new List<clsSchedule>();

            try
            {
                using (var connection = CreateConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.AppendLine(" select SB.*, period_type.DESCRIPTION as PERIOD_NAME from schedule_base SB ");
                    sbSQL.AppendLine(" join schedule_customer SU on SU.SCHEDULE_ID = SB.ID ");
                    sbSQL.AppendLine(" join schedule_item on schedule_item.SCHEDULE_ID = SB.ID  ");
                    sbSQL.AppendLine(" join period_type on period_type.ID = schedule_item.PERIOD_TYPE_ID ");
                    sbSQL.AppendLine(" where SB.ACTIVE = 1 and SU.CUSTOMER_ID = @ID ");

                    sbSQL.AppendLine(" UNION ");
                    sbSQL.AppendLine(" select SB.*, period_type.DESCRIPTION as PERIOD_NAME from schedule_base SB ");
                    sbSQL.AppendLine(" join schedule_partner SU on SU.SCHEDULE_ID = SB.ID ");
                    sbSQL.AppendLine(" join schedule_item on schedule_item.SCHEDULE_ID = SB.ID  ");
                    sbSQL.AppendLine(" join period_type on period_type.ID = schedule_item.PERIOD_TYPE_ID ");
                    sbSQL.AppendLine(" where SB.ACTIVE = 1 and SU.PARTNER_ID = @ID ");

                    sbSQL.AppendLine(" UNION ");
                    sbSQL.AppendLine(" select SB.*, period_type.DESCRIPTION as PERIOD_NAME from schedule_base SB ");
                    sbSQL.AppendLine(" join schedule_customer SC on SC.SCHEDULE_ID = SB.ID ");
                    sbSQL.AppendLine(" join schedule_customer_staff SCS on SCS.CUSTOMER_ID = SC.CUSTOMER_ID ");
                    sbSQL.AppendLine(" join schedule_item on schedule_item.SCHEDULE_ID = SB.ID  ");
                    sbSQL.AppendLine(" join period_type on period_type.ID = schedule_item.PERIOD_TYPE_ID ");

                    sbSQL.AppendLine(" where SB.ACTIVE = 1 and SCS.STAFF_ID = @ID ");

                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.VarChar));
                    dataProc.Parameters["@ID"].Value = userid;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        clsSchedule entity = new clsSchedule();
                        entity.ID = Int32.Parse(dr["ID"].ToString());
                        entity.NAME = dr["NAME"].ToString();
                        entity.PERIOD_NAME = dr["PERIOD_NAME"].ToString();
                        entity.ACTIVE = true;

                        entities.Add(entity);
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return entities;
        }

        #endregion PUBLIC METHODS

        #region PRIVATE METHODS 

        private string LoadInvoice(int ID, out float total)
        {
            StringBuilder entidade = new StringBuilder();
            float subTotal = 0;
            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine(" select T.NAME, T.PRICE, SIT1.QUANTITY from plan_task PT ");
                    sbSQL.AppendLine(" join task T on T.ID = PT.TASK_ID ");
                    sbSQL.AppendLine(" join schedule_item_task SIT1 on SIT1.TASK_ID = T.ID where T.EXTRA = 0 and SIT1.SCHEDULE_ITEM_ID = @ID ");
                    sbSQL.AppendLine(" union ");
                    sbSQL.AppendLine(" select TK.NAME, TK.PRICE, SIT2.QUANTITY from task TK ");
                    sbSQL.AppendLine(" join schedule_item_task SIT2 on SIT2.TASK_ID = TK.ID  where TK.EXTRA = 1 and SIT2.SCHEDULE_ITEM_ID = @ID ");



                    var dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;
                    dataProc.CommandText = sbSQL.ToString();

                    dataProc.Parameters.Add(NewParameter(dataProc, "@ID", ID));

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr["NAME"] != DBNull.Value && dr["PRICE"] != DBNull.Value && dr["QUANTITY"] != DBNull.Value)
                        {
                            entidade.AppendLine(string.Format("- {0} [{1}] = ${2}", dr["NAME"].ToString(), dr["QUANTITY"].ToString(), dr["PRICE"].ToString()));
                            subTotal += float.Parse(dr["PRICE"].ToString()) * Int32.Parse(dr["QUANTITY"].ToString());
                        }
                    }
                    dr.Close();
                    dataProc.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            entidade.AppendLine(LoadTotalScheduleItem(ID, out total));

            total += subTotal;

            return entidade.ToString();
        }

        private string LoadTotalScheduleItem(int ID, out float total)
        {
            total = 0;
            StringBuilder entidade = new StringBuilder();

            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine(" select ADDITIONAL, DESCRIPTION from schedule_item where ID = @ID ");


                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.Int32));
                    dataProc.Parameters["@ID"].Value = ID;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    if (dr.Read())
                    {
                        if (dr["DESCRIPTION"] != DBNull.Value && dr["ADDITIONAL"] != DBNull.Value)
                        {
                            total = float.Parse(dr["ADDITIONAL"].ToString());
                            entidade.AppendLine(string.Format("- {0} = ${1}", dr["DESCRIPTION"].ToString(), dr["ADDITIONAL"].ToString()));
                        }

                    }
                    dr.Close();
                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }



            return entidade.ToString();
        }

        private string LoadUser(int ID)
        {
            StringBuilder entidade = new StringBuilder();

            try
            {
                using (var connection = CreateConnection())
                {

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine(" select NAME, EMAIL from user where ID = @ID ");


                    MySqlCommand dataProc = new MySqlCommand(sbSQL.ToString(), connection);
                    dataProc.CommandType = CommandType.Text;

                    dataProc.Parameters.Add(new MySqlParameter("@ID", MySqlDbType.Int32));
                    dataProc.Parameters["@ID"].Value = ID;

                    // executa comando.
                    IDataReader dr = dataProc.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr["NAME"] != DBNull.Value && dr["EMAIL"] != DBNull.Value)
                        {
                            entidade.AppendLine(string.Format("{0} ({1})", dr["NAME"].ToString(), dr["EMAIL"].ToString()));
                        }
                    }
                    dr.Close();
                    dataProc.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return entidade.ToString();
        }

        #endregion PRIVATE METHODS 

    }
}
