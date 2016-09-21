using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace SistemaMJP
{
    public class EmailManager
    {
        bool invalid;
        SqlConnection con;
        public EmailManager() {

            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        
        }

        //metodo usado para obtener los correos de los administradores del almacen solicitado
        internal List<string> obtenerCorreosAdminAlmacen(string bodega) { 
            List<string> correos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Correos_AdminAlmacen";
                cmd.Parameters.AddWithValue("@bodega", bodega);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    correos.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return correos;        
        }

        //metodo usado para obtener los correos de los bodegueros del almacen solicitado
        internal List<string> obtenerCorreosBodegueros(string bodega)
        {
            List<string> correos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Correos_Bodeguero";
                cmd.Parameters.AddWithValue("@bodega", bodega);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    correos.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return correos;
        }

        //metodo usado para obtener los correos de los administradores del almacen solicitado
        internal List<string> obtenerCorreosAprobadorSegunPrograma(int requisicion)
        {
            List<string> correos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Correo_Requisicion_Programa";
                cmd.Parameters.AddWithValue("@idRequisicion", requisicion);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    correos.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return correos;
        }

        //metodo usado para obtener el correos de las personas de programa presupuestario
        internal List<string> obtenerCorreosAprobador(int programa, int bodega, int subBodega)
        {
            List<string> correos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Correos_Aprobador";
                cmd.Parameters.AddWithValue("@programa", programa);
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@subBodega", subBodega);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    correos.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return correos;
        }

        //metodo usado para obtener el correo del administrador general
        internal List<string> obtenerCorreoUsuarioRequisicion(int requisicion)
        {
            List<string> correos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Correo_Usuario_Requisicion";
                cmd.Parameters.AddWithValue("@idRequisicion", requisicion);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    correos.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return correos;
        }

        //Método usado para verificar emal
        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        public void MailSender(string informacion, string subject, List<string> correos)
        {
            //Por ahora esta configurado para correos hotmail, esto lo tendrá q cambiar los de TI para configurar el SMTP del MJP
            //foreach (string correo in correos)
            //{
            /*MailMessage o = new MailMessage("jvitaly_93@hotmail.com", "jvitaly.93@gmail.com", subject, informacion);
            NetworkCredential netCred = new NetworkCredential("primolo_0418@hotmail.com", "PRRasdf2512");//Usar credenciales de alguna cuenta hotmail para probar
            SmtpClient smtpobj = new SmtpClient("smtp.live.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);*/

            //}
        }
    }
}