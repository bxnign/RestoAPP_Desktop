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
    /// Lógica de interacción para RetiroStockWPF.xaml
    /// </summary>
    public partial class RetiroStockWPF : Window
    {
        public RetiroStockWPF()
        {
            InitializeComponent();
        }

        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");
        private void btnirAgregarRetiroStock_Click(object sender, RoutedEventArgs e)
        {
            grAgregar.Visibility = Visibility.Visible;
            grAgregar.IsEnabled = true;
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
        private void btnirListarRetiroStock_Click(object sender, RoutedEventArgs e)
        {
            grListar.Visibility = Visibility.Visible;
            grListar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;

            RetiroStockNegocio retiro_stock = new RetiroStockNegocio();
            DataTable datos = new DataTable();
            datos = retiro_stock.ListarStock();
            dtgridListaRetiroStock.ItemsSource = datos.DefaultView;
            conexion.Close();
        }
        public void CargarVariablesAgregar(ref RetiroStockNegocio retiro_stock)
        {
            retiro_stock.Id_producto = Convert.ToString(cboNombre.Text);
            retiro_stock.Cantidad = Convert.ToInt32(txtCantidad.Text);
        }
        public void VaciarCasillasAgregar()
        {
            cboNombre.Text = string.Empty;
            txtCantidad.Text = string.Empty;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            RetiroStockNegocio retiro_stock = new RetiroStockNegocio();


            CargarVariablesAgregar(ref retiro_stock);
            try
            {

                if (retiro_stock.Agregar() == 1)
                {
                    MessageBox.Show("Se realizo el ingreso del stock en la cocina");
                    VaciarCasillasAgregar();
                    conexion.Close();
                }
                else if (retiro_stock.Agregar() == 0)
                {
                    MessageBox.Show("Error , comuniquese con el administrador");
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
            RetiroStockNegocio retiro_stock = new RetiroStockNegocio();
            DataTable datos = new DataTable();
            datos = retiro_stock.ListarStock();


            try
            {


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estos son los registros de stock retirados");
                    dtgridListaRetiroStock.ItemsSource = datos.DefaultView;
                    conexion.Close();
                }
                else if (datos.DefaultView == null)
                {
                    MessageBox.Show("No existe informacion en la base de datos");
                    conexion.Close();
                }

            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }
        }

        // Mantenedor Agregar Listo 
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            RetiroStockNegocio retiro_stock = new RetiroStockNegocio();
            DataRowView view = (DataRowView)dtgridListaRetiroStock.SelectedItem;
            string id;
            id = view.Row.ItemArray[0].ToString();
            retiro_stock.Id_retiro_stock = Convert.ToInt32(id);

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea eliminar el stock?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (retiro_stock.Eliminar() == 1)
                    {
                        MessageBox.Show("El stock ha vuelto desde cocina, registro del retiro eliminado correctamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error , comuniquese con el administrador");
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
                RetiroStockNegocio stock = new RetiroStockNegocio();
                DataTable datos = new DataTable();
                datos = stock.ListarStock();
                dtgridListaRetiroStock.ItemsSource = datos.DefaultView;
            }
            else
            {
                RetiroStockNegocio retirostock = new RetiroStockNegocio();
                DataTable datos = new DataTable();
                string idstock = txtBuscarid.Text;
                retirostock.Id_retiro_stock = Convert.ToInt32(idstock);
                datos = retirostock.ListarStock();
                dtgridListaRetiroStock.ItemsSource = datos.DefaultView;
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

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
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

        private void txtCantidadMod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtidStockMod_KeyDown(object sender, KeyEventArgs e)
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
