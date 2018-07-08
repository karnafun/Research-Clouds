﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;

/// <summary>
/// Summary description for ScholarDBServices
/// </summary>
public class ScholarDBServices
{
    string cmdStr, conStr = WebConfigurationManager.ConnectionStrings["Test2DB"].ConnectionString;
    SqlConnection con;
    SqlCommand cmd;
    ScholarDBServices SDBS;
    SqlDataReader reader;
    public ScholarDBServices()
    {
        con = new SqlConnection(conStr);
        SDBS = new ScholarDBServices();
        //
        // TODO: Add constructor logic here
        //
    }

    public ScholarUser GetUserById(int id)
    {
        try
        {
            cmdStr = "select * from scholarUsers where suId = " + id;
            cmd = new SqlCommand(cmdStr, con);
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //int id = Convert.ToInt32(reader["suId"]);
                //string affiliation = reader["affiliation"].ToString();
                //string name = reader["name"].ToString();
                //string email = reader["email"].ToString();
                //string image = reader["image"].ToString();
                //ScholarUser user = new ScholarUser(id, name, affiliation, email, image);
                return GetUserLine(reader);
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            con.Close();
        }
        return null;
    }

    public List<ScholarUser> GetAllScholarUsers()
    {
        try
        {
            cmdStr = "select * from scholarUsers ";
            cmd = new SqlCommand(cmdStr, con);
            con.Open();
            reader = cmd.ExecuteReader();
            List<ScholarUser> users = new List<ScholarUser>();
            while (reader.Read())
            {
                users.Add(GetUserLine(reader));
            }
            return users;
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            con.Close();
        }

    }
    public List<string> GetUserInterests(int UserId)
    {
        try
        {
            cmdStr = "select * from scholarInterests where suId = " + UserId;
            cmd = new SqlCommand(cmdStr, con);
            con.Open();
            reader = cmd.ExecuteReader();
            List<string> interests = new List<string>();
            while (reader.Read())
            {
                //int id = Convert.ToInt32(reader["suId"]);
                interests.Add(reader["interest"].ToString());
            }
            return interests;
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public ScholarUser GetUserByName(string name)
    {
        try
        {
            cmdStr = "select * from scholarUsers where [name] = '" + name + "'";
            cmd = new SqlCommand(cmdStr, con);
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //int id = Convert.ToInt32(reader["suId"]);
                string affiliation = reader["affiliation"].ToString();
                int id = Convert.ToInt32(reader["suId"]);
                string email = reader["email"].ToString();
                string image = reader["image"].ToString();
                ScholarUser user = new ScholarUser(id, name, affiliation, email, image);
                return user;
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            con.Close();
        }
        return null;
    }

    public List<ScholarPublication> GetUserPublications(int userId)
    {
        try
        {
            cmdStr = "select * from scholarPublications where suId = " + userId;
            cmd = new SqlCommand(cmdStr, con);
            con.Open();
            List<ScholarPublication> publications = new List<ScholarPublication>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                publications.Add(GetPublicationLine(reader));
            }
            return publications;
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            con.Close();
        }
    }
    public string SqlScholarUsersToXML()
    {
        List<ScholarUser> users = SDBS.GetAllScholarUsers();
        XDocument doc = new XDocument();
        doc.Add(new XElement("ScholarUsers"));
        foreach (var user in users)
        {
            user.FillInfo();
            doc.Root.Add(user.ToXML());
        }
        string path = HttpContext.Current.Server.MapPath(".");
        string savePath = path + "\\" + "Backup" + ".xml";
        doc.Save(savePath);
        return savePath;
    }
    private ScholarPublication GetPublicationLine(SqlDataReader reader)
    {
        int Id = Convert.ToInt32(reader["pId"]);
        int AuthorId = Convert.ToInt32(reader["suId"]);
        string Author = reader["author"].ToString();
        string Publisher = reader["publisher"].ToString();
        string EPrint = reader["eprint"].ToString();
        string Title = reader["title"].ToString();
        string Url = reader["url"].ToString();
        string Year = reader["year"].ToString();
        string Abstract = reader["abstract"].ToString();
        return new ScholarPublication(Id, AuthorId, Author, Publisher, EPrint, Title, Url, Year, Abstract);
    }
    private ScholarUser GetUserLine(SqlDataReader reader)
    {
        int id = Convert.ToInt32(reader["suId"]);
        string affiliation = reader["affiliation"].ToString();
        string name = reader["name"].ToString();
        string email = reader["email"].ToString();
        string image = reader["image"].ToString();
        ScholarUser user = new ScholarUser(id, name, affiliation, email, image);
        return user;
    }

}