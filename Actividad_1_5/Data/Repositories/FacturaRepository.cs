using Actividad_1_5.Data.Contracts;
using Actividad_1_5.Data.Utils;
using Actividad_1_5.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Data.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        public bool CrearFactura(Factura factura)
        {
            bool result = true;
            SqlTransaction t = null;

            try
            {
                //DataHelper.GetInstance().OpenConnection();
                t = DataHelper.GetInstance().beginTransaction();
                List<Params>  lstMaestro = new List<Params>();
                Params cliente = new Params("@cliente", factura.Cliente);
                Params tipoPago = new Params("@formaPago", factura.FormaPago.Codigo); //revisar
                lstMaestro.Add(cliente);
                lstMaestro.Add(tipoPago);
                int idFactura = DataHelper.GetInstance().ExecuteSPwParams("SP_INSERTAR_MAESTRO", lstMaestro, t);

                List<Params> lstDetalle = new List<Params>();
                int conteoId = 1;
                int rows;
                foreach (DetalleFactura df in factura.Detalles)
                {
                    Params idDetalle = new Params("@id_detalle", conteoId);
                    Params idFac = new Params("@id_factura", idFactura);
                    Params idArt = new Params("@id_articulo", df.Articulo.Codigo);
                    Params cantidad = new Params("@cantidad", df.Cantidad);

                    lstDetalle.Add(idDetalle);
                    lstDetalle.Add(idFac);
                    lstDetalle.Add(idArt);
                    lstDetalle.Add(cantidad);
                    rows = DataHelper.GetInstance().ExecuteSPwParams("SP_INSERTAR_DETALLE", lstDetalle, t);
                    lstDetalle.Clear();
                    conteoId++;
                }



                DataHelper.GetInstance().CommitTransaction(t);

            }
            catch (Exception)
            {
                result = false;
                DataHelper.GetInstance().RollbackTransaction(t);
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }


            return result;
        }
    }
}
