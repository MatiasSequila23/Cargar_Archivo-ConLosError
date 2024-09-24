using System;
using System.Collections.Generic;
using System.Text;

namespace ENTIDAD
{
    public class Logs
    {
        public DateTime Fecha;
        public string NombreArchivo;
        public string TamañoArchivo;

        public Logs(DateTime fecha, string nombre, string tamaño)
        {
            this.Fecha = fecha;
            this.NombreArchivo = nombre;
            this.TamañoArchivo = tamaño;
        }

        public string CadenaParaGuardar()
        {
            return this.Fecha + "|" + this.NombreArchivo + "|" + this.TamañoArchivo;
        }
        
        

    }
}
