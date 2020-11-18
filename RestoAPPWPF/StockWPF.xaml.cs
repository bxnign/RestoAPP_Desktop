using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using RestoAPPNegocio;
using System.Data.SqlClient;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para Personas.xaml
    /// </summary>
    public partial class StockWPF : Window
    {
        public StockWPF()
        {
            InitializeComponent();
        }

        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");
        private void btnirAgregarStock_Click(object sender, RoutedEventArgs e)
        {
            grAgregar.Visibility = Visibility.Visible;
            grAgregar.IsEnabled = true;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            cboNombre.Items.Clear();

            OracleCommand comando = new OracleCommand("SELECT * FROM PRODUCTOS", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboNombre.Items.Add(registro[1].ToString());
            }
            conexion.Close();
            cboNombre.Items.Insert(0, "Seleccione un producto");
            cboNombre.SelectedIndex = 0;
        }

        private void btnirModificarStock_Click(object sender, RoutedEventArgs e)
        {
            grModificar.Visibility = Visibility.Visible;
            grModificar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            OracleCommand comando = new OracleCommand("SELECT * FROM PRODUCTOS", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboNombreMod.Items.Add(registro[1].ToString());
            }
            conexion.Close();
        }

        private void btnirListarStock_Click(object sender, RoutedEventArgs e)
        {
            grListar.Visibility = Visibility.Visible;
            grListar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;

            StockNegocio stock = new StockNegocio();
            DataTable datos = new DataTable();
            datos = stock.ListarStock();
            dtgridListaStock.ItemsSource = datos.DefaultView;
            conexion.Close();
        }
        public void CargarVariablesAgregar(ref StockNegocio stock)
        {
            stock.Id_producto = Convert.ToString(cboNombre.Text);
            stock.Cantidad = Convert.ToInt32(txtCantidad.Text);
        }
        public void CargarVariablesModificar(ref StockNegocio stock)
        {
            stock.Id_stock = Convert.ToInt32(txtidStockMod.Text);
            stock.Id_producto = Convert.ToString(cboNombreMod.Text);
            stock.Cantidad = Convert.ToDecimal(txtCantidadMod.Text);
        }
        public void VaciarCasillasModificar()
        {
            cboNombreMod.Text = string.Empty;
            txtCantidadMod.Text = string.Empty;
        }
        public void VaciarCasillasAgregar()
        {
            cboNombre.Text = string.Empty;
            txtCantidad.Text = string.Empty;
        }
        public void CargarCasillasModificar()
        {
            if (dtgridListaStock.SelectedItem == null)
            {

            }
            else 
            { 
                DataRowView view = (DataRowView)dtgridListaStock.SelectedItem;

                txtidStockMod.Text = view.Row.ItemArray[0].ToString();
                txtCantidadMod.Text = view.Row.ItemArray[2].ToString();
                txtidStockMod.IsEnabled = true;
            }
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            StockNegocio stock = new StockNegocio();


            CargarVariablesAgregar(ref stock);
            try
            {

                if (stock.Agregar() == 1)
                {
                    MessageBox.Show("El stock se creo correctamente.");
                    VaciarCasillasAgregar();
                    conexion.Close();
                }
                else if (stock.Agregar() == 0)
                {
                    MessageBox.Show("El stock ya existe o no se ingresaron todos los datos");
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion: " + ex);
                conexion.Close();
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            StockNegocio stock = new StockNegocio();
            DataTable datos = new DataTable();
            datos = stock.ListarStock();
           

            try
            {


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estos el stock encontrado");
                    dtgridListaStock.ItemsSource = datos.DefaultView;
                    conexion.Close();
                }
                else if (datos.DefaultView == null)
                {
                    MessageBox.Show("No existe personas en la base de datos");
                    conexion.Close();
                }

            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }

        }
        private void btnGuardarMod_Click(object sender, RoutedEventArgs e)
        {
            StockNegocio stock = new StockNegocio();
            CargarVariablesModificar(ref stock);
            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea modificar el stock?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (stock.ModificarStock() == 1)
                    {
                        MessageBox.Show("El stock se modifico correctamente");
                        VaciarCasillasModificar();
                        conexion.Close();
                    }
                    else if (stock.ModificarStock() == 0)
                    {
                        MessageBox.Show("El stock no existe");
                        conexion.Close();
                    }

                }
                    

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion: " + ex);
                conexion.Close();
            }
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            CargarCasillasModificar();
            grModificar.Visibility = Visibility.Visible;
            grModificar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            cboNombreMod.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM PRODUCTOS", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            string dato;
            DataRowView view = (DataRowView)dtgridListaStock.SelectedItem;
            if (dtgridListaStock.SelectedItem == null)
            {
                conexion.Close();
            }
            else
            {
                while (registro.Read())
                {
                    cboNombreMod.Items.Add(registro[1].ToString());
                    dato = registro[1].ToString();
                    if (dato == view.Row.ItemArray[1].ToString())
                    {
                        cboNombreMod.Text = dato;
                    }
                }
                conexion.Close();
            }
        }

        // Mantenedor Agregar Listo 

        private void btnirProductos_Click(object sender, RoutedEventArgs e)
        {
            ProductosWPF ver_productos = new ProductosWPF();
            ver_productos.Owner = this;
            ver_productos.Show();
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            StockNegocio stock = new StockNegocio();
            DataRowView view = (DataRowView)dtgridListaStock.SelectedItem;
            string id;
            id = view.Row.ItemArray[0].ToString();
            stock.Id_stock = Convert.ToInt32(id);

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea eliminar el stock?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (stock.Eliminar() == 1)
                    {
                        MessageBox.Show("Se Elimino el stock exitosamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el stock");
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
            }
        }

        private void Buscarid_KeyUp(object sender, RoutedEventArgs e)
        {
            if (txtBuscarid.Text == string.Empty)
            {
                StockNegocio stock = new StockNegocio();
                DataTable datos = new DataTable();
                datos = stock.ListarStock();
                dtgridListaStock.ItemsSource = datos.DefaultView;
            }
            else
            {
                StockNegocio stock = new StockNegocio();
                DataTable datos = new DataTable();
                string idstock = txtBuscarid.Text;
                stock.Id_stock = Convert.ToInt32(idstock);
                datos = stock.ListarPorId();
                dtgridListaStock.ItemsSource = datos.DefaultView;
                conexion.Close();
            }

        }

        private void txtBuscarid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
