

create database facturaciones

go 
use facturaciones
go


create table FORMAS_PAGOS(
	id_forma_pago INT identity(1,1),
	forma_pago varchar(20)

	constraint PK_FORMA_PAGO primary key(id_forma_pago)
);

create table ARTICULOS(
	id_articulo int identity(1,1),
	articulo varchar(50),
	precioUnitario decimal(10,2)

	constraint PK_ARTICULOS primary key(id_articulo) 
);

create table FACTURAS(
	id_factura INT identity(1,1),
	fecha datetime not null,
	formaPago int not null,
	cliente varchar(30) not null

	constraint PK_FACTURAS PRIMARY KEY(id_factura)
	constraint FK_FACTURA_PAGOS FOREIGN KEY(formaPago)
		references FORMAS_PAGOS(id_forma_pago)
);

create table DETALLES_FACTURAS(
	id_detalle INT not null,
	id_factura INT not null,
	id_articulo INT not null,
	cantidad INT not null

	CONSTRAINT PK_DETALLES PRIMARY KEY(id_detalle, id_factura)
	CONSTRAINT FK_DETALLES_FACTURAS FOREIGN KEY(id_factura)
		REFERENCES FACTURAS(id_factura),
	CONSTRAINT FK_DETALLE_ARTICULO FOREIGN KEY(id_articulo)
		REFERENCES ARTICULOS(id_articulo)

);

go
INSERT INTO ARTICULOS(articulo, precioUnitario)
			VALUES('Arroz', 1200);
INSERT INTO ARTICULOS(articulo, precioUnitario)
			VALUES('Fideos', 1370);
INSERT INTO ARTICULOS(articulo, precioUnitario)
			VALUES('Galletas', 890);
INSERT INTO ARTICULOS(articulo, precioUnitario)
			VALUES('Leche', 2000);
INSERT INTO ARTICULOS(articulo, precioUnitario)
			VALUES('Queso', 1560);

INSERT INTO FORMAS_PAGOS(forma_pago)
			values('Efectivo')
INSERT INTO FORMAS_PAGOS(forma_pago)
			values('Debito')
INSERT INTO FORMAS_PAGOS(forma_pago)
			values('QR')
go
set dateformat dmy

INSERT INTO FACTURAS(fecha, formaPago, cliente)
			VALUES('12-02-2024', 1, 'Franco Catania')
INSERT INTO FACTURAS(fecha, formaPago, cliente)
			VALUES('14-03-2024', 1, 'Mauro Catania')
INSERT INTO FACTURAS(fecha, formaPago, cliente)
			VALUES('11-05-2024', 1, 'Lionel Messi')

INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(1,1,2,2)
INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(2,1,1,2)
INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(3,1,3,2)

INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(1,2,1,2)
INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(2,2,3,1)

INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(1,3,2,1)
INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(2,3,1,2)
INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
			VALUES(3,3,4,2)

go
CREATE PROCEDURE SP_INSERTAR_MAESTRO
@formaPago int,
@cliente varchar(30),
@id_factura int output
AS
BEGIN
	INSERT INTO FACTURAS(fecha, formaPago, cliente)
				VALUES(GETDATE(), @formaPago, @cliente);
	SET @id_factura = SCOPE_IDENTITY();
END

go
CREATE PROCEDURE SP_INSERTAR_DETALLE
@id_detalle INT,
@id_factura INT,
@id_articulo INT,
@cantidad INT
AS
BEGIN
	INSERT INTO DETALLES_FACTURAS(id_detalle, id_factura, id_articulo, cantidad)
		VALUES(@id_detalle, @id_factura, @id_articulo,@cantidad)
END

go
create PROCEDURE SP_OBTENER_PRODUCTO
@nombre_producto varchar(30)
AS
BEGIN
	SELECT * FROM ARTICULOS
	WHERE articulo = @nombre_producto
END

go
create PROCEDURE SP_OBTENER_FORMA_PAGO
@forma_pago varchar(30)
AS
BEGIN
	SELECT * FROM FORMAS_PAGOS
	WHERE forma_pago = @forma_pago
END

go
create procedure SP_ACTUALIZAR_FACTURA
@id_factura int 
AS
BEGIN
DELETE FROM DETALLES_FACTURAS
WHERE id_factura = @id_factura
END

go
create PROCEDURE SP_ELIMINAR_FACTURA
@id_factura int
AS
BEGIN
	DELETE FROM DETALLES_FACTURAS
	WHERE id_factura = @id_factura

	DELETE FROM FACTURAS
	WHERE id_factura = @id_factura
END

go
create PROCEDURE SP_OBTENER_FACTURAS
AS
BEGIN
  SELECT f.id_factura, f.fecha, fp.forma_pago, f.cliente FROM FACTURAS f
  join FORMAS_PAGOS fp on f.formaPago = fp.id_forma_pago

END

go
create PROCEDURE SP_OBTENER_DETALLES
@id_factura int 
AS
BEGIN

  SELECT f.id_factura, a.articulo, df.cantidad FROM FACTURAS f
  JOIN DETALLES_FACTURAS df on f.id_factura = df.id_factura
  join ARTICULOS a on df.id_articulo = a.id_articulo
  where df.id_factura = @id_factura
END