using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Activos : System.Web.UI.Page
    {
        public static bool editar;
        ControladoraActivos controladora = new ControladoraActivos();
        Bitacora bitacora = new Bitacora();
        public static string numActivo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {//Se revisa primero si tiene los permisos para ingresar al sitio
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (rol.Equals("Inclusion Pedidos") || rol.Equals("Administrador Almacen"))
                {
                    llenarListaActivos();

                    if (editar)
                    {
                        //campos no editables
                        ListaActivos.Enabled = false;
                        txtNumActivo.Enabled = false;
                        llenarDatosActivo();
                    }
                }

                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {

                    Response.Redirect("MenuPrincipal");
                }
            }
        }

        private void llenarListaActivos() {
            ListaActivos.Items.Clear();
            List<string> bodegas = (List<string>)Session["bodegas"];
            string bodega = bodegas[0];
            Dictionary<string, int> activos = controladora.getActivos(bodega);
            ListaActivos.Items.Add("---Elija un Activo---");
            foreach (var nombreS in activos)
            {
                ListaActivos.Items.Add(new ListItem { Text = nombreS.Key, Value = nombreS.Value.ToString() });
            }
        
        }

        //Si se selecciona un Activo el msj de error se esconde
        protected void revisarActivo(object sender, EventArgs e)
        {
            if (ListaActivos.SelectedIndex != 0)
            {
                MsjErrorActivo.Style.Add("display", "none");
            }
        }

        //Obtiene los datos del producto a editar
        protected void llenarDatosActivo()
        {
            List<string> datos = controladora.obtenerDatosActivo(numActivo);
            txtNumActivo.Text = datos[0];
            txtFuncionario.Text = datos[1];
            txtCedula.Text = datos[2];
            txtDocumento.Text = datos[3];
            txtFecha.Text = datos[4];
            ListaActivos.Items.FindByText(datos[5]).Selected = true;

        }

        //Revisa que los datos proporcionados estén correctos,de ser así los inserta y se mantiene en la página para nuevo ingreso de productos
        protected void aceptar(object sender, EventArgs e)
        {
            
            if (validar())
            {//Si todo es valido, entonces procedo a obtener los datos dados por el usuario
                numActivo = txtNumActivo.Text;
                DateTime fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", null); ;
                string funcionario = txtFuncionario.Text;
                string cedula =txtCedula.Text;
                string documento = txtDocumento.Text;
                                
                object[] nuevoActivo = new object[6];
                nuevoActivo[0] = numActivo;
                nuevoActivo[1] = fecha;
                nuevoActivo[2] = funcionario;
                nuevoActivo[3] = cedula;
                nuevoActivo[4] = documento;
                nuevoActivo[5] = Convert.ToInt32(ListaActivos.SelectedValue.ToString());
                              
                if (editar)
                {
                    controladora.modificarActivo(nuevoActivo);
                    Response.Redirect("ControlActivos");
                }
                else
                {
                    int resultado = controladora.agregarActivoFinal(nuevoActivo);
                    if (resultado == 0) {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('El número de activo ya existe')", true);

                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje de alerta", "alert('Activo " + ListaActivos.SelectedItem.Text + " agregado con éxito')", true);

                }
            }
                            
                //Al final se limpian los campos
                txtNumActivo.Text = "";
                txtFecha.Text = "";
                txtFuncionario.Text = "";
                txtCedula.Text = "";
                txtDocumento.Text = "";
                ListaActivos.SelectedIndex = 0;

            
        }
       
        protected void cancelar(object sender, EventArgs e)
        {
              Response.Redirect("ControlActivos");
            
        }

        //Metodo encargado de validar los campos antes de ejecutar una accion
        protected bool validar()
        {
            bool valido = false;
            string numero = txtNumActivo.Text;
            string funcionario = txtFuncionario.Text;
            string cedula = txtCedula.Text;
            string fecha = txtFecha.Text;
            string documento = txtDocumento.Text;
            string producto = ListaActivos.Items[ListaActivos.SelectedIndex].Text;
            if (producto.Equals("---Elija un Activo---"))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorActivo.Style.Add("display", "block");
                MsjErrorNumActivo.Style.Add("display", "none");
                MsjErrorCedula.Style.Add("display", "none");
                MsjErrorFuncionario.Style.Add("display", "none");
                MsjErrorFecha.Style.Add("display", "none");
                MsjErrorDocumento.Style.Add("display", "none");
            }
            else if (numero.Replace(" ","").Equals(""))
            {
                MsjErrorActivo.Style.Add("display", "none");
                MsjErrorNumActivo.Style.Add("display", "block");
                MsjErrorCedula.Style.Add("display", "none");
                MsjErrorFuncionario.Style.Add("display", "none");
                MsjErrorFecha.Style.Add("display", "none");
                MsjErrorDocumento.Style.Add("display", "none");
            }
            else if (funcionario.Replace(" ","").Equals(""))
            {
                MsjErrorActivo.Style.Add("display", "none");
                MsjErrorNumActivo.Style.Add("display", "none");
                MsjErrorCedula.Style.Add("display", "none");
                MsjErrorFuncionario.Style.Add("display", "block");
                MsjErrorFecha.Style.Add("display", "none");
                MsjErrorDocumento.Style.Add("display", "none");
            }
            else if (cedula.Replace(" ","").Equals(""))
            {
                MsjErrorActivo.Style.Add("display", "none");
                MsjErrorNumActivo.Style.Add("display", "none");
                MsjErrorCedula.Style.Add("display", "block");
                MsjErrorFuncionario.Style.Add("display", "none");
                MsjErrorFecha.Style.Add("display", "none");
                MsjErrorDocumento.Style.Add("display", "none");

            }
            else if (documento.Replace(" ","").Equals(""))
            {
                MsjErrorActivo.Style.Add("display", "none");
                MsjErrorNumActivo.Style.Add("display", "none");
                MsjErrorCedula.Style.Add("display", "none");
                MsjErrorFuncionario.Style.Add("display", "none");
                MsjErrorFecha.Style.Add("display", "none");
                MsjErrorDocumento.Style.Add("display", "block");

            }
            else if (fecha.Equals(""))
            {
                MsjErrorActivo.Style.Add("display", "none");
                MsjErrorNumActivo.Style.Add("display", "none");
                MsjErrorCedula.Style.Add("display", "none");
                MsjErrorFuncionario.Style.Add("display", "none");
                MsjErrorFecha.Style.Add("display", "block");
                MsjErrorDocumento.Style.Add("display", "none");

            }
            else
            {
                valido = true;
            }
            return valido;
                
        }
    }
}