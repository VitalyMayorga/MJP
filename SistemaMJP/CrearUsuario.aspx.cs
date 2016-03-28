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
        private ControladoraUsuarios controladoraU = new ControladoraUsuarios();
       
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

                nomRol = controladoraU.getRoles();               

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
                nomPrograma = controladoraU.getProgramas();

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
                ListBodegas.Items.Clear();
                ListBodegas.Items.Insert(0, new ListItem("--Selecione la Bodega--", "0"));
                nomBodega = controladoraU.getBodegas();

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomBodega)
                {
                    ListBoxBodegasAsignadas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString()});
                    ListBodegas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }

            }

        }

        internal void llenarListBoxSubBodegas(string programa, string bodega)
        {
            if (!IsPostBack)
            {
                Dictionary<string, int> nomSubBodega = new Dictionary<string, int>();
                ListBoxSubBodegasAsignadas.Items.Clear();
                nomSubBodega = controladoraU.getSubBodegas(programa, bodega);

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomSubBodega)
                {
                    ListBoxSubBodegasAsignadas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }

            }

        }

        protected void asignarProgramas(object sender, EventArgs e)
        {
            if (ListBoxProgramasDisponibles.SelectedIndex != -1)
            { 
                string text = ListBoxProgramasDisponibles.SelectedItem.ToString();
                string value = ListBoxProgramasDisponibles.SelectedValue.ToString();
                ListBoxProgramasAsignados.Items.Add(new ListItem { Text = text, Value = value });
                ListBoxProgramasDisponibles.Items.Remove(ListBoxProgramasDisponibles.SelectedItem); 
            }
        }

        protected void desasignarProgramas(object sender, EventArgs e)
        {
            if (ListBoxProgramasAsignados.SelectedIndex != -1)
            {
                string text = ListBoxProgramasAsignados.SelectedItem.ToString();
                string value = ListBoxProgramasAsignados.SelectedValue.ToString();
                ListBoxProgramasDisponibles.Items.Add(new ListItem { Text = text, Value = value });
                ListBoxProgramasAsignados.Items.Remove(ListBoxProgramasAsignados.SelectedItem);
            }
        }

        protected void asignarBodegas(object sender, EventArgs e)
        {
            if (ListBoxBodegasDisponibles.SelectedIndex != -1)
            {
                string text = ListBoxBodegasDisponibles.SelectedItem.ToString();
                string value = ListBoxBodegasDisponibles.SelectedValue.ToString();
                ListBoxBodegasAsignadas.Items.Add(new ListItem { Text = text, Value = value });
                ListBoxBodegasDisponibles.Items.Remove(ListBoxBodegasDisponibles.SelectedItem);
                if (ListBoxProgramasAsignados.Items.IndexOf(ListBoxProgramasAsignados.Items.FindByText("Administración Penitenciaria")) != -1)
                {
                    ListBoxSubBodegasAsignadas.Items.Clear();
                    ListBoxSubBodegasDisponibles.Items.Clear();
                    Dictionary<string, int> items = new Dictionary<string, int>();

                    foreach (ListItem item in ListBoxBodegasAsignadas.Items)
                    {
                        items.Add(item.Text, Int32.Parse(item.Value));
                    }

                    foreach (KeyValuePair<string, int> entry in items)
                    {
                        llenarListBoxSubBodegas("Administración Penitenciaria", entry.Key);
                    }
                }
            }
        }

        protected void desasignarBodegas(object sender, EventArgs e)
        {
            if (ListBoxBodegasAsignadas.SelectedIndex != -1)
            {
                string text = ListBoxBodegasAsignadas.SelectedItem.ToString();
                string value = ListBoxBodegasAsignadas.SelectedValue.ToString();
                ListBoxBodegasDisponibles.Items.Add(new ListItem { Text = text, Value = value });
                ListBoxBodegasAsignadas.Items.Remove(ListBoxBodegasAsignadas.SelectedItem);
                if (ListBoxProgramasAsignados.Items.IndexOf(ListBoxProgramasAsignados.Items.FindByText("Administración Penitenciaria")) != -1)
                {
                    ListBoxSubBodegasAsignadas.Items.Clear();
                    ListBoxSubBodegasDisponibles.Items.Clear();
                    Dictionary<string, int> items = new Dictionary<string, int>();

                    foreach (ListItem item in ListBoxBodegasAsignadas.Items)
                    {
                        items.Add(item.Text, Int32.Parse(item.Value));
                    }

                    foreach (KeyValuePair<string, int> entry in items)
                    {
                        llenarListBoxSubBodegas("Administración Penitenciaria", entry.Key);
                    }
                }
            }
        }

        protected void asignarSubBodegas(object sender, EventArgs e)
        {
            if (ListBoxSubBodegasDisponibles.SelectedIndex != -1)
            {
                string text = ListBoxSubBodegasDisponibles.SelectedItem.ToString();
                string value = ListBoxSubBodegasDisponibles.SelectedValue.ToString();
                ListBoxSubBodegasAsignadas.Items.Add(new ListItem { Text = text, Value = value });
                ListBoxSubBodegasDisponibles.Items.Remove(ListBoxSubBodegasDisponibles.SelectedItem);
            }
        }

        protected void desasignarSubBodegas(object sender, EventArgs e)
        {
            if (ListBoxSubBodegasAsignadas.SelectedIndex != -1)
            {
                string text = ListBoxSubBodegasAsignadas.SelectedItem.ToString();
                string value = ListBoxSubBodegasAsignadas.SelectedValue.ToString();
                ListBoxSubBodegasDisponibles.Items.Add(new ListItem { Text = text, Value = value });
                ListBoxSubBodegasAsignadas.Items.Remove(ListBoxSubBodegasAsignadas.SelectedItem);
            }
        }

        protected void agregar(object sender, EventArgs e)
        {
            
        }

        protected void mostrarListBox(object sender, EventArgs e)
        {
            if (ListRoles.SelectedItem.Text=="Usuario" || ListRoles.SelectedItem.Text=="Aprobador" || ListRoles.SelectedItem.Text =="Consulta" || ListRoles.SelectedItem.Text == "Revision y Aprobador Almacen")
            {
                labelPrograma.Style.Add("display", "block");
                listBoxPrograma.Style.Add("display", "block");
                labelBodegas.Style.Add("display", "block");
                listBoxBodegas.Style.Add("display", "block");
                labelSubBodegas.Style.Add("display", "block");
                listBoxSubBodegas.Style.Add("display", "block");
                listBodega.Style.Add("display", "none");
            }
            else if (ListRoles.SelectedItem.Text=="Inclusion Pedidos" || ListRoles.SelectedItem.Text=="Administrador Almacen")
            {
                labelPrograma.Style.Add("display", "none");
                listBoxPrograma.Style.Add("display", "none");
                labelBodegas.Style.Add("display", "none");
                listBoxBodegas.Style.Add("display", "none");
                labelSubBodegas.Style.Add("display", "none");
                listBoxSubBodegas.Style.Add("display", "none");
                listBodega.Style.Add("display", "block");
            }
            else
            {
                labelPrograma.Style.Add("display", "none");
                listBoxPrograma.Style.Add("display", "none");
                labelBodegas.Style.Add("display", "none");
                listBoxBodegas.Style.Add("display", "none");
                labelSubBodegas.Style.Add("display", "none");
                listBoxSubBodegas.Style.Add("display", "none");
                listBodega.Style.Add("display", "none");
            }
        }

    }
}