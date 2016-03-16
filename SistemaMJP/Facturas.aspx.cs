using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Facturas1 : System.Web.UI.Page
    {
        public DataTable datosFactura;
        private ControladoraFacturas controladora = new ControladoraFacturas();        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarFacturas();

            }
        }

        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }

        protected void ingresar(object sender, EventArgs e)
        {
            Response.Redirect("Preingreso-Productos");
            
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

        protected void btnVer_Click(object sender, EventArgs e)
        {


        }
        protected void btnEditar_Click(object sender, EventArgs e)
        { 
            
        
        }
        //Llena la grid de facturas con los datos correspondientes
        internal void llenarFacturas()
        {
            DataTable tabla = crearTablaFacturas();
            string bodega = (string)Session["bodegas"];
            List<Item_Grid_Facturas> data = controladora.getListaFacturas(bodega);
            Object[] datos = new Object[7];
            
            foreach (Item_Grid_Facturas fila in data)
            {

                datos[0] = fila.NumFactura;
                datos[1] = fila.Fecha;
                datos[2] = fila.Proveedor;
                datos[3] = fila.Programa;
                datos[4] = fila.SubBodega.ToString();
                datos[5] = fila.Monto.ToString();
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
    }
}