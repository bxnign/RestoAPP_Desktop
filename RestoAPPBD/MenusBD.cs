using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using iText.Signatures;
using System.Runtime.CompilerServices;

namespace RestoAPPBD
{
    public class MenusBD
    {

        public object DatabaseHelper { get; private set; }
        OracleConnection conexion = new OracleConnection("user id=topherapp;password=restoapp;data source=" +
                                                         "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                                         "(HOST=restaurante.c8e27p3hegzq.us-east-1.rds.amazonaws.com)(PORT=1521))(CONNECT_DATA=" +
                                                         "(SERVICE_NAME=DATABASE)))");




        public int Eliminar(int id_menu)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_ELIMINAR_MENU", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("id", OracleType.Number).Value = id_menu;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public int EliminarRefPorcionesMenu(int id)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_ELIMINAR_REL_MENU_PORCION", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("id", OracleType.NVarChar).Value = id;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public int Agregar(int id_menu , string nombre , int precio , string descripcion)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_INGRESAR_MENU", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("nombre", OracleType.NVarChar).Value = nombre;
            comando.Parameters.Add("precio", OracleType.Number).Value = precio;
            comando.Parameters.Add("DESCRIP", OracleType.NVarChar).Value = descripcion;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public int Modificar(int id_menu, string nombre, int precio, string descripcion)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_MODIFICAR_MENU", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("id", OracleType.Number).Value = id_menu;
            comando.Parameters.Add("nombre", OracleType.NVarChar).Value = nombre;
            comando.Parameters.Add("precio", OracleType.Number).Value = precio;
            comando.Parameters.Add("DESCRIP", OracleType.NVarChar).Value = descripcion;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public int ModificarRelMenuPorciones(string menu , string porcion)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_MODIFICAR_REL_MENU_PORCION", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("menu", OracleType.NVarChar).Value = menu;
            comando.Parameters.Add("porcion", OracleType.NVarChar).Value = porcion;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public DataTable Listar()
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_LISTAR_MENU", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registro", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

        public DataTable LlenarCombobox()
        {

            OracleCommand comando = new OracleCommand("SELECT * FROM PORCIONES", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            DataTable tabla = new DataTable();
            while (registro.Read())
            {
              tabla.Columns.Add(registro[3].ToString());
            }

            return tabla;

        }

        public int AgregarRelacionMenuPorciones(string menu, string porcion)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_MENUS.SP_INGRESAR_REL_MENU_PORCION", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("menu", OracleType.NVarChar).Value = menu;
            comando.Parameters.Add("porcion", OracleType.NVarChar).Value = porcion;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }


    }
    }

    

