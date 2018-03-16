using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for LogManager
/// </summary>
public static class LogManager
{
    private static  NetworkCredential networkCredentials =  new NetworkCredential("researchcloudsdevelopment", "RandomPassword");
    private static string RCEmailAddress = "researchcloudsdevelopment @gmail.com";
    private static string gmailSmtpAddress = "smtp.gmail.com";
    const string logFilePath = "";

    public static void Report(Exception ex)
    {       
        string message = "Message: "+ex.Message+"\r\n";
        message += "ToString: \r\n"+ex.ToString() + "\r\n";
        message += "Data Keys Information";
        if (ex.Data.Keys.Count>0)
        {
            foreach (var item in ex.Data.Keys)
            {
               message+= item.ToString() + "\r\n";
            }
        }
        StackTrace stackTrace = new StackTrace();
        string res = String.Format("Calling Method:{0}\r\nException Info:\r\n{1}\r\n\r\nStackTraceInfo:\r\n{2}\r\n",
            stackTrace.GetFrame(1).GetMethod().Name, message, stackTrace.ToString());
        SendEmail(res);
    }
    public static void Report(string message)
    {
        StackTrace stackTrace = new StackTrace();
        string res = String.Format("Calling Method: {0}\r\nCustom Message:{1} \r\n\r\nStackTraceInfo:{2}\r\n",
            stackTrace.GetFrame(1).GetMethod().Name, message, stackTrace.ToString());
        SendEmail(res);
    }


    public  static void Logerror1(string _class,string method,  string message)
    {
        string res = "Date: " + DateTime.Now + "\n";
        res += "Class: " + _class + "\r\n";
        res += "Method: " + method+ "\r\n";
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
            SmtpClient SmtpServer = new SmtpClient(gmailSmtpAddress);

            mail.From = new MailAddress(RCEmailAddress);
            mail.To.Add(RCEmailAddress);
            mail.Subject = "Exception Notification";
            mail.Body = message;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = networkCredentials;
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            
        }
        catch (Exception)
        {
            
            throw new SystemException();
        }
    }
}