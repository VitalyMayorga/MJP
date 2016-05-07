﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraDevolucionBajas
    {
        ControladoraBDDevolucionBajas controladoraBD;
        ControladoraProgramasPresupuestarios controladoraPP;
        ControladoraBodegas controladoraB;
        ControladoraProductos controladoraP ;

        public ControladoraDevolucionBajas()
        {
            controladoraBD = new ControladoraBDDevolucionBajas();
            controladoraPP = new ControladoraProgramasPresupuestarios();
            controladoraB = new ControladoraBodegas();
            controladoraP = new ControladoraProductos();
        }

        //Llama a la controladora de Base de datos de bodegas, para obtener las subbodegas de un programa presupuestario en específico y la bodega específica
        internal Dictionary<string, int> getSubBodegas(int idPrograma,int idBodega)
        {
            return controladoraB.getSubBodegas(getNombrePrograma(idPrograma), getNombreBodega(idBodega));
        }

        //Llama a la controladora de Base de datos de bodegas,  para obtener las bodegas en el sistema
        internal Dictionary<string, int> getBodegas()
        {
            return controladoraB.getBodegas();
        }

        //Llama a la controladora de Programas presupuestarios, para obtener los programas presupuestarios en el sistema
        internal Dictionary<string, int> getProgramas()
        {
            return controladoraPP.getProgramas();
        }

        //Llama a la controladora de base de datos de bodegas para obtener el nombre de la bodega dado un id
        internal string getNombreBodega(int id)
        {
            return controladoraB.getNombreBodega(id);
        }

        //Llama a la controladora de base de datos de programas presupuestarios para obtener el nombre de un programa dado un id
        internal string getNombrePrograma(int id)
        {
            return controladoraPP.getNombrePrograma(id);
        }

        //Llama a la controladora de Productos, para obtener el id del producto con cantidad minima segun cierta descripcion
        internal int getProductoConCantidadMin(string descripcion)
        {
            return controladoraP.getProductoConCantidadMin(descripcion);
        }

        //Llama a la controladora de Base de datos de DevolucionBajas, para actualizar el estado de la devolucion
        internal void actualizarEstado(int idDevolucion, int aceptado)
        {
            controladoraBD.actualizarEstado(idDevolucion, aceptado);
        }

        //Llama a la controladora de Base de datos de DevolucionBajas, para actualizar la cantidad de producto
        internal void actualizarCantidadProducto(int idBodega, int idProducto, int idPrograma, int idSubBodega, int cantidad, string tipo)
        {
            controladoraBD.actualizarCantidadProducto(idBodega, idProducto, idPrograma, idSubBodega, cantidad, tipo);
        }

        //Llama a la controladora de Base de datos de DevolucionBajas, para agregar una Baja o Devolucion
        internal void agregarDevolucionBaja(string tipo, int idPrograma, int cantidad, string justificacion, int idBodega, int idProducto, int idSubBodega, string estado)
        {
            controladoraBD.agregarDevolucionBaja(tipo, idPrograma, cantidad, justificacion, idBodega, idProducto, idSubBodega, estado);
        }


    }
}