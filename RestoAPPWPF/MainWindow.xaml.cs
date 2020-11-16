using System;
using System.Collections.Generic;
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
using RestoAPPNegocio;
using RestoAPPBD;
using System.Data.OracleClient;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");


        public MainWindow()
        {
            InitializeComponent();
        }



        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            PersonasNegocio personas = new PersonasNegocio();
            personas.Rut_Persona_val = txtRut.Text;
            personas.Rut_Persona = txtRut.Text;
            personas.Pass = txtContraseña.Password;
            
            try
            {
                if (txtRut.Text == string.Empty || txtContraseña.Password == string.Empty)
                {
                    MessageBox.Show("Debe llenar todos los campos para ingresar");
                }else
                {
                    if (personas.Login())
                    {
                        PrincipalWPF ventana_principal = new PrincipalWPF();               
                        ventana_principal.rut = personas.Validacion();
                        ventana_principal.datos = personas.ListarPorRUT();
                        ventana_principal.validacion_rol();
                        ventana_principal.Show();
                        this.Hide();
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("El usuario y la contraseña no existen o son incorrectas");

                        conexion.Close();
                    }

                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error de Conexion: "+ ex);
                conexion.Close();

            }
           //PrincipalWPF ventana_principal = new PrincipalWPF();
           //this.Close();
           //ventana_principal.Show();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea salir de RestoApp?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }

    }

