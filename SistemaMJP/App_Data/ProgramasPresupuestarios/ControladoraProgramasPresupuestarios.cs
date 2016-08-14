using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraProgramasPresupuestarios
    {
        ControladoraBDProgramasPresupuestarios controladoraBD;
        public ControladoraProgramasPresupuestarios() {
            controladoraBD = new ControladoraBDProgramasPresupuestarios();
        }
        //LLama a la controladora de base de datos para obtener los programas presupuestarios
        internal Dictionary<string, int> getProgramas()
        {
            return controladoraBD.CargarProgramaPresupuestario();
        }

        //Llama a la controladora de base de datos de programas presupuestarios para obtener el nombre de un programa dado un id
        internal string getNombrePrograma(int id)
        {
            return controladoraBD.getNombrePrograma(id);
        }

        //Llama a la controladora de base de datos de programas presupuestarios para obtener el id del programa
        public int obtenerIDPrograma(string programa)
        {
            return controladoraBD.obtenerIDPrograma(programa);
        }

        //Llama a la controladora de base de datos de bodegas para obtener la lista de todas las subBodegas asignadas a un usuario del sistema
        internal List<int> getProgramasPorIdUsuario(int idUsuario)
        {
            return controladoraBD.getProgramasPorIdUsuario(idUsuario);
        }

    }
}