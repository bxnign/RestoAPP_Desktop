using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
using RestoAPPNegocio;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para EstadoMesaWPF.xaml
    /// </summary>
    public partial class EstadoMesaWPF : Window
    {
        public EstadoMesaWPF()
        {
            InitializeComponent();
            MostrarMesas();
        }

        OracleConnection conexion = new OracleConnection("user id=topherapp;password=restoapp;data source=" +
                                                         "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)" +
                                                         "(HOST=restaurante.c8e27p3hegzq.us-east-1.rds.amazonaws.com)(PORT=1521))(CONNECT_DATA=" +
                                                         "(SERVICE_NAME=DATABASE)))");

        public void MostrarMesas()
        {
            MesasNegocio mesas = new MesasNegocio();
            DataTable datos = new DataTable();
            datos = mesas.ListarEstadoMesa();
            dtgridMesasEstado.ItemsSource = datos.DefaultView;
            TextBlock bloque = new TextBlock();
            Button boton = new Button();
            
            int x = 66;
            int contador = 0;
            int y = 73;
            int bx = 66;
            int by = 273;
            
            
            foreach (DataRowView view in dtgridMesasEstado.Items)
            {
                bloque = new TextBlock();
                boton = new Button();
                bloque.Text = "\n Id de La mesa: " + view.Row.ItemArray[0].ToString() + " \n Nro de Mesa: "
                + view.Row.ItemArray[1].ToString() + "\n Estado de Mesa: " + view.Row.ItemArray[3].ToString();
                string color = view.Row.ItemArray[4].ToString();
                BrushConverter bc = new BrushConverter();
                if (view.Row.ItemArray[3].ToString() == "Disponible")
                {
                    bloque.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                }
                boton.Height = 39;
                boton.Width = 219;
                boton.HorizontalAlignment = HorizontalAlignment.Left;
                boton.VerticalAlignment = VerticalAlignment.Top;
                boton.Margin = new Thickness(x, by, 0, 0);
                bloque.Background = (Brush)bc.ConvertFrom(color);
                bloque.FontSize = 14;
                bloque.TextAlignment = TextAlignment.Center;
                bloque.FontWeight = FontWeights.UltraBold;
                bloque.HorizontalAlignment = HorizontalAlignment.Left;
                bloque.VerticalAlignment = VerticalAlignment.Top;
                bloque.Height = 184;
                bloque.Width = 219;
                bloque.Margin = new Thickness(x, y, 0, 0);
                
                x = (x + 300);
                contador = contador + 1;
                if (contador == 3)
                {
                    x = 66;
                    y = (y + 300);
                    by = (by + 300);
                    contador = 0;
                }
                boton.Content = "Cambiar Estado";
                boton.Click += Boton_Click;
                 void Boton_Click(object sender, RoutedEventArgs e)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea cambiar el estado?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (view.Row.ItemArray[3].ToString() == "Disponible")
                        {
                            MesasNegocio mesa = new MesasNegocio();
                            string id;
                            id = view.Row.ItemArray[0].ToString();
                            mesa.Id_Mesa = Convert.ToInt32(id);
                            mesa.Estado = "No Disponible";
                            mesa.Modificar_Estado();
                            MessageBox.Show("El estado de la mesa " + id + " se ha cambiado a No disponible");
                            conexion.Close();

                        }
                        else if (view.Row.ItemArray[3].ToString() == "No Disponible")
                        {
                            MesasNegocio mesa = new MesasNegocio();
                            string id;
                            id = view.Row.ItemArray[0].ToString();
                            mesa.Id_Mesa = Convert.ToInt32(id);
                            mesa.Estado = "En Limpieza";
                            mesa.Modificar_Estado();
                            MessageBox.Show("El estado de la mesa " + id + " se ha cambiado a limpieza");
                            conexion.Close();
                        }
                        else
                        {

                            MesasNegocio mesa = new MesasNegocio();
                            string id;
                            id = view.Row.ItemArray[0].ToString();
                            mesa.Id_Mesa = Convert.ToInt32(id);
                            mesa.Estado = "Disponible";
                            mesa.Modificar_Estado();
                            MessageBoxResult mensaje;
                            mensaje = MessageBox.Show("El estado de la mesa " + id + " se ha cambiado a disponible");

                        }
                        EstadoMesaWPF recargar = new EstadoMesaWPF();
                        recargar.Show();
                        this.Close();
                    }
                        
                    

                }

               
                

                gridContenedor.Children.Add(bloque);
                gridContenedor.Children.Add(boton);
            }
            conexion.Close();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            EstadoMesaWPF recargar = new EstadoMesaWPF();
            recargar.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
