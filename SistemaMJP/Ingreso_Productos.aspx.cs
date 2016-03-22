using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Ingreso_Productos : System.Web.UI.Page
    {
        public static string bodega;
        public static string subbodega;
        public static string programa;
        public static string proveedor;
        public static string numFactura;
        public static string subpartida;
        protected void Page_Load(object sender, EventArgs e)
        {

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
        
        //Si está seleccionado ingresar Factura, habilita la inserción de una factura
        protected void rbNo(object sender, EventArgs e)
        {
            if (esActivo.Checked)
            {

                formActivo.Style.Add("display", "block");
                
                
            }
        }
        //Si está seleccionado Mercaderia Inicial,esconde el div de la inserción de una factura
        protected void rbSi(object sender, EventArgs e)
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
    }
    
}