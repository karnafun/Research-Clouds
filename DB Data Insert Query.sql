
/*

delete from Affiliations
delete from UsersInCluster
delete from usersInArticle
delete from KeywordsInCluster
delete from KeywordsInArticle
delete from users
delete from Clusters
delete from Articles
delete from Keywords
delete from AcademicInstitutes

*/
--Insert Raw Data


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


