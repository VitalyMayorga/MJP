using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraProductos
    {
        ControladoraProgramasPresupuestarios controladoraPP;
        ControladoraBodegas controladoraB;
        public ControladoraProductos() {
            controladoraPP = new ControladoraProgramasPresupuestarios();
            controladoraB = new ControladoraBodegas();
        }
        //Llama a la controladora de programas presupuestarios, para obtener el nombre de los programas en el sistema
        internal Dictionary<string, int> getProgramas()
        {
            return controladoraPP.getProgramas();
        }

        //Llama a la controladora de Bodegas para retornar la lista de subbodegas del programa
        internal List<string> getSubBodegas(string programa,string bodega)
        {
            return controladoraB.getSubBodegas(programa,bodega);
        }
    }
}