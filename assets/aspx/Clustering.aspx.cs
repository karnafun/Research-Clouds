using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//algorithem ideas:
//Check if keyword = keyword
//check if keyword contains keyword
//split keyword by ' ' and check each sub keyword on its own
// user dictionary to give scores to keywords (user interest =3, article keyword =2, sub keyword =1 ) - in order to create usfull clusters
//User dictionary to count users with same keyword so clusters are created only for 3 users or more.
//Let user add keywords of his own to userInterests if failed to create engouh clusters

//Try running the algorithem on interests vs interests only. then keywords vs keywords only. then if not enough clusters: all vs all.



public partial class assets_aspx_Clustering : System.Web.UI.Page
{
    
    ScholarDBServices SDBS;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SDBS = new ScholarDBServices();
        DBServices db = new DBServices();
        User user = db.GetUserById(6);
        List<string> interests = db.GetUserInterests(user.Id);
        List<User> allUsers = db.GetAllUsers();
        foreach (User _user in allUsers)
        {
            if (user.Id==_user.Id)
            {
 //               continue;
            }
            //Get id list of _user interests!
            ScholarUser scholarUser= SDBS.GetUserByName(_user.FullName());
            if (scholarUser!=null)
            {
                List<string> _interests = SDBS.GetUserInterests(scholarUser.Id);
                lbl_res.Text += "<br/>" + scholarUser .Id+ "<br/>";
                foreach (var item in _interests)
                {
                    lbl_res.Text += "<br/>" + item;
                }
            }
            
            
        }
    }
}