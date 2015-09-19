using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SLouple.MVC.Shared
{
    public class Sql
    {
        private SqlConnection connection;

        public Sql(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
        }

        public Sql(string connectionString, SqlCredential credential)
        {
            this.connection = new SqlConnection(connectionString, credential);
        }

        [Obsolete]
        public Sql(string username, string password, string server, string database, bool ssl)
        {
            this.connection = GenerateSqlConnection(username, password, server, database, ssl);
        }

        public Sql(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void RunStoredProcedure(SqlSP sqlSP)
        {
            SqlDataReader reader;
            Dictionary<string, List<Object>> columns = new Dictionary<string, List<Object>>();
            using (SqlCommand cmd = new SqlCommand(sqlSP.GetName(), connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlSP.GetPars().ToArray());
                connection.Open();
                reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    sqlSP.AddOutputColumn(reader.GetName(i));
                }
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        sqlSP.AddOutputColumnValue(reader.GetName(i), reader.GetValue(i));
                    }
                }
                sqlSP.SetOutputPars(cmd.Parameters);
                connection.Close();
            }
        }

        [Obsolete]
        public static SqlConnection GenerateSqlConnection(string username, string password, string server, string database, bool ssl)
        {
            string connectionString;
            connectionString = "Server=" + server + ";";
            connectionString = connectionString + "Database=" + database + ";";
            connectionString = connectionString + "User Id=" + username + ";";
            connectionString = connectionString + "Password=" + password + ";";
            if (ssl)
            {
                connectionString = connectionString + "Encrypt=True;TrustServerCertificate=True;";
            }
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
