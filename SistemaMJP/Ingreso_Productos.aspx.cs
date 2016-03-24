using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace SistemaMJP
{
    public partial class Ingreso_Productos : System.Web.UI.Page
    {
        public static string bodega;
        public static int subbodega;
        public static int programa;
        public static string numFactura;
        public static int subpartida;
        ControladoraProductos controladora = new ControladoraProductos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {//Se revisa primero si tiene los permisos para ingresar al sitio
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Inclusión Pedidos"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                //else if (Page.PreviousPage==null) {
                //    Response.Redirect("MenuPrincipal");
                //}
                else{
                    
                }
            }
        }
        //Revisa si los campos necesarios fueron agregados, de ser así ingresa a la ventana de ingreso de productos
        //En otro caso, despliega los mensajes de error
        protected void aceptar(object sender, EventArgs e)
        {
            
        }
        //Revisa si los campos necesarios fueron agregados, de ser así ingresa a la ventana de ingreso de productos
        //En otro caso, despliega los mensajes de error
        protected void aceptarYSalir(object sender, EventArgs e)
        {
            
        }
        
        //Si está seleccionado es activo, habilita la inserción de datos del activo
        protected void rbSi(object sender, EventArgs e)
        {
            if (esActivo.Checked)
            {

                formActivo.Style.Add("display", "block");
                
                
            }
        }
        //Si está seleccionado no activo,esconde el div de la inserción de activos
        protected void rbNo(object sender, EventArgs e)
        {
            if (noActivo.Checked)
            {
                formActivo.Style.Add("display", "none");
            }
        }
        //Redirecciona al menu principal
        protected void cancelar(object sender, EventArgs e)
        {
            Response.Redirect("Facturas.aspx");
        }
        //Se llenan los datos iniciales, se cargan las bodegas programas, subpartidas
        protected void llenarDatos()
        {
            
        }

        protected void RevisarCampos(){
        
        
        }

        [WebMethod]
        public static string[] getProductos(string prefix)
        {
            List<string> customers = ControladoraProductos.getProductos(prefix);
            return customers.ToArray();
        }
    }
    
}