using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Diagnostics.Eventing.Reader;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;

using System.Configuration;
using System.Net.Http.Headers;

namespace RestoAPPBD
{
    public class PersonasBD
    {
        public object DatabaseHelper { get; private set; }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");

        //Conexion a la base de datos oracle
        public bool Login(string rut_persona, string password)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("SELECT * FROM PERSONAS WHERE RUT_PERSONA = :rut_persona AND PASS = :pass", conexion);
            comando.Parameters.AddWithValue(":rut_persona", rut_persona);
            comando.Parameters.AddWithValue(":pass", password);
            OracleDataReader lector = comando.ExecuteReader();
            bool resultado = lector.Read();
            return resultado;
        }

        // 
        public DataTable Validacion(string Rut_Persona_val)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_VALIDACION", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("rut", OracleType.NVarChar).Value = Rut_Persona_val;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

        public int HistorialAyuda(string asunto , string descripcion , int telefono , string rut)
        {
            conexion.Open();
            // especifico  el nombre del procedimiento y la conexion
            OracleCommand comando = new OracleCommand("PKG_MANT_AYUDA.SP_INGRESAR_AYUDA", conexion);
            // especifico el tipo de objeto que estoy trayendo de la base de datos
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            // lista de todas las variables que pide por parametro el procedimiento
            // los parametros y los tipos de valores que genero en  Negocio deben ser del mismos nombres
            comando.Parameters.Add("ASUN", OracleType.NVarChar).Value = asunto;
            comando.Parameters.Add("DESCRIP", OracleType.NVarChar).Value = descripcion;
            comando.Parameters.Add("FONO", OracleType.Number).Value = telefono;
            comando.Parameters.Add("RUT", OracleType.NVarChar).Value = rut;
            int resultado = comando.ExecuteNonQuery();

            return resultado;
        }
        public int Agregar(string rut, string nombre, string apellido_pat, string apellido_mat, string pass, string fecha_naci, string rol, string cargo, string correo)
        {

            conexion.Open();
            // especifico  el nombre del procedimiento y la conexion
            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_INGRESAR_PERSONA", conexion);
            // especifico el tipo de objeto que estoy trayendo de la base de datos
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            // lista de todas las variables que pide por parametro el procedimiento
            // los parametros y los tipos de valores que genero en  Negocio deben ser del mismos nombres
            comando.Parameters.Add("RUT", OracleType.NVarChar).Value = rut;
            comando.Parameters.Add("NOMBRE", OracleType.NVarChar).Value = nombre;
            comando.Parameters.Add("APE_PAT", OracleType.NVarChar).Value = apellido_pat;
            comando.Parameters.Add("APE_MAT", OracleType.NVarChar).Value = apellido_mat;
            comando.Parameters.Add("FE_NACI", OracleType.NVarChar).Value = fecha_naci;
            comando.Parameters.Add("CONTRASENNA", OracleType.NVarChar).Value = pass;
            comando.Parameters.Add("CORREO", OracleType.NVarChar).Value = correo;
            comando.Parameters.Add("ROL", OracleType.NVarChar).Value = rol;
            comando.Parameters.Add("CARGO", OracleType.NVarChar).Value = cargo;
            
            // se devuelte el resultado que es un int
            // update , insert , delete , retorna el numero de filas
            // drop y create devuelve un 0
            // todo lo demas un -1
            DataTable datos = SolucionError();
            int numerito = 0;
            foreach (DataRow row in datos.Rows)
            {

                if (row["Rut persona"].ToString() == rut)
                {

                    numerito = 5;
                    break;
                }
                
                else
                {
                    numerito = 1;
                }

               

            }
            comando.ExecuteNonQuery();
            return numerito;

        }

        public int ModificarPersonas(string rut, string nombre, string apellido_pat, string apellido_mat, string pass, string fecha_naci, string rol, string cargo, string correo)
        {

            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_MODIFICAR_PERSONA", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("RUT", OracleType.NVarChar).Value = rut;
            comando.Parameters.Add("NOMBRE", OracleType.NVarChar).Value = nombre;
            comando.Parameters.Add("APE_PAT", OracleType.NVarChar).Value = apellido_pat;
            comando.Parameters.Add("APE_MAT", OracleType.NVarChar).Value = apellido_mat;
            comando.Parameters.Add("FE_NACI", OracleType.NVarChar).Value = fecha_naci;
            comando.Parameters.Add("CONTRASENNA", OracleType.NVarChar).Value = pass;
            comando.Parameters.Add("CORREO", OracleType.NVarChar).Value = correo;
            comando.Parameters.Add("ROL", OracleType.NVarChar).Value = rol;
            comando.Parameters.Add("CARGO", OracleType.NVarChar).Value = cargo;

            DataTable datos = SolucionError();
            int numerito = 0;
            foreach (DataRow row in datos.Rows)
            {

                if (row["Rut persona"].ToString() == rut)
                {

                    numerito = 1;
                    break;

                }

                else
                {
                    numerito = 5;
                   
                }



            }
            comando.ExecuteNonQuery();
            return numerito;
        }

        public DataTable ListarPersonas()
        { 
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_LISTAR_PERSONA", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction=ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            conexion.Close();
            return table;
        }

        public DataTable SolucionError()
        {

            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_LISTAR_PERSONA", conexion);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }

        public int Eliminar(string rut_persona)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_ELIMINAR_PERSONA", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("RUT", OracleType.NVarChar).Value = rut_persona;
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public DataTable ListarPorRUT(string rut_persona)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_LISTAR_PERSONA_FILTR", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("rut", OracleType.NVarChar).Value = rut_persona;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }


        public DataTable Filtrado(string dato)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand("PKG_MANT_PERSONAS.SP_LISTAR_PERSONAS_FILTRO", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
            comando.Parameters.Add("dato", OracleType.NVarChar).Value = dato;
            OracleDataAdapter adaptador = new OracleDataAdapter();
            DataTable table = new DataTable();
            adaptador.SelectCommand = comando;
            adaptador.Fill(table);
            return table;
        }





    }
}
