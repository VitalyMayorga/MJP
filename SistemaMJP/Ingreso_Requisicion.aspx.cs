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
                }
                catch (Exception) { }
            }
        }
        //Revisa la cantidad ingresada por el usuario vs lo que hay en el almacen
        protected bool validar() {
            bool correcto = false;
            try
            {
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                if (cantidad > 0) {//Aqui esta el algoritmo necesario para indicar cantidades sugeridas o si se puede o no hacer el pedido con la cantidad ingresada
                    MsjErrorPrograma.Style.Add("display", "none");
                    int cantidadEnBodega = controladora.obtenerCantidadProductoBodega(bodega,subbodega,programa,producto);
                    List<int> empaques = controladora.obtenerCantPorEmpaque(bodega, subbodega, programa, producto);
                    if (cantidad > cantidadEnBodega)
                    {
                        MensajeErrorTxt.InnerText = "Cantidad ingresada sobrepasa la cantidad en almacén";
                        MsjErrorPrograma.Style.Add("display", "block");
                    }
                    else {
                        foreach (int empaque in empaques) {
                            int residuo = cantidad % empaque;
                            if (residuo == 0)
                            {
                                MsjErrorPrograma.Style.Add("display", "none");
                                correcto = true;
                            }
                            else if(!correcto){
                                int cantidadSugerida;
                                if (residuo < empaque / 2) {//caso cantidad es menor al 49%
                                    cantidadSugerida = cantidad - residuo;
                                    MensajeErrorTxt.InnerText = "Cantidad ingresada no cumple con requisitos, cantidad sugerida es "+cantidadSugerida;
                                    MsjErrorPrograma.Style.Add("display", "block");
                                }
                                else
                                {
                                    cantidadSugerida = (cantidad - residuo) + empaque;
                                    MensajeErrorTxt.InnerText = "Cantidad ingresada no cumple con requisitos, cantidad sugerida es " + cantidadSugerida;
                                    MsjErrorPrograma.Style.Add("display", "block");
                                }
                            }
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
            String nombre = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text);
            String descripcion = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[1].Text);
            //Se obtiene el id del producto
            producto = nombre;           
            nombreProducto.InnerText = nombre;
            descripcionLabel.InnerHtml = descripcion;
            txtCantidad.Text = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal();", true);

            
        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y se mantiene en la página para nuevo ingreso de productos
        protected void aceptar(object sender, EventArgs e)
        {
            if (validar())
            {//Si todo es valido, entonces se procede a guardar el producto en la requisicion
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                controladora.agregarProducto(producto, numRequisicion, cantidad);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                DataTable tablaP = crearTablaRequisiciones();
                List<Item_Grid_Productos_Bodega> data = controladora.getListaProductosBodega(bodega,subbodega,programa,busqueda);
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