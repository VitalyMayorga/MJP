using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Preingreso_Productos : System.Web.UI.Page
    {
        private ControladoraProductos controladora = new ControladoraProductos();
        private ServicioLogin servicio = new ServicioLogin();
        private EmailManager email = new EmailManager();
        Bitacora bitacora = new Bitacora();
        public string programa;
        public string valsubBodega;
        public bool tieneSubBodega;
        public string numFactura;
        public string proveedor;
        public string correo;
        public string telefonos;
        public bool nuevoProveedor = false;


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
                else
                {
                    tieneSubBodega = false;
                    valsubBodega = "";
                    llenarDatos();
                }
            }
            else
            {
                try
                {
                    programa = (string)ViewState["programa"];
                    valsubBodega = (string)ViewState["subBodega"];
                    tieneSubBodega = (bool)ViewState["tieneSubBodega"];
                    numFactura = (string)ViewState["numFactura"];
                    proveedor = (string)ViewState["proveedor"];
                    correo = (string)ViewState["correo"];
                    telefonos = (string)ViewState["telefonos"];
                    nuevoProveedor = (bool)ViewState["nuevoProveedor"];

                }
                catch (Exception) { 
                }

            }
        }
        //Revisa si los campos necesarios fueron agregados, de ser así ingresa a la ventana de ingreso de productos
        //En otro caso, despliega los mensajes de error
        protected void aceptar(object sender, EventArgs e)
        {
            string descripcion = "";
            string usuario = (string)Session["correoInstitucional"];
            string fecha = "";
            string bodega = ListaBodegas.Items[ListaBodegas.SelectedIndex].Text;
            programa = ListaProgramas.Items[ListaProgramas.SelectedIndex].Text;
            ViewState["programa"] = programa;
            if (tieneSubBodega)
            {
                valsubBodega = ListaSubBodegas.Items[ListaSubBodegas.SelectedIndex].Text;
                ViewState["subBodega"] = valsubBodega;
            }
            if (!nuevoProveedor)
            {
                proveedor = ListaProveedores.Items[ListaProveedores.SelectedIndex].Text;
                ViewState["proveedor"] = proveedor;
            }
            if (ingresoF.Checked)
            {
                numFactura = txtNumFactura.Text;
                fecha = txtFechaF.Text;
                numFactura = numFactura.Replace(" ", "");//Elimino espacios en blanco para saber si se digito numero factura o no
                ViewState["numFactura"] = numFactura;
            }
            if (bodega.Equals("---Elija una bodega---"))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "block");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");
                MsjErrorFactura.Style.Add("display", "none");

            }

            else if (programa.Equals("---Elija un Programa---"))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "block");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");
                MsjErrorFactura.Style.Add("display", "none");

            }
            else if (tieneSubBodega && (valsubBodega.Equals("---Elija un Departamento---")))
            {
                //Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "block");
                MsjErrorSubPartida.Style.Add("display", "none");
                MsjErrorFactura.Style.Add("display", "none");

            }

            else if (ingresoF.Checked && numFactura.Equals(""))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");
                MsjErrorFactura.Style.Add("display", "block");
                MsjErrorFecha.Style.Add("display", "none");
                MsjErrorProveedor.Style.Add("display", "none");

            }
            else if (ingresoF.Checked && fecha.Equals(""))
            {
                MsjErrorFactura.Style.Add("display", "none");
                MsjErrorFecha.Style.Add("display", "block");
                MsjErrorProveedor.Style.Add("display", "none");

            }
            else if (ingresoF.Checked && !nuevoProveedor && proveedor.Equals("---Elija un Proveedor---"))
            {
                MsjErrorProveedor.Style.Add("display", "block");
                MsjErrorFactura.Style.Add("display", "none");
            }
            else
            {//Se envían los datos necesarios para empezar a ingresar productos
                int idBodega = controladora.obtenerIDBodega(bodega);

                //Ingreso_Productos.bodega = idBodega;
                //Ingreso_Productos.programa = Convert.ToInt32(ListaProgramas.SelectedValue);
                int idSubBodega = 0;//Por default, subbodega 0 = No hay subbodega asignada
                if (tieneSubBodega)
                {
                    idSubBodega = Convert.ToInt32(ListaSubBodegas.SelectedValue);
                    //Ingreso_Productos.subbodega = Convert.ToInt32(ListaSubBodegas.SelectedValue);

                }
                if (ingresoF.Checked)
                {
                    //Ingreso_Productos.numFactura = numFactura;
                    controladora.agregarFactura(idBodega, proveedor, Convert.ToInt32(ListaProgramas.SelectedValue), idSubBodega, numFactura, fecha);
                    descripcion = "Agregada factura" + numFactura;
                    bitacora.registrarActividad(usuario, descripcion);
                }
                descripcion = "Nodifica bodega " + bodega + " para programa " + programa;
                bitacora.registrarActividad(usuario, descripcion);

                Response.Redirect("Ingreso_Productos.aspx?bodega=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idBodega.ToString(), "MJP")) + "&numFactura=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numFactura, "MJP")) + "&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(ListaProgramas.SelectedValue, "MJP")) + "&subbodega=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idSubBodega.ToString(), "MJP")) + "&editar=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode("0", "MJP")));


            }
        }
        //Si el programa Seleccionado posee subbodegas, entonces habilita la elección de la subbodega de dicho Programa
        protected void revisarPrograma(object sender, EventArgs e)
        {
            ListaSubBodegas.Items.Clear();
            Dictionary<string, int> subbodegas = controladora.getSubBodegas(ListaProgramas.Items[ListaProgramas.SelectedIndex].Text, ListaBodegas.Items[ListaBodegas.SelectedIndex].Text);
            if (subbodegas.Count > 0)
            {
                Subbodega.Style.Add("display", "block");
                ListaSubBodegas.Items.Add("---Elija un Departamento---");
                foreach (var nombreSb in subbodegas)
                {
                    ListaSubBodegas.Items.Add(new ListItem { Text = nombreSb.Key, Value = nombreSb.Value.ToString() });
                }
                tieneSubBodega = true;
            }
            else
            {
                Subbodega.Style.Add("display", "none");
                tieneSubBodega = false;

            }
            if (ListaProgramas.SelectedIndex != 0)
            {
                MsjErrorPrograma.Style.Add("display", "none");
            }
            ViewState["tieneSubBodega"] = tieneSubBodega;
        }

        //Si se selecciona la subbodega el msj de error se esconde
        protected void revisarSubB(object sender, EventArgs e)
        {
            if (ListaSubBodegas.SelectedIndex != 0)
            {
                MsjErrorSubBodega.Style.Add("display", "none");
            }
        }
        //Si se selecciona la Bodega el msj de error se esconde
        protected void revisarBodega(object sender, EventArgs e)
        {
            if (ListaBodegas.SelectedIndex != 0)
            {
                MsjErrorBodega.Style.Add("display", "none");
            }
        }

        //Si se selecciona la Bodega el msj de error se esconde
        protected void revisarProveedores(object sender, EventArgs e)
        {
            if (ListaProveedores.SelectedIndex != 0)
            {
                MsjErrorProveedor.Style.Add("display", "none");
            }
            else
            {
                proveedor = ListaProveedores.SelectedItem.Text;
            }
        }

        //Si está seleccionado Mercaderia Inicial,esconde el div de la inserción de una factura y proveedor
        protected void rbMercaderiaI(object sender, EventArgs e)
        {
            if (mercaderiaI.Checked)
            {
                formFacturas.Style.Add("display", "none");
                formProveedor.Style.Add("display", "none");
                formFacturaFecha.Style.Add("display", "none");
            }
        }

        //Si está seleccionado Facturas,muestra el div de numero de facturas y proveedor
        protected void rbIngresoF(object sender, EventArgs e)
        {
            if (ingresoF.Checked)
            {
                formFacturas.Style.Add("display", "block");
                formProveedor.Style.Add("display", "block");
                formFacturaFecha.Style.Add("display", "block");
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
            List<string> bodegas = (List<string>)Session["bodegas"];
            string bodega = bodegas[0];
            Dictionary<string, int> programas = controladora.getProgramas();
            Dictionary<string, int> proveedores = controladora.getProveedores();
            ListaBodegas.Items.Add("---Elija una bodega---");
            ListaBodegas.Items.Add(bodega);
            ListaProgramas.Items.Add("---Elija un Programa---");
            foreach (var nombreP in programas)
            {
                ListaProgramas.Items.Add(new ListItem { Text = nombreP.Key, Value = nombreP.Value.ToString() });
            }

            ListaProveedores.Items.Add("---Elija un Proveedor---");
            foreach (var nombrePr in proveedores)
            {
                ListaProveedores.Items.Add(new ListItem { Text = nombrePr.Key, Value = nombrePr.Value.ToString() });
            }
            ingresoF.Checked = true;
        }
        //Revisa si los datos del nuevo proveedor estan dados, sino no cierra el modal hasta que se llenen todos los campos
        protected void aceptarProveedor(object sender, EventArgs e)
        {
            string tmp = txtNombreProveedor.Text.Replace(" ", "");
            proveedor = txtNombreProveedor.Text;
            correo = txtCorreo.Text.Replace(" ", "");
            telefonos = txtTelefonos.Text.Replace(" ", "");
            if (tmp.Equals(""))
            {
                txtNombreProveedor.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (correo.Equals("") || !email.IsValidEmail(correo))
            {
                txtCorreo.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (telefonos.Equals(""))
            {
                txtTelefonos.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#ProveedorModal').modal('hide');</script>");
                nuevoProveedor = true;
                ListaProveedores.Enabled = false;
                ListaProveedores.SelectedIndex = 0;
                controladora.agregarProveedor(proveedor, correo, telefonos);
                string descripcion = "Agregado proveedor" + proveedor;
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcion);
                ViewState["proveedor"] = proveedor;
                ViewState["correo"] = correo;
                ViewState["telefonos"] = telefonos;
                ViewState["nuevoProveedor"] = nuevoProveedor;
            }

        }


    }
}