using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraDetalles_Producto
    {
        public ControladoraFacturas controladoraF;
        public ControladoraBDDetalles_Producto controladoraBD;
        public ControladoraDetalles_Producto() {
            controladoraF = new ControladoraFacturas();
            controladoraBD = new ControladoraBDDetalles_Producto();
        }
        //Llama a la controladora de facturas para obtener el ID de la factura
        internal int obtenerIDFactura(string numF) {
            return controladoraF.obtenerIDFactura(numF);
        }
        //Llama  a la controladora de base de datos para obtener la lista de productos de una factura
        internal List<Item_Grid_Produtos_Factura> obtenerListaProductos(int id) {
            return controladoraBD.obtenerListaProductos(id);
        }
        //llama a la controladora de base de datos para eliminar el producto seleccionado
        internal void eliminarProducto(int idFactura, int idProducto) {
            controladoraBD.eliminarProducto(idFactura, idProducto);
        }
        //Envia una factura a Aprobación
        internal void enviarAAprobacion(int idFactura) {
            controladoraBD.enviarAAprobacion(idFactura);
        
        }
        //Envia el estado del prodcuto de la factura a modificar a la controladora de base de datos
        internal void cambiarEstadoProducto(int idFactura, int idProducto, string estado) {
            controladoraBD.cambiarEstadoProducto(idFactura, idProducto, estado);
        }

        //Ingresa la fecha de Recpecion definitiva de un producto
        //internal void agregarRecepcionDefinitiva(int idFactura, int idProducto, string fecha)
        //{
        //    controladoraBD.agregarRecepcionDefinitiva(idFactura, idProducto, fecha);
        //}

        internal void aprobarFactura(int id_factura, string fecha) {
            controladoraBD.agregarRecepcionDefinitiva(id_factura, fecha);
        }

        internal void rechazarFactura(int id_factura)
        {
            controladoraBD.rechazarFactura(id_factura);
        }
    }
}