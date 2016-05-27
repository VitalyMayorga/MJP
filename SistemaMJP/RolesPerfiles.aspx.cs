using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SistemaMJP
{
    public partial class RolesPerfiles : System.Web.UI.Page
    {
        Bitacora bitacora = new Bitacora();
        public DataTable datosUsuario;
        private ControladoraRolesPerfiles controladora = new ControladoraRolesPerfiles(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar.aspx");
                }
                else if (!rol.Equals("Administrador General"))
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("Administracion.aspx");
                }
                else
                {
                    llenarUsuarios();
                    // Resize the master DataGridView columns to fit the newly loaded data.
                }
            }
        }


        protected void regresarMA(object sender, EventArgs e)
        {
            Response.Redirect("Administracion.aspx");
        }

        protected void crear(object sender, EventArgs e)
        {
            Response.Redirect("CrearUsuario.aspx");

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
            string nombre = "";
            string apellidos = "";
            string rol = "";
           
            //Get the button that raised the event
            System.Web.UI.WebControls.LinkButton btn = (System.Web.UI.WebControls.LinkButton)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            nombre = gvr.Cells[0].Text;
            apellidos = gvr.Cells[1].Text;
            rol = gvr.Cells[2].Text;
            EditarUsuario.nombre = nombre;
            EditarUsuario.apellidos = apellidos;
            EditarUsuario.rol = rol;
            Response.Redirect("EditarUsuario.aspx");
        }

        protected void btnEliminar_Click(object sender, System.EventArgs e)
        {
            string nombre="";
            string apellidos="";
            if (MessageBox.Show("Esta seguro que desea borrar al usuario del sistema?", "Confirmar borrado", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Get the button that raised the event
                System.Web.UI.WebControls.LinkButton btn = (System.Web.UI.WebControls.LinkButton)sender;

                //Get the row that contains this button
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
               nombre= gvr.Cells[0].Text;
               apellidos = gvr.Cells[1].Text;
               controladora.eliminarUsuario(nombre, apellidos);
               string descripcionRA = "Usuario " + nombre + " " + apellidos + " eliminado";
               string usuario = (string)Session["correoInstitucional"];
               bitacora.registrarActividad(usuario, descripcionRA);
               Response.Redirect("Administracion.aspx");
            }
            
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