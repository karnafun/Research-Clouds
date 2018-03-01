
/*
drop table Affiliations, UsersInCluster, usersInArticle,KeywordsInCluster,KeywordsInArticle
drop table users, ResearchClusters, Articles, Keywords, AcademicInstitutes
*/


create table Users(
[uId] int identity not null,
firstName nvarchar(50) not null,
middleName nvarchar(50) ,
lastName nvarchar(50) not null,
degree nvarchar(50) not null,
imgPath nvarchar(1000) not null,
birthDate datetime ,
registrationDAte datetime ,
administrator bit not null,
email nvarchar(250) not null,
uHash nvarchar(max) not null,
uSALT nvarchar(max) not null,
summery nvarchar(500) 
)
go

create table ResearchClusters(
cId int not null,
cName nvarchar(250) not null
)
go

create table Articles(
aId int not null,
title nvarchar(1000) not null,
aLink nvarchar(max) not null
) 
go

create table Keywords (
kId int not null,
phrase nvarchar(500) not null
)
go

create table AcademicInstitutes (
iId int not null,
iName nvarchar(500) not null
)


--Create Connection Tables:
create table KeywordsInCluster(
kId int not null,
cId int not null
)
go


create table KeywordsInArticle(
kId int not null,
aId int not null
)
go

create table UsersInArticle(
[uId] int not null,
aId int not null
)
go

create table Affiliations(
[uId] int not null,
iId int not null
)
go

create table UsersInCluster(
[uId] int not null,
cId int not null,
visible bit not null,
)
go

--Create primary keys

alter table Users
add constraint Users_id_PK primary key ([uId])
go

alter table ResearchClusters
add constraint ResearchClusters_id_PK primary key ([cId])
go

alter table Articles
add constraint Articles_id_PK primary key ([aId])
go

alter table Keywords
add constraint Keywords_id_PK primary key ([kId])
go


alter table AcademicInstitutes
add constraint AcademicInstitutes_id_PK primary key ([iId])
go

alter table KeywordsInCluster
add constraint KeywordsInCluster_kid_PK primary key ([kId],cId)
go

alter table KeywordsInArticle
add constraint KeywordsInArticle_kid_PK primary key ([kId],aId)
go


alter table Affiliations
add constraint Affiliations_uId_PK primary key ([uId],iId)
go

alter table UsersInCluster
add constraint UsersInCluster_uid_PK primary key ([uId],cId)
go
--Creating foreign keys


alter table KeywordsInCluster
add constraint KeywordsInCluster_FK_kId
foreign key (kId) references Keywords(kId),
	constraint KeywordsInCluster_FK_cId
foreign key (cId) references ResearchClusters(cId)
go


alter table KeywordsInArticle
add constraint KeywordsInArticle_FK_kId
foreign key (kId) references Keywords(kId),
	constraint KeywordsInArticle_FK_aId
foreign key (aId) references Articles(aId)
go

alter table UsersInArticle
add constraint UsersInArticle_FK_kId
foreign key ([uId]) references Users([uId]),
	constraint UsersInArticle_FK_aId
foreign key (aId) references Articles(aId)
go

alter table Affiliations
add constraint Affiliations_FK_uId
foreign key ([uId]) references Users([uId]),
	constraint Affiliations_FK_iId
foreign key (iId) references AcademicInstitutes(iId)
go

alter table UsersInCluster
add constraint UsersInCluster_FK_uId
foreign key ([uId]) references Users([uId]),
	constraint UsersInCluster_FK_iId
foreign key (cId) references ResearchClusters(cId)
go


--Insert Values

