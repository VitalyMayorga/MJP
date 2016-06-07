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
    public partial class RevisionBajas : System.Web.UI.Page
    {
        public static List<Int32> Ids = new List<Int32>();
        Bitacora bitacora = new Bitacora();
        public DataTable datosBaja;
        private ControladoraDevolucionBajas controladora = new ControladoraDevolucionBajas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar.aspx");
                }
                else if (!rol.Equals("Aprobador"))
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }
                else
                {

                    llenarBajas();
                }

            }
        }
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal.aspx");
        }


        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridBajas.PageIndex = e.NewPageIndex;
            GridBajas.DataSource = datosBaja;
            GridBajas.DataBind();
        }

       
        
        //Cambia el estado de la baja y cambia la cantidad solicitada del producto en el sistema
        public void aceptar(object sender, EventArgs e)
        {
            int rowindex = 0;
            string producto = "";
            string cantidad = "";
            string programa = "";
            string bodega = "";
            string subBodega = "";

            //Get the button that raised the event
            System.Web.UI.WebControls.LinkButton btn = (System.Web.UI.WebControls.LinkButton)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            producto = gvr.Cells[0].Text;
            cantidad = gvr.Cells[1].Text;
            programa = gvr.Cells[2].Text;
            bodega = gvr.Cells[3].Text;                      
            
            //Get the rowindex
            rowindex = gvr.RowIndex;
            controladora.actualizarEstado(buscarId(rowindex), 1);

            if (gvr.Cells[4].Text.Equals("--------"))
            {                
                controladora.actualizarCantidadProducto(controladora.obtenerIDBodega(bodega), controladora.getProductoConCantidadMin(producto), controladora.obtenerIDPrograma(programa), 0, Int32.Parse(cantidad), "Baja", buscarId(rowindex));
            }
            else
            {
                subBodega = gvr.Cells[4].Text;
                controladora.actualizarCantidadProducto(controladora.obtenerIDBodega(bodega), controladora.getProductoConCantidadMin(producto), controladora.obtenerIDPrograma(programa), controladora.obtenerIDSubBodega(subBodega), Int32.Parse(cantidad), "Baja", buscarId(rowindex));
            }  
            
            Ids.RemoveAt(rowindex);
            string descripcionRA = "Baja de " + cantidad + " " + producto + " en la bodega: " + bodega + ", subBodega: " + subBodega + " al programa presupuestario: " + programa + " Rechazada";
            string usuario = (string)Session["correoInstitucional"];
            bitacora.registrarActividad(usuario, descripcionRA);
            Response.Redirect("RevisionBajas.aspx");
            
        }

        //Cambia el estado de la baja
        public void rechazar(object sender, EventArgs e)
        {
            int rowindex = 0;
            string producto = "";
            string cantidad = "";
            string programa = "";
            string bodega = "";
            string subBodega = "";
            //Get the button that raised the event
            System.Web.UI.WebControls.LinkButton btn = (System.Web.UI.WebControls.LinkButton)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            producto = gvr.Cells[0].Text;
            cantidad = gvr.Cells[1].Text;
            programa = gvr.Cells[2].Text;
            bodega = gvr.Cells[3].Text;
            subBodega = gvr.Cells[4].Text;

            //Get the rowindex
            rowindex = gvr.RowIndex;

            controladora.actualizarEstado(buscarId(rowindex), 0);
            Ids.RemoveAt(rowindex);
            string descripcionRA = "Baja de " + cantidad + " " + producto + " en la bodega: " + bodega + ", subBodega: " + subBodega + " al programa presupuestario: " + programa + " Rechazada";
            string usuario = (string)Session["correoInstitucional"];
            bitacora.registrarActividad(usuario, descripcionRA);
            Response.Redirect("RevisionBajas.aspx");

        }

        //Recorre la lista para devolver el id de la baja correspondientes
        public int buscarId(int fila)
        {            
            return Ids.ElementAt(fila);
        }

        //Llena la grid de facturas con los datos correspondientes
        internal void llenarBajas()
        {
            DataTable tabla = crearTablaBajas();
            List<Item_Grid_Bajas> data = controladora.getListaBajasPendientes();
            Object[] datos = new Object[6];
            Ids.Clear();

            foreach (Item_Grid_Bajas fila in data)
            {
                /*                     
                    Ademas ver que hago con la justificacion que es gigante,
                    podria ser un boton y que cuando se le da click o 
                    se le pasa por encima lo muestr.
                */

                Ids.Add(fila.Id);
                datos[0] = controladora.getNombreProducto(fila.Producto);
                datos[1] = fila.Cantidad;
                datos[2] = controladora.getNombrePrograma(fila.Programa);
                datos[3] = controladora.getNombreBodega(fila.Bodega);
                if (fila.SubBodega == 0)
                {//Si es 0 entonces no muestra subbodega
                    datos[4] = "--------";
                }
                else
                {

                    datos[4] = controladora.getNombreSb(fila.SubBodega);
                }
                datos[5] = fila.Justificacion;

                tabla.Rows.Add(datos);
            }
            
            datosBaja = tabla;
            GridBajas.DataSource = datosBaja;
            GridBajas.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaBajas()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Producto";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cantidad";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Programa";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Bodega";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "SubBodega";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Justificacion";
            tabla.Columns.Add(columna);


            GridBajas.DataSource = tabla;
            GridBajas.DataBind();

            return tabla;
        }

        protected void gridBajas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridBajas.Columns)
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