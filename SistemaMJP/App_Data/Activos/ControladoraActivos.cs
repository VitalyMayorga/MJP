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

        internal List<Item_Grid_Activos> getListaActivos(string bodega)
        {
            return controladoraBD.getListaActivos(bodega);
        }

        //Metodo que llama ala controladora de base de datos para agregar el activo nuevo con documento integrado
        internal int agregarActivoFinal(object[] datos) {
            return controladoraBD.agregarActivoFinal(datos);
        
        }
        //Metodo que llama ala controladora de base de datos para obtener los activos de la bodega
        internal Dictionary<string, int> getActivos(string bodega) {
            return controladoraBD.getActivos(bodega);
        
        }
        //Metodo que llama ala controladora de base de datos para obtener los datos del activo
        internal List<string> obtenerDatosActivo(string numActivo) {
            return controladoraBD.obtenerDatosActivo(numActivo);
        }
        //Metodo que llama ala controladora de base de datos para modificar el activo
        internal void modificarActivo(object[] activo) {
            controladoraBD.modificarActivo(activo);
        }
        //Metodo que llama ala controladora de base de datos para eliminar el activo
        internal void eliminarActivo(string activo) {
            controladoraBD.eliminarActivo(activo);
        }
    }
}