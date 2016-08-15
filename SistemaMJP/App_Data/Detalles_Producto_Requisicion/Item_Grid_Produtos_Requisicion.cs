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
        public Item_Grid_Produtos_Requisicion(int producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;              
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
        
    }
}