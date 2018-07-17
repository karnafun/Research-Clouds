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


        // SDBS.IntegrateUser(9);

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