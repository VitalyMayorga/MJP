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
        ServicioBodegas bodega = new ServicioBodegas();
        protected void Page_Load(object sender, EventArgs e)
        {
            int cont = 0;
            int cont2 = 1;
            List<string> nomBodega = new List<string>();
           
            ListBodegas.Items.Insert(0, new ListItem("--Selecione la Bodega--", "0"));

            nomBodega = bodega.CargarBodegas();
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
                 bodega.AgergarBodega(txtBodega.Text);

             }
             else { 
                 if (RbSubBodegas.Checked) {
                     bodega.AgergarSubBodega(txtSubBodega.Text);
                     bodega.AgergarBodegaSubBodega(Int32.Parse(ListBodegas.SelectedValue), bodega.);
                 }
             }
             Response.Redirect("Administracion.aspx");
         }    
        
}
}