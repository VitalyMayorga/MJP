using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Produtos_Requisicion
    {
        private int producto;
        private int cantidad;
        private int id;
        public Item_Grid_Produtos_Requisicion(int id, int producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;           
            Id = id;            
                
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global descripcion con el parámetro
         * Retorna : El valor de la variable global descripcion
         */
        public int Producto
        {
            get { return producto; }
            set { producto = value; }
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
        
    }
}