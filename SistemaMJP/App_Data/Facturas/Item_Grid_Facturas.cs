using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Facturas
    {
        private String numFactura;
        private String fecha;
        private String proveedor;
        private String programa;
        private int subBodega;
        private int monto;
        private String estado;
        public Item_Grid_Facturas(String factura, String fecha, String proveedor,String programa, int subBodega, int monto,String estado)
        {
            NumFactura = factura;
            Fecha = fecha;
            Proveedor = proveedor;
            Monto = monto;
            Estado = estado;
            Programa = programa;
            SubBodega = subBodega;
            
                
        } 
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global numFactura con el parámetro
         * Retorna : El valor de la variable global numFactura
         */
        public String  NumFactura {
            get { return numFactura; }
            set { numFactura = value; }
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
         * Efectúa : Asigna a la variable global Proveedor con el parámetro
         * Retorna : El valor de la variable global Proveedor
         */
        public String Proveedor {
            get { return proveedor; } 
            set { proveedor = value;}
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Programa con el parámetro
        * Retorna : El valor de la variable global Programa
        */
        public String Programa
        {
            get { return programa; }
            set { programa = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global SubBodega con el parámetro
        * Retorna : El valor de la variable global SubBodega
        */
        public int SubBodega
        {
            get { return subBodega; }
            set { subBodega = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Monto con el parámetro
        * Retorna : El valor de la variable global Monto
        */
        public int Monto
        {
            get { return monto; }
            set { monto = value; }
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