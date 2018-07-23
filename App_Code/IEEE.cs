using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Xml.Linq;
/// <summary>
/// Summary description for IEEE
/// </summary>
public class IEEE
{
    string api_key= "4rvd3a3k97ja4h4zjmuj83e3";
    string api_path = "http://ieeexploreapi.ieee.org/api/v1/search/";
    public IEEE()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<string> GetArticleTerms(string title)
    {
        using (var client = new WebClient())
        {
            title = title.Replace(' ', '+');
            string query = api_path + "articles?article_title=" + title + "&apikey=" + api_key+ "&format=xml";
            var responseString = client.DownloadString(query);
            XElement res= XElement.Parse(responseString);            
            return GetTerms(res);
        }
        
    }

    public List<string> GetTerms(XElement element)
    {
        List<string> termList = new List<string>();
        foreach (XElement article in element.Elements())
        {
            if (article.Name == "article")
            {
                foreach (XElement termIndex in article.Elements())
                {
                    if (termIndex.Name == "index_terms")
                    {
                        foreach (XElement termType in termIndex.Elements())
                        {
                            if (termType.Name == "ieee_terms" || termType.Name == "author_terms")
                            {
                                foreach (XElement term in termType.Elements())
                                {
                                    termList.Add(term.Value);
                                }
                            }
                        }
                    }
                }
            }
        }
        return termList;
    }
}