using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class assets_aspx_ScholarDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScholarDBServices SDBS = new ScholarDBServices();
        
            SDBS.IntegrateUser(4);
        
    }
}