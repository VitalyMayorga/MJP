﻿using System;
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
        public static int bodega;
        public static int subbodega;
        public static int programa;
        public static string numFactura;
        public static int subpartida;
        public static bool editar;
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
                else if (!rol.Equals("Inclusion Pedidos"))
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
            bool correcto = false;//Para saber si agregó bien el producto
            if (validar()) {//Si todo es valido, entonces procedo a obtener los datos dados por el usuario
                decimal total = 0;
                int id_factura = 0;
                Nullable<DateTime> fechaV = null;
                Nullable<DateTime> fechaG = null;
                Nullable<DateTime> fechaC = null;
                string descripcion = txtDescripcion.Text;
                //Se modifica la descripcion para que la primer letra se mayúscula y no hayan tildes
                descripcion = descripcion.ToLower();
                descripcion = descripcion.Replace("á", "a");
                descripcion = descripcion.Replace("é", "e");
                descripcion = descripcion.Replace("í", "i");
                descripcion = descripcion.Replace("ó", "o");
                descripcion = descripcion.Replace("ú", "u");
                descripcion = descripcion.First().ToString().ToUpper() + descripcion.Substring(1);
                string presentacionEmpaque = txtPresentacion.Text;
                int cantidadEmpaque = Convert.ToInt32(txtCantidadE.Text);
                decimal precioU = Convert.ToDecimal(txtPrecioT.Text.Replace(".",","));
                int cantidad = Convert.ToInt32(txtCantidadT.Text);
                bool activo = false;
                string numActivo = txtNumActivo.Text;
                string funcionario = txtFuncionario.Text;
                string cedula = txtCedula.Text;
                if (esActivo.Checked) {
                    activo = true;
                }
                
                if (!txtFechaV.Text.Equals("")) {
                    fechaV = Convert.ToDateTime(txtFechaV.Text);
                
                }
                if (!txtFechaC.Text.Equals(""))
                {
                    fechaC = Convert.ToDateTime(txtFechaV.Text);

                }
                if (!txtFechaG.Text.Equals(""))
                {
                    fechaG = Convert.ToDateTime(txtFechaV.Text);

                }
                object[] nuevoProducto = new object[6];
                nuevoProducto[0] = descripcion;
                nuevoProducto[1] = presentacionEmpaque;
                nuevoProducto[2] = activo;
                nuevoProducto[3] = precioU;
                nuevoProducto[4] = cantidadEmpaque;
                nuevoProducto[5] = subpartida;

                correcto = controladora.agregarProducto(nuevoProducto);
                    
                
                if (!numActivo.Equals("") && correcto) { 
                    cantidad= 1;//Por defecto, si el producto ingresado
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Producto " + descripcion + " al ser activo asignado,su cantidad es 1')", true);
                    controladora.agregarActivo(numActivo, funcionario, cedula,descripcion,cantidadEmpaque);
                }
                if (correcto && numFactura==null) {//Si se ingreso el producto de mercaderia inicial,se procede a guardar el producto en la tabla Informacion producto
                    controladora.agregarProductoABodega(bodega, descripcion, cantidadEmpaque, programa, subbodega, cantidad, fechaC, fechaG, fechaV);
                }
                else if(correcto){//Si es un producto de factura
                    id_factura = controladora.obtenerIDFactura(numFactura);
                    total = precioU * cantidad;
                    controladora.agregarProductoFactura(id_factura, descripcion, cantidadEmpaque, cantidad, total, fechaC, fechaG, fechaV);
                }
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Producto " + descripcion + " agregado con éxito')", true);
                
                //Al final se limpian los campos
                txtDescripcion.Text = "";
                txtPresentacion.Text = "";
                txtCantidadE.Text = "";
                txtCantidadT.Text = "";
                txtPrecioT.Text = "";
                txtFechaC.Text = "";
                txtFechaG.Text = "";
                txtFechaV.Text = "";
                txtFuncionario.Text = "";
                txtCedula.Text = "";
                txtNumActivo.Text = "";

            }
        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y redirecciona a la página factura_detalles
        protected void aceptarYSalir(object sender, EventArgs e)
        {
            bool correcto = false;//Para saber si agregó bien el producto
            if (validar())
            {//Si todo es valido, entonces procedo a obtener los datos dados por el usuario
                decimal total = 0;
                int id_factura = 0;
                Nullable<DateTime> fechaV = null;
                Nullable<DateTime> fechaG = null;
                Nullable<DateTime> fechaC = null;
                string descripcion = txtDescripcion.Text;
                //Se modifica la descripcion para que la primer letra se mayúscula y no hayan tildes
                descripcion = descripcion.ToLower();
                descripcion = descripcion.Replace("á", "a");
                descripcion = descripcion.Replace("é", "e");
                descripcion = descripcion.Replace("í", "i");
                descripcion = descripcion.Replace("ó", "o");
                descripcion = descripcion.Replace("ú", "u");
                descripcion = descripcion.First().ToString().ToUpper() + descripcion.Substring(1);
                string presentacionEmpaque = txtPresentacion.Text;
                int cantidadEmpaque = Convert.ToInt32(txtCantidadE.Text);
                decimal precioU = Convert.ToDecimal(txtPrecioT.Text.Replace(".",","));//Se debe remplazar el . por , dependiendo del  idioma de la pc,sino da problemas
                int cantidad = Convert.ToInt32(txtCantidadT.Text);
                bool activo = false;
                string numActivo = txtNumActivo.Text;
                string funcionario = txtFuncionario.Text;
                string cedula = txtCedula.Text;
                if (esActivo.Checked)
                {
                    activo = true;
                }

                if (!txtFechaV.Text.Equals(""))
                {
                    fechaV = Convert.ToDateTime(txtFechaV.Text);

                }
                if (!txtFechaC.Text.Equals(""))
                {
                    fechaC = Convert.ToDateTime(txtFechaV.Text);

                }
                if (!txtFechaG.Text.Equals(""))
                {
                    fechaG = Convert.ToDateTime(txtFechaV.Text);

                }
                object[] nuevoProducto = new object[6];
                nuevoProducto[0] = descripcion;
                nuevoProducto[1] = presentacionEmpaque;
                nuevoProducto[2] = activo;
                nuevoProducto[3] = precioU;
                nuevoProducto[4] = cantidadEmpaque;
                nuevoProducto[5] = subpartida;

                correcto = controladora.agregarProducto(nuevoProducto);


                if (!numActivo.Equals("") && correcto)
                {
                    cantidad = 1;//Por defecto, si el producto ingresado
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Producto " + descripcion + " al ser activo asignado,su cantidad es 1')", true);
                    controladora.agregarActivo(numActivo, funcionario, cedula, descripcion, cantidadEmpaque);
                }
                if (correcto && numFactura == null)
                {//Si se ingreso el producto de mercaderia inicial,se procede a guardar el producto en la tabla Informacion producto
                    controladora.agregarProductoABodega(bodega, descripcion, cantidadEmpaque, programa, subbodega, cantidad, fechaC, fechaG, fechaV);
                }
                else if (correcto)
                {//Si es un producto de factura
                    id_factura = controladora.obtenerIDFactura(numFactura);
                    total = precioU * cantidad;
                    controladora.agregarProductoFactura(id_factura, descripcion, cantidadEmpaque, cantidad, total, fechaC, fechaG, fechaV);
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Producto " + descripcion + " agregado con éxito')", true);
                if (numFactura != null)
                {
                    Detalles_Factura.numFactura = numFactura;
                    Response.Redirect("Detalles_Factura");
                }

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
            if (editar)
            {
                Response.Redirect("Detalles_Factura");
            }
            else
            {
                Response.Redirect("Facturas.aspx");
            
            }
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