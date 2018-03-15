using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SHA2Encryption
/// </summary>
public static class SHA2
{

    


    public static string GenerateSHA256String(string password,string salt)
    {
        SHA256 sha256 = SHA256Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(salt+password);
        byte[] hash = sha256.ComputeHash(bytes);
        return GetStringFromHash(hash);
        
    }

    private static string GetStringFromHash(byte[] hash)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }
        return result.ToString();
    }


    public static string GenerateSALT()
    {
        //TODO: check that salt doesnt exist in the database
        Random rand = new Random();
        string salt = rand.Next().ToString("X")+rand.Next().ToString("X");
        return salt;
    }
}