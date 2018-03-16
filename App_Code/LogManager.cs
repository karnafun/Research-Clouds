using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for LogManager
/// </summary>
public static class LogManager
{
    const string logFilePath = "";

    public static void Report(Exception ex)
    {
        string nLine = "\r\n\r\n\r\n";
        string message = ex.Message+ nLine;
        message += ex.ToString()+ nLine;
        if (ex.Data.Keys.Count>0)
        {
            foreach (var item in ex.Data.Keys)
            {
               message+= item.ToString() + "\r\n";
            }
        }
        SendEmail(message);

    }



   public  static void Logerror1(string _class,string method,  string message)
    {
        string res = "Date: " + DateTime.Now + "\n";
        res += "Class: " + _class + "\r\n";
        res += "Method: " + method+ "\r\n";
       // res += "Exeption Type: " + _type+ "\r\n";
        res += "Message: " + message+ "\r\n";
        res += "*********************************************************************************\r\n";
        res += "*********************************************************************************\r\n";
        File.AppendAllText(logFilePath, res);
    }




    private static void SendEmail(string message)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("researchcloudsdevelopment@gmail.com");
            mail.To.Add("researchcloudsdevelopment@gmail.com");
            mail.Subject = "Exception Notification";
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("researchcloudsdevelopment", "RandomPassword");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}