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
    public partial class DetallesFacturaRevision : System.Web.UI.Page
    {
        private ControladoraDetalles_Producto controladora = new ControladoraDetalles_Producto();
        EmailManager email = new EmailManager();
        Bitacora bitacora = new Bitacora();
        ServicioLogin servicio = new ServicioLogin();
        private DataTable datosFactura;
        public  string numFactura;
        private  int id_factura;
        private  int[] ids;//se guardaran los ids de los productos de la factura
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Administrador Almacen"))
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
                        numFactura = servicio.TamperProofStringDecode(Request.QueryString["numF"], "MJP");
                    }
                    catch (Exception)
                    {
                        Response.Redirect("MenuPrincipal");
                    }
                    labelFactura.InnerText = "Factura " + numFactura;
                    llenarDetallesProducto();
                    ViewState["numFactura"] = numFactura;
                }

            }
            else {
                numFactura = (string)ViewState["numFactura"];
                id_factura = (int)ViewState["id_factura"];
                ids = (int[])ViewState["ids"];
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
            GridProductos.DataSource = datosFactura;
            GridProductos.DataBind();
        }
        //Elimina el item seleccionado
        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            String estado = HttpUtility.HtmlDecode(GridProductos.Rows[(this.GridProductos.PageIndex * 10)].Cells[3].Text);
            if (estado.Equals("Pendiente de Aprobación"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Justificación rechazo factura: " + numFactura + "');", true);
            }
        }
        //Dependiendo de si es aprobar o rechazar, modifica el estado para que se guarde el estado correspondiente del producto
        protected void aceptar(object sender, EventArgs e)
        {
            string justificacion = justificacionRechazo.Value;
            if (justificacionRechazo.Value.Trim() == "")
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal('Justificación rechazo factura: " + numFactura+"');", true);
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetalles').modal('show');</script>");
            }
            else{
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ModalDetalles').modal('hide');</script>");
                List<string> bodegas = (List<string>)Session["bodegas"];
                string bodega = bodegas[0];
                List<string> correos = email.obtenerCorreosBodegueros(bodega);
                controladora.rechazarFactura(id_factura);
                email.MailSender(justificacion, "Notificación de rechazo de factura", correos);
                string descripcionRA = "Factura: "+numFactura+" rechazado";
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcionRA);
                Response.Redirect("RevisionFacturas.aspx");
            }
        }

        protected void aceptarAprobado(object sender, EventArgs e)
        {
            string fecha = txtFecha.Text;
            if (fecha.Equals(""))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRD();", true);
                txtFecha.Focus();
            }
            else
            {
                controladora.aprobarFactura(id_factura,fecha);
                string descripcionRA = "Factura número: " + numFactura + " Aprobada por el administrador de almacen";
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcionRA);
                Response.Redirect("RevisionFacturas.aspx");
                
            }
        }
        //Edita el item seleccionado
        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            String estado = HttpUtility.HtmlDecode(GridProductos.Rows[(this.GridProductos.PageIndex * 10)].Cells[3].Text);
            if (estado.Equals("Pendiente de Aprobación"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalRD();", true);
            
            }
           
           

        }
        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }
        //Vuelve a la pantalla de lista de Facturas de la bodega
        protected void volver(object sender, EventArgs e)
        {
            Response.Redirect("RevisionFacturas.aspx");

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
            ViewState["ids"] = ids;
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