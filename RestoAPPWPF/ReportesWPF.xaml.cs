using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using RestoAPPNegocio;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using System.Data.SqlClient;

namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para ReportesWPF.xaml
    /// </summary>
    public partial class ReportesWPF : Window
    {
        public ReportesWPF()
        {
            InitializeComponent();


        }
        OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = admin ; USER ID = TOPHERAPP");


        private void btnReporteIngresos_Click(object sender, RoutedEventArgs e)
        {
            grListarIngresos.Visibility = Visibility.Visible;
            grListarIngresos.IsEnabled = true;
            grListarConsumos.Visibility = Visibility.Hidden;
            grListarConsumos.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListarAtencion.Visibility = Visibility.Hidden;
            grListarAtencion.IsEnabled = false;

            ReportesNegocio reportes = new ReportesNegocio();
            DataTable datos1 = new DataTable();
            DataTable datos2 = new DataTable();
            datos1 = reportes.ListarReporteIngresos();
            datos2 = reportes.ListarReporteEgresos();
            dtgridListaIngresos.ItemsSource = datos1.DefaultView;
            dtgridListaEgresos.ItemsSource = datos2.DefaultView;
            conexion.Close();
        }
        private void btnReporteConsumos_Click(object sender, RoutedEventArgs e)
        {
            grListarIngresos.Visibility = Visibility.Hidden;
            grListarIngresos.IsEnabled = false;
            grListarConsumos.Visibility = Visibility.Visible;
            grListarConsumos.IsEnabled = true;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListarAtencion.Visibility = Visibility.Hidden;
            grListarAtencion.IsEnabled = false;

            ReportesNegocio reportes = new ReportesNegocio();
            DataTable datos1 = new DataTable();
            DataTable datos2 = new DataTable();
            datos1 = reportes.ListarReporteConsumosMenu();
            datos2 = reportes.ListarReporteConsumosPorcion();
            dtgridListaConsumos1.ItemsSource = datos1.DefaultView;
            dtgridListaConsumos2.ItemsSource = datos2.DefaultView;
            conexion.Close();
        }
        private void btnReporteAtencion_Click(object sender, RoutedEventArgs e)
        {
            grListarIngresos.Visibility = Visibility.Hidden;
            grListarIngresos.IsEnabled = false;
            grListarConsumos.Visibility = Visibility.Hidden;
            grListarConsumos.IsEnabled = false;
            grInfo.Visibility = Visibility.Hidden;
            grInfo.IsEnabled = false;
            grListarAtencion.Visibility = Visibility.Visible;
            grListarAtencion.IsEnabled = true;

            ReportesNegocio reportes = new ReportesNegocio();
            DataTable datos = new DataTable();
            datos = reportes.ListarReporteAtencion();
            dtgridListaAtencion.ItemsSource = datos.DefaultView;
            conexion.Close();
        }
        private void btnBuscarListaIngresos_Click(object sender, RoutedEventArgs e)
        {
            ReportesNegocio reportes = new ReportesNegocio();
            DataTable datos1 = new DataTable();
            DataTable datos2 = new DataTable();
            datos1 = reportes.ListarReporteIngresos();
            datos2 = reportes.ListarReporteEgresos();

            try
            {
                if (datos1.DefaultView != null || datos2.DefaultView != null)
                {
                    MessageBox.Show("Esta es la informacion encontrada en la base de datos");
                    dtgridListaIngresos.ItemsSource = datos1.DefaultView;
                    dtgridListaEgresos.ItemsSource = datos2.DefaultView;
                    conexion.Close();
                }
                else if (datos1.DefaultView == null)
                {
                    MessageBox.Show("No existe informacion en la base de datos");
                    conexion.Close();
                }
            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }
        }
        private void btnBuscarListaConsumos_Click(object sender, RoutedEventArgs e)
        {
            ReportesNegocio reportes = new ReportesNegocio();
            DataTable datos1 = new DataTable();
            DataTable datos2 = new DataTable();
            datos1 = reportes.ListarReporteConsumosMenu();
            datos2 = reportes.ListarReporteConsumosPorcion();
            try
            {
                if (datos1.DefaultView != null || datos2.DefaultView != null)
                {
                    MessageBox.Show("Esta es la informacion encontrada en la base de datos");
                    dtgridListaConsumos1.ItemsSource = datos1.DefaultView;
                    dtgridListaConsumos2.ItemsSource = datos2.DefaultView;
                    conexion.Close();
                }
                else if (datos1.DefaultView == null || datos2.DefaultView != null)
                {
                    MessageBox.Show("No existe informacion en la base de datos");
                    conexion.Close();
                }
            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }
        }
        private void btnBuscarListaAtencion_Click(object sender, RoutedEventArgs e)
        {
            ReportesNegocio reportes = new ReportesNegocio();
            DataTable datos = new DataTable();
            datos = reportes.ListarReporteAtencion();

            try
            {
                if (datos.DefaultView != null)
                {
                    MessageBox.Show("Esta es la informacion encontrada en la base de datos");
                    dtgridListaAtencion.ItemsSource = datos.DefaultView;
                    conexion.Close();
                }
                else if (datos.DefaultView == null)
                {
                    MessageBox.Show("No existe informacion en la base de datos");
                    conexion.Close();
                }
            }
            catch
            {
                MessageBox.Show("Se actualizo la lista");
            }
        }

        private void btnGenerarReporteIngresos_Click(object sender, RoutedEventArgs e)
        {
            PdfWriter pdfWriter = new PdfWriter("reporteInicioIngresos.pdf");
            PdfDocument pdf = new PdfDocument(pdfWriter);
            PageSize tamannoH = new PageSize(612, 792);
            Document documento = new Document(pdf, tamannoH);
            Document documento2 = new Document(pdf, tamannoH);

            documento.SetMargins(100, 20, 125, 20);
            documento2.SetMargins(700, 20, 120, 500);

            ReportesNegocio reportes = new ReportesNegocio();
            Table tabla;
            Table tabla2;
            tabla = reportes.ReporteIngresos();
            tabla2 = reportes.ReporteIngresosTotal();
            conexion.Open();

            documento.Add(tabla);
            documento2.Add(tabla2);
            documento.Close();
            documento2.Close();

            var logo = new iText.Layout.Element.Image(ImageDataFactory.Create("modificar banner.png")).SetWidth(50);
            var plogo = new Paragraph("").Add(logo);

            var titulo = new Paragraph("Reporte de ingresos");
            titulo.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            titulo.SetFontSize(12);

            var dfecha = DateTime.Now.ToString("dd-MM-yyyy");
            var dhora = DateTime.Now.ToString("HH:mm:ss");
            var fecha = new Paragraph("Fecha: " + dfecha + "\nHora: " + dhora);
            fecha.SetFontSize(12);

            PdfDocument pdfDoc = new PdfDocument(new PdfReader("reporteInicioIngresos.pdf"), new PdfWriter(@"\ReporteIngresos.pdf"));
            Document doc = new Document(pdfDoc);

            int numeros = pdfDoc.GetNumberOfPages();

            for (int i = 1; i <= numeros; i++)
            {
                PdfPage pagina = pdfDoc.GetPage(i);

                float y = (pdfDoc.GetPage(i).GetPageSize().GetTop() - 15);
                doc.ShowTextAligned(plogo, 40, y, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);
                doc.ShowTextAligned(titulo, 150, y - 15, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);
                doc.ShowTextAligned(fecha, 720, y - 15, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);

                doc.ShowTextAligned(new Paragraph(string.Format("Pagina {0} de {1}", i, numeros)), pdfDoc.GetPage(i).GetPageSize().GetWidth() / 2, pdfDoc.GetPage(i).GetPageSize().GetBottom() + 30, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);

            }
            doc.Close();
            MessageBox.Show("Se genero el reporte en la ruta " + @"D:\ReporteIngresos.pdf");
            conexion.Close();
        }
        private void btnGenerarReporteConsumos_Click(object sender, RoutedEventArgs e)
        {
            PdfWriter pdfWriter = new PdfWriter("ReporteInicioConsumir.pdf");
            PdfDocument pdf = new PdfDocument(pdfWriter);
            PageSize tamannoH = new PageSize(792, 612);
            Document documento = new Document(pdf, tamannoH);
            Document documento2 = new Document(pdf, tamannoH);

            documento.SetMargins(100, 20, 120, 396);
            documento2.SetMargins(100, 396, 120, 20);

            ReportesNegocio reportes = new ReportesNegocio();
            Table tabla;
            Table tabla2;
            tabla = reportes.ReporteMenus();
            tabla2 = reportes.ReportePorciones();
            conexion.Open();

            documento.Add(tabla);
            documento2.Add(tabla2);
            documento.Close();
            documento2.Close();

            var logo = new iText.Layout.Element.Image(ImageDataFactory.Create("modificar banner.png")).SetWidth(50);
            var plogo = new Paragraph("").Add(logo);

            var titulo = new Paragraph("Reporte de Menus y porciones consumidas");
            titulo.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            titulo.SetFontSize(12);

            var dfecha = DateTime.Now.ToString("dd-MM-yyyy");
            var dhora = DateTime.Now.ToString("HH:mm:ss");
            var fecha = new Paragraph("Fecha: " + dfecha + "\nHora: " + dhora);
            fecha.SetFontSize(12);

            PdfDocument pdfDoc = new PdfDocument(new PdfReader("ReporteInicioConsumir.pdf"), new PdfWriter(@"\ReporteDeConsumoDeProductos.pdf"));
            Document doc = new Document(pdfDoc);

            int numeros = pdfDoc.GetNumberOfPages();

            for (int i = 1; i <= numeros; i++)
            {
                PdfPage pagina = pdfDoc.GetPage(i);

                float y = (pdfDoc.GetPage(i).GetPageSize().GetTop() - 15);
                doc.ShowTextAligned(plogo, 40, y, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);
                doc.ShowTextAligned(titulo, 200, y - 15, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);
                doc.ShowTextAligned(fecha, 720, y - 15, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);

                doc.ShowTextAligned(new Paragraph(string.Format("Pagina {0} de {1}", i, numeros)), pdfDoc.GetPage(i).GetPageSize().GetWidth() / 2, pdfDoc.GetPage(i).GetPageSize().GetBottom() + 30, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);

            }
            doc.Close();
            MessageBox.Show("Se genero el reporte en la ruta " + @"D:\ReporteDeConsumoDeProductos.pdf");
            conexion.Close();
        }
        private void btnGenerarReporteAtencion_Click(object sender, RoutedEventArgs e)
        {
            PdfWriter pdfWriter = new PdfWriter("reporteInicioTiempoPedidos.pdf");
            PdfDocument pdf = new PdfDocument(pdfWriter);
            PageSize tamannoH = new PageSize(792, 612);
            Document documento = new Document(pdf, tamannoH);
            Document documento2 = new Document(pdf, tamannoH);

            documento.SetMargins(100, 20, 105, 20);
            documento2.SetMargins(500, 20, 100, 650);

            ReportesNegocio reportes = new ReportesNegocio();
            Table tabla;
            Table tabla2;
            tabla = reportes.ReporteMinutosPedidos();
            tabla2 = reportes.ReporteTotalMinutosPedidos();
            conexion.Open();

            documento.Add(tabla);
            documento2.Add(tabla2);
            documento.Close();
            documento2.Close();

            var logo = new iText.Layout.Element.Image(ImageDataFactory.Create("modificar banner.png")).SetWidth(50);
            var plogo = new Paragraph("").Add(logo);

            var titulo = new Paragraph("Reporte de ingresos");
            titulo.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            titulo.SetFontSize(12);

            var dfecha = DateTime.Now.ToString("dd-MM-yyyy");
            var dhora = DateTime.Now.ToString("HH:mm:ss");
            var fecha = new Paragraph("Fecha: " + dfecha + "\nHora: " + dhora);
            fecha.SetFontSize(12);

            PdfDocument pdfDoc = new PdfDocument(new PdfReader("reporteInicioTiempoPedidos.pdf"), new PdfWriter(@"\reporteTiempoPedidos.pdf"));
            Document doc = new Document(pdfDoc);

            int numeros = pdfDoc.GetNumberOfPages();

            for (int i = 1; i <= numeros; i++)
            {
                PdfPage pagina = pdfDoc.GetPage(i);

                float y = (pdfDoc.GetPage(i).GetPageSize().GetTop() - 15);
                doc.ShowTextAligned(plogo, 40, y, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);
                doc.ShowTextAligned(titulo, 150, y - 15, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);
                doc.ShowTextAligned(fecha, 720, y - 15, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);

                doc.ShowTextAligned(new Paragraph(string.Format("Pagina {0} de {1}", i, numeros)), pdfDoc.GetPage(i).GetPageSize().GetWidth() / 2, pdfDoc.GetPage(i).GetPageSize().GetBottom() + 30, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 0);

            }
            doc.Close();
            MessageBox.Show("Se genero el reporte en la ruta " + @"D:\reporteTiempoPedidos.pdf");
            conexion.Close();
        }
    }
}
