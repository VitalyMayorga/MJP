using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Transactions;

namespace SistemaMJP
{
    public class ControladoraBDDevolucionBajas
    {
        SqlConnection con;
        public ControladoraBDDevolucionBajas()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        }             

        //Metodo que se encarga de agregar una Devolucion o Baja al sistema
        public void agregarDevolucionBaja(string tipo, int idPrograma, int cantidad, string justificacion, int idBodega, int idProducto, int idSubBodega, string estado)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Devolucion_Baja";
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@justificacion", justificacion);
                    cmd.Parameters.AddWithValue("@idBodega", idBodega);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@idSubBodega", idSubBodega);
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

        //Metodo que se encarga de actualizar el estado de una devolucion
        public void actualizarEstado(int idDevolucion, int aceptado)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Cambiar_Estado_Devolucion_Baja";
                    cmd.Parameters.AddWithValue("@idDevolucion", idDevolucion);
                    cmd.Parameters.AddWithValue("@aceptado", aceptado);
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

        //Metodo que se encarga de actualizar la cantidad de los productos asignados a una baja o Devolucion
        public void actualizarCantidadProducto(int idBodega, int idProducto, int idPrograma,  int idSubBodega, int cantidad, string tipo, int id)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Cambiar_Cantidad_Producto";
                    cmd.Parameters.AddWithValue("@idBodega", idBodega);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                    cmd.Parameters.AddWithValue("@idSubBodega", idSubBodega);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@idDevBaja", id);
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

        //Metodo que se encarga de buscar el id de la ultima Baja ingresada al sistema
        public int buscarMaximo()
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_DevBaja_MaxID";
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }

        //Metodo que se encarga de devolver la lista de Bajas Pendientes
        internal List<Item_Grid_Bajas> getListaBajasPendientes()
        {
            List<Item_Grid_Bajas> bajas = new List<Item_Grid_Bajas>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Bajas_Pendientes";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bajas.Add(LoadItemGridBajas(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return bajas;

        }

        //Metodo que se encarga de llenar los datos de la clase item grid bajas y devolver dicha clase encapsulada
        internal Item_Grid_Bajas LoadItemGridBajas(SqlDataReader reader)
        {
            int idDevolucionBaja = reader.GetInt32(0);
            int idProducto = reader.GetInt32(1);
            int cantidad = reader.GetInt32(2);
            int idPrograma = reader.GetInt32(3);
            int idBodega = reader.GetInt32(4);
            int idSubBodega = reader.GetInt32(5);
            String justificacion = reader.GetString(6);
            Item_Grid_Bajas items = new Item_Grid_Bajas(idDevolucionBaja, idProducto, cantidad, idPrograma, idBodega, idSubBodega, justificacion);
            return items;
        }

        //Metodo que se encarga de devolver la cantidad del producto introducido
        public int cantidadProducto(string nombreProducto)
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Cantidad_Producto";
                cmd.Parameters.AddWithValue("@nombreProducto", nombreProducto);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }


        //Metodo que se encarga de devolver la cantidad por empaque de un producto
        public int cantidadEmpaque(int producto)
        {
            int cantidad;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Obtener_CantidadPorEmpaque";
                cmd.Parameters.AddWithValue("@idProducto", producto);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                cantidad = reader.GetInt32(0);
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