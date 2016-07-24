using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraRequisicionesUsuario
    {
        ControladoraBDRequisicionesUsuario controladoraBD;
        public ControladoraRequisicionesUsuario() { 
            controladoraBD = new ControladoraBDRequisicionesUsuario();
        
        }

        //LLama a la controladoraBD de requisiciones para obtener la  lista con los item del grid
        internal List<Item_Grid_Requisiciones> getListaRequisiciones(string bodega,int usuario)
        {
            return controladoraBD.getListaRequisiciones(bodega,usuario);

        }
    }
}