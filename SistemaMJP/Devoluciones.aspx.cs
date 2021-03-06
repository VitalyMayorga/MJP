﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Diagnostics;
using System.Windows;

namespace SistemaMJP
{
    public partial class Devoluciones : System.Web.UI.Page
    {
        Bitacora bitacora = new Bitacora();
        ControladoraDevolucionBajas controladora = new ControladoraDevolucionBajas();
        private static int bodegaId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = (string)Session["rol"];
                if (Session["correoInstitucional"] == null)
                {
                    Response.Redirect("Ingresar.aspx");
                }
                else if (!rol.Equals("Administrador Almacen"))
                {
                    Response.Redirect("DevolucionBajas.aspx");
                }
                else if (Request.UrlReferrer == null)
                {
                    Response.Redirect("DevolucionBajas.aspx");
                }
                else
                {
                    cargarDatos();
                }
            }
            List<int> bodegas = new List<int>();
            bodegas = controladora.getBodegasPorIdUsuario((Int32)Session["userID"]);
            bodegaId = bodegas.ElementAt(0);
        }

        protected void regresarDB(object sender, EventArgs e)
        {
            Response.Redirect("DevolucionBajas.aspx");
        }

        protected void cargarDatos()
        {           
            Dictionary<string, int> nomPrograma = new Dictionary<string, int>();           
            DropDownPrograma.Items.Clear();

            DropDownSubBodegas.Items.Insert(0, new ListItem("--No hay SubBodegas Disponibles--", "0"));
            DropDownEmpaques.Items.Insert(0, new ListItem("--Selecione la cantidad por empaque--", "0"));
            DropDownPrograma.Items.Insert(0, new ListItem("--Selecione el Programa Presupuestario--", "0"));
           
            nomPrograma = controladora.getProgramas();     

            //Itera sobre el diccionario para obtener el programa y su respectivo id y guardarlo en un dropdownlist
            foreach (var item in nomPrograma)
            {
                DropDownPrograma.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
            }

        }

        protected void llenarCantidades(object sender, EventArgs e)
        {
            List<int> numEmpaque = new List<int>();
            DropDownEmpaques.Items.Clear();
            DropDownEmpaques.Items.Insert(0, new ListItem("--Selecione la cantidad por empaque--", "0"));

            numEmpaque = controladora.getEmpaques(txtProducto.Text);
            //Itera sobre el diccionario para obtener el programa y su respectivo id y guardarlo en un dropdownlist
            foreach (int item in numEmpaque)
            {
                DropDownEmpaques.Items.Add(new ListItem { Text = item.ToString(), Value = item.ToString() });
            }
        }
        
        protected void llenarSubBodegas(object sender, EventArgs e)
        {           
            if (DropDownPrograma.SelectedValue != "0")
            {
                Dictionary<string, int> nomSubBodega = new Dictionary<string, int>();
                DropDownSubBodegas.Items.Clear();
                DropDownSubBodegas.Items.Insert(0, new ListItem("--Selecione la SubBodega--", "0"));
                nomSubBodega = controladora.getSubBodegas(Int32.Parse(DropDownPrograma.SelectedValue), bodegaId);

                //Itera sobre el diccionario para obtener la subbodega y su respectivo id y guardarlo en un dropdownlist
                foreach (var item in nomSubBodega)
                {
                    DropDownSubBodegas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }
                if (DropDownSubBodegas.Items.Count < 2)
                {
                    listSubBodegas.Style.Add("display", "none");
                }
                else {
                    listSubBodegas.Style.Add("display", "block");
                }
                
            }
        }

        //Metodo que se encarga de obtener todos los productos que empiezan cn lo digitado por el usuario
        //Funcionalidad principal es mostrar sugerencias al ingresar la descripcion de un producto
        [WebMethod]
        public static string[] getProductosBodegaProgramaSubBodega(string prefix,int programa, int subBodega)
        {
            List<string> customers = ControladoraProductos.getProductosBodegaProgramaSubBodega(prefix, programa, bodegaId, subBodega);
            return customers.ToArray();
        }

        protected void devolver(object sender, EventArgs e)
        {
            string descripcionRA = "";
            if (DropDownPrograma.SelectedValue == "0")
            {
                MsjErrorlistPrograma.Style.Add("display", "block");
                
                if (txtProducto.Text == "")
                {
                    MsjErrortextProducto.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextProducto.Style.Add("display", "none");
                }

                if (TextCantidad.Text == "")
                {
                    MsjErrortextCantidad.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextCantidad.Style.Add("display", "none");
                }

                if (DropDownEmpaques.SelectedValue == "0")
                {
                    MsjErrorlistEmpaques.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorlistEmpaques.Style.Add("display", "none");
                }

                if (txtJustificacion.Text == "")
                {
                    MsjErrortextJustificacion.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextJustificacion.Style.Add("display", "none");
                }
                
            }
            else if (DropDownSubBodegas.Items.Count > 1 && DropDownSubBodegas.SelectedValue.Equals("0")) {
                MsjErrorlistSubBodega.Style.Add("display", "block");
            }
            else if (txtProducto.Text == "")
            {
                MsjErrorlistSubBodega.Style.Add("display", "none");
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrortextProducto.Style.Add("display", "block");

                if (TextCantidad.Text == "")
                {
                    MsjErrortextCantidad.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextCantidad.Style.Add("display", "none");
                }

                if (txtJustificacion.Text == "")
                {
                    MsjErrortextJustificacion.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextJustificacion.Style.Add("display", "none");
                }

            }
            else if (TextCantidad.Text == "")
            {
                MsjErrorlistSubBodega.Style.Add("display", "none");
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrortextProducto.Style.Add("display", "none");
                MsjErrortextCantidad.Style.Add("display", "block");

                if (txtJustificacion.Text == "")
                {
                    MsjErrortextCantidad.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextCantidad.Style.Add("display", "none");
                }

            }
            else if (DropDownEmpaques.SelectedValue == "0")
            {
                MsjErrorlistSubBodega.Style.Add("display", "none");
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrortextCantidad.Style.Add("display", "none");
                MsjErrortextProducto.Style.Add("display", "none");
                MsjErrorlistEmpaques.Style.Add("display", "block");

                if (txtJustificacion.Text == "")
                {
                    MsjErrorlistEmpaques.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorlistEmpaques.Style.Add("display", "none");
                }

            }
            else if (txtJustificacion.Text == "")
            {
                MsjErrorlistSubBodega.Style.Add("display", "none");
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrorlistEmpaques.Style.Add("display", "none");
                MsjErrortextProducto.Style.Add("display", "none");
                MsjErrortextCantidad.Style.Add("display", "none");
                MsjErrortextJustificacion.Style.Add("display", "block");
            }
            else
            {
                MsjErrortextJustificacion.Style.Add("display", "none");
                controladora.agregarDevolucionBaja("Devolución", Int32.Parse(DropDownPrograma.SelectedValue), Int32.Parse(TextCantidad.Text), txtJustificacion.Text, bodegaId, controladora.getProductoCantidadEmpaque(txtProducto.Text, Int32.Parse(DropDownEmpaques.SelectedValue)), Int32.Parse(DropDownSubBodegas.SelectedValue), "Aceptado");
                controladora.actualizarCantidadProducto(bodegaId, controladora.getProductoCantidadEmpaque(txtProducto.Text, Int32.Parse(DropDownEmpaques.SelectedValue)), Int32.Parse(DropDownPrograma.SelectedValue), Int32.Parse(DropDownSubBodegas.SelectedValue), Int32.Parse(TextCantidad.Text), "Devolución", controladora.buscarIdMaxDevolucion());
                if (Int32.Parse(DropDownSubBodegas.SelectedValue) == 0)
                {
                    descripcionRA = "Devolución de " + TextCantidad.Text + " " + txtProducto.Text + " en la bodega: " + controladora.getNombreBodega(bodegaId) + ", subBodega: -------- al programa presupuestario: " + controladora.getNombrePrograma(Int32.Parse(DropDownPrograma.SelectedValue));
                }
                else
                {
                    descripcionRA = "Devolución de " + TextCantidad.Text + " " + txtProducto.Text + " en la bodega: " + controladora.getNombreBodega(bodegaId) + ", subBodega: " + controladora.getNombreSb(Int32.Parse(DropDownSubBodegas.SelectedValue)) + "al programa presupuestario: " + controladora.getNombrePrograma(Int32.Parse(DropDownPrograma.SelectedValue));
                }
                string usuario = (string)Session["correoInstitucional"];
                bitacora.registrarActividad(usuario, descripcionRA);
                Response.Redirect("DevolucionBajas.aspx");
            }
        }

    }
}