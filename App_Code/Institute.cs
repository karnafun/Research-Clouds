using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Institute
/// </summary>
public class Institute
{

    //Fields
    DBServices db;
    int id;
    string name;
    string imgPath;
    
    List<User> users;


    //Props
    public int Id { get { return id; } }
    public string Name { get { return name; } }
    public string ImgPath { get { return imgPath; } }
    public List<User> Users
    {
        get
        {
            return users;
        }
    }


    //Constructors
    public Institute()
    {
        db = new DBServices();
    }
    public Institute(int id, string name)
    {
        db = new DBServices();
        this.id = id;
        this.name = name;
       
    }


    //Methods
    public List<Institute> GetAllInstitutes()
    {
        return db.GetAllInstitutes();
    }
    public Institute GetInstituteById(int iId)
    {
        return db.GetInstituteById(iId);
    }
    public List<User> GetInstituteUsersById(int iId)
    {
        return db.GetInstituteUsers(iId);
    }

    public override string ToString()
    {
        string info = "ID: " + id + "<br>";
        info += "Name: " + Name + "<br>";

        return info;
    }

    public void FillObject()
    {
        users = db.GetInstituteUsers(id);
    }
    
}