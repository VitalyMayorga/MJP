using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP.RequisicionesUsuario
{
    public class Item_Grid_Tracking
    {
        private DateTime fecha;
        private String estado;

        public Item_Grid_Tracking(DateTime fecha, String estado) {
            Fecha = fecha;
            Estado = estado;
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
    }
}