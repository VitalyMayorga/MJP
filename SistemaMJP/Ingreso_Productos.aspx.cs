using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Diagnostics;
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
                
                else if (Request.UrlReferrer== null)
                {
                    Response.Redirect("MenuPrincipal");
                }
                else {
                    noActivo.Checked = true;
                }
            }
        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y se mantiene en la página para nuevo ingreso de productos
        protected void aceptar(object sender, EventArgs e)
        {
            if (validar()) {
            
            
            }
        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y redirecciona a la página factura_detalles
        protected void aceptarYSalir(object sender, EventArgs e)
        {
            if (validar())
            {


            }
        }
        
        //Si está seleccionado es activo, habilita la inserción de datos del activo
        protected void rbSi(object sender, EventArgs e)
        {
            validar();
            if (esActivo.Checked)
            {

                formActivo.Style.Add("display", "block");
                
                
            }
        }
        //Si está seleccionado no activo,esconde el div de la inserción de activos
        protected void rbNo(object sender, EventArgs e)
        {
            validar();
            if (noActivo.Checked)
            {
                formActivo.Style.Add("display", "none");
            }
        }
        //Redirecciona a la pagina facturas
        protected void cancelar(object sender, EventArgs e)
        {
            Response.Redirect("Facturas.aspx");
        }
        
        
        //Metodo que se encarga de obtener todos los productos que empiezan cn lo digitado por el usuario
        //Funcionalidad principal es mostrar sugerencias al ingresar la descripcion de un producto
        [WebMethod]
        public static string[] getProductos(string prefix)
        {
            List<string> customers = ControladoraProductos.getProductos(prefix);
            return customers.ToArray();
        }
        //Metodo encargado de validar los campos antes de ejecutar una accion
        protected bool validar() {
            bool valido = false;
            string descripcion = txtDescripcion.Text;
            string presentacion = txtPresentacion.Text;
            string cantidad = txtCantidadE.Text;
            string cantidadT = txtCantidadT.Text;
            string precio = txtPrecioT.Text;
            string tmp = descripcion.Replace(" ", "");
            if (tmp.Equals(""))
            {
                MsjErrorDescripcion.Style.Add("display", "block");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");
            }
            else if (presentacion.Equals(""))
            {
                MsjErrorPresentacion.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
            }
            else if (cantidad.Equals(""))
            {
                MsjErrorCantEmp.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");

            }
            else if (cantidadT.Equals(""))
            {
                MsjErrorCantidad.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");

            }
            else if (precio.Equals(""))
            {
                MsjErrorPrecio.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");


            }
            else
            {//Primero se verifica que los datos numerales sean efectivamente numeros
                bool numero = true;
                int cantPorEmp = 0;
                int cantTotal = 0;
                decimal precioT = 0;
                if (numero) {
                    try
                    {

                        MsjErrorCantEmp.Style.Add("display", "none");
                        MsjErrorCantidad.Style.Add("display", "none");
                        MsjErrorPrecio.Style.Add("display", "none");
                        cantPorEmp = Convert.ToInt32(cantidad);
                    }
                    catch (Exception)
                    {
                        MsjErrorCantEmp.Style.Add("display", "block");
                        numero = false;
                    }
                }
                if (numero) {
                    try
                    {
                        MsjErrorCantEmp.Style.Add("display", "none");
                        MsjErrorCantidad.Style.Add("display", "none");
                        MsjErrorPrecio.Style.Add("display", "none");
                        cantTotal = Convert.ToInt32(cantidadT);
                        
                    }
                    catch (Exception)
                    {
                        MsjErrorCantidad.Style.Add("display", "block");
                        numero = false;
                    }
                
                }
                if (numero) {
                    try
                    {
                        MsjErrorCantEmp.Style.Add("display", "none");
                        MsjErrorCantidad.Style.Add("display", "none");
                        MsjErrorPrecio.Style.Add("display", "none");
                        precioT = Convert.ToDecimal(precio);
                        
                    }
                    catch (Exception)
                    {
                        MsjErrorPrecio.Style.Add("display", "block");
                        numero = false;
                    }
                
                }
                if (numero) {
                    string numActivo = txtNumActivo.Text.Replace(" ", "");
                    string cedula = txtCedula.Text.Replace(" ", "");
                    string funcionario = txtFuncionario.Text;
                    string funcioonarioTmp = funcionario.Replace(" ", "");
                    if (!numActivo.Equals("") || !cedula.Equals("") || !funcionario.Equals(""))
                    {
                        if (numActivo.Equals(""))
                        {
                            MsjErrorNumActivo.Style.Add("display", "block");
                            MsjErrorCedula.Style.Add("display", "none");
                            MsjErrorFuncionario.Style.Add("display", "none");
                        }
                        else if (funcionario.Equals(""))
                        {
                            MsjErrorNumActivo.Style.Add("display", "none");
                            MsjErrorCedula.Style.Add("display", "none");
                            MsjErrorFuncionario.Style.Add("display", "block");
                        }
                        else if (cedula.Equals(""))
                        {
                            MsjErrorNumActivo.Style.Add("display", "none");
                            MsjErrorCedula.Style.Add("display", "block");
                            MsjErrorFuncionario.Style.Add("display", "none");
                        }

                    }
                    else
                    {//Si todo está bien, devuelve verdadero
                        valido = true;
                    }

                }
                
            }
            return valido;
        }
    }
    
}