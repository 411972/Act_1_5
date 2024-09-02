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
    public class FormaPagoService
    {
        private ITipoPago _repository;

        public FormaPagoService()
        {
            _repository = new FormaPagoRepository();
        }

        public FormaPago ObtenerFormaPago(string nombre)
        {
            return _repository.ObtenerFormaPago(nombre);
        }
    }
}
