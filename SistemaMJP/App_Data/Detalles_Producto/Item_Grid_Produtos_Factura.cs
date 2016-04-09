using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Produtos_Factura
    {
        private String descripcion;
        private int cantidad;
        private decimal precioTotal;
        private String estado;
        private int id;
        public Item_Grid_Produtos_Factura(int id,String descripcion, int cantidad, decimal precioTotal, String estado)
        {
            Descripcion = descripcion;
            Cantidad = cantidad;
            PrecioTotal = precioTotal;
            Estado = estado;
            Id = id;
            
                
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global descripcion con el parámetro
         * Retorna : El valor de la variable global descripcion
         */
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Cantidad con el parámetro
         * Retorna : El valor de la variable global Cantidad
         */
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Id con el parámetro
        * Retorna : El valor de la variable global Id
        */
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global PrecioTotal con el parámetro
         * Retorna : El valor de la variable global PrecioTotal
         */
        public decimal PrecioTotal
        {
            get { return precioTotal; }
            set { precioTotal = value; }
        }

        
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Estado con el parámetro
        * Retorna : El valor de la variable global Estado
        */
        public String Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}