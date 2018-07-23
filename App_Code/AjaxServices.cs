using System;
using System.Collections.Generic;
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
    // Inserts a user to the database
    // returns the created user from the database via login
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
}
