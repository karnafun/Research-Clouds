using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_html_WebFormClassDemo : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Article article = new Article().GetArticleById(1);
        article.GetFullInfo();
        //article.Users[0].FirstName = "TESTING SHIT";
        article.UpdateArticleInDatabase();

    }

    //user demo
    private void InsertUserDemo()
    {
        User user = new User(0, "dor", "test", "danai", @"https://goo.gl/GMts4T", "El Doctore", "Pizdaput@gmail.hotdog", "you cannot sum me !", true,
            DateTime.MinValue,DateTime.MinValue);
        user.Password = "hotdog";
        user.RegistrationDate = DateTime.Now;
        user.BirthDate = DateTime.Now;
        user.InsertUserToDatabase();
    }
    private void UpdateUserDemo()
    {
        User user = new User().GetUserByEmail("messi@ruppin.ac.il");
        user.MiddleName = "";
        int effected = user.UpdateUserInDatabase();
    }
    private void DeleteUserDemo()
    {
        User user = new User().GetUserById(1);
        int effected = user.RemoveUserFromDatabase();
        Response.Write(effected);
    }
    private void UserFullClusters()
    {
        List<Cluster> clusters = new User().GetUserFullClusters(1);
        string res = "";
        foreach (Cluster cluster in clusters)
        {
            res += "Cluster: " + cluster.Name + "<br>";
            foreach (User user in cluster.Users)
            {
                res += user.ToString() + "<br>";
            }
            res += "<br><br>";
        }
        Response.Write(res);
    }
    //article demo
    private void InsertArticleDemo()
    {
        Article article = new Article(0, "Test Article", "FU");
        int rowsEffected = article.InsertArticleToDatabase();
        Response.Write(rowsEffected);
    }
    private void UpdateArticleDemo()
    {
        Article article = new Article().GetAllArticles()[0];        
        article.Title = "Changing Title Test";
        int effected = article.UpdateArticleInDatabase();
        Response.Write(effected);

    }
    private void DeleteArticleDemo()
    {
        Article article = new Article().GetAllArticles()[0];
        int effected = article.DeleteArticleFromDatabase();
        Response.Write(effected);

    }
    //cluster demo
    private void InsertClusterDemo()
    {
        Cluster cluster = new Cluster(0, "New Cluster i Made");
        int effected = cluster.InsertClusterToDatabase();
        Response.Write(effected);
    }
    private void UpdateClusterDemo()
    {
        Cluster cluster = new Cluster().GetClusterById(6);
        cluster.Name = "I CHANGE THE NAME ! ";
        int effected = cluster.UpdateClusterInDatabase();
        Response.Write(effected);
    }
    private void DeleteClusterDemo()
    {
        Cluster cluster = new Cluster().GetAllClusters()[0];
        int effected = cluster.DeleteClusterFromDatabase();
        Response.Write(effected);
    }
    //institute demo
    private void InsertInstituteDemo()
    {
        Institute institute = new Institute(0,"Academy Of Life Wallak");
        int effected = institute.InsertInstituteToDatabase();
        Response.Write(effected);

    }
    private void UpdateInstituteDemo()
    {
        Institute institute = new Institute().GetAllInstitutes()[0];
        institute.Name = "House Of Weed";
        int effected = institute.UpdateInstituteInDatabase();
        Response.Write(effected);
    }
    private void DeleteInstituteDemo()
    {
        Institute institute = new Institute().GetAllInstitutes()[0];
        int effected = institute.DeleteInstituteFromDatabase();
        Response.Write(effected);
    }
    //keyword demo
    private void InsertKeywordDemo()
    {
        Keyword keyword = new Keyword(0,"New Keyword i found");
        int effected = keyword.InsertKeywordToDatabase();
        Response.Write(effected);
    }
    private void UpdateKeywordDemo()
    {
        Keyword keyword = new Keyword().GetAllKeywords()[0];
        keyword.Phrase = "hotdogiia!";
        int effected = keyword.UpdateKeywordInDatabase();
        Response.Write(effected);
    }
    private void DeleteKeywordDemo()
    {
        Keyword keyword = new Keyword().GetAllKeywords()[0];
        int effected = keyword.DeleteKeywordFromDatabase();
        Response.Write(effected);
    }
    //Encryption demo
    private void GenerateHashDemo()
    {
        string messi = SHA2.GenerateSHA256String("messi123", "20E6494B4207A90D");
        string neymar = SHA2.GenerateSHA256String("neymar123", "3C3C58961451D04");
        string hazan = SHA2.GenerateSHA256String("hazan123", "66C26C8D58996B8F");
        string ronaldo = SHA2.GenerateSHA256String("ronaldo123", "7EE9BB521CE704BA");
        string bale = SHA2.GenerateSHA256String("bale123", "2813B5F0BA1E74");

        string res = "messi: " + messi + "\r\n";
        res += "neymar: " + neymar + "\r\n";
        res += "hazan: " + hazan + "\r\n";
        res += "ronaldo: " + ronaldo + "\r\n";
        res += "bale: " + bale + "\r\n";
        Response.Write(res);

    }
}