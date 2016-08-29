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
        public Item_Grid_Productos_Bodega(String nombre, String unidad)
        {
            Nombre = nombre;
            Unidad = unidad;
            
                
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
    }
}