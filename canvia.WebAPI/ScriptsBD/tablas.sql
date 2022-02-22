create table Producto(
	nId int primary key identity,
	cNombre varchar(250),
	cDescripcion varchar(500),
	nPrecio decimal(10,2),
	cRucEmpresa char(11),
	dtFechaCreacion Datetime,
	cEstado char(1)
);
go
create table Empresa(
	cRuc char(11) primary key,
	cNombre varchar(250),
	cDireccion varchar(500),
	dtFechaCreacion Datetime,
	cEstado char(1)
);
go
ALTER TABLE Producto ADD FOREIGN KEY (cRucEmpresa) REFERENCES Empresa(cRuc)
go
create nonclustered index IDX_cRucProducto on Producto(cRucEmpresa)
