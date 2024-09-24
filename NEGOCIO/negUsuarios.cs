using DAO;
using System;
using System.Collections.Generic;
using System.Text;
using ENTIDAD;
using System.Data;

namespace NEGOCIO
{
    public class negUsuarios
    {
        public static int BuscarDni(string legajo)
        {
            return daoUsuarios.BuscarDni(legajo);
        }

        public static int ActualizarMonto(string legajo, int pago)
        {
            return daoUsuarios.ModificarMonto(legajo, pago);
        }

        public static int InsertarNuevoUsuario(Usuarios usu)
        {
            return daoUsuarios.InsertarNuevoUsuario(usu);
        }

        public static DataTable TraerEmpleados()
        {
            return daoUsuarios.TraerEmpleados();
        }

        public static DataTable BuscarEmpleado(string legajo)
        {
            return daoUsuarios.BuscarEmpleado(legajo);
        }
        public static int max_connections()
        {
            return daoUsuarios.max_connections();
        }
    }
}
