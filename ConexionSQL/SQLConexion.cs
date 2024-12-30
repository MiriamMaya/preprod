using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace ReporteGeneral.ConexionSQL
{
    public class SQLConexion
    {
        /**********************************************************************************************************************************************************/
        public bool ConexionDB()
        {
            string connectionString = "Server=10.55.37.12; Database=master; User ID=sa; Password=Enter83";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    string sError = ex.Message.ToString();
                    return false;
                }
            }
            return true;
        }

        /**********************************************************************************************************************************************************/
        public bool CloseDB()
        {
            string connectionString = "Server=10.55.37.12; Database=master; User ID=sa; Password=Enter83";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Close();
                }
                catch (Exception ex)
                {
                    string sError = ex.Message.ToString();
                    return false;
                }
            }

            return true;
        }
        /**********************************************************************************************************************************************************/
    }
}