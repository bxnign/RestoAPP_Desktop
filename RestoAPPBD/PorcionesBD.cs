using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Diagnostics.Eventing.Reader;
using System.Data;

namespace RestoAPPBD
{
    public class PorcionesBD
    {
        OracleConnection conexion = new OracleConnection("user id=topherapp;password=restoapp;data source=" +
                                                         "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                                         "(HOST=restaurante.c8e27p3hegzq.us-east-1.rds.amazonaws.com)(PORT=1521))(CONNECT_DATA=" +
                                                         "(SERVICE_NAME=DATABASE)))");

        public object DatabaseHelper { get; private set; }
        public int AgregarPorciones(decimal cantidad, string id_producto, string nombre, int precio)
        {
            conexion.Open();
            // especifico  el nombre del procedimiento y la conexion
            OracleCommand comando = new OracleCommand("PKG_MANT_PORCIONES.SP_INGRESAR_PORCIONES", conexion);
            // especifico el tipo de objeto que estoy trayendo de la base de datos
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            // lista de todas las variables que pide por parametro el procedimiento
            // los parametros y los tipos de valores que genero en  Negocio deben ser del mismos nombres
            comando.Parameters.Add("CANT", OracleType.Number).Value = cantidad;
            comando.Parameters.Add("ID_PROD", OracleType.NVarChar).Value = id_producto;
            comando.Parameters.Add("NOMBRE", OracleType.NVarChar).Value = nombre;
            comando.Parameters.Add("PRECIO", OracleType.Number).Value = precio;

            int resultado = comando.ExecuteNonQuery();
            // se devuelte el resultado que es un int
            // update , insert , delete , retorna el numero de filas
            // drop y create devuelve un 0
            // todo lo demas un -1
            return resultado;
        }
        public int ModificarPorciones(int id_porcion, decimal cantidad, string id_producto, string nombre, int precio)
        {

            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PORCIONES.SP_MODIFICAR_PORCIONES", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("id", OracleType.Int32).Value = id_porcion;
            comando.Parameters.Add("CANT", OracleType.Number).Value = cantidad;
            comando.Parameters.Add("ID_PROD", OracleType.NVarChar).Value = id_producto;
            comando.Parameters.Add("NOMBRE", OracleType.NVarChar).Value = nombre;
            comando.Parameters.Add("PRECIO", OracleType.Number).Value = precio;

            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }
        public DataTable ListarPorciones()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PORCIONES.SP_LISTAR_PORCIONES", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public int EliminarPorciones(int id_porcion)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PORCIONES.SP_ELIMINAR_PORCIONES", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("id", OracleType.Number).Value = id_porcion;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }
        public DataTable ListarPorcionesPorId(int id_porcion)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PORCIONES.SP_LISTAR_PORCION_FILTR", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("ID_POR", OracleType.NVarChar).Value = id_porcion;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
    }
}
