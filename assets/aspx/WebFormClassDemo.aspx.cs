using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_html_WebFormClassDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        User user = new DBServices().GetUserByEmail("messi@ruppin.ac.il");
        user.MiddleName = "";
        int effected = user.UpdateUserInDatabase();

        Response.Write(effected+"\r\n User is "+user.Name);
    }
    
    private void HowInsertUserWorks()
    {
        User user = new User(0, "dor", "test", "danai", @"https://goo.gl/GMts4T", "El Doctore", "Pizdaput@gmail.hotdog", "you cannot sum me !", true);
        user.Password = "hotdog";
        user.RegistrationDate = DateTime.Now;
        user.BirthDate = DateTime.Now;
        user.InsertUserToDatabase();        
    }

    private void GenerateSoccerDataBaseHashes()
    {
        string messi = SHA2.GenerateSHA256String("messi123", "20E6494B4207A90D");
        string neymar = SHA2.GenerateSHA256String("neymar123", "3C3C58961451D04");
        string hazan = SHA2.GenerateSHA256String("hazan123", "66C26C8D58996B8F");
        string ronaldo = SHA2.GenerateSHA256String("ronaldo123", "7EE9BB521CE704BA");
        string bale = SHA2.GenerateSHA256String("bale123", "2813B5F0BA1E74");

        string res = "messi: " + messi + "\r\n";
        res += "neymar: " + neymar + "\r\n";
        res += "hazan: " + hazan + "\r\n";
        res += "ronaldo: " + ronaldo + "\r\n";
        res += "bale: " + bale + "\r\n";
        Response.Write(res);
    }
}