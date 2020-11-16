using RestoAPPNegocio;
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

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para PedidosWPF.xaml
    /// </summary>
    public partial class PedidosWPF : Window
    {
        public PedidosWPF()
        {
            InitializeComponent();
            MostrarPedidos();
        }


        private void Rectangule_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");
        public void MostrarPedidos()
        {

            MenusNegocio menus = new MenusNegocio();
            DataTable datos = new DataTable();
            datos = menus.ListarPedidos();
            dtGridListarPedidos.ItemsSource = datos.DefaultView;
            TextBlock bloque = new TextBlock();
            Button boton = new Button();
            int x = 76;
            int contador = 0;
            int y = 73;
            int bx = 76;
            int by = 273;
            foreach (DataRowView view in dtGridListarPedidos.Items)
            {
                if (Convert.ToInt32(view.Row.ItemArray[4].ToString()) == 1)
                {
                    bloque = new TextBlock();
                    boton = new Button();
                    bloque.Text = "Nro del pedido: " + view.Row.ItemArray[0].ToString() + "\n Descripcion del Pedido \n" + view.Row.ItemArray[1].ToString() + " \n Comentario: "
                    + view.Row.ItemArray[2].ToString() + "\n Hora inicio Pedido \n" + view.Row.ItemArray[3].ToString() + "\n Mesa: " + view.Row.ItemArray[5].ToString();
                    BrushConverter bc = new BrushConverter();
                    boton.Height = 39;
                    boton.Width = 219;
                    boton.HorizontalAlignment = HorizontalAlignment.Left;
                    boton.VerticalAlignment = VerticalAlignment.Top;
                    boton.Margin = new Thickness(x, by, 0, 0);
                    bloque.FontSize = 14;
                    bloque.TextAlignment = TextAlignment.Left;
                    bloque.FontWeight = FontWeights.UltraBold;
                    bloque.HorizontalAlignment = HorizontalAlignment.Left;
                    bloque.VerticalAlignment = VerticalAlignment.Top;
                    bloque.Height = 184;
                    bloque.Width = 290;
                    bloque.Margin = new Thickness(x, y, 0, 0);
                    //   boton.Content = "Pedido Listo Para Servir";
                    //  boton.Click += Boton_Click;
                    //  void Boton_Click(object sender, RoutedEventArgs e)
                    //    {
                    //      MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea cambiar el estado?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    //     if (result == MessageBoxResult.Yes)
                    //     {
                    //        menus.Id_pedido = Convert.ToInt64(view.Row.ItemArray[0].ToString());
                    //         menus.Estado_Pedido = 2;
                    //       try
                    // {
                    //if (menus.ActualizarEstado() == 1)
                    //{
                    //MessageBox.Show("El pedido " + view.Row.ItemArray[0].ToString() + "\n esta listo para entregar \n" + "Mesa: " + view.Row.ItemArray[5].ToString());
                    // }
                    //else
                    // {
                    //MessageBox.Show("No se pudo modificar el estado");
                    //}
                    //}
                    //catch (Exception ex)
                    //{
                    // MessageBox.Show("ERROR DE BASE DE DATOS :" + ex);
                    // }
                    //menus.ActualizarEstado();
                    //conexion.Close();

                    //PedidosWPF recargar = new PedidosWPF();
                    //recargar.Show();
                    //this.Close();
                }


            }
            x = (x + 355);
            contador = contador + 1;
            if (contador == 3)
            {
                x = 66;
                y = (y + 300);
                by = (by + 300);
                contador = 0;
            }
            gridContenedor.Children.Add(bloque);
         //   gridContenedor.Children.Add(boton);
            conexion.Close();

        }


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PedidosWPF pedidos = new PedidosWPF();
            pedidos.Show();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
