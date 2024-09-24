using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTIDAD;
using NEGOCIO;
using CSharpLib;


namespace Cargar_Archivo
{
    public partial class Principal : Form
    {
        // variables matias

        int denominacion1 = 0;
        int denominacion2 = 0;
        int CantD2Finall = 0;
        int ContadorF = 0;
        int contador = 0;
        int AB = 80;
        int CantD1Final = 0;
        int CantD2Final = 0;
        int Total = 0;
        int CantD1Inicio = 0;
        int aux = 0;
        int CantD2Inicio = 0;
        int difd1d2 = 0;
        int montoentrega = 0;
        // variables matias


        int importe_total = 0;
        int importe_apagar = 0;
        int cant_gav_1 = 0;
        int cant_gav_2 = 0;

        List<Billetes> lista = new List<Billetes>();

        public Principal()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            double porcentaje = 0;

            try
            {

                int actualizados = 0;                                                                                                                           // declaramos una variable int actualizados en 0
                int agregados = 0;                                                                                                                              // declaramos una variable int agregados en 0

                if (cmbGaveta1.Text == "" && cmbGaveta2.Text == "")
                {
                    MessageBox.Show("Debe ingresar la denominacion de las gavetas.");
                }
                else
                {
                    if (cmbGaveta1.Text == "" || cmbGaveta2.Text == "")
                    {

                        btnCargar.Enabled = false;                                                                                                                      //deshabilitamos el boton para que no se pueda volver a cargar el archivo. Se debe cerrar y volver a abrir el programa.
                        lblError.Text = "";                                                                                                                             // apenas se apreta el boton de cargar, mostramos en el label que esta actualizando la base de datos. Cuando termine de cargarlos a la bd, avisara por el mismo label.
                        lblError2.Text = "";
                        lblError.ForeColor = Color.DimGray;                                                                                                             // seteamos en el label de error el color de la letra.
                        lblError2.ForeColor = Color.DimGray;
                        lblError.Text = "Actualizando...";                                                                                                              // apenas se apreta el boton de cargar, mostramos en el label que esta actualizando la base de datos. Cuando termine de cargarlos a la bd, avisara por el mismo label.
                        lblError2.Text = "Por favor espere.";


                        Usuarios usu = new Usuarios();                                                                                                                  // declaramos un objeto usuarios usu.
                        int Con = negUsuarios.max_connections();

                        string[] archivo = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv");                                           // con el streamreader abrimos el archivo que vamos a leer. Guardamos las lineas en el vector de string
                        string nom = Path.GetFileName(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv");                                                  // declaramos string nom y guardamos el nombre del archivo. para guardarlo luego en el archivo logs.


                        FileInfo info = new FileInfo(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv");                                                   // en fileInfo info guardo la direccion del archivo para obtener informacion

                        string tam = (info.FormatBytes()).ToString();                                                                                                   // en tam, guardamos el peso del archivo con info.formatbytes. cuando los bytes pasan los 1024, nos lo convierte a kb, y asi sucesivamente..
                        Archivos.EscribeArchivoLogs(DateTime.Now, nom, tam);                                                                                            // vamos a escribir el archivo logs enviando la fecha, nombre y tamaño.

                        string nombrearchivo = string.Format("archivo_{0:yyyyMMdd HHmmss}.old", DateTime.Now);                                                          // en nombre archivo, guardamos el nombre del archivo y le agregamos la fecha, hora y la extension .old

                        string path = Directory.GetCurrentDirectory() + "\\BackUp\\" + nombrearchivo;                                                                   // guardamos en path la direccion donde se va a guardar el backup y el nombre del archivo que vamos a crear como archivo.old

                        File.Copy(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv", path);                                                                // copiamos el archivo.csv que actualizamos en la bd como archivo.old en la carpeta backup, y vamos teniendo registro de todos los archivos que van actualizando.


                        if (cmbGaveta1.Text != "")
                        {
                            denominacion1 = Convert.ToInt32(cmbGaveta1.SelectedItem);
                        }
                        else
                        {
                            if (cmbGaveta2.Text != "")
                            {
                                denominacion1 = Convert.ToInt32(cmbGaveta2.SelectedItem);
                            }
                        }

                        denominacion2 = 0;
                        float Tota = archivo.Length;
                        float por = Tota / 100;
                        float var = 0;
                        int con = 0;
                        pbrTrabajo.Visible = true;
                        for (int i = 1; i < archivo.Length; i++)
                        {

                            con = con + 1;
                            if (archivo[i] != null)
                            {
                                if (var <= Total)
                                {

                                }
                                string[] l = archivo[i].Split(';');

                                usu.Nombre = l[1];                                                                                                                                                                                                                                             // guardamos en usu.Cuil lo que esta en el vector l en la posicion 3. (cuarta palabra separada por ; de la linea del archivo).                                                                                                                                                                                                    // declaramos una variable int cen y guardamos convertido a int lo que esta en l en la posici                                                                                                                       // guardamos en usu.Centro lo que esta en cen, convertido a string y le agregamos los 0 necesarios para que tenga 3 caracteres.
                                usu.legajo = l[0];                                                                                                                      // guardamos en usu.legajo que esta en el vector l en la posicion 6. (septima palabra separada por ; de la linea del archivo).


                                string numero = l[2];

                                int car = numero.Length;

                                string monto = numero.Substring(0, (car - 4));

                                if(monto == " $ ")
                                {
                                    monto = " $ 0";
                                }

                                car = monto.Length;

                                numero = monto.Substring(3, car - 3);

                                string total = numero.Replace(".", "");

                                usu.Importe = int.Parse(total);

                                importe_total = importe_total + usu.Importe;

                                int dispUsuario = usu.Importe - (usu.Importe % denominacion1);

                                int fila = negUsuarios.BuscarDni(usu.legajo);                                                                                              // declaramos una variable int fila, llamamos a la funcion buscar dni y enviamos dni como paramero. Si encuentra un dni devolvera 1 en la fila,sino devueve 0.

                                if (fila == 1)                                                                                                                          // nos preguntamos si fila es igual a uno.
                                {
                                    int a = negUsuarios.ActualizarMonto(usu.legajo, usu.Importe);                                                                          // si es igual a 1, declaramos una variable int a y llamamos a actualizar monto enviando dni e importe. Si encontro dni, va a devolver en a 1, sino devuelve 0.
                                    if (a == 1)                                                                                                                          // preguntamos si a es igual a 1.
                                    {
                                        actualizados++;                                                                                                                 // si es igual, agrega uno a actualiados.
                                    }
                                }
                                else                                                                                                                                    // si fila es distinto de uno
                                {
                                    int a = negUsuarios.InsertarNuevoUsuario(usu);                                                                                      // declaramos una variable int a y llamamos a la funcion instarnuevousuario y mandamos el objeto usuario como parametro. Si lo pudo guardar, devuelve uno en a sino devuelve 0
                                    if (a == 1)                                                                                                                          // preguntamos si a es igual a 1
                                    {
                                        agregados++;                                                                                                                    // si es igual a 1, agrega uno a agregado.
                                    }
                                }

                                cant_gav_1 = (dispUsuario / denominacion1) + cant_gav_1;
                                cant_gav_2 = 0;

                            }
                            importe_apagar = (cant_gav_1 * denominacion1);



                            pbrTrabajo.Maximum = archivo.Length;
                            if (pbrTrabajo.Value < archivo.Length)
                            {
                                pbrTrabajo.Value++;
                            }

                        }
                        pbrTrabajo.Visible = false;
                    }
                    else
                    {
                        btnCargar.Enabled = false;                                                                                                                      //deshabilitamos el boton para que no se pueda volver a cargar el archivo. Se debe cerrar y volver a abrir el programa.
                        lblError.Text = "";                                                                                                                             // apenas se apreta el boton de cargar, mostramos en el label que esta actualizando la base de datos. Cuando termine de cargarlos a la bd, avisara por el mismo label.
                        lblError2.Text = "";
                        lblError.ForeColor = Color.DimGray;                                                                                                             // seteamos en el label de error el color de la letra.
                        lblError2.ForeColor = Color.DimGray;
                        lblError.Text = "Actualizando...";                                                                                                              // apenas se apreta el boton de cargar, mostramos en el label que esta actualizando la base de datos. Cuando termine de cargarlos a la bd, avisara por el mismo label.
                        lblError2.Text = "Por favor espere.";

                        int fila1 = negUsuarios.max_connections();
                        Usuarios usu = new Usuarios();                                                                                                                  // declaramos un objeto usuarios usu.


                        string[] archivo = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv");                                           // con el streamreader abrimos el archivo que vamos a leer. Guardamos las lineas en el vector de string
                        string nom = Path.GetFileName(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv");                                                  // declaramos string nom y guardamos el nombre del archivo. para guardarlo luego en el archivo logs.


                        FileInfo info = new FileInfo(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv");                                                   // en fileInfo info guardo la direccion del archivo para obtener informacion

                        string tam = (info.FormatBytes()).ToString();                                                                                                   // en tam, guardamos el peso del archivo con info.formatbytes. cuando los bytes pasan los 1024, nos lo convierte a kb, y asi sucesivamente..
                        Archivos.EscribeArchivoLogs(DateTime.Now, nom, tam);                                                                                            // vamos a escribir el archivo logs enviando la fecha, nombre y tamaño.

                        string nombrearchivo = string.Format("archivo_{0:yyyyMMdd HHmmss}.old", DateTime.Now);                                                          // en nombre archivo, guardamos el nombre del archivo y le agregamos la fecha, hora y la extension .old

                        string path = Directory.GetCurrentDirectory() + "\\BackUp\\" + nombrearchivo;                                                                   // guardamos en path la direccion donde se va a guardar el backup y el nombre del archivo que vamos a crear como archivo.old

                        File.Copy(Directory.GetCurrentDirectory() + "\\Archivo csv\\archivo.csv", path);                                                                // copiamos el archivo.csv que actualizamos en la bd como archivo.old en la carpeta backup, y vamos teniendo registro de todos los archivos que van actualizando.


                        pbrTrabajo.Visible = true;
                        for (int i = 1; i < archivo.Length; i++)
                        {                                                                                                                                  // iniciamos un ciclo for por la cantidad de lineas que tiene el archivo. Iniciamos en 1 asi no lee la primer linea del encabezado.

                            if (archivo[i] != null)
                            {
                                string[] l = archivo[i].Split(';');

                                usu.Nombre = l[1];                                                                                                                                                                                                                                             // guardamos en usu.Cuil lo que esta en el vector l en la posicion 3. (cuarta palabra separada por ; de la linea del archivo).                                                                                                                                                                                                    // declaramos una variable int cen y guardamos convertido a int lo que esta en l en la posici                                                                                                                       // guardamos en usu.Centro lo que esta en cen, convertido a string y le agregamos los 0 necesarios para que tenga 3 caracteres.
                                usu.legajo = l[0];                                                                                                                      // guardamos en usu.legajo que esta en el vector l en la posicion 6. (septima palabra separada por ; de la linea del archivo).


                                string numero = l[2];

                                int car = numero.Length;

                                string monto = numero.Substring(0, (car - 4));

                                if (monto == " $ ")
                                {
                                    monto = " $ 0";
                                }

                                car = monto.Length;

                                numero = monto.Substring(3, car - 3);

                                string total = numero.Replace(".", "");


                                usu.Importe = int.Parse(total);

                                importe_total = importe_total + usu.Importe;


                                denominacion1 = Convert.ToInt32(cmbGaveta1.SelectedItem);
                                denominacion2 = Convert.ToInt32(cmbGaveta2.SelectedItem);


                                int fila = negUsuarios.BuscarDni(usu.legajo);                                                                                              // declaramos una variable int fila, llamamos a la funcion buscar dni y enviamos dni como paramero. Si encuentra un dni devolvera 1 en la fila,sino devueve 0.

                                if (fila == 1)                                                                                                                          // nos preguntamos si fila es igual a uno.
                                {
                                    int a = negUsuarios.ActualizarMonto(usu.legajo, usu.Importe);                                                                          // si es igual a 1, declaramos una variable int a y llamamos a actualizar monto enviando dni e importe. Si encontro dni, va a devolver en a 1, sino devuelve 0.
                                    if (a == 1)                                                                                                                          // preguntamos si a es igual a 1.
                                    {
                                        actualizados++;                                                                                                                 // si es igual, agrega uno a actualiados.
                                    }
                                }
                                else                                                                                                                                    // si fila es distinto de uno
                                {
                                    int a = negUsuarios.InsertarNuevoUsuario(usu);                                                                                      // declaramos una variable int a y llamamos a la funcion instarnuevousuario y mandamos el objeto usuario como parametro. Si lo pudo guardar, devuelve uno en a sino devuelve 0
                                    if (a == 1)                                                                                                                          // preguntamos si a es igual a 1
                                    {
                                        agregados++;                                                                                                                    // si es igual a 1, agrega uno a agregado.
                                    }
                                }


                                if (denominacion1 == 500 & denominacion2 == 200)
                                {
                                    int cambio = denominacion1;
                                    denominacion1 = denominacion2;
                                    denominacion2 = cambio;
                                    CantD2Finall = 0;
                                    ContadorF = 0;
                                    contador = 0;
                                    AB = 99999;
                                    CantD1Final = 0;
                                    CantD2Final = 0;
                                    Total = Convert.ToInt32(usu.Importe);

                                    CantD2Inicio = aux / denominacion2;
                                    difd1d2 = denominacion1 / denominacion2;
                                    int resto = Total % denominacion1;
                                    if (resto != 0)
                                    {
                                        CantD2Inicio = 1;
                                        Total = Total - 500;
                                    }
                                    CantD1Inicio = Total / denominacion1;
                                    CantD2Final = CantD2Inicio;
                                    for (int a = CantD1Inicio; a >= 5; a = a - 5)
                                    {

                                        int auxiliar = Math.Abs(a - CantD2Final);
                                        if (AB > auxiliar)
                                        {
                                            AB = auxiliar;
                                            CantD1Final = a;
                                            CantD2Finall = (2 * contador) + CantD2Inicio;
                                            ContadorF = contador;

                                        }
                                        contador++;
                                        CantD2Final = (contador * 2) + CantD2Inicio;
                                    }
                                    montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;

                                    cant_gav_1 = CantD1Final + cant_gav_1;
                                    cant_gav_2 = CantD2Finall + cant_gav_2;
                                    importe_apagar = importe_apagar + montoentrega;
                                }
                                else
                                {
                                    if (denominacion1 == 200 & denominacion2 == 500)
                                    {
                                        CantD2Finall = 0;
                                        ContadorF = 0;
                                        contador = 0;
                                        AB = 99999;
                                        CantD1Final = 0;
                                        CantD2Final = 0;
                                        Total = Convert.ToInt32(usu.Importe);

                                        CantD2Inicio = aux / denominacion2;
                                        difd1d2 = denominacion1 / denominacion2;
                                        int resto = Total % denominacion1;
                                        if (resto != 0)
                                        {
                                            CantD2Inicio = 1;
                                            Total = Total - 500;
                                        }
                                        CantD1Inicio = Total / denominacion1;
                                        CantD2Final = CantD2Inicio;
                                        for (int a = CantD1Inicio; a >= 5; a = a - 5)
                                        {

                                            int auxiliar = Math.Abs(a - CantD2Final);
                                            if (AB > auxiliar)
                                            {
                                                AB = auxiliar;
                                                CantD1Final = a;
                                                CantD2Finall = (2 * contador) + CantD2Inicio;
                                                ContadorF = contador;

                                            }
                                            contador++;
                                            CantD2Final = (contador * 2) + CantD2Inicio;
                                        }
                                        montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;

                                        cant_gav_1 = CantD1Final + cant_gav_1;
                                        cant_gav_2 = CantD2Finall + cant_gav_2;
                                        importe_apagar = importe_apagar + montoentrega;
                                    }
                                    else
                                    {
                                        if (denominacion1 < denominacion2)
                                        {
                                            int aux = denominacion1;
                                            denominacion1 = denominacion2;
                                            denominacion2 = aux;


                                            CantD2Finall = 0;
                                            ContadorF = 0;
                                            contador = 0;
                                            AB = 99999;
                                            CantD1Final = 0;
                                            CantD2Final = 0;
                                            Total = Convert.ToInt32(usu.Importe);
                                            CantD1Inicio = Total / denominacion1;
                                            aux = Total % denominacion1;
                                            CantD2Inicio = aux / denominacion2;
                                            difd1d2 = denominacion1 / denominacion2;

                                            for (int a = CantD1Inicio; a >= 0; a--)
                                            {

                                                int auxiliar = Math.Abs(a - CantD2Final);
                                                if (AB > auxiliar)
                                                {
                                                    AB = auxiliar;
                                                    CantD1Final = a;
                                                    CantD2Finall = (contador * difd1d2) + CantD2Inicio;
                                                    ContadorF = contador;

                                                }
                                                contador++;
                                                CantD2Final = (contador * difd1d2) + CantD2Inicio;

                                            }
                                            montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;

                                            cant_gav_1 = CantD1Final + cant_gav_1;
                                            cant_gav_2 = CantD2Finall + cant_gav_2;
                                            importe_apagar = importe_apagar + montoentrega;

                                        }
                                        else
                                        {
                                            CantD2Finall = 0;
                                            ContadorF = 0;
                                            contador = 0;
                                            AB = 99999;
                                            CantD1Final = 0;
                                            CantD2Final = 0;
                                            Total = Convert.ToInt32(usu.Importe);
                                            CantD1Inicio = Total / denominacion1;
                                            aux = Total % denominacion1;
                                            CantD2Inicio = aux / denominacion2;
                                            difd1d2 = denominacion1 / denominacion2;

                                            for (int a = CantD1Inicio; a >= 0; a--)
                                            {

                                                int auxiliar = Math.Abs(a - CantD2Final);
                                                if (AB > auxiliar)
                                                {
                                                    AB = auxiliar;
                                                    CantD1Final = a;
                                                    CantD2Finall = (contador * difd1d2) + CantD2Inicio;
                                                    ContadorF = contador;

                                                }
                                                contador++;
                                                CantD2Final = (contador * difd1d2) + CantD2Inicio;

                                            }
                                            montoentrega = CantD1Final * denominacion1 + CantD2Finall * denominacion2;

                                            cant_gav_1 = CantD1Final + cant_gav_1;
                                            cant_gav_2 = CantD2Finall + cant_gav_2;
                                            importe_apagar = importe_apagar + montoentrega;
                                        }
                                    }

                                }

                            }

                            pbrTrabajo.Maximum = archivo.Length;
                            if (pbrTrabajo.Value < archivo.Length)
                            {
                                pbrTrabajo.Value++;
                            }
                        }
                        pbrTrabajo.Visible = false;
                    }

                    lblBill1.Text = "Cantidad de billetes de $" + denominacion1.ToString() + ":";
                    lblBill2.Text = "Cantidad de billetes de $" + denominacion2.ToString() + ":";

                    txtBill1.Text = cant_gav_1.ToString();
                    txtBill2.Text = cant_gav_2.ToString();
                    txtDisponible.Text = importe_apagar.ToString();
                    txtTotal.Text = importe_total.ToString();

                }

                lblError.ForeColor = Color.Green;
                lblError2.ForeColor = Color.Green;
                lblError.Text = "Se actualizaron " + actualizados + " usuarios";                                                                            // mostramos la cantidad que agrego y que actualizo.
                lblError2.Text = "Se agregaron " + agregados + " usuarios nuevos.";

            }
            catch (Exception ex)
            {

                // Qué ha sucedido
                var mensaje = "Error message: " + ex.Message;
                // Información sobre la excepción interna
                if (ex.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + ex.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + ex.StackTrace;
                LogExeption(mensaje);
                Error frm = new Error();
                frm.ShowDialog();
            }

        }

        public void LogExeption(string MensError)
        {
            string fech = DateTime.Now.ToString("dd MMMM yyyy");
            string hor = DateTime.Now.ToString(" HH mm ss");
            string ruta = Directory.GetCurrentDirectory() + @"\\ReporteError\\Reporte" + fech + hor + ".csv";
            StreamWriter mylogs = File.AppendText(Directory.GetCurrentDirectory() + @"\\Logs\\LogExeption\\Error " + fech + hor + ".csv");
            mylogs.WriteLine(MensError);
            mylogs.Close();
        }

        private void Principal_Load_1(object sender, EventArgs e)
        {
            StreamReader archivo = File.OpenText(Directory.GetCurrentDirectory() + "\\Gav.csv");                     // guardamos en la variable archivo, el archivo de las tablas, para ir leyendolo.                                                 

            string linea = "";                                                                                                  // declaramos una variable linea en vacio. Para leer cada linea del archivo.                                                     
            while (!archivo.EndOfStream)                                                                                        // iniciamos un ciclo while que lea el archivo hasta el final, cuando termina de leer el archivo, termina el while.                                                   
            {

                linea = archivo.ReadLine();                                                                                     // en la variable linea guarda la linea leida.                                                                                                                                  // guardamos la demonominacion en la variable den

                cmbGaveta1.Items.Add(linea);                                                                                         // agregamos den a cada combobox

                cmbGaveta2.Items.Add(linea);


            }

            archivo.Close();

            lblError.Text = "";
            lblError2.Text = "";
            this.FormClosed += new FormClosedEventHandler(cerrarform);

        }
        private void cerrarform(object sender, EventArgs e )
        {
            Application.Restart();
        }

        private void pbrTrabajo_Click(object sender, EventArgs e)
        {

        }
    }
}
