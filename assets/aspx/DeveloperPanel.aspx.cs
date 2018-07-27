using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_aspx_DeveloperPanel : System.Web.UI.Page
{
    Institute adminInstitute;
    protected void Page_Load(object sender, EventArgs e)
    {
         adminInstitute = new Institute().GetInstituteById(3);
        FillUserList();
        //FillPersonalInfo(new User().GetUserByEmail("margo@ruppin.ac.il").Id);
        //FillArticle(new User().GetUserByEmail("margo@ruppin.ac.il").Id);

    }

    private void FillUserList()
    {
        var users = new User().GetAllUsers();
        List<User> instituteUsers = new List<global::User>();
        foreach (var user in users)
        {
            foreach (var institute in user.Affiliations)
            {
                if (institute.Id == adminInstitute.Id)
                {
                    instituteUsers.Add(user);
                }
            }
        }

        foreach (var user in instituteUsers)
        {
            ddl_users.Items.Add(new ListItem(user.Name, user.Id.ToString()));
        }
        
        
    }

    private void FillArticlesAndKeywords(int id)
    {
        User user = new global::User().GetUserById(id);
        List<Keyword> userKeywords = new List<Keyword>();
        user.GetFullInfo();
        string articleString = "Articles: </br>";
        foreach (var item in user.Articles)
        {
            var _keywords = item.Keywords;
            foreach (var keyword in _keywords)
            {
                userKeywords.Add(keyword);
            }
            articleString += string.Format("{0}  </br>", item.Title);
        }
        div_articles.InnerHtml = articleString;
        var keywordsString = "User Keywords: </br>";
        foreach (var keyword in userKeywords)
        {
            keywordsString += keyword.Phrase + ", ";
        }
        div_keywords.InnerHtml = keywordsString;
    }

    public void FillPersonalInfo(int uId)
    {
        User user = new global::User().GetUserById(uId);
        img_user.ImageUrl = user.ImagePath;
        lbl_email.Text = user.Email;
        lbl_name.Text = user.Name;
        var summery = user.Summery;
        if (summery.Length < 5)
        {
            summery = "User didnt upload a summery";
            lbl_summery.ForeColor = System.Drawing.Color.Red;
        }
        lbl_summery.Text = summery;
        var interests = user.GetInterests();
        if (interests != null && interests.Count > 1)
        {
            var interestsString = "Main Interests: ";
            for (int i = 0; i < interests.Count; i++)
            {
                interestsString += interests[i];
                if (i < interests.Count - 1)
                {
                    interestsString += " ,";
                }
            }
            lbl_interests.Text = interestsString;
        }
        else
        {
            lbl_interests.Text = "Didn't find any Main Interests";
            lbl_interests.ForeColor = System.Drawing.Color.Red;
        }

    }

    protected void Ddl_users_SelectedIndexChanged(object sender, EventArgs e)
    {
        int uId = int.Parse(ddl_users.SelectedItem.Value);
        FillPersonalInfo(uId);
        FillArticlesAndKeywords(uId);
    }
}