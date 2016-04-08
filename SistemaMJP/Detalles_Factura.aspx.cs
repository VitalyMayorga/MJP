using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Detalles_Factura : System.Web.UI.Page
    {
        private ControladoraDetalles_Producto controladora = new ControladoraDetalles_Producto();
        public DataTable datosFactura;
        public static string numFactura;
        public static int id_factura;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Inclusion Pedidos"))
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
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }
        //Crea una vista Previa de la factura con los productos ingresados.
        protected void vistaPrevia(object sender, EventArgs e)
        {
            //Se debe encargar de generar una vista previa imprimible de la factura

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
        protected void btnEliminar_Click(object sender, EventArgs e)
        {


        }
        //Edita el item seleccionado
        protected void btnEditar_Click(object sender, EventArgs e)
        {


        }
        //Crea un nuevo producto
        protected void nuevoProducto(object sender, EventArgs e)
        {


        }
        //Cambia el estado de la factura a pendiente de aprobación, así como todos los productos
        //Este botón solo está disponible si la factura estaba anteriormente en modo aprobación
        protected void enviarAAprobacion(object sender, EventArgs e)
        {


        }
        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProducto()
        {
            DataTable tabla = crearTablaProductos();
            id_factura = controladora.obtenerIDFactura(numFactura);
            List<Item_Grid_Produtos_Factura> data = controladora.obtenerListaProductos(id_factura);
            Object[] datos = new Object[4];

            foreach (Item_Grid_Produtos_Factura fila in data)
            {

                datos[0] = fila.Descripcion;
                datos[1] = fila.Cantidad.ToString();
                datos[2] = fila.PrecioTotal.ToString();
                datos[3] = fila.Estado;
                

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