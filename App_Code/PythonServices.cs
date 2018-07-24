﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

public class PythonServices
{
    //string path = @"C:\Users\Admin\Desktop\Working with Scholarly\Scholarly";
    public PythonServices()
    {
        //path = HttpContext.Current.Server.MapPath(".") + "\\smart";
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetPath()
    {
        return HttpContext.Current.Server.MapPath(".") +"\\assets\\python";
    }
    public string PathConfiguration()
    {
        return @"C:\Users\Admin\Desktop\research-clouds\assets\python";
    }
    public string GetInfo()
    {
        string res = string.Empty;
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();
        cmd.StandardInput.WriteLine("cd " + PathConfiguration());
        cmd.StandardInput.WriteLine("python dor.py");
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();
        res = cmd.StandardOutput.ReadToEnd();
        return res;
    }

    public string Run_cmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python.exe"; //this is where python.exe is located on the machine. (need full path on server) 
        start.Arguments = string.Format("\"{0}\" \"{1}\"", GetPath() + "\\" + cmd, args); //-- This is where my script.py is
        start.UseShellExecute = false;// Do not use OS shell
        start.CreateNoWindow = true; // We don't need new window
        start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
        start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
        string res = string.Empty;

        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                string temp = string.Empty;
                res= reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")                
            }
        }
        return res + "done";
    }

    public string foo()
    {
        string res = string.Empty;
        Process cmd = new Process();
        cmd.StartInfo.FileName = "python";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();
        cmd.StandardInput.WriteLine("python ");
        cmd.StandardInput.WriteLine("num = 5");
        cmd.StandardInput.WriteLine("num");

        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();
        res = cmd.StandardOutput.ReadToEnd();
        return res;
    }


}