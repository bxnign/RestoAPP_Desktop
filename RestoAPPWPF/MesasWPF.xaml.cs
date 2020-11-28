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
    /// Lógica de interacción para MesasWPF.xaml
    /// </summary>
    public partial class MesasWPF : Window
    {
        public MesasWPF()
        {
            InitializeComponent();

        }

        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");

        public void VaciarCasillasAgregar()
        {
            txtNroMesa.Text = string.Empty;
            cboCantSillas.SelectedItem = cboItemdos;
        }

        public void VaciarCasillasModificar()
        {
            txtIdMod.Text = string.Empty;
            txtNroMesaMod.Text = string.Empty;
            txtCantSillasMod.Text = string.Empty;
            cboEstadoMod.SelectedItem = cboitemSeleccioneMod;
        }

        // --- BOTONES MENUS --- //
        private void btnirAgregar_Click(object sender, RoutedEventArgs e)
        {
            grAgregar.Visibility = Visibility.Visible;
            grAgregar.IsEnabled = true;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;
        }

        private void btnirModificar_Click(object sender, RoutedEventArgs e)
        {
            grModificar.Visibility = Visibility.Visible;
            grModificar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;
        }

        private void btnirListar_Click(object sender, RoutedEventArgs e)
        {
            grListar.Visibility = Visibility.Visible;
            grListar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;

            MesasNegocio mesas = new MesasNegocio();
            DataTable datos = new DataTable();
            datos = mesas.Listar();
            dtListarMesas.ItemsSource = datos.DefaultView;
            dtListarMesas.Columns.RemoveAt(0);
            conexion.Close();
        }

        private void btnirEstadoMesas_Click(object sender, RoutedEventArgs e)
        {
            EstadoMesaWPF ver_estado = new EstadoMesaWPF();
            ver_estado.ShowDialog();
        }

        private void btnIrEstadoMesa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                MesasNegocio mesas = new MesasNegocio();
                DataTable datos = new DataTable();
                datos = mesas.Listar();
               


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estos son todos los menus encontrados");
                    dtListarMesas.ItemsSource = datos.DefaultView;
                    dtListarMesas.Columns.RemoveAt(0);
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


        public bool CargarVariablesAgregar(ref MesasNegocio mesa)
        {
            if (ValidarAgregar())
            {
                
                mesa.Nro_Mesa = Convert.ToInt32(txtNroMesa.Text);
                mesa.Cant_Sillas = Convert.ToInt32(cboCantSillas.Text);
                return true;
            }

            return false;
        }

        public bool CargarVariablesModificar(ref MesasNegocio mesa)
        {
            if (ValidarModificar())
            {

                mesa.Id_Mesa = Convert.ToInt32(txtNroMesaMod.Text);
                mesa.Nro_Mesa = Convert.ToInt32(txtNroMesaMod.Text);
                mesa.Cant_Sillas = Convert.ToInt32(cboCantSillasMod.Text);
                mesa.Estado = cboEstadoMod.Text;
                return true;
            }

            return false;
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MesasNegocio mesas = new MesasNegocio();
            if (CargarVariablesAgregar(ref mesas))
            {
                try
                {

                    if (mesas.Agregar() == 1)
                    {
                        MessageBox.Show("Se agrego una Mesa Exitosamente " + "El estado de la mesa por defecto es: " + "Disponible");
                        VaciarCasillasAgregar();
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("El numero de la mesa Ya existe en el sistema");
                        conexion.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la base de datos: " + ex);
                }
            }
            
        }


        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            if(dtListarMesas.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una mesa para modificarla");
            }
            else
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
            

        }

        private void btnGuardarMod_Click(object sender, RoutedEventArgs e)
        {
            MesasNegocio mesas = new MesasNegocio();
            if (CargarVariablesModificar(ref mesas))
            {
                try
                {


                    MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea modificar la mesa?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (mesas.Modificar() == 1)
                        {
                            MessageBox.Show("Se modifico una mesa Exitosamente");
                            VaciarCasillasModificar();
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo modificar la mesa");
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MesasNegocio mesas = new MesasNegocio();
            DataRowView view = (DataRowView)dtListarMesas.SelectedItem;
            string id;
            id = view.Row.ItemArray[0].ToString();
            mesas.Id_Mesa = Convert.ToInt32(id);

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea eliminar la mesa?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (mesas.Eliminar() == 1)
                    {
                        MessageBox.Show("Se Elimino La mesa Exitosamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar La mesa");
                        conexion.Close();
                    }
                }
                   

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
            }
        }

        private void btnBuscarId_KeyUp(object sender, RoutedEventArgs e)
        {

        }
        
        private void cboBuscarPorEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListarPorEstado();
        }
        // --- BOTONES MENUS --- //

        // -- METODOS -- //

        public void ListarPorEstado()
        {

            if (cboItemSeleccioneEst.IsSelected)
            {


            }
            else
            {

            
            if (cboItemDisponibleEst.IsSelected)
            {
                MesasNegocio mesas = new MesasNegocio();
                string estadito = cboItemDisponibleEst.Content.ToString();
                mesas.Estado = estadito;
                DataTable dato = new DataTable();
                dato = mesas.ListarMesasPorEstado();
                dtListarMesas.ItemsSource = dato.DefaultView;
                    dtListarMesas.Columns.RemoveAt(0);
                    conexion.Close();

            }else if (cboItemEnLimpiezaEst.IsSelected)
            {

                MesasNegocio mesas = new MesasNegocio();
                string estadito = cboItemEnLimpiezaEst.Content.ToString();
                mesas.Estado = estadito;
                DataTable dato = new DataTable();
                dato = mesas.ListarMesasPorEstado();
                dtListarMesas.ItemsSource = dato.DefaultView;
                    dtListarMesas.Columns.RemoveAt(0);
                    conexion.Close();

            }
            else if (cboItemNoDisponibleEst.IsSelected)
            {
                MesasNegocio mesas = new MesasNegocio();
                string estadito = cboItemNoDisponibleEst.Content.ToString();
                mesas.Estado = estadito;
                DataTable dato = new DataTable();
                dato = mesas.ListarMesasPorEstado();
                dtListarMesas.ItemsSource = dato.DefaultView;
                    dtListarMesas.Columns.RemoveAt(0);
                    conexion.Close();
            }

            }
        }
        public void CargarCasillasModificar()
        {
            if (dtListarMesas.SelectedItem == null)
            {

            }
            else
            {
                DataRowView view = (DataRowView)dtListarMesas.SelectedItem;
                txtNroMesaMod.Text = view.Row.ItemArray[1].ToString();
                cboCantSillasMod.Text = view.Row.ItemArray[2].ToString();
                cboEstadoMod.Text = view.Row.ItemArray[3].ToString();
                txtNroMesaMod.IsEnabled = false;
            }
        }


<<<<<<< HEAD
=======
        public void VaciarCasillasModificar()
        {
            txtIdMod.Text = string.Empty;
            txtNroMesaMod.Text = string.Empty;
            cboCantSillasMod.Text = string.Empty;
            cboEstadoMod.SelectedItem = cboitemSeleccioneMod;
        }
>>>>>>> origin

        //VALIDACIONES 


         public bool ValidarAgregar()
        {
            if (ValidacionAgregarVacios())
            {
                if (ValidacionSimbolosyEspacios(txtNroMesa.Text))
                {
                    if (ValidacionTamanioNMesa(txtNroMesa.Text))
                    {
                        return true;
                    }
                }
                
            }

            return false;
        }

        public bool ValidacionSimbolosyEspacios(string nro)
        {
            string exp1;
            exp1 = "^[0-9]*$";

            if (Regex.IsMatch(nro, exp1) == false)
            {


                MessageBox.Show("No se aceptan Espacios ni caracteres como !#$%&/()=?¡-., \n revise su formato e intente nuevamente");
                return false;
            }

            return true;
        }

        public bool ValidarModificar()
        {
            if (ValidacionModificarVacios())
            {
                if (ValidacionTamanioNMesa(txtNroMesaMod.Text))
                {
                    return true;
                }
            }

            return false;
        }
        public bool ValidacionAgregarVacios()
        {
            if(txtNroMesa.Text == string.Empty || cboCantSillas.Text == string.Empty)
            {
                MessageBox.Show("No pueden existir elementos vacios");
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool ValidacionTamanioNMesa(string nro)
        {
            if(Convert.ToInt32(nro) < 51)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidacionModificarVacios()
        {
            if (txtNroMesaMod.Text == string.Empty || cboCantSillasMod.Text == string.Empty || cboEstadoMod.SelectedItem == cboItemSeleccioneEst || cboEstadoMod.Text == string.Empty)
            {
                MessageBox.Show("No pueden existir elementos vacios");
                return false;
            }
            else
            {
                return true;
            }
        }

        // VALIDACIONES FIN

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

        private void grAgregar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9){
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtNroMesaMod_KeyDown(object sender, KeyEventArgs e)
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


        private void txtIdMod_KeyDown(object sender, KeyEventArgs e)
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
        // -- METODOS -- //

    }
}
