using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraRolesPerfiles
    {
        ControladoraBDRolesPerfiles controladoraBD;
        private ControladoraProgramasPresupuestarios controladoraPP = new ControladoraProgramasPresupuestarios();
        private ControladoraBodegas controladoraB = new ControladoraBodegas();
        public ControladoraRolesPerfiles()
        {
            controladoraBD = new ControladoraBDRolesPerfiles();
        }

        internal List<Item_Grid_Usuarios> getListaUsuarios()
        {
            return controladoraBD.getListaUsuarios();

        }

        //Llama a la controladora de Base de datos de roles,  para obtener los roles en el sistema
        internal Dictionary<string, int> getRoles()
        {
            return controladoraBD.cargarRoles();
        }

        //Llama a la controladora de Programas presupuestarios, para obtener los programas presupuestarios en el sistema
        internal Dictionary<string, int> getProgramas()
        {
            return controladoraPP.getProgramas();
        }

        //Llama a la controladora de Bodegas, para obtener las Bodegas que estan en el sistema
        internal Dictionary<string, int> getBodegas()
        {
            return controladoraB.getBodegas();
        }
        //Llama a la controladora de Base de datos de bodegas, para obtener las subbodegas de un programa presupuestario en específico y la bodega específica
        internal Dictionary<string,int> getSubBodegas(string programa, string bodega)
        {
            return controladoraB.getSubBodegas(programa, bodega);
        }

    }
}