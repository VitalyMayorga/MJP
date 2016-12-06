using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
namespace SistemaMJP
{
    public partial class ReporteTrazabilidad : System.Web.UI.Page
    {
        private string idPrograma;
        private string programa;
        private string cantidad;
        private string subPartida;
        private string fechaF;
        private string fechaI;
        private ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            string DataString = servicio.TamperProofStringDecode(Request.QueryString["idPrograma"], "MJP");
            string DataString2 = servicio.TamperProofStringDecode(Request.QueryString["programa"], "MJP");
            string DataString3 = servicio.TamperProofStringDecode(Request.QueryString["subPartida"], "MJP");
            string DataString4 = servicio.TamperProofStringDecode(Request.QueryString["Periodo"], "MJP");
            string DataString5 = servicio.TamperProofStringDecode(Request.QueryString["FechaI"], "MJP");
            string DataString6 = servicio.TamperProofStringDecode(Request.QueryString["FechaF"], "MJP");
            idPrograma = DataString;
            programa = DataString2;
            subPartida = DataString3;
            cantidad = DataString4;
            fechaI = DataString5;
            fechaF = DataString6;
            CultureInfo crCulture = new CultureInfo("es-CR");
            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(Server.MapPath("~/Reporte_Trazabilidad.rpt"));
            Tabla_Existencias ds = new Tabla_Existencias();
            DataTable t = ds.Tables.Add("Items");
            t.Columns.Add("Descripcion", Type.GetType("System.String"));
            t.Columns.Add("Historico", Type.GetType("System.String"));           
            t.Columns.Add("RotacionPorMes", Type.GetType("System.String"));            
            t.Columns.Add("CantidadAlmacen", Type.GetType("System.String"));
            t.Columns.Add("CantidadProyectada", Type.GetType("System.String"));
            
            DataRow r;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Reporte_Trazabilidad_Articulos";
                con.Open();
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                cmd.Parameters.AddWithValue("@codigo", subPartida);
                cmd.Parameters.AddWithValue("@fechaI", fechaI);
                cmd.Parameters.AddWithValue("@fechaF", fechaF);
                cmd.Parameters.AddWithValue("@cantidadmeses", cantidad);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    r = t.NewRow();                    
                    r["Descripcion"] = reader.GetString(0);
                    r["Historico"] = reader.GetInt32(1).ToString();                  
                    r["RotacionPorMes"] = reader.GetInt32(2).ToString();                   
                    r["CantidadAlmacen"] = reader.GetInt32(3).ToString();
                    r["CantidadProyectada"] = reader.GetInt32(4).ToString();
                    t.Rows.Add(r);
                }
                
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            
            TextObject fechaFinalT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Fecha2"];
            fechaFinalT.Text = fechaF;

            TextObject fechaInicialT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Fecha1"];
            fechaInicialT.Text = fechaI;

            TextObject programaT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Programa"];
            programaT.Text = programa;

            TextObject subPartidaT = (TextObject)reportdocument.ReportDefinition.Sections["Section1"].ReportObjects["SubPartida"];
            subPartidaT.Text = subPartida;

            TextObject periodoT = (TextObject)reportdocument.ReportDefinition.Sections["Section1"].ReportObjects["Periodo"];
            periodoT.Text = cantidad;

            //CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //txtReportHeader = reportdocument.ReportClientDocument.ReportObjects["numeroFactura"] as TextObject;
            //txtReportHeader.Text = "FacturaTesting";
            reportdocument.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.RefreshReport();
        }
    }
}