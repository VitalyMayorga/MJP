using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDFacturas
    {
        SqlConnection con;
        public ControladoraBDFacturas() {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        }

        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal List<Item_Grid_Facturas> getListaFacturas(string bodega)
        {
            List<Item_Grid_Facturas> facturas = new List<Item_Grid_Facturas>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Facturas_Por_Bodega";
                con.Open();
                cmd.Parameters.AddWithValue("@nombreB", bodega);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    facturas.Add(LoadItemGridFacturas(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return facturas;

        }

        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal List<Item_Grid_Facturas> getListaFacturasAdmin(string bodega)
        {
            List<Item_Grid_Facturas> facturas = new List<Item_Grid_Facturas>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Facturas_Bodega_Admin";
                con.Open();
                cmd.Parameters.AddWithValue("@nombreB", bodega);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    facturas.Add(LoadItemGridFacturas(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return facturas;

        }
        //Metodo que se encarga de guardar una factura en la base de datos
        internal void agregarFactura(int bodega, int proveedor, int programa,int subbodega,string numF,string fecha) {
            DateTime fechaF = Convert.ToDateTime(fecha);
            string estado = "En edición";
            int total = 0;
            DateTime fhoy = Convert.ToDateTime(fecha);
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Factura";
                    cmd.Parameters.AddWithValue("@numF", numF);
                    cmd.Parameters.AddWithValue("@fecha", fechaF);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@proveedor", proveedor);
                    cmd.Parameters.AddWithValue("@bodega", bodega);
                    cmd.Parameters.AddWithValue("@programa", programa);
                    cmd.Parameters.AddWithValue("@subB", subbodega);
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

        //Metodo que se encarga de obtener el ID de la factura
        internal int obtenerIDFactura(string factura)
        {
            int id=0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_ID_Factura";
                con.Open();
                cmd.Parameters.AddWithValue("@numF", factura);
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

        //Metodo que se encarga de llenar los datos de la clase item grid facturas y devolver dicha clase encapsulada
        internal Item_Grid_Facturas LoadItemGridFacturas(SqlDataReader reader) {
            String factura = reader.GetString(0);
            DateTime fecha = reader.GetDateTime(1);
            String proveedor = reader.GetString(2);
            String programa = reader.GetString(3);
            int sb = reader.GetInt32(4);
            decimal monto = reader.GetDecimal(5);
            String estado = reader.GetString(6);
            Item_Grid_Facturas items = new Item_Grid_Facturas(factura,fecha,proveedor,programa,sb,monto,estado);
            return items;
        }
    }
}