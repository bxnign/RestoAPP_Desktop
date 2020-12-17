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
using System.Text.RegularExpressions;

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

            OracleCommand comando = new OracleCommand(" SELECT * FROM PRODUCTOS where cant_producto >= 5 and DISTRIBUCION_PRODUCT = 'Kilogramos' or cant_producto >= 500 and DISTRIBUCION_PRODUCT = 'Gramos' or cant_producto >= 5 and DISTRIBUCION_PRODUCT = 'Litros' ", conexion);
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

    
        public bool CargarVariablesAgregar(ref RetiroStockNegocio retiro_stock)
        {
            if (Validaciones())
            {
                retiro_stock.Id_producto = cboNombre.Text;
                retiro_stock.Cantidad = Convert.ToDouble(txtCantidad.Text);
                return true;
            }

            return false;
        }
        public void VaciarCasillasAgregar()
        {
            cboNombre.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
        }

        public bool  Validaciones()
        {
            if (ValidacionSinEspaciosEnBlancos())
            {
                if (ValidacionSignos(txtCantidad.Text))
                {
                    if (ValidacionDecimales(txtCantidad.Text))
                    {
                        if (ValidacionCantidad())
                        {
                            return true;
                        }
                    }
                }
                
            }

            return false;
            
        }

        public bool ValidacionSinEspaciosEnBlancos()
        {
            if(cboNombre.SelectedIndex == 0 || txtCantidad.Text == string.Empty)
            {
                MessageBox.Show("Las casillas no deben ir vacias");
                return false;

            }

            return true;
    
        }


        public bool ValidacionCantidad()
        {
            if(Convert.ToDouble(txtCantidad.Text) <= 0)
            {
                MessageBox.Show("La cantidad no puede ser 0");
                return false;
            }

            return true;
        }

        private bool ValidacionSignos(string dato)
        {
            string val2 = "^[0-9]*$";
            if (Regex.IsMatch(dato, val2))
            {
                return true;
            }
            else
            {
                MessageBox.Show("no se aceptan espacios signos como  !#$%&/()=?¡ :  \nVuelva a Intentarlo");
                return false;
            }
        }
        public bool ValidacionDecimales(string dato)
        {
            string val = "[0-9]*[,]?[0-9]*$";
            if (Regex.IsMatch(dato, val))
            {
                if (dato != ",")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Tiene que insertar Numeros");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("No se aceptan espacios ni signos distintos a: , sin estar asociados a un numero \nVuelva a Intentarlo");
                return false;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            RetiroStockNegocio retiro_stock = new RetiroStockNegocio();


            if(CargarVariablesAgregar(ref retiro_stock))
            {
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
                        DataTable datos = new DataTable();
                        datos = retiro_stock.ListarStock();
                        dtgridListaRetiroStock.ItemsSource = datos.DefaultView;
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
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public void CargarDistribucion(string text)
        {
            DataTable datos = new DataTable();
            StockNegocio stock = new StockNegocio();
            datos = stock.ListarStock();

            foreach (DataRow row in datos.Rows)
            {

                if (row["Nombre del producto"].ToString() == text)
                {
                    lblDistribucion.Content = row["Distribucion"].ToString();
                    lblDistribucion.Visibility = Visibility.Visible;
                    break;
                }
                else
                {
                    lblDistribucion.Content = "";
                    lblDistribucion.Visibility = Visibility.Hidden;
                }

            }
        }
        private void cboNombre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            CargarDistribucion(text);
        }
    }
}
