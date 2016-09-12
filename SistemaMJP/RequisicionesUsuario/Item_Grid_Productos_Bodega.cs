using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP.RequisicionesUsuario
{
    public class Item_Grid_Productos_Bodega
    {
        private String nombre;
        private String unidad;
        private int cantidad;
        public Item_Grid_Productos_Bodega(String nombre, String unidad,int cantidad)
        {
            Nombre = nombre;
            Unidad = unidad;
            Cantidad = cantidad;
                
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global nombre con el parámetro
         * Retorna : El valor de la variable global nombre
         */
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
       
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Unidad con el parámetro
         * Retorna : El valor de la variable global Unidad
         * */
        public String Unidad {
            get { return unidad; } 
            set { unidad = value;}
        }

        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Cantidad con el parámetro
         * Retorna : El valor de la variable global Cantidad
         * */
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}