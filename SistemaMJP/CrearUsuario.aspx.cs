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

        internal void revisarPrograma(string programa, string bodega)
        {           
                Dictionary<string, int> nomSubBodega = new Dictionary<string, int>();
                nomSubBodega = controladoraU.getSubBodegas(programa, bodega);

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomSubBodega)
                {
                    ListBoxSubBodegasAsignadas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }    
        }

        internal void llenarListBoxSubBodegas()
        {
            ListBoxSubBodegasAsignadas.Items.Clear();
            ListBoxSubBodegasDisponibles.Items.Clear();
            if (ListBoxProgramasAsignados.Items.IndexOf(ListBoxProgramasAsignados.Items.FindByText("Administración Penitenciaria")) != -1)
            {                
                Dictionary<string, int> items = new Dictionary<string, int>();

                foreach (ListItem item in ListBoxBodegasAsignadas.Items)
                {
                    items.Add(item.Text, Int32.Parse(item.Value));
                }

                foreach (KeyValuePair<string, int> entry in items)
                {
                    revisarPrograma("Administración Penitenciaria", entry.Key);
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
                llenarListBoxSubBodegas();
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
                llenarListBoxSubBodegas();
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
                llenarListBoxSubBodegas();
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
                llenarListBoxSubBodegas();
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

                if (txtCorreo.Text == "")
                {
                    MsjErrortextCorreo.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextCorreo.Style.Add("display", "none");
                }

                if (ListRoles.SelectedValue == "0")
                {
                    MsjErrorListRol.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorListRol.Style.Add("display", "none");
                    if (ListRoles.SelectedItem.Text == "Inclusion Pedidos" || ListRoles.SelectedItem.Text == "Administrador Almacen")
                    {
                        if (ListBodegas.SelectedValue == "0")
                        {
                            MsjErrorlistBodega.Style.Add("display", "block");
                        }
                        else
                        {
                            MsjErrorlistBodega.Style.Add("display", "none");
                        }
                    }
                }

            }
            else if(TextApellidos.Text == "")
            {
                MsjErrortextNombre.Style.Add("display", "none");
                MsjErrortextApellidos.Style.Add("display", "block");              
                
                if (txtCorreo.Text == "")
                {
                    MsjErrortextCorreo.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextCorreo.Style.Add("display", "none");
                }

                if (ListRoles.SelectedValue == "0")
                {
                    MsjErrorListRol.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorListRol.Style.Add("display", "none");
                    if (ListRoles.SelectedItem.Text == "Inclusion Pedidos" || ListRoles.SelectedItem.Text == "Administrador Almacen")
                    {
                        if (ListBodegas.SelectedValue == "0")
                        {
                            MsjErrorlistBodega.Style.Add("display", "block");
                        }
                        else
                        {
                            MsjErrorlistBodega.Style.Add("display", "none");
                        }
                    }
                }
            }
            else if (txtCorreo.Text == "")
            {
                MsjErrortextNombre.Style.Add("display", "none");
                MsjErrortextApellidos.Style.Add("display", "none");
                MsjErrortextCorreo.Style.Add("display", "block");

                if (ListRoles.SelectedValue == "0")
                {
                    MsjErrorListRol.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorListRol.Style.Add("display", "none");
                    if (ListRoles.SelectedItem.Text == "Inclusion Pedidos" || ListRoles.SelectedItem.Text == "Administrador Almacen")
                    {
                        if (ListBodegas.SelectedValue == "0")
                        {
                            MsjErrorlistBodega.Style.Add("display", "block");
                        }
                        else
                        {
                            MsjErrorlistBodega.Style.Add("display", "none");
                        }
                    }
                }
            }
            else if (ListRoles.SelectedValue == "0")
            {
                MsjErrortextNombre.Style.Add("display", "none");
                MsjErrortextApellidos.Style.Add("display", "none");
                MsjErrortextCorreo.Style.Add("display", "none");
                MsjErrorListRol.Style.Add("display", "block");
            }
            else
            {
                MsjErrorListRol.Style.Add("display", "none");
                if (ListRoles.SelectedItem.Text == "Usuario" || ListRoles.SelectedItem.Text == "Aprobador" || ListRoles.SelectedItem.Text == "Consulta" || ListRoles.SelectedItem.Text == "Revision y Aprobador Almacen")
                {
                   
                     Dictionary<string, int> itemsPrograma = new Dictionary<string, int>();
                     foreach (ListItem item in ListBoxProgramasAsignados.Items)
                     {
                         itemsPrograma.Add(item.Text, Int32.Parse(item.Value));
                     }

                     Dictionary<string, int> itemsBodega = new Dictionary<string, int>();
                     foreach (ListItem item in ListBoxBodegasAsignadas.Items)
                     {
                         itemsBodega.Add(item.Text, Int32.Parse(item.Value));
                     }

                     Dictionary<string, int> itemsSubBodega = new Dictionary<string, int>();
                     foreach (ListItem item in ListBoxSubBodegasAsignadas.Items)
                     {
                         itemsSubBodega.Add(item.Text, Int32.Parse(item.Value));
                     }

                     if (itemsPrograma.Count < 1)
                     {
                         MsjErrorListBoxPrograma.Style.Add("display", "block");
                         if (itemsBodega.Count < 1)
                         {
                             MsjErrorListBoxBodegas.Style.Add("display", "block");
                         }
                         else
                         {
                             MsjErrorListBoxBodegas.Style.Add("display", "none");
                         }

                         if (itemsSubBodega.Count < 1)
                         {
                             MsjErrorListBoxSubBodega.Style.Add("display", "block");
                         }
                         else
                         {
                             MsjErrorListBoxSubBodega.Style.Add("display", "none");
                         }
                     }
                     else if (itemsBodega.Count < 1)
                     {
                         MsjErrorListBoxPrograma.Style.Add("display", "none");
                         MsjErrorListBoxBodegas.Style.Add("display", "block");
                         if (itemsSubBodega.Count < 1)
                         {
                             MsjErrorListBoxSubBodega.Style.Add("display", "block");
                         }
                         else
                         {
                             MsjErrorListBoxSubBodega.Style.Add("display", "none");
                         }
                     }
                     else if (ListBoxProgramasAsignados.Items.IndexOf(ListBoxProgramasAsignados.Items.FindByText("Administración Penitenciaria")) != -1 && itemsSubBodega.Count < 1)
                     {
                         MsjErrorListBoxPrograma.Style.Add("display", "none");
                         MsjErrorListBoxBodegas.Style.Add("display", "none");
                         MsjErrorListBoxSubBodega.Style.Add("display", "block");
                     }
                     else {                   
                         
                         controladoraU.agregarUsuario(txtNombre.Text, TextApellidos.Text, txtCorreo.Text, Int32.Parse(ListRoles.SelectedValue));

                         //Se llena la tabla de UsuarioPrograma
                         foreach (KeyValuePair<string, int> entry in itemsPrograma)
                         {
                             controladoraU.agregarUsuarioPrograma(entry.Value);
                         }

                         //Se llena la tabla de UsuarioBodega
                         foreach (KeyValuePair<string, int> entry in itemsBodega)
                         {
                             controladoraU.agregarUsuarioBodega(entry.Value);
                         }

                         if (ListBoxProgramasAsignados.Items.IndexOf(ListBoxProgramasAsignados.Items.FindByText("Administración Penitenciaria")) != -1 )
                         { 
                             //Se llena la tabla de UsuarioSubBodega
                             foreach (KeyValuePair<string, int> entry in itemsSubBodega)
                             {
                                 controladoraU.agregarUsuarioSubBodega(entry.Value);
                             }
                         }
                         Response.Redirect("RolesPerfiles");
                     
                     }                  

                }
                else if (ListRoles.SelectedItem.Text == "Inclusion Pedidos" || ListRoles.SelectedItem.Text == "Administrador Almacen")
                {
                    if (ListBodegas.SelectedValue == "0")
                    {
                        MsjErrorlistBodega.Style.Add("display", "block");
                    }
                    else
                    {
                        controladoraU.agregarUsuario(txtNombre.Text, TextApellidos.Text, txtCorreo.Text, Int32.Parse(ListRoles.SelectedValue));
                        controladoraU.agregarUsuarioBodega(Int32.Parse(ListBodegas.SelectedValue));
                        Dictionary<string, int> SubBodegas = new Dictionary<string, int>();
                        SubBodegas = controladoraU.getSubBodegas("Administración Penitenciaria", ListBodegas.SelectedValue);

                        //Itera sobre el diccionario para relacionar al Usuario con cada SubBodega de la Bodega asignada
                        foreach (KeyValuePair<string, int> entry in SubBodegas)
                        {
                            controladoraU.agregarUsuarioSubBodega(entry.Value);
                        }
                        Response.Redirect("RolesPerfiles");
                    }                   
                }
            }      

        }

        protected void mostrarListBox(object sender, EventArgs e)
        {
            llenarListBoxSubBodegas();
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