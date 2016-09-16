using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraRequisicionAprobadores
    {
        ControladoraBDRequisicionAprobadores controladoraBD;
        ControladoraBodegas controladoraB;
        ControladoraUsuarios controladoraU;
        ControladoraProgramasPresupuestarios controladoraP;
        public ControladoraRequisicionAprobadores()
        {
            controladoraBD = new ControladoraBDRequisicionAprobadores();
            controladoraB = new ControladoraBodegas();
            controladoraU = new ControladoraUsuarios();
            controladoraP = new ControladoraProgramasPresupuestarios();
        }

        //Llama a la controladoraBD de requisiciones para obtener la  lista con los item del grid para el aprobador de programa
        internal List<Item_Grid_RequisicionAprobadores> getListaRequisicionAprobador(int subBodega)
        {
            return controladoraBD.getListaRequisicionAprobador(subBodega);
        }

        //Llama a la controladoraBD de requisiciones para obtener la  lista con los item del grid para el aprobador de almacen
        internal List<Item_Grid_RequisicionAprobadores> getListaRequisicionAlmacen(int bodega)
        {
            return controladoraBD.getListaRequisicionAlmacen(bodega);
        }

        //Llama a la controladoraBD para obtener las requisiciones despachadas
        internal List<Item_Grid_RequisicionAprobadores> getListaRequisicionDespachada(int bodega)
        {
            return controladoraBD.getListaRequisicionDespachada(bodega);
        } 

        //Llama a la controladora de Base de datos de requisicion Aprobadores, para devolver el nombre de una unidad solicitante segun su id 
        internal string getNombreUnidad(int id)
        {
            return controladoraBD.getNombreUnidad(id);
        }

        //Llama a la controladora de base de datos de requisicion Aprobadores para obtener el id de una requisicion
        internal int obtenerIDRequisicion(string numR)
        {
            return controladoraBD.obtenerIDRequisicion(numR);
        }

        //Llama a la controladora de base de datos de requisicion Aprobadores para actualizar la observacion de una requisicion en caso de ser necesario
        internal void actualizarObservacion(int idRequisicion, string observacion)
        {
            controladoraBD.actualizarObservacion(idRequisicion, observacion);
        }

        //Llama a la controladora de base de datos de requisicion Aprobadores para obtener el id de la subBodega relacionada con una requisicion
        internal int obtenerIDProgramaRequisicion(int requisicion)
        {
            return controladoraBD.obtenerIDProgramaRequisicion(requisicion);
        }

        //Llama a la controladora de base de datos de requisicion Aprobadores para obtener el id de la bodega relacionada con una requisicion
        internal int obtenerIDBodegaRequisicion(int requisicion)
        {
            return controladoraBD.obtenerIDBodegaRequisicion(requisicion);
        }

        //Llama a la controladora de base de datos de requisicion Aprobadores para obtener el id de la subBodega relacionada con una requisicion
        internal int obtenerIDSubBodegaRequisicion(int requisicion)
        {
            return controladoraBD.obtenerIDSubBodegaRequisicion(requisicion);
        }

        //Llama a la controladora de base de datos para actualizar la informacion de despacho de la requisicion
        internal void actualizarInfoDespacho(int idRequisicion)
        {
            controladoraBD.actualizarInfoDespacho(idRequisicion);
        }

        //Llama a la controladora de base de datos para agregar la informacion de despacho de la requisicion 
        internal void agregarInfoDespacho(int idRequisicion, string placa, string nomConductor, string destinatario)
        {
            controladoraBD.agregarInfoDespacho(idRequisicion, placa, nomConductor, destinatario);
        }

        //Llama a la controladora de programas presupuestarios para obtener el nombre de un programa dado un id
        internal string getNombrePrograma(int id)
        {
            return controladoraP.getNombrePrograma(id);
        }

        //Llama a la controladora de programas presupuestarios para obtener la lista de todos los programas asignados a un usuario del sistema
        internal List<int> getProgramasPorIdUsuario(int idUsuario)
        {
            return controladoraP.getProgramasPorIdUsuario(idUsuario);
        }

        //Llama a la controladora de usuarios, para devolver el nombre de un usuario segun su id 
        internal string getNombreUsuario(int id)
        {
            return controladoraU.getNombreUsuario(id);
        }

        //Llama a la controladora de bodegas para obtener el nombre de la subbodega dado un id
        internal string getNombreSb(int id)
        {
            return controladoraB.getNombreSb(id);
        }

        //Llama a la controladora de bodegas para obtener el nombre de la bodega dado un id
        internal string getNombreBodega(int id)
        {
            return controladoraB.getNombreBodega(id);
        }

        //Llama a la controladora de bodegas para obtener la lista de todas las bodegas asignadas a un usuario del sistema
        internal List<int> getBodegasPorIdUsuario(int idUsuario)
        {
            return controladoraB.getBodegasPorIdUsuario(idUsuario);
        }
    }
}