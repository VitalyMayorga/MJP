using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Requisiciones
    {
        private String numRequisicion;
        private String fecha;
        private String unidad;
        private String estado;
        public Item_Grid_Requisiciones(String requisicion, DateTime fecha, String unidad, String estado)
        {
            NumRequisicion = requisicion;
            Fecha = fecha.ToString("dd/MM/yyyy");
            Unidad = unidad;
            Estado = estado;
            
                
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global numRequisicion con el parámetro
         * Retorna : El valor de la variable global numRequisicion
         */
        public String NumRequisicion
        {
            get { return numRequisicion; }
            set { numRequisicion = value; }
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
         * Efectúa : Asigna a la variable global Unidad con el parámetro
         * Retorna : El valor de la variable global Unidad
         * */
        public String Unidad {
            get { return unidad; } 
            set { unidad = value;}
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