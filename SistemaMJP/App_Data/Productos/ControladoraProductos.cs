using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraProductos
    {
        ControladoraProgramasPresupuestarios controladoraPP;
        ControladoraBodegas controladoraB;
        ControladoraProveedores controladoraP;
        ControladoraBDProductos controladoraBD;
        ControladoraFacturas controladoraF;
        public ControladoraProductos() {
            controladoraPP = new ControladoraProgramasPresupuestarios();
            controladoraB = new ControladoraBodegas();
            controladoraP = new ControladoraProveedores();
            controladoraBD = new ControladoraBDProductos();
            controladoraF = new ControladoraFacturas();
        }
        //Llama a la controladora de programas presupuestarios, para obtener el nombre de los programas en el sistema
        internal Dictionary<string, int> getProgramas()
        {
            return controladoraPP.getProgramas();
        }

        //Llama a la controladoraBD de productos, para obtener el nombre de las subpartidas en el sistema
        internal Dictionary<string, int> getSubPartidas()
        {
            return controladoraBD.getSubPartidas();
        }

        //Llama a la controladora de proveedores, para obtener el nombre de los proveedores en el sistema
        internal Dictionary<string, int> getProveedores()
        {
            return controladoraP.getProveedores();
        }

        //Llama a la controladora de Bodegas para retornar la lista de subbodegas del programa
        internal Dictionary<string, int> getSubBodegas(string programa, string bodega)
        {
            return controladoraB.getSubBodegas(programa,bodega);
        }
        //Llama a la controladora de proveedores para insertar el proveedor
        internal void agregarProveedor(string proveedor, string correo, string telefonos) {
            controladoraP.agregarProveedor(proveedor, correo, telefonos);
        }
        //Llama a la controladora de bodegas para obtener el id de una bodega
        internal int obtenerIDBodega(string bodega) {
            return controladoraB.obtenerIDBodega(bodega);
        }
        //Primero obtiene el id del proveedor,luego llama a la controladora de Facturas para agregar una factura nueva
        internal void agregarFactura(int bodega, string proveedor, int programa,int subbodega,string numF) {
            int idProveedor = controladoraP.obtenerIDProveedor(proveedor);
            controladoraF.agregarFactura(bodega, idProveedor, programa,subbodega,numF);
        
        }
    }
}