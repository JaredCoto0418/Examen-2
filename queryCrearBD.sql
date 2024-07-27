create database db_amortizacion;
go
use db_amortizacion;
go
create table Cliente(
	Id int identity(1,1) primary key,
	Nombre varchar(50),
	NumeroIdentidad varchar(50)
);

create table Amortizacion(
	Id int identity(1,1) primary key,
	MontoPrestado decimal(10,2),
	FechaCreacion datetime,
	IdCliente int,
	foreign key(IdCliente) references Cliente(Id)
);

create table PlanAmortizacion(
	Id int identity(1,1) primary key,
	NumeroCuota int,
	FechaPago date,
	Dias int,
	Interes decimal(10,2),
	Capital decimal(10,2),
    PagoNiveladoSinSVSD decimal(10,2),
	PagoNiveladoConSVSD decimal(10,2),
    SaldoCapital decimal(10,2),
	IdAmortizacion int,
	foreign key(IdAmortizacion) references Amortizacion(Id)
)