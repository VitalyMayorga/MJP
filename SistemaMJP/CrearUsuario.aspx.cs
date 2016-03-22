using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
       private ControladoraRolesPerfiles controladoraRP = new ControladoraRolesPerfiles();
       private ControladoraProgramasPresupuestarios controladoraPP = new ControladoraProgramasPresupuestarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarListRoles();
            llenarListBoxProgramas();
        }

        protected void regresarRP(object sender, EventArgs e)
        {
            Response.Redirect("RolesPerfiles");
        }

        internal void llenarListRoles()
        {
            if (!IsPostBack)
            {
                Dictionary<string, int> nomRol = new Dictionary<string, int>();               
                ListRoles.Items.Clear();               

                ListRoles.Items.Insert(0, new ListItem("--Selecione el Rol--", "0"));

                nomRol = controladoraRP.getRoles();               

                //Itera sobre el diccionario para obtener la bodega y el respectivo id y guardarlo en un dropdownlist
                foreach (var item in nomRol)
                {
                    ListRoles.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }

            }
        }

        internal void llenarListBoxProgramas()
        {
            if (!IsPostBack)
            {
                Dictionary<string, int> nomPrograma = new Dictionary<string, int>();
                ListBoxProgramas.Items.Clear();
                nomPrograma = controladoraPP.getProgramas();

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomPrograma)
                {
                    ListBoxProgramas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }
                
            }

        }

        protected void asignar(object sender, EventArgs e)
        {


        }
        protected void desasignar(object sender, EventArgs e)
        {


        }

    }
}