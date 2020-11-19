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
            cboEspecificacion.Items.Insert(0, "Seleccione un tipo");
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
        public void CargarVariablesAgregar(ref ProductosNegocio producto)
        {
            producto.Nom_producto = txtNombre.Text;
            producto.Distribucion_product = cboDistribucion.Text;
            producto.Id_tipo = Convert.ToString(cboEspecificacion.Text);
        }
        public void CargarVariablesModificar(ref ProductosNegocio Producto)
        {
            Producto.Id_producto = Convert.ToInt32(txtIdProductoMod.Text);
            Producto.Nom_producto = txtNombreMod.Text;
            Producto.Distribucion_product = cboDistribucionMod.Text;
            Producto.Id_tipo = Convert.ToString(cboEspecificacionMod.Text); 
            
        }
        public void VaciarCasillasModificar()
        {
            txtIdProductoMod.Text = string.Empty;
            txtNombreMod.Text = string.Empty;
            cboDistribucionMod.Text = string.Empty;
            cboEspecificacionMod.Text = string.Empty;
        }
        public void VaciarCasillasAgregar()
        {
            txtNombre.Text = string.Empty;
            cboDistribucion.Text = string.Empty;
            cboEspecificacion.Text = string.Empty;
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
                txtIdProductoMod.IsEnabled = true;
            }
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ProductosNegocio productos = new ProductosNegocio();


            CargarVariablesAgregar(ref productos);
            try
            {

                if (productos.AgregarProductos() == 1)
                {
                    MessageBox.Show("El usuario se creo correctamente.");
                    VaciarCasillasAgregar();
                    conexion.Close();
                }
                else if (productos.AgregarProductos() == 0)
                {
                    MessageBox.Show("El usuario ya existe o no se ingresaron todos los datos");
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
            CargarVariablesModificar(ref productos);
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

            cboEspecificacionMod.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM ESPECIFICACIONES", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            string dato;
            DataRowView view = (DataRowView)dtgridListaProductos.SelectedItem;
            if (dtgridListaProductos.SelectedItem == null)
            {
                conexion.Close();
            }
            else
            {
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
    }
}
