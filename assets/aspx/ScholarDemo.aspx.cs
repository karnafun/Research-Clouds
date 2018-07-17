using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class assets_aspx_ScholarDemo : System.Web.UI.Page
{
    int index;
    DBServices db;
    List<User> users;
    ScholarDBServices SDBS;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        db = new DBServices();
        SDBS = new ScholarDBServices();


        //IntegrateAllScholarlyUsers();
       // ClusterCreator.CreateClusters();        
        foreach (var item in db.GetAllUsers())
        {
            if (item.Id >= 6)
            {
                string email = item.FirstName.ToLower() + "@ruppin.ac.il";
                string salt = SHA2.GenerateSALT();
                string password = "123";
                string hash = SHA2.GenerateSHA256String(password, salt);
                db.UpdateEmail(item.Id, email);
                db.UpdatePassword(item.Id, salt, hash);
            }
        }
        //SDBS.IntegrateUser(9);
    }
    public void IntegrateAllScholarlyUsers()
    {
        List<ScholarUser> scholarUsers = SDBS.GetAllScholarUsers();
        foreach (ScholarUser scholarUser in scholarUsers)
        {
            SDBS.IntegrateUser(scholarUser.Id);
        }
    }
    public void DataFromIEEE()
    {
        if (Session["index"] == null)
        {
            index = 8;
        }
        else
        {
            index = Convert.ToInt32(Session["index"]);
        }
        IEEE ieee = new IEEE();
        List<string> res = ieee.GetArticleTerms("Dynamic thermal management for high-performance microprocessors");

        lbl_name.Text = res.ToString();
        //GET IEEE DATA YA NOOB
    }

    public void ShowUser()
    {
        users = db.GetAllUsers();
        User _user = users[index];
        lbl_name.Text = _user.FirstName;
        if (_user.MiddleName != string.Empty) { lbl_name.Text += " " + _user.MiddleName; }
        lbl_name.Text += " " + _user.LastName;
        img.ImageUrl = _user.ImagePath;
        _user.GetFullInfo();
        foreach (var article in _user.Articles)
        {
            div_articles.InnerHtml += article.ToString() + "<br />";
        }
        var t = 0;
    }
}