using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Institute
/// </summary>
public class Institute :RCEntity
{

    //Fields
    string name;
    string imgPath;
    List<User> users;

    //Properties
    public string Name { get { return name; } set { name = value; } }
    public string ImgPath { get { return imgPath; } set { imgPath = value; } }
    public List<User> Users { get { return users; } }

    //Constructors
    public Institute()
    {
        db = new DBServices();
    }
    public Institute(int id, string name, string imgPath = null)
    {
        db = new DBServices();
        this.id = id;
        this.name = name;
        this.imgPath = imgPath;

    }

    //Methods
    public override string ToString()
    {
        string info = "ID: " + id + "<br>";
        info += "Name: " + Name + "<br>";

        return info;
    }
    public void GetFullInfo()
    {
        users = db.GetInstituteUsers(id);
    }

    //Database Related Methods
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
    public int InsertInstituteToDatabase()
    {
        if (id > 0)
        {
            LogManager.Report("trying to insert an institute with a valid id", this);
        }
        return db.InsertInstitute(this);
    }
    public int UpdateInstituteInDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to update an institute with an invalid id ", this);
            return -1;
        }
        return db.UpdateInstitute(this);
    }
    public int DeleteInstituteFromDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to delete an institute with invalid id", this);
            return -1;
        }
        return db.RemoveEntity(this);
    }

    internal Institute GetInstituteByName(string email)
    {
        return db.GetInstituteByName(email);
    }
}