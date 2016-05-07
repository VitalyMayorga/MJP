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
                    cmd.Parameters.AddWithValue("@idDevolucionBaja", idDevolucion);
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

        //Metodo que se encarga de actualizar el Rol del Usuario
        public void actualizarCantidadProducto(int idBodega, int idProducto, int idPrograma,  int idSubBodega, int cantidad, string tipo)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int id= buscarMaximo();
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

    }
}