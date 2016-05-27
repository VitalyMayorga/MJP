using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class DevolucionBajas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar.aspx");
                }               
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }
            }
        }

        protected void ingresarBajas(object sender, EventArgs e)
        {
            Response.Redirect("Bajas.aspx");
        }

        protected void ingresarDevoluciones(object sender, EventArgs e)
        {
            Response.Redirect("Devoluciones.aspx");
        }

        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal.aspx");
        }
    }
}