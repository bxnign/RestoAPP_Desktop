using iText.Signatures;
using RestoAPPNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para PersonasWPF.xaml
    /// </summary>
    public partial class PrincipalWPF : Window
    {
        public DataTable rut;
        public DataTable datos;
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        public PrincipalWPF()
        {
            InitializeComponent();
            lblfecha.Content = DateTime.Now.ToString("dd-MM-yyyy");
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }




        private void Timer_Click(object sender, EventArgs e)

        {
            DateTime d;
            d = DateTime.Now;

            lblhorario.Content = d.Hour + " : " + d.Minute + " : " + d.Second;

        }

       
        public void validacion_rol()
        {
         string rol = rut.Rows[0]["rol"].ToString();
         string usuario = rut.Rows[0]["nombre"].ToString();
         string cargo = rut.Rows[0]["cargo"].ToString();
            lblusuario.Content = usuario;
   
            if (rol == "1")
            {
                gridcontenedorbotones.Height = 650;
                lblrol.Content = "Rol: Administrador - "+ "Cargo: " + cargo;
               


            }
            else if(rol == "2")
            {
                lblrol.Content = "Rol: Bodeguero - " + "Cargo: " + cargo;
                btnFinanzas.IsEnabled = false;
                btnFinanzas.Visibility = Visibility.Hidden;
                btnMenus.IsEnabled = false;
                btnMenus.Visibility = Visibility.Hidden;
                btnMesas.IsEnabled = false;
                btnMesas.Visibility = Visibility.Hidden;
                btnPersonas.IsEnabled = false;
                btnPersonas.Visibility = Visibility.Hidden;
                btnPedidos.IsEnabled = false;
                btnPedidos.Visibility = Visibility.Hidden;
                btnStock.IsEnabled = true;
                btnStock.Visibility = Visibility.Visible;
                btnStock.Margin = new Thickness(45, 10, 0, 0);
                gridcontenedorbotones.Height = 340;
 



            }
            else if (rol == "3")
            {
                lblrol.Content = "Rol: Finanzas - " + "Cargo: " + cargo;
                btnFinanzas.IsEnabled = true;
                btnFinanzas.Visibility = Visibility.Visible;
                btnFinanzas.Margin = new Thickness(45, 10, 0, 0);
                btnMenus.IsEnabled = false;
                btnMenus.Visibility = Visibility.Hidden;
                btnMesas.IsEnabled = false;
                btnMesas.Visibility = Visibility.Hidden;
                btnPersonas.IsEnabled = false;
                btnPersonas.Visibility = Visibility.Hidden;
                btnPedidos.IsEnabled = false;
                btnPedidos.Visibility = Visibility.Hidden;
                btnStock.IsEnabled = false;
                btnStock.Visibility = Visibility.Hidden;
                gridcontenedorbotones.Height = 340;

            }
            else if (rol == "4")
            {
                lblrol.Content = "Rol: Cocina - " + "Cargo: "+cargo;
                btnFinanzas.IsEnabled = false;
                btnFinanzas.Visibility = Visibility.Hidden;
                btnMenus.IsEnabled = true;
                btnMenus.Visibility = Visibility.Visible;
                btnMenus.Margin = new Thickness(45, 10, 0, 0);
                btnMesas.IsEnabled = false;
                btnMesas.Visibility = Visibility.Hidden;
                btnPersonas.IsEnabled = false;
                btnPersonas.Visibility = Visibility.Hidden;
                btnPedidos.IsEnabled = true;
                btnPedidos.Visibility = Visibility.Visible;
                btnPedidos.Margin = new Thickness(45, 121, 0, 0);
                btnStock.IsEnabled = false;
                btnStock.Visibility = Visibility.Hidden;
                gridcontenedorbotones.Height = 340;
               

            }

        }


        private void btnminimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

     
        private void majustes_Click(object sender, RoutedEventArgs e)
        {
            AjustesWPF ver_ajustes = new AjustesWPF();
            ver_ajustes.AjustedPersonales(datos);
            ver_ajustes.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

      
        private void mcerrarprograma_Click(object sender, RoutedEventArgs g)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea salir de RestoApp?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }

        }

        private void mcerrarsesion_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea Cerrar su Sesion?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow ven_main = new MainWindow();
                this.Close();
                ven_main.Show();

            }

    

        }

        private void btnPersonas_Click(object sender, RoutedEventArgs e)
        {
            PersonasWPF ver_personas = new PersonasWPF();
            ver_personas.Owner = this;
            ver_personas.ShowDialog();
        }

        private void btnMenus_Click(object sender, RoutedEventArgs e)
        {
            MenusWPF ver_menus = new MenusWPF();
            ver_menus.Owner = this;
            ver_menus.ShowDialog();
        }

       private void btnMesas_Click(object sender, RoutedEventArgs e)
        {
            MesasWPF ver_mesas = new MesasWPF();
            ver_mesas.ShowDialog();
        }
        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            StockWPF ver_stock = new StockWPF();
            ver_stock.ShowDialog();
        }
        private void btnFinanzas_Click(object sender, RoutedEventArgs e)
        {
            FinanzasWPF ver_finanzas = new FinanzasWPF();
            ver_finanzas.ShowDialog();
        }

        private void btnPedidos_Click(object sender, RoutedEventArgs e)
        {
            PedidosWPF ver_pedidos = new PedidosWPF();
            ver_pedidos.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void mcontacto_Click(object sender, RoutedEventArgs e)
        {
            AyudaWPF ayuda = new AyudaWPF();
            ayuda.Show();
        }

        private void mtutorial_Click(object sender, RoutedEventArgs e)
        {
            TutorialWPF tutorial = new TutorialWPF();
            tutorial.Show();
        }
    }
}
