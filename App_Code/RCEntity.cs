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
    public int Id { get { return id; } }
    protected DBServices db;
    public RCEntity()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
}