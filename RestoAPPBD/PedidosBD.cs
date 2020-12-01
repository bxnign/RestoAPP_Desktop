using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPPBD
{
    public class PedidosBD
    {
        public object DatabaseHelper { get; private set; }
        OracleConnection conexion = new OracleConnection("user id=topherapp;password=restoapp;data source=" +
                                                          "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                                          "(HOST=restaurante.c8e27p3hegzq.us-east-1.rds.amazonaws.com)(PORT=1521))(CONNECT_DATA=" +
                                                          "(SERVICE_NAME=DATABASE)))");


        public DataTable ListarPedidos()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_LISTAR_PEDIDOS", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;

        }


        public int ActualizarEstado(Int64 id, int estado)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_MODIFICAR_PEDIDO", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("id", OracleType.Number).Value = id;
            comando.Parameters.Add("estado", OracleType.Number).Value = estado;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }
    } 
}
