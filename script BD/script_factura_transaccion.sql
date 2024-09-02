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


create PROCEDURE SP_OBTENER_PRODUCTO
@nombre_producto varchar(30)
AS
BEGIN
	SELECT * FROM ARTICULOS
	WHERE articulo = @nombre_producto
END

create PROCEDURE SP_OBTENER_FORMA_PAGO
@forma_pago varchar(30)
AS
BEGIN
	SELECT * FROM FORMAS_PAGOS
	WHERE forma_pago = @forma_pago
END

