using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDRolesPerfiles
    {
        SqlConnection con;
        public ControladoraBDRolesPerfiles()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        }

        //Metodo que se encarga de devolver la lista de usuarios que estan agregados al sistema
        internal List<Item_Grid_Usuarios> getListaUsuarios()
        {
            List<Item_Grid_Usuarios> usuarios = new List<Item_Grid_Usuarios>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Usuarios_Y_Rol";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(LoadItemGridUsuarios(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return usuarios;

        }

        internal Item_Grid_Usuarios LoadItemGridUsuarios(SqlDataReader reader)
        {
            String nombre = reader.GetString(0);
            String apellido = reader.GetString(1);
            String nomRol = reader.GetString(2);

            Item_Grid_Usuarios items = new Item_Grid_Usuarios(nombre,apellido, nomRol);
            return items;
        }

        //Metodo que se encarga de devolver la lista de todos los roles en el sistema
        public Dictionary<string, int> cargarRoles()
        {
            Dictionary<string, int> roles = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Roles";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add(reader.GetString(0), reader.GetInt32(1));
                }

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return roles;

        }

        //Metodo que se encarga de eliminar un Usuario del Sistema
        public void eliminarUsuario(string nombre, string apellidos)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Eliminar_Usuario";
                cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}