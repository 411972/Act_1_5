using Actividad_1_5.Entities;
using Actividad_1_5.Services;

FacturaService facturaManager = new FacturaService();
ArticuloService articuloManager= new ArticuloService();
FormaPagoService formaPagoService = new FormaPagoService();

Articulo oArticulo = null;
Articulo oArticulo2 = null;
Articulo oArticulo3 = null;
FormaPago formaPago = null;  
DetalleFactura oDetalle = new DetalleFactura();
DetalleFactura oDetalle2 = new DetalleFactura();
DetalleFactura oDetalle3 = new DetalleFactura();
Factura oFactura = new Factura();

oArticulo = articuloManager.ObtenerProducto("Arroz");
oArticulo2 = articuloManager.ObtenerProducto("Fideos");
oArticulo3 = articuloManager.ObtenerProducto("Fideos");

formaPago = formaPagoService.ObtenerFormaPago("Efectivo");

oDetalle.Articulo = oArticulo;
oDetalle.Cantidad = 1;

oDetalle2.Articulo = oArticulo2;
oDetalle2.Cantidad = 1;
oDetalle3.Articulo = oArticulo3;
oDetalle3.Cantidad = 1;

oFactura.FormaPago = formaPago;
oFactura.Cliente = "Franco Catania";
oFactura.AgregarDetalle(oDetalle);
oFactura.AgregarDetalle(oDetalle2);
oFactura.AgregarDetalle(oDetalle3);

if (facturaManager.GuardarFactura(oFactura))
{
    Console.WriteLine("Agregado!");
}
else
{
    Console.WriteLine("Error");
}


