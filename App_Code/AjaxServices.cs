using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;




/// <summary>
/// Summary description for AjaxServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]

public class AjaxServices : System.Web.Services.WebService
{
    const string rootPath = @"http://proj.ruppin.ac.il/bgroup62/test2/tar5/";
    public AjaxServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Gets a user from the database by the user id provided
    //--------------------------------------------------------------------------
    public string GetUserById(int Id)
    {
            User user = new User().GetUserById(Id);
        
        try
        {            
            JavaScriptSerializer js = new JavaScriptSerializer();
            user.GetFullInfo();
            return js.Serialize(user);    
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return ex.ToString();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Gets user information based on given credentials
    //--------------------------------------------------------------------------
    public string Login(string email, string password)
    {
        User user = new User().Login(email, password);
        if (user==null)
        {
            LogManager.Report("Invalid Login credentials for " + email);
            return null;
        }
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            user.GetFullInfo();
            return js.Serialize(user);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // 
    //--------------------------------------------------------------------------
    public string UpdateUser(string userString)
    {
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            User user = js.Deserialize<User>(userString);
            string res = user.UpdateUserInDatabase().ToString();
            return res;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex,userString);
            return ex.ToString();
        } 
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // 
    //--------------------------------------------------------------------------
    public string GetClusterById(string Id)
    {
        Cluster cluster = new Cluster().GetClusterById(int.Parse(Id));
        try
        {            
            JavaScriptSerializer js = new JavaScriptSerializer();
            cluster.GetFullInfo();
            return js.Serialize(cluster);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Returns all clusters for a specific user by user id
    // each cluster has users information in it 
    //--------------------------------------------------------------------------
    public string GetUserFullClusters(string Id)
    {        
        List<Cluster> clusters = new User().GetUserFullClusters(int.Parse(Id));
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();            
            return js.Serialize(clusters);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex,clusters);
           return null;
        }
        
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Returns a users with all needed information for the map-me animation
    // each one of users clusters has full information (has users in it)
    //--------------------------------------------------------------------------
    public string GetUserForAnimation(string Id)
    {
        User user = new User().GetUserById(int.Parse(Id));
        user.GetUserFullClusters();
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(user);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, user);
            return null;
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Inserts a user to the database
    // returns the created user from the database via login
    //--------------------------------------------------------------------------
    public string InsertUser(string userString)
    {
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            User user = js.Deserialize<User>(userString);
            string res = user.InsertUserToDatabase().ToString();
            user = user.Relog();           
            return js.Serialize(user);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return ex.ToString();
        }

    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Gets user information from Google Scholar and adds to scholarly table in DB
    // adds terms from IEE if any publications from there are found
    //inserts the information to the user in the database
    //returns the user with the new data
    //--------------------------------------------------------------------------
    public string FindUserAutomatically(string userString)
    {
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            User user = js.Deserialize<User>(userString);
            PythonServices py = new PythonServices();
            py.Run_cmd("InsertUser.py", user.Name);
            ScholarDBServices sdbs = new ScholarDBServices();
            ScholarUser sUser = sdbs.GetUserByName(user.Name);
            sdbs.IntegrateIntoUser(sUser.Id,user.Id);
            user = user.GetUserById(user.Id);
            //string res = user.InsertUserToDatabase().ToString();
            user = user.Relog();
            return js.Serialize(user);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return ex.ToString();
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Inserts a user to the database
    // returns the created user from the database via login
    //--------------------------------------------------------------------------
    public string UpdateArticle(string uId, string aId, string title, string link, string[] authors)
    {
        try
        {
            Article article = new Article().GetArticleById(int.Parse(aId));
            article.Title = title;
            article.Link = link;
            article.GetFullInfo();
            List<User> users = new List<global::User>();
            for (int i = 0; i < authors.Length; i++)
            {
                string[] name = authors[i].Trim().Split(' ');
                string fName = name[0];
                string mName = string.Empty;
                string lName = string.Empty;
                if (name.Length>2)
                {
                    mName = name[1];
                    lName = name[2];
                }
                else if (name.Length==2)
                {
                    lName = name[1];
                }
                else
                {
                    continue;
                }
                User author = new DBServices().GetUserByName(fName, mName, lName);
                if (author==null)
                {
                    author = new User(fName, mName, lName, article);                    
                    author.InsertAuthor();                    
                    author = new DBServices().GetUserByName(fName, mName, lName);
                }   
                users.Add(author);
            }
            foreach (var item in article.Users)
            {
                if (!users.Contains(item) && item.Id != int.Parse(uId))
                {
                    article.RemoveAuthor(item.Id);
                }
            }
            //User user = js.Deserialize<User>(userString); 
            //string res = user.InsertUserToDatabase().ToString();
            //user = user.Relog(); 
            article.UpdateUsers(users);
            new DBServices().FullArticleInsert(article);
            User user = new global::User().GetUserById(int.Parse(uId));
            user.GetFullInfo();
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(user);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, "UpdateArticle method in Ajax Services",
                "aId="+aId,
                "uId="+uId,
                "uId=" + uId,
                "title=" + title,
                "link= " + link);
            return ex.ToString();
        }

    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // 
    //--------------------------------------------------------------------------
    public string ImageUpload()
    {

       var uId =  HttpContext.Current.Request.Params["uId"];
        if (HttpContext.Current.Request.Files.AllKeys.Any())
        {
            // Get the uploaded image from the Files collection
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];
            if (httpPostedFile != null)
            {
                // Validate the uploaded image(optional)
                // Get the complete file path
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/images"), uId + ".jpg");                
                // Save the uploaded file to "UploadedFiles" folder
                httpPostedFile.SaveAs(fileSavePath);               
                string viewPath = rootPath + uId + ".jpg";
                new DBServices().UpdateUserImage(int.Parse(uId), viewPath);
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize("test string");
            }

        }
        return " ";
       
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // Inserts a user to the database
    // returns the created user from the database via login
    //--------------------------------------------------------------------------
    public string GetUserArticles(string uId)
    {
        try
        {
            User user = new global::User().GetUserById(int.Parse(uId));
            foreach (var item in user.Articles)
            {
                item.GetFullInfo();
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            
            return js.Serialize(user.Articles.ToArray());
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return ex.ToString();
        }

    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //--------------------------------------------------------------------------
    // 
    //--------------------------------------------------------------------------
    public string UpdatePersonalInfo()
    {

        var uId = HttpContext.Current.Request.Params["uId"];
        var fName = HttpContext.Current.Request.Params["firstName"];
        var mName = HttpContext.Current.Request.Params["middleName"];
        var lName = HttpContext.Current.Request.Params["lastName"];
        if (HttpContext.Current.Request.Files.AllKeys.Any())
        {
            // Get the uploaded image from the Files collection
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];
            if (httpPostedFile != null)
            {
                // Validate the uploaded image(optional)
                // Get the complete file path
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/images"), uId + ".jpg");
                // Save the uploaded file to "UploadedFiles" folder
                httpPostedFile.SaveAs(fileSavePath);
                string viewPath =  rootPath +"/images/"+ uId + ".jpg";
                new DBServices().UpdateUserImage(int.Parse(uId), viewPath);
            }
        }
        User user = new User().GetUserById(int.Parse(uId));
        user.GetFullInfo();
        user.FirstName = fName;
        user.MiddleName = mName;
        user.LastName = lName;
        user.FixNulls();
        user.UpdateUserInDatabase();
        return "Done"; 
    }

}
