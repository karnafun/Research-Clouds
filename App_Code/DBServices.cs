using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for DBServices
/// </summary>
public class DBServices
{    
    string connectionString = WebConfigurationManager.ConnectionStrings["Test1DB"].ConnectionString;
    SqlCommand cmd;
    SqlConnection con;
    SqlDataReader reader;
    public DBServices()
    {
        con = new SqlConnection(connectionString);
    }

    #region Get Commands

    /// <summary>
    /// Validates users credentials based on email and password
    /// </summary>
    /// <param name="email">Users login string, usually the email address</param>
    /// <param name="password">Users password</param>
    /// <returns>User if true, null if false</returns>
    public User Login(string email, string password)
    {
        string cmdStr = "select * from users where email=@email";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@email", email.ToLower());
        //cmd.Parameters.AddWithValue("@hash", hash);

        try
        {
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string hash = SHA2.GenerateSHA256String(password, reader["uSALT"].ToString());
                if (hash != reader["uHash"].ToString())
                {
                    continue;
                }
                else
                {
                    return CurrentLineUser(reader);
                }
            }
            return null;
        }
        catch (Exception ex)
        {

            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all users from the database 
    /// </summary>
    /// <returns>List of all users</returns>
    public List<User> GetAllUsers()
    {
        string cmdStr = "select * from users";
        SqlConnection con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        try
        {

            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(CurrentLineUser(reader));
            }
            return users;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets a specific user from the database by the user id
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns>The user with the wanted id</returns>
    public User GetUserById(int id)
    {
        string cmdStr = "select top(1) * from users where uId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                return CurrentLineUser(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;

        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets a specific user from the database by email address
    /// </summary>
    /// <param name="email">email address of the user</param>
    /// <returns>The user with the wanted email</returns>
    public User GetUserByEmail(string email)
    {
        string cmdStr = "select top(1) * from users where email= @email";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@email", email);
        try
        {
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return CurrentLineUser(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;

        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the clusters of a specific user by the user id
    /// </summary>
    /// <param name="uId">id of the user</param>
    /// <returns>All Clusters associated with this user</returns>
    public List<Cluster> GetUserClusters(int uId)
    {
        string cmdStr = "select * from v_UserClusters where uId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", uId);
        try
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Cluster> clusters = new List<Cluster>();
            while (reader.Read())
            {
               
                clusters.Add(CurrentLineCluster(reader));
            }
            return clusters;

        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the articles for a specific user by user id
    /// </summary>
    /// <param name="uId">the user id</param>
    /// <returns>all articles published by this user</returns>
    public List<Article> GetUserArticles(int uId)
    {
        string cmdStr = "select * from v_UserArticles where uId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", uId);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Article> articles = new List<Article>();
            while (reader.Read())
            {

                articles.Add(CurrentLineArticle(reader));
            }
            return articles;

        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the academic institutional affiliations for a specific user 
    /// by the user id
    /// </summary>
    /// <param name="uId">the user id</param>
    /// <returns>all institutes affiliated with the user</returns>
    public List<Institute> GetUserAffiliations(int uId)
    {
        string cmdStr = "select * from v_UserAffiliations where uId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", uId);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Institute> institutes = new List<Institute>();
            while (reader.Read())
            {               
                institutes.Add(CurrentLineInstitute(reader));
            }
            return institutes;

        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the clusters from the database
    /// </summary>
    /// <returns>All Clusters</returns>
    public List<Cluster> GetAllClusters()
    {
        string cmdStr = "select * from clusters";
        SqlConnection con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Cluster> clusters = new List<Cluster>();
            while (reader.Read())
            {
                clusters.Add(CurrentLineCluster(reader));
            }
            return clusters;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets a cluster by the cluster id
    /// </summary>
    /// <param name="id">the cluster id</param>
    /// <returns>the cluster with the wanted id </returns>
    public Cluster GetClusterById(int id)
    {
        string cmdStr = "select top(1) * from Clusters where cId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return CurrentLineCluster(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;

        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all keywords in a specific cluster by the cluster id
    /// </summary>
    /// <param name="cId">Cluster id</param>
    /// <returns>All keywords associated with the cluster id</returns>
    public List<Keyword> GetClusterKeywords(int cId)
    {
        string cmdStr = "select * from v_ClusterKeywords where cId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", cId);
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            List<Keyword> keywords = new List<Keyword>();
            while (reader.Read())
            {
                
                keywords.Add(CurrentLineKeyword(reader));
            }
            return keywords;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the users of a specific cluster by cluster id
    /// </summary>
    /// <param name="cId">id of the cluster</param>
    /// <returns>All users associated with this cluster</returns>
    public List<User> GetClusterUsers(int cId)
    {
        string cmdStr = "select * from v_UserClusters where cId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", cId);
        try
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(CurrentLineUser(reader));
            }
            return users;

        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the articles from the database
    /// </summary>
    /// <returns></returns>
    public List<Article> GetAllArticles()
    {
        string cmdStr = "select * from articles";
        SqlConnection con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Article> articles = new List<Article>();
            while (reader.Read())
            {
                articles.Add(CurrentLineArticle(reader));
            }
            return articles;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets a specific article by the article id
    /// </summary>
    /// <param name="id">The article id</param>
    /// <returns>The article with the wanted id</returns>
    public Article GetArticleById(int id)
    {
        string cmdStr = "select top(1) * from Articles where aId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return CurrentLineArticle(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;

        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the users in this article by article id
    /// </summary>
    /// <param name="aId">The article id</param>
    /// <returns>All users associated with this article</returns>
    public List<User> GetArticleUsers(int aId)
    {
        string cmdStr = "select * from v_UserArticles where aId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", aId);
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(CurrentLineUser(reader));
            }
            return users;
        }
        catch (Exception ex)
        {

            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the keywords for a specific article by the article id
    /// </summary>
    /// <param name="aId">the article id </param>
    /// <returns>All keywords associated with this article</returns>
    public List<Keyword> GetArticleKeywords(int aId)
    {
        string cmdStr = "select * from v_ArticleKeywords where aId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", aId);
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            List<Keyword> keywords = new List<Keyword>();
            while (reader.Read())
            {
                keywords.Add(CurrentLineKeyword(reader));
            }
            return keywords;
        }
        catch (Exception ex)
        {

            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all Keywords from the database
    /// </summary>
    /// <returns></returns>
    public List<Keyword> GetAllKeywords()
    {
        string cmdStr = "select * from Keywords";
        SqlConnection con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Keyword> keywords = new List<Keyword>();
            while (reader.Read())
            {                          
                keywords.Add(CurrentLineKeyword(reader));
            }
            return keywords;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets a specific keyword by the keyword id
    /// </summary>
    /// <param name="id">The keyword id</param>
    /// <returns>The keyword with the wanted id</returns>
    public Keyword GetKeywordById(int id)
    {
        string cmdStr = "select top(1) * from Keywords where kId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return CurrentLineKeyword(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the clusters with a specific keyword using keyword id
    /// </summary>
    /// <param name="kId">the id of the keyword</param>
    /// <returns>All clusters associated with this keyword</returns>
    public List<Cluster> GetKeywordClusters(int kId)
    {
        string cmdStr = "select * from v_ClusterKeywords where kId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", kId);
        try
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Cluster> clusters = new List<Cluster>();
            while (reader.Read())
            {
                
                clusters.Add(CurrentLineCluster(reader));
            }
            return clusters;

        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all Academic Institutes from the database
    /// </summary>
    /// <returns>All Institutes</returns>
    public List<Institute> GetAllInstitutes()
    {
        string cmdStr = "select * from AcademicInstitutes";
        SqlConnection con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Institute> institutes = new List<Institute>();
            while (reader.Read())
            {       
                institutes.Add(CurrentLineInstitute(reader));
            }
            return institutes;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets a specific academic institute by the institute id
    /// </summary>
    /// <param name="id">The institute id</param>
    /// <returns>The academic institute associated with the provided</returns>
    public Institute GetInstituteById(int id)
    {
        string cmdStr = "select top(1) * from [AcademicInstitutes] where iId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return CurrentLineInstitute(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    /// <summary>
    /// Gets all the users in a specific academic institute by the institute id
    /// </summary>
    /// <param name="iId">Theinsitute id</param>
    /// <returns>All the users associated with the provided institute id</returns>
    public List<User> GetInstituteUsers(int iId)
    {
        string cmdStr = "select * from v_InstituteUsers where iId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", iId);
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {                
                users.Add(CurrentLineUser(reader));
            }
            return users;
        }
        catch (Exception ex)
        {

            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    public List<Article> GetKeywordArticles(int kId)
    {
        string cmdStr = "select * from v_ArticleKeywords where kId = @id";
        con = new SqlConnection(connectionString);
        cmd = new SqlCommand(cmdStr, con);
        cmd.Parameters.AddWithValue("@id", kId);
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            List<Article> articles = new List<Article>();
            while (reader.Read())
            {
                articles.Add(CurrentLineArticle(reader));
            }
            return articles;
        }
        catch (Exception ex)
        {

            LogManager.Report(ex);
            return null;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }

    #endregion

    #region Insert Methods
    public int InsertUser(User user)
    {
        con = new SqlConnection(connectionString);
        cmd = UserCommand(user, true);
        try
        {
            cmd.Connection.Open();
            int res = cmd.ExecuteNonQuery();
            return res;
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return -1;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    public int InsertArticle(Article article)
    {
        cmd = ArticleCommand(article, true);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return -1;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    public int InsertCluster(Cluster cluster)
    {
        cmd = ClusterCommand(cluster, true);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, cluster);
            return -1;
        }finally
        {
            cmd.Connection.Close();
        }
    }
    public int InsertInstitute(Institute institute)
    {
        cmd = InstituteCommand(institute, true);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, institute);
            return -1;            
        }finally
        {
            cmd.Connection.Close();
        }
    }
    public int InsertKeyword(Keyword keyword)
    {
        cmd = KeywordCommand(keyword, true);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return -1;
        }finally
        {
            cmd.Connection.Close();
        }
    }  
    #endregion


    #region Update Methods
    public int UpdateUser(User user)
    {
        cmd = UserCommand(user, false);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return -1;
        }
        finally
        {
            cmd.Connection.Close();
        }

    }
    public int UpdateArticle(Article article)
    {
        cmd = ArticleCommand(article,false);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, article);
            return -1;
        }finally
        {
            cmd.Connection.Close();
        }
    }
    public int UpdateCluster (Cluster cluster)
    {
        cmd = ClusterCommand(cluster, false);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, cluster);
            return -1;
        }finally
        {
            cmd.Connection.Close();
        }
    }
    public int UpdateInstitute(Institute institute)
    {
        cmd = InstituteCommand(institute, false);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, institute);
            return -1;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    public int UpdateKeyword(Keyword keyword)
    {
        cmd = KeywordCommand(keyword, false);
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex);
            return -1;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    #endregion

    #region Remove Methods
    public int RemoveEntity(RCEntity entity)
    {
        
        string cmdStr = "";
        if (entity is User)
        {
            cmdStr = "p_deleteUser";
        }
        else if (entity is Article)
        {
            cmdStr = "p_deleteArticle";
        }
        else if (entity is Cluster)
        {
            cmdStr = "p_deleteCluster";
        }
        else if (entity is Institute)
        {
            cmdStr = "p_deleteInstitute";
        }
        else if (entity is Keyword)
        {
            cmdStr = "p_deleteKeyword";
        }else
        {
            LogManager.Report("trying to remove an unknown RCEntity", entity);
            return -1;
        }
        cmd = new SqlCommand(cmdStr, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = entity.Id;
        try
        {
            cmd.Connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            LogManager.Report(ex, entity);
            return -1;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    
    #endregion

    #region Utility Methods
    private User CurrentLineUser(SqlDataReader reader)
    {
        int id = (int)reader["uId"];
        string fName = reader["firstName"].ToString();
        string mName = reader["middleName"].ToString();
        string lName = reader["lastName"].ToString();
        string degree = reader["degree"].ToString();
        string imgPath = reader["imgPath"].ToString();
        DateTime bdate = reader["birthDate"] != null ? Convert.ToDateTime(reader["birthDate"]) : DateTime.MinValue;
        DateTime registrationDate = reader["registrationDate"] != null ? Convert.ToDateTime(reader["registrationDate"]) : DateTime.MinValue;
        bool administrator = Convert.ToBoolean(reader["administrator"]);
        string email = reader["email"].ToString();
        string summery = reader["summery"].ToString();        
        string hash = reader["uHash"].ToString();
        string salt = reader["uSALT"].ToString();
        bool isRegistered = (bool)reader["isRegistered"];
        return new User(id, fName, mName, lName, imgPath, degree, email, summery, administrator,bdate,registrationDate,hash,salt,isRegistered);
    }
    private Article CurrentLineArticle(SqlDataReader reader)
    {
        int id = (int)reader["aId"];
        string title = reader["title"].ToString();
        string link = reader["aLink"].ToString();
        return new Article(id, title, link);
    }
    private Cluster CurrentLineCluster(SqlDataReader reader)
    {
        int id = (int)reader["cId"];
        string name = reader["cName"].ToString();
        return new Cluster(id,name);
    }
    private Institute CurrentLineInstitute(SqlDataReader reader)
    {
        int id = (int)reader["iId"];
        string name = reader["iName"].ToString();
        return new Institute(id, name);
    }
    private Keyword CurrentLineKeyword(SqlDataReader reader )
    {
        int id = (int)reader["kId"];
        string phrase = reader["phrase"].ToString();
        return new Keyword(id, phrase);
        
    }

    private SqlCommand UserCommand(User user, bool isNewUser)
    {
        SqlCommand _cmd = new SqlCommand();
        StringBuilder cmdStr = new StringBuilder();

        if (isNewUser)
        {
            cmdStr.Append("insert into users values");
            cmdStr.Append("(@firstName,@middleName,@lastName,@degree,@imgPath,@birthDate, @registrationDate,");
            cmdStr.Append("@administrator,@email,@uHash,@uSALT,@summery,@isRegistered)");
        }
        else
        {
            cmdStr.Append(" update users set ");
            cmdStr.Append("firstName = @firstName,");
            cmdStr.Append("middleName = @middleName,");
            cmdStr.Append("lastName = @lastName,");
            cmdStr.Append("degree = @degree,");
            cmdStr.Append("imgPath = @imgPath,");
            cmdStr.Append("birthDate = @birthDate,");
            cmdStr.Append("registrationDate = @registrationDate, ");
            cmdStr.Append("administrator = @administrator,");
            cmdStr.Append("email = @email,");
            cmdStr.Append("summery = @summery ");
            cmdStr.Append(" where uId = @id ");
        }
        _cmd = new SqlCommand(cmdStr.ToString(), con);         
        _cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        _cmd.Parameters.AddWithValue("@middleName", user.MiddleName); //nullable
        _cmd.Parameters.AddWithValue("@lastName", user.LastName);
        _cmd.Parameters.AddWithValue("@degree", user.Degree);
        _cmd.Parameters.AddWithValue("@imgPath", user.ImagePath);
        _cmd.Parameters.AddWithValue("@birthDate", user.BirthDate); //nullable - but i wont allow
        _cmd.Parameters.AddWithValue("@registrationDate", user.RegistrationDate);
        _cmd.Parameters.AddWithValue("@administrator", user.IsAdmin);
        _cmd.Parameters.AddWithValue("@email", user.Email);
        _cmd.Parameters.AddWithValue("@summery", user.Summery);
        _cmd.Parameters.AddWithValue("@isRegistered", user.IsRegistered);
        if (isNewUser)
        {
            _cmd.Parameters.AddWithValue("@uHash", user.Hash); //nullable - but i wont allow
            _cmd.Parameters.AddWithValue("@uSALT", user.Salt); //nullable - but i wont allow
        }
        else
        {
            _cmd.Parameters.AddWithValue("@id", user.Id);
        }

        return _cmd;
    }
    private SqlCommand ArticleCommand(Article article, bool isNewArticle)
    {
        SqlCommand _cmd = new SqlCommand();
        StringBuilder cmdStr = new StringBuilder();

        if (isNewArticle)
        {
            cmdStr.Append("insert into articles values");
            cmdStr.Append("(@title,@link)");            
        }
        else
        {
            cmdStr.Append(" update articles set ");
            cmdStr.Append("title = @title,");
            cmdStr.Append("aLink= @link");           
            cmdStr.Append(" where aId = @id ");
        }
        _cmd = new SqlCommand(cmdStr.ToString(), con);
        _cmd.Parameters.AddWithValue("@id", article.Id);
        _cmd.Parameters.AddWithValue("@title", article.Title);
        _cmd.Parameters.AddWithValue("@link", article.Link);
        return _cmd;
    }
    private SqlCommand ClusterCommand(Cluster cluster, bool isNewCluster)
    {
        SqlCommand _cmd = new SqlCommand();
        StringBuilder cmdStr = new StringBuilder();

        if (isNewCluster)
        {
            cmdStr.Append("insert into Clusters values");
            cmdStr.Append("(@name)");
        }
        else
        {
            cmdStr.Append(" update Clusters set ");
            cmdStr.Append("cName = @name");
            cmdStr.Append(" where cId = @id ");
        }
        _cmd = new SqlCommand(cmdStr.ToString(), con);
        _cmd.Parameters.AddWithValue("@id", cluster.Id);
        _cmd.Parameters.AddWithValue("@name", cluster.Name);
        
        return _cmd;
    }
    private SqlCommand InstituteCommand(Institute institute, bool isNewInstitute)
    {
        SqlCommand _cmd = new SqlCommand();
        StringBuilder cmdStr = new StringBuilder();

        if (isNewInstitute)
        {
            cmdStr.Append("insert into AcademicInstitutes values");
            cmdStr.Append("(@name)");
        }
        else
        {
            cmdStr.Append(" update AcademicInstitutes set ");
            cmdStr.Append("iName = @name ");
            cmdStr.Append(" where iId = @id ");
        }
        _cmd = new SqlCommand(cmdStr.ToString(), con);
        _cmd.Parameters.AddWithValue("@id", institute.Id);
        _cmd.Parameters.AddWithValue("@name", institute.Name);
        
        return _cmd;
    }
    private SqlCommand KeywordCommand(Keyword keyword, bool isNewKeyword)
    {
        SqlCommand _cmd = new SqlCommand();
        StringBuilder cmdStr = new StringBuilder();

        if (isNewKeyword)
        {
            cmdStr.Append("insert into Keywords values");
            cmdStr.Append("(@phrase)");
        }
        else
        {
            cmdStr.Append(" update Keywords set ");
            cmdStr.Append("phrase = @phrase ");
            cmdStr.Append(" where kId = @id ");
        }
        _cmd = new SqlCommand(cmdStr.ToString(), con);
        _cmd.Parameters.AddWithValue("@id", keyword.Id);
        _cmd.Parameters.AddWithValue("@phrase", keyword.Phrase);

        return _cmd;
    }
    #endregion
}