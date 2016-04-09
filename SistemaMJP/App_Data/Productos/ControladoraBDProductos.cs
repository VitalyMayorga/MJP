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
        //Obtiene los datos de un producto de una factura en especifico
        internal List<string> obtenerDatosProducto(int idF, int idP)
        {
            List<string> datos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Datos_Producto_Factura";
                cmd.Parameters.AddWithValue("@idFactura", idF);
                cmd.Parameters.AddWithValue("@idProducto", idP);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                datos.Add(reader.GetString(0));
                datos.Add(reader.GetString(1));
                datos.Add((reader.GetInt32(2)).ToString());
                datos.Add((reader.GetInt32(3)).ToString());
                datos.Add((reader.GetDecimal(4)).ToString());
                bool active = reader.GetBoolean(reader.GetOrdinal("activo"));
                if (active)
                {
                    datos.Add("1");
                }
                else {
                    datos.Add("0");
                }
                //Revisa si son datos null
                if (reader.IsDBNull(6)) {
                    datos.Add(null);
                }
                else
                {
                    datos.Add((reader.GetDateTime(6)).ToString());
                }
                if (reader.IsDBNull(7))
                {
                    datos.Add(null);
                }
                else
                {
                    datos.Add((reader.GetDateTime(7)).ToString());
                }
                if (reader.IsDBNull(8))
                {
                    datos.Add(null);
                }
                else
                {
                    datos.Add((reader.GetDateTime(8)).ToString());
                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return datos;
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
        internal bool agregarProducto(EntidadProductos producto) {
            int valor = 0;
            bool agregado = false;
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
                    agregado = true;
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return agregado;
        
        }
        //Metodo que se encarga de del producto buscado en el sistema
        public int obtenerIDProducto(string descripcion, int cantidad)
        {
            int id = -1;//inicializo el id
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_ID_Producto";
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@cantE", cantidad);
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
        
        //Recibe la informacion del producto para asignarlo a la bodega programa  y subbodega correspondiente.
        internal void agregarProductoABodega(int bodega,int producto,int programa,int subbodega,int cantidad, Nullable<DateTime> fechaG,Nullable<DateTime>fechaC,Nullable<DateTime>fechaV)
        {
            
             using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Informacion_Producto";
                    cmd.Parameters.AddWithValue("@bodega", bodega);
                    cmd.Parameters.AddWithValue("@producto", producto);
                    cmd.Parameters.AddWithValue("@programa", programa);
                    cmd.Parameters.AddWithValue("@subbodega", subbodega);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    if (fechaG.HasValue) {
                        cmd.Parameters.AddWithValue("@fechaG", fechaG);
                    }
                    if (fechaC.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@fechaC", fechaC);
                    }
                    if (fechaV.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@fechaV", fechaV);
                    }
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

        //Recibe la informacion del producto para asignarlo a la Factura correspondiente.
        internal void agregarProductoFactura(int factura, int producto, int cantidad,decimal total, Nullable<DateTime> fechaG, Nullable<DateTime> fechaC, Nullable<DateTime> fechaV)
        {
            string estado = "Pendiende de aprobación";
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Producto_Factura";
                    cmd.Parameters.AddWithValue("@factura", factura);
                    cmd.Parameters.AddWithValue("@producto", producto);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@total", total);
                    if (fechaG.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@fechaG", fechaG);
                    }
                    if (fechaC.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@fechaC", fechaC);
                    }
                    if (fechaV.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@fechaV", fechaV);
                    }
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