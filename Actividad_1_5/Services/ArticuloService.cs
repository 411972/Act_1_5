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
    public class ArticuloService
    {
        private IArticuloRepository _repository;

        public ArticuloService()
        {
            _repository = new ArticuloRepository(); 
        }

        public Articulo ObtenerProducto(string nombre)
        {
            return _repository.ObtenerArticuloPorNombre(nombre);
        }
    }
}
