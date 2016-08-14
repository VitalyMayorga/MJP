using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class MenuPrincipal : System.Web.UI.Page
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
            }
        }

        protected void ingresarFacturas(object sender, EventArgs e)
        {
            if (Session["rol"].Equals("Inclusion Pedidos")) {
                Response.Redirect("Facturas.aspx");
            
            }
            else if (Session["rol"].Equals("Administrador Almacen")) {
                Response.Redirect("RevisionFacturas.aspx");
            }
        }

        protected void ingresarMenuAdministracion(object sender, EventArgs e)
        {
            Response.Redirect("Administracion.aspx");
        }

        protected void ingresarRequisiciones(object sender, EventArgs e)
        {
            if (Session["rol"].Equals("Usuarios"))
            {
                Response.Redirect("Requisiciones.aspx");

            }
            else if (Session["rol"].Equals("Aprobador") || Session["rol"].Equals("Revision y Aprobador Almacen"))
            {
                Response.Redirect("RevisionRequisiciones.aspx");
            }
        }

        protected void ingresarReportes(object sender, EventArgs e)
        {
            Response.Redirect("Reportes.aspx");
        }

        protected void ingresarControlActivos(object sender, EventArgs e)
        {
            Response.Redirect("ControlActivos.aspx");
        }

        protected void ingresarDevolucionBajas(object sender, EventArgs e)
        {
            if (Session["rol"].Equals("Administrador Almacen"))
            {
                Response.Redirect("DevolucionBajas.aspx");

            }
            else if (Session["rol"].Equals("Aprobador"))
            {
                Response.Redirect("RevisionBajas.aspx");
            }            
        }

        protected void ingresarSeguimiento(object sender, EventArgs e)
        {
            Response.Redirect("Seguimiento.aspx");
        }

    }
}