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

        internal void agregarProveedor(string proveedor, string correo, string telefonos) {
            controladoraBD.agregarProveedor(proveedor, correo, telefonos);
        }
        //Obtengo los proveedores
        internal Dictionary<string, int> getProveedores() {
            return controladoraBD.getProveedores();
        }
    }
}