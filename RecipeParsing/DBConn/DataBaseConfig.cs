using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeParsing.DBConn
{
    class DataBaseConfig
    {
        // Winpac Test Database
        public Oracle.ManagedDataAccess.Client.OracleTransaction g_OraTransaction = null;
        public Oracle.ManagedDataAccess.Client.OracleConnection g_OraConnection = null;
        public Oracle.ManagedDataAccess.Client.OracleCommand g_OraCommand = null;
        public string m_ConnectionString = "Data Source=(DESCRIPTION="
                     + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=193.169.10.78)(PORT=1521)))"
                     + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
                     + "User Id=RMS;Password=rms1234";
        public void InitDB()
        {
            try
            {
                g_OraConnection = new Oracle.ManagedDataAccess.Client.OracleConnection();
                g_OraConnection.ConnectionString = m_ConnectionString;
                g_OraConnection.Open();

                if (g_OraConnection.State == System.Data.ConnectionState.Open)
                {
                    g_OraCommand = new Oracle.ManagedDataAccess.Client.OracleCommand();
                    g_OraCommand.Connection = g_OraConnection;
                }
                else
                {
                    Console.WriteLine("InitDB :" + "DB Connect Failed");
                    //Logger.Logging("ERROR", "InitDB : " + "DB Connect Failed.", EWSLog.LOGTYPE.TEXT);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR :" + "InitDB " + ex.Message);
                //Logger.Logging("ERROR", "InitDB : " + ex.Message, EWSLog.LOGTYPE.TEXT);
            }
        }

        public void InsertQuery(string sql)
        {

            InitDB();

            g_OraTransaction = g_OraConnection.BeginTransaction();
            g_OraCommand.CommandText = sql;
            g_OraCommand.ExecuteNonQuery();

            g_OraTransaction.Commit();
            //END

        }

    }
}
