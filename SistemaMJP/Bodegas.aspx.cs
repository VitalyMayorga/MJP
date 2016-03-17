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
        public static string programa;
        public static string subBodega;
        protected void Page_Load(object sender, EventArgs e)
        {
            int cont = 0;
            int cont2 = 1;
            List<string> nomBodega = new List<string>();
            List<string> nomPrograma = new List<string>();
            ListBodegas.Items.Clear();
            ListProgramas.Items.Clear();
           
           // ListBodegas.Items.Insert(0, new ListItem("--Selecione la Bodega--", "0"));
            //ListProgramas.Items.Insert(0, new ListItem("--Selecione el Programa Presupuestario--", "0"));

            nomBodega = bodega.getBodegas();
            nomPrograma = bodega.getProgramas();
            while (cont < nomBodega.Count())
            {
                ListBodegas.Items.Add(new ListItem { Text = nomBodega[cont], Value = nomBodega[cont+1] });
               // ListBodegas.Items.Insert(cont2, new ListItem(, ));
                
                cont+=2;
                cont2++;
            }
            cont = 0;
            cont2 = 1;
            while (cont < nomPrograma.Count())
            {
                ListProgramas.Items.Add(new ListItem { Text = nomPrograma[cont], Value = nomPrograma[cont + 1] });
                //ListProgramas.Items.Insert(cont2, new ListItem(nomPrograma[cont], nomPrograma[cont+1]));

                cont+=2;
                cont2++;
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
                         RbSubBodegas.Checked = true;
                         txtSubBodega.Enabled = true;
                         ListBodegas.Enabled = true;
                         ListProgramas.Enabled = true;
                     }
                     else
                     {
                         //string g = programa;
                         //string h = subBodega;
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

        protected void guardardatosPrograma(object sender, EventArgs e)
        {
            programa = ListProgramas.SelectedValue;
            /*RbSubBodegas.Checked = true;
            txtSubBodega.Enabled = true;
            ListBodegas.Enabled = true;
            ListProgramas.Enabled = true;*/
        }

        protected void guardardatosBodega(object sender, EventArgs e)
        {
            subBodega = ListBodegas.SelectedValue;
           /* RbSubBodegas.Checked = true;
            txtSubBodega.Enabled = true;
            ListBodegas.Enabled = true;
            ListProgramas.Enabled = true;*/
        }

    }
}