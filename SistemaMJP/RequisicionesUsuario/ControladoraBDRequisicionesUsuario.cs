using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDRequisicionesUsuario
    {
        SqlConnection con;
        public ControladoraBDRequisicionesUsuario()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        }

        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal List<Item_Grid_Requisiciones> getListaRequisiciones(string bodega,int usuarioID)
        {
            List<Item_Grid_Requisiciones> requisiciones = new List<Item_Grid_Requisiciones>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Requisicion_Por_Bodega";
                con.Open();
                cmd.Parameters.AddWithValue("@nombreB", bodega);
                cmd.Parameters.AddWithValue("@usuarioID", usuarioID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    requisiciones.Add(LoadItemGridRequisiciones(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return requisiciones;

        }

        //Metodo que se encarga de llenar los datos de la clase item grid facturas y devolver dicha clase encapsulada
        internal Item_Grid_Requisiciones LoadItemGridRequisiciones(SqlDataReader reader)
        {
            String requisicion = reader.GetString(0);
            DateTime fecha = reader.GetDateTime(1);
            String unidad = reader.GetString(2);
            String estado = reader.GetString(3);
            Item_Grid_Requisiciones items = new Item_Grid_Requisiciones(requisicion, fecha, unidad, estado);
            return items;
        }
    }
}