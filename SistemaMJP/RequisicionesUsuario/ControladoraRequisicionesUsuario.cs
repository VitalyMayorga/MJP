using SistemaMJP.RequisicionesUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraRequisicionesUsuario
    {
        ControladoraBDRequisicionesUsuario controladoraBD;
        ControladoraBodegas controladoraB;
        public ControladoraRequisicionesUsuario() { 
            controladoraBD = new ControladoraBDRequisicionesUsuario();
            controladoraB = new ControladoraBodegas();
        }

        //LLama a la controladoraBD de requisiciones para obtener la  lista con los item del grid
        internal List<Item_Grid_Requisiciones> getListaRequisiciones(string bodega,int usuario)
        {
            return controladoraBD.getListaRequisiciones(bodega,usuario);

        }
        //Llama a la controladora de bodega para obtener el id de una bodega
        internal int obtenerIDBodega(string bodega)
        {
            return controladoraB.obtenerIDBodega(bodega);
        }

        //Llama a la controladora de Bodegas para retornar la lista de subbodegas del programa
        internal Dictionary<string, int> getSubBodegas(string programa, string bodega)
        {
            return controladoraB.getSubBodegas(programa, bodega);
        }

        //Llama a la controladora de base de datos de requisiciones para agregar la nueva requisicion
        internal string agregarRequisicion(int usuario,int bodega, string programa, int subbodega,string unidadSolicitante,string destino, string observaciones)
        {
            return controladoraBD.agregarRequisicion(usuario,bodega, programa,subbodega,unidadSolicitante, destino, observaciones);
        }
        //Llama a la controladora de base de datos de requisiciones para obtener las unidades solicitantes en el sistema
        internal List<string> getUnidades() {
            return controladoraBD.getUnidades();
        
        }
        //Llama a la controladora de base de datos de requisiciones para obtener la cantidad del producto en la bodega
        internal int obtenerCantidadProductoBodega(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraBD.obtenerCantidadProductoBodega(bodega, subbodega, programa, producto);
        }
        //Llama a la controladora de base de datos de requisiciones para obtener las muestras de empaque por producto
        internal List<int> obtenerCantPorEmpaque(int bodega, int subbodega, string programa, string producto)
        {
            return controladoraBD.obtenerCantPorEmpaque(bodega, subbodega, programa, producto);
        }
        //Llama a la controladora de base de datos de requisiciones para guardar un producto en una requisicion
        internal void agregarProducto(string producto, string numRequisicion, int cantidad)
        {
            controladoraBD.agregarProducto(producto, numRequisicion, cantidad);

        }

        internal List<Item_Grid_Productos_Bodega> getListaProductosBodega(int bodega,int subbodega,string programa,string busqueda){
            return controladoraBD.getListaProductosBodega(bodega,subbodega,programa,busqueda);
        }
    }
}