using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        private void btnModInforPersonal_Click(object sender, RoutedEventArgs e)
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
                Body =  txtNombre.Text + "\n Numero de Telefono:  " + txtTelefono.Text + "\n Correo :" +txtCorreo.Text+ "\n "+txtCuerpo.Text,
            };
            message.To.Add(toEmail);
            try
            {
                client.Send(message);
                MessageBox.Show("Se envio el correo exitosamente");
                txtCorreo.Text = string.Empty;
                txtCuerpo.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtTelefono.Text = string.Empty;
                cboAsunto.SelectedItem = cboItemSeleccione;


            }
            catch (Exception ex)
            {
                MessageBox.Show("no se mando el correo" + ex);
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
