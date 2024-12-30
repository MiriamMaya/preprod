<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ReporteGeneral.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
   
            <h1 id="aspnetTitle" style="color:gainsboro; font-family:Arial; font-size: 50px; text-align: center; font-weight: bold;">
            MC Collect
            </h1>

        </section>

        <seccion class="row" aria-labelledby="aspnetTitle" style="font-family:Arial; font-size:20px; background-color:darkblue; color: blue; font-weight: bold;">
            <p class="lead" style="color:white; font-family:Arial; font-size: 20px; text-align: left; font-weight: bold; ">Vamos juntos por el crecimiento de tu negocio.</p>
        </seccion>
        <br />
        <br />
        <div class="col-1">
            <link rel="stylesheet" href="Content/StyleCalendar.css" />
            <section class="col-md-1" aria-labelledby="librariesTitle" style="text-align: left;">
                <asp:Table ID="Table1" runat="server" Border="2" CellPadding="5" CellSpacing="0" Style="width: auto; table-layout: auto; border-collapse: collapse;">
                    <asp:TableRow>
                        <asp:TableCell Style="border: 1px solid black; padding: 10px; white-space: nowrap; overflow: auto;">
                            <asp:CheckBox ID="CbxReporteDia" runat ="server"  Text="Reporte de 1 dia" style="font-family: Arial; font-size:16px; color:black;" Checked="false" Enabled="true" AutoPostBack="true" OnCheckedChanged="CbxReporteDia_CheckedChanged">
                            </asp:CheckBox>
                        </asp:TableCell>
                        <asp:TableCell Style="border: 1px solid black; padding: 10px; white-space: nowrap; overflow: auto;">
                            <asp:CheckBox ID="CbxReporteVariosDias" runat ="server"  Text="Reporte de un Rango de dias" style="font-family: Arial; font-size:16px; color:black;" Checked="false" Enabled="true" AutoPostBack="true" OnCheckedChanged="CbxReporteVariosDias_CheckedChanged">
                            </asp:CheckBox>
                        </asp:TableCell>
                    </asp:TableRow>
                  </asp:Table>
                <br />
                <br />
                <asp:Table ID="Table2" runat="server" Border="2" CellPadding="5" CellSpacing="0" Style="width: auto; table-layout: auto; border-collapse: collapse;">
                    <asp:TableRow>
                        <asp:TableCell ID="TituloFecha" Visible="false" Style="border: 1px solid black; padding: 10px; background-color:darkblue; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:Label ID="lbSeleccioneFecha" runat="server"  Text ="Seleccione la(s) Fecha(s) de Reporte:" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:white; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="Tabla2Cell2" Visible="false" Style="border: 0px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" ></asp:TableCell>
                        <asp:TableCell ID="Tabla2Cell3" Visible="false" Style="border: 0px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" ></asp:TableCell>
                        <asp:TableCell ID="Tabla2Cell4" Visible="false" Style="border: 0px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:Label ID="lbLimpiarConsulta" runat="server"  Text ="Limpiar Consulta" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:black; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="CellLimpiarConsulta" Enable="false" Visible ="false" Style="border: 0px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:ImageButton ID="BtLimpiarConsulta" Enable="false" Visible ="false" runat="server" Width="50px" Height="50px" ImageUrl="~/Imagen/LimpiarConsulta.png" OnClick="BtLimpiarConsulta_Click">
                            </asp:ImageButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Table ID="Table3" runat="server" Border="2" CellPadding="5" CellSpacing="0" Style="width: auto; table-layout: auto; border-collapse: collapse;">
                    <asp:TableRow>
                         <asp:TableCell ID="Tabla3Cell1" Visible="false" Style="border: 1px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                         </asp:TableCell>
                        <asp:TableCell ID="Tabla3Cell2" Visible="false" Style="border: 1px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                        </asp:TableCell>
                        <asp:TableCell ID="CellPrimerRangoFechas" Visible="false" Style="border: 1px solid black; background-color:darkblue; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                              <asp:Label ID="lbPrimerRangoFechas" runat="server"  Text ="Seleccione la Fecha del Primer Rango" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:white; width: auto;" Enabled="false" Visible="false">
                              </asp:Label>
                        </asp:TableCell>
                         <asp:TableCell ID="CellSegundoRangoFechas" Visible="false" Style="border: 1px solid black; background-color:darkblue; padding: 10px;  white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                             <asp:Label ID="lbSegundoRangoFechas" runat="server"  Text ="Seleccione la Fecha del Segundo Rango" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:white; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                         </asp:TableCell>
                        <asp:TableCell ID="Tabla3Cell4" Visible="false" Style="border: 1px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ID="Calendario" Visible="false" Style="border: 1px solid black; padding: 10px; background-color:darkblue; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold; text-align: center;">
                            <asp:DropDownList ID="YearSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="YearSelector_SelectedIndexChanged">
                            </asp:DropDownList>
                                <asp:Calendar 
                                     ID="Calendar1" 
                                     runat="server" 
                                     CssClass="custom-calendar" 
                                     ShowTitle="true" 
                                     ShowNextPrevMonth="true" 
                                     OnSelectionChanged="Calendar1_SelectionChanged"
                                     DayNameFormat="Shortest"
                                     AutoPostBack="true">
                                </asp:Calendar>
                        </asp:TableCell>
                        <asp:TableCell ID="FechaInicialReporte" Visible="false" Style="border: 0px solid black; padding: 10px;  white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:Label ID="FechaIReporte" runat="server"  Text ="Fecha Inicial de Reporte:" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:black; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="FechaObtenidaReporte" Visible="false" Style="border: 0px solid black; padding: 10px;  white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:TextBox ID="Fecha_Inicial_Reporte" runat="server" AutoPostBack="true" style="font-family: Arial; font-size:14px;  color:black;" Visible="true">
                            </asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell ID="FechaFinalReporte" Visible="false" Style="border: 0px solid black; padding: 10px;  white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:Label ID="FechaFReporte" runat="server"  Text ="Fecha Final de Reporte:" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:black; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="FechaObtenidaFinalReporte" Visible="false" Style="border: 0px solid black; padding: 10px;  white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:TextBox ID="Fecha_Final_Reporte" runat="server" AutoPostBack="true" style="font-family: Arial; font-size:14px;  color:black;" Visible="true">
                            </asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell ID="CellCrearReporteFinal" Visible="false" Style="border: 0px solid black; background-color:white; padding: 10px;  white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:Label ID="LbCrearReporteFinal" runat="server"  Text ="Crear Reporte" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:darkblue; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                        </asp:TableCell>
                         <asp:TableCell ID="CellBtCrearReporteFinal" Visible="false" Style="border: 0px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:ImageButton ID="BtCrearReporteFinal" runat="server" Width="50px" Height="50px" ImageUrl="~/Imagen/reporte.jpg" Enabled="false" Visible="false" OnClick="Crear_Reporte_Final_Click">
                            </asp:ImageButton>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ID="CellRangoSeleccionado" ColumnSpan="7"   Visible="false" Style="border: 1px solid black; background-color:cadetblue; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:Label ID="lbRangoSeleccionado" runat="server"  Text ="RANGO SELECCIONADO" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:white; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ID="CellGenerandoReporte" ColumnSpan="7"   Visible="false" Style="border: 1px solid black; background-color:cadetblue; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                            <asp:Label ID="LbGenerandoReporte" runat="server"  Text ="GENERANDO REPORTE" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:white; width: auto;" Enabled="false" Visible="false">
                            </asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table4" runat="server" Border="2" CellPadding="5" CellSpacing="0" Style="width: auto; table-layout: auto; border-collapse: collapse;">
                        <asp:TableRow>   
                            <asp:TableCell ID="CellBtDescargarReporte" Enabled="false" Visible="false" Style="border: 2px solid black; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                                <asp:ImageButton ID="BtDescargarReporte" runat="server" Width="180px" Height="50px" ImageUrl="~/Imagen/Descargar reporte.png" Enabled="false" Visible="false" OnClick="BtDescargarReporte_Click">
                                </asp:ImageButton>
                            </asp:TableCell>
                            <asp:TableCell ID="CellReporteObtenido" ColumnSpan="10"  Enabled="false" Visible="false" Style="border: 1px solid black; background-color:chartreuse; padding: 10px; white-space: nowrap; overflow: auto; font-family: Arial; font-weight: bold;  text-align: center;" >
                                <asp:Label ID="LbReporteObtenido" runat="server" Text ="REPORTE GENERADO" AutoPostBack="true" style="font-family: Arial; font-size:14px; font-weight: bold; color:white; width: auto;" Enabled="false" Visible="false">
                                </asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table5" runat="server" Border="2" CellPadding="5" CellSpacing="0" Style="width: auto; table-layout: auto; border-collapse: collapse;">
                        <asp:TableRow>   
                            <asp:TableCell ID="CellGdVwClienteCampaña" Enabled="false" Visible="false" Style="border: 0px solid black; padding: 8px; white-space: nowrap; overflow: auto; font-family: Arial;   text-align: center;" >
                                <asp:GridView ID="GdVwClienteCampaña" runat="server" Enabled="false" Visible="false" Style="font-size: 8px; font-family: Arial;" AutoGenerateColumns="True"  BorderColor="Black" BorderWidth="1px"  HeaderStyle-BackColor="DarkBlue" HeaderStyle-ForeColor="White" CellPadding="4">
                                </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </section>
          </div>
    </main>
</asp:Content>
