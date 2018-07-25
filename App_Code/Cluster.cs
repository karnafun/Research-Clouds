using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cluster
/// </summary>
public class Cluster : RCEntity, IComparable
{


    //Fields:    
    string name;
    List<Keyword> keywords;
    List<User> users;
    public bool visible = true;
    //Properties:    
    public string Name { get { return name; } set { name = value; } }
    public List<User> Users { get { return users; } set { users = value; } }
    public List<Keyword> Keywords { get { return keywords; } }


    //Constructors:
    public Cluster()
    {
        db = new DBServices();
    }
    public Cluster(int id, string name)
    {
        db = new DBServices();
        this.id = id;
        this.name = name;
    }

    //Methods:
    public override string ToString()
    {
        string info = "ID: " + id + "<br>";
        info += "Name: " + name + "<br>";

        return base.ToString();
    }
    public void GetFullInfo()
    {
        users = db.GetClusterUsers(id);
        keywords = db.GetClusterKeywords(id);
    }

    //Database Related Methods
    public List<Cluster> GetAllClusters()
    {
        return db.GetAllClusters();
    }
    public Cluster GetClusterById(int _id)
    {
        return db.GetClusterById(_id);
    }
    public int InsertClusterToDatabase()
    {
        if (id > 0)
        {
            LogManager.Report("Inserting a new cluster, with a valid id", this);
        }
        return db.InsertCluster(this);
    }
    public int UpdateClusterInDatabase()
    {
        if (id < 1)
        {
            LogManager.Report("Tried to update a cluster with an invalid id", this);
            return -1;
        }
        return db.UpdateCluster(this);
    }
    public int DeleteClusterFromDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to delete a cluster with invalid id", this);
            return -1;
        }
        return db.RemoveEntity(this);
    }

    public int CompareTo(object obj)
    {
        try
        {

            return (obj as Cluster).users.Count.CompareTo(this.users.Count);
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, obj);
            return -1;
        }
    }

    public int UpdateVisiblity(int uId)
    {
        return db.UpdateClusterVisibility(this, uId);
    }
}