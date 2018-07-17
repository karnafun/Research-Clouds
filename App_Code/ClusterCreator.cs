using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClusterCreator
/// </summary>
public static class ClusterCreator
{
    public static void CreateClusters()
    {
        DBServices db = new DBServices();
        List<User> allUsers = new User().GetAllUsers();
        Dictionary<int, List<User>> dict = new Dictionary<int, List<User>>();
        foreach (Keyword _keyword in new Keyword().GetAllKeywords())
        {
            dict.Add(_keyword.Id, new List<User>());
        }
        foreach (User _user in allUsers)
        {
            _user.GetFullInfo();
            foreach (Article _article in _user.Articles)
            {
                foreach (Keyword _keyword in _article.Keywords)
                {
                    bool userInDictionary = false;
                    foreach (User dictUser in dict[_keyword.Id])
                    {
                        if (dictUser.Id == _user.Id)
                        {
                            userInDictionary = true;
                        }
                    }
                    if (!userInDictionary)
                    {
                        dict[_keyword.Id].Add(_user);
                    }
                }
            }
        }



        //First level: Find out how many users share the same keyword.
        List<Cluster> clusters = new List<Cluster>();
        foreach (var item in dict.Keys)
        {
            if (dict[item].Count >= 3)
            {
                Keyword k = new Keyword().GetKeywordById(item);
                //if (k == null) { continue; }
                Cluster cluster = new Cluster(0, k.Phrase);
                cluster.Users = dict[k.Id];
                clusters.Add(cluster);
            }
        }

        foreach (Cluster cluster in clusters)
        {
            //insert cluster to DB
            if (!VerifyUserCombination(cluster)) { continue; }
            cluster.InsertClusterToDatabase();
            //Get cluster id
            cluster.Id = db.GetClusterByName(cluster.Name).Id;
            foreach (User _user in cluster.Users)
            {
                //Insert cluster user 
                db.AddUserToCluster(_user.Id, cluster.Id);
            }
        }
    }


    
    public static bool VerifyUserCombination(Cluster cluster)
    {
        List<Cluster> allClusters = new Cluster().GetAllClusters();
        foreach (var _cluster in allClusters)
        {
            _cluster.GetFullInfo();
            if (MatchClusterUsers(cluster,_cluster))
            {
                return false;
            }
        }
        return true ;
    }

    static bool MatchClusterUsers(Cluster c1, Cluster c2)
    {
        List<int> c1Ids = new List<int>();
        List<int> c2Ids = new List<int>();
        foreach (var user in c1.Users)
        {
            c1Ids.Add(user.Id);
        }
        foreach (var user in c2.Users)
        {
            c2Ids.Add(user.Id);
        }

        bool step1 = true;
        foreach (var id in c1Ids)
        {
            if (!c2Ids.Contains(id))
            {
                step1 = false;
            }
        }


        bool step2 = true;
        foreach (var id in c2Ids)
        {
            if (!c1Ids.Contains(id))
            {
                step2 = false;
            }
        }

        return step1&&step2;
    }
}