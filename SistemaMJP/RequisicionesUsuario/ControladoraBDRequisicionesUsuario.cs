using SistemaMJP.RequisicionesUsuario;
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
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@idUsuario", usuarioID);
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
            string prefijo = obtenerPrefijo(bodega);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Ultima_Requisicion";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (!reader.HasRows) {
                    num = "";
                }
                else
                {
                    num = reader.GetString(0);

                }
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

        //Metodo que se encarga de obtener el prefijo de una bodega, esto para poder crear los numero de requisicion
        internal string obtenerPrefijo(int bodega) {
            string prefijo = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Prefijo";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                prefijo = reader.GetString(0);
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return prefijo;
        }
        //Metodo que se encarga de obtener la cantidad de producto  que hay por bodega y subbodega(si existiese)
        internal int obtenerCantidadProductoBodega(int bodega, int subbodega, string programa, string producto)
        {
            int cantidad = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Cantidad_Producto_En_Bodega";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@subbodega", subbodega);
                cmd.Parameters.AddWithValue("@programa", programa);
                cmd.Parameters.AddWithValue("@producto", producto);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                cantidad = reader.GetInt32(0);
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return cantidad;
        }
        //Metodo que se encarga de obtener la cantidad de productos que hay por empaque y los retorna en forma de lista
        internal List<int> obtenerCantPorEmpaque(int bodega, int subbodega, string programa, string producto)
        {
            List<int> empaques = new List<int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Cantidad_Empaque_Producto";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@subbodega", subbodega);
                cmd.Parameters.AddWithValue("@programa", programa);
                cmd.Parameters.AddWithValue("@producto", producto);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    empaques.Add(reader.GetInt32(0));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return empaques;
        }
        //Metodo que se encarga de obtener el tipo de Empaque del producto buscado en forma de Lista, por si exisitieran varias presentaciones de empaque
        internal List<int> obtenerEmpaque(int bodega, int subbodega, string programa, string producto)
        {
            List<int> empaques = new List<int>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Empaque_Producto";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@subbodega", subbodega);
                cmd.Parameters.AddWithValue("@programa", programa);
                cmd.Parameters.AddWithValue("@producto", producto);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    empaques.Add(reader.GetInt32(0));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return empaques;
        }
        //Metodo que se encarga de obtener todos los productos que cumplan con el criterio de busqueda dado por el usuario y pertenenzcan a la bodega correspondiente
        internal List<Item_Grid_Productos_Bodega> getListaProductosBodega(int bodega, int subbodega, string programa, string busqueda)
        {
            List<Item_Grid_Productos_Bodega> productos = new List<Item_Grid_Productos_Bodega>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Productos_Bodega_Busqueda";
                con.Open();
                cmd.Parameters.AddWithValue("@bodega", bodega);
                cmd.Parameters.AddWithValue("@subbodega", subbodega);
                cmd.Parameters.AddWithValue("@programa", programa);
                cmd.Parameters.AddWithValue("@busqueda", busqueda);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productos.Add(LoadItemGridProductos(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }
        //Metodo que se encarga de agregar un producto a la requisicon
        internal void agregarProducto(string producto, string numRequisicion,int cantidad)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_Producto_Requisicion";
                    cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                    cmd.Parameters.AddWithValue("@producto", producto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
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
        //Metodo que se encarga de editar la cantidad especificada de un producto en una requisicion
        internal void editarProducto(string producto, string numRequisicion, int cantidad)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Editar_Producto_Requisicion";
                    cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                    cmd.Parameters.AddWithValue("@producto", producto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
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

        //Metodo que se encarga de llenar los datos de la clase item grid productos bodega y devolver dicha clase encapsulada
        internal Item_Grid_Productos_Bodega LoadItemGridProductos(SqlDataReader reader)
        {
            String nombre = reader.GetString(0);
            String unidad = reader.GetString(1);
            int cantidad = 0;

            try
            {
                cantidad = reader.GetInt32(2);
            }
            catch (Exception ex) { }
            Item_Grid_Productos_Bodega items = new Item_Grid_Productos_Bodega(nombre,unidad,cantidad);
            return items;
        }
        //Metodo que se encarga de obtener el estado actual de la requisicion
        internal string getEstadoRequisicion(string numRequisicion)
        {
            string estado;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Estado_Requiscion";
                con.Open();
                cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                estado = reader.GetString(0);
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return estado;

        }
        //Metodo que se encarga de eliminar un producto asociado a una requisicion
        internal void eliminarProducto(string numRequisicion, string producto)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Producto_Requisicion_usuario";
                    cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                    cmd.Parameters.AddWithValue("@producto", producto);
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
        //Metodo que se encarga de eliminar una requisicion que no ha sido enviada a aprobacion de la BD
        internal void eliminarRequisicion(string numRequisicion)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Eliminar_Requisicion";
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

        }
        //Metodo que se encarga de obtener los datos de una requisicion(bodega,programa,subbodega)
        internal List<string> getDatosRequisicion(string numRequisicion)
        {
            List<string> datos = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Datos_Requisicion";
                con.Open();
                cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    datos.Add(reader.GetInt32(0).ToString());//bodega
                    datos.Add(reader.GetString(1));//programa
                    datos.Add(reader.GetInt32(2).ToString());//subbodega
                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return datos;
        }

        //Envia una requisicion y sus productos a Aprobación
        internal void enviarAAprobacion(string numRequisicion)
        {
            string estado = "Pendiente Aprobación Programa";
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Enviar_Requisicion_Aprobacion_1";
                    cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

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
        //Metodo que se encarga de obtener los productos de una bodega y devolverlos encapsulados
        internal List<Item_Grid_Productos_Bodega> getListaProductosRequisicion(string numRequisicion)
        {
            List<Item_Grid_Productos_Bodega> productos = new List<Item_Grid_Productos_Bodega>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Productos_Requisicion";
                con.Open();
                cmd.Parameters.AddWithValue("@numReq", numRequisicion);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productos.Add(LoadItemGridProductos(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }
        //Metodo que se encarga de obtener el id de una requisicion
        internal int obtenerIDRequisicion(string numRequisicion)
        {
            int cantidad = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Id_Requisicion";
                con.Open();
                cmd.Parameters.AddWithValue("@numRequisicion", numRequisicion);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                cantidad = reader.GetInt32(0);
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return cantidad;

        }
        //Obtiene el seguimiento de la requisicion que se pasa como parametro, y lo retorna para llenar la tabla
        internal List<Item_Grid_Tracking> getTracking(string numRequisicion)
        {
            List<Item_Grid_Tracking> track = new List<Item_Grid_Tracking>();
            int id = obtenerIDRequisicion(numRequisicion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Tracking";
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    track.Add(LoadItemGridTracking(reader));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return track;
        }
        //Metodo que se encarga de llenar los datos de la clase item grid tracking y devolver dicha clase encapsulada
        internal Item_Grid_Tracking LoadItemGridTracking(SqlDataReader reader)
        {
            DateTime fecha = reader.GetDateTime(0);
            String estado = reader.GetString(1);
            Item_Grid_Tracking items = new Item_Grid_Tracking(fecha, estado);
            return items;
        }

        
    }
}