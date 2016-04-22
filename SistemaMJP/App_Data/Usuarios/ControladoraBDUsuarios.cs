using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Transactions;

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
            using (TransactionScope ts = new TransactionScope())
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
                    ts.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //Metodo que se encarga de agregar los usuario y bodegas relacionadas a la tabla de BodegaUsuario
        public void agregarUsuarioBodega(int bodega)
        {
            using (TransactionScope ts = new TransactionScope())
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
                    ts.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //Metodo que se encarga de agregar los usuarios y subBodegas relacionadas a la tabla de SubBodegaUsuario
        public void agregarUsuarioSubBodega(int subBodega)
        {
            using (TransactionScope ts = new TransactionScope())
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
                    ts.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //Metodo que se encarga de agregar los usuarios y programas relacionados a la tabla de UsuarioProgramaPresupuestario
        public void agregarUsuarioPrograma(int programa)
        {
            using (TransactionScope ts = new TransactionScope())
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
                    ts.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
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

        //Metodo que se encarga de obtener el id del rol
        public int ObtenerIdRol(string nombre)
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Obtener_Id_Rol";
                cmd.Parameters.AddWithValue("@nomRol", nombre);
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

        //Metodo que se encarga de devolver la lista de Bodegas relacionadas con cierto Usuario
        public Dictionary<string, int> llenarBodegasAsignadas(string nombre, string apellidos)
        {
            Dictionary<string, int> bodegas = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Bodega_Segun_Id";
                con.Open();
                cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bodegas.Add(reader.GetString(0), reader.GetInt32(1));
                }

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return bodegas;
        }

        //Metodo que se encarga de devolver la lista de SubBodegas relacionadas con cierto Usuario
        public Dictionary<string, int> llenarSubBodegasAsignadas(string nombre, string apellidos)
        {
            Dictionary<string, int> subBodegas = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_SubBodega_Segun_Id";
                con.Open();
                cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subBodegas.Add(reader.GetString(0), reader.GetInt32(1));
                }

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return subBodegas;
        }

        //Metodo que se encarga de devolver la lista de Programas relacionados con cierto Usuario
        public Dictionary<string, int> llenarProgramasAsignados(string nombre, string apellidos)
        {
            Dictionary<string, int> programas = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Programa_Segun_Id";
                con.Open();
                cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    programas.Add(reader.GetString(0), reader.GetInt32(1));
                }

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return programas;
        }

        //Metodo que se encarga de eliminar las relaciones entre un Usuario y las Bodegas 
        public void eliminarUsuarioBodega(string nombre, string apellidos)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Usuario_Bodega";
                    cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                    cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);
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

        //Metodo que se encarga de eliminar las relaciones entre un Usuario y los Programas 
        public void eliminarUsuarioPrograma(string nombre, string apellidos)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Usuario_Programa";
                    cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                    cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);
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

        //Metodo que se encarga de eliminar las relaciones entre un Usuario y las SubBodegas 
        public void eliminarUsuarioSubBodega(string nombre, string apellidos)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Usuario_SubBodega";
                    cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                    cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);
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
        public void actualizarRolUsuario(int id, string nombre, string apellidos)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Actualizar_Rol_Usuario";
                    cmd.Parameters.AddWithValue("@idRol", id);
                    cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                    cmd.Parameters.AddWithValue("@apellidosUsuario", apellidos);
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

        //Metodo que se encarga de editar la informacion personal de un Usuario
        public void editarInfoUsuario(string password, string nombre, string apellidos, string correo)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Actualizar_Usuario";
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@nomUsuario", nombre);
                    cmd.Parameters.AddWithValue("@apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@correo", correo);
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