using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraUsuarios
    {
        ControladoraBDUsuarios controladoraBD;
        private ControladoraProgramasPresupuestarios controladoraPP = new ControladoraProgramasPresupuestarios();
        private ControladoraBodegas controladoraB = new ControladoraBodegas();
        private ControladoraRolesPerfiles controladoraRP = new ControladoraRolesPerfiles();
        public ControladoraUsuarios()
        {
            controladoraBD = new ControladoraBDUsuarios();
        }

        //Llama a la controladora de Base de datos de roles,  para obtener los roles en el sistema
        internal Dictionary<string, int> getRoles()
        {
            return controladoraRP.getRoles();
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
        internal Dictionary<string, int> getSubBodegas(string programa, string bodega)
        {
            return controladoraB.getSubBodegas(programa, bodega);
        }

        //Llama a la controladora de Base de datos de usuarios, para agregar un nuevo usuario al sistema
        internal void agregarUsuario(string nombre, string apellidos, string correo, int idRol)
        {
            controladoraBD.agregarUsuario(nombre, apellidos, correo, idRol);
        }

        //Llama a la controladora de Base de datos de usuarios, para agregar la relacion entre usuarios y bodegas
        internal void agregarUsuarioBodega(int bodega)
        {
            controladoraBD.agregarUsuarioBodega(bodega);
        }

        //Llama a la controladora de Base de datos de usuarios, para agregar la relacion entre usuarios y subBodegas
        internal void agregarUsuarioSubBodega(int subBodega)
        {
            controladoraBD.agregarUsuarioSubBodega(subBodega);
        }
        //Llama a la controladora de Base de datos de usuarios, para agregar la relacion entre usuarios y programas
        internal void agregarUsuarioPrograma(int programa)
        {
            controladoraBD.agregarUsuarioPrograma(programa);
        }

    }
}