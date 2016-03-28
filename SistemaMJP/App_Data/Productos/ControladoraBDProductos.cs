using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Transactions;
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
        //Recibe el producto encapsulado y procede a guardarlo en la BD
        internal void agregarProducto(EntidadProductos producto) {
            int valor = 0;
            if (producto.Activo) {
                valor = 1;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Producto";
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@presentacion", producto.Presentacion);
                    cmd.Parameters.AddWithValue("@activo", valor);
                    cmd.Parameters.AddWithValue("@precioU", producto.PrecioU);
                    cmd.Parameters.AddWithValue("@cantidad", producto.CantidadEmpaque);
                    cmd.Parameters.AddWithValue("@catalogo", producto.Catalogo);
                    
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
                            productos.Add(string.Format("{0}", sdr["descripcion"]));
                        }
                    }
                    conn.Close();
                }
            }
            return productos;
        }
    }
}