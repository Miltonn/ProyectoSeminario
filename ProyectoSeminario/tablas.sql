 create table perfilusers(
	idPersona int identity primary key not null,
	nombre varchar(50)not null,
	apellido varchar(50)not null,
	avatar varchar(30)not null,
	ubicacion varchar(50)not null,
	interes text not null,
	UserId uniqueidentifier foreign key(UserId)references aspnet_Users(UserId) not null
 )

 create table perfil(
	idPerfil int identity primary key not null,
	infraccion int not null,
	karma float(5) not null,
	beneado varchar(10)not null,
	UserId uniqueidentifier foreign key(UserId)references aspnet_Users(UserId) not null
 )

 create table categoria(
	idCategoria int identity primary key not null,
	tipo varchar(50)not null
 )
 
create table categorizacion(
	idCategorizacion int identity primary key not null,
	idCategoria int foreign key(idCategoria)references categoria(idCategoria)not null,
	idPublicacion int foreign key(idPublicacion)references publicacion(idPublicacion)not null
 )
 
 create table publicacion(
	idPublicacion int identity primary key not null,
	titulo varchar(50)not null,
	portada varchar(50)not null,
	contenido text not null,
	correcciones varchar(10)not null,
	puntaje int not null,
	fecha_publicacion datetime not null,
	descripcion varchar(100)not null,
	UserId uniqueidentifier foreign key(UserId)references aspnet_Users(UserId) not null
 ) 
 
 create table libro(
	idLibro int identity primary key not null,
	autor varchar(50)not null,
	año_publicacion varchar(50)not null,
	idioma varchar(50)not null,
	tamaño varchar(50)not null,
	idPublicacion int foreign key(idPublicacion)references publicacion(idPublicacion)not null
 )
 
 create table indice(
	idIndice int identity primary key not null,
	nombre_indice varchar(50) not null,
	url text not null,
	idPublicacion int foreign key(idPublicacion)references publicacion(idPublicacion)not null
 )
 
 create table comentarios(
	idComentarios  int identity primary key not null,
	contenido text not null,
	vistobueno varchar(10)not null,
	fecha_publicacion datetime not null,
	idPublicacion int foreign key(idPublicacion)references publicacion(idPublicacion)not null,
	UserId uniqueidentifier foreign key(UserId)references aspnet_Users(UserId) not null
 )
 
 create table articulo(
	idArticulo int identity primary key not null,
	idPublicacion int foreign key(idPublicacion)references publicacion(idPublicacion)not null
 )
 
 create table curso(
	idCurso int identity primary key not null,
	idPublicacion int foreign key(idPublicacion)references publicacion(idPublicacion)not null
 )
 
 create table tutorial(
	idTutorial int identity primary key not null,
	idPublicacion int foreign key(idPublicacion)references publicacion(idPublicacion)not null
 )