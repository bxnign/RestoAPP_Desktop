using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Source;
using RestoAPPBD;

namespace RestoAPPNegocio
{
    public class MenusNegocio
    {
        public int Id_Menu { get; set; }
        public string Nombre { get; set; }

        public string Porcion { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }

        public Int64 Id_pedido { get; set; }
        public int Estado_Pedido { get; set; }



    public DataTable Listar()
        {
            return new MenusBD().Listar();
        }

        public DataTable ListarPedidos()
        {
            return new PedidosBD().ListarPedidos();
        }

        public int Eliminar()
        {
            return new MenusBD().Eliminar(this.Id_Menu);
        }

        public int EliminarRefPorcionesMenu()
        {
            return new MenusBD().EliminarRefPorcionesMenu(this.Id_Menu);
        }


        public int Agregar()
        {
            return new MenusBD().Agregar(this.Id_Menu, this.Nombre, this.Precio, this.Descripcion);
        }

        public int Modificar()
        {
            return new MenusBD().Modificar(this.Id_Menu, this.Nombre, this.Precio, this.Descripcion);
        }

        public int ActualizarEstado()
        {
            return new PedidosBD().ActualizarEstado(this.Id_pedido, this.Estado_Pedido);
        }

        public DataTable LlenarCombobox()
        {
            return new MenusBD().LlenarCombobox();
        }

        public int AgregarRelacionMenuPorciones()
        {
            return new MenusBD().AgregarRelacionMenuPorciones(this.Nombre , this.Porcion);
        }

        public int ModificarRelMenuPorciones()
        {
            return new MenusBD().ModificarRelMenuPorciones(this.Nombre, this.Porcion);
        }
    }

    
}
