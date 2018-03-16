

--all passwords are messi123, neymar123, hazan123, bale123, ronaldo123

insert into users values
('Lionel', '', 'Messi','Degree','https://cdn.images.express.co.uk/img/dynamic/67/590x/Lionel-Messi-Barcelona-778351.jpg',
'5-20-1985','3-16-2018',1,'messi@ruppin.ac.il','61A1B1A373DFA2C6515612B1A8A77F83AE28B7E0C5E39816B64D67739019F669','20E6494B4207A90D',
'Messi is unsumable ! ')
go

insert into users values
('Neymar', 'Da Silva', ' Santos','Degree','http://www.whoateallthepies.tv/wp-content/uploads/2013/05/neymar-cry.jpg',
'5-20-1985','3-16-2018',0,'neymar@ruppin.ac.il','B9383D41E4D70F56E4A2FE34B9F116EA9BBCEE04B4A61E4957D184074CC4EF58','3C3C58961451D04',
'A Summery About Neymar')
go
insert into users values
('Oren', '', 'Hazan','Degree','http://www.maariv.co.il/HttpHandlers/ShowImage.ashx?ID=292760',
'5-20-1985','3-16-2018',0,'hazan@ruppin.ac.il','5E55DE8E9F15CC8373BFD3B1866DDDB55499F66F6E978E7C85BA4D55191477B5','66C26C8D58996B8F',
'We Dont Think that Oren needs a summery')
go
insert into users values
('Cristiano', '', 'Ronaldo','Degree','https://secure.i.telegraph.co.uk/multimedia/archive/02479/infant_2479350k.jpg',
'5-20-1985','3-16-2018',0,'ronaldo@ruppin.ac.il','11CEE9D8C4546E96675E38898B3BFE25A9313D645394AFFCBFACEEE439006972','7EE9BB521CE704BA',
'this is my summery')
go
insert into users values
('Gareth', '', 'Bale','Degree','http://news.images.itv.com/image/file/1401468/stream_img.jpg',
'5-20-1985','3-16-2018',0,'bale@ruppin.ac.il','C9EC3C42562081E5439A40A0D9C06630261827C09CE375D5335795548D4004F3','2813B5F0BA1E74',
'Gareth Bale Summery, Somthing unique and long enough to make your HTML wonder if he is ready for somthing heavy like this.')
go

select * from users where email='messi@ruppin.ac.il'and uHash = '1145FA52010BB26C17C6FC854BEAA21FA41E7943C8B079E4D02671DF03273214'
--Insert All Articles:
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




--insert Keywords:
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



--create clusters with users in them

insert into Clusters values('Top Athletes') 
insert into UsersInCluster values (1,1,1) --uId, cId, visible
insert into UsersInCluster values (2,1,1) --uId, cId, visible
insert into UsersInCluster values (4,1,1) --uId, cId, visible
insert into UsersInCluster values (5,1,1) --uId, cId, visible

insert into Clusters values('Soccer Training') 
insert into UsersInCluster values (1,2,1) --uId, cId, visible
insert into UsersInCluster values (2,2,1) --uId, cId, visible
insert into UsersInCluster values (4,2,1) --uId, cId, visible
insert into UsersInCluster values (5,2,1) --uId, cId, visible

insert into Clusters values('Game Analysis') 
insert into UsersInCluster values (4,3,1) --uId, cId, visible
insert into UsersInCluster values (3,3,1) --uId, cId, visible
insert into UsersInCluster values (5,3,1) --uId, cId, visible

insert into Clusters values('Soccer') 
insert into UsersInCluster values (1,4,1) --uId, cId, visible
insert into UsersInCluster values (2,4,1) --uId, cId, visible
insert into UsersInCluster values (3,4,1) --uId, cId, visible
insert into UsersInCluster values (4,4,1) --uId, cId, visible
insert into UsersInCluster values (5,4,1) --uId, cId, visible

insert into Clusters values('Fatigue Development') 
insert into UsersInCluster values (1,5,1) --uId, cId, visible
insert into UsersInCluster values (3,5,1) --uId, cId, visible
insert into UsersInCluster values (2,5,1) --uId, cId, visible


