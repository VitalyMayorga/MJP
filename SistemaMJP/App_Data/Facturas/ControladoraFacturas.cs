using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraFacturas
    {
        ControladoraBDFacturas controladoraBD;
        public ControladoraFacturas() {
            controladoraBD = new ControladoraBDFacturas();
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
    }
}