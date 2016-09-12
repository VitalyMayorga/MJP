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
        public  int bodega;
        public  int subbodega;
        public  int programa;
        public  string numFactura;
        public  int subpartida;
        public  int idProducto;
        public  int id_factura;
        public  bool editar;
        ControladoraProductos controladora = new ControladoraProductos();
        ServicioLogin servicio = new ServicioLogin();
        Bitacora bitacora = new Bitacora();
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

                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {//Leer los datos del URL
                    try
                    {
                        
                        string dato = servicio.TamperProofStringDecode(Request.QueryString["numFactura"], "MJP");
                        if (!dato.Equals("noData"))
                        {
                            numFactura = dato;
                        }
                        dato = servicio.TamperProofStringDecode(Request.QueryString["editar"], "MJP");
                        if (dato.Equals("1"))
                        {
                            editar = true;
                        }
                        else
                        {
                            editar = false;
                            bodega = Convert.ToInt32(servicio.TamperProofStringDecode(Request.QueryString["bodega"], "MJP"));
                            programa = Convert.ToInt32(servicio.TamperProofStringDecode(Request.QueryString["programa"], "MJP"));
                            subbodega = Convert.ToInt32(servicio.TamperProofStringDecode(Request.QueryString["subbodega"], "MJP"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("MenuPrincipal.aspx");
                    }
                    noActivo.Checked = true;
                    //lleno la lista de subpartidas
                    ListaSubPartidas.Items.Clear();
                    Dictionary<string, int> subpartidas = controladora.getSubPartidas();
                    ListaSubPartidas.Items.Add("---Elija una Subpartida---");
                    foreach (var nombreS in subpartidas)
                    {
                        ListaSubPartidas.Items.Add(new ListItem { Text = nombreS.Key, Value = nombreS.Value.ToString() });
                    }
                    if (editar)
                    {
                        try
                        {
                            id_factura = Convert.ToInt32(servicio.TamperProofStringDecode(Request.QueryString["id_factura"], "MJP"));
                            idProducto = Convert.ToInt32(servicio.TamperProofStringDecode(Request.QueryString["idProducto"], "MJP"));
                            
                        }
                        catch (Exception ex)
                        {
                            Response.Redirect("MenuPrincipal.aspx");
                        }
                        //campos no editables
                        txtDescripcion.Enabled = false;
                        ListaSubPartidas.Enabled = false;
                        esActivo.Enabled = false;
                        noActivo.Enabled = false;
                        //La informacion del activo si fue asignada, se modifica desde la interfaz de control de activos, no desde esta

                        ViewState["idProducto"] = idProducto;
                        ViewState["id_factura"] = id_factura;
                        llenarDatosProducto();
                    }
                   
                }
                ViewState["bodega"] = bodega;
                ViewState["programa"] = programa;
                ViewState["numFactura"] = numFactura;
                ViewState["subbodega"] = subbodega;
                ViewState["editar"] = editar;
            }
            else {
                try { 
                    bodega= (int)ViewState["bodega"];
                    subbodega = (int)ViewState["subbodega"];
                    programa = (int)ViewState["programa"];
                    numFactura = (string)ViewState["numFactura"];
                    subpartida = (int)ViewState["subpartida"];
                    editar = (bool)ViewState["bodega"];
                    idProducto = (int)ViewState["idProducto"];
                    id_factura = (int)ViewState["id_factura"];
                }
                catch (Exception) { }
            }
        }

        //Si se selecciona la Subpartida el msj de error se esconde
        protected void revisarSubPartida(object sender, EventArgs e)
        {
            if (ListaSubPartidas.SelectedIndex != 0)
            {
                MsjErrorSubPartida.Style.Add("display", "none");
            }
        }
        //Obtiene los datos del producto a editar
        protected void llenarDatosProducto() {
            List<string> datos = controladora.obtenerDatosProducto(id_factura, idProducto);
            txtDescripcion.Text = datos[0];
            txtPresentacion.Text = datos[1];
            txtCantidadE.Text = datos[2];
            txtCantidadT.Text = datos[3];
            //Obtengo  el precio unitario con base en la cntidad y el precioTotal
            int cantidad = Convert.ToInt32(datos[3]);
            decimal precio = Convert.ToDecimal(datos[4]);
            decimal precioUnitario = precio / cantidad;
            txtPrecioT.Text = precioUnitario.ToString();
            int activo = Convert.ToInt32(datos[5]);
            if (activo == 1)
            {
                esActivo.Checked = true;
                
            }
            else {
                noActivo.Checked = true;
            }
            if (datos[6] != null) {
                txtFechaV.Text = datos[6];
            }
            if (datos[7] != null)
            {
                txtFechaC.Text = datos[7];
            }
            if (datos[8] != null)
            {
                txtFechaG.Text = datos[8];
            }
            ListaSubPartidas.Items.FindByText(datos[9]).Selected = true;

        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y se mantiene en la página para nuevo ingreso de productos
        protected void aceptar(object sender, EventArgs e)
        {
            bool correcto = false;//Para saber si agregó bien el producto
            if (validar()) {//Si todo es valido, entonces procedo a obtener los datos dados por el usuario
                decimal total = 0;
                id_factura = 0;
                subpartida = Convert.ToInt32(ListaSubPartidas.SelectedValue);
                Nullable<DateTime> fechaV = null;
                Nullable<DateTime> fechaG = null;
                Nullable<DateTime> fechaC = null;
                string descripcion = txtDescripcion.Text;
                //Se modifica la descripcion para que la primer letra sea mayúscula y no hayan tildes
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
                    fechaV = DateTime.ParseExact(txtFechaV.Text, "dd/MM/yyyy", null);
                
                }
                if (!txtFechaC.Text.Equals(""))
                {
                    fechaC = DateTime.ParseExact(txtFechaC.Text, "dd/MM/yyyy", null);

                }
                if (!txtFechaG.Text.Equals(""))
                {
                    fechaG = DateTime.ParseExact(txtFechaG.Text, "dd/MM/yyyy", null);

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
                if (correcto && String.IsNullOrEmpty(numFactura))
                {//Si se ingreso el producto de mercaderia inicial,se procede a guardar el producto en la tabla Informacion producto
                    controladora.agregarProductoABodega(bodega, descripcion, cantidadEmpaque, programa, subbodega, cantidad, fechaC, fechaG, fechaV);
                    string descripcionRA = "Agregado producto " + descripcion+" a la bodega";
                    string usuario = (string)Session["correoInstitucional"];
                    bitacora.registrarActividad(usuario, descripcionRA);
                }
                else if(correcto){//Si es un producto de factura
                    id_factura = controladora.obtenerIDFactura(numFactura);
                    total = precioU * cantidad;
                    if(editar){
                        controladora.modificarProductoFactura(id_factura, idProducto, cantidadEmpaque, cantidad, total, fechaC, fechaG, fechaV);
                        Response.Redirect("DetallesFactura.aspx?numF=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numFactura, "MJP")));
                    }
                    else
                    {
                        controladora.agregarProductoFactura(id_factura, descripcion, cantidadEmpaque, cantidad, total, fechaC, fechaG, fechaV);

                    }
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
                MsjErrorNumActivo.Style.Add("display", "none");
                MsjErrorCedula.Style.Add("display", "none");
                MsjErrorFuncionario.Style.Add("display", "none");

            }
        }
        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y redirecciona a la página DetallesFactura
        protected void aceptarYSalir(object sender, EventArgs e)
        {
            bool correcto = false;//Para saber si agregó bien el producto
            if (validar())
            {//Si todo es valido, entonces procedo a obtener los datos dados por el usuario
                decimal total = 0;
                id_factura = 0;
                subpartida = Convert.ToInt32(ListaSubPartidas.SelectedValue);
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
                if (!txtFechaV.Text.Equals(""))
                {
                    fechaV = DateTime.ParseExact(txtFechaV.Text, "dd/MM/yyyy", null);

                }
                if (!txtFechaC.Text.Equals(""))
                {
                    fechaC = DateTime.ParseExact(txtFechaC.Text, "dd/MM/yyyy", null);

                }
                if (!txtFechaG.Text.Equals(""))
                {
                    fechaG = DateTime.ParseExact(txtFechaG.Text, "dd/MM/yyyy", null);

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
                    controladora.agregarActivo(numActivo, funcionario, cedula, descripcion, cantidadEmpaque);
                    string descripcionRA = "Agregado producto " + descripcion + " como activo a "+funcionario+ " cédula "+cedula;
                    string usuario = (string)Session["correoInstitucional"];
                    bitacora.registrarActividad(usuario, descripcionRA);
                    
                }
                if (correcto && String.IsNullOrEmpty(numFactura))
                {//Si se ingreso el producto de mercaderia inicial,se procede a guardar el producto en la tabla Informacion producto
                    controladora.agregarProductoABodega(bodega, descripcion, cantidadEmpaque, programa, subbodega, cantidad, fechaC, fechaG, fechaV);
                    string descripcionRA = "Agregado producto " + descripcion + " a la bodega";
                    string usuario = (string)Session["correoInstitucional"];
                    bitacora.registrarActividad(usuario, descripcionRA);
                }
                else if (correcto)
                {//Si es un producto de factura
                    id_factura = controladora.obtenerIDFactura(numFactura);
                    total = precioU * cantidad;
                    if (editar)
                    {
                        controladora.modificarProductoFactura(id_factura, idProducto, cantidadEmpaque, cantidad, total, fechaC, fechaG, fechaV);
                    }
                    else
                    {
                        controladora.agregarProductoFactura(id_factura, descripcion, cantidadEmpaque, cantidad, total, fechaC, fechaG, fechaV);

                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Producto " + descripcion + " agregado con éxito')", true);
                if (!String.IsNullOrEmpty(numFactura))
                {
                    //DetallesFactura.numFactura = numFactura;
                    Response.Redirect("DetallesFactura.aspx?numF=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numFactura, "MJP")));
                }
                else {
                    Response.Redirect("MenuPrincipal");
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
                txtCantidadT.Text = "1";
                txtCantidadT.Enabled = false;

                
            }
        }
        //Si está seleccionado no activo,esconde el div de la inserción de activos
        protected void rbNo(object sender, EventArgs e)
        {
            validar();
            if (noActivo.Checked)
            {
                formActivo.Style.Add("display", "none");
                txtCantidadT.Enabled = true;
               
            }
        }
        //Redirecciona a la pagina facturas
        protected void cancelar(object sender, EventArgs e)
        {
            if (editar)
            {
                //DetallesFactura.numFactura = numFactura;
                Response.Redirect("DetallesFactura.aspx?numF"+HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numFactura,"MJP")));
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
            string subpartida = ListaSubPartidas.Items[ListaSubPartidas.SelectedIndex].Text;
            string tmp = descripcion.Replace(" ", "");
            if (subpartida.Equals("---Elija una Subpartida---"))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "block");
            }
            else if (tmp.Equals(""))
            {
                MsjErrorDescripcion.Style.Add("display", "block");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");
            }
            else if (presentacion.Equals(""))
            {
                MsjErrorPresentacion.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");
            }
            else if (cantidad.Equals(""))
            {
                MsjErrorCantEmp.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");

            }
            else if (cantidadT.Equals(""))
            {
                MsjErrorCantidad.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorPrecio.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");

            }
            else if (precio.Equals(""))
            {
                MsjErrorPrecio.Style.Add("display", "block");
                MsjErrorDescripcion.Style.Add("display", "none");
                MsjErrorPresentacion.Style.Add("display", "none");
                MsjErrorCantidad.Style.Add("display", "none");
                MsjErrorCantEmp.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");


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
                        else {
                            valido = true;
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