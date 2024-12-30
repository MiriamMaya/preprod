using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace ReporteGeneral
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Count_Reporte_Final();
            }
        }
        /***************************************************************************************************************************************************/
        public bool Count_Reporte_Final()
        {
            string connectionString = "Server=10.55.37.12; Database=REPORTES_MC_COLLECT; User ID=sa; Password=Enter83";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                   
                    using (SqlCommand command = new SqlCommand("SP_COUNT_REPORT_GENERAL_CC", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        
                        object result = command.ExecuteScalar();
                        int totalRegistros = result != null ? Convert.ToInt32(result) : 0;

                        if (totalRegistros > 0)
                        {
                        
                            using (SqlCommand sqlCommand = new SqlCommand("SP_DELETE_REPORTE_GRAL_CC", connection))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;

                               
                                object result2 = sqlCommand.ExecuteScalar();
                                int totalRegistros2 = result2 != null ? Convert.ToInt32(result2) : 0;

                                //Console.WriteLine($"Registros eliminados: {totalRegistros2}");
                            }
                        }
                        else
                        {
                            //Console.WriteLine("No hay registros que procesar.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error de SQL: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return true;
        }
        /***************************************************************************************************************************************************/
    }
}