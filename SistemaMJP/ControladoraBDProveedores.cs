using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Transactions;

namespace SistemaMJP
{
    public class ControladoraBDProveedores
    {
        SqlConnection con;
        public ControladoraBDProveedores() {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }
        //Metodo que se encarga de agregar proveedores al sistema
        internal void agregarProveedor(string proveedor, string correo,string telefonos)
        {
            using (TransactionScope ts = new TransactionScope()) {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Proveedor";
                    cmd.Parameters.AddWithValue("@nombre", proveedor);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    agregarTelefonosProveedor(telefonos,proveedor);
                    ts.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            
            }

            
        }
        //Agrega el o los telefonos del proveedor recien incluido
        internal void agregarTelefonosProveedor(string telefonos,string nombre) {
            int id = obtenerId(nombre);//obtenemos el id del proveedor
            string[] resultado = telefonos.Split(',');
            foreach (var tel in resultado) {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Agregar_Telefono_Proveedor";
                cmd.Parameters.AddWithValue("@tel", tel);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.Close();
            
            }
            
        
        }
        //Metodo que se encarga de devolver la lista de todas los proveedores en el sistema
        public Dictionary<string, int> getProveedores()
        {
            Dictionary<string, int> programa = new Dictionary<string, int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Proveedores";
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

        //obtiene el id del proveedor
        internal int obtenerId(string proveedor) {
            int id=0;
            try
            {
                proveedor.Replace(";", "");//Evitar sqlinjection
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_id_Proveedor";
                con.Open();
                cmd.Parameters.AddWithValue("@nombre", proveedor);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    id = reader.GetInt32(0);

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return id;

        }
        

    }
}