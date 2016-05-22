using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Administracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Administrador General"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal");
                }
            }
        }

        protected void ingresarBodegas(object sender, EventArgs e)
        {
            Response.Redirect("~/Bodegas.aspx");
        }

        protected void ingresarRolesPerfiles(object sender, EventArgs e)
        {
            Response.Redirect("~/RolesPerfiles.aspx");
        }

        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("~/MenuPrincipal.aspx");
        }
    }
}