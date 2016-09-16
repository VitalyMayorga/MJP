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

        //Metodo que llama a la controladora de base de datos para agregar el activo nuevo
        internal void agregarActivo(string numActivo, string funcionario, string cedula,int idProducto)
        {
            controladoraBD.agregarActivo(numActivo, funcionario, cedula,idProducto);
        }

        internal List<Item_Grid_Activos> getListaActivos(string bodega)
        {
            return controladoraBD.getListaActivos(bodega);
        }

        //Metodo que llama a la controladora de base de datos para agregar el activo nuevo con documento integrado
        internal int agregarActivoFinal(object[] datos) {
            return controladoraBD.agregarActivoFinal(datos);        
        }

        //Metodo que llama a la controladora de base de datos para obtener los activos de la bodega
        internal Dictionary<string, int> getActivos(string bodega) {
            return controladoraBD.getActivos(bodega);
        
        }
        //Metodo que llama a la controladora de base de datos para obtener las requisiciones de la bodega (no aceptadas en destino o rechazadas)
        internal Dictionary<string, int> getRequisicionesBodega(string bodega)
        {
            return controladoraBD.getRequisicionesBodega(bodega);

        }
        //Metodo que llama a la controladora de base de datos para obtener los datos del activo
        internal List<string> obtenerDatosActivo(string numActivo) {
            return controladoraBD.obtenerDatosActivo(numActivo);
        }

        //Metodo que llama a la controladora de base de datos para modificar el activo
        internal void modificarActivo(object[] activo) {
            controladoraBD.modificarActivo(activo);
        }

        //Metodo que llama a la controladora de base de datos para eliminar el activo
        internal void eliminarActivo(string activo) {
            controladoraBD.eliminarActivo(activo);
        }

        //Metodo que llama a la controladora de base de datos para obtenar la cantidad de activos de un determinado producto asignados a una requisicion
        internal int obtenerCantidadActivos(int idRequisicion, int idProducto)
        {
            return controladoraBD.obtenerCantidadActivos(idRequisicion, idProducto);
        }

        //Metodo que llama a la controladora de base de datos para revisar si un producto es un activo
        internal bool esActivo(int producto)
        {
            return controladoraBD.esActivo(producto);
        }

    }
}