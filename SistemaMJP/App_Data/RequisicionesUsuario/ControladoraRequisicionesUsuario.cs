using SistemaMJP.RequisicionesUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraRequisicionesUsuario
    {
        ControladoraBDRequisicionesUsuario controladoraBD;
        ControladoraBodegas controladoraB;
        ControladoraRequisicionAprobadores controladoraR;
        ControladoraProgramasPresupuestarios controladoraP;
        public ControladoraRequisicionesUsuario() { 
            controladoraBD = new ControladoraBDRequisicionesUsuario();
            controladoraB = new ControladoraBodegas();
            controladoraR = new ControladoraRequisicionAprobadores();
            controladoraP = new ControladoraProgramasPresupuestarios();
        }

        //Llama a la controladora de programa presupuestario para obtener el ID del programa
        internal int obtenerIDPrograma(string programa){
            return controladoraP.obtenerIDPrograma(programa);
        }

        //LLama a la controladoraBD de requisiciones para obtener la  lista con los item del grid
        internal List<Item_Grid_Requisiciones> getListaRequisiciones(string bodega,int usuario)
        {
            return controladoraBD.getListaRequisiciones(bodega,usuario);

        }

        //Llama a la controladora de bodega para obtener el id de una bodega
        internal int obtenerIDBodega(string bodega)
        {
            return controladoraB.obtenerIDBodega(bodega);
        }

        //Llama a la controladora de Bodegas para retornar la lista de subbodegas del programa
        internal Dictionary<string, int> getSubBodegas(string programa, string bodega)
        {
            return controladoraB.getSubBodegas(programa, bodega);
        }

        //Llama a la controladora de base de datos de requisiciones para agregar la nueva requisicion
        internal string agregarRequisicion(int usuario,int bodega, string programa, int subbodega,string unidadSolicitante,string destino, string observaciones)
        {
            return controladoraBD.agregarRequisicion(usuario,bodega, programa,subbodega,unidadSolicitante, destino, observaciones);
        }

        //Llama a la controladora de base de datos de requisiciones para obtener las unidades solicitantes en el sistema
        internal List<string> getUnidades() {
            return controladoraBD.getUnidades();
        
        }

        //Llama a la controladora de base de datos de requisiciones para obtener la cantidad del producto en la bodega
        internal int obtenerCantidadProductoBodega(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraBD.obtenerCantidadProductoBodega(bodega, subbodega, programa, producto);
        }

        //Llama a la controladora de base de datos de requisiciones para obtener la cantidad de las muestras de empaque por producto
        internal List<int> obtenerCantPorEmpaque(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraBD.obtenerCantPorEmpaque(bodega, subbodega, programa, producto);
        }

        //Llama a la controladora de base de datos de requisiciones para obtener las muestras de empaque por producto
        internal List<int> obtenerEmpaque(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraBD.obtenerEmpaque(bodega, subbodega, programa, producto);
        }

        //Llama a la controladora de base de datos de requisiciones para guardar un producto en una requisicion
        internal void agregarProducto(string producto, string numRequisicion, int cantidad)
        {
            controladoraBD.agregarProducto(producto, numRequisicion, cantidad);

        }

        //Llama a la controladora de base de datos de requisiciones para guardar un producto en una requisicion
        internal void editarProducto(string producto, string numRequisicion, int cantidad)
        {
            controladoraBD.editarProducto(producto, numRequisicion, cantidad);

        }
        //Llama a a la controladora de base de datos para obtener los productos de la bodega
        internal List<Item_Grid_Productos_Bodega> getListaProductosBodega(int bodega,int subbodega,string programa,string busqueda){
            return controladoraBD.getListaProductosBodega(bodega,subbodega,programa,busqueda);
        }
        //Llama a la controladora de base de datos para obtener el estado de la requisicion
        internal string getEstadoRequisicion(string numRequisicion){
            return controladoraBD.getEstadoRequisicion(numRequisicion);

        }
        //Llama a la controladora de base de datos para eliminar un producto de una requisicion
        internal void eliminarProducto(string numRequisicion,string producto)
        {
            controladoraBD.eliminarProducto(numRequisicion,producto);

        }
        //Llama a la controladora de base de datos para eliminar la requisicion
        internal void eliminarRequisicion(string numRequisicion)
        {
             controladoraBD.eliminarRequisicion(numRequisicion);

        }
        //Llama a la controladora de base de datos para obtener los datos de la requisicion
        internal List<string> getDatosRequisicion(string numRequisicion)
        {
            return controladoraBD.getDatosRequisicion(numRequisicion);
        }
        //Llama a la controladora de base de datos para enviar la requisicion a aprobacion
        internal void enviarAAprobacion(string numRequisicion)
        {
            controladoraBD.enviarAAprobacion(numRequisicion);

        }
        //Llama a la controladora de base de datos para obtener la lista de productos de la requisicion
        internal List<Item_Grid_Productos_Bodega> getListaProductosRequisicion(string numRequisicion)
        {
            return controladoraBD.getListaProductosRequisicion(numRequisicion);
        }
        //Llama a la controladora de base de datos para obtener el num de requisicion
        internal int obtenerIDRequisicion(string numRequisicion)
        {
            return controladoraBD.obtenerIDRequisicion(numRequisicion);

        }
        //Llama a la controladora de aprobadores para actualizar la observacion de la requisicion
        internal void actualizarObservacion(int id_req, string observacion) {
            controladoraR.actualizarObservacion(id_req, observacion);
        }
        //Llama a la controladora de base de datos para obtener el seguimiento de la requisicion
        internal List<Item_Grid_Tracking> getTracking(string numRequisicion)
        {
            
            return controladoraBD.getTracking(numRequisicion);
        }

        internal List<String> getRequisicionInfo(string numRequisicion) {
            return controladoraBD.getRequisicionInfo(numRequisicion);
        
        }

    }
}