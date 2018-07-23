import pypyodbc
import scholarly

def TestFunction():
	return 'TestFunction is working ! '
def GetConnection():
	return pypyodbc.connect('Driver={SQL Server};'
                            'Server=media.ruppin.ac.il;'
                            'Database=bgroup62_test2;'
                            'uid=bgroup62;pwd=bgroup62_00472')

def InsertAuthorInfo(_author):								
	connection = GetConnection()
	cursor = connection.cursor() 
	SQLCommand = ("insert into scholarUsers values('%s','%s','%s','%s')" % (_author.name,_author.affiliation,_author.email,"https://scholar.google.co.il"+_author.url_picture))
	cursor.execute(SQLCommand)
	connection.commit()
	connection.close()

def GetAuthorId(_name):
	connection = GetConnection()
	cursor = connection.cursor()
	SQLCommand = ('select suId from scholarUsers where name = \'%s\'' % (_name))
	cursor.execute(SQLCommand)
	res = ''
	for row in cursor.fetchall():
		res = row[0]	
	connection.close()
	return res

	
def InsertInterests(_author):
	_id = GetAuthorId(_author.name)
	connection = GetConnection()
	cursor = connection.cursor()
	for interest in _author.interests:
		SQLCommand = 'insert into scholarInterests values(\'%s\',\'%s\')' % (_id,interest)
		cursor.execute(SQLCommand)
		connection.commit()
	connection.close()

# InsertInterests(author)
def InsertPublications(_author):
	# pId
	# suId
	# author
	# publisher
	# eprint
	# title
	# [url] 
	# [year]
	# abstract
	try:
		_author.fill()
		suId = GetAuthorId(_author.name)
		connection = GetConnection()
		cursor = connection.cursor()
		
		for i,pub in enumerate(_author.publications):
			pub.fill()	
			author = str(pub.bib['author'])
			title = str(pub.bib['title'])
			abstract = str(pub.bib['abstract'])
			if 'eprint' in pub.bib.keys():
				eprint = str(pub.bib['eprint'])	
			else:
				eprint = None	
			url = str(pub.bib['url'])
			if 'year' in pub.bib.keys():
				year = str(pub.bib['year'])	
			else:
				year = None
			if 'publisher' in pub.bib.keys():	
				publisher = str(pub.bib['publisher'])
			else:
				publisher = None
			
			ch ='\''
			if  ch in abstract:
				abstract=abstract.replace(ch,"'"+ch)
			if ch in url:
				url=url.replace(ch,"'"+ch)
			if eprint is not None and ch in eprint:
				eprint=eprint.replace(ch,"'"+ch)
							
			SQLCommand = 'insert into scholarPublications values(\'%s\',\'%s\',\'%s\',\'%s\',\'%s\',\'%s\',\'%s\',\'%s\')' % (suId,author,publisher,eprint,title,url,year,abstract)
			# print (SQLCommand )
			cursor.execute(SQLCommand)
			connection.commit()
			print('added publication number: '+str(i+1))		
		connection.close()
	except:
		connection.close()

def FixName(_name):	
	name = _name.split() # Splits string at whitespace into LIST
	if name.__len__() == 3:
		_name = '%s %s' % (name[0],name[2])
	return _name

def GetNames():
	f= open("professors.txt","r")
	contents =f.read()
	return  contents.split('\n')

def InsertAllUsers(authorList):
	for _name in authorList:
		_name = FixName(_name)
		query = scholarly.search_author(_name)
		while True:
			try:
				author = next(query)
				print(author.email)			
				# if '@seas.harvard.edu' == author.email:			
				if 'harvard' in author.email.split('.'):			
					print(author)
					author.fill()
					InsertAuthorInfo(author)				
					InsertInterests(author)
					InsertPublications(author)				
					print('WORKED')
					break
				
			except StopIteration:
				print('done with: ' +_name)
				print('\n\n\n\n')
				break

	print('Done madafaka')
	

def ExctractUserInfo(name):
		_name = FixName(name)
		query = scholarly.search_author(_name)
		res =''
		while True:
			try:
				author = next(query)				
				author.fill()				
				InsertAuthorInfo(author)				
				InsertInterests(author)
				InsertPublications(author)					
				print('WORKED')
				break				
			except StopIteration:
				print('done with: ' +_name)
				print('\n\n\n\n')
				break

		print('res')

# authorList = ['david m. brooks','yiling chen']
# InsertAllUsers(authorList)




# InsertAllUsers(GetNames())










