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
using System.Text.RegularExpressions;

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


        private bool Validaciones()
        {
            if (ValidarEspaciosEnBlancos(txtRut.Text))
            {
                    if (LargoString(txtRut.Text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            else
            {
                return false;
            }
        }

        public bool LargoString(string rut)
        {

            if (rut.Length <= 7)
            {
                MessageBox.Show("El largo del rut es muy corto \nIngrese minimo 8 caracteres");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidarEspaciosEnBlancos(string dato)
        {
            string val = "^[A-Za-z0-9]*$";
            if (Regex.IsMatch(dato, val))
            {
                return true;
            }

            MessageBox.Show("No se aceptan ESPACIOS ni simbolos como !#$%&/()=?¡-., \n revise su formato e intente nuevamente");
            return false;
        }

        // ------- Metodos Propios ------- //

   


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
                    if (Validaciones())
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
                    else
                    {
                        
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

        private void txtRut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Tab)
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

