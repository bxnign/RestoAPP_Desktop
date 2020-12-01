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
        OracleConnection conexion = new OracleConnection("user id=topherapp;password=restoapp;data source=" +
                                                         "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                                         "(HOST=restaurante.c8e27p3hegzq.us-east-1.rds.amazonaws.com)(PORT=1521))(CONNECT_DATA=" +
                                                         "(SERVICE_NAME=DATABASE)))");

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
    }
}
