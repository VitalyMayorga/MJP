using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
namespace SistemaMJP
{
    public class ControladoraProductos
    {
        ControladoraProgramasPresupuestarios controladoraPP;
        ControladoraBodegas controladoraB;
        ControladoraProveedores controladoraP;
        ControladoraBDProductos controladoraBD;
        ControladoraFacturas controladoraF;
        ControladoraActivos controladoraA;
        public ControladoraProductos() {
            controladoraPP = new ControladoraProgramasPresupuestarios();
            controladoraB = new ControladoraBodegas();
            controladoraP = new ControladoraProveedores();
            controladoraBD = new ControladoraBDProductos();
            controladoraF = new ControladoraFacturas();
            controladoraA = new ControladoraActivos();
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
        //Recibe un objeto de datos para encapsularlo y enviar a agregar a la controladora de base de datos de productos
        internal bool agregarProducto(object[] producto) {
            EntidadProductos productoEncapsulado = new EntidadProductos(producto);

           return controladoraBD.agregarProducto(productoEncapsulado);
        }
        //Metodo que se encarga primero de obtener el id del producto en la base de datos para luego guardarlo en la tabla informacion producto
        internal void agregarProductoABodega(int bodega, string descripcion, int cantEmpaque, int programa, int subBodega, int cantidad, Nullable<DateTime> fechaG, Nullable<DateTime> fechaC, Nullable<DateTime> fechaV)
        {
            int idProducto = controladoraBD.obtenerIDProducto(descripcion, cantEmpaque);
            controladoraBD.agregarProductoABodega(bodega, idProducto, programa, subBodega, cantidad, fechaG, fechaC, fechaV);
        }
        //Metodo que llama la controladora de facturas, para obtener el ID de la factura
        internal int obtenerIDFactura(string factura) {
           return controladoraF.obtenerIDFactura(factura);
        }

        //Metodo que llama a la controladora de base de datos para guardar un producto de una factura
        internal void agregarProductoFactura(int id_factura, string descripcion, int cantidadEmpaque, int cantidad, decimal total, Nullable<DateTime> fechaC, Nullable<DateTime> fechaG, Nullable<DateTime> fechaV) {
            int idProducto = controladoraBD.obtenerIDProducto(descripcion, cantidadEmpaque);
            controladoraBD.agregarProductoFactura(id_factura,idProducto,cantidad,total,fechaC,fechaG,fechaV);
        }
        //Metodo que llama ala controladora de activos para agregar el activo nuevo
        internal void agregarActivo(string numActivo, string funcionario, string cedula,string descripcion,int cantidadEmpaque) {
            int idProducto = controladoraBD.obtenerIDProducto(descripcion, cantidadEmpaque);
            controladoraA.agregarActivo(numActivo, funcionario, cedula,idProducto);
        }

        [WebMethod]
        public static List<string> getProductos(string prefix) {
            return ControladoraBDProductos.getProductos(prefix);
        }
    }
}