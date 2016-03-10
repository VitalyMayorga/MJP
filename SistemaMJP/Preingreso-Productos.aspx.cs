using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Preingreso_Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                llenarDatos();
            }
        }

        protected void aceptar(object sender, EventArgs e)
        {

        }

        protected void llenarDatos()
        {
            ListaBodegas.Items.Add("---Elija una bodega---");
            ListaProgramas.Items.Add("---Elija un Programa---");
            ListaSubBodegas.Items.Add("---Elija un Departamento---");

        }
    }
}