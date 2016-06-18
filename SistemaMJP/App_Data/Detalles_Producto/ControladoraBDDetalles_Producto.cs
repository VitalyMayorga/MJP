using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Transactions;

namespace SistemaMJP
{
    public class ControladoraBDDetalles_Producto
    {
        SqlConnection con;
        public ControladoraBDDetalles_Producto()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);

        }
        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal List<Item_Grid_Produtos_Factura> obtenerListaProductos(int id)
        {
            List<Item_Grid_Produtos_Factura> productos = new List<Item_Grid_Produtos_Factura>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Productos_Factura";
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(LoadItemGridFacturas(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return productos;

        }

        //Metodo que se encarga de llenar los datos de la clase item grid facturas y devolver dicha clase encapsulada
        internal Item_Grid_Produtos_Factura LoadItemGridFacturas(SqlDataReader reader)
        {
            String descripcion = reader.GetString(0);
            int cantidad = reader.GetInt32(1);
            decimal precioTotal = reader.GetDecimal(2);
            String estado = reader.GetString(3);
            int id = reader.GetInt32(4);
            Item_Grid_Produtos_Factura items = new Item_Grid_Produtos_Factura(id, descripcion, cantidad, precioTotal, estado);
            return items;
        }

        //elimina el producto de la factura dada
        internal void eliminarProducto(int idFactura, int idProducto)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Producto_Factura";
                    cmd.Parameters.AddWithValue("@factura", idFactura);
                    cmd.Parameters.AddWithValue("@producto", idProducto);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ts.Complete();

                }
                catch (Exception)
                {
                    throw;
                }

            }


        }
        //Envia una factura y sus productos a aprobacion
        internal void enviarAAprobacion(int idFactura)
        {
            string estado = "Pendiente de aprobación";
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Enviar_Factura_Aprobacion";
                    cmd.Parameters.AddWithValue("@factura", idFactura);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ts.Complete();

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }
        //Modifica el estado de un producto de una factura 
        internal void cambiarEstadoProducto(int idFactura, int idProducto, string estado)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Cambiar_Estado_Producto_Factura";
                    cmd.Parameters.AddWithValue("@factura", idFactura);
                    cmd.Parameters.AddWithValue("@producto", idProducto);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ts.Complete();

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }


        //Ingresa la fecha de Recpecion definitiva de un producto
        internal void agregarRecepcionDefinitiva(int idFactura,string fecha)
        {
            DateTime fechaRD = Convert.ToDateTime(fecha);
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_FechaRD_Factura";
                    cmd.Parameters.AddWithValue("@factura", idFactura);
                    cmd.Parameters.AddWithValue("@fecha", fechaRD);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ts.Complete();

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        //Rechaza la factura y la envía a edición
        internal void rechazarFactura(int idFactura)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Rechazar_Factura";
                    cmd.Parameters.AddWithValue("@factura", idFactura);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ts.Complete();

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
    }
}