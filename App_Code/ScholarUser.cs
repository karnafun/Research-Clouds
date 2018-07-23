using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for scholarUser
/// </summary>
public class ScholarUser
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Affiliation { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
    public List<string> Interests { get; set; }
    public List<ScholarPublication> Publications { get; set; }
    public ScholarUser()
    {
        //
        // TODO: Add constructor logic here
        // id name affiliation email image
    }
    public ScholarUser(int id, string name, string affiliation, string email, string image): this(name,affiliation,email,image)
    {
        Id = id;
    }
    public ScholarUser(string name, string affiliation, string email, string image)
    {
        Name = name;
        Affiliation = affiliation;
        Email = email;
        Image = image;
    }

    public XElement ToXML()
    {
        XElement root = new XElement("ScholarUser");        
        root.Add(new XElement("Name", Name));
        root.Add(new XElement("Affiliation", Affiliation));
        root.Add(new XElement("Email", Email));
        root.Add(new XElement("Image", Image));

        XElement interestsElement = new XElement("Interests");
        foreach (var item in Interests)
        {
            interestsElement.Add(item);
        }
        root.Add(interestsElement);

        XElement publicationsElement = new XElement("Publications");
        foreach (var item in Publications)
        {
            publicationsElement.Add(item.ToXML());
        }
        root.Add(publicationsElement);
        return root;
    }

    public void FillInfo()
    {
        this.Publications = new ScholarDBServices().GetUserPublications(Id);
        this.Interests = new ScholarDBServices().GetUserInterests(Id);
    }
}