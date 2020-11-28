using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPPBD;


namespace RestoAPPNegocio
{
    public class RetiroStockNegocio
    {
        public int Id_retiro_stock { get; set; }
        public decimal Cantidad { get; set; }
        public string Fecha_retiro { get; set; }
        public string Id_producto { get; set; }

        public int Agregar()
        {
            return new RetiroStockBD().AgregarRetiroStock(this.Cantidad, this.Id_producto);
        }
        public DataTable ListarStock()
        {
            return new RetiroStockBD().ListarRetiroStock();
        }
        public int Eliminar()
        {
            return new RetiroStockBD().EliminarRetiroStock(this.Id_retiro_stock);
        }
    }
}
