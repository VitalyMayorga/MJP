﻿using System;
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
    public partial class Boleta : System.Web.UI.Page
    {
        private string numReq;
        private string idReq;
        private string comodin;
        private string conductor;
        private string placa;
        private string destinatario;
        private DateTime fecha;
        private ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            string DataString = servicio.TamperProofStringDecode(Request.QueryString["numReq"], "MJP");
            string DataString2 = servicio.TamperProofStringDecode(Request.QueryString["idReq"], "MJP");
            numReq = DataString;
            idReq = DataString2;
            CultureInfo crCulture = new CultureInfo("es-CR");
            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(Server.MapPath("~/Boleta_Reporte.rpt"));
            DatosFacturaAgregada ds = new DatosFacturaAgregada();
            DataTable t = ds.Tables.Add("Items");
            t.Columns.Add("Item", Type.GetType("System.String"));
            t.Columns.Add("Descripcion", Type.GetType("System.String"));
            t.Columns.Add("Cantidad", Type.GetType("System.String"));
            
            DataRow r;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Productos_Requisicion";
                con.Open();
                cmd.Parameters.AddWithValue("@numReq", numReq);
                SqlDataReader reader = cmd.ExecuteReader();
                int contador = 1;
                while (reader.Read())
                {
                    r = t.NewRow();
                    r["Item"] = contador.ToString();
                    r["Descripcion"] = reader.GetString(0);
                    comodin=reader.GetString(1);  
                    r["Cantidad"] = reader.GetInt32(2).ToString();                                      
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
                cmd.CommandText = "P_Obtener_Fecha_Despacho";
                con.Open();
                cmd.Parameters.AddWithValue("@numReq", numReq);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    fecha = reader.GetDateTime(0);
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
                cmd.CommandText = "P_Obtener_InfoDespacho";
                con.Open();
                cmd.Parameters.AddWithValue("@idRequisicion", Int32.Parse(idReq));
                SqlDataReader reader = cmd.ExecuteReader();               
                while (reader.Read())
                {
                    placa = reader.GetString(0);
                    conductor=reader.GetString(1);
                    destinatario = reader.GetString(2);  
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

            TextObject conductorT = (TextObject)reportdocument.ReportDefinition.Sections["Section4"].ReportObjects["Conductor"];
            conductorT.Text = conductor;

            TextObject placaT = (TextObject)reportdocument.ReportDefinition.Sections["Section4"].ReportObjects["Placa"];
            placaT.Text = placa;

            TextObject destinatarioT = (TextObject)reportdocument.ReportDefinition.Sections["Section4"].ReportObjects["Destinatario"];
            destinatarioT.Text = destinatario;

            //CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //txtReportHeader = reportdocument.ReportClientDocument.ReportObjects["numeroFactura"] as TextObject;
            //txtReportHeader.Text = "FacturaTesting";
            reportdocument.SetDataSource(ds.Tables[1]);
            CrystalReportViewer1.ReportSource = reportdocument;
            CrystalReportViewer1.RefreshReport();
        }
    }
}