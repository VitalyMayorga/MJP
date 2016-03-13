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
            int cont = 0;
            int cont2 = 1;
            List<string> nomBodega = new List<string>();
           
            ListBodegas.Items.Insert(0, new ListItem("--Selecione la Bodega--", "0"));

            nomBodega = bodega.getBodegas();
            while (cont < nomBodega.Count())
            {
                ListBodegas.Items.Insert(cont2, new ListItem(nomBodega[cont], nomBodega[cont+1]));
                
                cont+=2;
                cont2++;
            }
            
        }

        protected void incluir(object sender, EventArgs e)
        {
             if (RbBodegas.Checked)
             {
                 bodega.AgregarBodega(txtBodega.Text);

             }
             else { 
                 if (RbSubBodegas.Checked) {
                     bodega.AgregarSubBodega(txtSubBodega.Text);
                     bodega.AgregarBodegaSubBodega(Int32.Parse(ListBodegas.SelectedValue));
                 }
             }
             Response.Redirect("Administracion.aspx");
         }    
        
}
}