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
    public partial class DetallesFactura : System.Web.UI.Page
    {
        private ControladoraDetalles_Producto controladora = new ControladoraDetalles_Producto();
        private ServicioLogin servicio = new ServicioLogin();
        Bitacora bitacora = new Bitacora();
        EmailManager email = new EmailManager();
        private DataTable datosFactura;
        public  string numFactura;
        private  int id_factura;
        private  int[] ids;//se guardaran los ids de los productos de la factura
        private  int i;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Inclusión Pedidos"))
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
                        string DataString = servicio.TamperProofStringDecode(Request.QueryString["numF"], "MJP");
                        numFactura = DataString;
                        ViewState["numFactura"] = numFactura;
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("MenuPrincipal.aspx");
                    }
                    labelFactura.InnerText = "Factura " + numFactura;
                    llenarDetallesProducto();
                }

            }
            else {

                id_factura = (int)ViewState["id_factura"];
                numFactura = (string)ViewState["numFactura"];
                datosFactura = (DataTable)ViewState["tabla"];
                try
                {
                    i = (int)ViewState["i"];
                }
                catch (Exception)
                {
                }
                ids = (int[])ViewState["ids"];
            }
        }
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }
        //Crea una vista Previa de la factura con los productos ingresados.
        protected void vistaPrevia(object sender, EventArgs e)
        {
            Response.Redirect("FacturaVistaPrevia.aspx?id_factura=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(id_factura.ToString(), "MJP")));
            
        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridProductos.PageIndex = e.NewPageIndex;
            GridProductos.DataSource = datosFactura;
            GridProductos.DataBind();
        }
        //Elimina el item seleccionado
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            i = Convert.ToInt32(row.RowIndex);
            ViewState["i"] = i;
            //HTMLDECODE: es necesario para leer caracteres con tilde
            String estado = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[3].Text);
            if (estado.Equals("En edición"))
            {
                //Se obtiene el id del producto
                int idProducto = ids[i + (this.GridProductos.PageIndex * 10)];
                String descripcion = GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[0].Text;
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Desea eliminar el producto: " + descripcion + "?');", true);
                

            }
        }

        protected void aceptarEliminado(object sender, EventArgs e)
        {
            int idProducto = ids[i + (this.GridProductos.PageIndex * 10)];
            controladora.eliminarProducto(id_factura,idProducto);
            llenarDetallesProducto();
        }

        //Edita el item seleccionado
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            i = Convert.ToInt32(row.RowIndex);
            ViewState["i"] = i;
            String estado = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[3].Text);
            if (estado.Equals("En edición"))
            {
                //Se obtiene el id del producto
                int idProducto = ids[i + (this.GridProductos.PageIndex * 10)];
                //Ingreso_Productos.id_factura = id_factura;
                //Ingreso_Productos.idProducto = idProducto;
                //Ingreso_Productos.numFactura = numFactura;
                //Ingreso_Productos.editar = true;
                Response.Redirect("Ingreso_Productos.aspx?id_factura=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(id_factura.ToString(), "MJP")) + "&numFactura=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numFactura, "MJP")) + "&idProducto=" +
                    HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idProducto.ToString(), "MJP")) +"&editar=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode("1", "MJP")));
            }
        }

        //Crea un nuevo producto
        protected void nuevoProducto(object sender, EventArgs e)
        {
            //Ingreso_Productos.numFactura = numFactura;
            //Ingreso_Productos.editar = false;
            Response.Redirect("Ingreso_Productos.aspx?numFactura=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numFactura, "MJP"))+ "&editar=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode("0", "MJP")));
            

        }
        //Cambia el estado de la factura a pendiente de aprobación, así como todos los productos
        //Este botón solo está disponible si la factura estaba anteriormente en modo edicion
        protected void btnEnviar(object sender, EventArgs e)
        {

            String estado = HttpUtility.HtmlDecode(GridProductos.Rows[i + (this.GridProductos.PageIndex * 10)].Cells[3].Text);
            if (estado.Equals("En edición"))
            {
                controladora.enviarAAprobacion(id_factura);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Factura enviada a aprobación.\n Cuando sea revisada por el administrador, su estado será Finalizado')", true);
                llenarDetallesProducto();
                string descripcionRA = "Factura" + numFactura + " enviada a revisión";
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcionRA);
                List<string> bodegas = (List<string>)Session["bodegas"];
                string bodega = bodegas[0];
                List<string> correos = email.obtenerCorreosAdminAlmacen(bodega);
                email.MailSender("Nueva factura enviada a revisión por el usuario " + usuario + ".\nNúmero de factura " + numFactura + ".", "Notificación de solicitud de aprobación de productos de factura", correos);

            }
        }
        //Llena la grid de productos con los datos correspondientes
        internal void llenarDetallesProducto()
        {
            CultureInfo crCulture = new CultureInfo("es-CR");
            DataTable tabla = crearTablaProductos();
            id_factura = controladora.obtenerIDFactura(numFactura);
            ViewState["id_factura"] = id_factura;
            List<Item_Grid_Produtos_Factura> data = controladora.obtenerListaProductos(id_factura);
            Object[] datos = new Object[4];
            ids = new int[data.Count];
            int contador = 0;
            foreach (Item_Grid_Produtos_Factura fila in data)
            {
                ids[contador] = fila.Id;
                datos[0] = fila.Descripcion;
                datos[1] = fila.Cantidad.ToString();
                datos[2] = fila.PrecioTotal.ToString("C2", crCulture);
                datos[3] = fila.Estado;
                contador++;

                tabla.Rows.Add(datos);
            }
            ViewState["ids"] = ids;
            ViewState["tabla"] = tabla;
            datosFactura = tabla;
            GridProductos.DataSource = datosFactura;
            GridProductos.DataBind();

        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaProductos()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Descripcion";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cantidad";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Precio Total";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);

            GridProductos.DataSource = tabla;
            GridProductos.DataBind();

            return tabla;
        }

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

    }
}