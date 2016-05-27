using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Bodegas : System.Web.UI.Page
    {
        Bitacora bitacora = new Bitacora();
        ControladoraBodegas bodega = new ControladoraBodegas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar.aspx");
                }
                else if (!rol.Equals("Administrador General"))
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("Administracion.aspx");
                }
                else
                {
                    cagarDatos();
                }
            }
                
        }

        protected void regresarMA(object sender, EventArgs e)
        {
            Response.Redirect("Administracion.aspx");
        }
       
        protected void cagarDatos()
        {
            Dictionary<string, int> nomBodega = new Dictionary<string, int>();
            Dictionary<string, int> nomPrograma = new Dictionary<string, int>();
            ListBodegas.Items.Clear();
            ListProgramas.Items.Clear();

            ListBodegas.Items.Insert(0, new ListItem("--Selecione la Bodega--", "0"));
            ListProgramas.Items.Insert(0, new ListItem("--Selecione el Programa Presupuestario--", "0"));

            nomBodega = bodega.getBodegas();
            nomPrograma = bodega.getProgramas();

            //Itera sobre el diccionario para obtener la bodega y el respectivo id y guardarlo en un dropdownlist
            foreach (var item in nomBodega)
            {
                ListBodegas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
            }

            //Itera sobre el diccionario para obtener el programa y su respectivo id y guardarlo en un dropdownlist
            foreach (var item in nomPrograma)
            {
                ListProgramas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
            }

            txtBodega.Enabled = false;
            txtPrefijo.Enabled = false;
            txtSubBodega.Enabled = false;
            ListBodegas.Enabled = false;
            ListProgramas.Enabled = false;
        }

        protected void incluir(object sender, EventArgs e)
        {
             if (RbBodegas.Checked)
             {
                 if (txtBodega.Text == "")
                 {
                     MsjErrortextBodega.Style.Add("display", "block");                    
                     RbBodegas.Checked = true;
                     txtBodega.Enabled = true;
                     txtPrefijo.Enabled = true;
                 }
                 else
                 {
                     bodega.agregarBodega(txtPrefijo.Text,txtBodega.Text);
                     string descripcionRA = "Bodega " + txtBodega.Text + ", prefijo " + txtPrefijo.Text + " agregada al sistema";
                     string usuario = (string)Session["correoInstitucional"];
                     bitacora.registrarActividad(usuario, descripcionRA);
                     Response.Redirect("Administracion.aspx");
                 }
             }
             else { 
                 if (RbSubBodegas.Checked)
                 {      
                     if (txtSubBodega.Text == "")
                     {
                         MsjErrortextSubBodega.Style.Add("display", "block");

                         if (ListProgramas.SelectedValue == "0")
                         {
                             MsjErrorListProgramas.Style.Add("display", "block");
                         }
                         else 
                         {
                             MsjErrorListProgramas.Style.Add("display", "none");
                         }

                         if (ListBodegas.SelectedValue == "0")
                         {
                             MsjErrorListBodegas.Style.Add("display", "block");
                         }
                         else
                         {
                             MsjErrorListBodegas.Style.Add("display", "none");
                         }                        
                        
                        RbSubBodegas.Checked = true;
                        txtSubBodega.Enabled = true;
                        ListBodegas.Enabled = true;
                        ListProgramas.Enabled = true;
                     }
                     else
                     {
                         if (ListProgramas.SelectedValue == "0")
                         {
                             if (ListBodegas.SelectedValue == "0")
                             {
                                 MsjErrorListBodegas.Style.Add("display", "block");
                             }
                             else
                             {
                                 MsjErrorListBodegas.Style.Add("display", "none");
                             }                             
                             MsjErrortextSubBodega.Style.Add("display", "none");
                             MsjErrorListProgramas.Style.Add("display", "block");
                             RbSubBodegas.Checked = true;
                             txtSubBodega.Enabled = true;
                             ListBodegas.Enabled = true;
                             ListProgramas.Enabled = true;
                         }
                         else
                         {
                             if (ListBodegas.SelectedValue == "0")
                             {                                 
                                 MsjErrorListBodegas.Style.Add("display", "block");
                                 MsjErrorListProgramas.Style.Add("display", "none");
                                 MsjErrortextSubBodega.Style.Add("display", "none");
                                 RbSubBodegas.Checked = true;
                                 txtSubBodega.Enabled = true;
                                 ListBodegas.Enabled = true;
                                 ListProgramas.Enabled = true;
                             }
                             else
                             {
                                 bodega.agregarSubBodega(txtSubBodega.Text, Int32.Parse(ListProgramas.SelectedValue));
                                 bodega.agregarBodegaSubBodega(Int32.Parse(ListBodegas.SelectedValue));
                                 string descripcionRA = "SubBodega " + txtSubBodega.Text + ", bodega: " + bodega.getNombrePrograma(Int32.Parse(ListBodegas.SelectedValue)) + ", programa: " + bodega.getNombrePrograma(Int32.Parse(ListProgramas.SelectedValue)) + " agregada al sistema";
                                 string usuario = (string)Session["correoInstitucional"];
                                 bitacora.registrarActividad(usuario, descripcionRA);
                                 Response.Redirect("Administracion.aspx");
                             }                             
                         }
                    }
                 }
             }
         }

        protected void rbEnable(object sender, EventArgs e)
        {
            if (RbBodegas.Checked)
            {
                MsjErrortextSubBodega.Style.Add("display", "none");
                MsjErrorListProgramas.Style.Add("display", "none");
                MsjErrorListBodegas.Style.Add("display", "none");
                txtBodega.Enabled = true;
                txtPrefijo.Enabled = true;
                txtSubBodega.Enabled = false;
                ListBodegas.Enabled = false;
                ListProgramas.Enabled = false;
            }
            else
            {
                if (RbSubBodegas.Checked)
                {
                    MsjErrortextBodega.Style.Add("display", "none");
                    txtBodega.Enabled=false;
                    txtPrefijo.Enabled = false;
                    txtSubBodega.Enabled = true;
                    ListBodegas.Enabled = true;
                    ListProgramas.Enabled = true;
                }
            }
        }

    }
}