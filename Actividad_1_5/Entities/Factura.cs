using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Entities
{
    public class Factura
    {
        public int NroFactura { get; set; }

        public DateTime Fecha { get; set; }

        public FormaPago FormaPago { get; set; }

        public string Cliente { get; set; }

        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }

        public void AgregarDetalle(DetalleFactura df)
        {
            bool encontrado = false;
            foreach(DetalleFactura d in Detalles)
            {
                if(df.Articulo.Codigo == d.Articulo.Codigo)
                {
                    d.Cantidad += df.Cantidad;
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Detalles.Add(df);
            }
            
        }
    }
}
