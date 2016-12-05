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
    public partial class ReporteArticulosDestinoSubPartida : System.Web.UI.Page
    {
        private string idPrograma;
        private string programa;
        private string codigo;
        private string destino;
        private DateTime fechaF;
        private string fechaI;
        private ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            string DataString = servicio.TamperProofStringDecode(Request.QueryString["idPrograma"], "MJP");
            string DataString2 = servicio.TamperProofStringDecode(Request.QueryString["programa"], "MJP");
            string DataString3 = servicio.TamperProofStringDecode(Request.QueryString["subPartida"], "MJP");
            string DataString4 = servicio.TamperProofStringDecode(Request.QueryString["destino"], "MJP");
            idPrograma = DataString;
            programa = DataString2;
            codigo = DataString3;
            destino = DataString4;
            CultureInfo crCulture = new CultureInfo("es-CR");
            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(Server.MapPath("~/Reporte_Articulos_Destino_SubPartida.rpt"));
            Tabla_Existencias ds = new Tabla_Existencias();
            DataTable t = ds.Tables.Add("Items");
            t.Columns.Add("NumeroRequisicion", Type.GetType("System.String"));
            t.Columns.Add("Descripcion", Type.GetType("System.String"));           
            t.Columns.Add("Cantidad", Type.GetType("System.String"));            
            t.Columns.Add("PrecioUnitario", Type.GetType("System.String"));
            t.Columns.Add("PrecioTotal", Type.GetType("System.String"));
            
            DataRow r;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Reporte_Articulos_Destino_SubPartida";
                con.Open();
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@destino", destino);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    r = t.NewRow();                    
                    r["NumeroRequisicion"] = reader.GetString(0);
                    r["Descripcion"] = reader.GetString(1);                   
                    r["Cantidad"] = reader.GetInt32(2).ToString();                   
                    r["PrecioUnitario"] = reader.GetDecimal(3).ToString();
                    r["PrecioTotal"] = reader.GetDecimal(4).ToString();
                    t.Rows.Add(r);
                }
                
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Fecha_Inicial_Reporte_SubPartida_Destino";
                con.Open();
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@destino", destino);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                fechaI = reader.GetDateTime(0).ToString().Substring(0, 10);
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            fechaF = DateTime.Now;
            TextObject fechaFinalT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Fecha2"];
            fechaFinalT.Text = fechaF.ToString("dd/MM/yyyy");

            TextObject fechaInicialT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Fecha1"];
            fechaInicialT.Text = fechaI;

            TextObject programaT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Programa"];
            programaT.Text = programa;

            TextObject subPartidaT = (TextObject)reportdocument.ReportDefinition.Sections["Section1"].ReportObjects["SubPartida"];
            subPartidaT.Text = codigo;

            TextObject destinoT = (TextObject)reportdocument.ReportDefinition.Sections["Section1"].ReportObjects["Destino"];
            destinoT.Text = destino;

            //CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //txtReportHeader = reportdocument.ReportClientDocument.ReportObjects["numeroFactura"] as TextObject;
            //txtReportHeader.Text = "FacturaTesting";
            reportdocument.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.RefreshReport();
        }
    }
}