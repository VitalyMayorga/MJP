using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class EntidadProductos
    {
        private string descripcion;
        private string presentacion;
        private bool activo;
        private decimal precioU;
        private int cantidadEmpaque;
        private int catalogo;
        public EntidadProductos(object [] datos)
        {
            Descripcion = datos[0].ToString();
            Presentacion = datos[1].ToString();
            Activo = Convert.ToBoolean(datos[2]);
            PrecioU = Convert.ToDecimal(datos[3]);
            CantidadEmpaque = Convert.ToInt32(datos[4]);
            Catalogo = Convert.ToInt32(datos[5]);
                     
                
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Descripcion con el parámetro
         * Retorna : El valor de la variable global Descripcion
         */
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Presentacion con el parámetro
         * Retorna : El valor de la variable global Presentacion
         */
        public string Presentacion
        {
            get { return presentacion; }
            set { presentacion = value; }
        }
        /*
         * Requiere: una Hilera con el valor nuevo
         * Efectúa : Asigna a la variable global Activo con el parámetro
         * Retorna : El valor de la variable global Activo
         */
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global PrecioU con el parámetro
        * Retorna : El valor de la variable global PrecioU
        */
        public decimal PrecioU
        {
            get { return precioU; }
            set { precioU = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global CantidadEmpaque con el parámetro
        * Retorna : El valor de la variable global CantidadEmpaque
        */
        public int CantidadEmpaque
        {
            get { return cantidadEmpaque; }
            set { cantidadEmpaque = value; }
        }

        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Catalogo con el parámetro
        * Retorna : El valor de la variable global Catalogo
        */
        public int Catalogo
        {
            get { return catalogo; }
            set { catalogo = value; }
        }

        
    }
}