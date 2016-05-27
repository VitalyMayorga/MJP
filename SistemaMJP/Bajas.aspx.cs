using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Diagnostics;

namespace SistemaMJP
{
    public partial class Bajas : System.Web.UI.Page
    {
        ControladoraDevolucionBajas controladora = new ControladoraDevolucionBajas();
        EmailManager email = new EmailManager();
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
                    cagarDatos();
                }
            }
        }

        protected void regresarDB(object sender, EventArgs e)
        {
            Response.Redirect("DevolucionBajas.aspx");
        }

        protected void cagarDatos()
        {
            Dictionary<string, int> nomBodega = new Dictionary<string, int>();
            Dictionary<string, int> nomPrograma = new Dictionary<string, int>();
            DropDownBodegas.Items.Clear();
            DropDownPrograma.Items.Clear();

            DropDownSubBodegas.Items.Insert(0, new ListItem("--No hay SubBodegas Disponibles--", "0"));
            DropDownBodegas.Items.Insert(0, new ListItem("--Selecione la Bodega--", "0"));
            DropDownPrograma.Items.Insert(0, new ListItem("--Selecione el Programa Presupuestario--", "0"));

            nomBodega = controladora.getBodegas();
            nomPrograma = controladora.getProgramas();

            //Itera sobre el diccionario para obtener la bodega y el respectivo id y guardarlo en un dropdownlist
            foreach (var item in nomBodega)
            {
                DropDownBodegas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
            }

            //Itera sobre el diccionario para obtener el programa y su respectivo id y guardarlo en un dropdownlist
            foreach (var item in nomPrograma)
            {
                DropDownPrograma.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
            }

        }

        protected void llenarSubBodegas(object sender, EventArgs e)
        {
            if (DropDownBodegas.SelectedValue != "0" && DropDownPrograma.SelectedValue != "0")
            {
                Dictionary<string, int> nomSubBodega = new Dictionary<string, int>();
                DropDownSubBodegas.Items.Clear();
                DropDownSubBodegas.Items.Insert(0, new ListItem("--Selecione la SubBodega--", "0"));
                nomSubBodega = controladora.getSubBodegas(Int32.Parse(DropDownPrograma.SelectedValue), Int32.Parse(DropDownBodegas.SelectedValue));

                //Itera sobre el diccionario para obtener el programa y su respectivo id y guardarlo en un dropdownlist
                foreach (var item in nomSubBodega)
                {
                    DropDownSubBodegas.Items.Add(new ListItem { Text = item.Key, Value = item.Value.ToString() });
                }
            }
        }

        //Metodo que se encarga de obtener todos los productos que empiezan cn lo digitado por el usuario
        //Funcionalidad principal es mostrar sugerencias al ingresar la descripcion de un producto
        [WebMethod]
        public static string[] getProductosBodegaProgramaSubBodega(string prefix, int programa, int bodega, int subBodega)
        {
            List<string> customers = ControladoraProductos.getProductosBodegaProgramaSubBodega(prefix, programa, bodega, subBodega);
            return customers.ToArray();
        }

        protected void bajar(object sender, EventArgs e)
        {
            string usuario = (string)Session["correoInstitucional"];
            List<string> correo;
            if (DropDownPrograma.SelectedValue == "0")
            {
                MsjErrorlistPrograma.Style.Add("display", "block");

                if (DropDownBodegas.SelectedValue == "0")
                {
                    MsjErrorlistBodega.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorlistBodega.Style.Add("display", "none");
                }

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

                if (txtJustificacion.Text == "")
                {
                    MsjErrortextJustificacion.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextJustificacion.Style.Add("display", "none");
                }

                /*if (ListRoles.SelectedValue == "0")
                {
                    MsjErrorListRol.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorListRol.Style.Add("display", "none");
                    if (ListRoles.SelectedItem.Text == "Inclusion Pedidos" || ListRoles.SelectedItem.Text == "Administrador Almacen")
                    {
                        if (ListBodegas.SelectedValue == "0")
                        {
                            MsjErrorlistBodega.Style.Add("display", "block");
                        }
                        else
                        {
                            MsjErrorlistBodega.Style.Add("display", "none");
                        }
                    }
                }*/

            }
            else if (DropDownBodegas.SelectedValue == "0")
            {
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrorlistBodega.Style.Add("display", "block");

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

                if (txtJustificacion.Text == "")
                {
                    MsjErrortextJustificacion.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextJustificacion.Style.Add("display", "none");
                }

                /*if (ListRoles.SelectedValue == "0")
                {
                    MsjErrorListRol.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorListRol.Style.Add("display", "none");
                    if (ListRoles.SelectedItem.Text == "Inclusion Pedidos" || ListRoles.SelectedItem.Text == "Administrador Almacen")
                    {
                        if (ListBodegas.SelectedValue == "0")
                        {
                            MsjErrorlistBodega.Style.Add("display", "block");
                        }
                        else
                        {
                            MsjErrorlistBodega.Style.Add("display", "none");
                        }
                    }
                }*/
            }
            else if (txtProducto.Text == "")
            {
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrorlistBodega.Style.Add("display", "none");
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
                /*if (ListRoles.SelectedValue == "0")
                {
                    MsjErrorListRol.Style.Add("display", "block");
                }
                else
                {
                    MsjErrorListRol.Style.Add("display", "none");
                    if (ListRoles.SelectedItem.Text == "Inclusion Pedidos" || ListRoles.SelectedItem.Text == "Administrador Almacen")
                    {
                        if (ListBodegas.SelectedValue == "0")
                        {
                            MsjErrorlistBodega.Style.Add("display", "block");
                        }
                        else
                        {
                            MsjErrorlistBodega.Style.Add("display", "none");
                        }
                    }
                }*/
            }
            else if (TextCantidad.Text == "")
            {
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrorlistBodega.Style.Add("display", "none");
                MsjErrortextProducto.Style.Add("display", "none");
                MsjErrortextCantidad.Style.Add("display", "block");

                if (txtJustificacion.Text == "")
                {
                    MsjErrortextJustificacion.Style.Add("display", "block");
                }
                else
                {
                    MsjErrortextJustificacion.Style.Add("display", "none");
                }

            }
            else if (txtJustificacion.Text == "")
            {
                MsjErrorlistPrograma.Style.Add("display", "none");
                MsjErrorlistBodega.Style.Add("display", "none");
                MsjErrortextProducto.Style.Add("display", "none");
                MsjErrortextCantidad.Style.Add("display", "none");
                MsjErrortextJustificacion.Style.Add("display", "block");
            }
            else
            {
                MsjErrortextJustificacion.Style.Add("display", "none");
                controladora.agregarDevolucionBaja("Baja", Int32.Parse(DropDownPrograma.SelectedValue), Int32.Parse(TextCantidad.Text), txtJustificacion.Text, Int32.Parse(DropDownBodegas.SelectedValue), controladora.getProductoConCantidadMin(txtProducto.Text), Int32.Parse(DropDownSubBodegas.SelectedValue), "Pendiente");
                correo = email.obtenerCorreosAprobador(Int32.Parse(DropDownPrograma.SelectedValue), Int32.Parse(DropDownBodegas.SelectedValue), Int32.Parse(DropDownSubBodegas.SelectedValue));
                email.MailSender("Solicitud de baja enviada a revisión por el usuario " + usuario + ".", "Notificación de solicitud de aprobación de productos de factura", correo);
                Response.Redirect("DevolucionBajas.aspx");
            }
        }

    }
}