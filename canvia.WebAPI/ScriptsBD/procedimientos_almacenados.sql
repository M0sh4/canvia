create proc createUpdateProducto
@nId int = 0,
@cNombre varchar(250),
@cDescripcion varchar(500),
@nPrecio decimal(10,2),
@cRucEmpresa char(11)
as begin
	declare @nExisteEmpresa int = 0;
	select @nExisteEmpresa = count(1) from Empresa where cRuc = @cRucEmpresa;
	if(@nExisteEmpresa != 0)
	begin
		if(@nId != 0)
		begin
			declare @nExiste int = 0;
			select @nExiste = count(1) from Producto where nId = @nId;
			if(@nExiste != 0)
			begin
				update Producto set cNombre = @cNombre, cDescripcion = @cDescripcion, nPrecio = @nPrecio, cRucEmpresa = @cRucEmpresa where nId = @nId
				select * from Producto where nId = @nId;
			end
		end
		else
		begin
			insert into Producto(cNombre,cDescripcion,nPrecio,cRucEmpresa,dtFechaCreacion,cEstado) values(@cNombre,@cDescripcion,@nPrecio,@cRucEmpresa,GETDATE(),1)
			declare @nIdCreado int = (select SCOPE_IDENTITY());
			select * from Producto where nId = @nIdCreado;
		end
	end
end
go
create proc deleteProducto
@nId int,
@operacion int
as begin
	declare @nExiste int = (select count(1) from Producto where nId = @nId);
	if(@nExiste > 0)
	begin
		if(@operacion = 1)
		begin
			delete from Producto where nId = @nId;
		end
		else if(@operacion = 2)
		begin
			update Producto set cEstado = 0 where nId = @nId;
		end
		select 'OK' as response;
	end
	else
	begin
		select 'el producto con ese codigo no existe' as response;
	end
end
go
create proc readProducto
@nId int = 0,
@nRegistros int = 0,
@nPagina int = 0
as begin
	if(@nId = 0)
	begin
		if(@nRegistros = 0)
		begin
			select * from Producto;
		end
		else
		begin
			select * from Producto order by nId asc offset @nPagina * @nRegistros rows
			fetch next @nRegistros rows only
		end
	end
	else
	begin
		select * from Producto where nId = @nId;
	end
end
go
create proc readProductoEmpresa
as begin
	select p.nId, p.cNombre, p.cDescripcion, p.nPrecio, p.dtFechaCreacion, p.cEstado
		, e.cRuc as cRucEmpresa,e.cNombre,e.cDireccion,e.dtFechaCreacion as dtFechaCreacionEmpresa, e.cEstado as cEstadoEmpresa 
		from Producto p join Empresa e on  p.cRucEmpresa = e.cRuc
end
--------------------------------------------
go
create proc createUpdateEmpresa
@cRuc char(11) = 0,
@cNombre varchar(250),
@cDireccion varchar(500)
as begin
	declare @nExiste int = 0;
	select @nExiste = count(1) from Empresa where cRuc = @cRuc;
	if(@nExiste != 0)
	begin
		update Empresa set cNombre = @cNombre, cDireccion = @cDireccion where cRuc = @cRuc
		select * from Empresa where cRuc = @cRuc;
	end
	else
	begin
		insert into Empresa(cRuc,cNombre,cDireccion,dtFechaCreacion,cEstado) values(@cRuc,@cNombre,@cDireccion,GETDATE(),1)
		select * from Empresa where cRuc = @cRuc;
	end
end
go
create proc deleteEmpresa
@cRuc char(11),
@operacion int
as begin
	declare @nExiste int = (select count(1) from Empresa where cRuc = @cRuc);
	if(@nExiste > 0)
	begin
			if(@operacion = 1)
			begin
				declare @nCantidadProductos int = (select count(1) from Producto where cRucEmpresa = @cRuc);
				if(@nCantidadProductos = 0)
				begin
					delete from Empresa where cRuc = @cRuc;
				end
				else
				begin
					select 'No se puede eliminar porque esta empresa contiene productos' as response;
				end
			end
			else if(@operacion = 2)
			begin
				update Empresa set cEstado = 0 where cRuc = @cRuc;
			end
			select 'OK' as response;
	end
	else
	begin
		select 'el producto con ese codigo no existe' as response;
	end
end
go
CREATE proc readEmpresa
@cRuc char(11) = '',
@nRegistros int = 0,
@nPagina int = 0
as begin
	if(@cRuc = '')
	begin
		if(@nRegistros = 0)
		begin
			select * from Empresa;
		end
		else
		begin
			select * from Empresa order by cRuc asc offset @nPagina * @nRegistros rows
			fetch next @nRegistros rows only
		end
	end
	else
	begin
		select * from Empresa where cRuc = @cRuc;
	end
end