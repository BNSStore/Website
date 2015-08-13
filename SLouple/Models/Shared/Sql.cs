using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SLouple.MVC.Shared
{
    public class Sql
    {
        public const string ProviderName = "SL_MVC";
        //Unsafe! Move to config file
        #if DEBUG
        public const string SqlUsername = "SL_MVC";
        public const string SqlPassword = "59713e03ac";
        public const string SqlServer = "COSH_C1\\SLDEVSQLSERVER";
        public const string SqlDatabase = "BNSStore";
        #else
        public const string SqlUsername = "DB_9B9A99_BNSStore_admin";
        public const string SqlPassword = "bbysd41its";
        public const string SqlServer = "SQL5012.myWindowsHosting.com";
        public const string SqlDatabase = "DB_9B9A99_BNSStore";
        #endif
        private SqlConnection connection { get; set; }

        public Sql()
        {
            this.connection = GenerateSqlConnection(SqlUsername, SqlPassword, SqlServer, SqlDatabase, true);
        }

        public Sql(string username, string password, string server, string database, bool ssl)
        {
            this.connection = GenerateSqlConnection(username, password, server, database, ssl);
        }

        public Sql(SqlConnection connection)
        {
            this.connection = connection;
        }

        public SqlParameterCollection RunStoredProcedure(string spName, List<SqlParameter> sqlPars)
        {
            using (SqlCommand cmd = new SqlCommand(spName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter sqlPar in sqlPars)
                {
                    cmd.Parameters.Add(sqlPar);
                }
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return cmd.Parameters;
            }
        }

        public Dictionary<string, List<Object>> RunStoredProcedure(string spName, List<SqlParameter> sqlPars, string[] columnNames)
        {
            SqlDataReader reader;
            Dictionary<string, List<Object>> columns = new Dictionary<string, List<Object>>();
            foreach(string columnName in columnNames){
                columns.Add(columnName, new List<Object>());
            }
            using (SqlCommand cmd = new SqlCommand(spName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (sqlPars != null)
                {
                    foreach (SqlParameter sqlPar in sqlPars)
                    {
                        cmd.Parameters.Add(sqlPar);
                    }
                }
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach(string columnName in columnNames){
                        columns[columnName].Add(reader[columnName]);
                    }
                }
                connection.Close();
                return columns;
            }
        }

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

        public static SqlParameter GenerateSqlParameter(string name, SqlDbType type, int size, object value, bool output)
        {
            SqlParameter par = new SqlParameter(name, type);
            if (size != 0)
            {
                par.Size = size;
            }
            if (value != null)
            {
                par.Value = value;
            }
            if (output)
            {
                par.Direction = ParameterDirection.Output;
            }
            return par;
        }
    }
}
