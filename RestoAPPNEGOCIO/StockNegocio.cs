using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPPBD;

namespace RestoAPPNegocio
{
    public class StockNegocio
    {
        public int Id_stock { get; set; }
        public decimal Cantidad { get; set; }
        public string Fecha_entrega { get; set; }
        public string Id_producto { get; set; }

        public int Precio { get; set; }


        public int Agregar()
        {
            return new StockBD().AgregarStock(this.Cantidad, this.Id_producto, this.Precio);
        }
        public int ModificarStock()
        {
            return new StockBD().ModificarStock(this.Id_stock, this.Cantidad, this.Id_producto, this.Precio);
        }
        public DataTable ListarStock()
        {
            return new StockBD().ListarStock();
        }
        public int Eliminar()
        {
            return new StockBD().EliminarStock(this.Id_stock);
        }

        public DataTable ListarPorId()
        {
            return new StockBD().ListarPorId(this.Id_stock);
        }

    }
}
