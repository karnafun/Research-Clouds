using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Keywords
/// </summary>
public class Keyword :RCEntity
{
    //Fields:
    string phrase;
    List<Cluster> clusters;
    List<Article> articles;

    //Properties:
    public string Phrase { get { return phrase; }  set { phrase = value; } }
    public List<Cluster> Clusters { get { return clusters; } }
    public List<Article> Articles { get { return articles; } }

    //Constructors
    public Keyword()
    {

        db = new DBServices();
    }
    public Keyword(int id, string phrase)
    {
        db = new DBServices();
        this.id = id;
        this.phrase = phrase;
    }

    //methods:
    public override string ToString()
    {
        string info = "ID: " + id + "<br>";
        info += "Pharse: " + "<br>";
        return info;
    }
    public void GetFullInfo()
    {
        clusters = db.GetKeywordClusters(id);
        articles = db.GetKeywordArticles(id);
    }

    //Database Related Methods
    /// <summary>
    /// Gets all the keywords from the database
    /// </summary>
    /// <returns>List of all keywords</returns>
    public List<Keyword> GetAllKeywords()
    {
        return db.GetAllKeywords();
    }
    public Keyword GetKeywordById(int id)
    {
        return GetKeywordById(id);
    }
    public int InsertKeywordToDatabase()
    {
        if (id>0)
        {
            LogManager.Report("tring to insert a Keyword with valid id ", this);            
        }
        return db.InsertKeyword(this);
    }
    public int UpdateKeywordInDatabase()
    {
        if (id<0)
        {
            LogManager.Report("tried to update a keyword with invalid id", this);
            return -1;
        }
        return db.UpdateKeyword(this);
    }
    public int DeleteKeywordFromDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to delete a keyword with invalid id", this);
            return -1;
        }
        return db.RemoveEntity(this);
    }
}