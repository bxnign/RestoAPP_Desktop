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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using RestoAPPNegocio;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using System.Data.SqlClient;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para FinanzasWPF.xaml
    /// </summary>
    public partial class FinanzasWPF : Window
    {
        public FinanzasWPF()
        {
            InitializeComponent();
            FinanzasNegocio finanzas = new FinanzasNegocio();
            DataTable datos = new DataTable();
            datos = finanzas.ListarFinanzas();
            dtgridListaFinanzas.ItemsSource = datos.DefaultView;
            conexion.Close();
        }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");



        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            FinanzasNegocio finanzas = new FinanzasNegocio();
            DataTable datos = new DataTable();
            datos = finanzas.ListarFinanzas();
            
            try
            {


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estas son los ingresos encontrados");
                    dtgridListaFinanzas.ItemsSource = datos.DefaultView;
                    conexion.Close();
                }
                else if (datos.DefaultView == null)
                {
                    MessageBox.Show("No existen ingresos en la base de datos");
                    conexion.Close();
                }

            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }
        }

        private void btnIrReportes_Click(object sender, RoutedEventArgs e)
        {
            ReportesWPF ver_reportes = new ReportesWPF();
            ver_reportes.Owner = this;
            ver_reportes.ShowDialog();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
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
        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            TutorialFinanzasWPF ver_tutotial_finanzas = new TutorialFinanzasWPF();
            ver_tutotial_finanzas.Owner = this;
            ver_tutotial_finanzas.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dtgFiltrar.Visibility = Visibility.Hidden;
            dtgFiltrar.IsEnabled = false;
            cboFiltromes.SelectedIndex = 0;
            dtpfechafiltro.Text = string.Empty;
            FinanzasNegocio finanzas = new FinanzasNegocio();
            DataTable datos = new DataTable();
            datos = finanzas.ListarFinanzas();
            dtgridListaFinanzas.ItemsSource = datos.DefaultView;
            conexion.Close();


        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            dtgFiltrar.Visibility = Visibility.Visible;
            dtgFiltrar.IsEnabled = true;
        }

        private void FiltradoMensual(string mes)
        {
            FinanzasNegocio finanzas = new FinanzasNegocio();
            DataTable datos;
            if (cboFiltromes.SelectedIndex != 0)
            {

                switch (mes)
            {
                case "Enero":
                    finanzas.v_Fecha = "1";
                    break;
                case "Febrero":
                    finanzas.v_Fecha = "2";
                    break;
                case "Marzo":
                    finanzas.v_Fecha = "3";
                    break;
                case "Abril":
                    finanzas.v_Fecha = "4";
                    break;
                case "Mayo":
                    finanzas.v_Fecha = "5";
                    break;
                case "Junio":
                    finanzas.v_Fecha = "6";
                    break;
                case "Julio":
                    finanzas.v_Fecha = "7";
                    break;
                case "Agosto":
                    finanzas.v_Fecha = "8";
                    break;
                case "Septiembre":
                    finanzas.v_Fecha = "9";
                    break;
                case "Octubre":
                    finanzas.v_Fecha = "10";
                    break;
                case "Noviembre":
                    finanzas.v_Fecha = "11";
                    break;
                case "Diciembre":
                    finanzas.v_Fecha = "12";
                    break;


            }
                datos = finanzas.ListarporMes();
                dtgridListaFinanzas.ItemsSource = datos.DefaultView;
            }
            else
            {

            }
            
        }

        private void FiltradoDiario()
        {
            if (dtpfechafiltro.Text != string.Empty)
            {
                FinanzasNegocio finanzas = new FinanzasNegocio();
            DataTable datos;
            DateTime fecha = (DateTime)dtpfechafiltro.SelectedDate;
            string formato = "dd/MM/yyyy";
            DateTime fecha_actual = DateTime.Now.Date;
            finanzas.v_Fecha = fecha.ToString(formato);
           
                if(fecha <= fecha_actual)
                {
                    datos = finanzas.ListarPorDia();
                    dtgridListaFinanzas.ItemsSource = datos.DefaultView;
                }
                else
                {
                    MessageBox.Show("La fecha no puede ser mas alla de la actual");
                }
                
            }
            
        }

        private void cboFiltromes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            FiltradoMensual(text);
        }

        private void dtpfechafiltro_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltradoDiario();
        }

        private void dtpfechafiltro_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}

