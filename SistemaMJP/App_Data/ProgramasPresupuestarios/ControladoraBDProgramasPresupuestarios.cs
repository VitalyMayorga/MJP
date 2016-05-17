using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDProgramasPresupuestarios
    {
        SqlConnection con;
        public ControladoraBDProgramasPresupuestarios()
        { 
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }

        //Metodo que se encarga de devolver la lista de todas los programas presupuestarios en el sistema
        public Dictionary<string, int> CargarProgramaPresupuestario()
        {
            Dictionary<string, int> programa = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_ProgramaPresupuestario";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    programa.Add(reader.GetString(0), reader.GetInt32(1));

                }

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return programa;

        }

        //Metodo que se encarga de obtener el nombre de un programa,dado su id
        internal string getNombrePrograma(int id)
        {
            string nombre = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_nombre_Programa";
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


        //Metodo que se encarga de obtener el id de un programa presupuestario, dado su nombre
        public int obtenerIDPrograma(string programa)
        {
            int id = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Id_Programa";
                cmd.Parameters.AddWithValue("@nombre", programa);
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
    }
}