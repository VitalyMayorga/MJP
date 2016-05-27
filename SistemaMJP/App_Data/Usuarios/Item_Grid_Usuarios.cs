using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Usuarios
    {
        private String nombre;
        private String apellido;
        private String nomRol;
       
        public Item_Grid_Usuarios(String nombre, String apellido, String nomRol)
        {
            Nombre = nombre;
            Apellido = apellido;
            NomRol = nomRol;
           
        } 
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global nombre con el parámetro
         * Retorna : El valor de la variable global nombre
         */
        public String  Nombre {
            get { return nombre; }
            set { nombre = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global apellido con el parámetro
         * Retorna : El valor de la variable global apellido
         */
        public String Apellido {
            get { return apellido; }
            set { apellido = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global nomRol con el parámetro
         * Retorna : El valor de la variable global nomRol
         */
        public String NomRol {
            get { return nomRol; } 
            set { nomRol = value;}
        }

       
    }
}