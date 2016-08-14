using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Requisiciones : System.Web.UI.Page
    {
        public DataTable requisiciones;
        ControladoraRequisicionesUsuario controladora = new ControladoraRequisicionesUsuario();
        private ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Usuario"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {

                    llenarRequisiciones();
                }

            }
            else {
                requisiciones = (DataTable)ViewState["tabla"];
            }
        }
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }

        protected void ingresar(object sender, EventArgs e)
        {
            Response.Redirect("Pre-ingresoRequisicion.aspx");

        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridRequisiciones.PageIndex = e.NewPageIndex;
            GridRequisiciones.DataSource = requisiciones;
            GridRequisiciones.DataBind();
        }

        //Obtiene el id de la requisicion seleccionada y redirecciona a la pantalla DetallesRequisicion
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);
            string numRequisicion = GridRequisiciones.Rows[i + (this.GridRequisiciones.PageIndex * 10)].Cells[0].Text;
            Response.Redirect("DetallesRequisicion.aspx?numReq=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequisicion, "MJP")));
            

        }

        //Obtiene el id de la requisicion y se abre un modal con la tabla del historico de dicha requisicion
        protected void btnVer_Click(object sender, EventArgs e)
        {
            //Aqui se debe de abrir el modal con el historico de la requisicion seleccionada
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);
            string numRequisicion = GridRequisiciones.Rows[i + (this.GridRequisiciones.PageIndex * 10)].Cells[0].Text;

        }

        //Llena la grid de facturas con los datos correspondientes
        internal void llenarRequisiciones()
        {
            int usuario = (int)Session["userID"];
            DataTable tabla = crearTablaRequisiciones();
            List<string> bodegas = (List<string>)Session["bodegas"];
            List<Item_Grid_Requisiciones> data = new List<Item_Grid_Requisiciones>();
            foreach (string bodega in bodegas) { 
                data.AddRange(controladora.getListaRequisiciones(bodega,usuario));
            }
            Object[] datos = new Object[4];

            foreach (Item_Grid_Requisiciones fila in data)
            {
                //
                datos[0] = fila.NumRequisicion;
                datos[1] = fila.Fecha;
                datos[2] = fila.Unidad;
                datos[3] = fila.Estado;
                

                tabla.Rows.Add(datos);
            }

            requisiciones = tabla;
            ViewState["tabla"] = tabla;
            GridRequisiciones.DataSource = requisiciones;
            GridRequisiciones.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaRequisiciones()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Requisición";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Fecha";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Unidad Solicitante";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);

            GridRequisiciones.DataSource = tabla;
            GridRequisiciones.DataBind();

            return tabla;
        }

        protected void gridRequisiciones_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridRequisiciones.Columns)
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