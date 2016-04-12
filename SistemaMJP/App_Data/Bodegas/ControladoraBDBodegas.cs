using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDBodegas
    {
        SqlConnection con;
        public ControladoraBDBodegas()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        }

        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal Dictionary<string,int> getSubBodegas(string programa, string bodega)
        {
            Dictionary<string,int> subbodegas = new Dictionary<string,int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Nombre_SubBodegas_Por_Bodega_Programa";
                con.Open();
                cmd.Parameters.AddWithValue("@nombreB", bodega);
                cmd.Parameters.AddWithValue("@nombreP", programa);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    subbodegas.Add(reader.GetString(1),reader.GetInt32(0));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return subbodegas;
        }

        //Metodo que se encarga de devolver la lista de todas las Bodegas en el sistema
        public Dictionary<string, int> cargarBodegas()
        {
            Dictionary<string, int> bodega = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Bodega";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bodega.Add(reader.GetString(0), reader.GetInt32(1));
                }

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return bodega;
        }        

        //Metodo que se encarga de agregar las bodegas al sistema
        public void agregarBodega(string prefijo,string bodega)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Agregar_Bodega";
                cmd.Parameters.AddWithValue("@prefijo", prefijo);
                cmd.Parameters.AddWithValue("@nombre", bodega);
                cmd.ExecuteNonQuery();
                con.Close();                
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de agregar las subBodegas al sistema
        public void agregarSubBodega(string subBodega, int idPrograma)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Agregar_SubBodega";
                cmd.Parameters.AddWithValue("@nombre", subBodega);
                cmd.Parameters.AddWithValue("@idPrograma", idPrograma);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de agregar las bodegas y subBodegas relacionadas a la tabla de BodegaSubBodega
        public void agregarBodegaSubBodega(int bodega)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                int id=buscarMaximo();
                con.Open();
                cmd.CommandText = "P_Agregar_Bodega_SubBodega";
                cmd.Parameters.AddWithValue("@idBodega", bodega);
                cmd.Parameters.AddWithValue("@idSubBodega", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de buscar el id de la ultima bodega agregada al sistema
        public int buscarMaximo()
        {
            int id;
            try
            {                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_SubBodega_MaxID";
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

        //Metodo que se encarga de obtener el id de una bodega, dado su nombre
        internal int obtenerIDBodega(string bodega)
        {
            int id = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_id_bodega";
                cmd.Parameters.AddWithValue("@nombre", bodega);
                con.Open();
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

        //Metodo que se encarga de obtener el nombre de una subbodega,dado su id
        internal string getNombreSb(int id)
        {
            string nombre = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_nombre_SubBodega";
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                nombre = reader.GetString(0);

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return nombre;
        }

        //Metodo que se encarga de obtener el nombre de una subbodega, dado el id de una Bodega
        internal Dictionary<string, int> getSubBodegasPorBodega(int idBodega)
        {
            Dictionary<string, int> subbodegas = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Nombre_SubBodegas_Por_Bodega";
                con.Open();
                cmd.Parameters.AddWithValue("@idBodega", idBodega);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    subbodegas.Add(reader.GetString(0), reader.GetInt32(1));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return subbodegas;
        }
        
    }
}