using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraRolesPerfiles
    {
        ControladoraBDRolesPerfiles controladoraBD;
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
    }
}