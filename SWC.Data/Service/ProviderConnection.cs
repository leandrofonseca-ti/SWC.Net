using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.Common;

namespace SWC.Data.Service
{
    public class ProviderConnection
    {

        public MySqlConnection CreateConnection()
        {

            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Assume failure.
            MySqlConnection connection = null;

            // Create the DbProviderFactory and DbConnection.
            if (_connectionString != null)
            {
                try
                {
                    connection = new MySqlConnection(_connectionString);
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                }
                catch (Exception ex)
                {
                    // Set the connection to null if it was created.
                    if (connection != null)
                    {
                        connection = null;
                    }
                    Console.WriteLine(ex.Message);
                }
            }
            // Return the connection.
            return connection;
        }

        public DbParameter NewParameter(MySqlCommand cmd, string name, object value)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

     
    }
}
