using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraProveedores
    {
        ControladoraBDProveedores controladoraBD;
        public ControladoraProveedores() {
            controladoraBD = new ControladoraBDProveedores();
        }
        //llama a la controladora de base de datos para agregar un nuevo proveedor al sistema
        internal void agregarProveedor(string proveedor, string correo, string telefonos) {
            controladoraBD.agregarProveedor(proveedor, correo, telefonos);
        }
        //Obtengo los proveedores
        internal Dictionary<string, int> getProveedores() {
            return controladoraBD.getProveedores();
        }
        //llama a la controladora de base de datos de proveedor para obtener el id del proveedor
        internal int obtenerIDProveedor(string proveedor) {
            return controladoraBD.obtenerId(proveedor);
        }
    }
}