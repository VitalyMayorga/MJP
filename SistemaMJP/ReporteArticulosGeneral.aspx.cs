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
    public partial class ReporteArticulosGeneral : System.Web.UI.Page
    {
      /*  private string idPrograma;
        private string programa;
        private DateTime fecha;
        private ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            string DataString = servicio.TamperProofStringDecode(Request.QueryString["idPrograma"], "MJP");
            string DataString2 = servicio.TamperProofStringDecode(Request.QueryString["programa"], "MJP");
            idPrograma = DataString;
            programa = DataString2;
            CultureInfo crCulture = new CultureInfo("es-CR");
            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(Server.MapPath("~/Reporte_Existencia.rpt"));
            Tabla_Existencias ds = new Tabla_Existencias();
            DataTable t = ds.Tables.Add("Items");
            t.Columns.Add("SubPartida", Type.GetType("System.String"));
            t.Columns.Add("Descripcion", Type.GetType("System.String"));
            t.Columns.Add("Cantidad", Type.GetType("System.String"));
            t.Columns.Add("TipoEmpaque", Type.GetType("System.String"));
            t.Columns.Add("PrecioUnitario", Type.GetType("System.String"));
            t.Columns.Add("PrecioTotal", Type.GetType("System.String"));
            
            DataRow r;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_reporte_existencias";
                con.Open();
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    r = t.NewRow();
                    r["SubPartida"] = reader.GetString(0);
                    r["Descripcion"] = reader.GetString(1);
                    r["Cantidad"] = reader.GetInt32(2).ToString();
                    r["TipoEmpaque"] = reader.GetString(3);
                    r["PrecioUnitario"] = reader.GetDecimal(4).ToString();
                    r["PrecioTotal"] = reader.GetDecimal(5).ToString();
                    t.Rows.Add(r);
                }
                
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }  
            
            TextObject fechaT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Fecha"];
            fechaT.Text = fecha.ToString("dd/MM/yyyy");

            TextObject programaT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["Programa"];
            programaT.Text = programa;

            //CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //txtReportHeader = reportdocument.ReportClientDocument.ReportObjects["numeroFactura"] as TextObject;
            //txtReportHeader.Text = "FacturaTesting";
            reportdocument.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.RefreshReport();*/
        }
    }
