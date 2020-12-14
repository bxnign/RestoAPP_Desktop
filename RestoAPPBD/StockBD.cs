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
    public class StockBD
    {
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");

        public object DatabaseHelper { get; private set; }
        public int AgregarStock(decimal cantidad, string id_produc)
        {
            conexion.Open();
            // especifico  el nombre del procedimiento y la conexion
            OracleCommand comando = new OracleCommand("PKG_MANT_STOCK.SP_INGRESAR_STOCK", conexion);
            // especifico el tipo de objeto que estoy trayendo de la base de datos
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            // lista de todas las variables que pide por parametro el procedimiento
            // los parametros y los tipos de valores que genero en  Negocio deben ser del mismos nombres
            comando.Parameters.Add("cant", OracleType.Number).Value = cantidad;
            comando.Parameters.Add("id_prod", OracleType.NVarChar).Value = id_produc;
            int resultado = comando.ExecuteNonQuery();
            // se devuelte el resultado que es un int
            // update , insert , delete , retorna el numero de filas
            // drop y create devuelve un 0
            // todo lo demas un -1
            return resultado;
        }
        public int ModificarStock(int id_stock, decimal cantidad, string id_prod)
        {

            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_STOCK.SP_MODIFICAR_STOCK", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("id", OracleType.Int32).Value = id_stock;
            comando.Parameters.Add("CANT", OracleType.Number).Value = cantidad;
            comando.Parameters.Add("ID_PROD", OracleType.NVarChar).Value = id_prod;
            int resultado = comando.ExecuteNonQuery();
            return resultado;

        }
        public DataTable ListarStock()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_STOCK.SP_LISTAR_STOCK", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public int EliminarStock(int id_Stock)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_STOCK.SP_ELIMINAR_STOCK", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("ID", OracleType.Number).Value = id_Stock;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public DataTable ListarPorId(int Id_stock)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_STOCK.SP_LISTAR_STOCK_FILTR ", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("stock_id", OracleType.Number).Value = Id_stock;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
    }
}