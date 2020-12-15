using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
namespace RestoAPPBD
{
    public class FinanzasBD
    {

        public object DatabaseHelper { get; private set; }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");

        public DataTable ListarFinanzas()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_FINANZAS", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

        public DataTable ListarPorDia(string dia)
        {

             conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_FINANZAS_FILTR_DIARIA", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("v_fecha", OracleType.NVarChar).Value = dia;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

        public DataTable ListarPorMes(string mes)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_FINANZAS_FILTR_MENSUAL", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("v_fecha", OracleType.Number).Value = Convert.ToInt32(mes);
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
    }
}
