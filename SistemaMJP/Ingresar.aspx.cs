using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Ingresar : System.Web.UI.Page
    {
        ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Login de cualquier Usuario
        protected void login(object sender, EventArgs e) {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;
            contraseña = contraseña.Replace(" ", "");
            usuario = usuario.Replace(" ", "");
            if (usuario.Equals(""))
            {
                MsjErrorUsuario.Style.Add("display", "block");
                MsjErrorContraseña.Style.Add("display", "none");
                txtUsuario.Text = "";
            }
            
            else if (contraseña.Equals(""))
            {
                MsjErrorUsuario.Style.Add("display", "none");
                MsjErrorContraseña.Style.Add("display", "block");
                txtContraseña.Text = "";
            }

            else if (servicio.Autenticar(usuario, contraseña))
            {//Se guardan los datos importantes del usuario logueado en la variable Session, para su posterior uso cuando sea necesario en el programa
                Session["username"] = servicio.GetUsername(usuario);
                Session["rol"] = servicio.GetRol(usuario);
                if (!Session["rol"].Equals("Administrador General")) {//Solo el AdminGeneral no posee bodegas ni programas presupuestarios, ya que su labor es crear cuentas
                    if (!Session["rol"].Equals("Inclusion Pedidos") && !Session["rol"].Equals("Administrador Almacen"))
                    {
                        Session["programas"] = servicio.GetProgramasPresupuestarios(usuario);
                    
                    
                    }
                    Session["bodegas"] = servicio.GetBodegas(usuario);
                }
                Session["userID"] = servicio.GetID(usuario);
                Session["correoInstitucional"] = usuario;
                Response.Redirect("MenuPrincipal.aspx");
            }
            else {
                MsjErrorLogin.Style.Add("display", "block");
                MsjErrorContraseña.Style.Add("display", "none");
                MsjErrorUsuario.Style.Add("display", "none");
                
            }
            
        }
    }
}