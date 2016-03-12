using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        ServicioLogin servicio = new ServicioLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = servicio.GetUsername((string)(Session["correoInstitucional"]));
            if (user!=null)
                {
                    nombreLabel.Text = "Bienvenido "+user;
                }
                else
                {
                    nombreLabel.Text = "Bienvenido";
                }
            }


        protected void clickSalir(object sender, EventArgs e)//Desloguearse del Sistema
        {
            Session["correoInstitucional"] = null;
            Session["programas"] = null;
            Session["bodegas"] = null;
            Session["username"] = null;
            Session["rol"] = null;
            Response.Redirect("~/Ingresar.aspx");
        }
    }

}