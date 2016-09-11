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
        private String nombre;
        private String descripcion;
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
                        if (!estado.Equals("En edición") && !estado.Equals("Devuelto a Usuario")) {
                            btnEnviarAprobacion.Enabled = false;

                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("MenuPrincipal.aspx");
                    }
                    llenarDetallesProducto();

                }
                ViewState["numRequisicion"] = numRequisicion;
                ViewState["estado"] = estado;
            }
            else
            {
                try
                {
                    numRequisicion = (string)ViewState["numRequisicion"];
                    producto = (string)ViewState["producto"];
                    estado = (string)ViewState["estado"];
                    tabla = (DataTable)ViewState["tabla"];
                    descripcion = (string)ViewState["descripcion"];
                    nombre = (string)ViewState["nombre"];
                    eliminado = (int)ViewState["elimnado"];//0= requisicion, 1= producto
                    i = (int)ViewState["i"];
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
            if (estado.Equals("En edición") || estado.Equals("Devuelto a Usuario"))
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
                Response.Redirect("Requisiciones.aspx");
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
                 nombre = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text);
                 descripcion = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[1].Text);
                //Se obtiene el id del producto
                producto = nombre;
                ViewState["producto"] = producto;
                txtCantidad.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('" + nombre + "','" + descripcion + "');", true);
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
            List<string> datosReq = controladora.getDatosRequisicion(numRequisicion);
            List<string> correos = email.obtenerCorreosAprobador(Convert.ToInt32(datosReq[1]),Convert.ToInt32(datosReq[0]),Convert.ToInt32(datosReq[2]));
            email.MailSender("Nueva requisición enviada a revisión por el usuario " + usuario + ".\nRequisición " + numRequisicion + ".", "Notificación de solicitud de aprobación de requisición", correos);
        }
        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProducto()
        {
            DataTable tablaP = crearTablaRequisiciones();
            List<Item_Grid_Productos_Bodega> data = controladora.getListaProductosRequisicion(numRequisicion);
            Object[] datos = new Object[2];
            foreach (Item_Grid_Productos_Bodega fila in data)
            {
                datos[0] = fila.Nombre;
                datos[1] = fila.Unidad;
                tablaP.Rows.Add(datos);
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
            tabl.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Unidad Medida";
            tabl.Columns.Add(columna);

            GridProductos.DataSource = tabl;
            GridProductos.DataBind();
            return tabl;
        }

        //Pone las columnas con botones al final de la tabla
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

        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y se mantiene en la página para nuevo ingreso de productos
        protected void aceptar(object sender, EventArgs e)
        {
            if (validar())
            {//Si todo es valido, entonces se procede a guardar el producto en la requisicion
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                controladora.editarProducto(producto, numRequisicion, cantidad);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        //Revisa la cantidad ingresada por el usuario vs lo que hay en el almacen
        protected bool validar()
        {
            bool correcto = false;
            int cantSugeridaFinal = 0;
            int i = 0;
            try
            {
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                if (cantidad > 0)
                {//Aqui esta el algoritmo necesario para indicar cantidades sugeridas o si se puede o no hacer el pedido con la cantidad ingresada
                    MsjErrorPrograma.Style.Add("display", "none");
                    List<string> datos = controladora.getDatosRequisicion(numRequisicion);
                    int cantidadEnBodega = controladora.obtenerCantidadProductoBodega(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[2]), datos[1], producto);
                    List<int> empaques = controladora.obtenerEmpaque(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[2]), datos[1], producto);
                    List<int> cantPorEmpaque = controladora.obtenerCantPorEmpaque(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[2]), datos[1], producto);
                    if (cantidad > cantidadEnBodega)
                    {
                        MensajeErrorTxt.InnerText = "Cantidad ingresada sobrepasa la cantidad en almacén";
                        MsjErrorPrograma.Style.Add("display", "block");
                    }
                    else
                    {
                        int residuoCercano = 99999; //Usado para recomendar siempre el empaque mas cercano                        
                        int cantidadSugerida;
                        foreach (int empaque in empaques)
                        {
                            int residuo = cantidad % empaque;
                            if (residuo == 0 && cantidad <= cantPorEmpaque[i])
                            {
                                MsjErrorPrograma.Style.Add("display", "none");
                                correcto = true;
                            }
                            else if (!correcto)
                            {
                                if (residuo < empaque / 2)
                                {//caso cantidad es menor al 49%
                                    cantidadSugerida = cantidad - residuo;
                                }
                                else
                                {//cantidad es mayor al 49%
                                    cantidadSugerida = (cantidad - residuo) + empaque;
                                }
                                if (residuo < residuoCercano)
                                {
                                    residuoCercano = residuo;
                                    cantSugeridaFinal = cantidadSugerida;
                                }
                            }
                            i++;
                        }
                        if (!correcto)
                        {

                            MensajeErrorTxt.InnerText = "Cantidad ingresada no cumple con requisitos, cantidad sugerida es " + cantSugeridaFinal;
                            MsjErrorPrograma.Style.Add("display", "block");
                        }
                    }
                }
                else
                {
                    MensajeErrorTxt.InnerText = "Cantidad ingresada no es válida";
                    MsjErrorPrograma.Style.Add("display", "block");
                }
            }
            catch (Exception e)
            {
                MensajeErrorTxt.InnerText = "Cantidad ingresada no es válida";
                MsjErrorPrograma.Style.Add("display", "block");
            }

            return correcto;

        }
    }
}