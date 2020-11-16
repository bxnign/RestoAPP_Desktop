using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPPBD;

namespace RestoAPPNegocio
{
    public class ProductosNegocio
    {
        public int Id_producto { get; set; }
        public string Nom_producto { get; set; }
        public string Distribucion_product { get; set; }
        public decimal Cant_producto { get; set; }
        public string Id_tipo { get; set; }

        public int AgregarProductos()
        {
            return new ProductosBD().AgregarProductos(this.Nom_producto, this.Distribucion_product, this.Id_tipo);
        }
        public int ModificarProductos()
        {
            return new ProductosBD().ModificarProductos(this.Id_producto, this.Nom_producto, this.Distribucion_product, this.Id_tipo);
        }
        public DataTable ListarProductos()
        {
            return new ProductosBD().ListarProductos();
        }
        public int Eliminar()
        {
            return new ProductosBD().EliminarProductos(this.Id_producto);
        }

        public DataTable ListarProductosPorId()
        {
            return new ProductosBD().ListarProductosPorId(this.Id_producto);
        }
    }
}
