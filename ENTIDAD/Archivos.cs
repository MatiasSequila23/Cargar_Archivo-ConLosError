using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ENTIDAD;

namespace ENTIDAD
{
    public class Archivos
    {
        public static void GenerarCSV(string ruta, string cadena)
        {
            using (StreamWriter mylogs = File.AppendText(ruta))         //se crea el archivo
            {

                mylogs.WriteLine(cadena);
                mylogs.Close();

            }
        }

        public static Logs EscribeArchivoLogs(DateTime fecha, string nombre, string tamaño)
        {
            Logs d = new Logs(fecha, nombre, tamaño);                                                                                  // se instancia el archivo balanceo, y  se envian los parametros. En balanceo archivos se elimina la ultima linea del archivo, asi se reescribe despues.
            Archivos.GenerarCSV(Directory.GetCurrentDirectory() + "\\Logs\\Logs.csv", d.CadenaParaGuardar());                                   // se genera el archivo guardado, con la cadena de caracteres que se va a guradar.

            return d;
        }

        public static EmpleadosCSV EscribeArchivoEmpleados(string legajo, string nombre, string monto, string fecha)
        {
            EmpleadosCSV d = new EmpleadosCSV(legajo, nombre, monto, fecha);                                                                                  // se instancia el archivo balanceo, y  se envian los parametros. En balanceo archivos se elimina la ultima linea del archivo, asi se reescribe despues.
            
            string nombrearchivo = "Empleados " + DateTime.Now.ToString("dd MMMM yyyy ") + ".txt";          

            Archivos.GenerarCSV(Directory.GetCurrentDirectory() + "\\Archivo csv\\"+nombrearchivo, d.CadenaParaGuardar());                                   // se genera el archivo guardado, con la cadena de caracteres que se va a guradar.

            return d;
        }
    }
}
