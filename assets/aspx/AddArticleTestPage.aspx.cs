using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_aspx_AddArticleTestPage : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //Adding to database: new article, with new and old users and keywords.
        List<User> authors = new List<global::User>
        {
            new User("Dor", "middle", "danai", null),
            new User("Lionel", "  ", "messi", null),
            new User("newuser", "testingASPX", "dordanai", null)
        };

        List<Keyword> keywords = new List<Keyword>
        {
            new Keyword(0,"csharp"),
            new Keyword(0,"javascript"),
            new Keyword(0,"css"),
        };
        Article a = new Article(-9, "karnatest001", "karnatest001");
        a.UpdateUsers(authors);
        a.UpdateKeywords(keywords);
        lbl_res.Text= new DBServices().FullArticleInsert(a).ToString(); 
        
    }

    
}