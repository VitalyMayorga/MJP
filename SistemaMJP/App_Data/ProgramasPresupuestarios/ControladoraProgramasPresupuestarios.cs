﻿using System;
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

        internal Dictionary<string, int> getProgramas()
        {
            return controladoraBD.CargarProgramaPresupuestario();
        }
    }
}