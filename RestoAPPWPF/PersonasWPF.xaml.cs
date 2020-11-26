using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Security.Policy;
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
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Text.RegularExpressions;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para PersonasWPF.xaml
    /// </summary>
    public partial class PersonasWPF : Window
    {
        // conexion a la base de datos (creo que oracle connection para cerrar la conexion abierta en controlador //
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");
        public PersonasWPF()
        {
            InitializeComponent();
            grInfo.Visibility = Visibility.Visible;
            grInfo.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;
            grListar.Visibility = Visibility.Hidden;
            grListar.IsEnabled = false;
        }


        public bool ValidarEspaciosEnBlancos(string dato)
        {
            string val = "^[A-Za-z0-9]*$";
            if (Regex.IsMatch(dato, val))
            {
                return true;
            }

            return false;
        }

        // ------- Metodos Propios ------- //

        public bool ProhibirSimbolos(string rut , string nombre, string apellido1, string apellido2)
        {
            string exp, exp1;
            exp = "^[A-Za-z0-9]*$";
            exp1 = "^[ A-Za-z ]*$";

            if (Regex.IsMatch(rut, exp) == false || Regex.IsMatch(nombre, exp1) == false || Regex.IsMatch(apellido1, exp1) == false || Regex.IsMatch(apellido2, exp1) == false)
            {


                MessageBox.Show("No se aceptan caracteres como !#$%&/()=?¡-., \n revise su formato e intente nuevamente");
                return false;
            }
            else
            {
                return true;
            }


        }
        public bool CargarVariablesAgregar(ref PersonasNegocio personas)
        {



            if (dtpFechaNac.Text == string.Empty || txtRut_personas.Text == string.Empty || txtNombrePer.Text == string.Empty ||
                txtApellidoPat.Text == string.Empty || txtApellidoMat.Text == string.Empty || cboCargo.Text == string.Empty || cboRoles.Text == string.Empty)
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios (*)");
                return false;

            }
            else
            {
                if (cboCargo.SelectedItem != cboitemSeleccione && cboRoles.SelectedItem != cborolSeleccione)
                {

                    if (LargoString(txtRut_personas.Text, txtNombrePer.Text, txtApellidoPat.Text, txtApellidoMat.Text))
                    {
                        DateTime año = (DateTime)dtpFechaNac.SelectedDate;
                        int edad = (DateTime.Now.Year - año.Year);
                        if (edad >= 16)
                        {
                            if (ProhibirSimbolos(txtRut_personas.Text, txtNombrePer.Text, txtApellidoPat.Text, txtApellidoMat.Text))
                            {
                                personas.Rut_Persona = txtRut_personas.Text;
                                personas.Nom_Persona = txtNombrePer.Text;
                                personas.Apel_Pat_Persona = txtApellidoPat.Text;
                                personas.Apel_Mat_Persona = txtApellidoMat.Text;
                                personas.Email = txtCorreo.Text;
                                DateTime fecha = (DateTime)dtpFechaNac.SelectedDate;
                                string formato = "dd/MM/yyyy";
                                personas.Fecha_Nac = fecha.ToString(formato);
                                string contraseña = txtRut_personas.Text;
                                string contraseña_pt2 = fecha.ToString(formato);
                                personas.Pass = contraseña.Substring(0, 2) + contraseña_pt2.Substring(0, 2);
                                personas.Id_Cargo = Convert.ToString(cboCargo.Text);
                                personas.Id_Rol = Convert.ToString(cboRoles.Text);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("El Usuario debe ser mayor de 15 años");
                            return false;
                        }

                    }
                    else
                    {

                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("Debe llenar todos los campos obligatorios (*)");
                    return false;
                }
            }

        }

        public bool LargoString(string rut, string nombre, string apellido, string apellido2)
        {

            if (rut.Length <= 2 || nombre.Length <= 2 || apellido.Length <= 2 || apellido2.Length <= 2)
            {
                MessageBox.Show("El largo del rut , nombre o apellidos son muy cortos \n Ingrese minimo 3 caracteres");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CargarVariablesModificar(ref PersonasNegocio personas)
        {
            if (txtRutMod.Text == string.Empty || txtpassmod.Text == string.Empty || txtNombremod.Text == string.Empty
                 || txtApellidoPatMod.Text == string.Empty || txtApellidoMatMod.Text == string.Empty || dtFechaNacMod.Text == string.Empty
                  || cboCargoMod.Text == string.Empty || cboRolMod.Text == string.Empty)
            {
                MessageBox.Show("Los campos Obligatorios (*) No deben estar vacios");
                return false;

            }
            else
            {
                if (cboCargoMod.SelectedItem != cboitemSeleccioneMOD && cboRolMod.SelectedItem != cborolSeleccioneMOD)
                {
                    if (LargoString(txtRutMod.Text, txtNombremod.Text, txtApellidoPatMod.Text, txtApellidoMatMod.Text))
                    {
                        DateTime año = (DateTime)dtFechaNacMod.SelectedDate;
                        int edad = (DateTime.Now.Year - año.Year);
                        if (edad >= 16)
                        {
                            if (ProhibirSimbolos(txtRutMod.Text, txtNombremod.Text, txtApellidoPatMod.Text, txtApellidoMatMod.Text))
                            {
                                personas.Rut_Persona = txtRutMod.Text;
                                personas.Pass = txtpassmod.Text;
                                personas.Nom_Persona = txtNombremod.Text;
                                personas.Apel_Pat_Persona = txtApellidoPatMod.Text;
                                personas.Apel_Mat_Persona = txtApellidoMatMod.Text;
                                DateTime fecha = (DateTime)dtFechaNacMod.SelectedDate;
                                string formato = "dd/MM/yyyy";
                                personas.Email = txtCorreoMod.Text;
                                personas.Fecha_Nac = fecha.ToString(formato);
                                personas.Id_Cargo = Convert.ToString(cboCargoMod.Text);
                                personas.Id_Rol = Convert.ToString(cboRolMod.Text);

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("El Usuario debe ser mayor de 15 años");
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("Los campos Obligatorios (*) No deben estar vacios");
                    return false;

                }

            }

        }

        public void VaciarCasillasModificar()
        {
            txtRutMod.Text = string.Empty;
            txtNombremod.Text = string.Empty;
            txtApellidoPatMod.Text = string.Empty;
            txtApellidoMatMod.Text = string.Empty;
            dtFechaNacMod.Text = string.Empty;
            cboCargoMod.Text = string.Empty;
            cboRolMod.Text = string.Empty;
            txtpassmod.Text = string.Empty;
            txtCorreoMod.Text = string.Empty;

        }

        public void VaciarCasillasAgregar()
        {
            txtRut_personas.Text = string.Empty;
            txtNombrePer.Text = string.Empty;
            txtApellidoPat.Text = string.Empty;
            txtApellidoMat.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            dtpFechaNac.Text = string.Empty;
            cboCargo.Text = string.Empty;
            cboRoles.Text = string.Empty;
        }

        public bool ValidarCorreoAgregar()
        {
            string validando;
            validando = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            if (Regex.IsMatch(txtCorreo.Text, validando))
            {
                return true;
            }

            return false;
        }


        public bool ValidarCorreoModificar()
        {
            string validando;
            validando = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            if (Regex.IsMatch(txtCorreoMod.Text, validando))
            {
                return true;
            }

            return false;
        }


        public void CargarCasillasModificar()
        {


            if (dtgridListaPersonas.SelectedItem == null)
            {

            }
            else
            {
                DataRowView view = (DataRowView)dtgridListaPersonas.SelectedItem;
                txtRutMod.Text = view.Row.ItemArray[0].ToString();
                txtNombremod.Text = view.Row.ItemArray[1].ToString();
                txtApellidoPatMod.Text = view.Row.ItemArray[2].ToString();
                txtApellidoMatMod.Text = view.Row.ItemArray[3].ToString();
                dtFechaNacMod.Text = view.Row.ItemArray[4].ToString();
                txtpassmod.Text = view.Row.ItemArray[5].ToString();
                cboCargoMod.Text = view.Row.ItemArray[6].ToString();
                cboRolMod.Text = view.Row.ItemArray[7].ToString();
                txtCorreoMod.Text = view.Row.ItemArray[8].ToString();
               // txtRutMod.IsEnabled = false;
            }
        }




        private void EnviarEmail()
        {

            PersonasNegocio personas = new PersonasNegocio();
            DateTime fecha = (DateTime)dtpFechaNac.SelectedDate;
            string formato = "dd/MM/yyyy";
            string contraseña = txtRut_personas.Text;
            string contraseña_pt2 = fecha.ToString(formato);
            string contrasenna = contraseña.Substring(0, 2) + contraseña_pt2.Substring(0, 2);
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "restoapp.app@gmail.com",
                    Password = "restomundo"
                }
            };
            MailAddress fromEmail = new MailAddress("restoapp.app@gmail.com", "restomundo");
            MailAddress toEmail = new MailAddress(txtCorreo.Text, "Someone");
            MailMessage message = new MailMessage()
            {
                From = fromEmail,
                Subject = "Bienvenido a Restaurant XXI",
                Body = "Se genero un usuario en el restaurant RestoAPP \n" + txtNombrePer.Text + " " + txtApellidoPat.Text + " " + txtApellidoMat.Text + "\n" + "Usuario: " + txtRut_personas.Text + " y contraseña: " + contrasenna + "\n" + "Porfavor Actualice su contraseña en: http://127.0.0.1:8000/accounts/password_reset/"
            };
            message.To.Add(toEmail);
            try
            {
                client.Send(message);
                MessageBox.Show("Se envio el correo exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("no se mando el correo" + ex);
            }
        }

        // ------- Metodos Propios ------- //


        // ------- Metodos de Controles ------- //

        // Mantenedor Agregar Listo 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PersonasNegocio personas = new PersonasNegocio();
            if (CargarVariablesAgregar(ref personas) == true)
            {
                if (LargoString(txtRut_personas.Text, txtNombrePer.Text, txtApellidoPat.Text, txtApellidoMat.Text))
                {
                    if (ValidarEspaciosEnBlancos(txtRut_personas.Text) == false)
                    {
                        MessageBox.Show("No pueden Existir Espacios en el RUT");
                    }
                    else
                    {
                        try
                        {
                            if (ValidarCorreoAgregar())
                            {
                                if (personas.Agregar() == 1)
                                {
                                    MessageBox.Show("El usuario se creo correctamente.  \n" + "Se Genero la contraseña: " + personas.Pass);
                                    EnviarEmail();
                                    VaciarCasillasAgregar();
                                    conexion.Close();
                                }
                                else
                                {
                                    MessageBox.Show("El usuario ya existe o no se ingresaron todos los datos");
                                    conexion.Close();
                                }

                            }
                            else
                            {
                                MessageBox.Show("El Email ingresado no cumple con el formato de un correo");
                                conexion.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error de Conexion: " + ex);
                            conexion.Close();
                        }
                    }

                }
            }
            else
            {

            }

        }

        // Mantenedor listar todos listos 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PersonasNegocio personas = new PersonasNegocio();
            DataTable datos = new DataTable();
            datos = personas.ListarPersonas();

            try
            {


                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Estos son todos los usuarios encontrados");
                    dtgridListaPersonas.ItemsSource = datos.DefaultView;
                    conexion.Close();
                }
                else if (datos.DefaultView == null)
                {
                    MessageBox.Show("No existe personas en la base de datos");
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha actualizado la lista");
            }

        }

        // Mantenedor Modificar listos 
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PersonasNegocio personas = new PersonasNegocio();
            if (CargarVariablesModificar(ref personas))
            {

                if (LargoString(txtRutMod.Text, txtNombremod.Text, txtApellidoPatMod.Text, txtApellidoMatMod.Text))
                {
                    if (ValidarEspaciosEnBlancos(txtRutMod.Text) == false)
                    {
                        MessageBox.Show("No deben haber espacios en el Rut");
                    } else
                    {
                        try
                        {
                            if (ValidarCorreoModificar())
                            {
                                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea Modificar esta persona?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                                if (result == MessageBoxResult.Yes)
                                {

                                    if (personas.ModificarPersonas() == 1)
                                    {
                                        MessageBox.Show("El usuario se modifico correctamente");
                                        VaciarCasillasModificar();
                                        conexion.Close();
                                    }

                                    else
                                    {
                                        MessageBox.Show("El usuario no existe");
                                        conexion.Close();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("El email ingresado no cumple con el formato de un correo");

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error de Conexion: " + ex);
                            conexion.Close();
                        }
                    }

                }

            }
        }


        // ir a modificar desde la lista 
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            PersonasNegocio personas = new PersonasNegocio();
            DataRowView view = (DataRowView)dtgridListaPersonas.SelectedItem;
            string rut;
            rut = view.Row.ItemArray[0].ToString();
            personas.Rut_Persona = rut;

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("¿Esta seguro que desea eliminar esta persona?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (personas.Eliminar() == 1)
                    {
                        MessageBox.Show("Se Elimino La persona Exitosamente");
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la persona");
                        conexion.Close();
                    }

                }

            } catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex);
            }
        }

        private void Button_ClickAgregar(object sender, RoutedEventArgs e)
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

        private void Button_ClickModificar(object sender, RoutedEventArgs e)
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

        private void Button_ClickListar(object sender, RoutedEventArgs e)
        {
            dtgridListaPersonas.IsEnabled = true;
            grListar.Visibility = Visibility.Visible;
            grListar.IsEnabled = true;
            grAgregar.Visibility = Visibility.Hidden;
            grAgregar.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grModificar.Visibility = Visibility.Hidden;
            grModificar.IsEnabled = false;

            PersonasNegocio personas = new PersonasNegocio();
            DataTable datos = new DataTable();
            datos = personas.ListarPersonas();

            dtgridListaPersonas.ItemsSource = datos.DefaultView;
            conexion.Close();

        }

        private void BuscarRut_KeyUp(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text == string.Empty)
            {
                PersonasNegocio personas = new PersonasNegocio();
                DataTable datos = new DataTable();
                datos = personas.ListarPersonas();
                dtgridListaPersonas.ItemsSource = datos.DefaultView;
            }
            else
            {
                PersonasNegocio personas = new PersonasNegocio();
                DataTable datos = new DataTable();
                personas.Rut_Persona = txtRut.Text;
                datos = personas.ListarPorRUT();
                dtgridListaPersonas.ItemsSource = datos.DefaultView;
                conexion.Close();
            }

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

        // VALIDACIONES TEXTBOX AGREGAR
        private void txtRut_personas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                e.Handled = true;
            }
            else
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

        private void txtNombrePer_KeyDown(object sender, KeyEventArgs e)
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

        private void dtpFechaNac_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        // VALIDACION TEXTBOX MODIFICAR

        private void txtRutMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                e.Handled = true;
            }
            else
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

        private void txtNombremod_KeyDown(object sender, KeyEventArgs e)
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

        private void txtpassmod_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtApellidoPatMod_KeyDown(object sender, KeyEventArgs e)
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



    private void txtApellidoMatMod_KeyDown(object sender, KeyEventArgs e)
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

        private void dtFechaNacMod_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void txtRut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl || e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                e.Handled = true;
            }
            else
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


        // ------- Metodos de Controles ------- //


    }

}


