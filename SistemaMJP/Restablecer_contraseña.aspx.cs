using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Restablecer_contraseña : System.Web.UI.Page
    {
        private EmailManager email = new EmailManager();
        ServicioLogin servicio = new ServicioLogin();
        private string usuario;
        private string valor;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {//Se revisa primero si tiene los permisos para ingresar al sitio
                usuario = servicio.TamperProofStringDecode(Request.QueryString["usuario"], "MJP");
                valor = servicio.TamperProofStringDecode(Request.QueryString["val"], "MJP");
                ViewState["usuario"] = usuario;
                ViewState["valor"] = valor;

                if (Request.Cookies["value"] != null)
                {

                        HttpCookie aCookie = Request.Cookies["value"];
                        string cookieValue = Server.HtmlEncode(aCookie.Value);
                        //Si se ingresa al link y la cookie expiró, redirecciona al login
                        if (!cookieValue.Equals(valor))
                        {
                            Response.Redirect("Ingresar.aspx");
                        }

                }
                else {

                    Response.Redirect("Ingresar.aspx");
                }
               
            }
            else {
                usuario = (string)ViewState["usuario"];
                valor = (string)ViewState["valor"];
            }

        }

         //Revisa que los datos proporcionados estén correctos,y restablece la contraseña
        protected void restablecer(object sender, EventArgs e)
        {
            string pwd1 = txtContraseña.Text;
            string pwd2 = txtContraseña2.Text;

            if (String.IsNullOrEmpty(pwd1)) {
                MsjErrorContraseña.Style.Add("display", "block");
                MsjErrorDifContraseña.Style.Add("display", "none");
                MsjErrorNoContraseña.Style.Add("display", "none");
                MsjErrorTamContraseña.Style.Add("display", "none");
                
            }
            else if (String.IsNullOrEmpty(pwd2)) {
                MsjErrorContraseña.Style.Add("display", "none");
                MsjErrorDifContraseña.Style.Add("display", "none");
                MsjErrorNoContraseña.Style.Add("display", "block");
                MsjErrorTamContraseña.Style.Add("display", "none");
            }
            else if (!pwd1.Equals(pwd2))
            {
                MsjErrorContraseña.Style.Add("display", "none");
                MsjErrorDifContraseña.Style.Add("display", "block");
                MsjErrorNoContraseña.Style.Add("display", "none");
                MsjErrorTamContraseña.Style.Add("display", "none");
            }
            else if (pwd1.Length < 5)
            {
                MsjErrorContraseña.Style.Add("display", "none");
                MsjErrorDifContraseña.Style.Add("display", "none");
                MsjErrorNoContraseña.Style.Add("display", "none");
                MsjErrorTamContraseña.Style.Add("display", "block");
            }
            else {
                //Si todo está bien, se procede a cambiar la contraseña
                servicio.restablecerContraseña(servicio.EncodePassword(string.Concat(usuario, pwd1)), usuario);
                ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Contraseña restablecida con éxito');window.location='Ingresar.aspx';</script>'");
            }
            txtContraseña.Text = "";
            txtContraseña2.Text = "";
        }
    }
}