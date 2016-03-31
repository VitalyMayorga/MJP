using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraFacturas
    {
        ControladoraBDFacturas controladoraBD;
        ControladoraBodegas controladoraB;
        public ControladoraFacturas() {
            controladoraBD = new ControladoraBDFacturas();
            controladoraB = new ControladoraBodegas();
        }
        //LLama a la controladora de facturas para obtener la  lista con los item del grid
        internal List<Item_Grid_Facturas> getListaFacturas(string bodega)
        {
            return controladoraBD.getListaFacturas(bodega);

        }
        //Llama a la controladora de base de datos de facturas para agregar la nueva factura
        internal void agregarFactura(int bodega, int proveedor, int programa,int subbodega,string numF) {
            controladoraBD.agregarFactura(bodega, proveedor, programa, subbodega, numF);
        }
        //Metodo que llama la controladora de base de datos de facturas, para obtener el ID de la factura
        internal int obtenerIDFactura(string factura)
        {
           return controladoraBD.obtenerIDFactura(factura);
        }
        //Llama a la controladora de Bodegas para obtener el nombre de la subbodega de la factura.
        internal string getNombreSb(int id) {
            return controladoraB.getNombreSb(id);
        }
    }
}