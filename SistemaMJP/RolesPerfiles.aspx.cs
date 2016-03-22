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
    public partial class RolesPerfiles : System.Web.UI.Page
    {
        public DataTable datosUsuario;
        private ControladoraRolesPerfiles controladora = new ControladoraRolesPerfiles(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Administrador General"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {

                    llenarUsuarios();
                }

            }
        }


        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }

        protected void crear(object sender, EventArgs e)
        {
            Response.Redirect("CrearUsuario");

        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridUsuarios.PageIndex = e.NewPageIndex;
            GridUsuarios.DataSource = datosUsuario;
            GridUsuarios.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {


        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {


        }

        //Llena la grid de usuarios con los datos correspondientes
        internal void llenarUsuarios()
        {
            DataTable tabla = crearTablaUsuarios();

            List<Item_Grid_Usuarios> data = controladora.getListaUsuarios();
            Object[] datos = new Object[3];

            foreach (Item_Grid_Usuarios fila in data)
            {

                datos[0] = fila.Nombre;
                datos[1] = fila.Apellido;
                datos[2] = fila.NomRol;
              
                tabla.Rows.Add(datos);
            }

            datosUsuario = tabla;
            GridUsuarios.DataSource = datosUsuario;
            GridUsuarios.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaUsuarios()
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Apellido";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Rol";
            tabla.Columns.Add(columna);

            GridUsuarios.DataSource = tabla;
            GridUsuarios.DataBind();

            return tabla;
        }

        protected void gridUsuario_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridUsuarios.Columns)
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