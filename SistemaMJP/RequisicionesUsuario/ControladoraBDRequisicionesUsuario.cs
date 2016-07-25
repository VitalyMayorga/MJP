using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
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

        //Metodo que se encarga de guardar una requisicion en la base de datos
        internal string agregarRequisicion(int usuario, int bodega, string programa, int subbodega, string unidadSolicitante, string destino, string observaciones)
        {
            DateTime fechaF = DateTime.Today;
            string estado = "En edición";
            string numRequisicion = obtenerNumReq(bodega);
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Requisicion";
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@fecha", fechaF);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@bodega", bodega);
                    cmd.Parameters.AddWithValue("@programa", programa);
                    cmd.Parameters.AddWithValue("@subB", subbodega);
                    cmd.Parameters.AddWithValue("@unidad", unidadSolicitante);
                    cmd.Parameters.AddWithValue("@destino", destino);
                    cmd.Parameters.AddWithValue("@observaciones", observaciones);
                    cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ts.Complete();
                }
                catch (Exception)
                {
                    throw;
                }

            }

            return numRequisicion;
        }
        //Retorna las unidades solicitantes existentes en la BD
        internal List<string> getUnidades()
        {
            List<string> unidades = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Unidades";
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    unidades.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return unidades;

        }
        //Metodo que se encarga de generar los numeros de requisicion dependiendo de la bodega
        internal string obtenerNumReq(int bodega) {
            string num = "";
            string prefijo = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Ultima_Requisicion";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                SqlDataReader reader = cmd.ExecuteReader();
                prefijo = reader.GetString(0);
                num = reader.GetString(1);
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            if (string.IsNullOrEmpty(num))
            {
                num = prefijo + "-1";
            }
            else {//Crea un numero con el siguiente entero
                int val = 0;
                string []tmp = num.Split('-');
                val = Convert.ToInt32(tmp[1]);
                val++;
                num = tmp[0] + "-" + val;
            }

            return num;
        }
    }
}