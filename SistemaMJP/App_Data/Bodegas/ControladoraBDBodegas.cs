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
        internal List<string> getSubBodegas(string programa, string bodega)
        {
            List<string> subbodegas = new List<string>();
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
                    subbodegas.Add(reader.GetString(0));

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
        public List<string> CargarBodegas()
        {
            List<string> bodega = new List<string>();
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
                    bodega.Add(reader.GetString(0));
                    bodega.Add(reader.GetInt32(1).ToString());
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
        public void AgregarBodega(string prefijo,string bodega)
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
        public void AgregarSubBodega(string subBodega, int idPrograma)
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
        public void AgregarBodegaSubBodega(int bodega)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                int id=BuscarMaximo();
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
        public int BuscarMaximo()
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

    }
}