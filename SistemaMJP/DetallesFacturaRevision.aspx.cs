using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SistemaMJP
{
    public partial class DetallesFacturaRevision : System.Web.UI.Page
    {
        private ControladoraDetalles_Producto controladora = new ControladoraDetalles_Producto();
        Bitacora bitacora = new Bitacora();
        private DataTable datosFactura;
        public static string numFactura;
        private static int id_factura;
        private static int tipo;//tipo 1= aceptar, tipo 2= rechazar
        private static int[] ids;//se guardaran los ids de los productos de la factura
        private static int i; 
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
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {
                    labelFactura.InnerText = "Factura " + numFactura;
                    llenarDetallesProducto();
                }

            }

        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridProductos.PageIndex = e.NewPageIndex;
            GridProductos.DataSource = datosFactura;
            GridProductos.DataBind();
        }
        //Elimina el item seleccionado
        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            i = Convert.ToInt32(row.RowIndex);

            //HTMLDECODE: es necesario para leer caracteres con tilde
            String estado = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[3].Text);
            if (estado.Equals("Pendiente de aprobación"))
            {
                //Se obtiene el id del producto
                int idProducto = ids[i + (this.GridProductos.PageIndex * 10)];
                String descripcion = GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Desea rechazar el producto: " + descripcion + "?');", true);
                tipo = 2;

            }
        }
        //Dependiendo de si es aprobar o rechazar, modifica el estado para que se guarde el estado correspondiente del producto
        protected void aceptar(object sender, EventArgs e)
        {
            string estado = "";

            int idProducto = ids[i + (this.GridProductos.PageIndex * 10)];
            if (tipo == 1) {//Tambien modifica la BD para que el producto ya sea visible en dicha bodega
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRD();", true);
            }
            else if (tipo == 2) {
                estado = "Rechazado";
                controladora.cambiarEstadoProducto(id_factura, idProducto, estado);
                string descripcionRA = "Producto " + GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text + " rechazado";
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcionRA);
                llenarDetallesProducto();
            }
        }

        protected void aceptarAprobado(object sender, EventArgs e)
        {
            string fecha = txtFecha.Text;
            if (fecha.Equals(""))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRD();", true);
                txtFecha.Focus();
            }
            else
            {
                string estado = "Aprobado";
                int idProducto = ids[i + (this.GridProductos.PageIndex * 10)];
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalRD').modal('hide');</script>");
                controladora.agregarRecepcionDefinitiva(id_factura, idProducto, fecha);//Primero se agrega la fecha de RecepcionDefinitiva,por dentro agregara producto a la bodega                    
                controladora.cambiarEstadoProducto(id_factura, idProducto, estado);
                string descripcionRA = "Producto " + GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text + " aprobado";
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcionRA);
                llenarDetallesProducto();
            }
        }
        //Edita el item seleccionado
        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            i = Convert.ToInt32(row.RowIndex);

            String estado = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[3].Text);
            if (estado.Equals("Pendiente de aprobación"))
            {
                //Se obtiene el id del producto
                int idProducto = ids[i + (this.GridProductos.PageIndex * 10)];
                String descripcion = GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Desea aprobar el producto: " + descripcion + "?');", true);
                tipo = 1;

            }


        }
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }
        //Vuelve a la pantalla de lista de Facturas de la bodega
        protected void volver(object sender, EventArgs e)
        {
            Response.Redirect("RevisionFacturas");

        }
        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProducto()
        {

            CultureInfo crCulture = new CultureInfo("es-CR");
            DataTable tabla = crearTablaProductos();
            id_factura = controladora.obtenerIDFactura(numFactura);
            List<Item_Grid_Produtos_Factura> data = controladora.obtenerListaProductos(id_factura);
            Object[] datos = new Object[4];
            ids = new int[data.Count];
            int contador = 0;
            foreach (Item_Grid_Produtos_Factura fila in data)
            {
                ids[contador] = fila.Id;
                datos[0] = fila.Descripcion;
                datos[1] = fila.Cantidad.ToString();
                datos[2] = fila.PrecioTotal.ToString("C2", crCulture);
                datos[3] = fila.Estado;
                contador++;

                tabla.Rows.Add(datos);
            }
            datosFactura = tabla;
            GridProductos.DataSource = datosFactura;
            GridProductos.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaProductos()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Descripcion";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cantidad";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Precio Total";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);

            GridProductos.DataSource = tabla;
            GridProductos.DataBind();

            return tabla;
        }

        protected void gridProductos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridProductos.Columns)
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