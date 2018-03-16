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
        try
        {
            string a = "s";
            var t = int.Parse(a);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);

        }
    }

    private void HowInsertUserWorks()
    {
        User user = new User(0, "dor", "test", "danai", @"https://goo.gl/GMts4T", "El Doctore", "Pizdaput@gmail.hotdog", "you cannot sum me !", true);
        user.Password = "hotdog";
        user.RegistrationDate = DateTime.Now;
        user.BirthDate = DateTime.Now;
        user.InsertUserToDatabase();        
    }
}