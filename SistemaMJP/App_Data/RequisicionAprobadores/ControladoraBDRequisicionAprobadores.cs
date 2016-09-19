using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDRequisicionAprobadores
    {
        SqlConnection con;
        public ControladoraBDRequisicionAprobadores()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);
        }

        //Metodo que se encarga de devolver la lista de requisiciones en espara de ser aprobadas por el aprobador de programa
        internal List<Item_Grid_RequisicionAprobadores> getListaRequisicionAprobador(int programa)
        {
            List<Item_Grid_RequisicionAprobadores> requisiciones = new List<Item_Grid_RequisicionAprobadores>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Requisicion_RevisionPrograma";                
                cmd.Parameters.AddWithValue("@idPrograma", programa);
                con.Open();
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
        //Metodo que se encarga de devolver la lista de Requisiciones Finalizadas por programa
        internal List<Item_Grid_Requisiciones_Finalizadas> getRequisicionesFinalizadas(int programa)
        {
            List<Item_Grid_Requisiciones_Finalizadas> requisiciones = new List<Item_Grid_Requisiciones_Finalizadas>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Requisicion_RevisionAprobador_Finalizadas";
                cmd.Parameters.AddWithValue("@idPrograma", programa);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    requisiciones.Add(LoadItemGridTracking(reader));

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

        //Metodo que se encarga de llenar los datos de la clase item grid tracking y devolver dicha clase encapsulada
        internal Item_Grid_Requisiciones_Finalizadas LoadItemGridTracking(SqlDataReader reader)
        {
            String requisicion = reader.GetString(0); 
            DateTime fecha = reader.GetDateTime(1);
            String estado = reader.GetString(2);
            Item_Grid_Requisiciones_Finalizadas items = new Item_Grid_Requisiciones_Finalizadas(requisicion,fecha, estado);
            return items;
        }

        //Metodo que se encarga de devolver la lista de requisiciones en espara de ser aprobadas por el aprobador de almacen
        internal List<Item_Grid_RequisicionAprobadores> getListaRequisicionAlmacen(int bodega)
        {
            List<Item_Grid_RequisicionAprobadores> requisiciones = new List<Item_Grid_RequisicionAprobadores>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Requisicion_RevisionAlmacen";
                cmd.Parameters.AddWithValue("@idBodega", bodega);
                con.Open();
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

        //Metodo que se encarga de devolver la lista de requisiciones despachadas, aceptadas o rechazadas
        internal List<Item_Grid_RequisicionAprobadores> getListaRequisicionDespachada(int bodega)
        {
            List<Item_Grid_RequisicionAprobadores> requisiciones = new List<Item_Grid_RequisicionAprobadores>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Lista_Requisicion_Despachada";
                cmd.Parameters.AddWithValue("@idBodega", bodega);
                con.Open();
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
        internal Item_Grid_RequisicionAprobadores LoadItemGridRequisiciones(SqlDataReader reader)
        {
            String requisicion = reader.GetString(0);
            DateTime fecha = reader.GetDateTime(1);
            String destino = reader.GetString(2);
            int usuario = reader.GetInt32(3);
            int programa = reader.GetInt32(4);
            int bodega = reader.GetInt32(5);
            int subBodega = reader.GetInt32(6);
            int unidad = reader.GetInt32(7);
            String observacion;
            if (reader.IsDBNull(8))
            {
                observacion = "";
            }
            else {
                 observacion = reader.GetString(8);
            }
            Item_Grid_RequisicionAprobadores items = new Item_Grid_RequisicionAprobadores(requisicion, fecha, destino, usuario, programa, bodega, subBodega, unidad, observacion);
            return items;
        }

        //Metodo que se encarga de obtener el nombre de un usuario,dado su id
        internal string getNombreUnidad(int id)
        {
            string nombre = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_nombre_Unidad";
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                nombre = reader.GetString(0);

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return nombre;
        }

        //Metodo que se encarga de obtener el id de una requisicion dado su numero 
        internal int obtenerIDRequisicion(string numR)
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_id_Requisicion";
                cmd.Parameters.AddWithValue("@numRequisicion", numR);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }

        //Metodo que se encarga de actualizar las observaciones de la requisicion especificada
        internal void actualizarObservacion(int idRequisicion, string observacion)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Actualizar_Observacion";
                    cmd.Parameters.AddWithValue("@idRequisicion", idRequisicion);
                    cmd.Parameters.AddWithValue("@observacion", observacion);
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

        //Metodo que se encarga de obtener el id del programa asignado a una requisicion
        internal int obtenerIDProgramaRequisicion(int requisicion)
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Id_Programa_Requisicion";
                cmd.Parameters.AddWithValue("@idRequisicion", requisicion);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }

        //Metodo que se encarga de obtener el id de una bodega asignada a una requisicion 
        internal int obtenerIDBodegaRequisicion(int requisicion)
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Id_Bodega_Requisicion";
                cmd.Parameters.AddWithValue("@idRequisicion", requisicion);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }

        //Metodo que se encarga de obtener el id de una subBodega asignada a una requisicion 
        internal int obtenerIDSubBodegaRequisicion(int requisicion)
        {
            int id;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Id_SubBodega_Requisicion";
                cmd.Parameters.AddWithValue("@idRequisicion", requisicion);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);

                reader.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }

        //Metodo que se encarga de actualizar la fecha de entrega de la requisicion despachada
        internal void actualizarInfoDespacho(int idRequisicion)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Actualizar_InfoDespacho";
                    cmd.Parameters.AddWithValue("@idRequisicion", idRequisicion);
                    cmd.Parameters.AddWithValue("@fechaRecibido", DateTime.Now);
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

        //Metodo que guarda la informacion de despacho de la requisicion
        internal void agregarInfoDespacho(int idRequisicion, string placa, string nomConductor, string destinatario)
        {           
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.CommandText = "P_Agregar_InfoDespacho";
                    cmd.Parameters.AddWithValue("@idRequisicion", idRequisicion);
                    cmd.Parameters.AddWithValue("@fechaDespacho", DateTime.Now);
                    cmd.Parameters.AddWithValue("@placa", placa);
                    cmd.Parameters.AddWithValue("@nomConductor", nomConductor);
                    cmd.Parameters.AddWithValue("@destinatario", destinatario);
                    
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