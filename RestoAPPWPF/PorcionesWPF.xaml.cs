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
    /// Lógica de interacción para PorcionesWPF.xaml
    /// </summary>
    public partial class PorcionesWPF : Window
    {
        public PorcionesWPF()
        {
            InitializeComponent();
        }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");


        private void btnirAgregarPorcion_Click(object sender, RoutedEventArgs e)
        {
            grAgregar.Visibility = Visibility.Visible;
            grAgregar.IsEnabled = true;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            cboNombreProd.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM productos", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboNombreProd.Items.Add(registro[1].ToString());
            }
            conexion.Close();
            cboNombreProd.Items.Insert(0, "Seleccione un tipo");
            cboNombreProd.SelectedIndex = 0;
        }

        private void btnirModificarPorcion_Click(object sender, RoutedEventArgs e)
        {
            grModificar.Visibility = Visibility.Visible;
            grModificar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            cboNombreProductoMod.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM productos", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboNombreProductoMod.Items.Add(registro[1].ToString());
            }
            conexion.Close();
        }

        private void btnirListarPorcion_Click(object sender, RoutedEventArgs e)
        {
            grListar.Visibility = Visibility.Visible;
            grListar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;

            PorcionesNegocio porciones = new PorcionesNegocio();
            DataTable datos = new DataTable();
            datos = porciones.ListarPorciones();
            dtgridListaPorcion.ItemsSource = datos.DefaultView;
            conexion.Close();


        }
        public bool CargarVariablesAgregar(ref PorcionesNegocio porcion)
        {
            if (ValidacionTexBoxVacioAgregar())
            {
                if(ValidacionEspaciosEnBlancos(txtCantidad.Text) && ValidacionEspacioEnBlancosPrecio(txtPrecio.Text))
                {
                    if(ValidacionNumeros(txtCantidad.Text , txtPrecio.Text))
                    {
                        porcion.Cant_porcion = Convert.ToDecimal(txtCantidad.Text);
                        porcion.Id_producto = cboNombreProd.Text;
                        porcion.Nombre_porcion = txtNombrePorcion.Text;
                        porcion.Precio_porcion = Convert.ToInt32(txtPrecio.Text);
                        return true;
                    }
                }
            }

            return false;

        }
        public void CargarVariablesModificar(ref PorcionesNegocio porcion)
        {
            porcion.Id_porcion = Convert.ToInt32(txtIdPorcionMod.Text);
            porcion.Cant_porcion = Convert.ToDecimal(txtCantidadMod.Text);
            porcion.Id_producto = cboNombreProductoMod.Text;
            porcion.Nombre_porcion = txtNombrePorcionMod.Text;
            porcion.Precio_porcion = Convert.ToInt32(txtPrecioMod.Text);
        }
        public void VaciarCasillasModificar()
        {
            txtIdPorcionMod.Text = string.Empty;
            txtCantidadMod.Text = string.Empty;
            cboNombreProductoMod.Text = string.Empty;
            txtNombrePorcionMod.Text = string.Empty;
            txtPrecioMod.Text = string.Empty;
        }
        public void VaciarCasillasAgregar()
        {
            txtCantidadMod.Text = string.Empty;
            cboNombreProductoMod.Text = string.Empty;
            txtNombrePorcionMod.Text = string.Empty;
            txtPrecioMod.Text = string.Empty;
        }
        public void CargarCasillasModificar()
        {
            if (dtgridListaPorcion.SelectedItem == null)
            {

            }
            else
            {
                DataRowView view = (DataRowView)dtgridListaPorcion.SelectedItem;

                txtIdPorcionMod.Text = view.Row.ItemArray[0].ToString();
                txtCantidadMod.Text = view.Row.ItemArray[1].ToString();
                cboNombreProductoMod.Text = view.Row.ItemArray[2].ToString();
                txtNombrePorcionMod.Text = view.Row.ItemArray[3].ToString();
                txtPrecioMod.Text = view.Row.ItemArray[4].ToString();
                txtIdPorcionMod.IsEnabled = true;
            }
        }

        //VALIDACIONES
        public bool ValidacionTexBoxVacioAgregar()
        {
            
            if(txtCantidad.Text == string.Empty ||
            cboNombreProd.Text == string.Empty ||
            txtNombrePorcion.Text == string.Empty ||
            txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("Ninguna Casilla debe ir Vacia");
                return false;
            }
            else
            {
                if(cboNombreProd.Text != "Seleccione un tipo")
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

        public bool ValidacionTexBoxVacioModificar()
        {
            if(txtIdPorcionMod.Text == string.Empty ||
            txtCantidadMod.Text == string.Empty ||
            cboNombreProductoMod.Text == string.Empty ||
            txtNombrePorcionMod.Text == string.Empty ||
            txtPrecioMod.Text == string.Empty)
            {
                MessageBox.Show("Ninguna Casilla debe ir Vacia");
                return false;
            }
            else
            {
                if(cboNombreProductoMod.Text != "Seleccione")
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

        public bool ValidacionEspaciosEnBlancos(string dato)
        {
            // Valida espacios en blancos y que no exista signo que no sea la coma
            string val = "^[0-9,]*$";
            if (Regex.IsMatch(dato, val))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se aceptan espacios ni signos distintos a: , \nVuelva a Intentarlo");
                return false;
            }

           
        }

        public bool ValidacionEspacioEnBlancosPrecio(string dato)
        {
            string val2 = "^[0-9]*$";
            if (Regex.IsMatch(dato, val2))
            {
                return true;
            }
            else
            {
                MessageBox.Show("En Precio no se aceptan espacios signos como  !#$%&/()=?¡ : , \nVuelva a Intentarlo");
                return false;
            }
        }

        public bool ValidacionNumeros(string dato , string precio)
        {

            if (Convert.ToDecimal(dato) == 0 || Convert.ToInt32(precio) == 0)
            {

                MessageBox.Show("No puede ingresar un valor 0 \nVuelva a Intentarlo ");
                return false;
            }
            else
            {

                if(Convert.ToInt32(precio) < 600)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("El precio ingresado es muy bajo \n ¿esta seguro que desea ingresarlo?", "Cuidado", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                   
                }

                return true;
            }
        }

        //VALIDACIONES

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            PorcionesNegocio porciones = new PorcionesNegocio();


           if(CargarVariablesAgregar(ref porciones))
            {
                try
                {

                    if (porciones.AgregarPorciones() == 1)
                    {
                        MessageBox.Show("El usuario se creo correctamente.");
                        VaciarCasillasAgregar();
                        conexion.Close();
                    }
                    else if (porciones.AgregarPorciones() == 0)
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
           
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            PorcionesNegocio porciones = new PorcionesNegocio();
            DataTable datos = new DataTable();
            datos = porciones.ListarPorciones();
            
            try
            {


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estos el stock encontrado");
                    dtgridListaPorcion.ItemsSource = datos.DefaultView;
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
            PorcionesNegocio porciones = new PorcionesNegocio();
            CargarVariablesModificar(ref porciones);
            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea modificar la porcion?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (porciones.ModificarPorciones() == 1)
                    {
                        MessageBox.Show("El usuario se modifico correctamente");
                        VaciarCasillasModificar();
                        conexion.Close();
                    }
                    else if (porciones.ModificarPorciones() == 0)
                    {
                        MessageBox.Show("El usuario no existe");
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

            cboNombreProductoMod.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM productos", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            string dato;
            DataRowView view = (DataRowView)dtgridListaPorcion.SelectedItem;
            if (dtgridListaPorcion.SelectedItem == null)
            {
                conexion.Close();
            }
            else
            {
                while (registro.Read())
                {

                    cboNombreProductoMod.Items.Add(registro[1].ToString());
                    dato = registro[1].ToString();
                    if (dato == view.Row.ItemArray[3].ToString())
                    {
                        cboNombreProductoMod.Text = dato;
                    }
                }
                conexion.Close();
            }
            
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            PorcionesNegocio porciones = new PorcionesNegocio();
            DataRowView view = (DataRowView)dtgridListaPorcion.SelectedItem;
            string id;
            id = view.Row.ItemArray[0].ToString();
            porciones.Id_porcion = Convert.ToInt32(id);

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea eliminar la porcion?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (porciones.EliminarPorciones() == 1)
                    {
                        MessageBox.Show("La porcion se elimino exitosamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la porcion");
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
                PorcionesNegocio porciones = new PorcionesNegocio();
                DataTable datos = new DataTable();
                datos = porciones.ListarPorciones();
                dtgridListaPorcion.ItemsSource = datos.DefaultView;
            }
            else
            {
                PorcionesNegocio porciones = new PorcionesNegocio();
                DataTable datos = new DataTable();
                string prod = txtBuscarid.Text;
                porciones.Id_porcion = Convert.ToInt32(prod);
                datos = porciones.ListarPorcionesPorId();
                dtgridListaPorcion.ItemsSource = datos.DefaultView;
                conexion.Close();
            }
        }

        private void txtBuscarid_TextChanged(object sender, TextChangedEventArgs e)
        {

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

        private void txtNombrePorcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
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

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
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

        private void txtIdPorcionMod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtNombrePorcionMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z)
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

        private void txtPrecioMod_KeyDown(object sender, KeyEventArgs e)
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
