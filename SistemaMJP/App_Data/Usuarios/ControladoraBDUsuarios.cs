using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDUsuarios
    {
        SqlConnection con;
        public ControladoraBDUsuarios()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        }

        //Metodo que se encarga de agregar un Usuario al sistema
        public void agregarUsuario(string password,string nombre, string apellidos, string correo, int rol)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Agregar_Usuario";
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@nombreUsuario", nombre);
                cmd.Parameters.AddWithValue("@apellidos", apellidos);
                cmd.Parameters.AddWithValue("@correoInstitucional", correo);
                cmd.Parameters.AddWithValue("@idRol", rol);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de agregar los usuario y bodegas relacionadas a la tabla de BodegaUsuario
        public void agregarUsuarioBodega(int bodega)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                int id = buscarMaximo();
                con.Open();
                cmd.CommandText = "P_Agregar_Usuario_Bodega";
                cmd.Parameters.AddWithValue("@idUsuario", id);
                cmd.Parameters.AddWithValue("@idBodega", bodega);
                
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de agregar los usuarios y subBodegas relacionadas a la tabla de SubBodegaUsuario
        public void agregarUsuarioSubBodega(int subBodega)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                int id = buscarMaximo();
                con.Open();
                cmd.CommandText = "P_Agregar_Usuario_SubBodega";
                cmd.Parameters.AddWithValue("@idUsuario", id);
                cmd.Parameters.AddWithValue("@idSubBodega", subBodega);                
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de agregar los usuarios y programas relacionados a la tabla de UsuarioProgramaPresupuestario
        public void agregarUsuarioPrograma(int programa)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                int id = buscarMaximo();
                con.Open();
                cmd.CommandText = "P_Agregar_Usuario_Programa";
                cmd.Parameters.AddWithValue("@idUsuario", id);
                cmd.Parameters.AddWithValue("@idPrograma", programa);                
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de buscar el id del ultimo usuario agregado al sistema
        public int buscarMaximo()
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Usuario_MaxID";
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