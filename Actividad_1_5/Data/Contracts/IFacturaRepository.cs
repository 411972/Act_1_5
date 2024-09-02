using Actividad_1_5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Data.Contracts
{
    public interface IFacturaRepository
    {
        bool CrearFactura(Factura factura);
    }
}
