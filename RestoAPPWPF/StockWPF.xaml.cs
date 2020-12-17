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
            cboNombre.Items.Insert(0, "Seleccione");
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
        public bool CargarVariablesAgregar(ref StockNegocio stock)
        {
            if (ValidacionAgregar())
            {
                stock.Id_producto = Convert.ToString(cboNombre.Text);
                stock.Cantidad = Convert.ToInt32(txtCantidad.Text);
                stock.Precio = Convert.ToInt32(txtPrecio.Text);
                return true;
            }

            return false;
            
        }
        public bool CargarVariablesModificar(ref StockNegocio stock)
        {
            if (ValidacionModificar())
            {
                stock.Id_stock = Convert.ToInt32(txtidStockMod.Text);
                stock.Id_producto = Convert.ToString(cboNombreMod.Text);
                stock.Cantidad = Convert.ToDecimal(txtCantidadMod.Text);
                stock.Precio = Convert.ToInt32(txtPrecioMod.Text);
                return true;
            }

            return false;
        }
        public void VaciarCasillasModificar()
        {
            txtidStockMod.Text = string.Empty;
            cboNombreMod.SelectedIndex = 0;
            txtCantidadMod.Text = string.Empty;
            txtPrecioMod.Text = string.Empty;
        }
        public void VaciarCasillasAgregar()
        {
            cboNombre.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
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
                txtPrecioMod.Text = view.Row.ItemArray[5].ToString();
                txtidStockMod.IsEnabled = false;
            }
        }


        // VALIDACION

        private bool ValidacionAgregar()
        {
           if(ValidacionTextoVacioAgregar())
            {
                if (ValidacionSignos(txtCantidad.Text, txtPrecio.Text))
                {
                    if (ValidacionSimbolosDecimalesEspacios(txtCantidad.Text))
                    {
                        if (ValidacionNumeros(txtCantidad.Text, txtPrecio.Text))
                        {
                            return true;
                        }
                    }
                }
                
            }

            return false;
        }

        private bool ValidacionModificar()
        {
            if (ValidacionTextoVacioModificar())
            {
                if (ValidacionSignos(txtCantidadMod.Text , txtPrecioMod.Text))
                {
                    if (ValidacionSimbolosDecimalesEspacios(txtCantidadMod.Text))
                    {
                        if (ValidacionNumeros(txtCantidadMod.Text, txtPrecioMod.Text))
                        {
                            return true;
                        }
                    }
                }
               
            }

            return false;
        }

        private bool ValidacionTextoVacioAgregar()
        {
            if(txtCantidad.Text == string.Empty || cboNombre.Text == string.Empty || cboNombre.Text == "Seleccione" || txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("No deben existir Casillas Vacias");
                return false;
            }else

            {

                return true;
            }
        }
        private bool ValidacionTextoVacioModificar()
        {
            if (txtCantidadMod.Text == string.Empty || cboNombreMod.Text == string.Empty || cboNombreMod.Text == "Seleccione" || txtPrecioMod.Text == string.Empty)
            {
                MessageBox.Show("No deben existir Casillas Vacias");
                return false;
            }
            else

            {

                return true;
            }
        }

        private bool ValidacionSignos(string dato , string dato2)
        {
                string val2 = "^[0-9]*$";
            if (Regex.IsMatch(dato, val2) && Regex.IsMatch(dato2, val2))
            {
                return true;
            }
            else
            {
                MessageBox.Show("En Precio y cantidad no se aceptan espacios ni signos como  !#$%&/()=?¡ :  \nVuelva a Intentarlo");
                return false;
            }
        }
        private bool ValidacionSimbolosDecimalesEspacios(string dato)
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
            }else
            {
                MessageBox.Show("No se aceptan espacios ni signos distintos a: , sin estar asociados a un numero \nVuelva a Intentarlo");
                return false;
            }
        }

     
        private bool ValidacionNumeros(string dato , string dato2)
        {
            if (Convert.ToInt32(dato2) <= 0|| Convert.ToInt32(dato) <= 0)
            {

                MessageBox.Show("No puede ingresar un valor 0  \nVuelva a Intentarlo ");
                return false;
            }
            else
            {


                return true;

            }
        }

        // FIN VALIDACION
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            StockNegocio stock = new StockNegocio();


            if(CargarVariablesAgregar(ref stock))
            {
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
                    MessageBox.Show("No existe inforamcion en la base de datos");
                    conexion.Close();
                }

            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }

        }

        // distribucion segun el producto
      
        private void btnGuardarMod_Click(object sender, RoutedEventArgs e)
        {
            StockNegocio stock = new StockNegocio();
           if( CargarVariablesModificar(ref stock))
            {
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
            
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgridListaStock.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un stock  para Modificar");
            }
            else
            {
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
                cboNombreMod.Items.Insert(0, "Seleccione");
                CargarCasillasModificar();
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
                        DataTable datos = new DataTable();
                        datos = stock.ListarStock();
                        dtgridListaStock.ItemsSource = datos.DefaultView;
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
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
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

        private void txtCantidadMod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtidStockMod_KeyDown(object sender, KeyEventArgs e)
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

        private void btnirRetiroStock_Click(object sender, RoutedEventArgs e)
        {
            RetiroStockWPF ver_retirostock = new RetiroStockWPF();
            ver_retirostock.Owner = this;
            ver_retirostock.Show();
        }
        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            TutorialStockWPF ver_tutotial_stock = new TutorialStockWPF();
            ver_tutotial_stock.Owner = this;
            ver_tutotial_stock.Show();
        }

        public void CargarDistribucion(string text)
        {
            DataTable datos = new DataTable();
            StockNegocio stock = new StockNegocio();
            ProductosNegocio producto = new ProductosNegocio();
            datos = producto.ListarProductos();
          //datos = stock.ListarStock();

                foreach (DataRow row in datos.Rows)
                {

                    if (row["Nombre del producto"].ToString() == text)
                    {
                        lbldistribucion.Content = row["Distribucion"].ToString();
                        lbldistribucion.Visibility = Visibility.Visible;
                        break;
                    }
                    else
                    {
                        lbldistribucion.Content = "";
                        lbldistribucion.Visibility = Visibility.Hidden;
                    }

                }
        }

        private void cboNombre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            CargarDistribucion(text);
        }

        private void cboNombre_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void cboNombre_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }
        public void CargarDistribucionMod(string text)
        {
            DataTable datos = new DataTable();
            StockNegocio stock = new StockNegocio();
            ProductosNegocio producto = new ProductosNegocio();
            datos = producto.ListarProductos();
            //datos = stock.ListarStock();

            foreach (DataRow row in datos.Rows)
            {

                if (row["Nombre del producto"].ToString() == text)
                {
                    lbldistribucionmod.Content = row["Distribucion"].ToString();
                    lbldistribucionmod.Visibility = Visibility.Visible;
                    break;
                }
                else
                {
                    lbldistribucionmod.Content = "";
                    lbldistribucionmod.Visibility = Visibility.Hidden;
                }

            }
        }

        private void cboNombreMod_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                string text = (sender as ComboBox).SelectedItem as string;
                CargarDistribucionMod(text);
            }

        private void txtPrecioMod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
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
    }
 }
