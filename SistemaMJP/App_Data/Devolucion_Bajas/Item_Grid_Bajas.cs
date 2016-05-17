using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Bajas
    {
        private int idDevolucionBaja;
        private int idProducto;
        private int cantidad;
        private int idPrograma;
        private int idBodega;
        private int idSubBodega;
        private string justificacion;
        public Item_Grid_Bajas(int idDevolucionBaja, int idProducto, int cantidad, int idPrograma, int idBodega, int idSubBodega, string justificacion)
        {
            Id = idDevolucionBaja;
            Producto = idProducto;
            Cantidad = cantidad;
            Programa = idPrograma;
            Bodega = idBodega;
            SubBodega = idSubBodega;
            Justificacion = justificacion;
            
                
        } 
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global numFactura con el parámetro
         * Retorna : El valor de la variable global numFactura
         */
        public int Id
        {
            get { return idDevolucionBaja; }
            set { idDevolucionBaja = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Fecha con el parámetro
         * Retorna : El valor de la variable global Fecha
         */
        public int Producto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Proveedor con el parámetro
         * Retorna : El valor de la variable global Proveedor
         */
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Programa con el parámetro
        * Retorna : El valor de la variable global Programa
        */
        public int Programa
        {
            get { return idPrograma; }
            set { idPrograma = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global SubBodega con el parámetro
        * Retorna : El valor de la variable global SubBodega
        */
        public int Bodega
        {
            get { return idBodega; }
            set { idBodega = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Monto con el parámetro
        * Retorna : El valor de la variable global Monto
        */
        public int SubBodega
        {
            get { return idSubBodega; }
            set { idSubBodega = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Estado con el parámetro
        * Retorna : El valor de la variable global Estado
        */
        public String Justificacion
        {
            get { return justificacion; }
            set { justificacion = value; }
        }
    }
}