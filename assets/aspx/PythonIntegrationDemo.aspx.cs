using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_aspx_PythonIntegrationDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // PythonServices py = new PythonServices();
        User user = new global::User("amit", " ", "rechavi", null);
        user.Email = "amit@ruppin.ac.il";
        user.FixNulls();
        user.InsertUserToDatabase();
        user = user.GetUserByEmail("amit@ruppin.ac.il");
        user.GetFullInfo();        
        //div_res.InnerText =  py.GetPath();
        //div_res.InnerHtml = py.Run_cmd("InsertUser.py", "Noga alon");
        PythonServices py = new PythonServices();
        py.Run_cmd("InsertUser.py", user.Name);
        ScholarDBServices sdbs = new ScholarDBServices();
        ScholarUser sUser = sdbs.GetUserByName(user.Name);
        sdbs.IntegrateIntoUser(sUser.Id, user.Id);
        user = user.GetUserById(user.Id);
        //string res = user.InsertUserToDatabase().ToString();
        user = user.Relog();
    }
}