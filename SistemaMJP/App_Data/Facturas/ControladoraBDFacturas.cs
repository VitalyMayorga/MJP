using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        internal Item_Grid_Facturas LoadItemGridFacturas(SqlDataReader reader) {
            String factura = reader.GetString(0);
            String fecha = reader.GetString(1);
            String proveedor = reader.GetString(2);
            String programa = reader.GetString(3);
            int sb = reader.GetInt32(4);
            int monto = reader.GetInt32(5);
            String estado = reader.GetString(6);
            Item_Grid_Facturas items = new Item_Grid_Facturas(factura,fecha,proveedor,programa,sb,monto,estado);
            return items;
        }
    }
}