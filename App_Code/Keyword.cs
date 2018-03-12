using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Keywords
/// </summary>
public class Keyword
{
    //Fields:
    DBServices db;
    int id;
    string phrase;
    List<Cluster> clusters;
    List<Article> articles;

    //Props:
    public int Id { get { return id; } }
    public string Phrase { get { return phrase; } }
    public List<Cluster> Clusters {
        get
        {
            if (clusters == null)
            {
                clusters = db.GetKeywordClusters(id);
            }
            return clusters;
        }
    }
    public List<Article> Articles {
        get
        {
            if (articles ==null)
            {
                articles = db.GetKeywordArticles(id);
            }
            return articles;
        }
    }



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


    /// <summary>
    /// Gets all the keywords from the database
    /// </summary>
    /// <returns>List of all keywords</returns>
    public List<Keyword> GetAllKeywords()
    {
        return db.GetAllKeywords();
    }
   
    public override string ToString()
    {
        string info = "ID: " + id + "<br>";
        info += "Pharse: " + "<br>";
        return info;
    }
 
}