using SistemaMJP.RequisicionesUsuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class DetallesRequisicion : System.Web.UI.Page
    {
        private string numRequisicion;
        ControladoraRequisicionesUsuario controladora = new ControladoraRequisicionesUsuario();
        ServicioLogin servicio = new ServicioLogin();
        Bitacora bitacora = new Bitacora();
        EmailManager email = new EmailManager();
        private int i;
        private int eliminado;
        private DataTable tabla;
        private string estado;
        private string producto;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {//Se revisa primero si tiene los permisos para ingresar al sitio
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Usuario"))
                {
                    Response.Redirect("MenuPrincipal");
                }

                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {//Leer los datos del URL
                    try
                    {
                        numRequisicion = (servicio.TamperProofStringDecode(Request.QueryString["numReq"], "MJP")).ToString();
                        estado = controladora.getEstadoRequisicion(numRequisicion);
                        if (!estado.Equals("En edición")) {
                            btnEnviarAprobacion.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("MenuPrincipal.aspx");
                    }

                }
                ViewState["numRequisicion"] = numRequisicion;
                ViewState["estado"] = estado;
            }
            else
            {
                try
                {
                    numRequisicion = (string)ViewState["numRequisicion"];
                    i = (int)ViewState["i"];
                    tabla = (DataTable)ViewState["tabla"];
                    eliminado = (int)ViewState["elimnado"];//0= requisicion, 1= producto
                    producto = (string)ViewState["producto"];
                }
                catch (Exception) { }
            }
        }

        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }
        //Crea una vista Previa de la factura con los productos ingresados.
        protected void btnEliminar(object sender, EventArgs e)
        {
            eliminado = 0;
            ViewState["elimnado"] = eliminado;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalEliminar('Desea eliminar la requisición?');", true);

        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridProductos.PageIndex = e.NewPageIndex;
            GridProductos.DataSource = tabla;
            GridProductos.DataBind();
        }
        //Elimina el item seleccionado
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            i = Convert.ToInt32(row.RowIndex);
            ViewState["i"] = i;
            if (estado.Equals("En edición"))
            {
                //Se obtiene el id del producto
                producto = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text);
                ViewState["producto"] = producto;
                eliminado = 1;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalEliminar('Desea eliminar el producto: " + producto + "?');", true);


            }
        }

        protected void aceptarEliminado(object sender, EventArgs e)
        {
            if (eliminado == 0)
            {
                controladora.eliminarRequisicion(numRequisicion);

            }
            else
            {
                controladora.eliminarProducto(numRequisicion, producto);
                llenarDetallesProducto();
            }
        }

        //Edita el item seleccionado
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (estado.Equals("En edición"))
            {

                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                i = Convert.ToInt32(row.RowIndex);
                ViewState["i"] = i;
                //HTMLDECODE: es necesario para leer caracteres con tilde
                String nombre = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text);
                String descripcion = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[1].Text);
                //Se obtiene el id del producto
                producto = nombre;
                ViewState["producto"] = producto;
                nombreProducto.InnerText = nombre;
                descripcionLabel.InnerHtml = descripcion;
                txtCantidad.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        //Crea un nuevo producto
        protected void nuevoProducto(object sender, EventArgs e)
        {
            List<string> datos = controladora.getDatosRequisicion(numRequisicion);
            Response.Redirect("Ingreso_Requisicion.aspx?bodega=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(datos[0], "MJP")) + "&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(datos[1], "MJP")) + "&subbodega=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(datos[2], "MJP"))
                    + "&numReq=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequisicion, "MJP")));
        }
        //Cambia el estado de la factura a pendiente de aprobación, así como todos los productos
        //Este botón solo está disponible si la factura estaba anteriormente en modo aprobación
        protected void btnEnviar(object sender, EventArgs e)
        {
            controladora.enviarAAprobacion(numRequisicion);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Requisición enviada a aprobación.\n Puede seguir el histórico del estado de la requisción en el menú requisiciones')", true);
            llenarDetallesProducto();
            string descripcionRA = "Requisición " + numRequisicion + " enviada a aprobacion programa";
            string usuario = (string)Session["correoInstitucional"];
            bitacora.registrarActividad(usuario, descripcionRA);
            List<string> bodegas = (List<string>)Session["bodegas"];
            string bodega = bodegas[0];
            List<string> correos = email.obtenerCorreosAprobadorSegunPrograma(controladora.obtenerIDRequisicion(numRequisicion));
            email.MailSender("Nueva requisición enviada a revisión por el usuario " + usuario + ".\nRequisición " + numRequisicion + ".", "Notificación de solicitud de aprobación de requisición", correos);
        }
        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProducto()
        {
            DataTable tablaP = crearTablaRequisiciones();
            List<Item_Grid_Productos_Bodega> data = controladora.getListaProductosRequisicion(numRequisicion);
            Object[] datos = new Object[3];
            foreach (Item_Grid_Productos_Bodega fila in data)
            {
                datos[0] = fila.Nombre;
                datos[1] = fila.Descripcion;
                datos[2] = fila.Unidad;
                tabla.Rows.Add(datos);
            }

            tabla = tablaP;
            ViewState["tabla"] = tabla;
            GridProductos.DataSource = tabla;
            GridProductos.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaRequisiciones()//consultar
        {
            DataTable tabl = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Descripción";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Unidad Medida";
            tabla.Columns.Add(columna);

            GridProductos.DataSource = tabl;
            GridProductos.DataBind();
            return tabl;
        }

        //Pone las columnas con botones al final de la tabla
        protected void gridRequisiciones_RowCreated(object sender, GridViewRowEventArgs e)
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