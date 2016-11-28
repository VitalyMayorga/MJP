using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraDetalles_Producto_Requisicion{
        public ControladoraActivos controladoraA;
        public ControladoraProductos controladoraP;
        public ControladoraRequisicionAprobadores controladoraR;
        public ControladoraBDDetalles_Producto_Requisicion controladoraBD;
        public ControladoraRequisicionesUsuario controladoraRU;

        public ControladoraDetalles_Producto_Requisicion() {
            controladoraA = new ControladoraActivos();
            controladoraP = new ControladoraProductos();
            controladoraR = new ControladoraRequisicionAprobadores();
            controladoraBD = new ControladoraBDDetalles_Producto_Requisicion();
            controladoraRU = new ControladoraRequisicionesUsuario();
        }

        //Llama a la controladora de requisiciones para obtener el ID de la requisicion
        internal int obtenerIDRequisicion(string numR) {
            return controladoraR.obtenerIDRequisicion(numR);
        }

        //Llama a la controladora de requisiciones para actualizar la observacion de la requisicion solicitada
        internal void actualizarObservacion(int idRequisicion, string observacion)
        {
            controladoraR.actualizarObservacion(idRequisicion, observacion);
        }

        //Llama a la controladora de requisiciones para obtener el id de la subBodega relacionada con una requisicion
        internal int obtenerIDProgramaRequisicion(int requisicion)
        {
            return controladoraR.obtenerIDProgramaRequisicion(requisicion);
        }

        //Llama a la controladora de requisiciones para obtener el id de la bodega relacionada con una requisicion
        internal int obtenerIDBodegaRequisicion(int requisicion)
        {
            return controladoraR.obtenerIDBodegaRequisicion(requisicion);
        }

        //Llama a la controladora de requisiciones para obtener el id de la subBodega relacionada con una requisicion
        internal int obtenerIDSubBodegaRequisicion(int requisicion)
        {
            return controladoraR.obtenerIDSubBodegaRequisicion(requisicion);
        }

        //Llama a la controladora de base de datos de requisiciones para actualizar la informacion de despacho de la requisicion
        internal void actualizarInfoDespacho(int idRequisicion, DateTime fecha, string persona)
        {
            controladoraR.actualizarInfoDespacho(idRequisicion, fecha, persona);
        }

        //Llama a la controladora de base de datos de requisiciones para agregar la informacion de despacho de la requisicion 
        internal void agregarInfoDespacho(int idRequisicion, string placa, string nomConductor, string destinatario)
        {
            controladoraR.agregarInfoDespacho(idRequisicion, placa, nomConductor, destinatario);
        }

        //Metodo que llama a la controladora de activos para obtenar la cantidad de activos de un determinado producto asignados a una requisicion
        internal int obtenerCantidadActivos(int idRequisicion, int idProducto)
        {
            return controladoraA.obtenerCantidadActivos(idRequisicion, idProducto);
        }

        //Metodo que llama a la controladora de activos para revisar si un producto es un activo
        internal bool esActivo(int producto)
        {
            return controladoraA.esActivo(producto);
        }

        //Llama a la controladora de base de datos para eliminar el producto solicitado de la requisicion
        internal void eliminarProductoRequisicion(string numR, int idProducto)
        {
            controladoraBD.eliminarProductoRequisicion(obtenerIDRequisicion(numR), idProducto);
        }

        //Llama  a la controladora de base de datos para obtener la lista de productos de una factura
        internal List<Item_Grid_Produtos_Requisicion> obtenerListaProductos(int id) {
            return controladoraBD.obtenerListaProductos(id);
        }

        //Cambia el estado de la requisicion segun la accion especificada
        internal void cambiarEstadoRequisicion(int idRequisicion, int estado) {
            controladoraBD.cambiarEstadoRequisicion(idRequisicion, estado);
        }

        //Llama a la controladora de Base de datos para reducir la cantidad de cada producto asignado a la requisicion despachada
        internal void actualizarCantidadProductosRequisicion(int idBodega, int idProducto, int idPrograma, int idSubBodega, int cantidad)
        {
            controladoraBD.actualizarCantidadProductosRequisicion(idBodega, idProducto, idPrograma, idSubBodega, cantidad);
        }

        //Llama a la controladora de Base de datos para odificar la cantidad de un producto determinado en la requisicion
        internal void modificarCantidadLinea(int idRequisicion, int idProducto, int cantidad)
        {
            controladoraBD.modificarCantidadLinea(idRequisicion, idProducto, cantidad);
        }

        //Llama a la controladora de base de datos de productos para obtener el nombre del producto dado un id
        internal string getNombreProducto(int id)
        {
            return controladoraP.getNombreProducto(id);
        }

        //Llama a la controladora de base de datos de requisiciones usuario para obtener los datos de la requisicion
        internal List<string> getDatosRequisicion(string numRequisicion)
        {
            return controladoraRU.getDatosRequisicion(numRequisicion);
        }

        //Llama a la controladora de base de datos de requisiciones usuario para obtener la cantidad del producto en la bodega
        internal int obtenerCantidadProductoBodega(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraRU.obtenerCantidadProductoBodega(bodega, subbodega, programa, producto);
        }

        //Llama a la controladora de base de datos de requisiciones usuario para obtener las muestras de empaque por producto
        internal List<int> obtenerEmpaque(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraRU.obtenerEmpaque(bodega, subbodega, programa, producto);
        }

        //Llama a la controladora de base de datos de requisiciones usuario para obtener la cantidad de las muestras de empaque por producto
        internal List<int> obtenerCantPorEmpaque(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraRU.obtenerCantPorEmpaque(bodega, subbodega, programa, producto);
        }

        //llama a la controladora de base de datos  para obtener la cantidad de producto en salida
        internal int obtenerCantidadProductoSalida(int bodega, int subbodega, string programa, string producto) {
            return controladoraBD.obtenerCantidadProductoSalida(bodega,subbodega,programa,producto);
        }


        //llama a la controladora de base de datos  para obtener la cantidad de producto en transaccion (aprobado en programa)
        internal int obtenerCantidadTransaccion(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraBD.obtenerCantidadTransaccion(bodega, subbodega, programa, producto);
        }

        internal string obtenerEstadoRequisicion(string numeroReq) {
            return controladoraBD.obtenerEstadoRequisicion(numeroReq);
        }

        internal bool productoNoRebajado(string idProd, string numReq) {
            return controladoraBD.productoNoRebajado(idProd, numReq);
        }
    }
}