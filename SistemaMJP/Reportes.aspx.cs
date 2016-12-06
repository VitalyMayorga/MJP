using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaMJP
{
    public partial class Reportes : System.Web.UI.Page
    {        
        private ServicioLogin servicio = new ServicioLogin();
        private ControladoraProductos controladoraP = new ControladoraProductos();
        private ControladoraRequisicionesUsuario controladoraRU = new ControladoraRequisicionesUsuario();
        private ControladoraProgramasPresupuestarios controladoraPP = new ControladoraProgramasPresupuestarios();
        private ControladoraUsuarios controladoraU = new ControladoraUsuarios();
        string programa;
        string idprograma;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar.aspx");
                }
                else if (rol.Equals("Administrador General") || rol.Equals("Inclusión Pedidos") || rol.Equals("Usuario"))
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }
                else
                {
                    llenarProgramas();
                    llenarDestinos();
                    llenarSubPartidas();
                    mostrarListaReporte();
                }
            }
        }

        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("~/MenuPrincipal.aspx");
        }

        protected void mostrarListaReporte()
        {
            string rol = (string)Session["rol"];
            if (rol.Equals("Revisión y Aprobador Almacen"))
            {
                rAlgunos.Style.Add("display", "block");
            }
            else {
                rTodos.Style.Add("display", "block");
            }
        }
        
        protected void mostrarReporte(object sender, EventArgs e)
        {
            if (ListaReporte1.SelectedItem.Text=="Reporte de existencias"){
                rExistencias.Style.Add("display", "block");
                rGeneral.Style.Add("display", "none");
                rDestinoSubPartida1.Style.Add("display", "none");
                rTrazabilidad1.Style.Add("display", "none");
                rDestinoSubPartida2.Style.Add("display", "none");
                rTrazabilidad2.Style.Add("display", "none");
                rRequisicion.Style.Add("display", "none");
            }
            else if (ListaReporte1.SelectedItem.Text == "Reporte de articulos general")
            {
                rGeneral.Style.Add("display", "block");
                rExistencias.Style.Add("display", "none");
                rDestinoSubPartida1.Style.Add("display", "none");
                rTrazabilidad1.Style.Add("display", "none");
                rDestinoSubPartida2.Style.Add("display", "none");
                rTrazabilidad2.Style.Add("display", "none");
                rRequisicion.Style.Add("display", "none");
            }
            else if (ListaReporte1.SelectedItem.Text == "Reporte de articulos por destino y subPartida")
            {
                rDestinoSubPartida1.Style.Add("display", "block");
                rDestinoSubPartida2.Style.Add("display", "block");
                rTrazabilidad1.Style.Add("display", "none");
                rTrazabilidad2.Style.Add("display", "none");
                rExistencias.Style.Add("display", "none");
                rGeneral.Style.Add("display", "none");
                
                rRequisicion.Style.Add("display", "none");
            }
            else if (ListaReporte1.SelectedItem.Text == "Reporte de trazabilidad")
            {
                rTrazabilidad1.Style.Add("display", "block");
                rTrazabilidad2.Style.Add("display", "block");
                rExistencias.Style.Add("display", "none");
                rGeneral.Style.Add("display", "none");
                rDestinoSubPartida1.Style.Add("display", "none");
                rDestinoSubPartida2.Style.Add("display", "none");
                rRequisicion.Style.Add("display", "none");
            }
            else if (ListaReporte1.SelectedItem.Text == "Reporte de requisiciones")
            {
                rRequisicion.Style.Add("display", "block");
                rExistencias.Style.Add("display", "none");
                rGeneral.Style.Add("display", "none");
                rDestinoSubPartida1.Style.Add("display", "none");
                rTrazabilidad1.Style.Add("display", "none");
                rDestinoSubPartida2.Style.Add("display", "none");
                rTrazabilidad2.Style.Add("display", "none");
            }

         }

            protected void mostrarReporte2(object sender, EventArgs e)
            {
                if (ListaReporte2.SelectedItem.Text == "Reporte de requisiciones")
                {               
                    rRequisicion.Style.Add("display", "block");
                }
                else
                {
                    rRequisicion.Style.Add("display", "none");
                } 
            }

        protected void llenarProgramas()
        {
            string usuario = (string)Session["correoInstitucional"];
            string rol = (string)Session["rol"];
            List<int> nomPrograma = new List<int>();
            Dictionary<string, int> program = new Dictionary<string, int>();
            int idUsuario = controladoraU.ObtenerIdUsuarioPorCorreo(usuario);
            
            if (rol.Equals("Administrador Almacen"))
            {
                program = controladoraPP.getProgramas();
;
                foreach (var items in program)
                {
                    ListProgramas.Items.Add(new ListItem { Text = items.Key, Value = items.Value.ToString() });
                }              
            }
            else
            {               
                nomPrograma = controladoraPP.getProgramasPorIdUsuario(idUsuario);

                //Itera sobre el diccionario para obtener los programas presupuestarios y guardarlos en el listBox
                foreach (var item in nomPrograma)
                {
                    ListProgramas.Items.Add(new ListItem { Text = controladoraPP.getNombrePrograma(item), Value = item.ToString() });
                }
            }          
            
        }

        protected void llenarDestinos()
        {
            List<string> destinos = new List<string>();
            destinos = controladoraRU.getUnidades();

            foreach (var items in destinos)
            {
                ListDestino1.Items.Add(new ListItem { Text = items, Value = "0" });
                ListDestino2.Items.Add(new ListItem { Text = items, Value = "0" });
            }   

        }

        protected void llenarSubPartidas()
        {
            Dictionary<string, int> subPrograma = new Dictionary<string, int>();
            subPrograma = controladoraP.getSubPartidas();

            foreach (var items in subPrograma)
            {
                ListSubPartida1.Items.Add(new ListItem { Text = items.Key, Value = items.Value.ToString() });
                ListSubPartida2.Items.Add(new ListItem { Text = items.Key, Value = items.Value.ToString() });
            }   
        }

        protected void existencia(object sender, EventArgs e)
        {
            programa = ListProgramas.SelectedItem.Text;
            idprograma = ListProgramas.SelectedItem.Value;
            Response.Redirect("ReporteExistencia.aspx?idPrograma=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idprograma, "MJP")) + "&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(programa, "MJP")));        
        }

        protected void general(object sender, EventArgs e)
        {
            programa = ListProgramas.SelectedItem.Text;
            idprograma = ListProgramas.SelectedItem.Value;
            Response.Redirect("ReporteArticulosGeneral.aspx?idPrograma=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idprograma, "MJP")) + "&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(programa, "MJP")));      
        }

        protected void destinoSubPartida(object sender, EventArgs e)
        {
            programa = ListProgramas.SelectedItem.Text;
            idprograma = ListProgramas.SelectedItem.Value;
            string destino = ListDestino1.SelectedItem.Text;
            string subPartida = ListSubPartida1.SelectedItem.Text;
            Response.Redirect("ReporteArticulosDestinoSubPartida.aspx?idPrograma=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idprograma, "MJP")) + "&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(programa, "MJP")) + "&destino=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(destino, "MJP")) + "&subPartida=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(subPartida, "MJP")));      
        }

        protected void trazabilidad(object sender, EventArgs e)
        {
            programa = ListProgramas.SelectedItem.Text;
            idprograma = ListProgramas.SelectedItem.Value;
            string subPartida = ListSubPartida2.SelectedItem.Text;
            string periodo = ListPeriodo.SelectedItem.Text;
            int año = Int32.Parse(DateTime.Now.ToString("yyyy"));
            int dia = Int32.Parse(DateTime.Now.ToString("dd"));
            int mes = Int32.Parse(DateTime.Now.ToString("MM"));
            DateTime fechaInicial  = new DateTime(año - Int32.Parse(periodo), mes, dia);
            string fechaI = fechaInicial.ToString("yyyy/MM/dd");
            Response.Redirect("ReporteTrazabilidad.aspx?idPrograma=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idprograma, "MJP")) + "&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(programa, "MJP")) + "&subPartida=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(subPartida, "MJP")) + "&periodo=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(periodo, "MJP")) + "&fechaI=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(fechaI, "MJP")) + "&fechaF=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(DateTime.Now.ToString("yyyy/MM/dd"), "MJP")));      
        }

        protected void requisiciones(object sender, EventArgs e)
        {
            programa = ListProgramas.SelectedItem.Text;
            idprograma = ListProgramas.SelectedItem.Value;
            string destino = ListDestino2.SelectedItem.Text;
            Response.Redirect("ReporteArticulosDestino.aspx?idPrograma=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(idprograma, "MJP")) + "&programa=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(programa, "MJP")) + "&destino=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(destino, "MJP")));      
        }

    }
}