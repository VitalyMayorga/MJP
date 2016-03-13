﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBDBodegas
    {
        SqlConnection con;
        public ControladoraBDBodegas()
        {
            con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConexionSistemaInventario"].ConnectionString);

        }

        //Metodo que se encarga de devolver la lista de Programas presupuestarios en el sistema
        internal List<string> getSubBodegas(string programa, string bodega)
        {
            List<string> subbodegas = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_Obtener_Nombre_SubBodegas_Por_Bodega_Programa";
                con.Open();
                cmd.Parameters.AddWithValue("@nombreB", bodega);
                cmd.Parameters.AddWithValue("@nombreP", programa);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    subbodegas.Add(reader.GetString(0));

                }
                reader.Close();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return subbodegas;

        }

        //Metodo que se encarga de devolver la lista de todas las Bodegas en el sistema
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

        //Metodo que se encarga de agregar las bodegas al sistema
        public void AgregarBodega(string bodega)
        {

            try
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Agregar_Bodega";
                cmd.Parameters.AddWithValue("@nombre", bodega);
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de agregar las subBodegas al sistema
        public void AgregarSubBodega(string subBodega)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "P_Agregar_SubBodega";
                cmd.Parameters.AddWithValue("@nombre", subBodega);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo que se encarga de agregar las bodegas y subBodegas relacionadas a la tabla de BodegaSubBodega
        public void AgregarBodegaSubBodega(int bodega)
        {
            try
            {
                con.Open();
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
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}