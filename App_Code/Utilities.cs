using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
    DBServices db;
    ScholarDBServices sdbs;
    public Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
        db = new DBServices();
        sdbs = new ScholarDBServices();
    }


    public void DeleteAmit()
    {
        User user = new User().GetUserByEmail("amit@ruppin.ac.il");
        ScholarUser scholarUser = sdbs.GetUserByName("amit rechavi");
        if (scholarUser != null)
        {
            sdbs.RemoveScholarUser(scholarUser.Id);
        }
        db.RemoveEntity(user);
        //Remove from Scholar

    }
}