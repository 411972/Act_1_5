using Actividad_1_5.Entities;
using Actividad_1_5.Services;

FacturaService facturaManager = new FacturaService();
ArticuloService articuloManager= new ArticuloService();
FormaPagoService formaPagoService = new FormaPagoService();

Articulo oArticulo = null;
Articulo oArticulo2 = null;
Articulo oArticulo3 = null;

Articulo oArticulo4 = null;
Articulo oArticulo5 = null;
FormaPago formaPago = null;  
DetalleFactura oDetalle = new DetalleFactura();
DetalleFactura oDetalle2 = new DetalleFactura();
DetalleFactura oDetalle3 = new DetalleFactura();

DetalleFactura oDetalle4 = new DetalleFactura();
DetalleFactura oDetalle5 = new DetalleFactura();
Factura oFactura = new Factura();

oArticulo = articuloManager.ObtenerProducto("Arroz");
oArticulo2 = articuloManager.ObtenerProducto("Fideos");
oArticulo3 = articuloManager.ObtenerProducto("Fideos");

oArticulo4 = articuloManager.ObtenerProducto("Galletas");
oArticulo5 = articuloManager.ObtenerProducto("Leche");

formaPago = formaPagoService.ObtenerFormaPago("Efectivo");

oDetalle.Articulo = oArticulo;
oDetalle.Cantidad = 1;

oDetalle2.Articulo = oArticulo2;
oDetalle2.Cantidad = 1;
oDetalle3.Articulo = oArticulo3;
oDetalle3.Cantidad = 1;

oDetalle4.Articulo = oArticulo4;
oDetalle4.Cantidad = 3;
oDetalle5.Articulo = oArticulo5;
oDetalle5.Cantidad = 1;

oFactura.FormaPago = formaPago;
oFactura.Cliente = "Franco Catania";
oFactura.AgregarDetalle(oDetalle);
oFactura.AgregarDetalle(oDetalle2);
oFactura.AgregarDetalle(oDetalle3);

// CREAR FACTURA

if (facturaManager.GuardarFactura(oFactura))
{
    Console.WriteLine("Factura creada.");
}
else
{
    Console.WriteLine("Error");
}

// ACTUALIZAR FACTURA CON ID 1 O SI NO CAMBIAR EL ID (TIRAR UN SELECT PRIMERO EN LA BD PARA VER LOS ID DISPONIBLES)

oFactura.EliminarDetalles();
oFactura.AgregarDetalle(oDetalle4);
oFactura.AgregarDetalle(oDetalle5);

if (facturaManager.ActualizarFactura(oFactura, 2))
{
    Console.WriteLine("Factura actualizada.");
}
else
{
    Console.WriteLine("Error");
}

// ELIMINAR FACTURA

if (facturaManager.EliminarFactura(1))
{
    Console.WriteLine("Factura eliminada");
}
else
{
    Console.WriteLine("Error");

}

// OBTENER TODAS LAS FACTURAS CON SUS DETALLES

List<Factura> list = facturaManager.ObtenerFacturas();

foreach(Factura f in list)
{
    Console.WriteLine("--------");
    Console.WriteLine("Factura id: " + f.NroFactura);
    Console.WriteLine("Cliente: " + f.Cliente);
    Console.WriteLine("Fecha: " + f.Fecha);
    Console.WriteLine("Tipo pago: " + f.FormaPago.Nombre);
    Console.WriteLine("--------");

    foreach(DetalleFactura df in f.Detalles)
    {
        Console.WriteLine("Articulo: " + df.Articulo.Nombre);
        Console.WriteLine("Cantidad: " + df.Cantidad);
        Console.WriteLine("--------");
    }
}