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
    /// Lógica de interacción para MesasWPF.xaml
    /// </summary>
    public partial class MesasWPF : Window
    {
        public MesasWPF()
        {
            InitializeComponent();

        }

        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");

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

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MesasNegocio mesas = new MesasNegocio();
                mesas.Nro_Mesa = Convert.ToInt32(txtNroMesa.Text);
                mesas.Cant_Sillas = Convert.ToInt32(cboCantSillas.Text);

                    if (mesas.Agregar() == 1)
                {
                    MessageBox.Show("Se agrego una Mesa Exitosamente " + "El estado de la mesa por defecto es: " + "Disponible");
                    VaciarCasillasAgregar();
                    conexion.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo crear la mesa");
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
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

        }

        private void btnGuardarMod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MesasNegocio mesas = new MesasNegocio();
                mesas.Id_Mesa = Convert.ToInt32(txtIdMod.Text);
                mesas.Nro_Mesa = Convert.ToInt32(txtNroMesaMod.Text);
                mesas.Cant_Sillas = Convert.ToInt32(txtCantSillasMod.Text);
                mesas.Estado = cboEstadoMod.Text;
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
                conexion.Close();

            }else if (cboItemEnLimpiezaEst.IsSelected)
            {

                MesasNegocio mesas = new MesasNegocio();
                string estadito = cboItemEnLimpiezaEst.Content.ToString();
                mesas.Estado = estadito;
                DataTable dato = new DataTable();
                dato = mesas.ListarMesasPorEstado();
                dtListarMesas.ItemsSource = dato.DefaultView;
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
                txtIdMod.Text = view.Row.ItemArray[0].ToString();
                txtNroMesaMod.Text = view.Row.ItemArray[1].ToString();
                txtCantSillasMod.Text = view.Row.ItemArray[2].ToString();
                cboEstadoMod.Text = view.Row.ItemArray[3].ToString();
                txtIdMod.IsEnabled = false;
            }
        }

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






        // -- METODOS -- //

    }
}
