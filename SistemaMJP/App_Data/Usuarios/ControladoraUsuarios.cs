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
        private ServicioLogin servicio = new ServicioLogin();
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
            controladoraBD.agregarUsuario(EncodePassword(correo, "12345"), nombre, apellidos, correo, idRol);
        }

        //Llama a la controladora de Base de datos de usuarios, para agregar la relacion entre usuarios y bodegas
        internal void agregarUsuarioBodega(int id, int bodega)
        {
            controladoraBD.agregarUsuarioBodega(id, bodega);
        }

        //Llama a la controladora de Base de datos de usuarios, para agregar la relacion entre usuarios y subBodegas
        internal void agregarUsuarioSubBodega(int id, int subBodega)
        {
            controladoraBD.agregarUsuarioSubBodega(id, subBodega);
        }

        //Llama a la controladora de Base de datos de usuarios, para agregar la relacion entre usuarios y programas
        internal void agregarUsuarioPrograma(int id, int programa)
        {
            controladoraBD.agregarUsuarioPrograma(id, programa);
        }

        //Llama a la controladora de Base de datos de usuarios, para buscar el ultimo usuario que se agrego al sistema
        internal int buscarUltimoUsuario()
        {
            return controladoraBD.buscarMaximo();
        }

        //Llama a una instancia de servicioLogin, para encriptar la contraseña
        internal string EncodePassword(string usuario, string contraseña)
        {
            return servicio.EncodePassword(string.Concat(usuario, contraseña));
        }

        //Llama a la controladora de Base de datos de usuarios, para obtener el id del rol
        internal int ObtenerIdRol(string nombreRol)
        {
            return controladoraBD.ObtenerIdRol(nombreRol);
        }

        //Llama a la controladora de Base de datos de usuarios, para obtener el id del usuario segun el nombre, los apellidos y el rol asignado
        internal int  ObtenerIdUsuarioPorNombreApellidosRol(int idRol, string nombre, string apellidos)
        {
            return controladoraBD. ObtenerIdUsuarioPorNombreApellidosRol(idRol, nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de Usuarios, para eliminar las relaciones entre programas y usuarios
        internal void eliminarUsuarioPrograma(string nombre, string apellidos)
        {
            controladoraBD.eliminarUsuarioPrograma(nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de Usuarios, para eliminar las relaciones entre bodegas y usuarios
        internal void eliminarUsuarioBodega(string nombre, string apellidos)
        {
            controladoraBD.eliminarUsuarioBodega(nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de Usuarios, para eliminar las relaciones entre subBodegas y usuarios
        internal void eliminarUsuarioSubBodega(string nombre, string apellidos)
        {
            controladoraBD.eliminarUsuarioSubBodega(nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de Usuarios, para actualizar el rol del usuario
        internal void actualizarRolUsuario(int id, string nombre, string apellidos)
        {
            controladoraBD.actualizarRolUsuario(id, nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de Usuarios, para llenar la lista de Programas segun un determinado Usuario
        internal Dictionary<string, int> llenarProgramasAsignados(string nombre, string apellidos)
        {
           return controladoraBD.llenarProgramasAsignados(nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de Usuarios, para llenar la lista de Bodegas segun un determinado Usuario
        internal Dictionary<string, int> llenarBodegasAsignadas(string nombre, string apellidos)
        {
            return controladoraBD.llenarBodegasAsignadas(nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de Usuarios, para llenar la lista de SubBodegas segun un determinado Usuario
        internal Dictionary<string, int> llenarSubBodegasAsignadas(string nombre, string apellidos)
        {
            return controladoraBD.llenarSubBodegasAsignadas(nombre, apellidos);
        }

        //Llama a la controladora de Base de datos de bodegas, para obtener las subBodegas segun el id de una Bodega
        internal Dictionary<string, int> getSubBodegasPorBodega(int idBodega)
        {
            return controladoraB.getSubBodegasPorBodega(idBodega);
        }

        //Llama a la controladora de Base de datos de usuarios, para editar la info personal del Usuario
        internal void editarInfoUsuario(string correo, string pass, string nombre, string apellidos)
        {
            controladoraBD.editarInfoUsuario(EncodePassword(correo, pass), nombre, apellidos, correo);
        }
        
        //Llama a la controladora de Base de datos de usuarios, para devolver el nombre de un usuario segun su id 
        internal string getNombreUsuario(int id)
        {
            return controladoraBD.getNombreUsuario(id);
        }

        //Llama a la controladora de Base de datos de usuarios, para devolver el nombre de un usuario segun su id 
        internal int ObtenerIdUsuarioPorCorreo(string correo)
        {
            return controladoraBD.ObtenerIdUsuarioPorCorreo(correo);
        }

    }
}