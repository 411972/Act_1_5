using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Entities
{
    public class DetalleFactura
    {
        public Articulo Articulo { get; set; }

        public int Cantidad { get; set; }

        public double Subtotal()
        {
            return Articulo.precioUnitario * Cantidad;
        }
    }
}
