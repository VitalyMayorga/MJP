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
        internal List<string> getSubBodegas(string programa,string bodega)
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
    }
}