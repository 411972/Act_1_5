using Actividad_1_5.Data.Contracts;
using Actividad_1_5.Data.Utils;
using Actividad_1_5.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Data.Repositories
{
    public class FormaPagoRepository : ITipoPago
    {
        public FormaPago ObtenerFormaPago(string nombre)
        {
            DataTable dt = null;
            Params paramsql = null;
            FormaPago formaPago = new FormaPago();

            try
            {
                DataHelper.GetInstance().OpenConnection();
                paramsql = new Params("@forma_pago", nombre);

                dt = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_FORMA_PAGO", paramsql);

                foreach (DataRow dr in dt.Rows)
                {
                    formaPago.Codigo = (int)dr["id_forma_pago"];
                    formaPago.Nombre = (string)dr["forma_pago"];
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


            return formaPago;
        }
    }
}
