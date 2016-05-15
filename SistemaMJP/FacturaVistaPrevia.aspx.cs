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
    public partial class FacturaVistaPrevia : System.Web.UI.Page
    {
        public static int idFactura;
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo crCulture = new CultureInfo("es-CR");
            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(Server.MapPath("~/Factura_vista_previa.rpt"));
            DatosFacturaAgregada ds = new DatosFacturaAgregada();
            DataTable t = ds.Tables.Add("Items");
            t.Columns.Add("ITEM", Type.GetType("System.String"));
            t.Columns.Add("CANTIDAD", Type.GetType("System.String"));
            t.Columns.Add("DESCRIPCION", Type.GetType("System.String"));
            t.Columns.Add("PRECIO UNIT.", Type.GetType("System.String"));
            t.Columns.Add("PRECIO TOTAL", Type.GetType("System.String"));

            DataRow r;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Factura_VistaPrevia";
                con.Open();
                cmd.Parameters.AddWithValue("@idFactura", idFactura);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    r = t.NewRow();
                    r["ITEM"] = reader.GetString(0);
                    r["CANTIDAD"] = reader.GetInt32(1).ToString();
                    r["DESCRIPCION"] = reader.GetString(2);
                    decimal precioTotal = reader.GetDecimal(3);
                    decimal precioUnitario = precioTotal/reader.GetInt32(1);
                    r["PRECIO UNIT."] = precioUnitario.ToString("C2", crCulture);
                    r["PRECIO TOTAL"] = precioTotal.ToString("C2", crCulture);
                    t.Rows.Add(r);
                }

                
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            

            //CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //txtReportHeader = reportdocument.ReportClientDocument.ReportObjects["numeroFactura"] as TextObject;
            //txtReportHeader.Text = "FacturaTesting";
            reportdocument.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.RefreshReport();
        }
    }
}