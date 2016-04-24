using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class Item_Grid_Activos
    {
        private String numero;
        private String documento;
        private DateTime fecha;
        private String funcionario;
        private String cedula;
        private String producto;
        public Item_Grid_Activos(String numero,String documento,DateTime fecha,String funcionario,String cedula,String producto) { 
        
        
        }
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Numero con el parámetro
        * Retorna : El valor de la variable global Numero
        */
        public String Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Documento con el parámetro
        * Retorna : El valor de la variable global Documento
        */
        public String Documento
        {
            get { return documento; }
            set { documento = value; }
        }
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Fecha con el parámetro
        * Retorna : El valor de la variable global Fecha
        */
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Funcionario con el parámetro
        * Retorna : El valor de la variable global Funcionario
        */
        public String Funcionario
        {
            get { return funcionario; }
            set { funcionario = value; }
        }
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Cedula con el parámetro
        * Retorna : El valor de la variable global Cedula
        */
        public String Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }
        /*
        * Requiere: una Hilera con el valor nuevo
        * Efectúa : Asigna a la variable global Producto con el parámetro
        * Retorna : El valor de la variable global Producto
        */
        public String Producto
        {
            get { return producto; }
            set { producto = value; }

        }
    }
}