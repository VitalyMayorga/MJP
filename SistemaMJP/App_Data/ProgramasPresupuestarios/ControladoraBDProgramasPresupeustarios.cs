using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDProgramasPresupeustarios
    {
        SqlConnection con;
        public ControladoraBDProgramasPresupeustarios()
        { 
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }

        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal List<string> getProgramas()
        {
            List<string> programas = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_NombresProgramas";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    programas.Add(reader.GetString(0));

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
    }
}