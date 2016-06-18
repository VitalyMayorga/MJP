using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class RevisionFacturas : System.Web.UI.Page
    {
        public DataTable datosFactura;
        private ControladoraFacturas controladora = new ControladoraFacturas();
        ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Administrador Almacen"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {

                    llenarFacturas();
                }

            }
        }
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }

        
        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridFacturas.PageIndex = e.NewPageIndex;
            GridFacturas.DataSource = datosFactura;
            GridFacturas.DataBind();
        }

        //Obtiene el id de la factura seleccionada y redirecciona a la pantalla DetallesFactura
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);


            string numFactura = GridFacturas.Rows[i + (this.GridFacturas.PageIndex * 10)].Cells[0].Text;
            string estado = GridFacturas.Rows[i + (this.GridFacturas.PageIndex * 10)].Cells[6].Text;
            if (estado.Equals("Pendiente de Aprobación")) {
                Response.Redirect("DetallesFacturaRevision.aspx?numF=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numFactura, "MJP")));
            
            }

        }
        //Llena la grid de facturas con los datos correspondientes
        internal void llenarFacturas()
        {

            CultureInfo crCulture = new CultureInfo("es-CR");
            DataTable tabla = crearTablaFacturas();
            List<string> bodegas = (List<string>)Session["bodegas"];
            string bodega = bodegas[0];
            List<Item_Grid_Facturas> data = controladora.getListaFacturasAdmin(bodega);
            Object[] datos = new Object[7];

            foreach (Item_Grid_Facturas fila in data)
            {

                datos[0] = fila.NumFactura;
                datos[1] = fila.Fecha;
                datos[2] = fila.Proveedor;
                datos[3] = fila.Programa;
                if (fila.SubBodega == 0)
                {//Si es 0 entonces no muestra subbodega
                    datos[4] = "--------";
                }
                else
                {

                    datos[4] = controladora.getNombreSb(fila.SubBodega);
                }
                datos[5] = fila.Monto.ToString("C2", crCulture);
                datos[6] = fila.Estado;

                tabla.Rows.Add(datos);
            }

            datosFactura = tabla;
            GridFacturas.DataSource = datosFactura;
            GridFacturas.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaFacturas()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Factura";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Fecha";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Proveedor";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Programa";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "SubBodega";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Monto Total";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);


            GridFacturas.DataSource = tabla;
            GridFacturas.DataBind();

            return tabla;
        }

        protected void gridFacturas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridFacturas.Columns)
                {
                    //Get the first Cell /Column
                    TableCell cell = row.Cells[0];
                    // Then Remove it after
                    row.Cells.Remove(cell);
                    //And Add it to the List Collections
                    columns.Add(cell);
                }
                // Add cells
                row.Cells.AddRange(columns.ToArray());
            }
        }
    }
}