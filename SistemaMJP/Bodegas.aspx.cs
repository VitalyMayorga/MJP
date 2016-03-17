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
        ControladoraBodegas bodega = new ControladoraBodegas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                int cont = 0;
                int cont2 = 1;
                List<string> nomBodega = new List<string>();
                Dictionary<string, int> nomPrograma = new Dictionary<string, int>();
                ListBodegas.Items.Clear();
                ListProgramas.Items.Clear();

                 ListBodegas.Items.Insert(0, new ListItem("--Selecione la Bodega--", "0"));
                 ListProgramas.Items.Insert(0, new ListItem("--Selecione el Programa Presupuestario--", "0"));

                nomBodega = bodega.getBodegas();
                nomPrograma = bodega.getProgramas();
                while (cont < nomBodega.Count())
                {
                    ListBodegas.Items.Add(new ListItem { Text = nomBodega[cont], Value = nomBodega[cont + 1] });
                    cont += 2;
                    cont2++;
                }
                //Itera sobre el diccionario para obtener el programa y su respectivo id y guardaro en un dropdownlist
                foreach (var item in nomPrograma){
                    ListProgramas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                    
                }

            
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
                     MsjErrortextSubBodega.Style.Add("display", "none");
                     RbBodegas.Checked = true;
                     txtBodega.Enabled = true;
                     txtPrefijo.Enabled = true;
                 }
                 else
                 {
                     bodega.AgregarBodega(txtPrefijo.Text,txtBodega.Text);                    
                     Response.Redirect("Administracion.aspx");
                 }
             }
             else { 
                 if (RbSubBodegas.Checked) {
                     if (txtSubBodega.Text == "")
                     {
                         MsjErrortextSubBodega.Style.Add("display", "block");
                         MsjErrortextBodega.Style.Add("display", "none");
                         if (ListProgramas.SelectedValue=="0")
                         {
                             MsjErrorListProgramas.Style.Add("display", "block");
                         }
                        if (ListBodegas.SelectedValue == "0")
                        {
                            MsjErrorListBodegas.Style.Add("display", "block");
                        }
                         RbSubBodegas.Checked = true;
                         txtSubBodega.Enabled = true;
                         ListBodegas.Enabled = true;
                         ListProgramas.Enabled = true;
                     }
                     else
                     {
                         bodega.AgregarSubBodega(txtSubBodega.Text, Int32.Parse(ListProgramas.SelectedValue));
                         bodega.AgregarBodegaSubBodega(Int32.Parse(ListBodegas.SelectedValue));
                         Response.Redirect("Administracion.aspx");
                     }
                 }
             }
         }

        protected void rbEnable(object sender, EventArgs e)
        {
            if (RbBodegas.Checked)
            {
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