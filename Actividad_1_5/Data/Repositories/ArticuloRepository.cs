using Actividad_1_5.Data.Contracts;
using Actividad_1_5.Data.Utils;
using Actividad_1_5.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Data.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        public Articulo ObtenerArticuloPorNombre(string nombre)
        {
            DataTable dt = null;
            Params paramsql = null;
            Articulo articulo = new Articulo();

            try
            {
                DataHelper.GetInstance().OpenConnection();
                paramsql = new Params("@nombre_producto", nombre);

                dt = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_PRODUCTO", paramsql);

                foreach(DataRow dr in dt.Rows)
                {
                    articulo.Codigo = (int)dr["id_articulo"];
                    articulo.Nombre = (string)dr["articulo"];
                    articulo.precioUnitario = Convert.ToDouble(dr["precioUnitario"]);
                }
            }
            catch (Exception exc) 
            {
                throw exc;
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }


            return articulo;
        }
    }
}
