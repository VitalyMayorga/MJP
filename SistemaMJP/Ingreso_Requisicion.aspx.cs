using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Diagnostics;
using System.Data;
using SistemaMJP.RequisicionesUsuario;

namespace SistemaMJP
{
    public partial class Ingreso_Requisicion : System.Web.UI.Page
    {
        private int bodega;
        private int subbodega;
        private string programa;
        private string numRequisicion;
        private DataTable tabla;
        private string producto;
        private int i;
        private String nombre;
        private String descripcion;
        ControladoraRequisicionesUsuario controladora = new ControladoraRequisicionesUsuario();
        ServicioLogin servicio = new ServicioLogin();
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
                        
                        bodega = Convert.ToInt32(servicio.TamperProofStringDecode(Request.QueryString["bodega"], "MJP"));
                        programa = (servicio.TamperProofStringDecode(Request.QueryString["programa"], "MJP")).ToString();
                        subbodega = Convert.ToInt32(servicio.TamperProofStringDecode(Request.QueryString["subbodega"], "MJP"));
                        numRequisicion = (servicio.TamperProofStringDecode(Request.QueryString["numReq"], "MJP")).ToString();
                        
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("MenuPrincipal.aspx");
                    }                       

                }
                ViewState["bodega"] = bodega;
                ViewState["programa"] = programa;
                ViewState["numRequisicion"] = numRequisicion;
                ViewState["subbodega"] = subbodega;

            }
            else
            {
                try
                {
                    bodega = (int)ViewState["bodega"];
                    subbodega = (int)ViewState["subbodega"];
                    programa = (string)ViewState["programa"];
                    numRequisicion = (string)ViewState["numRequisicion"];
                    tabla = (DataTable)ViewState["tabla"];
                    i = (int)ViewState["i"];
                    producto =(string) ViewState["producto"];
                    descripcion = (string) ViewState["descripcion"];
                    nombre = (string)ViewState["nombre"];
                }
                catch (Exception) { }
            }
        }

        //Revisa la cantidad ingresada por el usuario vs lo que hay en el almacen
        protected bool validar() {
            bool correcto = false;
            int cantSugeridaFinal=0;
            int i = 0;
            try
            {
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                if (cantidad > 0) {//Aqui esta el algoritmo necesario para indicar cantidades sugeridas o si se puede o no hacer el pedido con la cantidad ingresada
                    MsjErrorPrograma.Style.Add("display", "none");
                    int cantidadEnBodega = controladora.obtenerCantidadProductoBodega(bodega,subbodega,programa,producto);
                    List<int> empaques = controladora.obtenerEmpaque(bodega, subbodega, programa, producto);
                    List<int> cantPorEmpaque = controladora.obtenerCantPorEmpaque(bodega, subbodega, programa, producto); 
                    if (cantidad > cantidadEnBodega)
                    {
                        MensajeErrorTxt.InnerText = "Cantidad ingresada sobrepasa la cantidad en almacén";
                        MsjErrorPrograma.Style.Add("display", "block");
                    }
                    else {
                        int residuoCercano = 99999; //Usado para recomendar siempre el empaque mas cercano                        
                        int cantidadSugerida;
                        foreach (int empaque in empaques) {
                            if(cantidad <= cantPorEmpaque[i]){

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
            catch (Exception e) {
                MensajeErrorTxt.InnerText = "Cantidad ingresada no es válida";
                MsjErrorPrograma.Style.Add("display", "block");
            }

            return correcto;
        
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e) {
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
            ViewState["descripcion"] = descripcion;
            ViewState["nombre"] = nombre;
            
            //nombreProducto.Text = nombre;
            //descripcionLabel.Text = descripcion;
            txtCantidad.Text = "";
            MsjErrorPrograma.Style.Add("display", "none");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('"+nombre+"','"+descripcion+"');", true);

            
        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y se mantiene en la página para nuevo ingreso de productos
        protected void aceptar(object sender, EventArgs e)
        {
            if (validar())
            {//Si todo es valido, entonces se procede a guardar el producto en la requisicion
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                controladora.agregarProducto(producto, numRequisicion, cantidad);
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#AgregarProducto').modal('hide');</script>");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('" + nombre + "','" + descripcion + "');", true);
            }
        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y redirecciona a la página DetallesRequisicion
        protected void aceptarYSalir(object sender, EventArgs e)
        {
            if (validar())
            {//Si todo es valido, entonces se procede a guardar el producto en la requisicion
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                controladora.agregarProducto(producto, numRequisicion, cantidad);
                Response.Redirect("DetallesRequisicion.aspx?numReq=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequisicion, "MJP")));

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('" + nombre + "','" + descripcion + "');", true);
            }

               
        }

        //Redirecciona a la pagina 
        protected void cancelar(object sender, EventArgs e)
        {
           
              Response.Redirect("Requisiciones.aspx");

        }

        //Obtiene los productos que cumplen con lo buscado por el usuario
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = productoBusqueda.Text;
            if (!String.IsNullOrEmpty(busqueda))
            {
                //se eliminan tildes
                busqueda = busqueda.Replace("á", "a");
                busqueda = busqueda.Replace("é", "e");
                busqueda = busqueda.Replace("í", "i");
                busqueda = busqueda.Replace("ó", "o");
                busqueda = busqueda.Replace("ú", "u");
                DataTable tablaP = crearTablaRequisiciones();
                List<Item_Grid_Productos_Bodega> data = controladora.getListaProductosBodega(bodega,subbodega,programa,busqueda);
                Object[] datos = new Object[2];
                if (data.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('No existen productos que cumplan el criterio de búsqueda')", true);
                    productoBusqueda.Text = "";
                    GridProductos.Visible = false;
                }
                else {
                    foreach (Item_Grid_Productos_Bodega fila in data)
                    {
                        datos[0] = fila.Nombre;
                        datos[1] = fila.Unidad;
                        tablaP.Rows.Add(datos);
                    }

                    tabla = tablaP;
                    ViewState["tabla"] = tablaP;
                    GridProductos.Visible = true;
                    GridProductos.DataSource = tabla;
                    GridProductos.DataBind();
                }
                

            }

            productoBusqueda.Text = "";
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


    }
}