using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraBodegas
    {
        ControladoraBDBodegas controladoraBD;
        ControladoraProgramasPresupuestarios controladoraPP;
        public ControladoraBodegas() {
            controladoraBD = new ControladoraBDBodegas();
            controladoraPP = new ControladoraProgramasPresupuestarios();

        }
        //Llama a la controladora de Base de datos de bodegas, para obtener las subbodegas de un programa presupuestario en específico y la bodega específica
        internal Dictionary<string, int> getSubBodegas(string programa, string bodega)
        {
            return controladoraBD.getSubBodegas(programa,bodega);
        }

        //Llama a la controladora de Base de datos de bodegas,  para obtener las bodegas en el sistema
        internal Dictionary<string, int> getBodegas()
        {
            return controladoraBD.cargarBodegas();
        }

        //Llama a la controladora de Programas presupuestarios, para obtener los programas presupuestarios en el sistema
        internal Dictionary<string, int> getProgramas()
        {
            return controladoraPP.getProgramas();
        }

        //Llama a la controladora de Base de datos de bodegas, para agregar nuevas bodegas al sistema 
        internal void AgregarBodega(string prefijo,string bodega)
        {
            controladoraBD.agregarBodega(prefijo,bodega);
        }

        //Llama a la controladora de Base de datos de bodegas, para agregar nuevas subBodegas al sistema
        internal void AgregarSubBodega(string subBodega, int idPrograma)
        {
            controladoraBD.agregarSubBodega(subBodega, idPrograma);
        }

        //Llama a la controladora de Base de datos de bodegas, para agregar la relacion entre bodegas y subBodegas
        internal void AgregarBodegaSubBodega(int bodega)
        {
            controladoraBD.agregarBodegaSubBodega(bodega);
        }
        //Llama a la controladora de base de datos de bodegas para obtener el id de la bodega
        internal int obtenerIDBodega(string bodega) {
            return controladoraBD.obtenerIDBodega(bodega);
        }
        //Llama a la controladora de base de datos de bodegas para obtener el nombre de la subbodega dado un id
        internal string getNombreSb(int id) {
            return controladoraBD.getNombreSb(id);
        }
    }
}