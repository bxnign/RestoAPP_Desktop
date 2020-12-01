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
    public class ProductosBD
    {
        OracleConnection conexion = new OracleConnection("user id=topherapp;password=restoapp;data source=" +
                                                         "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                                         "(HOST=restaurante.c8e27p3hegzq.us-east-1.rds.amazonaws.com)(PORT=1521))(CONNECT_DATA=" +
                                                         "(SERVICE_NAME=DATABASE)))");

        public object DatabaseHelper { get; private set; }
        public int AgregarProductos(string nombre_prod, string distrib, string id_tipo)
        {
            conexion.Open();
            // especifico  el nombre del procedimiento y la conexion
            OracleCommand comando = new OracleCommand("PKG_MANT_PRODUCTOS.SP_INGRESAR_PRODUCTO", conexion);
            // especifico el tipo de objeto que estoy trayendo de la base de datos
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            // lista de todas las variables que pide por parametro el procedimiento
            // los parametros y los tipos de valores que genero en  Negocio deben ser del mismos nombres
            comando.Parameters.Add("NOMBRE", OracleType.NVarChar).Value = nombre_prod;
            comando.Parameters.Add("DISTRIBUCION", OracleType.NVarChar).Value = distrib;
            comando.Parameters.Add("ID_TIPO_ESP", OracleType.NVarChar).Value = id_tipo;

            int resultado = comando.ExecuteNonQuery();
            // se devuelte el resultado que es un int
            // update , insert , delete , retorna el numero de filas
            // drop y create devuelve un 0
            // todo lo demas un -1
            return resultado;
        }
        public int ModificarProductos(int id_prod, string nombre_prod, string distrib, string id_tipo)
        {

            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PRODUCTOS.SP_MODIFICAR_PRODUCTO", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("ID_PROD", OracleType.Int32).Value = id_prod;
            comando.Parameters.Add("NOMBRE", OracleType.NVarChar).Value = nombre_prod;
            comando.Parameters.Add("DISTRIBUCION", OracleType.NVarChar).Value = distrib;
            comando.Parameters.Add("ID_TIPO_ESP", OracleType.NVarChar).Value = id_tipo;
            int resultado = comando.ExecuteNonQuery();
            return resultado;

        }
        public DataTable ListarProductos()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PRODUCTOS.SP_LISTAR_PRODUCTO", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public int EliminarProductos(int id_producto)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PRODUCTOS.SP_ELIMINAR_PRODUCTO", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("id_prod", OracleType.Number).Value = id_producto;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }
        public DataTable ListarProductosPorId(int id_producto)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PRODUCTOS.SP_LISTAR_PRODUCTO_FILTR", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("id_prod", OracleType.NVarChar).Value = id_producto;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

    }
}
