using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP.RequisicionesUsuario
{
    public class Item_Grid_Productos_Bodega
    {
        private String nombre;
        private String descripcion;
        private String unidad;
        private int id;
        public Item_Grid_Productos_Bodega(String nombre, String descripcion, int id, String unidad)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Unidad = unidad;
            Id = id;
            
                
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
         * Efectúa : Asigna a la variable global descripcion con el parámetro
         * Retorna : El valor de la variable global descripcion
         */
        public String Descripcion {
            get { return descripcion; }
            set { descripcion = value; }
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
        * Efectúa : Asigna a la variable global id con el parámetro
        * Retorna : El valor de la variable global id
        */
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}