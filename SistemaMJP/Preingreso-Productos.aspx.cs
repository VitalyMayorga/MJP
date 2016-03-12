using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Preingreso_Productos : System.Web.UI.Page
    {
        ControladoraProductos controladora = new ControladoraProductos();
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
            string subpartida = ListaSubPartidas.Items[ListaSubPartidas.SelectedIndex].Text;
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
            else if (subpartida.Equals("---Elija una Subpartida---")) {
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorSubPartida.Style.Add("display", "block");
                MsjErrorFactura.Style.Add("display", "none");
            }
            else if (ingresoF.Checked)
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
        //Si el programa Seleccionado posee subbodegas, entonces habilita la elección de la subbodega de dicho Programa
        protected void revisarPrograma(object sender, EventArgs e)
        {
            ListaSubBodegas.Items.Clear();
            List<string> subbodegas = controladora.getSubBodegas(ListaProgramas.Items[ListaProgramas.SelectedIndex].Text, ListaBodegas.Items[ListaBodegas.SelectedIndex].Text);
            if(subbodegas.Count>0){
                Subbodega.Style.Add("display", "block");
                ListaSubBodegas.Items.Add("---Elija un Departamento---");
                foreach (string nombreSb in subbodegas)
                {
                    ListaSubBodegas.Items.Add(nombreSb);
                }
                tieneSubBodega = true;
            }
            else{
                Subbodega.Style.Add("display", "none");
                tieneSubBodega = false;
                
            }
        }
        //Si está seleccionado ingresar Factura, habilita la inserción de una factura
        protected void rbIngresoF(object sender, EventArgs e)
        {
            if (ingresoF.Checked)
            {

                formFacturas.Style.Add("display", "block");
                
                
            }
        }
        //Si está seleccionado Mercaderia Inicial,esconde el div de la inserción de una factura
        protected void rbMercaderiaI(object sender, EventArgs e)
        {
            if (mercaderiaI.Checked)
            {
                formFacturas.Style.Add("display", "none");
            }
        }
        //Redirecciona al menu principal
        protected void cancelar(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal.aspx");
        }
        //Se llenan los datos iniciales, se cargan las bodegas programas, subpartidas
        protected void llenarDatos()
        {
            ArrayList bodegas = (ArrayList)Session["bodegas"];
            List<string> programas = controladora.getProgramas();
            ListaBodegas.Items.Add("---Elija una bodega---");
            ListaBodegas.Items.Add("Barrio Cuba");
            ListaProgramas.Items.Add("---Elija un Programa---");           
            foreach(string nombreP in programas){
                ListaProgramas.Items.Add(nombreP);
            }
            ListaSubPartidas.Items.Add("---Elija una Subpartida---");
            ingresoF.Checked = true;
        }
    }
}