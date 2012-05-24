 create table persona(
	idUsuario int identity primary key not null,
	nombre varchar(50)not null,
	apellido varchar(50)not null,
	avatar varchar(30)not null,
	ubicacion varchar(50)not null,
	interes text not null
 )

 create table publicacion(
	idPublicacion int identity primary key not null,
	contenido text not null,
	correcciones varchar(10)not null,
	puntaje int not null,
	UserId uniqueidentifier foreign key(UserId)references aspnet_Users(UserId) not null
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