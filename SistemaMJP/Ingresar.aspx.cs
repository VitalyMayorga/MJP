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
        EmailManager email = new EmailManager();
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
                    if (!Session["rol"].Equals("Inclusión Pedidos") && !Session["rol"].Equals("Administrador Almacen"))
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

        //Restablece la contraseña si el usuario esta dado
        protected void restablecer(object sender, EventArgs e) {
            try
            {
                string correo = txtUsuario.Text;
                if (!String.IsNullOrEmpty(correo))
                {
                    if (!String.IsNullOrEmpty(servicio.GetUsername(correo)))
                    {

                        Random random = new Random();
                        int randomNumber = random.Next(0, 100);
                        Response.Cookies["value"].Value = randomNumber.ToString();
                        Response.Cookies["value"].Expires = DateTime.Now.AddDays(1);
                        string data1 = servicio.TamperProofStringEncode(correo, "MJP");
                        string data2 = servicio.TamperProofStringEncode(randomNumber.ToString(), "MJP");
                        string msjCorreo = "Este es un correo automático para restablecer la contraseña, si usted no pidió restablecer la contraseña,ignore este mensaje\n\nPara cambiar la contraseña, ingrese al siguiente link:\n http://localhost:62386/Restablecer_contraseña.aspx?usuario=" + data1 + "&val=" + data2 + "";
                        List<string> lista = new List<string>();
                        lista.Add(correo);
                        email.MailSender(msjCorreo, "Restablecer contraseña", lista);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Se ha enviado un correo para restablecer la contraseña')", true);
                    }
                }

            }
            catch (Exception ex) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Usuario incorrecto')", true);
            }
            
            
        }
    }
}