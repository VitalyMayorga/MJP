using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraActivos
    {
        ControladoraBDActivos controladoraBD;
        public ControladoraActivos() {
            controladoraBD = new ControladoraBDActivos();
        }
        //Metodo que llama ala controladora de base de datos para agregar el activo nuevo
        internal void agregarActivo(string numActivo, string funcionario, string cedula,int idProducto)
        {
            controladoraBD.agregarActivo(numActivo, funcionario, cedula,idProducto);
        }
    }
}