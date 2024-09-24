using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using ENTIDAD;
using System.Data;

namespace DAO
{
    public class daoUsuarios
    {
        public static int BuscarDni(string legajo)
        {
            int fila;

            Conexion cn = new Conexion();
            MySqlConnection conexion = cn.conectar();
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT legajo, nombre_apellido , pago FROM usuarios where legajo = '" + legajo + "'", conexion);
            MySqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                fila = 1;
                return fila;
            }
            else
            {
                fila = 0;
                return fila;
            }

        }


        public static int ModificarMonto(string legajo, int pago)                                                                               // metodo para modificar el offset.
        {

            int Filas = 0;
            Conexion cn = new Conexion();
            MySqlConnection conexion = cn.conectar();
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("update usuarios set pago = @pago, fecha = @fecha where legajo = '" + legajo + "'", conexion);

            try
            {
                cmd.Parameters.Add("@pago", MySqlDbType.String).Value = pago;
                cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = DateTime.Now;

                Filas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                
                Filas = 0;
            }
            finally
            {
                cmd.Connection.Close();
            }

            cmd.Connection.Close();

            return Filas;

        }


        public static int InsertarNuevoUsuario(Usuarios usu)
        {
          
            int Filas = 0;
            Conexion cn = new Conexion();
            MySqlConnection conexion = cn.conectar();
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("insert into usuarios (legajo, nombre_apellido, pago, offset, fecha) " +
            "values (@legajo, @nombre_apellido, @pago, @offset, @fecha )", conexion);

            try
            {

                cmd.Parameters.Add("@nombre_apellido", MySqlDbType.String).Value = usu.Nombre;
                cmd.Parameters.Add("@pago", MySqlDbType.Int32).Value = usu.Importe;
                cmd.Parameters.Add("@offset", MySqlDbType.String).Value = "0000";
                cmd.Parameters.Add("@legajo", MySqlDbType.String).Value = usu.legajo;
                cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = DateTime.Now;


                Filas = cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                Filas = 0;
            }
            finally
            {
             
                cmd.Connection.Close();
               
            }

            cmd.Connection.Close();

            return Filas;
        }


        public static DataTable TraerEmpleados()
        {

            Conexion cn = new Conexion();
            string Consulta = "SELECT usuarios.legajo as Legajo,usuarios.nombre_apellido as Nombre,usuarios.pago as Pago,usuarios.fecha as Fecha FROM usuarios";
            MySqlDataAdapter da = new MySqlDataAdapter(Consulta, cn.conectar());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public static DataTable BuscarEmpleado(string legajo)
        {

            Conexion cn = new Conexion();
            string Consulta = "SELECT usuarios.legajo as Legajo,usuarios.nombre_apellido as Nombre,usuarios.pago as Pago,usuarios.fecha as Fecha FROM usuarios where usuarios.legajo = " + legajo;
            MySqlDataAdapter da = new MySqlDataAdapter(Consulta, cn.conectar());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public static int max_connections()
        {
            int fila;
            try
            {

                Conexion cn = new Conexion();
                MySqlConnection conexion = cn.conectar();
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("SET GLOBAL max_connections = 500;", conexion);

                fila = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                fila = 0;
            }
            return fila;
        }
    }
}
