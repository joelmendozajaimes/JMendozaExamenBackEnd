CREATE DATABASE JMendozaExamenBackEnd;

USE JMendozaExamenBackEnd;

CREATE TABLE TipoPersonaFiscal(
	IdTipoPersonaFiscal INT PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR(50) UNIQUE
);

CREATE TABLE Pais(
	IdPais INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50) UNIQUE
);

CREATE TABLE Ocupacion(
	IdOcupacion INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50) UNIQUE
);

CREATE TABLE Sexo(
	IdSexo INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50) UNIQUE
);

CREATE TABLE Banco(
	IdBanco INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50) UNIQUE
);

CREATE TABLE Persona(
	IdPersona INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50),
	ApellidoPaterno VARCHAR(50),
	ApellidoMaterno VARCHAR(50),
	IdTipoPersonaFiscal INT REFERENCES TipoPersonaFiscal(IdTipoPersonaFiscal),
	RFC VARCHAR(13),
	CURP VARCHAR(18),
	IdPais INT REFERENCES Pais(IdPais),
	IdOcupacion INT REFERENCES Ocupacion(IdOcupacion),
	IdSexo INT REFERENCES Sexo(IdSexo),
	Estatus BIT
);

CREATE TABLE Cuenta(
	IdCuenta INT PRIMARY KEY IDENTITY(1,1),
	NumeroCuenta INT UNIQUE,
	IdPersona INT REFERENCES Persona(IdPersona),
	IdBanco INT REFERENCES Banco(IdBanco)
);

INSERT INTO TipoPersonaFiscal(Descripcion)
VALUES
('Física'),
('Moral'),
('Física con Actividad Empresarial');

INSERT INTO Pais(Nombre)
VALUES
('México'),
('USA'),
('Canadá');

INSERT INTO Ocupacion(Nombre)
VALUES
('Empresario'),
('Empleado'),
('Comerciante');

INSERT INTO Sexo(Nombre)
VALUES
('Femenino'),
('Masculino'),
('No Binario');

INSERT INTO Banco (Nombre)
VALUES
('BBVA'),
('Banorte'),
('Banco Azteca'),
('Santander'),
('BanCoppel');

INSERT INTO Persona(Nombre, ApellidoPaterno, ApellidoMaterno, IdTipoPersonaFiscal, RFC, CURP, IdPais, IdOcupacion, IdSexo, Estatus)
VALUES
('Joel Magdiel','Mendoza','Jaimes',1,'MEJJ010420N98','MEJJ010420HDFNMLA7',1,2,2,1);