﻿using System;
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

        //Llama a la controladora de Base de datos de bodegas,  para obtener las bodegas en el sistema
        internal List<string> getBodegas()
        {
            return controladoraBD.CargarBodegas();
        }

        //Llama a la controladora de Base de datos de bodegas, para obtener los programas presupuestarios en el sistema
        internal List<string> getProgramas()
        {
            return controladoraBD.CargarProgramaPresupuestario();
        }

        //Llama a la controladora de Base de datos de bodegas, para agregar nuevas bodegas al sistema 
        internal void AgregarBodega(string prefijo,string bodega)
        {
            controladoraBD.AgregarBodega(prefijo,bodega);
        }

        //Llama a la controladora de Base de datos de bodegas, para agregar nuevas subBodegas al sistema
        internal void AgregarSubBodega(string subBodega, int idPrograma)
        {
            controladoraBD.AgregarSubBodega(subBodega, idPrograma);
        }

        //Llama a la controladora de Base de datos de bodegas, para agregar la relacion entre bodegas y subBodegas
        internal void AgregarBodegaSubBodega(int bodega)
        {
            controladoraBD.AgregarBodegaSubBodega(bodega);
        }
    }
}