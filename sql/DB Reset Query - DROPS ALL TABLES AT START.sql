/*
	Part 1: Drop Data
	Part 2: Tables
	Part 3: Relationship Tables
	Part 4: Primary Keys
	Part 5: Foreign Keys
	Part 6: Views
	Part 7: Procedures
	Part 8: User Data
	Part 9: Article Data (Soccer DB)
	Part 10: Keywords Data (Soccer DB)
	Part 11: Cluster Data (Soccer DB)
	Part 12: Institutes data  (Soccer DB)
	Part 13: User-Clusters Relationship Data (Soccer DB)
	Part 14: Users-Articles Relationship Data (Soccer DB)
	Part 15: Users-AcademicInstitutes Relationship Data (Soccer DB) 

*/
/*
select * from scholarPublications
select * from scholarusers
select * from scholarInterests
select * from users 
select * from UserScholarInterests
select * from articles
select * from v_UserArticles where uId = 9
select * from UsersInArticle where uId = 9
*/
/****************************************************************************************************************************
	Part 1: Drop Data.
	Droping all Research clouds related tables, views and procedures ordered by foreign keys constraints
*****************************************************************************************************************************/

drop table Affiliations, UsersInCluster, usersInArticle,KeywordsInCluster,KeywordsInArticle, UserScholarInterests
drop table users, Clusters, Articles, Keywords, AcademicInstitutes
drop view [dbo].[v_ArticleKeywords],[dbo].[v_ClusterKeywords],[dbo].[v_InstituteUsers],[dbo].[v_UserAffiliations],[dbo].[v_UserArticles],[dbo].[v_UserClusters]

drop proc p_deleteUser, p_deleteArticle, p_deleteCluster, p_deleteInstitute,p_deleteKeyword,p_deleteUserFull


/****************************************************************************************************************************
	Part 2: Tables
	Creating tables for all entities
	Users, Clusters, Articles, Keywords, AcademicInstitutes
*****************************************************************************************************************************/

create table Users(
[uId] int identity not null,
firstName nvarchar(50) not null,
middleName nvarchar(50) ,
lastName nvarchar(50) not null,
degree nvarchar(50) ,
imgPath nvarchar(1000) ,
birthDate datetime ,
registrationDate datetime ,
administrator bit ,
email nvarchar(250) ,
uHash nvarchar(max) ,
uSALT nvarchar(max) ,
summery nvarchar(500),
isRegistered bit not null
)
go

create table Clusters(
cId int identity not null,
cName nvarchar(250) not null
)
go

create table Articles(
aId int identity not null,
title nvarchar(1000)  not null,
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
iName nvarchar(500) not null unique
)


/****************************************************************************************************************************
	Part 3:Relationship Tables
	Creating all the entities relashionship tables

*****************************************************************************************************************************/

create table KeywordsInCluster(
kId int not null,
cId int not null
)
go



create table UserScholarInterests(
[uId] int not null,
interest nvarchar(250) not null
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

/****************************************************************************************************************************
	Part 4: Primary Keys
	Entering primary keys to all entity and entity relashionship tables
*****************************************************************************************************************************/


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


alter table UserScholarInterests
add constraint UserScholarInterests_uid_PK primary key ([uId],interest)
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

alter table UsersInArticle
add constraint UsersInArticle_uid_PK primary key ([uId],aId)
go

/****************************************************************************************************************************
	Part 5: Foreign Keys
	Creating foreign keys for every table
*****************************************************************************************************************************/



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



/****************************************************************************************************************************
	Part 6: Views
	Creating all relevent views
	all view names starts with v_
*****************************************************************************************************************************/


create view v_UserClusters
as
SELECT        dbo.Users.uId, dbo.Users.firstName, dbo.Users.middleName, dbo.Users.lastName, dbo.Users.degree, dbo.Users.imgPath, dbo.Users.birthDate, dbo.Users.registrationDate, dbo.Users.administrator, dbo.Users.email, 
                         dbo.Users.uHash, dbo.Users.uSALT, dbo.Users.summery, dbo.Users.isRegistered, dbo.Clusters.cId, dbo.Clusters.cName, dbo.UsersInCluster.visible
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



/****************************************************************************************************************************
	Part 7: Procedures
	Creating all the procedures, starting with deleting procedures only.
	all procedure names starts with p_
*****************************************************************************************************************************/


create proc p_deleteUser
@id int
as
delete from UsersInCluster where [uId] =@id
delete from UsersInArticle where [uId] = @id
delete from Affiliations where [uId] = @id
delete from users where [uId] =@id
go


create proc p_deleteArticle
@id int
as
delete from KeywordsInArticle where aId = @id
delete from UsersInArticle where aId =@id
delete from articles where aId=@id
go

create proc p_deleteCluster
@id int
as
delete from KeywordsInCluster where cId = @id
delete  from UsersInCluster where cId = @id
delete  from clusters where cId = @id
go


create proc p_deleteInstitute
@id int
as
delete  from Affiliations where iId = @id
delete  from AcademicInstitutes where iId = @id
go


create proc p_deleteKeyword
@id int
as
delete  from KeywordsInCluster where kId = @id
delete  from KeywordsInArticle where kId = @id
delete  from Keywords where kId = @id
go


create proc p_deleteUserFull 
@userId int
as
DECLARE @aId int
DECLARE MY_CURSOR CURSOR 
  LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR 
SELECT DISTINCT aId
FROM UsersInArticle where uId = @userId

OPEN MY_CURSOR
FETCH NEXT FROM MY_CURSOR INTO @aId
WHILE @@FETCH_STATUS = 0
BEGIN 
    --Do something with Id here
	delete from UsersInArticle where aId = @aId
	delete from KeywordsInArticle where aId = @aId
	delete from articles where aId = @aId
    PRINT @aId
    FETCH NEXT FROM MY_CURSOR INTO @aId
END
CLOSE MY_CURSOR
DEALLOCATE MY_CURSOR
delete from UserScholarInterests where uId = @userId
delete from affiliations where uId = @userId
delete from users where uId = @userId
go




/****************************************************************************************************************************
	
	Part 8: Soccer Users Table Information
	Inserting hard coded data into Users table based on Benney's personal preferences.
	All passwords are 'lastname123' i.e: messi123, neymar123, hazan123, bale123, ronaldo123	
											
*****************************************************************************************************************************/



insert into users values
('Lionel', '', 'Messi','Degree','https://cdn.images.express.co.uk/img/dynamic/67/590x/Lionel-Messi-Barcelona-778351.jpg',
'5-20-1985','3-16-2018',1,'messi@ruppin.ac.il','61A1B1A373DFA2C6515612B1A8A77F83AE28B7E0C5E39816B64D67739019F669','20E6494B4207A90D',
'Messi is unsumable ! ',1)
go

insert into users values
('Neymar', 'Da Silva', ' Santos','Degree','http://www.whoateallthepies.tv/wp-content/uploads/2013/05/neymar-cry.jpg',
'5-20-1985','3-16-2018',0,'neymar@ruppin.ac.il','B9383D41E4D70F56E4A2FE34B9F116EA9BBCEE04B4A61E4957D184074CC4EF58','3C3C58961451D04',
'A Summery About Neymar',1)
go
insert into users values
('Oren', '', 'Hazan','Degree','http://www.maariv.co.il/HttpHandlers/ShowImage.ashx?ID=292760',
'5-20-1985','3-16-2018',0,'hazan@ruppin.ac.il','5E55DE8E9F15CC8373BFD3B1866DDDB55499F66F6E978E7C85BA4D55191477B5','66C26C8D58996B8F',
'We Dont Think that Oren needs a summery',1)
go
insert into users values
('Cristiano', '', 'Ronaldo','Degree','https://secure.i.telegraph.co.uk/multimedia/archive/02479/infant_2479350k.jpg',
'5-20-1985','3-16-2018',0,'ronaldo@ruppin.ac.il','11CEE9D8C4546E96675E38898B3BFE25A9313D645394AFFCBFACEEE439006972','7EE9BB521CE704BA',
'this is my summery',1)
go
insert into users values
('Gareth', '', 'Bale','Degree','http://news.images.itv.com/image/file/1401468/stream_img.jpg',
'5-20-1985','3-16-2018',0,'bale@ruppin.ac.il','C9EC3C42562081E5439A40A0D9C06630261827C09CE375D5335795548D4004F3','2813B5F0BA1E74',
'Gareth Bale Summery, Somthing unique and long enough to make your HTML wonder if he is ready for somthing heavy like this.',1)
go


/****************************************************************************************************************************
	
	Part 9: Soccer Article Table Information
	Inserting soccer related articles found randomly on google scholar using "soccer"....	
											
*****************************************************************************************************************************/


insert into Articles values
('Match performance of high-standard soccer players with special reference to development of fatigue',
'http://www.academia.edu/download/41464251/Mohr_M_Krustrup_P_Bangsbo_J._Match_perfo20160123-13511-12u6xhn.pdf')
insert into Articles values
('Aerobic endurance training improves soccer performance',
'http://www.henriquetateixeira.com.br/up_artigo/aerobic_endurance_training_improves_soccer_performance_va5te8.pdf')
insert into Articles values
('Performance Characteristics According to Playing Position in Elite Soccer',
'https://www.researchgate.net/profile/Harald_Tschan/publication/6769686_Performance_Characteristics_According_to_Playing_Position_in_Elite_Soccer/links/004635225987faa227000000/Performance-Characteristics-According-to-Playing-Position-in-Elite-Soccer.pdf')
insert into Articles values
('Automatic Soccer Video Analysis and Summarization',
'https://pdfs.semanticscholar.org/e6a0/e34cabc62890039efbd88b2f9c40e6885ef3.pdf')
insert into Articles values
('Use of RPE-Based Training Load in Soccer',
'https://s3.amazonaws.com/academia.edu.documents/46677566/Use_of_RPE-based_training_load_in_soccer20160621-23194-1dxpw0d.pdf?AWSAccessKeyId=AKIAIWOWYYGZ2Y53UL3A&Expires=1521166379&Signature=Tu5N5XTM65DiE3E9%2BzxkZ3KtkKk%3D&response-content-disposition=inline%3B%20filename%3DUse_of_RPE-Based_Training_Load_in_Soccer.pdf')
insert into Articles values
('Salivary testosterone and cortisol in rugby players: correlation with psychological overtraining items',
'http://bjsm.bmj.com/content/bjsports/38/3/263.full.pdf')
insert into Articles values
('Muscle Flexibility as a Risk Factor for Developing Muscle Injuries in Male Professional Soccer Players: A Prospective Study',
'https://www.researchgate.net/profile/Dirk_Cambier/publication/10946840_Muscle_Flexibility_as_a_Risk_Factor_for_Developing_Muscle_Injuries_in_Male_Professional_Soccer_Players_A_Prospective_Study/links/0fcfd50b8d191404e1000000/Muscle-Flexibility-as-a-Risk-Factor-for-Developing-Muscle-Injuries-in-Male-Professional-Soccer-Players-A-Prospective-Study.pdf')
insert into Articles values
('Isokinetic Strength and Anaerobic Power of Elite, Subelite and Amateur French Soccer Players',
'https://www.researchgate.net/profile/Nicola_Maffulli/publication/12073325_Isokinetic_Strength_and_Anaerobic_Power_of_Elite_Subelite_and_Amateur_French_Soccer_Players/links/0fcfd513e0e2d57605000000.pdf')
insert into Articles values
('Variation in Top Level Soccer Match Performance',
'https://www.researchgate.net/profile/Franco_Impellizzeri/publication/6333655_Variation_in_Top_Level_Soccer_Match_Performance/links/0a85e53984626ddc0e000000/Variation-in-Top-Level-Soccer-Match-Performance.pdf')
insert into Articles values
('Fatigue in soccer',
'https://s3.amazonaws.com/academia.edu.documents/41464247/Fatigue_in_soccer_A_brief_review20160123-13509-1x1gjz8.pdf?AWSAccessKeyId=AKIAIWOWYYGZ2Y53UL3A&Expires=1521166468&Signature=T8bi4xjmskzFnBwOFhQxRFVai7E%3D&response-content-disposition=inline%3B%20filename%3DFatigue_in_soccer_A_brief_review.pdf')
insert into Articles values
('Physical Demands during an Elite Female Soccer Game: Importance of Training Status',
'https://s3.amazonaws.com/academia.edu.documents/45937159/Physical_Demands_during_an_Elite_Female_20160525-26781-4j7les.pdf?AWSAccessKeyId=AKIAIWOWYYGZ2Y53UL3A&Expires=1521166468&Signature=4ZOEh0UEx89VA4P50B7cJt2U2fY%3D&response-content-disposition=inline%3B%20filename%3DPhysical_Demands_during_an_Elite_Female.pdf')




/****************************************************************************************************************************
	
	Part 10: Soccer Keywords Data
	Inserting soccer related keywords made up based on the article titles previously added	
											
*****************************************************************************************************************************/


insert into Keywords values('Soccer Offence')
insert into Keywords values('Fatigue')
insert into Keywords values('Elite Soccer')
insert into Keywords values('Top Athletes')
insert into Keywords values('Soccer Analysis')
insert into Keywords values('Fatigue Development')
insert into Keywords values('Match Performance')
insert into Keywords values('Soccer Players')
insert into Keywords values('Defensive Plays')
insert into Keywords values('Training Importance')
insert into Keywords values('Barcelon')
insert into Keywords values('Soccer')
insert into Keywords values('Advanced Tactics')
insert into Keywords values('Endurance')
insert into Keywords values('Top Strikers')




/****************************************************************************************************************************
	
	Part 11: Soccer Cluster Data
	Inserting hard coded soccer related clusters made up based on the keyword phrases previously added	
											
*****************************************************************************************************************************/

insert into Clusters values('Top Athletes') 
insert into Clusters values('Soccer Training') 
insert into Clusters values('Game Analysis') 
insert into Clusters values('Soccer') 
insert into Clusters values('Fatigue Development') 





/****************************************************************************************************************************
	
	Part 12: Soccer Institutes data 
	Inserting institutes data. 
	Rupping is there for obvious reasons, Camp Nou was added based on the users previously added
											
*****************************************************************************************************************************/

insert into AcademicInstitutes values ('Ruppin Academic Center')
insert into AcademicInstitutes values ('Camp Nou')
insert into AcademicInstitutes values ('Harverd University')
insert into AcademicInstitutes values ('University of Haifa')



/****************************************************************************************************************************
	
	Part 13: Soccer User and Cluster Relashionship Data 
	Inserting hard coded users and clusters relationship
											
*****************************************************************************************************************************/

insert into UsersInCluster values (1,1,1) --uId, cId, visible
insert into UsersInCluster values (2,1,1) --uId, cId, visible
insert into UsersInCluster values (4,1,1) --uId, cId, visible
insert into UsersInCluster values (5,1,1) --uId, cId, visible
insert into UsersInCluster values (1,2,1) --uId, cId, visible
insert into UsersInCluster values (2,2,1) --uId, cId, visible
insert into UsersInCluster values (4,2,1) --uId, cId, visible
insert into UsersInCluster values (5,2,1) --uId, cId, visible
insert into UsersInCluster values (4,3,1) --uId, cId, visible
insert into UsersInCluster values (3,3,1) --uId, cId, visible
insert into UsersInCluster values (5,3,1) --uId, cId, visible
insert into UsersInCluster values (1,4,1) --uId, cId, visible
insert into UsersInCluster values (2,4,1) --uId, cId, visible
insert into UsersInCluster values (3,4,1) --uId, cId, visible
insert into UsersInCluster values (4,4,1) --uId, cId, visible
insert into UsersInCluster values (5,4,1) --uId, cId, visible
insert into UsersInCluster values (1,5,1) --uId, cId, visible
insert into UsersInCluster values (3,5,1) --uId, cId, visible
insert into UsersInCluster values (2,5,1) --uId, cId, visible


/****************************************************************************************************************************
	
	Part 14: Users and Articles Relationship Data 
	Inserting hard coded relationships between users and articles based on divine guidence
											
*****************************************************************************************************************************/

insert into UsersInArticle values (1,2) --uId, aId
insert into UsersInArticle values (1,4)
insert into UsersInArticle values (1,6)
insert into UsersInArticle values (2,1)
insert into UsersInArticle values (2,11)
insert into UsersInArticle values (2,5)
insert into UsersInArticle values (2,7)
insert into UsersInArticle values (3,8)
insert into UsersInArticle values (3,3)
insert into UsersInArticle values (4,6)
insert into UsersInArticle values (4,9)
insert into UsersInArticle values (4,10)
insert into UsersInArticle values (5,4)
insert into UsersInArticle values (5,10)
insert into UsersInArticle values (5,3)
insert into UsersInArticle values (5,1)


/****************************************************************************************************************************
	
	Part 15: Users and Academic Institutes Relationship Data 
	Inserting hard coded relationships between users and Institutes randomly
											
*****************************************************************************************************************************/

insert into Affiliations values (1,1) --uId iId
insert into Affiliations values (2,1)
insert into Affiliations values (3,1)
insert into Affiliations values (4,1)
insert into Affiliations values (5,1)
insert into Affiliations values (1,2)
insert into Affiliations values (2,2)
