using System;
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
using System.Windows.Shapes;
using RestoAPPNegocio;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para MenusWPF.xaml
    /// </summary>
    public partial class MenusWPF : Window
    {
        public MenusWPF()
        {
            InitializeComponent();
            
        }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");


        // -- Metodos propios -- //

        public void CargarCasillasModificar()
        {
            if (dtListarMenus.SelectedItem == null)
            {

            }
            else
            {
                DataRowView view = (DataRowView)dtListarMenus.SelectedItem;
                txtId_Mod.Text = view.Row.ItemArray[0].ToString();
                txtNombreMod.Text = view.Row.ItemArray[1].ToString();
                txtPrecioMod.Text = view.Row.ItemArray[2].ToString();
                txtDescripcionMod.Text = view.Row.ItemArray[4].ToString();
                txtId_Mod.IsEnabled = false;


                cboPorcionesMod.Items.Clear();
                OracleCommand comando = new OracleCommand("SELECT * FROM PORCIONES", conexion);
                conexion.Open();
                OracleDataReader registro = comando.ExecuteReader();
                while (registro.Read())
                {
                    cboPorcionesMod.Items.Add(registro[3].ToString());
                }
                conexion.Close();
                cboPorcionesMod.Items.Insert(0, "Seleccione un tipo");
                cboPorcionesMod.SelectedIndex = 0;
                cboPorcionesMod.Text = view.Row.ItemArray[3].ToString();
            }
        }

        // -- Metodos propios -- //


        // --- BOTONES MENU VENTANAS --- //

        private void Button_ClickAgregar(object sender, RoutedEventArgs e)
        {
            grAgregar.Visibility = Visibility.Visible;
            grAgregar.IsEnabled = true;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            cboPorciones.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM PORCIONES", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboPorciones.Items.Add(registro[3].ToString());
            }
            conexion.Close();
            cboPorciones.Items.Insert(0, "Seleccione un tipo");
            cboPorciones.SelectedIndex = 0;

        }

        private void Button_ClickModificar(object sender, RoutedEventArgs e)
        {
            grModificar.Visibility = Visibility.Visible;
            grModificar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;

            cboPorcionesMod.Items.Clear();
            OracleCommand comando = new OracleCommand("SELECT * FROM PORCIONES", conexion);
            conexion.Open();
            OracleDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                cboPorcionesMod.Items.Add(registro[3].ToString());
            }
            conexion.Close();
            cboPorcionesMod.Items.Insert(0, "Seleccione un tipo");
            cboPorcionesMod.SelectedIndex = 0;
        }

        private void Button_ClickListar(object sender, RoutedEventArgs e)
        {

            grListar.Visibility = Visibility.Visible;
            grListar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;

            MenusNegocio menus = new MenusNegocio();
            DataTable datos = new DataTable();
            datos = menus.Listar();
            dtListarMenus.ItemsSource = datos.DefaultView;
            conexion.Close();

        }


        

       
        

        // --- BOTONES MENU VENTANAS --- //
        private void btnirModificar_Click(object sender, RoutedEventArgs e)
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
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MenusNegocio menus = new MenusNegocio();
            DataRowView view = (DataRowView)dtListarMenus.SelectedItem;
            string  id;
            id = view.Row.ItemArray[0].ToString();
            menus.Id_Menu = Convert.ToInt32(id);

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea eliminar este menu?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (menus.Eliminar() == 1 && menus.EliminarRefPorcionesMenu() == 1)
                    {
                        MessageBox.Show("Se Elimino el menu Exitosamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el menu");
                        conexion.Close();
                    }
                }
                    

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
            }

        }

 



        private void Button_AgregarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MenusNegocio menus = new MenusNegocio();
                menus.Nombre = txtNombre.Text;
                menus.Precio = Convert.ToInt32(txtPrecio.Text);
                menus.Descripcion = txtDescripcion.Text;
                menus.Porcion = cboPorciones.Text;

                if (menus.Agregar() == 1)
                {
                    if(menus.AgregarRelacionMenuPorciones() == 1)
                    {
                        MessageBox.Show("Se agrego un Menu Exitosamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo crear el menu");
                        conexion.Close();
                    }
                   
                }
                else
                {
                    MessageBox.Show("No se pudo crear el menu");
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
            }
        }

        private void Button_ModificarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MenusNegocio menus = new MenusNegocio();
                menus.Id_Menu = Convert.ToInt32(txtId_Mod.Text);
                menus.Nombre = txtNombreMod.Text;
                menus.Precio = Convert.ToInt32(txtPrecioMod.Text);
                menus.Descripcion = txtDescripcionMod.Text;
                menus.Porcion = cboPorcionesMod.Text;
                txtId_Mod.IsEnabled = false;
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea modificar este menu?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (menus.Modificar() == 1 && menus.ModificarRelMenuPorciones() == 1)
                    {
                        MessageBox.Show("Se modifico un Menu Exitosamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el menu");
                        conexion.Close();
                    }
                }
                   

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            MenusNegocio menus = new MenusNegocio();
            DataTable datos = new DataTable();
            datos = menus.Listar();

            try
            {


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estos son todos los menus encontrados");
                    dtListarMenus.ItemsSource = datos.DefaultView;
                    conexion.Close();
                }
                else if (datos.DefaultView == null)
                {
                    MessageBox.Show("No existen menus en la base de datos");
                    conexion.Close();
                }

            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }
        }

        private void btnirPorciones_Click(object sender, RoutedEventArgs e)
        {
            PorcionesWPF ver_porciones = new PorcionesWPF();
            ver_porciones.Owner = this;
            ver_porciones.Show();
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
            if (e.Key >= Key.A && e.Key <= Key.Z)
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

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
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

        private void txtId_Mod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtNombreMod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtDescripcionMod_KeyDown(object sender, KeyEventArgs e)
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
    }
}
