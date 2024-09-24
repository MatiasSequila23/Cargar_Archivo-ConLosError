using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEGOCIO;
using ENTIDAD;
using System.IO;

namespace Cargar_Archivo
{
    public partial class VerInformes : Form
    {
        public VerInformes()
        {
            InitializeComponent();
        }

        private void VerInformes_Load(object sender, EventArgs e)
        {

            try
            {
                dgvEmpleados.AllowUserToAddRows = false;
                dgvEmpleados.DataSource = negUsuarios.TraerEmpleados();
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                string nombrearchivo = "Empleados " + DateTime.Now.ToString("dd MMMM yyyy ") + ".txt";

                if (File.Exists(Directory.GetCurrentDirectory() + "\\Archivo csv\\" + nombrearchivo))
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\Archivo csv\\" + nombrearchivo);
                }

                List<Usuarios> list = new List<Usuarios>();

                int filas = dgvEmpleados.Rows.Count;

                if (MessageBox.Show("¿Desea guardar el informe?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    for (int i = 0; i < filas; i++)
                    {
                        Usuarios usu = new Usuarios();

                        usu.legajo = (dgvEmpleados.Rows[i].Cells[0].Value).ToString();
                        usu.Nombre = (dgvEmpleados.Rows[i].Cells[1].Value).ToString();
                        usu.Importe = Convert.ToInt32((dgvEmpleados.Rows[i].Cells[2].Value).ToString());
                        usu.Fecha = Convert.ToDateTime((dgvEmpleados.Rows[i].Cells[3].Value).ToString());

                        list.Add(usu);
                    }

                    int a = list.Count;



                    for (int i = 0; i < a; i++)
                    {
                        Usuarios usu = new Usuarios();
                        usu = list[i];

                        Archivos.EscribeArchivoEmpleados(usu.legajo.ToString(), usu.Nombre.ToString(), usu.Importe.ToString(), usu.Fecha.ToString());
                    }

                }
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtLegajo.Text != "")
                {
                    string legajo = txtLegajo.Text;

                    dgvEmpleados.DataSource = negUsuarios.BuscarEmpleado(legajo);
                    if (dgvEmpleados.Rows.Count == 0)
                    {
                        MessageBox.Show("No existe empleado con ese legajo.");
                    }

                }
                else
                {
                    MessageBox.Show("Debe ingresar un numero de legajo.");
                }
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

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            try
            {
                dgvEmpleados.AllowUserToAddRows = false;
                dgvEmpleados.DataSource = negUsuarios.TraerEmpleados();
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
    }
}
