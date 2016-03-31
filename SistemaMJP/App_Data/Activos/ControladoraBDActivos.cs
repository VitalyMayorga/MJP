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
    public class ControladoraBDActivos
    {
        SqlConnection con;
        public ControladoraBDActivos()
        { 
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }
        //Recibe la informacion del activo parcial para asignarlo al control de activos
        internal void agregarActivo(string numActivo,string funcionario,string cedula,int idProducto)
        {

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Activo";
                    cmd.Parameters.AddWithValue("@numActivo", numActivo);
                    cmd.Parameters.AddWithValue("@funcionario", funcionario);
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Today);
                    cmd.Parameters.AddWithValue("@producto", idProducto);
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