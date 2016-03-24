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
       private string pp = "";
       private string b = "";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarListRoles();
            llenarListBoxProgramas();
            llenarListBoxBodegas();
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
                ListBoxProgramasAsignados.Items.Clear();
                nomPrograma = controladoraRP.getProgramas();

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomPrograma)
                {
                    ListBoxProgramasAsignados.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }
                
            }

        }

        internal void llenarListBoxBodegas()
        {
            if (!IsPostBack)
            {
                Dictionary<string, int> nomBodega = new Dictionary<string, int>();
                ListBoxBodegasAsignadas.Items.Clear();
                nomBodega = controladoraRP.getBodegas();

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomBodega)
                {
                    ListBoxBodegasAsignadas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }

            }

        }

        internal void llenarListBoxSubBodegas(string programa, string bodega)
        {
            if (!IsPostBack)
            {
                Dictionary<string, int> nomSubBodega = new Dictionary<string, int>();
                ListBoxSubBodegasAsignadas.Items.Clear();
                //nomSubBodega = controladoraRP.getSubBodegas(programa, bodega);

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomSubBodega)
                {
                    ListBoxSubBodegasAsignadas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }

            }

        }

        protected void asignarProgramas(object sender, EventArgs e)
        {
            string text = ListBoxProgramasDisponibles.SelectedItem.ToString();
            string value = ListBoxProgramasDisponibles.SelectedValue.ToString();
            ListBoxProgramasAsignados.Items.Add(new ListItem { Text = text, Value = value });
            ListBoxProgramasDisponibles.Items.Remove(ListBoxProgramasDisponibles.SelectedItem); 
        }

        protected void desasignarProgramas(object sender, EventArgs e)
        {           
            string text = ListBoxProgramasAsignados.SelectedItem.ToString();
            string value = ListBoxProgramasAsignados.SelectedValue.ToString();
            ListBoxProgramasDisponibles.Items.Add(new ListItem { Text = text, Value = value });
            ListBoxProgramasAsignados.Items.Remove(ListBoxProgramasAsignados.SelectedItem);               
        }

        protected void asignarBodegas(object sender, EventArgs e)
        {
            string text = ListBoxBodegasDisponibles.SelectedItem.ToString();
            string value = ListBoxBodegasDisponibles.SelectedValue.ToString();
            ListBoxBodegasAsignadas.Items.Add(new ListItem { Text = text, Value = value });
            ListBoxBodegasDisponibles.Items.Remove(ListBoxBodegasDisponibles.SelectedItem);           
            /*if (ListBoxProgramasAsignados.Items.IndexOf(ListBoxProgramasAsignados.Items.FindByText("Administración Penitenciaria")) != -1)
            {
                llenarListBoxSubBodegas("Administración Penitenciaria",);
            }*/
        }

        protected void desasignarBodegas(object sender, EventArgs e)
        {
            string text = ListBoxBodegasAsignadas.SelectedItem.ToString();
            string value = ListBoxBodegasAsignadas.SelectedValue.ToString();
            ListBoxBodegasDisponibles.Items.Add(new ListItem { Text = text, Value = value });
            ListBoxBodegasAsignadas.Items.Remove(ListBoxBodegasAsignadas.SelectedItem);
           /* if (ListBoxProgramasAsignados.Items.IndexOf(ListBoxProgramasAsignados.Items.FindByText("Administración Penitenciaria")) != -1)
            {
                Dictionary<string, int> items = new Dictionary<string, int>();

                foreach (ListItem item in ListBoxProgramasAsignados.Items)
                {
                    items.Add(item.Text, Int32.Parse(item.Value));
                } 
              
                foreach(KeyValuePair<string, int> entry in items)
                {
                    llenarListBoxSubBodegas("Administración Penitenciaria", entry.Key);
                }
                        

            }*/
        }

        protected void asignarSubBodegas(object sender, EventArgs e)
        {
            string text = ListBoxSubBodegasDisponibles.SelectedItem.ToString();
            string value = ListBoxSubBodegasDisponibles.SelectedValue.ToString();
            ListBoxSubBodegasAsignadas.Items.Add(new ListItem { Text = text, Value = value });
            ListBoxSubBodegasDisponibles.Items.Remove(ListBoxSubBodegasDisponibles.SelectedItem); 
        }

        protected void desasignarSubBodegas(object sender, EventArgs e)
        {
            string text = ListBoxSubBodegasAsignadas.SelectedItem.ToString();
            string value = ListBoxSubBodegasAsignadas.SelectedValue.ToString();
            ListBoxSubBodegasDisponibles.Items.Add(new ListItem { Text = text, Value = value });
            ListBoxSubBodegasAsignadas.Items.Remove(ListBoxSubBodegasAsignadas.SelectedItem); 
        }

        protected void agregar(object sender, EventArgs e)
        {
            Dictionary<string, int> items = new Dictionary<string, int>();

            foreach (ListItem item in ListBoxProgramasAsignados.Items)
            {
                items.Add(item.Text, Int32.Parse(item.Value));
            }

            foreach (var item in items)
            {
                ListBoxSubBodegasAsignadas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
            }
        }
    }
}