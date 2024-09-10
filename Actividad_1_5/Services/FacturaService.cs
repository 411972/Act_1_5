using Actividad_1_5.Data.Contracts;
using Actividad_1_5.Data.Repositories;
using Actividad_1_5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Services
{
    public class FacturaService
    {
        private IFacturaRepository _repository;

        public FacturaService()
        {
            _repository = new FacturaRepository();
        }

        public bool GuardarFactura(Factura factura)
        {
            return _repository.CrearFactura(factura);
        }

        public bool ActualizarFactura(Factura factura, int id)
        {
            return _repository.ActualizarFactura(factura, id);
        }

        public bool EliminarFactura(int id)
        {
            return _repository.BorrarFactura(id);
        }

        public List<Factura> ObtenerFacturas()
        {
            return _repository.GetFacturaList();
        }
    }
}
