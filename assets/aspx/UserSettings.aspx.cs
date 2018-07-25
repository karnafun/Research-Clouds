using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_aspx_UserSettings : System.Web.UI.Page
{
    User user;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = new global::User().GetUserById(21);
        FillPersonalInfo();
    }
    public void FillPersonalInfo()
    {
        txt_email.Text = user.Email;
        txt_summery.Text = user.Summery;
        img_user.ImageUrl = user.ImagePath;
    }
    
}