using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPPBD
{
    public class MesasBD
    {
        public object DatabaseHelper { get; private set; }
        OracleConnection conexion = new OracleConnection("user id=topherapp;password=restoapp;data source=" +
                                                         "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                                         "(HOST=restaurante.c8e27p3hegzq.us-east-1.rds.amazonaws.com)(PORT=1521))(CONNECT_DATA=" +
                                                         "(SERVICE_NAME=DATABASE)))");


        public DataTable Listar()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_LISTAR_MESA", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

          public DataTable SolucionError()
        {

            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_LISTAR_MESA", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

        public int Eliminar(int id)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_ELIMINAR_MESA", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("ID", OracleType.Number).Value = id;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public int Agregar( int nro , int cantidad_sillas )
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_INGRESAR_MESA", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("NRO", OracleType.Number).Value = nro;
            comando.Parameters.Add("CANTIDAD", OracleType.Number).Value = cantidad_sillas;
            DataTable datos = SolucionError();
            int numerito = 0;
            foreach (DataRow row in datos.Rows)
            {

                if (Convert.ToInt32(row["Id Mesa"].ToString()) == nro)
                {

                    numerito = 5;
                    break;
                }

                else
                {
                    numerito = 1;
                }



            }
            if(numerito == 1)
            {
                comando.ExecuteNonQuery();
                return numerito;
            }
            else
            {
                return numerito;
            }
            
        }

        public int Modificar(int id, int nro, int cantidad_sillas, string estado)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_MODIFICAR_MESA", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("ID", OracleType.Number).Value = id;
            comando.Parameters.Add("NRO", OracleType.Number).Value = nro;
            comando.Parameters.Add("CANTIDAD", OracleType.Number).Value = cantidad_sillas;
            comando.Parameters.Add("ESTADO", OracleType.NVarChar).Value = estado;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public int Modificar_Estado(int id , string estado)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_MODIFICAR_ESTADO", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("ID", OracleType.Number).Value = id;
            comando.Parameters.Add("estado", OracleType.NVarChar).Value = estado;
            int resultado = comando.ExecuteNonQuery();
            return resultado;

        }

        public DataTable ListarEstadoMesa()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_LISTAR_EST_MESA", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

        public DataTable ListarMesasPorEstado(string estado)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MESAS.SP_LISTAR_FILT_ESTADO", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("estado", OracleType.NVarChar).Value = estado;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

    }
}
