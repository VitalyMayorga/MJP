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

        //Llama a la controladora de Base de datos de bodegas, para obtener las subbodegas de un programa presupuestario en específico y la bodega específica
        internal List<string> getBodegas()
        {
            return controladoraBD.CargarBodegas();
        }

        //Llama a la controladora de Base de datos de bodegas, para obtener todas las bodegas 
        internal void AgregarBodega(string bodega)
        {
            controladoraBD.AgregarBodega(bodega);
        }

        //Llama a la controladora de Base de datos de bodegas, para agregar las 
        internal void AgregarSubBodega(string subBodega)
        {
            controladoraBD.AgregarSubBodega(subBodega);
        }

        //Llama a la controladora de Base de datos de bodegas, para obtener las subbodegas de un programa presupuestario en específico y la bodega específica
        internal void AgregarBodegaSubBodega(int bodega)
        {
            controladoraBD.AgregarBodegaSubBodega(bodega);
        }
    }
}