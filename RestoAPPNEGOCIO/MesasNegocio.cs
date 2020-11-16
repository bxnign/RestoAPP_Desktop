using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPPBD;

namespace RestoAPPNegocio
{
    public class MesasNegocio
    {
        public int Id_Mesa { get; set; }
        public int Nro_Mesa { get; set; }
        public int Cant_Sillas { get; set; }
        public string Estado { get; set; }


        public int Agregar()
        {
            return new MesasBD().Agregar(this.Nro_Mesa, this.Cant_Sillas);
        }

        public int Modificar()
        {
            return new MesasBD().Modificar(this.Id_Mesa, this.Nro_Mesa, this.Cant_Sillas, this.Estado);
        }

        public int Modificar_Estado()
        {
            return new MesasBD().Modificar_Estado(this.Id_Mesa, this.Estado);
        }

        public int Eliminar()
        {
            return new MesasBD().Eliminar(this.Id_Mesa);
        }

        public DataTable Listar()
        {
            return new MesasBD().Listar();
        }

        public DataTable ListarEstadoMesa()
        {
            return new MesasBD().ListarEstadoMesa();
        }

        public DataTable ListarMesasPorEstado()
        {
            return new MesasBD().ListarMesasPorEstado(this.Estado);
        }
    }
}
