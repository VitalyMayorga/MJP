using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBodegas
    {
        ControladoraBDBodegas controladoraBD;
        public ControladoraBodegas() {
            controladoraBD = new ControladoraBDBodegas();

        }
        //Llama a la controladora de Base de datos de bodegas, para obtener las subbodegas de un programa presupuestario en específico y la bodega específica
        internal List<string> getSubBodegas(string programa,string bodega) {
            return controladoraBD.getSubBodegas(programa,bodega);
        }
    }
}