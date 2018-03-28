using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RCEntity
/// </summary>
public abstract class RCEntity
{
    protected  int id;
    public int Id { get { return id; } set { id = value; } }
    protected DBServices db;
    protected DBServices Db  { get { return db; }   }

    public RCEntity()
    {
        db = new DBServices();
        //
        // TODO: Add constructor logic here
        //
    }
    public string FirstCharToUpper(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("ARGH!");
        return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
    }

}