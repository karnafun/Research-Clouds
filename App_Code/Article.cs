using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Article
/// </summary>
public class Article : RCEntity
{
    //Fields
    //DBServices db;
    //int id;
    string title, link;
    List<User> users;
    List<Keyword> keywords;

    //Properties
    public string Title { get { return title; } set { title = value; } }
    public string Link { get { return link; } set { link = value; } }
    public List<User> Users { get { return users; } }
    public List<Keyword> Keywords { get { return keywords; } }

    //Constructors:
    public Article()
    {

        db = new DBServices();
    }
    public Article(int id, string title, string link)
    {

        db = new DBServices();
        this.id = id;
        this.title = title;
        this.link = link;
    }

    //Methods
    public void GetFullInfo()
    {
        users = db.GetArticleUsers(id);
        keywords = db.GetArticleKeywords(id);
    }
    public override string ToString()
    {
        string info = "Id: " + id + "<br>";
        info += "Title: " + title + "<br>";
        info += "Link: " + link + "<br>";
        return info;
    }

    //Database Related Methods
    public List<Article> GetAllArticles()
    {
        return db.GetAllArticles();
    }
    public Article GetArticleById(int aId)
    {
        return db.GetArticleById(aId);
    }
    public int InsertArticleToDatabase()
    {
        if (id > 0) //it means you tried to insert an article with a valid id ! you CANNOT choose the id yourself
        {
            LogManager.Report(String.Format("trying to insert:\r\n{0}\r\n\r\n into the database (has valid ID)", ToString()));
        }
        return db.InsertArticle(this);
    }
    public int UpdateArticleInDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to update an article with invalid id", this);
            return -1;
        }

        ValidateUsers();
        return db.UpdateArticle(this);
    }
    public int DeleteArticleFromDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to delete an article with invalid id", this);
            return -1;
        }
        return db.RemoveEntity(this);
    }

    private void ValidateUsers()
    {
        List<User> dbArticleusers = db.GetArticleUsers(id);
        foreach (User _user in users)
        {
            for (int i = 0; i < users.Count; i++)
            {

            }
        }
    }
}