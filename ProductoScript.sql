
CREATE DATABASE ProductoCRUD

USE ProductoCRUD

CREATE TABLE Producto(
IdProducto int identity primary key,
Nombre varchar(50),
Marca varchar(50),
Categoria varchar (50),
Precio decimal,
Cantidad int
)

INSERT INTO Producto VALUES('Jabón', 'Xedex', 'Limpieza', 50.00, 10)

SELECT * FROM Producto


/*SP*/

/*STORED PROCEDURES*/

CREATE PROCEDURE Listar
AS
BEGIN
	SELECT * FROM Producto
END

CREATE PROCEDURE Obtener(
@IdProducto int
)
AS
BEGIN
	SELECT * FROM Producto WHERE IdProducto = @IdProducto
END

CREATE PROCEDURE Guardar(
@Nombre varchar(50),
@Marca varchar (50),
@Categoria varchar (50),
@Precio decimal,
@Cantidad int
)
AS
BEGIN
	INSERT INTO Producto VALUES (@Nombre, @Marca, @Categoria, @Precio, @Cantidad)
END

CREATE PROCEDURE Editar(
@IdProducto int,
@Nombre varchar(50),
@Marca varchar (50),
@Categoria varchar (50),
@Precio decimal,
@Cantidad int
)
AS
BEGIN
	UPDATE Producto SET Nombre = @Nombre, Marca = @Marca, Categoria = @Categoria, Precio = @Precio, Cantidad = @Cantidad WHERE IdProducto = @IdProducto
END

CREATE PROCEDURE Eliminar(
@IdProducto int
)
AS
BEGIN
	DELETE FROM Producto WHERE IdProducto = @IdProducto
END