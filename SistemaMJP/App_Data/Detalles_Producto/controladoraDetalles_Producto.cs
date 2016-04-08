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
    }
}