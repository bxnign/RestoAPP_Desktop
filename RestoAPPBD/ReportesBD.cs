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
    public class ReportesBD
    {
        public object DatabaseHelper { get; private set; }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");


        public DataTable ListarReporteIngresos()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_REPORTE_INGRESOS", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public DataTable ListarReporteEgresos()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_REPORTE_EGRESOS", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public DataTable ListarReporteConsumosPorcion()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_REPORTE_CONSUMOS_PORCION", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public DataTable ListarReporteConsumosMenu()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_REPORTE_CONSUMOS_MENU", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public DataTable ListarReporteAtencion()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SP_LISTAR_REPORTE_ATENCION", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }
        public Table ReporteIngresos()
        {
            conexion.Open();
            string oracle = "select ID_REGISTRO as Id, INGRESO as Ingresos, to_char(fecha,'hh/mm/yyyy') as Fecha from registros";
            OracleCommand comando = new OracleCommand(oracle, conexion);
            OracleDataReader reader = comando.ExecuteReader();
            OracleDataAdapter adaptador = new OracleDataAdapter();

            PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string[] columnas = { "Id Registro", "Ingresos", "Fecha de Compra" };
            float[] tamannos = { 2, 7, 2 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tamannos));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (string columna in columnas)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
            }

            while (reader.Read())
            {
                tabla.AddCell(new Cell().Add(new Paragraph(reader["id"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["Ingresos"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["Fecha"].ToString()).SetFont(fontContenido)));
            }
            return tabla;
        }
        public Table ReporteIngresosTotal()
        {
            conexion.Open();
            string oracle = "select sum(ingreso) as total from registros";
            OracleCommand comando = new OracleCommand(oracle, conexion);
            OracleDataReader reader = comando.ExecuteReader();
            OracleDataAdapter adaptador = new OracleDataAdapter();

            PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string[] columnas = { "ingresos Totales" };
            float[] tamannos = { 3 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tamannos));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (string columna in columnas)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
            }
            while (reader.Read())
            {
                tabla.AddCell(new Cell().Add(new Paragraph(reader["total"].ToString()).SetFont(fontContenido)));
            }
            return tabla;
        }
        public Table ReporteMenus()
        {
            conexion.Open();
            string oracle = "select me.id_menu as id,me.nom_menu as nombre, count(rel.id_menu) as cantidad, me.precio_menu * count(rel.id_menu) as precio from REL_PEDIDOS_MENUS rel join menus me on rel.id_menu = me.id_menu group by me.nom_menu, me.precio_menu, me.id_menu";
            OracleCommand comando = new OracleCommand(oracle, conexion);
            OracleDataReader reader = comando.ExecuteReader();
            OracleDataAdapter adaptador = new OracleDataAdapter();

            PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string[] columnas = { "Id Menu", "Nombre Menu", "Menus comprados", "Precio Total" };
            float[] tamannos = { 2, 7, 2, 3 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tamannos));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (string columna in columnas)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
            }
            while (reader.Read())
            {
                tabla.AddCell(new Cell().Add(new Paragraph(reader["id"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["nombre"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["cantidad"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["precio"].ToString()).SetFont(fontContenido)));
            }
            return tabla;
        }
        public Table ReportePorciones()
        {
            conexion.Open();
            string oracle = "select PO.ID_PORCION as id, po.nombre_porcion as nombre, count(rel.id_porcion) as cantidad, po.precio_porcion * count(rel.id_porcion) as precio from REL_PEDIDOS_PORCIONES rel join PORCIONES PO on rel.id_porcion = po.id_porcion group by PO.ID_PORCION, po.nombre_porcion, po.precio_porcion";
            OracleCommand comando = new OracleCommand(oracle, conexion);
            OracleDataReader reader = comando.ExecuteReader();
            OracleDataAdapter adaptador = new OracleDataAdapter();

            PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string[] columnas = { "Id Porcion", "Nombre Porcion", "Porciones comprados", "Precio Total" };
            float[] tamannos = { 2, 7, 2, 3 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tamannos));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (string columna in columnas)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
            }
            while (reader.Read())
            {
                tabla.AddCell(new Cell().Add(new Paragraph(reader["id"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["nombre"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["cantidad"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["precio"].ToString()).SetFont(fontContenido)));
            }
            return tabla;
        }
        public Table ReporteTotalMinutosPedidos()
        {
            conexion.Open();
            string oracle = "SELECT TRUNC(AVG(trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (24))))||':'||TRUNC(AVG(trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (60 * 24)) - (trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (24)) * 60))) ||':'|| TRUNC(AVG(trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (60 * 60 * 24)) - (trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (60 * 24)) * 60))) PROMEDIO FROM PEDIDOS where ID_EST_PEDIDO = 2";
            OracleCommand comando = new OracleCommand(oracle, conexion);
            OracleDataReader reader = comando.ExecuteReader();
            OracleDataAdapter adaptador = new OracleDataAdapter();

            PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string[] columnas = { "Promedio de pedidos" };
            float[] tamannos = { 2 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tamannos));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (string columna in columnas)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
            }
            while (reader.Read())
            {
                tabla.AddCell(new Cell().Add(new Paragraph(reader["PROMEDIO"].ToString()).SetFont(fontContenido)));
            }
            return tabla;
        }
        public Table ReporteMinutosPedidos()
        {
            conexion.Open();
            string oracle = "select id_pedido id, to_char(HORA_INI_PEDIDO, 'dd/mm/yyyy') Fecha, trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (24)) ||':'|| round(trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (60 * 24)) - (trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (24)) * 60)) ||':'|| round(trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (60 * 60 * 24)) - (trunc((HORA_TER_PEDIDO - HORA_INI_PEDIDO) * (60 * 24)) * 60)) TIEMPO from pedidos where ID_EST_PEDIDO = 2";
            OracleCommand comando = new OracleCommand(oracle, conexion);
            OracleDataReader reader = comando.ExecuteReader();
            OracleDataAdapter adaptador = new OracleDataAdapter();

            PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string[] columnas = { "Id pedido", "Fecha", "Tiempo de entrega" };
            float[] tamannos = { 2, 3, 2 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tamannos));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (string columna in columnas)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
            }
            while (reader.Read())
            {
                tabla.AddCell(new Cell().Add(new Paragraph(reader["id"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["Fecha"].ToString()).SetFont(fontContenido)));
                tabla.AddCell(new Cell().Add(new Paragraph(reader["TIEMPO"].ToString()).SetFont(fontContenido)));
            }
            return tabla;
        }

    }
}