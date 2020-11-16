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
        private void dtgridListaStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  CargarCasillasModificar();
          //  int index = dtgridListaFinanzas.CurrentCell.Column.DisplayIndex;
        }

        private void btnIrReportes_Click(object sender, RoutedEventArgs e)
        {
            ReportesWPF ver_reportes = new ReportesWPF();
            ver_reportes.Owner = this;
            ver_reportes.ShowDialog();
        }
    }
}

