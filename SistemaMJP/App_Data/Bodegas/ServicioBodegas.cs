using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaMJP
{
    public class ServicioBodegas
    {
       
        SqlConnection con;
        public ServicioBodegas()
        { 
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }

        public List<string> CargarBodegas()
        {
            List<string> bodega = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Bodega";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();              
                while (reader.Read())
                {
                    bodega.Add(reader.GetString(0));
                    bodega.Add(reader.GetInt32(1).ToString());
                  

                }              

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return bodega;
            
        }

        public void AgergarBodega(string bodega)
        {
           
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Agregar_Bodega";
                cmd.Parameters.AddWithValue("@nombre", bodega);
                con.Open();
                
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AgergarSubBodega(string subBodega)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Agregar_SubBodega";
                cmd.Parameters.AddWithValue("@nombre", subBodega);
                con.Open();

                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AgergarBodegaSubBodega(int bodega, int subBodega)
        {

            try
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "P_SubBodega_MaxID";
                SqlDataReader reader = cmd1.ExecuteReader(); 
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Agregar_Bodega_SubBodega";               

                cmd.Parameters.AddWithValue("@idBodega", bodega);
                cmd.Parameters.AddWithValue("@idSubBodega", reader.GetInt32(0));
                con.Open();

                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        //Metodo que se encarga de revisar si el usuario puso la contraseña correcta
        public bool Autenticar(string usuario, string contraseña)
        {
            bool correcto = false;
            string hash = EncodePassword(string.Concat(usuario, contraseña));
            try
            {
                SqlDataAdapter MyDataAdapter = new SqlDataAdapter("P_Bodega", con);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
              
                ////Create and add an output parameter to the Parameters collection. 
                //MyDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@RowCount", SqlDbType.Int, 4));

                ////Set the direction for the parameter. This parameter returns the Rows that are returned.
                //MyDataAdapter.SelectCommand.Parameters["@RowCount"].Direction = ParameterDirection.Output;
                con.Open();
                //Create a new DataSet to hold the records.
                DataSet ds = new DataSet();

                //Fill the DataSet with the rows that are returned.
                MyDataAdapter.Fill(ds, "Login");
                MyDataAdapter.Dispose(); //Dispose the DataAdapter.
                con.Close(); //Close the connection.
                if (ds.Tables[0].Rows.Count > 0) {
                    correcto = true;
                }
                //Declaramos la sentencia SQL
                //string sql = "SELECT COUNT(*) FROM Usuario WHERE Username = '" + usuario + "' AND Contrasena = '" + hash + "'";
                //DataTable dt = adaptadorBD.consultar(sql);
                //if (int.Parse(dt.Rows[0][0].ToString()) == 0) { return false; }
                //else { return true; }
            }
            catch (Exception e) { 
            
            }
           
            return correcto;

        }
        public static string EncodePassword(string originalPassword)
        {
            //Clave que se utilizará para encriptar el usuario y la contraseña
            string clave = "7f9facc418f74439c5e9709832;0ab8a5:OCOdN5Wl,q8SLIQz8i|8agmu¬s13Q7ZXyno/";
            //Se instancia el objeto sha512 para posteriormente usarlo para calcular la matriz de bytes especificada
            SHA512 sha512 = new SHA512CryptoServiceProvider();

            //Se crea un arreglo llamada inputbytes donde se convierte el usuario, la contraseña y la clave a una secuencia de bytes.
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword + clave);
            //Se calcula la matriz de bytes del arreglo anterior y se encripta.
            byte[] hash = sha512.ComputeHash(inputBytes);
            //Convertimos el arreglo de bytes a cadena.
            return Convert.ToBase64String(hash);
        }

        //Metodo que se encarga de devolver el nombre de usuario por medio del correoInstitucional
        public string GetUsername(string correo)
        {
           string nombre = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Username";
                cmd.Parameters.AddWithValue("@correo", correo);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read()) {
                    nombre += reader.GetString(0) + " " + reader.GetString(1); 
                
                }
                reader.Close();
                con.Close();
                //SqlDataAdapter MyDataAdapter = new SqlDataAdapter("P_Username", con);
                //MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                ////Se asigan los parametros
                //MyDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@correo", SqlDbType.VarChar)).Value = (correo).Trim();
                //con.Open();
                ////Create a new DataSet to hold the records.
                //DataSet ds = new DataSet();

                ////Fill the DataSet with the rows that are returned.
                //MyDataAdapter.Fill(ds, "Username");
                //MyDataAdapter.Dispose(); //Dispose the DataAdapter.
                //con.Close(); //Close the connection.
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    nombre = (string)ds.Tables[0].Rows[0]["nombreUsuario"];
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return nombre;

        }

        //Metodo que se encarga de devolver el rol por medio del correoInstitucional
        public string GetRol(string correo)
        {
            string nombre = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Rol";
                cmd.Parameters.AddWithValue("@correo", correo);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    nombre = reader.GetString(0);

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return nombre;

        }

        //Metodo que se encarga de devolver la lista de programas presupuestarios accesibles de un usuario, por medio del correoInstitucional
        public List<string> GetProgramasPresupuestarios(string correo)
        {
            List<string> programas = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Programas_Presupuestarios_Usuario";
                cmd.Parameters.AddWithValue("@correo", correo);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    programas.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return programas;

        }
        //Metodo que se encarga de devolver la lista de bodegas accesibles de un usuario, por medio del correoInstitucional
        public List<string> GetBodegas(string correo)
        {
            List<string> programas = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Bodegas_Usuario";
                cmd.Parameters.AddWithValue("@correo", correo);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    programas.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return programas;

        }*/
    }
}