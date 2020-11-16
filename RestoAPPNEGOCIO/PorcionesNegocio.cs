using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPPBD;

namespace RestoAPPNegocio
{
    public class PorcionesNegocio
    {
        public int Id_porcion { get; set; }
        public decimal Cant_porcion { get; set; }
        public string Id_producto { get; set; }
        public string Nombre_porcion { get; set; }
        public int Precio_porcion { get; set; }

        public int AgregarPorciones()
        {
            return new PorcionesBD().AgregarPorciones(this.Cant_porcion, this.Id_producto, this.Nombre_porcion, this.Precio_porcion);
        }
        public int ModificarPorciones()
        {
            return new PorcionesBD().ModificarPorciones(this.Id_porcion, this.Cant_porcion, this.Id_producto, this.Nombre_porcion, this.Precio_porcion);
        }
        public int EliminarPorciones()
        {
            return new PorcionesBD().EliminarPorciones(this.Id_porcion);
        }
        public DataTable ListarPorciones()
        {
            return new PorcionesBD().ListarPorciones();
        }
        public DataTable ListarPorcionesPorId()
        {
            return new PorcionesBD().ListarPorcionesPorId(this.Id_porcion);
        }
    }
}
