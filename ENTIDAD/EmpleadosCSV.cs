using System;
using System.Collections.Generic;
using System.Text;

namespace ENTIDAD
{
    public class EmpleadosCSV
    {
        public string Fecha;
        public string Nombre;
        public string Legajo;
        public string Monto;

        public EmpleadosCSV(string legajo, string nombre, string monto, string fecha)
        {
            this.Fecha = fecha;
            this.Nombre = nombre;
            this.Legajo = legajo;
            this.Monto = monto;
        }

        public string CadenaParaGuardar()
        {
            return this.Legajo + ";" + this.Nombre + ";" + this.Monto + ";"+ this.Fecha;
        }
    }
}
