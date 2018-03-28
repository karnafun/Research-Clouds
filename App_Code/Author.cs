using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Author
/// </summary>
public class Author : RCEntity
{
    

    private string firstName;

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    private string middleName;

    public string MiddleName
    {
        get { return middleName; }
        set { middleName = value; }
    }

    private string lastName;

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }
    private string degree;

    public string Degree
    {
        get { return degree; }
        set { degree = value; }
    }

    public Author() :base() 
    {
      
    }

    public Author(string firstName, string middleName, string lastName, string degree = null) : this()
    {

        this.firstName = firstName;
        this.middleName = middleName;
        this.lastName = lastName;
        this.degree = degree;
    }
    public Author(int id,string firstName, string middleName, string lastName, string degree = null) :
        this(firstName,middleName,lastName,degree)
    {
        //db = new DBServices();
        this.id = id;
    }

    public Author GetAuthorById(int id)
    {
        return db.GetAuthorById(id);
    }
    public List<Author> GetAllAuthors()
    {
        return db.GetAllAuthors();
        
    }

    public int UpdateAuthorInDatabase(Author author = null)
    {
        if (author==null)
        {
            return Db.UpdateAuthor(this);
        }
        else
        {
            return Db.UpdateAuthor(author);
        }
    }
    public int InsertAuthorToDatabase(Author author = null)
    {
        if (author==null)        
            return db.InsertAuthor(this);        
        if (id > 0)         
            LogManager.Report(String.Format("trying to insert:\r\n{0}\r\n\r\n into the database (has valid ID)", ToString()));

        return db.InsertAuthor(author);
    }
    
}