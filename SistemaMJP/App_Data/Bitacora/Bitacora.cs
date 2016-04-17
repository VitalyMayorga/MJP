using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace SistemaMJP
{
    public class Bitacora
    {
        SqlConnection con;
        public Bitacora() {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);

        }
        //Metodo que se encarga de registrar los movimientos de los usuarios en el sistema
        public void registrarActividad(string usuario,string descripcion) {
            DateTime fecha = DateTime.Now;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Registro_Actividad";
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
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