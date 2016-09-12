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
        private int idFactura;
        private string numFactura;
        private string proveedor;
        private DateTime fecha;
        private Decimal total;
        private ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            string DataString = servicio.TamperProofStringDecode(Request.QueryString["id_factura"], "MJP");
            idFactura = Convert.ToInt32(DataString);
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
                int contador = 1;
                while (reader.Read())
                {
                    r = t.NewRow();
                    r["ITEM"] = contador.ToString();
                    r["CANTIDAD"] = reader.GetInt32(1).ToString();
                    r["DESCRIPCION"] = reader.GetString(2);
                    decimal precioTotal = reader.GetDecimal(3);
                    decimal precioUnitario = precioTotal/reader.GetInt32(1);
                    r["PRECIO UNIT."] = precioUnitario.ToString("C2", crCulture);
                    r["PRECIO TOTAL"] = precioTotal.ToString("C2", crCulture);
                    t.Rows.Add(r);
                    contador++;
                }

                
                reader.Close();
                con.Close();

            }


            catch (Exception)
            {
                throw;
            }

            try
            {//Segmento que trae los datos a mostrar en la vista previa de la factura no relacionados con los productos
                //numFactura,proveedor,fecha inclusion
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Factura_DatosVista";
                con.Open();
                cmd.Parameters.AddWithValue("@id", idFactura);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                numFactura = reader.GetString(0);
                proveedor = reader.GetString(1);
                fecha = reader.GetDateTime(2);
                total = reader.GetDecimal(3);
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            TextObject factura = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["numeroFactura"];
            factura.Text = numFactura;

            TextObject proveedorT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["proveedor"];
            proveedorT.Text = proveedor;

            TextObject fechaT = (TextObject)reportdocument.ReportDefinition.Sections["Section2"].ReportObjects["fecha"];
            fechaT.Text = fecha.ToString("dd/MM/yyyy");

            TextObject totalT = (TextObject)reportdocument.ReportDefinition.Sections["Section4"].ReportObjects["totalFacturado"];
            totalT.Text = total.ToString("C2", crCulture);
            

            //CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //txtReportHeader = reportdocument.ReportClientDocument.ReportObjects["numeroFactura"] as TextObject;
            //txtReportHeader.Text = "FacturaTesting";
            reportdocument.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.RefreshReport();
        }
    }
}