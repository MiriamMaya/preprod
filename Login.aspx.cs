using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ReporteGeneral.ConexionSQL;
using System.Net.NetworkInformation;
using System.Globalization;
using System.Drawing;
using Microsoft.SqlServer.Server;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.Ajax.Utilities;
using System.Threading;

namespace ReporteGeneral
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int year = DateTime.Now.Year - 10; year <= DateTime.Now.Year + 10; year++)
                {
                    YearSelector.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }

                if (CbxReporteVariosDias.Checked)
                {
                    CbxReporteVariosDias.Checked = false;
                }
                else
                {
                    if (CbxReporteDia.Checked)
                    {
                        CbxReporteDia.Checked = false;
                    }
                }
            }
        }

        /***********************************************************************************************************************/
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (CbxReporteDia.Checked)
            {
                DateTime selectedDate = Calendar1.SelectedDate;
                Fecha_Inicial_Reporte.Text = selectedDate.ToString("dd/MM/yyyy 00:00:000");
                Fecha_Final_Reporte.Text = selectedDate.ToString("dd/MM/yyyy 23:59:000");

                if (!(string.IsNullOrEmpty(Fecha_Inicial_Reporte.Text) && (!string.IsNullOrEmpty(Fecha_Final_Reporte.Text))))
                {
                    CbxReporteVariosDias.Enabled = false;
                    Fecha_Inicial_Reporte.Enabled = false;
                    Fecha_Final_Reporte.Enabled = false;
                    Calendar1.Enabled = false;
                    CellRangoSeleccionado.Visible = true;
                    lbRangoSeleccionado.Visible = true;

                    CellRangoSeleccionado.Enabled = true;
                    lbRangoSeleccionado.Enabled = true;

                    CellRangoSeleccionado.Visible = true;
                    lbRangoSeleccionado.Visible = true;

                    CellCrearReporteFinal.Enabled = true;
                    CellCrearReporteFinal.Visible = true;

                    LbCrearReporteFinal.Enabled = true;
                    LbCrearReporteFinal.Visible = true;

                    CellBtCrearReporteFinal.Enabled = true;
                    BtCrearReporteFinal.Enabled = true;

                    CellBtCrearReporteFinal.Visible = true;
                    BtCrearReporteFinal.Visible = true;
                }
            }
            else
            {
                if(CbxReporteVariosDias.Checked)
                {
                    if (string.IsNullOrEmpty(Fecha_Inicial_Reporte.Text))
                    {
                        CbxReporteDia.Enabled = false;
                        DateTime selectedDate = Calendar1.SelectedDate;
                        Fecha_Inicial_Reporte.Visible = true;

                        Fecha_Inicial_Reporte.Text = selectedDate.ToString("dd/MM/yyyy 00:00:000");
                        Fecha_Inicial_Reporte.Enabled = false;
                        Fecha_Final_Reporte.Enabled = true;

                        CellSegundoRangoFechas.Visible = true;
                        lbSegundoRangoFechas.Visible = true;
                        lbSegundoRangoFechas.Enabled = true;
                       
                    }
                    else
                    {
                        CbxReporteVariosDias.Enabled = false;
                        DateTime selectedDate = Calendar1.SelectedDate;
                        Fecha_Final_Reporte.Visible = true;

                        Fecha_Final_Reporte.Text = selectedDate.ToString("dd/MM/yyyy 23:59:000");
                        Fecha_Final_Reporte.Enabled = false;

                        if(!(string.IsNullOrEmpty(Fecha_Inicial_Reporte.Text) && (!string.IsNullOrEmpty(Fecha_Final_Reporte.Text))))
                        {
                            Calendar1.Enabled = false;

                            CellRangoSeleccionado.Visible = true;
                            lbRangoSeleccionado.Visible = true;

                            CellRangoSeleccionado.Enabled = true;
                            lbRangoSeleccionado.Enabled = true;

                            CellCrearReporteFinal.Enabled = true;
                            CellCrearReporteFinal.Visible = true;

                            LbCrearReporteFinal.Enabled = true;
                            LbCrearReporteFinal.Visible = true;

                            CellBtCrearReporteFinal.Enabled = true;
                            BtCrearReporteFinal.Enabled = true;

                            CellBtCrearReporteFinal.Visible = true;
                            BtCrearReporteFinal.Visible = true;
                        }
                    }
                }
            }
        }
        /***********************************************************************************************************************/
        public bool ConexionDB()
        {
            try
            {
                ConexionSQL.SQLConexion sQLConexion = new SQLConexion();
                bool sConn = sQLConexion.ConexionDB();
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
            }

            return true;
        }
        /***********************************************************************************************************************/
        public bool ObtenerCliente_Campañas()
        {
            int TotalInsertados = 0;
            int FilasObtenidas = 0;
            string connectionString = "Server=10.55.37.12; Database=master; User ID=sa; Password=Enter83; MultipleActiveResultSets=True";

            try
            {
                string fechaInicialOriginal = Fecha_Inicial_Reporte.Text;
                string formatoInicialEntrada = "dd/MM/yyyy HH:mm:fff";
                DateTime fecha_Inicial = DateTime.ParseExact(fechaInicialOriginal, formatoInicialEntrada, CultureInfo.InvariantCulture);
                string fechaConvertida_Inicial = fecha_Inicial.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string fechaFinalOriginal = Fecha_Final_Reporte.Text;
                string formatoFinalEntrada = "dd/MM/yyyy HH:mm:fff";
                DateTime fecha_Final = DateTime.ParseExact(fechaFinalOriginal, formatoFinalEntrada, CultureInfo.InvariantCulture);
                string fechaConvertida_Final = fecha_Final.ToString("yyyy-MM-dd HH:mm:ss.fff");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_REPORTE_GENERAL_CARTERAS_DE_MC_COLLECT", connection))
                    {
                        command.CommandTimeout = 300;
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string sPhysical_Data_Base_Name = reader[0].ToString();
                                string sCliente = reader[1].ToString();
                                string sCampaña = reader[2].ToString();

                               // if (string.Equals(sCliente, "ALIO"))
                                {
                                    using (SqlConnection innerConnection = new SqlConnection(connectionString))
                                    {
                                        innerConnection.Open();
                                        //SP_REPORTE_GENERAL_CARTERAS_POR_FECHAS
                                        using (SqlCommand innerCommand = new SqlCommand("SP_REPORTE_GENERAL_CARTERAS_POR_FECHAS", innerConnection))
                                        {
                                            innerCommand.CommandType = CommandType.StoredProcedure;
                                            innerCommand.Parameters.AddWithValue("@FECHA_INICIO", fechaConvertida_Inicial);
                                            innerCommand.Parameters.AddWithValue("@FECHA_FIN", fechaConvertida_Final);
                                            innerCommand.Parameters.AddWithValue("@OBJDB", sPhysical_Data_Base_Name);
                                            innerCommand.Parameters.AddWithValue("@CLIENTE", sCliente);
                                            innerCommand.Parameters.AddWithValue("@CAMPAÑA", sCampaña);

                                            using (SqlDataReader innerReader = innerCommand.ExecuteReader())
                                            {
                                                while (innerReader.Read())
                                                {
                                                    string sFecha = innerReader[0].ToString();


                                                    sFecha = sFecha.Replace(" a. m.", " AM").Replace(" p. m.", " PM");

                                                    string formato = "dd/MM/yyyy hh:mm:ss tt"; // El formato para la fecha con AM/PM
                                                    DateTime fechaConvertida;
                                                    fechaConvertida = DateTime.ParseExact(sFecha, formato, CultureInfo.InvariantCulture);

                                                    string sClienteRead = innerReader[1].ToString();
                                                    string sCampañaRead = innerReader[2].ToString();
                                                    string sAgencia = innerReader[3].ToString();
                                                    string sCuentas_Activas = innerReader[4].ToString();
                                                    string sUsuarios_Call_Center = innerReader[5].ToString();
                                                    string sGestiones_tel_gest = innerReader[6].ToString();
                                                    string sUsuarios_Visitadores = innerReader[7].ToString();
                                                    string sGestiones_tel_vist = innerReader[8].ToString();
                                                    string sUsuarios_Administradores = innerReader[9].ToString();
                                                    string sTotal_Usuarios = innerReader[10].ToString();

                                                    //SP_REPORTE_GENERAL_CARTERAS_POR_FECHAS
                                                    using (SqlCommand insertCommand = new SqlCommand("REPORTES_MC_COLLECT.dbo.SP_REPORTE_GRAL_CARTERAS_POR_FECHA", innerConnection))
                                                    {
                                                        insertCommand.CommandType = CommandType.StoredProcedure;
                                                        insertCommand.Parameters.AddWithValue("@R_CC_FECHA", fechaConvertida);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_CLIENTE", sClienteRead);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_CAMPAÑA", sCampañaRead);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_AGENCIA", sAgencia);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_CUENTAS_ACTIVAS", sCuentas_Activas);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_USUARIOS_CALL_CENTER", sUsuarios_Call_Center);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_GESTIONES_TEL_GEST", sGestiones_tel_gest);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_USUARIOS_VIST", sUsuarios_Visitadores);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_GESTIONES_TEL_VIST", sGestiones_tel_vist);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_USUARIOS_ADMON", sUsuarios_Administradores);
                                                        insertCommand.Parameters.AddWithValue("@R_CC_TOTAL_USUARIOS", sTotal_Usuarios);

                                                        insertCommand.ExecuteNonQuery();
                                                        TotalInsertados++;

                                                    }
                                                }

                                                FilasObtenidas = innerReader.RecordsAffected;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
            }

            if (TotalInsertados > 0)
            {
                DesplegarReporte();
                Thread.Sleep(3000); 

                CellBtDescargarReporte.Enabled = true;
                BtDescargarReporte.Enabled = true;

                CellBtDescargarReporte.Visible = true;
                BtDescargarReporte.Visible = true;

                CellReporteObtenido.Enabled = true;
                LbReporteObtenido.Enabled = true;

                CellReporteObtenido.Visible = true;
                LbReporteObtenido.Visible = true;
                
            }
            else 
            {
                CellReporteObtenido.Enabled = true;
                LbReporteObtenido.Enabled =true;

                CellReporteObtenido.Visible = true;
                LbReporteObtenido.Visible = true;

                LbReporteObtenido.Text = "NO EXISTEN DATOS EN ESTA FECHA O RANGO DE FECHAS";
            }

            return true;
        }


        /***********************************************************************************************************************/
        protected void BtReporte_Click(object sender, ImageClickEventArgs e)
        {
            bool bOCC= ObtenerCliente_Campañas();
        }
        /***********************************************************************************************************************/
        protected void CbxReporteDia_CheckedChanged(object sender, EventArgs e)
        {
            if (CbxReporteVariosDias.Checked)
            {
                CbxReporteVariosDias.Checked = false;
            }
            else
            {
                CbxReporteDia.Enabled = true;
                CbxReporteDia.Visible = true;
                CbxReporteDia.Checked = true;

                Calendario.Enabled = true;
                Calendario.Visible = true;

                Calendar1.Enabled = true;
                Calendar1.Visible = true;
                Calendar1.SelectedDate = DateTime.MinValue;

                Fecha_Inicial_Reporte.Enabled = true;
                Fecha_Inicial_Reporte.Visible = true;
                
                Fecha_Final_Reporte.Enabled = true;
                Fecha_Final_Reporte.Visible = true;
                
                Fecha_Inicial_Reporte.Text = "";
                Fecha_Final_Reporte.Text = "";

                TituloFecha.Enabled = true;
                TituloFecha.Visible = true;

                lbSeleccioneFecha.Enabled = true;
                lbSeleccioneFecha.Visible = true;

                lbSeleccioneFecha.Enabled = true;

                FechaInicialReporte.Enabled = true;
                FechaInicialReporte.Visible = true;

                FechaIReporte.Enabled = true;
                FechaIReporte.Visible = true;

                FechaObtenidaReporte.Enabled = true;
                FechaObtenidaReporte.Visible = true;

                FechaFinalReporte.Enabled = true;
                FechaFinalReporte.Visible = true;

                FechaFReporte.Enabled = true;
                FechaFReporte.Visible = true;

                FechaObtenidaFinalReporte.Enabled = true;
                FechaObtenidaFinalReporte.Visible = true;

                Tabla2Cell2.Enabled = true;
                Tabla2Cell2.Visible = true;

                Tabla2Cell3.Enabled = true;
                Tabla2Cell3.Visible = true;

                Tabla2Cell4.Enabled = true;
                Tabla2Cell4.Visible = true;

                lbLimpiarConsulta.Enabled = true;
                lbLimpiarConsulta.Visible = true;

                CellLimpiarConsulta.Enabled = true;
                CellLimpiarConsulta.Visible = true;
                
                BtLimpiarConsulta.Enabled = true;
                BtLimpiarConsulta.Visible = true;

            }
        }
        /***********************************************************************************************************************/
        protected void CbxReporteVariosDias_CheckedChanged(object sender, EventArgs e)
        {
            if(CbxReporteDia.Checked)
            {
                CbxReporteDia.Checked=false;   
            }
            else
            {
                CbxReporteVariosDias.Enabled = true;
                CbxReporteVariosDias.Visible = true;    
                CbxReporteVariosDias.Checked = true;

                
                Calendario.Enabled = true;
                Calendario.Visible = true;

                Calendar1.Enabled = true;
                Calendar1.SelectedDate = DateTime.MinValue;
                Calendar1.Visible = true;

                TituloFecha.Enabled = true;
                TituloFecha.Visible = true;

                lbSeleccioneFecha.Enabled = true;
                lbSeleccioneFecha.Visible = true;

                FechaInicialReporte.Enabled = true;
                FechaInicialReporte.Visible = true;

                
                FechaIReporte.Enabled = true;
                FechaIReporte.Visible = true;

                FechaObtenidaReporte.Enabled = true;
                FechaObtenidaReporte.Visible = true;

                Fecha_Inicial_Reporte.Enabled = true;
                Fecha_Inicial_Reporte.Visible = true;

                
                FechaIReporte.Enabled = true;
                FechaIReporte.Visible = true;

                FechaObtenidaReporte.Enabled = true;
                FechaObtenidaReporte.Visible = true;

                FechaFinalReporte.Enabled = true;
                FechaFinalReporte.Visible = true;

                FechaFinalReporte.Enabled = true;
                FechaFinalReporte.Visible = true;


                FechaFReporte.Enabled = true;
                FechaFReporte.Visible = true;

                FechaObtenidaFinalReporte.Enabled = true;
                FechaObtenidaFinalReporte.Visible = true;

                Fecha_Inicial_Reporte.Enabled=true;
                Fecha_Inicial_Reporte.Visible=true;
                Fecha_Inicial_Reporte.Text = "";


                Fecha_Final_Reporte.Enabled = true;
                Fecha_Final_Reporte.Visible = true;
                Fecha_Final_Reporte.Text = "";

                Tabla3Cell1.Enabled = true;
                Tabla3Cell1.Visible = true;

                Tabla3Cell2.Enabled = true;
                Tabla3Cell2.Visible = true;

                CellPrimerRangoFechas.Enabled = true;
                CellPrimerRangoFechas.Visible = true;

                lbPrimerRangoFechas.Enabled = true;
                lbPrimerRangoFechas.Visible = true;

                Tabla3Cell4.Enabled = true;
                Tabla3Cell4.Visible = true;

                Tabla2Cell2.Enabled = true;
                Tabla2Cell2.Visible = true;

                Tabla2Cell3.Enabled = true;
                Tabla2Cell3.Visible = true;

                Tabla2Cell4.Enabled = true;
                Tabla2Cell4.Visible = true;

                lbLimpiarConsulta.Enabled = true;
                lbLimpiarConsulta.Visible = true;

                CellLimpiarConsulta.Enabled = true;
                CellLimpiarConsulta.Visible = true;

                BtLimpiarConsulta.Enabled = true;
                BtLimpiarConsulta.Visible = true;
                
                CellRangoSeleccionado.Enabled = false;
                CellRangoSeleccionado.Visible = false;

                lbRangoSeleccionado.Enabled = false;
                lbRangoSeleccionado.Visible = false;
            }
        }

        /***********************************************************************************************************************/
        protected void BtLimpiarConsulta_Click(object sender, ImageClickEventArgs e)
        {
            if (CbxReporteVariosDias.Checked)
            {
                CbxReporteDia.Checked = false;
                CbxReporteVariosDias.Checked = false;

                CbxReporteDia.Enabled = true;
                CbxReporteVariosDias.Enabled = true;

                CbxReporteDia.Visible = true;
                CbxReporteVariosDias.Visible = true;

                Tabla2Cell2.Enabled = false;
                Tabla2Cell3.Enabled = false;
                Tabla2Cell4.Enabled = false;

                Tabla2Cell2.Visible = false;
                Tabla2Cell3.Visible = false;
                Tabla2Cell4.Visible = false;

                Tabla3Cell1.Enabled = false;
                Tabla3Cell2.Enabled = false;
                Tabla3Cell4.Enabled = false;

                Tabla3Cell1.Enabled = false;
                Tabla3Cell2.Enabled = false;
                Tabla3Cell4.Enabled = false;

                lbLimpiarConsulta.Enabled = false;
                CellLimpiarConsulta.Enabled = false;
                BtLimpiarConsulta.Enabled = false ;

                lbLimpiarConsulta.Visible = false;
                CellLimpiarConsulta.Visible = false;
                BtLimpiarConsulta.Visible = false;

                TituloFecha.Visible = false;
                lbSeleccioneFecha.Visible = false;
                Tabla2Cell2.Visible = false;
                Tabla2Cell3.Visible = false;
                Tabla2Cell4.Visible = false;
                lbLimpiarConsulta.Visible = false;
                CellLimpiarConsulta.Visible = false;
                BtLimpiarConsulta.Visible = false;
                Tabla3Cell1.Visible = false;
                Tabla3Cell2.Visible = false;
                CellPrimerRangoFechas.Visible = false;
                lbPrimerRangoFechas.Visible = false;
                CellSegundoRangoFechas.Visible = false;
                lbSegundoRangoFechas.Visible = false;
                Tabla3Cell4.Visible = false;
                Calendario.Visible = false;
                Calendar1.Visible = false;
                FechaInicialReporte.Visible = false;
                FechaIReporte.Visible = false;
                FechaObtenidaReporte.Visible = false;
                Fecha_Inicial_Reporte.Visible = false;
                FechaFinalReporte.Visible = false;
                FechaFReporte.Visible = false;
                FechaObtenidaFinalReporte.Visible = false;
                Fecha_Final_Reporte.Visible = false;
                CellRangoSeleccionado.Visible = false;
                lbRangoSeleccionado.Visible = false;

                CellCrearReporteFinal.Enabled = false;
                LbCrearReporteFinal.Enabled = false;
                CellBtCrearReporteFinal.Enabled = false;
                BtCrearReporteFinal.Enabled = false;

                CellCrearReporteFinal.Visible = false;
                LbCrearReporteFinal.Visible = false;
                CellBtCrearReporteFinal.Visible = false;
                BtCrearReporteFinal.Visible = false;

                GdVwClienteCampaña.DataSource = null;
                GdVwClienteCampaña.DataBind();

                CellGdVwClienteCampaña.Enabled = false;
                CellGdVwClienteCampaña.Visible = false;

                GdVwClienteCampaña.Enabled = false;
                GdVwClienteCampaña.Visible = false;

                CellBtDescargarReporte.Enabled = false;
                BtDescargarReporte.Enabled = false;

                CellBtDescargarReporte.Visible = false;
                BtDescargarReporte.Visible = false;

                CellReporteObtenido.Enabled = false;
                LbReporteObtenido.Enabled = false;

                CellReporteObtenido.Visible = false;
                LbReporteObtenido.Visible = false;
               

                Borrar_Datos_TMPTabla();
            }
            else
            {
                if(CbxReporteDia.Checked)
                {
                    CbxReporteDia.Checked = false;
                    CbxReporteVariosDias.Checked = false;

                    CbxReporteDia.Enabled = true;
                    CbxReporteVariosDias.Enabled = true;

                    CbxReporteDia.Visible = true;
                    CbxReporteVariosDias.Visible = true;

                    Tabla2Cell2.Enabled = false;
                    Tabla2Cell3.Enabled = false;
                    Tabla2Cell4.Enabled = false;

                    Tabla2Cell2.Visible = false;
                    Tabla2Cell3.Visible = false;
                    Tabla2Cell4.Visible = false;

                    Tabla3Cell1.Enabled = false;
                    Tabla3Cell2.Enabled = false;
                    Tabla3Cell4.Enabled = false;

                    Tabla3Cell1.Enabled = false;
                    Tabla3Cell2.Enabled = false;
                    Tabla3Cell4.Enabled = false;

                    TituloFecha.Visible = false;
                    lbSeleccioneFecha.Visible = false;

                    lbLimpiarConsulta.Enabled = false;
                    CellLimpiarConsulta.Enabled = false;
                    BtLimpiarConsulta.Enabled = false;

                    lbLimpiarConsulta.Visible = false;
                    CellLimpiarConsulta.Visible = false;
                    BtLimpiarConsulta.Visible = false;

                    Calendario.Enabled = false;
                    Calendar1.Enabled = false;
                    FechaInicialReporte.Enabled = false;
                    FechaIReporte.Enabled = false;
                    FechaObtenidaReporte.Enabled = false;
                    Fecha_Inicial_Reporte.Enabled = false;
                    FechaFinalReporte.Enabled = false;
                    FechaFReporte.Enabled = false;
                    FechaObtenidaFinalReporte.Enabled = false;
                    Fecha_Final_Reporte.Enabled = false;
                    CellRangoSeleccionado.Enabled = false;
                    lbRangoSeleccionado.Enabled= false;
                    FechaInicialReporte.Enabled= false;
                    FechaIReporte.Enabled = false;
                    FechaObtenidaReporte.Enabled = false;
                    Fecha_Inicial_Reporte.Enabled = false;
                    FechaFinalReporte.Enabled = false;
                    FechaFReporte.Enabled = false;
                    FechaObtenidaFinalReporte.Enabled = false;
                    Fecha_Final_Reporte.Enabled = false;
                    CellRangoSeleccionado.Enabled = false;
                    lbRangoSeleccionado.Enabled = false;
                    CellCrearReporteFinal.Enabled = false ;
                    LbCrearReporteFinal.Enabled= false;
                    CellBtCrearReporteFinal.Enabled = false;
                    BtCrearReporteFinal.Enabled =false;

                    CellCrearReporteFinal.Enabled = false;
                    LbCrearReporteFinal.Enabled = false;
                    CellBtCrearReporteFinal.Enabled = false;
                    BtCrearReporteFinal.Enabled = false;

                    
                    Calendario.Visible = false;
                    Calendar1.Visible = false;
                    FechaInicialReporte.Visible = false;
                    FechaIReporte.Visible = false;
                    FechaObtenidaReporte.Visible = false;
                    Fecha_Inicial_Reporte.Visible = false;
                    FechaFinalReporte.Visible = false;
                    FechaFReporte.Visible = false;
                    FechaObtenidaFinalReporte.Visible = false;
                    Fecha_Final_Reporte.Visible = false;
                    CellRangoSeleccionado.Visible = false;
                    lbRangoSeleccionado.Visible = false;
                    FechaInicialReporte.Visible = false;
                    FechaIReporte.Visible = false;
                    FechaObtenidaReporte.Visible = false;
                    Fecha_Inicial_Reporte.Visible = false;
                    FechaFinalReporte.Visible = false;
                    FechaFReporte.Visible = false;
                    FechaObtenidaFinalReporte.Visible = false;
                    Fecha_Final_Reporte.Visible = false;
                    CellRangoSeleccionado.Visible = false;
                    lbRangoSeleccionado.Visible = false;

                   
                    CellCrearReporteFinal.Visible = false;
                    LbCrearReporteFinal.Visible = false;
                    CellBtCrearReporteFinal.Visible =false;
                    BtCrearReporteFinal.Visible = false ;

                    GdVwClienteCampaña.DataSource = null;
                    GdVwClienteCampaña.DataBind();

                    CellGdVwClienteCampaña.Enabled = false;
                    CellGdVwClienteCampaña.Visible = false;

                    GdVwClienteCampaña.Enabled = false;
                    GdVwClienteCampaña.Visible = false;

                    CellBtDescargarReporte.Enabled = false;
                    BtDescargarReporte.Enabled = false;

                    CellBtDescargarReporte.Visible = false;
                    BtDescargarReporte.Visible = false;

                    CellReporteObtenido.Enabled = false;
                    LbReporteObtenido.Enabled = false;

                    CellReporteObtenido.Visible = false;
                    LbReporteObtenido.Visible = false;

                    Borrar_Datos_TMPTabla();
                }
            }
        }

        /***********************************************************************************************************************/
        protected void Crear_Reporte_Final_Click(object sender, ImageClickEventArgs e)
        {
            bool bOCC = ObtenerCliente_Campañas();
        }

        /***********************************************************************************************************************/
        public bool DesplegarReporte()
        {
            CellGdVwClienteCampaña.Enabled = true;
            CellGdVwClienteCampaña.Visible = true;  

            GdVwClienteCampaña.Enabled = true;
            GdVwClienteCampaña.Visible = true;

            string connectionString = "Server=10.55.37.12; Database=master; User ID=sa; Password=Enter83; MultipleActiveResultSets=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("REPORTES_MC_COLLECT.dbo.SP_SELECT_REPORTE_GRAL_CC", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            GdVwClienteCampaña.DataSource = dt;
                            GdVwClienteCampaña.DataBind(); // Realizar el enlace de datos
                        }
                    }
                }
                catch (Exception ex)
                {
                    string sError = ex.Message.ToString();
                }
            }
            return true;
        }
        /***********************************************************************************************************************/
        protected void BtDescargarReporte_Click(object sender, ImageClickEventArgs e)
        {
           
            DataTable dt = new DataTable();

            // Decodificar encabezados
            foreach (TableCell cell in GdVwClienteCampaña.HeaderRow.Cells)
            {
                dt.Columns.Add(HttpUtility.HtmlDecode(cell.Text));
            }

            foreach (GridViewRow row in GdVwClienteCampaña.Rows)
            {
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    // Decodificar celdas
                    dataRow[i] = HttpUtility.HtmlDecode(row.Cells[i].Text);
                }
                dt.Rows.Add(dataRow);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Datos");

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);

                    string fileName = "ExportedData.xlsx";
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", $"attachment;filename={fileName}");

                    HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                    HttpContext.Current.Response.End();
                }
            }
        }
        /***********************************************************************************************************************/
        bool Borrar_Datos_TMPTabla()
        {
            string connectionString = "Server=10.55.37.12; Database=REPORTES_MC_COLLECT; User ID=sa; Password=Enter83";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_DLTE_REPORTE_GRAL_CC", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        int filasAfectadas = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
            }
            return true;
        }

        /***********************************************************************************************************************/
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
                        int totalRegistros = (int)command.ExecuteScalar();

                        if (totalRegistros > 0)
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SP_DLTE_REPORTE_GRAL_CC", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString() ;
            }

            return true;
        }

        /***********************************************************************************************************************/
        protected void YearSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedYear = int.Parse(YearSelector.SelectedValue);
            Calendar1.VisibleDate = new DateTime(selectedYear, Calendar1.VisibleDate.Month, 1);
        }
    }
}