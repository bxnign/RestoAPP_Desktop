using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using System.Windows.Shapes;
using RestoAPPNegocio;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para AyudaWPF.xaml
    /// </summary>
    public partial class AyudaWPF : Window
    {
        public AyudaWPF()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        public void InfoPersona(DataTable data)
        {
            lblrut.Content = data.Rows[0]["Rut persona"].ToString();
            lblNombre.Content = data.Rows[0]["Nombre persona"].ToString();
        }

        public bool CargarVariables(ref PersonasNegocio person)
        {
            if (ValidacionCasillasVacias())
            {
                person.Asunto = cboAsunto.Text;
                person.Descripcion = txtCuerpo.Text;
                person.Telefono = Convert.ToInt32(txtTelefono.Text);
                person.Rut_Persona = lblrut.Content.ToString();
                return true;
            }

            return false;
        }

        //VALIDACIONES 

        public bool ValidacionCasillasVacias()
        {
            if (txtCorreo.Text == string.Empty || txtCuerpo.Text == string.Empty || txtTelefono.Text == string.Empty || cboAsunto.SelectedItem == cboItemSeleccione)
            {
                MessageBox.Show("Ninguna Casilla debe ir vacia");   
                return false;
            }


            return true;
        }

        public bool LargoTelefono(string dato)
        {
            if(dato.Length == 9)
            {
                if (dato.StartsWith("9"))
                {
                    return true;
                }
            }

            lblfonoerror.Content = "El telefono debe ser de 9 caracteres y debe empezar con el codigo CLP(9)";
            btnEnviar.IsEnabled = false;
            lblfonoerror.Visibility = Visibility.Visible;
            return false;
        }
        public bool ValidacionTelefono(string dato)
        {
            string val2 = "^[0-9]*$";
   
                if (Regex.IsMatch(dato, val2) || dato == string.Empty)
                {
                    return true;
                }
                else
                {
                    lblfonoerror.Content = "Solo se aceptan digitos del 0-9";
                    btnEnviar.IsEnabled = false;
                    lblfonoerror.Visibility = Visibility.Visible;
                    return false;
                }

        }

        public bool ValidacionEmail(string dato)
        {
            string validando;
            validando = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
   
            
                if (Regex.IsMatch(dato, validando) || dato == string.Empty)
                {

                    return true;
                }
                else
                {
                    lblvalidacioncorreo.Content = "El correo no posee el formato correcto";
                    btnEnviar.IsEnabled = false;
                    lblvalidacioncorreo.Visibility = Visibility.Visible;
                    return false;
                }
           

            
        }


        //FIN VALIDACIONES 

        private void btnModInforPersonal_Click(object sender, RoutedEventArgs e)
        {
            PersonasNegocio personas = new PersonasNegocio();
            if (CargarVariables(ref personas))
            {
                try
                {
                    if (personas.HistorialAyuda() == 1)
                    {
                        string nombre, asunto, correo, telefono, cuerpo;
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
                        MailAddress toEmail = new MailAddress("benjaignacioiv@gmail.com", "Someone");
                        MailMessage message = new MailMessage()
                        {
                            From = fromEmail,
                            Subject = cboAsunto.Text,
                            Body = "Rut: " + lblrut + "\n Nombre: " + lblNombre + "\n Numero de Telefono:  " + txtTelefono.Text + "\n Correo :" + txtCorreo.Text + "\n " + txtCuerpo.Text,
                        };
                        message.To.Add(toEmail);
                        try
                        {
                            client.Send(message);
                            MessageBox.Show("Se envio el correo exitosamente");
                            txtCorreo.Text = string.Empty;
                            txtCuerpo.Text = string.Empty;
                            txtTelefono.Text = string.Empty;
                            cboAsunto.SelectedItem = cboItemSeleccione;


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("no se mando el correo" + ex);
                        }
                        
                     }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la Base de Datos " + ex);
                }
               
                
               
            }
           
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void txtTelefono_KeyDown_1(object sender, KeyEventArgs e)
        {

        }



        private void txtTelefono_KeyUp(object sender, KeyEventArgs e)
        {
            if (ValidacionTelefono(txtTelefono.Text))
            {
                if (LargoTelefono(txtTelefono.Text))
                {
                    btnEnviar.IsEnabled = true;
                    lblfonoerror.Visibility = Visibility.Hidden;
                }

               

            }
            else
            {

            }
        }

        private void txtCorreo_KeyUp(object sender, KeyEventArgs e)
        {
            if (ValidacionEmail(txtCorreo.Text))
            {

                btnEnviar.IsEnabled = true;
                lblvalidacioncorreo.Visibility = Visibility.Hidden;

            }
            else
            {

            }
        }
    }
}
