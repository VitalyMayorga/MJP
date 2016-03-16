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

        internal List<Item_Grid_Facturas> getListaFacturas(string bodega)
        {
            return controladoraBD.getListaFacturas(bodega);

        }
    }
}