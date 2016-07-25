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
        public ControladoraRequisicionesUsuario() { 
            controladoraBD = new ControladoraBDRequisicionesUsuario();
            controladoraB = new ControladoraBodegas();
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
    }
}