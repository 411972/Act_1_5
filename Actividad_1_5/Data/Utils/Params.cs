using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Data.Utils
{
    public class Params
    {
        public string Nombre { get; set; }

        public object Valor { get; set; }

        public Params(string nombre, object valor) {
            Nombre = nombre;
            Valor = valor;
        }
    }
}
