using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;

namespace SistemaMJP
{
    public class ServicioLogin
    {
        SqlConnection con;
        public ServicioLogin() { 
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }
        //Metodo que se encarga de revisar si el usuario puso la contraseña correcta
        public bool Autenticar(string usuario, string contraseña)
        {
            bool correcto = false;
            string hash = EncodePassword(string.Concat(usuario, contraseña));
            try
            {
                SqlDataAdapter MyDataAdapter = new SqlDataAdapter("P_Login", con);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //Se asigan los parametros
                MyDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar)).Value = (usuario).Trim();
                MyDataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@contraseña", SqlDbType.VarChar)).Value = (hash);

                DataSet ds = new DataSet();

                //Fill the DataSet with the rows that are returned.
                MyDataAdapter.Fill(ds, "Login");
                MyDataAdapter.Dispose(); //Dispose the DataAdapter.
                con.Close(); //Close the connection.
                if (ds.Tables[0].Rows.Count > 0) {
                    correcto = true;
                }
                
            }
            catch (Exception e) { 
            
            }
           
            return correcto;

        }

        public string EncodePassword(string originalPassword)
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
                    nombre += reader.GetString(0) + "?" + reader.GetString(1); 
                
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

        public int GetID(string correo)
        {
            int id = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_UserID";
                cmd.Parameters.AddWithValue("@usuario", correo);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();                
                id= reader.GetInt32(0);                
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return id;
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
                cmd.CommandText = "P_ProgramaPresupuestario_Usuario";
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

        }
        //encriptar texto
        public string TamperProofStringEncode(string value, string key)
        {
            if (value == null) {
                value = "noData";
            }
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + "-" + Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
        }
        //desencriptar texto
        public string TamperProofStringDecode(string value, string key)
        {
            string dataValue = "";
            string calcHash = "";
            string storedHash = "";

            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

            try
            {
                dataValue = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Split('-')[0]));
                storedHash = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Split('-')[1]));
                calcHash = System.Text.Encoding.UTF8.GetString(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)));

                if (storedHash != calcHash)
                {
                    //'Data was corrupted
                    throw new ArgumentException("Hash value does not match");
                    //  'This error is immediately caught below

                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid TamperProofString");
            }

            return dataValue;

        } 

    }
}