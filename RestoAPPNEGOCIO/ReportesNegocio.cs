using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using RestoAPPBD;
using System.Data;

namespace RestoAPPNegocio
{
    public class ReportesNegocio
    {
        public DataTable ListarReporteIngresos()
        {
            return new ReportesBD().ListarReporteIngresos();
        }
        public DataTable ListarReporteAtencion()
        {
            return new ReportesBD().ListarReporteAtencion();
        }
        public Table ReporteIngresos()
        {
            return new ReportesBD().ReporteIngresos();
        }
        public Table ReporteIngresosTotal()
        {
            return new ReportesBD().ReporteIngresosTotal();
        }
        public Table ReporteMenus()
        {
            return new ReportesBD().ReporteMenus();
        }
        public Table ReportePorciones()
        {
            return new ReportesBD().ReportePorciones();
        }
        public Table ReporteTotalMinutosPedidos()
        {
            return new ReportesBD().ReporteTotalMinutosPedidos();
        }
        public Table ReporteMinutosPedidos()
        {
            return new ReportesBD().ReporteMinutosPedidos();
        }
    }
}
