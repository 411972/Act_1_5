using Actividad_1_5.Data.Contracts;
using Actividad_1_5.Data.Utils;
using Actividad_1_5.Entities;
using Actividad_1_5.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Data.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        public bool ActualizarFactura(Factura factura, int id)
        {
            bool result = true;
            SqlTransaction t = null;
            int rows = 0;
            int rowsDetalle = 0;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();

                Params idFactura = new Params("@id_factura", id);
                rows = DataHelper.GetInstance().ExecuteSPwParams("SP_ACTUALIZAR_FACTURA", idFactura, t);

                List<Params> lstDetalle = new List<Params>();
                int conteoId = 1;
                
                foreach (DetalleFactura df in factura.Detalles)
                {
                    Params idDetalle = new Params("@id_detalle", conteoId);
                    Params idFac = new Params("@id_factura", id);
                    Params idArt = new Params("@id_articulo", df.Articulo.Codigo);
                    Params cantidad = new Params("@cantidad", df.Cantidad);

                    lstDetalle.Add(idDetalle);
                    lstDetalle.Add(idFac);
                    lstDetalle.Add(idArt);
                    lstDetalle.Add(cantidad);
                    rowsDetalle = DataHelper.GetInstance().ExecuteSPwParams("SP_INSERTAR_DETALLE", lstDetalle, t);
                    lstDetalle.Clear();
                    conteoId++;
                }

                DataHelper.GetInstance().CommitTransaction(t);


            }
            catch (Exception exc)
            {
                DataHelper.GetInstance().RollbackTransaction(t);
                throw exc;
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }

            if (rows > 0 && rowsDetalle > 0)
            {
                return result;
            }
            else
            {
                return result = false;
            }
        }

        public bool BorrarFactura(int id)
        {
            bool result = true;
            int rows = 0;
            SqlTransaction t = null;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();

                Params idFactura = new Params("@id_factura", id);

                rows = DataHelper.GetInstance().ExecuteSPwParams("SP_ELIMINAR_FACTURA", idFactura, t);

                DataHelper.GetInstance().CommitTransaction(t);

            }
            catch (Exception exc)
            {
                DataHelper.GetInstance().RollbackTransaction(t);
                throw exc;
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }

            if(rows > 0)
            {
                return result;
            }
            else
            {
                return result = false;
            }


        }

        public bool CrearFactura(Factura factura)
        {
            bool result = true;
            SqlTransaction t = null;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();
                List<Params>  lstMaestro = new List<Params>();
                Params cliente = new Params("@cliente", factura.Cliente);
                Params tipoPago = new Params("@formaPago", factura.FormaPago.Codigo);
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

        public List<Factura> GetFacturaList()
        {
            FormaPagoService formaPagoService = new FormaPagoService();
            ArticuloService articuloManager = new ArticuloService();
            List<Factura> Facturas = new List<Factura>();
            DataTable dataTableFact = null;
            DataTable dataTableDet = null;
            try
            {
                DataHelper.GetInstance().OpenConnection();
                dataTableFact = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_FACTURAS");

                foreach (DataRow r in dataTableFact.Rows)
                {
                    Factura oFactura = new Factura();
                    oFactura.NroFactura = (int)r["id_factura"];
                    oFactura.Fecha = (DateTime)r["fecha"];
                    oFactura.Cliente = (string)r["cliente"];
                    oFactura.FormaPago = formaPagoService.ObtenerFormaPago((string)r["forma_pago"]);

                    Facturas.Add(oFactura);
                }


                foreach (Factura f in Facturas)
                {
                    DataHelper.GetInstance().OpenConnection();
                    Params p = new Params("@id_factura", f.NroFactura);
                    dataTableDet = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_DETALLES", p);

                    foreach (DataRow r in dataTableDet.Rows)
                    {
                        DetalleFactura df = new DetalleFactura();
                        
                        df.Articulo = articuloManager.ObtenerProducto((string)r["articulo"]);
                        df.Cantidad = (int)r["cantidad"];
                        f.AgregarDetalle(df);
                    }

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

            

            return Facturas;
        }
    }
}
