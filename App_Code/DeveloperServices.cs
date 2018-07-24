using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for DeveloperServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class DeveloperServices : System.Web.Services.WebService
{

    public DeveloperServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<User> GetAllUsers()
    {
        List<User> users = new List<global::User>();
        foreach (var item in users)
        {
            item.GetFullInfo();
        }
        return users;

    }

    [WebMethod]
    public User GetUserById(int id)
    {
        return new DBServices().GetUserById(id);
    }

    //[WebMethod]
    //public User GetUserByName(string fName, string mName, string lName)
    //{
    //    return new DBServices().GetUserByName(fName, mName, lName);
    //}
    [WebMethod]
    public User GetUserByName(string fName, string lName)
    {
        return new DBServices().GetUserByName(fName, "", lName);
    }
    [WebMethod]
    public User GetUserByEmail(string email)
    {
        return new DBServices().GetUserByEmail(email);
    }

    [WebMethod]
    public int RemoveUserById(int id)
    {
        return new DBServices().RemoveEntity(new User().GetUserById(id));
    }
    [WebMethod]
    public int RemoveUserByName(string fName, string lName)
    {
        return new DBServices().RemoveEntity(new DBServices().GetUserByName(fName, "", lName));
    }


    //[WebMethod]
    //public List<Article> GetAllArticles()
    //{
    //    List<Article> articles = new Article().GetAllArticles();
    //    foreach (var item in articles)
    //    {
    //        item.GetFullInfo();
    //    }
    //    return articles;
    //}

    //[WebMethod]
    //public List<Article> GetUserArticles(int uId)
    //{
    //    List<Article> articles = new User().GetUserById(uId).Articles;
    //    foreach (var item in articles)
    //    {
    //        item.GetFullInfo();
    //    }
    //    return articles;
    //}
    //[WebMethod]
    //public Article GetArticleById(int aId)
    //{

    //    Article a = new Article().GetArticleById(aId);
    //    a.GetFullInfo();
    //    return a;
    //}
}
