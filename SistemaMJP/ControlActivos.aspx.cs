using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class ControlActivos : System.Web.UI.Page
    {
        public DataTable datosActivos;
        private int i;
        private String numActivo;
        ControladoraActivos controladora = new ControladoraActivos();
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
                else if (rol.Equals("Inclusión Pedidos") || rol.Equals("Administrador Almacen"))
                {
                    llenarActivos();
                }
                else
                {
                    Response.Redirect("MenuPrincipal");
                    
                }

            }
            else
            {
               try {
                   numActivo = (String)ViewState["numActivo"];
                    i = (int)ViewState["i"];                    
               }
               catch (Exception)
               {
                  
               }
            }
        }
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }

        protected void ingresar(object sender, EventArgs e)
        {
            //Activos.editar = false;
            Response.Redirect("Activos.aspx?editar=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode("false", "MJP")));

        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridActivos.PageIndex = e.NewPageIndex;
            GridActivos.DataSource = datosActivos;
            GridActivos.DataBind();
        }

        //Obtiene el id de la factura seleccionada y redirecciona a la pantalla DetallesFactura
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);


            numActivo = GridActivos.Rows[i + (this.GridActivos.PageIndex * 10)].Cells[0].Text;
            ViewState["i"] = i;
            ViewState["numActivo"] = numActivo;
            //Activos.editar = true;
            //Activos.numActivo = numActivo;
            Response.Redirect("Activos.aspx?editar=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode("true", "MJP")) + "&numActivo=" +
             HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numActivo, "MJP")));
        }

        //Elimina el item seleccionado
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            i = Convert.ToInt32(row.RowIndex);

            //HTMLDECODE: es necesario para leer caracteres con tilde
            numActivo = HttpUtility.HtmlDecode(GridActivos.Rows[i + (this.GridActivos.PageIndex * 10)].Cells[0].Text);
            ViewState["i"] = i;
            ViewState["numActivo"] = numActivo;
            String descripcion = HttpUtility.HtmlDecode(GridActivos.Rows[i + (this.GridActivos.PageIndex * 10)].Cells[1].Text);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Desea eliminar el activo asignado: " + descripcion + "?');", true);
                        
        }

        protected void aceptarEliminado(object sender, EventArgs e)
        {
            controladora.eliminarActivo(numActivo);
            llenarActivos();
        }
        //Llena la grid de facturas con los datos correspondientes
        internal void llenarActivos()
        {

            DataTable tabla = crearTablaFacturas();
            List<string> bodegas = (List<string>)Session["bodegas"];
            string bodega = bodegas[0];
            List<Item_Grid_Activos> data = controladora.getListaActivos(bodega);
            Object[] datos = new Object[6];

            foreach (Item_Grid_Activos fila in data)
            {

                datos[0] = fila.Numero;
                datos[1] = fila.Producto;
                datos[2] = fila.Funcionario;
                datos[3] = fila.Cedula;
                datos[4] = fila.Fecha.ToString("dd/MM/yyyy");
                datos[5] = fila.Documento;
               

                tabla.Rows.Add(datos);
            }

            datosActivos = tabla;
            GridActivos.DataSource = datosActivos;
            GridActivos.DataBind();

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
            columna.ColumnName = "Número Activo";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Producto";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Funcionario Asignado";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cédula";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Fecha";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Documento";
            tabla.Columns.Add(columna);

            GridActivos.DataSource = tabla;
            GridActivos.DataBind();

            return tabla;
        }

        protected void gridActivos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridActivos.Columns)
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