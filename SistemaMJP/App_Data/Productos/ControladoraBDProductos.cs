using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
namespace SistemaMJP
{
    public class ControladoraBDProductos
    {
        SqlConnection con;
        public ControladoraBDProductos()
        { 
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }
        //Metodo que se encarga de devolver la lista de todas las supartidas en el sistema
        public Dictionary<string, int> getSubPartidas()
        {
            Dictionary<string, int> programa = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Subpartidas";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    programa.Add(reader.GetString(1), reader.GetInt32(0));

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
       //Obtiene la lista de productos que comienzan con el prefijo dado
        [WebMethod]
        public static List<string> getProductos(string prefix)
        {
            prefix = prefix.ToLower();
            prefix = prefix.First().ToString().ToUpper() + prefix.Substring(1);
            List<string> productos = new List<string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "P_Obtener_Productos";
                    cmd.Parameters.AddWithValue("@Buscar", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            productos.Add(string.Format("{0}-{1}", sdr["descripcion"], sdr["id_producto"]));
                        }
                    }
                    conn.Close();
                }
            }
            return productos;
        }
    }
}