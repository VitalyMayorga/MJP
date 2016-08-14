using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Transactions;
using System.Web.UI;
namespace SistemaMJP
{
    public class ControladoraBDActivos
    {
        SqlConnection con;
        public ControladoraBDActivos()
        { 
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }

        //Recibe la informacion del activo parcial para asignarlo al control de activos
        internal void agregarActivo(string numActivo,string funcionario,string cedula,int idProducto)
        {

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Activo";
                    cmd.Parameters.AddWithValue("@numActivo", numActivo);
                    cmd.Parameters.AddWithValue("@funcionario", funcionario);
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Today);
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

        //Metodo que se encarga de devolver la lista de activos en el sistema para una bodega dada
        internal List<Item_Grid_Activos> getListaActivos(string bodega)
        {
            List<Item_Grid_Activos> activos = new List<Item_Grid_Activos>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Activos_Por_Bodega";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    activos.Add(LoadItemGridActivos(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return activos;

        }

        //Metodo que se encarga de llenar los datos de la clase item grid activos y devolver dicha clase encapsulada
        private Item_Grid_Activos LoadItemGridActivos(SqlDataReader reader)
        {
            String documento = "";
            String numero = reader.GetString(0);
            try
            {

                documento = reader.GetString(1);
            }
            catch (Exception e) {
                documento = "No asignado";
            }
                
            DateTime fecha = reader.GetDateTime(2);            
            String funcionario = reader.GetString(3);
            String cedula = reader.GetString(4);
            String producto = reader.GetString(5);
            Item_Grid_Activos items = new Item_Grid_Activos(numero, documento, fecha, funcionario, cedula, producto);
            return items;
        }

        //Agrega un activo final a la base de datos
        internal int agregarActivoFinal(object[] datos)
        {
            int agregado = -1;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Activo";
                    cmd.Parameters.AddWithValue("@numActivo", datos[0]);
                    cmd.Parameters.AddWithValue("@funcionario", datos[2]);
                    cmd.Parameters.AddWithValue("@cedula", datos[3]);
                    cmd.Parameters.AddWithValue("@fecha", datos[1]);
                    cmd.Parameters.AddWithValue("@documento", datos[4]);
                    cmd.Parameters.AddWithValue("@producto", datos[5]);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ts.Complete();
                    agregado = 1;
                }
                catch (Exception)
                {
                    agregado = 0;
                }

            }
            return agregado;
        }

        //Metodo que llama ala controladora de base de datos para obtener los activos de la bodega
        internal Dictionary<string, int> getActivos(string bodega)
        {
            Dictionary<string, int> activos = new Dictionary<string,int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Activos_Por_Bodega";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    activos.Add(reader.GetString(0),reader.GetInt32(1));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return activos;

        }

        //Metodo que llama ala controladora de base de datos para obtener los datos del activo
        internal List<string> obtenerDatosActivo(string numActivo)
        {
            List<string> datos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Datos_Activo";
                con.Open();
                cmd.Parameters.AddWithValue("@numActivo", numActivo);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                datos.Add(reader.GetString(0));
                datos.Add(reader.GetString(1));
                datos.Add(reader.GetString(2));
                try
                {
                    datos.Add(reader.GetString(3));
                }
                catch (Exception e)
                {
                    datos.Add("");
                }
                datos.Add((reader.GetDateTime(4)).ToString("dd/MM/yyyy"));
                datos.Add(reader.GetString(5));
                                               
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return datos;
        }

        //Metodo que llama ala controladora de base de datos para modificar el activo
        internal void modificarActivo(object[] datos)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Modificar_Activo";
                    cmd.Parameters.AddWithValue("@numActivo", datos[0]);
                    cmd.Parameters.AddWithValue("@funcionario", datos[2]);
                    cmd.Parameters.AddWithValue("@cedula", datos[3]);
                    cmd.Parameters.AddWithValue("@fecha", datos[1]);
                    cmd.Parameters.AddWithValue("@documento", datos[4]);
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

        //Metodo que llama ala controladora de base de datos para eliminar el activo
        internal void eliminarActivo(string activo)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Activo";
                    cmd.Parameters.AddWithValue("@numActivo", activo);
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

        //Metodo que llama a la controladora de base de datos para obtener la cantidad de activos asignados de un producto asignados a una requisicion especifica
        internal int obtenerCantidadActivos(int idRequisicion, int idProducto)
        {
            int cantidad = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Cantidad_Activo";
                con.Open();
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.Parameters.AddWithValue("@idRequisicion", idRequisicion);
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

        //Metodo que llama a la controladora de base de datos para revisar si un producto es o no un activo
        internal bool esActivo(int idProducto)
        {
            bool activo = false;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Es_Activo";
                con.Open();
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                activo = reader.GetBoolean(0);
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return activo;
        }

    }
}