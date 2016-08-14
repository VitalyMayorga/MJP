using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Pre_ingresoRequisicion : System.Web.UI.Page
    {
        private ControladoraRequisicionesUsuario controladora = new ControladoraRequisicionesUsuario();
        private ServicioLogin servicio = new ServicioLogin();
        private EmailManager email = new EmailManager();
        Bitacora bitacora = new Bitacora();
        private string programa;
        private string valsubBodega;
        private bool tieneSubBodega;
        private string observacion;
        private string destino;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {//Se revisa primero si tiene los permisos para ingresar al sitio
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Usuario"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {
                    tieneSubBodega = false;
                    valsubBodega = "";
                    llenarDatos();
                }
            }
            else
            {
                try
                {
                    programa = (string)ViewState["programa"];
                    valsubBodega = (string)ViewState["subBodega"];
                    tieneSubBodega = (bool)ViewState["tieneSubBodega"];
                    observacion = (string)ViewState["observacion"];
                    destino = (string)ViewState["destino"];
                }
                catch (Exception)
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }

            }
        }

        //Revisa si los campos necesarios fueron agregados, de ser así ingresa a la ventana de ingreso de requisicion
        //En otro caso, despliega los mensajes de error
        protected void aceptar(object sender, EventArgs e)
        {
            string descripcion = "";
            string usuario = (string)Session["correoInstitucional"];
            string bodega = ListaBodegas.Items[ListaBodegas.SelectedIndex].Text;
            string unidad = ListaUnidadSol.Items[ListaUnidadSol.SelectedIndex].Text;
            string destino = txtDestino.Text;
            programa = ListaProgramas.Items[ListaProgramas.SelectedIndex].Text;
            ViewState["programa"] = programa;
            if (tieneSubBodega)
            {
                valsubBodega = ListaSubBodegas.Items[ListaSubBodegas.SelectedIndex].Text;
                ViewState["subBodega"] = valsubBodega;
            }                        
            if (bodega.Equals("---Elija una bodega---"))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "block");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorDestino.Style.Add("display", "none");
                MsjErrorUnidad.Style.Add("display", "none");

            }

            else if (programa.Equals("---Elija un Programa---"))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "block");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorDestino.Style.Add("display", "none");
                MsjErrorUnidad.Style.Add("display", "none");

            }
            else if (tieneSubBodega && (valsubBodega.Equals("---Elija una SubBodega---")))
            {
                //Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "block");
                MsjErrorDestino.Style.Add("display", "none");
                MsjErrorUnidad.Style.Add("display", "none");

            }
            else if (unidad.Equals("---Elija una unidad solicitante---"))
            {//Ocultar y mostrar mensajes de Error
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorDestino.Style.Add("display", "none");
                MsjErrorUnidad.Style.Add("display", "block");

            }
            else if (String.IsNullOrEmpty("destino"))
            {
                MsjErrorDestino.Style.Add("display", "block");
                MsjErrorBodega.Style.Add("display", "none");
                MsjErrorPrograma.Style.Add("display", "none");
                MsjErrorSubBodega.Style.Add("display", "none");
                MsjErrorDestino.Style.Add("display", "none");
                MsjErrorUnidad.Style.Add("display", "none");
            }
            else
            {//Se envían los datos necesarios para empezar a ingresar productos
                int idBodega = controladora.obtenerIDBodega(bodega);

                int idSubBodega = 0;//Por default, subbodega 0 = No hay subbodega asignada
                if (tieneSubBodega)
                {
                    idSubBodega = Convert.ToInt32(ListaSubBodegas.SelectedValue);

                }
                //Como el numero de requisicion es auto generado, en la controladora se generara y se devolvera, para luego pasarlo a la siguienta pantalla
                int userID = (int)Session["userID"];
                string numRequiscion = controladora.agregarRequisicion(userID,idBodega,programa, idSubBodega,unidad,destino, observacion);
                descripcion = "Se crea requisicion " + numRequiscion;
                bitacora.registrarActividad(usuario, descripcion);

                Response.Redirect("Ingreso_Requisicion.aspx?bodega=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idBodega.ToString(), "MJP")) +"&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(programa, "MJP")) + "&subbodega=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idSubBodega.ToString(), "MJP"))
                    + "&numReq=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequiscion, "MJP")));


            }
        }
        //Si el programa Seleccionado posee subbodegas, entonces habilita la elección de la subbodega de dicho Programa
        protected void revisarPrograma(object sender, EventArgs e)
        {
            ListaSubBodegas.Items.Clear();
            Dictionary<string, int> subbodegas = controladora.getSubBodegas(ListaProgramas.Items[ListaProgramas.SelectedIndex].Text, ListaBodegas.Items[ListaBodegas.SelectedIndex].Text);
            if (subbodegas.Count > 0)
            {
                Subbodega.Style.Add("display", "block");
                ListaSubBodegas.Items.Add("---Elija una SubBodega---");
                foreach (var nombreSb in subbodegas)
                {
                    ListaSubBodegas.Items.Add(new ListItem { Text = nombreSb.Key, Value = nombreSb.Value.ToString() });
                }
                tieneSubBodega = true;
            }
            else
            {
                Subbodega.Style.Add("display", "none");
                tieneSubBodega = false;

            }
            if (ListaProgramas.SelectedIndex != 0)
            {
                MsjErrorPrograma.Style.Add("display", "none");
            }
            ViewState["tieneSubBodega"] = tieneSubBodega;
        }

        //Si se selecciona la subbodega el msj de error se esconde
        protected void revisarSubB(object sender, EventArgs e)
        {
            if (ListaSubBodegas.SelectedIndex != 0)
            {
                MsjErrorSubBodega.Style.Add("display", "none");
            }
        }
        //Si se selecciona la Bodega el msj de error se esconde
        protected void revisarBodega(object sender, EventArgs e)
        {
            if (ListaBodegas.SelectedIndex != 0)
            {
                MsjErrorBodega.Style.Add("display", "none");
            }
        }

        //Si se selecciona la Unidad el msj de error se esconde
        protected void revisarUnidad(object sender, EventArgs e)
        {
            if (ListaUnidadSol.SelectedIndex != 0)
            {
                MsjErrorUnidad.Style.Add("display", "none");
            }
        }    
        //Redirecciona al menu principal
        protected void cancelar(object sender, EventArgs e)
        {
            Response.Redirect("Requisiciones.aspx");
        }
        //Se llenan los datos iniciales, se cargan las bodegas programas, subpartidas
        protected void llenarDatos()
        {
            List<string> bodegas = (List<string>)Session["bodegas"];
            List<string>programas = (List<string>)Session["programas"];
            List<string> unidades = controladora.getUnidades();
            ListaBodegas.Items.Add("---Elija una bodega---");
            foreach (string bodega in bodegas)
            {
                ListaBodegas.Items.Add(bodega);
            }
            ListaProgramas.Items.Add("---Elija un Programa---");
            foreach (var nombreP in programas)
            {
                ListaProgramas.Items.Add(nombreP);
            }
            ListaUnidadSol.Items.Add("---Elija una unidad solicitante---");
            foreach (string unidad in unidades)
            {
                ListaUnidadSol.Items.Add(unidad);
            }
           
        }
       
    }
}