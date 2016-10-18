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
    public class ControladoraBDDetalles_Producto_Requisicion
    {
        SqlConnection con;
        public ControladoraBDDetalles_Producto_Requisicion()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);

        }
        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal List<Item_Grid_Produtos_Requisicion> obtenerListaProductos(int id)
        {
            List<Item_Grid_Produtos_Requisicion> productos = new List<Item_Grid_Produtos_Requisicion>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Productos_Requisicion";
                con.Open();
                cmd.Parameters.AddWithValue("@idRequisicion", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(LoadItemGridRequisicion(reader));

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
        internal Item_Grid_Produtos_Requisicion LoadItemGridRequisicion(SqlDataReader reader)
        {            
            int producto = reader.GetInt32(0);
            int cantidad = reader.GetInt32(1);
            Item_Grid_Produtos_Requisicion items = new Item_Grid_Produtos_Requisicion(producto, cantidad);
            return items;
        }

        //Envia una factura y sus productos a Aprobación
        internal void cambiarEstadoRequisicion(int idRequisicion, int estado)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Cambiar_Estado_Requisicion";
                    cmd.Parameters.AddWithValue("@idRequisicion", idRequisicion);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
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

        //Metodo que se encarga de eliminar las lineas de la requisicion solicitada
        public void eliminarProductoRequisicion(int idRequisicion, int idProducto)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Producto_Requisicion";
                    cmd.Parameters.AddWithValue("@idRequisicion", idRequisicion);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
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

        //Metodo que se encarga de actualizar la cantidad de los productos asignados a una requisicion
        public void actualizarCantidadProductosRequisicion(int idBodega, int idProducto, int idPrograma, int idSubBodega, int cantidad)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Reducir_Cantidad_Producto_Requisicion";
                    cmd.Parameters.AddWithValue("@idBodega", idBodega);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                    cmd.Parameters.AddWithValue("@idSubBodega", idSubBodega);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
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

        //Metodo que se encarga de modificar la cantidad del producto seleccionado en la requisicion
        public void modificarCantidadLinea(int idRequisicion, int idProducto, int cantidad)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Modificar_Cantidad_Producto_Requisicion";
                    cmd.Parameters.AddWithValue("@idRequisicion", idRequisicion);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
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

        //llama a la base de datos  para obtener la cantidad de producto en salida
        public int obtenerCantidadProductoSalida(int bodega, int subbodega, string programa, string producto)
        {
            int cantidad = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Cantidad_Salida_Producto";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@subbodega", subbodega);
                cmd.Parameters.AddWithValue("@programa", programa);
                cmd.Parameters.AddWithValue("@producto", producto);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.HasRows) {
                        cantidad = reader.GetInt32(0);

                    }
                    
                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return cantidad;
        }


        //llama a la base de datos  para obtener la cantidad de producto en transaccion (aprobado en programa)
        public int obtenerCantidadTransaccion(int bodega, int subbodega, string programa, string producto)
        {
            int cantidad = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Cantidad_Producto_Transaccion";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@subbodega", subbodega);
                cmd.Parameters.AddWithValue("@programa", programa);
                cmd.Parameters.AddWithValue("@producto", producto);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        cantidad = reader.GetInt32(0);

                    }

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return cantidad;
        }

    }
}