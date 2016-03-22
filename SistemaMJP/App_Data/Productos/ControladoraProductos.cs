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
        public ControladoraProductos() {
            controladoraPP = new ControladoraProgramasPresupuestarios();
            controladoraB = new ControladoraBodegas();
            controladoraP = new ControladoraProveedores();
            controladoraBD = new ControladoraBDProductos();
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
        internal List<string> getSubBodegas(string programa,string bodega)
        {
            return controladoraB.getSubBodegas(programa,bodega);
        }
        //Llama a la controladora de proveedores para insertar el proveedor
        internal void agregarProveedor(string proveedor, string correo, string telefonos) {
            controladoraP.agregarProveedor(proveedor, correo, telefonos);
        }
    }
}