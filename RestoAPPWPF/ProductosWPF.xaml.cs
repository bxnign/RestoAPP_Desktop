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
using System.Text.RegularExpressions;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para ProductosWPF.xaml
    /// </summary>
    public partial class ProductosWPF : Window
    {
        public ProductosWPF()
        {
            InitializeComponent();
        }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");


        //CAMBIO DE VENTANA PRINCIPAL
        private void btnirAgregarProductos_Click(object sender, RoutedEventArgs e)
        {
            grAgregar.Visibility = Visibility.Visible;
            grAgregar.IsEnabled = true;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            cboEspecificacion.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM ESPECIFICACIONES", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboEspecificacion.Items.Add(registro[1].ToString());
            }
            conexion.Close();
            cboEspecificacion.Items.Insert(0, "Seleccione");
            cboEspecificacion.SelectedIndex = 0;
        }

        private void btnirModificarProductos_Click(object sender, RoutedEventArgs e)
        {
            grModificar.Visibility = Visibility.Visible;
            grModificar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;


            OracleCommand comando = new OracleCommand("SELECT * FROM ESPECIFICACIONES", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboEspecificacionMod.Items.Add(registro[1].ToString());
            }
            conexion.Close();
        }

        private void btnirListarProductos_Click(object sender, RoutedEventArgs e)
        {
            grListar.Visibility = Visibility.Visible;
            grListar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;

            ProductosNegocio productos = new ProductosNegocio();
            DataTable datos = new DataTable();
            datos = productos.ListarProductos();
            dtgridListaProductos.ItemsSource = datos.DefaultView;
            conexion.Close();
        }

        // FIN CAMBIO DE VENTANA PRINCIPAL

        // CARGA DE VARIABLES
        public bool CargarVariablesAgregar(ref ProductosNegocio producto)
        {
            if (ValidacionAgregar())
            {
                producto.Nom_producto = txtNombre.Text;
                producto.Distribucion_product = cboDistribucion.Text;
                producto.Id_tipo = Convert.ToString(cboEspecificacion.Text);

                return true;
            }


            return false;
        }
        public bool CargarVariablesModificar(ref ProductosNegocio Producto)
        {
            if (ValidacionModificar())
            {
                Producto.Id_producto = Convert.ToInt32(txtIdProductoMod.Text);
                Producto.Nom_producto = txtNombreMod.Text;
                Producto.Distribucion_product = cboDistribucionMod.Text;
                Producto.Id_tipo = Convert.ToString(cboEspecificacionMod.Text);

                return true;
            }

            return false;
        }
      
        public void CargarCasillasModificar() { 
            if (dtgridListaProductos.SelectedItem == null)
            {

            }
            else
            {
                DataRowView view = (DataRowView)dtgridListaProductos.SelectedItem;

                txtIdProductoMod.Text = view.Row.ItemArray[0].ToString();
                txtNombreMod.Text = view.Row.ItemArray[1].ToString();
                cboDistribucionMod.Text = view.Row.ItemArray[3].ToString();
            }
        }

        // CARGA DE VARIABLES

        // LIMPIAR ELEMENTOS
        public void VaciarCasillasModificar()
        {
            txtIdProductoMod.Text = string.Empty;
            txtNombreMod.Text = string.Empty;
            cboDistribucionMod.SelectedItem = cboitemModSeleccione;
            cboEspecificacionMod.SelectedIndex = 0; 
        }

        // LOS COMBOBOX NUNCA NUNCA NUNCA DEBEN QUEDAR VACIOS , FIJARSE EN ESOS DETALLES IMPORTANTES
        public void VaciarCasillasAgregar()
        {
            txtNombre.Text = string.Empty;
            cboDistribucion.SelectedItem = cboitemSeleccione;
            cboEspecificacion.SelectedIndex = 0;
        }

        // FIN LIMPIAR ELEMENTOS

        // VALIDACIONES
        public bool ValidacionAgregar()
        {
            if (ValidacionTexBoxVacioAgregar())
            {
                if (ProhibirSimbolos(txtNombre.Text))
                {
                    return true;
                }
            }

            return false;
        }
        public bool ValidacionModificar()
        {
            if (ValidacionTexBoxVacioModificar())
            {
                if (ProhibirSimbolos(txtNombreMod.Text))
                {
                    return true;
                }
            }

            return false;
        }
        public bool ValidacionTexBoxVacioModificar()
        {

            if (txtNombreMod.Text == string.Empty || txtIdProductoMod.Text == string.Empty )
            {
                MessageBox.Show("Ninguna Casilla debe ir Vacia");
                return false;
            }
            else
            {
                if (cboDistribucionMod.SelectedItem != cboitemModSeleccione || cboEspecificacionMod.Text != "Seleccione")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Ninguna Casilla debe ir Vacia");
                    return false;
                }
            }
        }

        public bool ValidacionTexBoxVacioAgregar()
        {

            if (txtNombre.Text == string.Empty || cboDistribucion.Text == string.Empty || cboEspecificacion.Text == string.Empty)
            {
                MessageBox.Show("Ninguna Casilla debe ir Vacia");
                return false;
            }
            else
            {

                if (cboDistribucion.SelectedItem != cboitemSeleccione && cboEspecificacion.Text != "Seleccione" )
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Ninguna Casilla debe ir Vacia");
                    return false;
                }
            }
        }
        public bool ProhibirSimbolos(string dato)
        {
            string exp1;
            exp1 = "^[A-Za-z]*$";

            if (Regex.IsMatch(dato, exp1) == false)
            {


                MessageBox.Show("No se aceptan Espacios ni caracteres como !#$%&/()=?¡-., \n revise su formato e intente nuevamente");
                return false;
            }
            else
            {
                if(dato.Length >= 2)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("El lago del nombre del producto no pueden ser menos de 3 caracteres");
                    return false;
                }
                
            }


        }

        //FIN VALIDACIONES

        // BOTONES Y EVENTOS HASTA EL FINAL

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
            { 
            ProductosNegocio productos = new ProductosNegocio();


            if(CargarVariablesAgregar(ref productos))
            {
                try
                {

                    if (productos.AgregarProductos() == 1)
                    {
                        MessageBox.Show("El producto se ingreso correctamente.");
                        VaciarCasillasAgregar();
                        conexion.Close();
                    }
                    else if (productos.AgregarProductos() == 0)
                    {
                        MessageBox.Show("no se pudo ingresar el producto , contactese con el administrador");
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
            ProductosNegocio productos = new ProductosNegocio();
            DataTable datos = new DataTable();
            datos = productos.ListarProductos();



            try
            {


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estos son los productos encontrado");
                    dtgridListaProductos.ItemsSource = datos.DefaultView;
                    conexion.Close();
                }
                else if (datos.DefaultView == null)
                {
                    MessageBox.Show("No existen productos en la base de datos");
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
            ProductosNegocio productos = new ProductosNegocio();
           if( CargarVariablesModificar(ref productos))
            {
                try
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea modificar el producto?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (productos.ModificarProductos() == 1)
                        {
                            MessageBox.Show("El producto se modifico correctamente");
                            VaciarCasillasModificar();
                            conexion.Close();
                        }
                        else if (productos.ModificarProductos() == 0)
                        {
                            MessageBox.Show("El producto no existe");
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
            if(dtgridListaProductos.SelectedItem == null)
            {
                MessageBox.Show("Para ir a modificar debe seleccionar un producto");
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
                txtIdProductoMod.IsEnabled = false;

                cboEspecificacionMod.Items.Clear();
                OracleCommand comando = new OracleCommand("SELECT * FROM ESPECIFICACIONES", conexion);
                conexion.Open();
                OracleDataReader registro = comando.ExecuteReader();
                string dato;
                DataRowView view = (DataRowView)dtgridListaProductos.SelectedItem;
                
                    while (registro.Read())
                    {
                        cboEspecificacionMod.Items.Add(registro[1].ToString());
                        dato = registro[1].ToString();
                        if (dato == view.Row.ItemArray[4].ToString())
                        {
                            cboEspecificacionMod.Text = dato;
                        }

                    }
                    conexion.Close();
                    cboEspecificacionMod.Items.Insert(0, "Seleccione");
                    CargarCasillasModificar();
            }
        }

        // Mantenedor Agregar Listo 

        //private void btnProductos_Click(object sender, RoutedEventArgs e)
        //{
        //    ProductosWPF ver_productos = new ProductosWPF();
        //    ver_productos.Owner = this;
        //    ver_productos.Show();
        //}


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ProductosNegocio productos = new ProductosNegocio();
            DataRowView view = (DataRowView)dtgridListaProductos.SelectedItem;
            string id;
            id = view.Row.ItemArray[0].ToString();
            productos.Id_producto = Convert.ToInt32(id);

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea eliminar el producto?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (productos.Eliminar() == 1)
                    {
                        MessageBox.Show("Se Elimino el producto Exitosamente");
                        DataTable datos = new DataTable();
                        datos = productos.ListarProductos();
                        dtgridListaProductos.ItemsSource = datos.DefaultView;
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el producto");
                        conexion.Close();
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
            }
        }
        private void BuscarId_KeyUp(object sender, RoutedEventArgs e)
        {
            if (txtBuscarid.Text == string.Empty)
            {
                ProductosNegocio productos = new ProductosNegocio();
                DataTable datos = new DataTable();
                datos = productos.ListarProductos();
                dtgridListaProductos.ItemsSource = datos.DefaultView;
            }
            else
            {
                ProductosNegocio productos = new ProductosNegocio();
                DataTable datos = new DataTable();
                string prod = txtBuscarid.Text;
                productos.Id_producto = Convert.ToInt32(prod);
                datos = productos.ListarProductosPorId();
                dtgridListaProductos.ItemsSource = datos.DefaultView;
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

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Tab) 
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtIdProductoMod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtNombreMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Tab)
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
