using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public void VaciarCasillasAgregar()
        {
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            cboPorciones.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        public void VaciarCasillasModificar()
        {
            txtId_Mod.Text = string.Empty;
            txtNombreMod.Text = string.Empty;
            txtPrecioMod.Text = string.Empty;
            cboPorcionesMod.Text = string.Empty;
            txtDescripcionMod.Text = string.Empty;
        }
        // -- Metodos propios -- //

        public void CargarCasillasModificar()
        {
           
                DataRowView view = (DataRowView)dtListarMenus.SelectedItem;
                txtId_Mod.Text = view.Row.ItemArray[0].ToString();
                txtNombreMod.Text = view.Row.ItemArray[1].ToString();
                txtPrecioMod.Text = view.Row.ItemArray[2].ToString();
                cboPorcionesMod.Text = view.Row.ItemArray[3].ToString();
                txtDescripcionMod.Text = view.Row.ItemArray[4].ToString();
                
                txtId_Mod.IsEnabled = false;
            
        }

        public bool CargarVariablesAgregar(ref MenusNegocio menu)
        {
            if (ValidacionAgregar())
            {
                menu.Nombre = txtNombre.Text;
                menu.Precio = Convert.ToInt32(txtPrecio.Text);
                menu.Descripcion = txtDescripcion.Text;
                menu.Porcion = cboPorciones.Text;
                return true;
            }

            return false;
            
        }

        public bool CargarVariablesModificar(ref MenusNegocio menu)
        {
            if (ValidacionModificar())
            {
                menu.Id_Menu = Convert.ToInt32(txtId_Mod.Text);
                menu.Nombre = txtNombreMod.Text;
                menu.Precio = Convert.ToInt32(txtPrecioMod.Text);
                menu.Descripcion = txtDescripcionMod.Text;
                menu.Porcion = cboPorcionesMod.Text;
                txtId_Mod.IsEnabled = false;
                return true;
            }
            return false;
        }

        // VALIDACIONES 

        public bool ValidacionAgregar()
        {
            if (ValidacionElementosVaciosAgregar())
            {
                if (ValidacionSimbolosEspacios(txtNombre.Text , txtPrecio.Text , txtDescripcion.Text))
                {
                    if(ValidacionLargoNombe(txtNombre.Text) && ValidacionPrecio(txtPrecio.Text))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool ValidacionModificar()
        {
            if (ValidacionElementosVaciosModificar())
            {
                if (ValidacionSimbolosEspacios(txtNombreMod.Text, txtPrecioMod.Text, txtDescripcionMod.Text))
                {
                    if (ValidacionLargoNombe(txtNombreMod.Text) && ValidacionPrecio(txtPrecioMod.Text))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool ValidacionElementosVaciosAgregar()
        {
            if (txtNombre.Text == string.Empty || cboPorciones.Text == string.Empty || cboPorciones.Text == "Seleccione")
            {
                MessageBox.Show("No puede haber ninguna casilla obligatoria (*) Vacia");
                return false;
            }

            return true;
        }

        public bool ValidacionElementosVaciosModificar()
        {
            if (txtNombreMod.Text == string.Empty || cboPorcionesMod.Text == string.Empty || cboPorcionesMod.Text == "Seleccione")
            {
                MessageBox.Show("No puede haber ninguna casilla obligatoria (*) Vacia");
                return false;
            }

            return true;
        }
    

        public bool ValidacionLargoNombe(string nombre)
        {
           if(nombre.Length < 4)
            {
                MessageBox.Show("El largo del nombre del menu no puede ser menos de 4 caracteres");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidacionSimbolosEspacios(string nombre , string precio , string descripcion)
        {
            string val = "^[ A-Za-z ]*$";
            string val2 = "^[0-9]*$";
            if (Regex.IsMatch(nombre, val)  == false || Regex.IsMatch(precio, val2) == false || Regex.IsMatch(descripcion, val) == false)
            {
                MessageBox.Show("No pueden haber Simbolos en las casillas ni espacios en precio");
                return false;
            }

            return true;
        }
       
   

        public bool ValidacionPrecio(string precio)
        {  
            if(Convert.ToInt32(precio) <= 900)
            {
                MessageBox.Show("El valor que esta ingreasando es muy bajo, el monto no debe ser menor a 1000 pesos");
                return false;
            }

            return true;
        }

        // VALIDACIONES FIN 


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
            cboPorciones.Items.Insert(0, "Seleccione");
            cboPorciones.SelectedIndex = 0;

        }

        private void Button_ClickModificar(object sender, RoutedEventArgs e)
        {
           
            
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
            
         

            if (dtListarMenus.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un menu para modificar");
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

                cboPorcionesMod.Items.Clear();
                OracleCommand comando = new OracleCommand("SELECT * FROM PORCIONES", conexion);
                conexion.Open();
                OracleDataReader registro = comando.ExecuteReader();
                while (registro.Read())
                {
                    cboPorcionesMod.Items.Add(registro[3].ToString());
                }
                conexion.Close();
                cboPorcionesMod.Items.Insert(0, "Seleccione");
                CargarCasillasModificar();
            }
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
            MenusNegocio menus = new MenusNegocio();

            if (CargarVariablesAgregar(ref menus))
            {
                try
                {


                    if (menus.Agregar() == 1)
                    {
<<<<<<< HEAD
                        MessageBox.Show("Se agrego un Menu Exitosamente");
                        VaciarCasillasAgregar();
                        conexion.Close();
=======
                        if (menus.AgregarRelacionMenuPorciones() == 1)
                        {
                            MessageBox.Show("Se agrego un Menu Exitosamente");
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo crear el menu");
                            conexion.Close();
                        }

>>>>>>> origin
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
           
        }

        private void Button_ModificarClick(object sender, RoutedEventArgs e)
        {
            MenusNegocio menus = new MenusNegocio();
            if (CargarVariablesModificar(ref menus))
            {
                try
                {
<<<<<<< HEAD
                    if (menus.Modificar() == 1 && menus.ModificarRelMenuPorciones() == 1)
                    {
                        MessageBox.Show("Se modifico un Menu Exitosamente");
                        VaciarCasillasModificar();
                        conexion.Close();
                    }
                    else
=======

                    MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea modificar este menu?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
>>>>>>> origin
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
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Tab)
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

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
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

        private void txtId_Mod_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
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
