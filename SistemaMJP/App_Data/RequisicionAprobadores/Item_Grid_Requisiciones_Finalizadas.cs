using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Requisiciones_Finalizadas
    {
        private DateTime fecha;
        private String estado;
        private String requisicion;
        public Item_Grid_Requisiciones_Finalizadas(String requisicion,DateTime fecha, String estado)
        {
            Fecha = fecha;
            Estado = estado;
            Requisicion = requisicion;
        }
        /*
       * Requiere: una Hilera con el valor nuevo
       * Efectúa : Asigna a la variable global Estado con el parámetro
       * Retorna : El valor de la variable global Fecha
       */
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
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

        /*
      * Requiere: una Hilera con el valor nuevo
      * Efectúa : Asigna a la variable global Requisicion con el parámetro
      * Retorna : El valor de la variable global Requisicion
      */
        public String Requisicion
        {
            get { return requisicion; }
            set { requisicion = value; }
        }
    }
}