using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Users
/// </summary>
public class User : RCEntity
{


    //Fields:
    string fName, mName, lName;
    string imgPath, degree, hash, salt, email, summery;
    string password; //Not in the constructor, only for creating users
    bool administrator,isRegistered;
    DateTime bdate, registrationDate;
    List<Article> articles;
    List<Institute> affiliations;
    List<Cluster> clusters;

    //Properties:    
    public string FirstName { get { return fName; } set { fName = value; } }
    public string MiddleName { get { return mName; } set { mName = value; } }
    public string LastName { get { return lName; } set { lName = value; } }
    public string Name
    {
        get
        {
            if (!String.IsNullOrWhiteSpace(mName) && mName.Length > 2)
            {
                return string.Format("{0} {1} {2}", fName, mName, lName);
            }
            else
            {
                mName = " ";
                return string.Format("{0} {1}", fName, lName);
            }
        }
    }
    public string ImagePath { get { return imgPath; } set { imgPath = value; } }
    public string Degree { get { return degree; } set { degree = value; } }
    public bool IsAdmin { get { return administrator; } }
    public string Email { get { return email; } set { email = value; } }
    public string Summery { get { return summery; } set { summery = value; } }
    public DateTime BirthDate { get { return bdate; } set { bdate = value; } }
    public DateTime RegistrationDate { get { return registrationDate; } set { registrationDate = value; } }
    public string Hash { get { return hash; } }
    public string Salt { get { return salt; } }
    public string Password { set { password = value; } }
    public bool IsRegistered { set { IsRegistered = value; } get { return IsRegistered; } }
    public List<Article> Articles
    {
        get
        {
            if (articles == null)
            {
                articles = db.GetUserArticles(id);
            }
            return articles;
        }
    }
    public List<Cluster> Clusters
    {
        get
        {
            if (clusters == null)
            {
                clusters = db.GetUserClusters(id);
            }
            return clusters;
        }
    }
    public List<Institute> Affiliations
    {
        get
        {
            if (affiliations == null)
            {
                affiliations = db.GetUserAffiliations(id);
            }
            return affiliations;
        }
    }

    //Constructors:
    public User()
    {
        db = new DBServices();
    }
    public User(int id, string fName, string mName, string lName, string imgPath, string degree,
        string email, string summery, bool administrator, DateTime bdate, DateTime registrationDate,
        string hash = null, string salt = null, bool isRegistered = true)
    {
        db = new DBServices();
        this.id = id;
        this.fName = fName;
        this.mName = mName;
        this.lName = lName;
        this.imgPath = imgPath;
        this.degree = degree;
        this.email = email;
        this.summery = summery;
        this.administrator = administrator;
        this.bdate = bdate;
        this.registrationDate = registrationDate;
        this.hash = hash;
        this.salt = salt;
        this.isRegistered = isRegistered;
    }


    //Methods
    public void GetFullInfo()
    {
        foreach (Article article in Articles) //Get users for each article
        {
            article.GetFullInfo();
        }
        foreach (Institute institute in Affiliations)
        {
            institute.GetFullInfo();
        }
    }
    public override string ToString()
    {
        string info = "id: " + id + "<br>";
        info += "Name: " + Name + "<br>";
        info += "Image: " + imgPath + "<br>";
        info += "Admin: " + IsAdmin + "<br>";
        info += "Email: " + Email + "<br>";
        info += "Summery: " + Summery + "<br>";
        info += "Birth Date: " + bdate + "<br>";
        info += "Registration Date: " + registrationDate + "<br>";

        return info;
    }

    //Databse Related Methods
    public User Login(string email, string password)
    {
        return db.Login(email, password);
    }
    public User Relog()
    {
        return db.Login(email, password);
    }
    public User GetUserById(int id)
    {
        return db.GetUserById(id);
    }
    public User GetUserByEmail(string email)
    {
        return db.GetUserByEmail(email);
    }
    public List<User> GetAllUsers()
    {
        return db.GetAllUsers();

    }
    public List<Cluster> GetUserFullClusters(int _id = -1)
    {

        User _user = _id > 0 ? GetUserById(_id) : this; //If i get an ID, i return results for that id. else, i give results for this user
        _user.clusters = _user.Clusters; //Gets clusters from db if this.clusters==null
        foreach (Cluster cluster in _user.clusters)
        {
            cluster.GetFullInfo(); //filling cluster with users and keywords
        }

        if (_id < 0)
            clusters = _user.clusters;


        return _user.clusters;

    }
    public int InsertUserToDatabase()
    {
        registrationDate = DateTime.Now;
        salt = SHA2.GenerateSALT();
        hash = SHA2.GenerateSHA256String(password, salt);
        if (id > 0)
        {
            LogManager.Report("trying to insert a user with a valid ID", this);
        }
        DateTime sqlMinDate = new DateTime(1800, 1, 1);
        if (BirthDate < sqlMinDate)
        {
            bdate = sqlMinDate;
        }
        if (RegistrationDate < sqlMinDate)
        {
            registrationDate = DateTime.Now;
        }
        return db.InsertUser(this);
    }
    public int UpdateUserInDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to update user with invalid id", this);
            return -1;
        }
        DateTime sqlMinDate = new DateTime(1800, 1, 1);
        if (BirthDate < sqlMinDate)
        {
            bdate = sqlMinDate;
        }
        if (RegistrationDate < sqlMinDate)
        {
            registrationDate = DateTime.Now;
        }
        if (articles !=null)
            foreach (Article article in articles)
                article.UpdateArticleInDatabase();
        return db.UpdateUser(this);
    }
    public int RemoveUserFromDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to delete a user with invalid id", this);
            return -1;
        }
        return db.RemoveEntity(this);
    }


}