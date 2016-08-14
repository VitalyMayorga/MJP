using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_RequisicionAprobadores
    {
        private String requisicion;
        private String fecha;
        private String destino;
        private int usuario;
        private int programa;
        private int bodega;
        private int subBodega;        
        private int unidad;
        private String observacion;

        public Item_Grid_RequisicionAprobadores(String requisicion, DateTime fecha, String destino, int usuario, int programa, int bodega, int subBodega, int unidad, String observacion)
        {
            Requisicion = requisicion;
            Fecha = fecha.ToString("dd/MM/yyyy");
            Destino = destino;
            Usuario = usuario;
            Programa = programa;            
            Bodega = bodega;
            SubBodega = subBodega;
            Unidad = unidad;
            Observacion = observacion;
                
        } 
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global numFactura con el parámetro
         * Retorna : El valor de la variable global numFactura
         */
        public String Requisicion {
            get { return requisicion; }
            set { requisicion = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Fecha con el parámetro
         * Retorna : El valor de la variable global Fecha
         */
        public String Fecha {
            get { return fecha; }
            set { fecha = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Proveedor con el parámetro
         * Retorna : El valor de la variable global Proveedor
         */
        public string Destino
        {
            get { return destino; }
            set { destino = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Proveedor con el parámetro
         * Retorna : El valor de la variable global Proveedor
         */
        public int Usuario {
            get { return usuario; } 
            set { usuario = value;}
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Programa con el parámetro
        * Retorna : El valor de la variable global Programa
        */
        public int Programa
        {
            get { return programa; }
            set { programa = value; }
        }

        /*
       * Requiere: una Hilera con el valor nuevo
       * Efectúa : Asigna a la variable global Monto con el parámetro
       * Retorna : El valor de la variable global Monto
       */
        public int Bodega
        {
            get { return bodega; }
            set { bodega = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global SubBodega con el parámetro
        * Retorna : El valor de la variable global SubBodega
        */
        public int SubBodega
        {
            get { return subBodega; }
            set { subBodega = value; }
        }       

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Estado con el parámetro
        * Retorna : El valor de la variable global Estado
        */
        public int Unidad
        {
            get { return unidad; }
            set { unidad = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Proveedor con el parámetro
         * Retorna : El valor de la variable global Proveedor
         */
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }
    }
}