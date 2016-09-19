using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SistemaMJP
{
    public partial class RevisionRequisiciones : System.Web.UI.Page
    {
        public DataTable datosRequisicion;
        public DataTable datosRequisiciondespacho;
        private DataTable seguimiento;
        private ControladoraRequisicionAprobadores controladora = new ControladoraRequisicionAprobadores();
        ServicioLogin servicio = new ServicioLogin();
        private List<string> observaciones = new List<string>();//se guardaran las observaciones de cada requisicion
        private List<string> observacionesDespacho = new List<string>();//se guardaran las observaciones de cada requisicion   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar");
                }
                else if (!rol.Equals("Aprobador") && !rol.Equals("Revisión y Aprobador Almacen"))
                {
                    Response.Redirect("MenuPrincipal");
                }
                else
                {
                    if (rol.Equals("Aprobador"))
                    {
                        btnReqFinalizadas.Style.Add("display", "block");
                    }
                    llenarRequisicion();
                }
            }
            else {
                try
                {

                    observaciones = (List<string>)ViewState["observaciones"];
                    observacionesDespacho = (List<string>)ViewState["observacionesDespacho"];
                    seguimiento = (DataTable)ViewState["tabla2"];
                    datosRequisicion = (DataTable)ViewState["tablaDR"];
                    GridRequisicion.DataSource = datosRequisicion;
                    GridRequisicion.DataBind();
                    datosRequisiciondespacho = (DataTable)ViewState["tablaDD"];
                }
                catch (Exception ex) { 
                
                }
            }
        }

        //Regresa al menu principal
        protected void regresarMP(object sender, EventArgs e)
        {
            Response.Redirect("MenuPrincipal");
        }
        
        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridRequisicion.PageIndex = e.NewPageIndex;
            GridRequisicion.DataSource = datosRequisicion;
            GridRequisicion.DataBind();
        }

        /*
        *  Requiere: Controladores de eventos de la interfaz.
        *  Efectúa:  Cambia el contenido de la tabla de las requisiciones despachadas al índice seleccionado.
        *  Retrona:  N/A
        */
        protected void PageIndexChangingDespacho(Object sender, GridViewPageEventArgs e)
        {
            GridRequisicionDespacho.PageIndex = e.NewPageIndex;
            GridRequisicionDespacho.DataSource = datosRequisiciondespacho;
            GridRequisicionDespacho.DataBind();
        }

        protected void btnDetalles_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;            
            string buttonId = btn.ID;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);

            string numRequisicion = GridRequisicion.Rows[i + (this.GridRequisicion.PageIndex * 10)].Cells[0].Text;
            //string estado = GridRequisicion.Rows[i + (this.GridRequisicion.PageIndex * 10)].Cells[6].Text;

            Response.Redirect("DetallesRequisicionRevision.aspx?numR=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(numRequisicion, "MJP")) + "&btn=" + HttpUtility.UrlEncode(servicio.TamperProofStringEncode(buttonId, "MJP")));
            
        }

        //Muestra las observaciones de la requisicion
        protected void btnVer_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string buttonId = btn.ID;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int id_row = Convert.ToInt32(row.RowIndex);
           int pageIndex;
           string observacion;
           if (buttonId == "btnObservacionAlmacen"){
                pageIndex = GridRequisicion.PageIndex;
                //Se obtiene el id del producto            
                observacion = observaciones.ElementAt(id_row + (pageIndex * 10));
            }else{
                pageIndex = GridRequisicionDespacho.PageIndex;
                //Se obtiene el id del producto            
                observacion = observacionesDespacho.ElementAt(id_row + (pageIndex * 10));
            }
               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModalObservacion('" + observacion + "');", true);
        }

        //Llena la grid con las requisiciones correspondientes
        internal void llenarRequisicion()
        {
            List<Item_Grid_RequisicionAprobadores> data = new List<Item_Grid_RequisicionAprobadores>();
            List<Item_Grid_RequisicionAprobadores> data2 = new List<Item_Grid_RequisicionAprobadores>();
            List<int> datos_usuario= null;
            string rol = (string)Session["rol"];
            
                if(rol.Equals("Aprobador")){
                    datos_usuario = controladora.getProgramasPorIdUsuario((Int32)Session["userID"]);
                    foreach (int element in datos_usuario)
                     {                                       
                        data.AddRange(controladora.getListaRequisicionAprobador(element));
                     }
                  
                }else{
                    GridDespacho.Style.Add("display", "block");
                    datos_usuario = controladora.getBodegasPorIdUsuario((Int32)Session["userID"]);
                    foreach (int element in datos_usuario)
                     {   
                        data.AddRange(controladora.getListaRequisicionAlmacen(element));
                        data2.AddRange(controladora.getListaRequisicionDespachada(element));
                     }                    
                }               

                datosRequisicion = llenarTablas(data,true);
                ViewState["tablaDR"] = datosRequisicion;
                if (!rol.Equals("Aprobador"))
                {
                    datosRequisiciondespacho = llenarTablas(data2, false);
                    ViewState["tablaDD"] = datosRequisiciondespacho;
                }
                GridRequisicion.DataSource = datosRequisicion;
                GridRequisicion.DataBind();
                GridRequisicion.Dispose();
                if (datosRequisiciondespacho.Rows.Count > 0)
                {
                    GridRequisicionDespacho.DataSource = datosRequisiciondespacho;
                    GridRequisicionDespacho.DataBind();
                }
                UpdatePanel1.Update();
        }

        private DataTable llenarTablas(List<Item_Grid_RequisicionAprobadores> data, bool aprobador)        {

            CultureInfo crCulture = new CultureInfo("es-CR");
            DataTable tabla = crearTablaRequisicion();
            Object[] datos = new Object[8];
            int contador = 0;

            foreach (Item_Grid_RequisicionAprobadores fila in data)
            {
                datos[0] = fila.Requisicion;
                datos[1] = fila.Fecha;
                datos[2] = fila.Destino;
                datos[3] = controladora.getNombreUsuario(fila.Usuario);
                datos[4] = controladora.getNombrePrograma(fila.Programa);
                datos[5] = controladora.getNombreBodega(fila.Bodega);
                if (fila.SubBodega == 0)
                {//Si es 0 entonces no muestra subbodega
                    datos[6] = "--------";
                }
                else
                {
                    datos[6] = controladora.getNombreSb(fila.SubBodega);
                }
                datos[7] = controladora.getNombreUnidad(fila.Unidad);
                if (fila.Observacion == "" || fila.Observacion == null)
                {
                   if (aprobador){
                       observaciones.Add("Esta requisicion no posee ninguna observacion");
                       ViewState["observaciones"] = observaciones;
                   }else{
                       observacionesDespacho.Add("Esta requisicion no posee ninguna observacion");
                       ViewState["observacionesDespacho"] = observacionesDespacho;
                   }                    
                }
                else
                {
                    if (aprobador)
                    {
                        observaciones.Add(fila.Observacion);
                        ViewState["observaciones"] = observaciones;
                    }
                    else
                    {
                        observacionesDespacho.Add(fila.Observacion);
                        ViewState["observacionesDespacho"] = observacionesDespacho;
                    }                    
                }

                tabla.Rows.Add(datos);
                contador++;
            }

            return tabla;
        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaRequisicion()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Requisicion";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Fecha";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Destino";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Usuario";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Programa";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Bodega";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "SubBodega";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Unidad Solicitante";
            tabla.Columns.Add(columna);

            GridRequisicion.DataSource = tabla;
            GridRequisicion.DataBind();

            return tabla;
        }

        protected void gridRequisicion_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                foreach (DataControlField column in GridRequisicion.Columns)
                {
                    //Get the first Cell /Column
                    TableCell cell = row.Cells[0];
                    // Then Remove it after
                    row.Cells.Remove(cell);
                    //And Add it to the List Collections
                    columns.Add(cell);
                }
                // Add cells
                row.Cells.AddRange(columns.ToArray());
            }
        }

        //Obtiene las requisiciones que ya han sido finalizadas
        protected void verRequisiciones(object sender, EventArgs e)
        {
            List<int> datos_usuario = controladora.getProgramasPorIdUsuario((Int32)Session["userID"]);
            DataTable tabla = crearTablaTrack();
            //Aqui se debe de abrir el modal con el historico de la requisicion seleccionada
            List<Item_Grid_Requisiciones_Finalizadas> track = null;
            int i  = 0;
            foreach(int id in datos_usuario){
                if(i==0){
                    track = controladora.getRequisicionesFinalizadas(id);
                }
                else
                {
                    track.AddRange(controladora.getRequisicionesFinalizadas(id));
                }
                i++;
            }
            Object[] datos = new Object[3];
            foreach (Item_Grid_Requisiciones_Finalizadas dato in track)
            {
                datos[0] = dato.Requisicion;
                datos[1] = dato.Fecha.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                datos[2] = dato.Estado;
                tabla.Rows.Add(datos);
            }

            trackingGrid.DataSource = tabla;
            ViewState["tabla2"] = tabla;
            trackingGrid.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "openModal();", true);
            upModal.Update();
        }

        /**
      * Requiere: n/a
      * Efectua: Crea la DataTable para desplegar.
      * retorna:  un dato del tipo DataTable con la estructura para consultar.
      */
        protected DataTable crearTablaTrack()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;


            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Requisición";
            tabla.Columns.Add(columna);


            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Fecha";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estado";
            tabla.Columns.Add(columna);

            trackingGrid.DataSource = tabla;
            trackingGrid.DataBind();

            return tabla;
        }

    }
}