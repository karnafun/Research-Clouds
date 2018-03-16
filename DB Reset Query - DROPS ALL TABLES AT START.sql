

drop table Affiliations, UsersInCluster, usersInArticle,KeywordsInCluster,KeywordsInArticle
drop table users, Clusters, Articles, Keywords, AcademicInstitutes
drop view [dbo].[v_ArticleKeywords],[dbo].[v_ClusterKeywords],[dbo].[v_InstituteUsers],[dbo].[v_UserAffiliations],[dbo].[v_UserArticles],[dbo].[v_UserClusters]



create table Users(
[uId] int identity not null,
firstName nvarchar(50) not null,
middleName nvarchar(50) ,
lastName nvarchar(50) not null,
degree nvarchar(50) not null,
imgPath nvarchar(1000) not null,
birthDate datetime ,
registrationDate datetime ,
administrator bit not null,
email nvarchar(250) not null,
uHash nvarchar(max) ,
uSALT nvarchar(max) ,
summery nvarchar(500) 
)
go

create table Clusters(
cId int identity not null,
cName nvarchar(250) not null
)
go

create table Articles(
aId int identity not null,
title nvarchar(1000) not null,
aLink nvarchar(max) not null
) 
go

create table Keywords (
kId int identity not null,
phrase nvarchar(500) not null
)
go

create table AcademicInstitutes (
iId int identity not null,
iName nvarchar(500) not null
)


--Create Connection Tables:
create table KeywordsInCluster(
kId int not null,
cId int not null
)
go


create table KeywordsInArticle(
aId int not null,
kId int not null
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

alter table Clusters
add constraint Clusters_id_PK primary key ([cId])
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
foreign key (cId) references Clusters(cId)
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
foreign key (cId) references Clusters(cId)
go





-- Create Views:

create view v_UserClusters
as
SELECT        dbo.Users.*, dbo.Clusters.*
FROM            dbo.Users INNER JOIN
                         dbo.UsersInCluster ON dbo.Users.uId = dbo.UsersInCluster.uId INNER JOIN
                         dbo.Clusters ON dbo.UsersInCluster.cId = dbo.Clusters.cId
go



create view v_UserArticles
as
SELECT        dbo.Users.*, dbo.Articles.*
FROM            dbo.Articles INNER JOIN
                         dbo.UsersInArticle ON dbo.Articles.aId = dbo.UsersInArticle.aId INNER JOIN
                         dbo.Users ON dbo.UsersInArticle.uId = dbo.Users.uId
go


create view v_UserAffiliations
as
SELECT        dbo.Users.uId, dbo.Users.email, dbo.AcademicInstitutes.iId, dbo.AcademicInstitutes.iName
FROM            dbo.Users INNER JOIN
                         dbo.Affiliations ON dbo.Users.uId = dbo.Affiliations.uId INNER JOIN
                         dbo.AcademicInstitutes ON dbo.Affiliations.iId = dbo.AcademicInstitutes.iId
go


create view v_ClusterKeywords
as
SELECT        dbo.Clusters.cId, dbo.Clusters.cName, dbo.Keywords.kId, dbo.Keywords.phrase
FROM            dbo.Clusters INNER JOIN
                         dbo.KeywordsInCluster ON dbo.Clusters.cId = dbo.KeywordsInCluster.cId INNER JOIN
                         dbo.Keywords ON dbo.KeywordsInCluster.kId = dbo.Keywords.kId
go


create view v_InstituteUsers
as
SELECT        dbo.Users.*, dbo.AcademicInstitutes.*
FROM            dbo.Affiliations INNER JOIN
                         dbo.Users ON dbo.Affiliations.uId = dbo.Users.uId INNER JOIN
                         dbo.AcademicInstitutes ON dbo.Affiliations.iId = dbo.AcademicInstitutes.iId
go

create view v_ArticleKeywords
as
SELECT        dbo.Articles.*, dbo.Keywords.*
FROM            dbo.Articles INNER JOIN
                         dbo.KeywordsInArticle ON dbo.Articles.aId = dbo.KeywordsInArticle.aId INNER JOIN
                         dbo.Keywords ON dbo.KeywordsInArticle.kId = dbo.Keywords.kId
go



--Insert The User:
insert into users values
('Amit', '', 'Rechavi','Ph.D','https://goo.gl/s235Pe',null,null,0,'emailaddress','password',null,'this is my summery')
insert into users values
('Test', 'First', 'User','Ph.D','https://goo.gl/Du4qQK',null,null,0,'ghandi@afterlife.com',null,null,'test summery for first testing user')
insert into users values
('Testing', 'Second', 'Researcher','Ph.D','https://goo.gl/uYwD2J',null,null,0,'RandomEmailAddress',null,null,'test summery for Second testing user')
go


--Insert the Articles
insert into Articles values ('Knowledge and social networks in Yahoo! Answers','https://pdfs.semanticscholar.org/f593/fa3c4862d4befc015b36e7585a388ec98266.pdf')
insert into Articles values('Not all is gold that glitters: Response time & satisfaction rates in yahoo! answers','http://www.academia.edu/download/30720477/4578a904.pdf')
insert into Articles values('Engaging Students in MIS Course through the Creation of e-Businesses: A Self Determination Theory Analysis','http://openaccess.city.ac.uk/18015/8/Engaging%20Students%20in%20MIS%20Final%20Revision%2018%20August%202014.pdf')
insert into Articles values('Hackers topology matter geography: Mapping the dynamics of repeated system trespassing events networks','http://infosoc.haifa.ac.il/images/publications/2015/Hackers_Topology_Matter_Geography.pdf')
go


--Insert Keywords (not related to article yet)
insert into Keywords values('Q&A Sites')
insert into Keywords values('Social Networks')
insert into Keywords values('Social Relationship Networks')
insert into Keywords values('Yahoo Answers')
insert into Keywords values('Response Time')
insert into Keywords values('Satisfaction Rate')
insert into Keywords values('Information Systems')
insert into Keywords values('Manage Information Systems Course')
insert into Keywords values('Encourage Motivation')
insert into Keywords values('Hacking')
go

--Insert Institutions 
insert into AcademicInstitutes values('University of Haifa')
insert into AcademicInstitutes values('Ruppin Academic Center')
go

--Assign Articles To User (uId,aId)
insert into UsersInArticle values(1,1)
insert into UsersInArticle values(1,2)
insert into UsersInArticle values(1,3)
insert into UsersInArticle values(1,4)

--test users:
insert into UsersInArticle values(2,2)
insert into UsersInArticle values(2,1)
insert into UsersInArticle values(3,2)
insert into UsersInArticle values(3,4)
go

--Assign Keywords to Article (aId,kId)
insert into KeywordsInArticle values (1,1)
insert into KeywordsInArticle values (1,2)
insert into KeywordsInArticle values (1,3)
insert into KeywordsInArticle values (1,4)
insert into KeywordsInArticle values (2,4)
insert into KeywordsInArticle values (2,5)
insert into KeywordsInArticle values (2,6)
insert into KeywordsInArticle values (3,7)
insert into KeywordsInArticle values (3,8)
insert into KeywordsInArticle values (3,9)
insert into KeywordsInArticle values (4,2)
insert into KeywordsInArticle values (4,7)
insert into KeywordsInArticle values (4,10)

go

--Assign User To Institutes (uId, iId)
insert into Affiliations values (1,1)
insert into Affiliations values (1,2)

--test users
insert into Affiliations values (2,2)
insert into Affiliations values (3,2)
go

--Create Clusters 
insert into Clusters values ('Social Networks')
insert into Clusters values ('Satisfaction')
go

--Assign user to clusters (uId,cId,Visible)
insert into UsersInCluster values (1,1,1)
insert into UsersInCluster values (1,2,1)

--test users
insert into UsersInCluster values (2,2,1)
insert into UsersInCluster values (3,2,1)
go




-- Add Keywords to cluster !!!!!!!!!!!!!!!!!!!!!


