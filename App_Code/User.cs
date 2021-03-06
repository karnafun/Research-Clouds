﻿using System;
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
    bool administrator, isRegistered;
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

    public void LimitArticles(int max)
    {
        List<Article> _articles = new List<Article>();
        foreach (var item in this.Articles)
        {
            if (_articles.Count >= max) { break; }

            item.GetFullInfo();
            _articles.Add(item);
        }

        this.articles = _articles;
    }



    //public string Name
    //{
    //    get
    //    {
    //        if (!String.IsNullOrWhiteSpace(mName) && mName.Length > 2)
    //        {
    //            return string.Format("{0} {1} {2}", fName, mName, lName);
    //        }
    //        else
    //        {
    //            mName = " ";
    //            return string.Format("{0} {1}", fName, lName);
    //        }
    //    }
    //}
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
    public bool IsRegistered { set { isRegistered = value; } get { return isRegistered; } }
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
    public User(string fName, string mName, string lName, Article article)
    {
        this.fName = fName;
        if (!string.IsNullOrEmpty(mName)) { this.mName = mName; }
        this.lName = lName;
        articles = new List<Article>() { article };
        //  this.articles.Add()
        isRegistered = false;
        BirthDate = DateTime.MaxValue;
        RegistrationDate = DateTime.MaxValue;
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
        // articles = db.GetUserArticles(this.id);
        // affiliations = db.GetUserAffiliations(this.id);

        if (articles != null && articles.Count >= 50)
        {
            List<Article> newArticles = new List<Article>();
            for (int i = 0; i < 50; i++)
            {
                newArticles.Add(articles[i]);
            }
            this.articles = newArticles;
        }
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
        info += "Name: " + FirstName;
        if (string.IsNullOrEmpty(MiddleName)) { info += " " + MiddleName; }
        info += " " + LastName + "<br>";
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
        int rowsEffected = db.InsertUser(this);
        foreach (var item in Articles)
        {
            db.FullArticleInsert(item);
        }
        return rowsEffected;
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
        if (articles != null)
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

    public int UpdateAffiliations(List<Institute> _affiliations)
    {
        foreach (var item in _affiliations)
        {
            if (!this.Affiliations.Contains(item))
            {
                this.affiliations.Add(item);
            }
        }
        int rowseffected = 0;
        foreach (var item in this.Affiliations)
        {
            rowseffected += db.InsertUserAffiliation(id, item.Id);
        }
        return rowseffected;
    }
    public void FixNulls()
    {
        //string fName, mName, lName;
        //string imgPath, degree, hash, salt, email, summery;
        //string password; //Not in the constructor, only for creating users
        //bool administrator, isRegistered;
        //DateTime bdate, registrationDate;
        //List<Article> articles;
        //List<Institute> affiliations;
        //List<Cluster> clusters;
        if (salt == null) { salt = ""; }
        if (email == null) { email = ""; }
        if (hash == null) { hash = ""; }
        if (summery == null) { summery = ""; }
        if (degree == null) { degree = ""; }
        { BirthDate = new DateTime(2018, 1, 1); }
        { RegistrationDate = new DateTime(2018, 1, 1); }
        if (imgPath == null) { imgPath = " "; }



    }

    public void InsertAuthor()
    {

        if (this.articles == null)
        {
            return;
        }
        this.FixNulls();
        this.IsRegistered = false;
        db.InsertAuthor(this);

    }

    //public int UpdateAffiliations(List<Institute> _affiliations)
    //{
    //    foreach (var item in _affiliations)
    //    {
    //        if (!this.Affiliations.Contains(item))
    //        {
    //            this.affiliations.Add(item);
    //        }
    //    }
    //    int rowseffected = 0;
    //    foreach (var item in this.Affiliations)
    //    {
    //        rowseffected += db.InsertUserAffiliation(id, item.Id);
    //    }
    //    return rowseffected;
    //}
}