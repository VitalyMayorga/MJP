using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        ServicioLogin servicio = new ServicioLogin();
        ControladoraUsuarios controladoraU = new ControladoraUsuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {
                    cargarDatos();
                }
            }
            
        }

        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }
           

        internal void cargarDatos(){
           string datos = servicio.GetUsername((string)Session["correoInstitucional"]);
           char[] delimiterChars = { ' ', '\t' };
           string[] words = datos.Split(delimiterChars); 
           txtNombre.Text= words[0];
           if (words.Length==3)
           {
               TextApellidos.Text = words[1] + ' ' + words[2];
           }
           else
           {
              TextApellidos.Text = words[1];
           }
           
        }

        protected void editarInfo(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                MsjErrortextNombre.Style.Add("display", "block");

                if (TextApellidos.Text == "")
                {
                    MsjErrortextApellidos.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextApellidos.Style.Add("display", "none");
                }

                if (txtPassword.Text == "")
                {
                    MsjErrortextPassword.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextPassword.Style.Add("display", "none");
                }            

            }
            else if(TextApellidos.Text == "")
            {
                MsjErrortextNombre.Style.Add("display", "none");
                MsjErrortextApellidos.Style.Add("display", "block");              
                
                if (txtPassword.Text == "")
                {
                    MsjErrortextPassword.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextPassword.Style.Add("display", "none");
                }  
                 
            }
            else if (txtPassword.Text == "")
            {
                MsjErrortextNombre.Style.Add("display", "none");
                MsjErrortextApellidos.Style.Add("display", "none");
                MsjErrortextPassword.Style.Add("display", "block"); 
            } 
            else{
                MsjErrortextPassword.Style.Add("display", "none");

                if (revisarPssword())
                {
                    controladoraU.editarInfoUsuario((string)Session["correoInstitucional"], txtPassword.Text, txtNombre.Text, TextApellidos.Text);
                    Response.Redirect("MenuPrincipal");
                }else{
                   MsjErrortextRevisarPassword.Style.Add("display", "block");  
                }
                
            }    

        }

        protected bool revisarPssword()
        {
            bool comparacion = false;
            if (txtPassword.Text == txtPassword2.Text)
            {
                comparacion = true;
            }
            return comparacion;
        }


    }

}