using RestoAPPNegocio;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para AjustesWPF.xaml
    /// </summary>
    public partial class AjustesWPF : Window
    {

        public AjustesWPF()
        {
            InitializeComponent();
        }


        public string rol;
        public string cargo;
        public string rut;
        public void AjustedPersonales(DataTable datos_final)
        {
            rut = datos_final.Rows[0]["Rut persona"].ToString();
            lblRut.Content = "Su Rut es " + datos_final.Rows[0]["Rut persona"].ToString();
            lblNombre.Content = "Nombre: " + datos_final.Rows[0]["Nombre persona"].ToString() + "  " + datos_final.Rows[0]["Apellido paterno"].ToString();
            lblCargo.Content = "Su cargo es " + datos_final.Rows[0]["Cargo"].ToString();
            lblRol.Content = "Su rol es " + datos_final.Rows[0]["Rol"].ToString();
            txtNombre.Text = datos_final.Rows[0]["Nombre persona"].ToString();
            txtApellidoPat.Text = datos_final.Rows[0]["Apellido paterno"].ToString();
            txtApellidoMat.Text = datos_final.Rows[0]["Apellido materno"].ToString();
            dtpFechaNacimiento.Text = datos_final.Rows[0]["Fecha de nacimiento"].ToString();
            txtCorreo.Text = datos_final.Rows[0]["Correo"].ToString();
            txtPass.Text = datos_final.Rows[0]["Contraseña"].ToString();
            rol = datos_final.Rows[0]["Rol"].ToString();
            cargo = datos_final.Rows[0]["Cargo"].ToString();

        }

        private void btnModInforPersonal_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Validacion())
                {
                    PersonasNegocio personas = new PersonasNegocio();
                    personas.Rut_Persona = rut;
                    personas.Nom_Persona = txtNombre.Text;
                    personas.Apel_Pat_Persona = txtApellidoPat.Text;
                    personas.Apel_Mat_Persona = txtApellidoMat.Text;
                    personas.Fecha_Nac = dtpFechaNacimiento.Text;
                    personas.Email = txtCorreo.Text;
                    personas.Pass = txtPass.Text;
                    personas.Id_Rol = rol;
                    personas.Id_Cargo = cargo;
                    if (personas.ModificarPersonas() == 1)
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea Modificar sus datos?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (result == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("Tus datos se Actualizaron");
                            DataTable datos = personas.ListarPorRUT();
                            lblRut.Content = "Su Rut es " + datos.Rows[0]["Rut persona"].ToString();
                            lblNombre.Content = "Nombre: " + datos.Rows[0]["Nombre persona"].ToString() + " " + datos.Rows[0]["Apellido paterno"].ToString();
                            lblCargo.Content = "Su cargo es " + datos.Rows[0]["Cargo"].ToString();
                            lblRol.Content = "Su rol es " + datos.Rows[0]["Rol"].ToString();
                            txtNombre.Text = datos.Rows[0]["Nombre persona"].ToString();
                            txtApellidoPat.Text = datos.Rows[0]["Apellido paterno"].ToString();
                            txtApellidoMat.Text = datos.Rows[0]["Apellido materno"].ToString();
                            dtpFechaNacimiento.Text = datos.Rows[0]["Fecha de nacimiento"].ToString();
                            txtCorreo.Text = datos.Rows[0]["Correo"].ToString();
                            txtPass.Text = datos.Rows[0]["Contraseña"].ToString();
                        }
                            
                    }
                    else
                    {
                        MessageBox.Show("Tus datos no se pudieron actualizar :(");
                    }
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR DE BASE DE DATOS: \n" + ex);
            }

        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApellidoPat.Text = string.Empty;
            txtApellidoMat.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtPass.Text = string.Empty;
            lblRut.Content = string.Empty;
            lblNombre.Content = string.Empty;
            lblCargo.Content = string.Empty;
            lblRol.Content = string.Empty;
            this.Close();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }


        }

        public bool ValidarCorreo(string correo)
        {
            string validando;
            validando = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            if (Regex.IsMatch(correo, validando))
            {
                return true;
            }

            MessageBox.Show("Debe ingresar un formato de correo correcto");
            return false;
        }

        public bool LargoString(string nombre, string apellido, string apellido2)
        {

            if (nombre.Length <= 2 || apellido.Length <= 2 || apellido2.Length <= 2)
            {
                MessageBox.Show("El largo del  nombre o apellidos son muy cortos \n Ingrese minimo 3 caracteres");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ProhibirSimbolos(string nombre, string apellido1, string apellido2)
        {
            string exp;
            exp = "^[A-Za-z]*$";

            if (Regex.IsMatch(nombre, exp) == false || Regex.IsMatch(apellido1, exp) == false || Regex.IsMatch(apellido2, exp) == false)
            {


                MessageBox.Show("No se aceptan caracteres como !#$%&/()=?¡-., \n revise su formato e intente nuevamente");
                return false;
            }
            else
            {
                return true;
            }


        }

        public bool FechaNacimiento()
        {
            DateTime año = (DateTime)dtpFechaNacimiento.SelectedDate;
            int edad = (DateTime.Now.Year - año.Year);
            if (edad >= 16)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Su edad no puede ser menor a 16 años");
                return false;
            }
        }

        public bool ValidarElementosVacios()
        {
            if(txtNombre.Text == string.Empty || txtApellidoPat.Text == string.Empty || txtApellidoMat.Text == string.Empty || txtCorreo.Text == string.Empty || dtpFechaNacimiento.Text == string.Empty ||
                txtPass.Text == string.Empty)
            {
                MessageBox.Show("No debe haber ningun elemento vacio");
                return false;
            }
            return true;
        }

        public bool Validacion()
        {
            if (ValidarElementosVacios())
            {
                if(ValidarCorreo(txtCorreo.Text) && LargoString(txtNombre.Text, txtApellidoPat.Text , txtApellidoMat.Text))
                {
                    if (ProhibirSimbolos(txtNombre.Text, txtApellidoPat.Text, txtApellidoMat.Text) && FechaNacimiento())
                   
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                e.Handled = true;
            }
            else
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

        }


        private void txtApellidoPat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                e.Handled = true;
            }
            else
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
        }
    

        private void txtApellidoMat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                e.Handled = true;
            }
            else
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
        }

        private void dtpFechaNacimiento_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
