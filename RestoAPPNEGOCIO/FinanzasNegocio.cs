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
    public class FinanzasNegocio
    {
        public int Id_registro { get; set; }
        public int Ingreso { get; set; }
        public int Id_boleta { get; set; }
        public DataTable ListarFinanzas()
        {
            return new FinanzasBD().ListarFinanzas();
        }
    }
}
