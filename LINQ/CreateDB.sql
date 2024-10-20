
use master 
go
create database CountriesV2
go
use CountriesV2
go


create table PartsOfTheWorld(
	ID int identity(1,1) not null primary key,
	Name nvarchar(30) constraint UQ_PartOC unique
)
create table BigSities(
	ID int identity(1,1) not null primary key,
	CountOfPersons int not null,
	Name nvarchar(30) constraint UQBC unique,
)
create table CapitalsOfCountries(
	ID int identity(1,1) not null primary key,
	SityId int not null constraint FK_CAPIDFLI foreign key
								(SityId) references BigSities(ID) constraint UQ_SITYR unique,
	Name nvarchar(30) not null,
	CountOfPersons int not null
)
create table Country(
	ID int identity(1,1) not null primary key,
	BigSitiesId int constraint FK_BBS foreign key
								(BigSitiesId) references BigSities(ID),
	CapitalsId int constraint FK_CapitalsId foreign key
								(CapitalsId) references CapitalsOfCountries(ID),
	PartOfTheWorldId int constraint FK_POFWLI foreign key
								(PartOfTheWorldId) references PartsOfTheWorld(ID),
	Name nvarchar(30) not null,
	TotalCountOfPersons int not null,
	SquareOfCountry int not null
)

