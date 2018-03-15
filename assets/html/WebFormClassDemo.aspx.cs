using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_html_WebFormClassDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
     
    }

    private void HowInsertUserWorks()
    {
        string password = "hotdog";
        string salt = SHA2.GenerateSALT();
        string hash = SHA2.GenerateSHA256String(password, salt);
        User user = new User(0, "dor", "test", "danai", @"https://goo.gl/GMts4T", "El Doctore", "Pizdaput@gmail.hotdog", "you cannot sum me !", true, hash, salt);
        user.RegistrationDate = DateTime.Now;
        user.BirthDate = DateTime.Now;
        new DBServices().CreateUser(user);
    }
}