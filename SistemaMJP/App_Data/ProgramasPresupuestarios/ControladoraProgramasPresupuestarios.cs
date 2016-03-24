using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMJP
{
    public class ControladoraProgramasPresupuestarios
    {
        ControladoraBDProgramasPresupeustarios controladoraBD;
        public ControladoraProgramasPresupuestarios() {
            controladoraBD = new ControladoraBDProgramasPresupeustarios();
        }
        //LLama a la controladora de base de datos para obtener los programas presupuestarios
        internal Dictionary<string, int> getProgramas()
        {
            return controladoraBD.CargarProgramaPresupuestario();
        }
    }
}