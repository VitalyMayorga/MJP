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
        public DataTable datosFactura;
        public static string numFactura;
        
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {


        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {


        }
        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProducto()
        {
            //DataTable tabla = crearTablaProductos();
            //List<string> bodegas = (List<string>)Session["bodegas"];
            //string bodega = bodegas[0];
            //List<Item_Grid_Produtos_Factura> data = controladora.getListaFacturas(bodega);
            //Object[] datos = new Object[7];

            //foreach (Item_Grid_Produtos_Factura fila in data)
            //{

            //    datos[0] = fila.NumFactura;
            //    datos[1] = fila.Fecha;
            //    datos[2] = fila.Proveedor;
            //    datos[3] = fila.Programa;
            //    datos[5] = fila.Monto.ToString();
            //    datos[6] = fila.Estado;

            //    tabla.Rows.Add(datos);
            //}

            //datosFactura = tabla;
            //GridProductos.DataSource = datosFactura;
            //GridProductos.DataBind();

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