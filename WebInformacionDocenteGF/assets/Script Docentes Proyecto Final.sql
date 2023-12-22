-- Gabriel Figueros Cardona 229287 --

CREATE DATABASE DBCONTACTO;

USE DBCONTACTO;

CREATE TABLE CONTACTO(
	IdContacto int identity,
	Nombres varchar(100),
	Apellidos varchar(100),
	Telefono varchar(100),
	Salario numeric(10, 5),
	FechaNacimiento date,
	Correo varchar(100)
)

INSERT INTO CONTACTO(Nombres, Apellidos, Telefono, Salario, FechaNacimiento, Correo) values
('Gabriel', 'Figueros', '32307740', 8000.00, '2003-05-19', 'gabriel@gmail.com'),
('Jose', 'Rodenas', '52117895',12000.00, '1993-09-11', 'jose@gmail.com'),
('Diego', 'Chamale', '21548962',11500.00, '1999-03-08', 'diego@gmail.com'),
('Leo', 'Castaneda', '78985421',6000.00, '1996-09-12', 'leo@gmail.com'),
('Selvin', 'Icu', '31649785', 15000.00,'1995-12-23', 'selvin@gmail.com')

SELECT * FROM CONTACTO;

GO

CREATE PROCEDURE sp_Registrar(
	@Nombres varchar(100),
	@Apellidos varchar(100),
	@Telefono varchar(100),
	@Salario numeric(10, 5),
	@FechaNacimiento date,
	@Correo varchar(100)
)
AS
BEGIN
	insert into CONTACTO(Nombres, Apellidos, Telefono, Salario, FechaNacimiento, Correo) values (@Nombres, @Apellidos, @Telefono, @Salario, @FechaNacimiento, @Correo)
END

GO

CREATE PROCEDURE sp_Editar(
	@IdContacto int,
	@Nombres varchar(100),
	@Apellidos varchar(100),
	@Telefono varchar(100),
	@Salario numeric(10, 5),
	@FechaNacimiento date,
	@Correo varchar(100)
)
AS
BEGIN
	UPDATE CONTACTO SET Nombres = @Nombres, Apellidos = @Apellidos, Telefono = @Telefono, Salario = @Salario, FechaNacimiento = @FechaNacimiento, Correo = @Correo 
	where IdContacto = @IdContacto
END

GO

CREATE PROCEDURE sp_Eliminar(
	@IdContacto int
)
AS
BEGIN
	DELETE FROM CONTACTO WHERE IdContacto = @IdContacto
END