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
    }
}