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
    public partial class DetallesRequisicionRevision : System.Web.UI.Page
    {
        private ControladoraDetalles_Producto_Requisicion controladora = new ControladoraDetalles_Producto_Requisicion();
        EmailManager email = new EmailManager();
        Bitacora bitacora = new Bitacora();
        ServicioLogin servicio = new ServicioLogin();
        private DataTable datosRequisicion;
        public  string numRequisicion;
        private int id_row;
        private  int id_requisicion;
        private  int[] ids;//se guardaran los ids de los productos de la requisicion 
        private int[] cantidades;//se guardaran las cantidades de los productos de la requisicion 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Aprobador") && !rol.Equals("Revision y Aprobador Almacen"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {
                    try
                    {
                        numRequisicion = servicio.TamperProofStringDecode(Request.QueryString["numR"], "MJP");
                    }
                    catch (Exception)
                    {
                        Response.Redirect("MenuPrincipal");
                    }
                    labelRequisicion.InnerText = "Requisicion " + numRequisicion;
                    llenarDetallesProductoRequisicion();
                    ViewState["numRequisicion"] = numRequisicion;
                }

            }
            else {
                numRequisicion = (string)ViewState["numRequisicion"];
                id_requisicion = (int)ViewState["id_requisicion"];
                ids = (int[])ViewState["ids"];
                cantidades = (int[])ViewState["cantidades"];
            }

        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridProductosRequisicion.PageIndex = e.NewPageIndex;
            GridProductosRequisicion.DataSource = datosRequisicion;
            GridProductosRequisicion.DataBind();
        }

        //Se encarga de las revisiciones previas al despacho de la requisicion
        protected void btnDespachar_Click(object sender, EventArgs e)
        {
            bool activosAsignados = true;
            int indice=0;
           
            while (indice < ids.Count() && activosAsignados)
            {
                if(controladora.esActivo(ids[indice])){
                    if (!(controladora.obtenerCantidadActivos(id_requisicion, ids[indice]) == cantidades[indice]))
                    {
                        MsjErrorCantActivo.Style.Add("display", "block");
                        activosAsignados = false;                        
                    }
                }
                indice++;
            }

            if (activosAsignados)
            {
                indice = 0;
                controladora.cambiarEstadoRequisicion(id_requisicion, 0);
                while (indice < ids.Count())
                {
                    controladora.actualizarCantidadProductosRequisicion(controladora.obtenerIDBodegaRequisicion(id_requisicion), ids[indice], controladora.obtenerIDProgramaRequisicion(id_requisicion), controladora.obtenerIDSubBodegaRequisicion(id_requisicion), cantidades[indice]);
                    indice++;
                }
                Response.Redirect("RevisionRequisiciones.aspx"); 
            }
        }

        //Cambia el estado de la requisicion segun el rol del usuario que realizo la devolucion
        protected void aceptarDevolucion(object sender, EventArgs e)
        {
            string justificacion = justificacionDevolucion.Value;
            string rol = (string)Session["rol"];
            string descripcionRA = "";
            List<string> correos = null;
            if (justificacionDevolucion.Value.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Justificación rechazo requisicion: " + numRequisicion+"');", true);
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetalles').modal('show');</script>");
            }
            else{
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetalles').modal('hide');</script>");
                
                if (rol.Equals("Revision y Aprobador Almacen"))
                {
                    controladora.cambiarEstadoRequisicion(id_requisicion, 2);// Revisar como se obtiene el id de la requisicion
                    correos = email.obtenerCorreosAprobadorSegunPrograma(id_requisicion);
                    descripcionRA = "Requisicion: " + numRequisicion + " devuelta a aprobador programa";
                }
                else
                {
                    controladora.cambiarEstadoRequisicion(id_requisicion, 3);
                    correos = email.obtenerCorreoUsuarioRequisicion(id_requisicion);
                    descripcionRA = "Requisicion: " + numRequisicion + " devuelta a usuario";
                }
                //email.MailSender(justificacion, "Notificación de devolucion de requisicion", correos);
                controladora.actualizarObservacion(id_requisicion, justificacion);
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcionRA);
                Response.Redirect("RevisionRequisiciones.aspx");                
            }
        }       

        //Envia la requisicion a los aprobadores de almacen
        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            controladora.cambiarEstadoRequisicion(id_requisicion, 1);
            Response.Redirect("RevisionRequisiciones.aspx");
        }

        //Antes de devolver la requisicion abre el panel para escribir la observacion
        protected void btnDevolver_Click(object sender, EventArgs e)
        {            
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Justificación devolucion requisicion: " + numRequisicion + "');", true);            
        }

        //Redirecciona a la pantalla de editar linea
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("DetallesRequisicionRevision.aspx?numR=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequisicion, "MJP")));
        }

        //Pregunta si deseaeliminar la linea seleccionada
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            id_row = Convert.ToInt32(row.RowIndex);           

            //Se obtiene el id del producto            
            String descripcion = GridProductosRequisicion.Rows[id_row + (this.GridProductosRequisicion.PageIndex * 10)].Cells[0].Text;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalDelete('Desea eliminar el producto: " + descripcion + "?');", true);            
        }

        //Elimina la linea deseada de la requisicion
        protected void aceptarEliminado(object sender, EventArgs e)
        {
            int idProducto = ids[id_row + (this.GridProductosRequisicion.PageIndex * 10)];
            controladora.eliminarProductoRequisicion(numRequisicion, idProducto);
            llenarDetallesProductoRequisicion();
        }

        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }        

        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProductoRequisicion()
        {
            string rol = (string)Session["rol"];
            if (rol.Equals("Aprobador"))
            {
                btnAprobador.Style.Add("display", "block");
            }
            else
            {
                btnAlmacen.Style.Add("display", "block");
            }            
            
            CultureInfo crCulture = new CultureInfo("es-CR");
            DataTable tabla = crearTablaProductosRequisicion();
            id_requisicion = controladora.obtenerIDRequisicion(numRequisicion);
            ViewState["id_requisicion"] = id_requisicion;
            List<Item_Grid_Produtos_Requisicion> data = controladora.obtenerListaProductos(id_requisicion);
            Object[] datos = new Object[2];
            ids = new int[data.Count];
            ViewState["ids"] = ids;
            cantidades = new int[data.Count];
            ViewState["cantidades"] = cantidades;
            int contador = 0;
            foreach (Item_Grid_Produtos_Requisicion fila in data)
            {
                ids[contador] = fila.Producto;
                datos[0] = controladora.getNombreProducto(fila.Producto);
                datos[1] = fila.Cantidad.ToString();
                cantidades[contador] = fila.Cantidad;
                contador++;

                tabla.Rows.Add(datos);
            }
            datosRequisicion = tabla;
            GridProductosRequisicion.DataSource = datosRequisicion;
            GridProductosRequisicion.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaProductosRequisicion()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Producto";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cantidad Solicitada";
            tabla.Columns.Add(columna);            

            GridProductosRequisicion.DataSource = tabla;
            GridProductosRequisicion.DataBind();

            return tabla;
        }

        protected void gridProductosRequisicion_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridProductosRequisicion.Columns)
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

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string rol = (string)Session["rol"];
            if (rol.Equals("Revision y Aprobador Almacen"))
            {
               e.Row.Cells[2].Visible = false;
               e.Row.Cells[3].Visible = false;
           }
        }

    }
}