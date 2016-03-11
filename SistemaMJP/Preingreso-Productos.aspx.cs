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
        public static string programa;
        public static string subBodega;
        public bool tieneSubBodega;
        public static string numFactura;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                tieneSubBodega = false;
                subBodega = "";
                llenarDatos();
            }
        }
        //Revisa si los campos necesarios fueron agregados, de ser así ingresa a la ventana de ingreso de productos
        //En otro caso, despliega los mensajes de error
        protected void aceptar(object sender, EventArgs e)
        {
            string bodega = ListaBodegas.Items[ListaBodegas.SelectedIndex].Text;
            programa = ListaProgramas.Items[ListaProgramas.SelectedIndex].Text;
            string subpartida = txtSubPartida.Text;
            subpartida = subpartida.Replace(" ", "");//Elimino espacios en blanco para saber si se digito subpartida o no
            if (bodega.Equals("---Elija una bodega---"))
            {
                MsjErrorBodega.Style.Add("display", "block");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");
                MsjErrorFactura.Style.Add("display", "none");
                
            }

            else if (programa.Equals("---Elija un Programa---"))
            {
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "block");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "none");
                MsjErrorFactura.Style.Add("display", "none");
                
            }
            else if (tieneSubBodega) { 
                subBodega = ListaSubBodegas.Items[ListaSubBodegas.SelectedIndex].Text;
                if (subBodega.Equals("---Elija un Departamento---")) {
                    MsjErrorBodega.Style.Add("display", "none");
                    MsjErrorPrograma.Style.Add("display", "none");
                    MsjErrorSubBodega.Style.Add("display", "block");
                    MsjErrorSubPartida.Style.Add("display", "none");
                    MsjErrorFactura.Style.Add("display", "none");
                
                }
            
            
            }
            else if (subpartida.Equals("")) {
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "block");
                MsjErrorFactura.Style.Add("display", "none");
            }
            else if (IngresoFactura.Checked)
            {
                numFactura = txtNumFactura.Text;
                numFactura = numFactura.Replace(" ", "");//Elimino espacios en blanco para saber si se digito numero factura o no
                if (numFactura.Equals(""))
                {
                    MsjErrorBodega.Style.Add("display", "none");
                    MsjErrorPrograma.Style.Add("display", "none");
                    MsjErrorSubBodega.Style.Add("display", "none");
                    MsjErrorSubPartida.Style.Add("display", "none");
                    MsjErrorFactura.Style.Add("display", "block");
                }
            }
            else
            {//Se envían los datos necesarios para empezar a ingresar productos


            }
        }

        protected void revisarPrograma(object sender, EventArgs e)
        { 
            if(ListaProgramas.Items[ListaProgramas.SelectedIndex].Text.Equals("783")){
                Subbodega.Style.Add("display", "block");
                tieneSubBodega = true;
            }
            else{
                Subbodega.Style.Add("display", "none");
                tieneSubBodega = false;
                
            }
        }
        //protected void ingresarFactura(object sender, EventArgs e)
        //{
        //    if (IngresoFactura.Checked)
        //    {
        //        txtNumFactura.Enabled = true ;
        //    }
        //}
        //protected void inventarioInicial(object sender, EventArgs e)
        //{
        //    if (InventarioInicial.Checked)
        //    {
        //        txtNumFactura.Enabled = false;
        //        txtNumFactura.Text = "";
        //    }
        //}
        protected void cancelar(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal.aspx");
        }

        protected void llenarDatos()
        {
            ListaBodegas.Items.Add("---Elija una bodega---");
            ListaProgramas.Items.Add("---Elija un Programa---");
            ListaSubBodegas.Items.Add("---Elija un Departamento---");
            IngresoFactura.Checked = true;
        }
    }
}