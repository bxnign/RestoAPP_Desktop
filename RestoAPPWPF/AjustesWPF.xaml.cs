using RestoAPPNegocio;
using System;
using System.Collections.Generic;
using System.Data;
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
        public  string rut;
        public void AjustedPersonales(DataTable datos_final)
        {
            rut = datos_final.Rows[0]["Rut persona"].ToString();
            lblRut.Content = "Su Rut es "+datos_final.Rows[0]["Rut persona"].ToString();
            lblNombre.Content = "Nombre: " + datos_final.Rows[0]["Nombre persona"].ToString() +"  "+ datos_final.Rows[0]["Apellido paterno"].ToString();
            lblCargo.Content = "Su cargo es "+datos_final.Rows[0]["Cargo"].ToString();
            lblRol.Content = "Su rol es "+datos_final.Rows[0]["Rol"].ToString();
            txtNombre.Text  = datos_final.Rows[0]["Nombre persona"].ToString();
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
                    MessageBox.Show("Tus datos se Actualizaron");
                    DataTable datos = personas.ListarPorRUT();
                    lblRut.Content = "Su Rut es " + datos.Rows[0]["Rut persona"].ToString();
                    lblNombre.Content = "Nombre: " + datos.Rows[0]["Nombre persona"].ToString() + datos.Rows[0]["Apellido paterno"].ToString();
                    lblCargo.Content = "Su cargo es " + datos.Rows[0]["Cargo"].ToString();
                    lblRol.Content = "Su rol es " + datos.Rows[0]["Rol"].ToString();
                    txtNombre.Text = datos.Rows[0]["Nombre persona"].ToString();
                    txtApellidoPat.Text = datos.Rows[0]["Apellido paterno"].ToString();
                    txtApellidoMat.Text = datos.Rows[0]["Apellido materno"].ToString();
                    dtpFechaNacimiento.Text = datos.Rows[0]["Fecha de nacimiento"].ToString();
                    txtCorreo.Text = datos.Rows[0]["Correo"].ToString();
                    txtPass.Text = datos.Rows[0]["Contraseña"].ToString();
                }
                else
                {
                    MessageBox.Show("Tus datos no se pudieron actualizar :(");
                }

            }catch(Exception ex)
            {
                MessageBox.Show("ERROR DE BASE DE DATOS: \n"+ex);
            }

        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
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
    }
}
