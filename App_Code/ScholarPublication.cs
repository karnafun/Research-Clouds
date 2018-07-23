using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/*
pId int identity primary key not null,
suId int foreign key references scholarUsers(suId) not null,
author nvarchar(500) not null,
publisher nvarchar(1000),
eprint nvarchar(1000),
title nvarchar(1000),
[url] nvarchar(1000),
[year] nvarchar(10),
abstract nvarchar(2500)
 */
/// <summary>
/// Summary description for ScholarPublication
/// </summary>
public class ScholarPublication
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public string EPrint { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Year { get; set; }
    public string Abstract { get; set; }
    public ScholarPublication()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    public ScholarPublication(string author, string publisher, string ePrint, string title, string url, string year, string @abstract)
    {
        Author = author;
        Publisher = publisher;
        EPrint = ePrint;
        Title = title;
        Url = url;
        Year = year;
        Abstract = @abstract;
    }

    public ScholarPublication(int authorId, string author, string publisher, string ePrint, string title, string url, string year, string @abstract)
    {
        AuthorId = authorId;
        Author = author;
        Publisher = publisher;
        EPrint = ePrint;
        Title = title;
        Url = url;
        Year = year;
        Abstract = @abstract;
    }

    public ScholarPublication(int id, int authorId, string author, string publisher, string ePrint, string title, string url, string year, string @abstract)
    {
        Id = id;
        AuthorId = authorId;
        Author = author;
        Publisher = publisher;
        EPrint = ePrint;
        Title = title;
        Url = url;
        Year = year;
        Abstract = @abstract;
    }
    
   public XElement ToXML()
    {
        XElement root = new XElement("Publication");
        root.Add(new XElement("Id", Id));
        root.Add(new XElement("AuthorId", AuthorId ));
        root.Add(new XElement("Author", Author));
        root.Add(new XElement("Publisher", Publisher));
        root.Add(new XElement("EPrint", EPrint));
        root.Add(new XElement("Url", Url));
        root.Add(new XElement("Year", Year));
        root.Add(new XElement("Abstract", Abstract));
        return root;        
    }
}