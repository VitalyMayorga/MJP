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
        private  string numRequisicion;
        private string btnId;
        private string unidad;
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
                else if (!rol.Equals("Aprobador") && !rol.Equals("Revisión y Aprobador Almacen"))
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
                        btnId = servicio.TamperProofStringDecode(Request.QueryString["btn"], "MJP");
                        if (btnId == "btnDetallesAlmacen")
                        {
                            unidad = servicio.TamperProofStringDecode(Request.QueryString["unidad"], "MJP");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Redirect("MenuPrincipal");
                    }
                    labelRequisicion.InnerText = "Requisicion " + numRequisicion;
                    llenarDetallesProductoRequisicion();
                    ViewState["numRequisicion"] = numRequisicion;
                    ViewState["btnId"] = btnId;
                    ViewState["unidad"] = unidad;
                }

            }
            else {
                numRequisicion = (string)ViewState["numRequisicion"];
                btnId = (string)ViewState["btnId"];
                unidad = (string)ViewState["unidad"];
                id_requisicion = (int)ViewState["id_requisicion"];
                //id_row = (int)ViewState["id_row"];
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
            string rol = (string)Session["rol"];
            if(rol.Equals("Aprobador")){
                GridProductosRequisicion.PageIndex = e.NewPageIndex;
                GridProductosRequisicion.DataSource = datosRequisicion;
                GridProductosRequisicion.DataBind();
            }else{
                GridProductosRequisicionAlmacen.PageIndex = e.NewPageIndex;
                GridProductosRequisicionAlmacen.DataSource = datosRequisicion;
                GridProductosRequisicionAlmacen.DataBind();
            }
        }

        //Crea la Boleta con los productos de la requisicion
        protected void btnBoleta_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductosRequisicion.aspx?numReq=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequisicion, "MJP")));
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalCompletarInfo('Datos de despacho de la requisicion: " + numRequisicion + "');", true); 
            }
        }

         //Cambia el estado de la requisicion segun el rol del usuario que realizo la Devolución
        protected void aceptarDespacho(object sender, EventArgs e)
        {
            string descripcionRA = "";
            string usuario = (string)Session["correoInstitucional"];
            string placa = txtPlaca.Text;
            string nomConductor = txtConductor.Text;
            string destinatario = unidad;
                        
            controladora.cambiarEstadoRequisicion(id_requisicion, 3);
            controladora.agregarInfoDespacho(id_requisicion, placa, nomConductor, destinatario);      
            descripcionRA = "Requisicion: " + numRequisicion + " despachada";
            bitacora.registrarActividad(usuario, descripcionRA);
            Response.Redirect("Boleta.aspx?numReq=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequisicion, "MJP")) + "&idReq=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(id_requisicion.ToString(), "MJP")));
            Response.Redirect("RevisionRequisiciones.aspx"); 
        }


        //Cambia el estado de la requisicion segun el rol del usuario que realizo la Devolución
        protected void aceptarDevolucion(object sender, EventArgs e)
        {
            string justificacion = justificacionDevolucion.Value;
            string rol = (string)Session["rol"];
            string usuario = (string)Session["correoInstitucional"];
            string descripcionRA = "";
            List<string> correos = null;
            if (justificacionDevolucion.Value.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Justificación rechazo requisicion: " + numRequisicion+"');", true);
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetalles').modal('show');</script>");
            }
            else{
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetalles').modal('hide');</script>");
                
                if (rol.Equals("Revisión y Aprobador Almacen"))
                {
                    controladora.cambiarEstadoRequisicion(id_requisicion, 2);
                    correos = email.obtenerCorreosAprobadorSegunPrograma(id_requisicion);
                    descripcionRA = "Requisicion: " + numRequisicion + " devuelta a aprobador programa";
                }
                else
                {
                    controladora.cambiarEstadoRequisicion(id_requisicion, 5);
                    correos = email.obtenerCorreoUsuarioRequisicion(id_requisicion);
                    descripcionRA = "Requisicion: " + numRequisicion + " devuelta a usuario";
                }
                //email.MailSender(Justificación, "Notificación de devolución de requisicion", correos);
                controladora.actualizarObservacion(id_requisicion, justificacion);
                bitacora.registrarActividad(usuario, descripcionRA);
                Response.Redirect("RevisionRequisiciones.aspx");                
            }
        }

        //Antes de devolver la requisicion abre el panel para escribir la observacion
        protected void btnDevolver_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Justificación devolución requisicion: " + numRequisicion + "');", true);
        }

        //Cambia el estado de la requisicion a rechazado
        protected void aceptarRechazo(object sender, EventArgs e)
        {
            string justificacion = justificacionRechazo.Value;
            string usuario = (string)Session["correoInstitucional"];
            string descripcionRA = "";
            
            if (justificacionRechazo.Value.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalRechazo('Justificación rechazo requisicion: " + numRequisicion + "');", true);
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetallesRechazo').modal('show');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetallesRechazo').modal('hide');</script>");
                controladora.cambiarEstadoRequisicion(id_requisicion, 4);
                
                controladora.actualizarObservacion(id_requisicion, justificacion);
                descripcionRA = "Requisicion: " + numRequisicion + " rechazada en destino";
                bitacora.registrarActividad(usuario, descripcionRA);
                Response.Redirect("RevisionRequisiciones.aspx");
            }
        }

        //Antes de devolver la requisicion abre el panel para escribir la observacion
        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalRechazo('Justificación rechazo requisicion: " + numRequisicion + "');", true);
        }

        //Envia la requisicion a los aprobadores de almacen
        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            int indice = 0;
            DateTime fechaRecibido = DateTime.Now;
            string personaRecibe = "";
            string usuario = (string)Session["correoInstitucional"];
            string descripcionRA = "";

            if (btnId != "btnDetallesDespacho")
            {
                controladora.cambiarEstadoRequisicion(id_requisicion, 1);
            }
            else{
                controladora.cambiarEstadoRequisicion(id_requisicion, 0);
                controladora.actualizarInfoDespacho(id_requisicion, fechaRecibido, personaRecibe);

                while (indice < ids.Count())
                {
                    controladora.actualizarCantidadProductosRequisicion(controladora.obtenerIDBodegaRequisicion(id_requisicion), ids[indice], controladora.obtenerIDProgramaRequisicion(id_requisicion), controladora.obtenerIDSubBodegaRequisicion(id_requisicion), cantidades[indice]);
                    indice++;
                }
                descripcionRA = "Requisicion: " + numRequisicion + " acepatada en destino";
                bitacora.registrarActividad(usuario, descripcionRA);
            }           
            
            Response.Redirect("RevisionRequisiciones.aspx");
        }        

        //Cambia el estado de la requisicion segun el rol del usuario que realizo la Devolución
        protected void aceptarEdicion(object sender, EventArgs e)
        {
            string cantidad = txtCantidad.Text;
            string descripcion = nombreP.InnerText;
            int ir = Int32.Parse(idroweditar.InnerText);
            string descripcionRA = "";
            string rol = (string)Session["rol"];
            string usuario = (string)Session["correoInstitucional"];

            if (validar(descripcion))
            {//Si todo es valido, entonces se procede a guardar el producto en la requisicion
                if (txtCantidad.Text.Trim() == "")
                {
                    //string descrip = GridProductosRequisicion.Rows[ir + (this.GridProductosRequisicion.PageIndex * 10)].Cells[0].Text;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalEdicion('Editar cantidad del producto: " + descripcion + "');", true);
                    ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalEditar').modal('show');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalEditar').modal('hide');</script>");
                    controladora.modificarCantidadLinea(id_requisicion, ids[ir + (this.GridProductosRequisicion.PageIndex * 10)], Int32.Parse(cantidad));
                }
                descripcionRA = "Requisicion: " + numRequisicion + " editada";
                bitacora.registrarActividad(usuario, descripcionRA);
                llenarDetallesProductoRequisicion();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEdicion('Editar cantidad del producto: " + descripcion + "');", true);
            }
        } 

        //Redirecciona a la pantalla de editar linea
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            id_row = Convert.ToInt32(row.RowIndex);
            idroweditar.InnerText = id_row.ToString();
            txtCantidad.Text = cantidades[id_row + (this.GridProductosRequisicion.PageIndex * 10)].ToString();

            //Se obtiene el id del producto            
            String descripcion = GridProductosRequisicion.Rows[id_row + (this.GridProductosRequisicion.PageIndex * 10)].Cells[0].Text;
            nombreP.InnerText = descripcion;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalEdicion('Editar cantidad del producto: " + descripcion + "');", true);
           
        }

        //Pregunta si deseaeliminar la linea seleccionada
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            id_row = Convert.ToInt32(row.RowIndex);
            idroweliminar.InnerText = id_row.ToString();
            //Se obtiene el id del producto            
            String descripcion = GridProductosRequisicion.Rows[id_row + (this.GridProductosRequisicion.PageIndex * 10)].Cells[0].Text;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalDelete('Desea eliminar el producto: " + descripcion + "?');", true);            
        }

        //Elimina la linea deseada de la requisicion
        protected void aceptarEliminado(object sender, EventArgs e)
        {
            int ir = Int32.Parse(idroweliminar.InnerText);
            int idProducto = ids[ir + (this.GridProductosRequisicion.PageIndex * 10)];
            controladora.eliminarProductoRequisicion(numRequisicion, idProducto);
            llenarDetallesProductoRequisicion();
        }

        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }

        //Revisa la cantidad ingresada por el usuario vs lo que hay en el almacen
        protected bool validar(string descripcion)
        {
            List<string> dato = controladora.getDatosRequisicion(numRequisicion);
            bool correcto = false;
            int cantSugeridaFinal = 0;
            int i = 0;
            try
            {
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                if (cantidad > 0)
                {//Aqui esta el algoritmo necesario para indicar cantidades sugeridas o si se puede o no hacer el pedido con la cantidad ingresada
                    MsjErrorcantidad.Style.Add("display", "none");
                    int cantidadEnBodega = controladora.obtenerCantidadProductoBodega(Convert.ToInt32(dato[0]), Convert.ToInt32(dato[2]), dato[1], descripcion);
                    List<int> empaques = controladora.obtenerEmpaque(Convert.ToInt32(dato[0]), Convert.ToInt32(dato[2]), dato[1], descripcion);
                    List<int> cantPorEmpaque = controladora.obtenerCantPorEmpaque(Convert.ToInt32(dato[0]), Convert.ToInt32(dato[2]), dato[1], descripcion);
                    if (cantidad > cantidadEnBodega)
                    {
                        MensajeErrorTxt.InnerText = "Cantidad ingresada sobrepasa la cantidad en almacén";
                        MsjErrorcantidad.Style.Add("display", "block");
                    }
                    else
                    {
                        int residuoCercano = 99999; //Usado para recomendar siempre el empaque mas cercano                        
                        int cantidadSugerida;
                        foreach (int empaque in empaques)
                        {
                            if (cantidad <= cantPorEmpaque[i])
                            {

                                int residuo = cantidad % empaque;
                                if (residuo == 0 && cantidad <= cantPorEmpaque[i])
                                {
                                    MsjErrorcantidad.Style.Add("display", "none");
                                    correcto = true;
                                }
                                else if (!correcto)
                                {
                                    if (residuo < empaque / 2 && (cantidad - residuo) != 0)
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
                            }

                            i++;
                        }
                        if (!correcto)
                        {
                            MensajeErrorTxt.InnerText = "Cantidad ingresada no cumple con requisitos, cantidad sugerida es " + cantSugeridaFinal;
                            MsjErrorcantidad.Style.Add("display", "block");
                        }
                    }
                }
                else
                {
                    MensajeErrorTxt.InnerText = "Cantidad ingresada no es válida";
                    MsjErrorcantidad.Style.Add("display", "block");
                }
            }
            catch (Exception e)
            {
                MensajeErrorTxt.InnerText = "Cantidad ingresada no es válida";
                MsjErrorcantidad.Style.Add("display", "block");
            }

            return correcto;

        }




        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProductoRequisicion()
        {
            string rol = (string)Session["rol"];
            if (rol.Equals("Aprobador"))
            {
                btn_aprobar.Style.Add("display", "block");
                GridAprobadorPrograma.Style.Add("display", "block");
            }
            else
            {
                if (btnId != "btnDetallesDespacho")
                {
                    btn_rechazar.Style.Add("display", "none");
                    btn_aprobar.Style.Add("display", "none");
                    btn_boleta.Style.Add("display", "block");
                    btn_despachar.Style.Add("display", "block");
                    btn_devolver.Style.Add("display", "block");
                }
                else{
                    btn_rechazar.Style.Add("display", "block");
                    btn_aprobar.Style.Add("display", "block");
                    btn_boleta.Style.Add("display", "none");
                    btn_despachar.Style.Add("display", "none");
                    btn_devolver.Style.Add("display", "none");
                }
                
                GridAprobadorAlmacen.Style.Add("display", "block");
            }            
            
            CultureInfo crCulture = new CultureInfo("es-CR");
            DataTable tabla = crearTablaProductosRequisicion();
            id_requisicion = controladora.obtenerIDRequisicion(numRequisicion);
            ViewState["id_requisicion"] = id_requisicion;
            List<string> dato = controladora.getDatosRequisicion(numRequisicion);
            List<Item_Grid_Produtos_Requisicion> data = controladora.obtenerListaProductos(id_requisicion);
            Object[] datos;
            if (rol.Equals("Aprobador"))
            {
                datos = new Object[3];
            }else{
                datos = new Object[2];
            }

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
                if (rol.Equals("Aprobador"))
                {
                    string cantTotal= controladora.obtenerCantidadProductoBodega(Convert.ToInt32(dato[0]), Convert.ToInt32(dato[2]), dato[1], controladora.getNombreProducto(fila.Producto)).ToString();
                    if(cantTotal!=null){
                        datos[2] = cantTotal;
                    }else{
                        datos[2] = "0";
                    }
                    
                }
                cantidades[contador] = fila.Cantidad;
                contador++;

                tabla.Rows.Add(datos);
            }
            datosRequisicion = tabla;
            if (rol.Equals("Aprobador"))
            {
                GridProductosRequisicion.DataSource = datosRequisicion;
                GridProductosRequisicion.DataBind();
            }
            else
            {
                GridProductosRequisicionAlmacen.DataSource = datosRequisicion;
                GridProductosRequisicionAlmacen.DataBind();
            }
           
        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaProductosRequisicion()//consultar
        {
            string rol = (string)Session["rol"];
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

            if (rol.Equals("Aprobador"))
            {
                columna = new DataColumn();
                columna.DataType = System.Type.GetType("System.String");
                columna.ColumnName = "Cantidad Total en Almacen";
                tabla.Columns.Add(columna);
                
                GridProductosRequisicion.DataSource = tabla;
                GridProductosRequisicion.DataBind();
            }
            else
            {
                GridProductosRequisicionAlmacen.DataSource = tabla;
                GridProductosRequisicionAlmacen.DataBind();
            }          

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

    }
}