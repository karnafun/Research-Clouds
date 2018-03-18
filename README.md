 
 Research Clouds Platform.

Research Clouds is a college project under development by [ilyakolker](http://github.com/ilyakolker) and [karnafun](http://github.com/karnafun)    
 
We are focusing most of our efforts on using industry coding standards and best practices while building a robust, scalable and well documented code.  
Because of that, our development process is slower than rushing towards the close deadlines.  
we have implemented the following **extra features**
 - [SHA2Encryption class](https://github.com/karnafun/Research-clouds/wiki/SHA2Encryption), hashing with `SHA256` and generating simple random hexadecimal SALT.  
 - [LogManager class](https://github.com/karnafun/Research-clouds/wiki/Log-Manager), sending live reports and exception updates to our dedicated email address ResearchCloudsDe<span>velopment@g</span>mail.com 
 - RCEntity class, implementing OOP Inheritance for better scalability       
 - Strict usage of parameters with SqlCommand object to avoid sql injections
 - SQL Stored Procedures combined with the inheritance to create a better code
 
  ***
  
 In order not to store our passwords in plain text *like suggested by our college!* we have implemented a very basic encryption using
  ```C#
    System.Security.Cryptography.SHA256Managed
```  
For each user we are creating a random SALT using out own method and hashing it together with the password  
Usage:  
after obtaining the users password and salt, call `GenerateSHA256String` using the static `SHA2` class
```chsarp  
string userHash = SHA2.GenerateSHA256String(password, SALT);  
```
 in order to generate a salt, you can call `GenerateSALT` using the same `SHA2` class  
```csharp  
string SALT = SHA2.GenerateSALT();  
```  


If you feel like playing around with the very basics of cryptography ideas, in our [aspx folder](https://github.com/karnafun/Research-clouds/tree/master/assets/aspx) you can find `WebFormClassDemo.aspx.cs` file which is connected to a plain web form where you can manipulate data without having to reach the client side. that page was mainly created for server-side testing
 

  ***  

  `LogManager` class is used to generate reports and live updates to make exception handling and bug fixing a bit easier.  
We are planning on generating log files on the server when certain conditions are met, but until then we are using gmail for that.  
  
`LogManager` is using `System.Net.NetworkCredential` and googles smtp server to send emails on exception and on other certain events.  
Information sent is gathered also using ``StackTrace`` (sending  `stackTrace.GetFrame(1).GetMethod().Name`)

```csharp  
 public static void Report(Exception ex, Object _obj=null)  
``` 
The function requires an exception which will be displayed in detail in the email sent.  
Using an optional Object parameter you can send additional information to be displayed in the email.  

examples:  
```csharp  
    catch (Exception ex)
        {
            LogManager.Report(ex, entity);
            return -1;
        }
```  
The message sent via email will contain not only the exception information but also entity.ToString() which can help me a lot realizing what exactly caused that exception.  
 
Another Method for LogManager is  
```csharp  
public static void Report(string message, Object _obj=null);  
```  
Like in the last Report function, the Object parameter is optional, and you can send a custom message wherever in the code. Example:  
```csharp    
public int UpdateUserInDatabase()
    {
        if (id < 0)
        {
            LogManager.Report("tried to update user with invalid id", this);
            return -1;
        }  

```

In the above example, i am finding the problem even before it reaches DBServices and causes an exception.  
Sending email with an explanation, calling function and the actual object information will help understanding problems you might have issues recreating, especially on production.


	***  
	
 
 
 
 
 
# Todo Before 50%:

 - Fix Navbar for mobile (ugly, and when clicking 'settings' its ugly as fuck)
 - Change the way clusters and users are displayed in CloudsView.html
 	- no specific idea on how it should look, we are not animating yet. but it needs to look good enough for Benny
 
 - Enable adding articles and image for users
 - Fix the way cluster information is displayed on ResearcherProfile.html
 - Upload all the code the production folder on ruppin server
 - verify that everything works



### QA Stages:

1) Register as a new User
	
	- Is field validation working?
	- Are you getting redirected to UserProfile with the new User?


##### if articles and image update is working, verify it now

2) Log out and log back in as a user with information

	- Check that the logout button is working and redirects to login
	- Check 'Edit Profile' button
	- Check if changed information is displayed.
	- Verify that clicking on a cluster displays the cluster information
	- Cancel changes and verify that everything still looks the same

3) Clouds View

	- Check that "clouds view" button from UserProfile.html redirects to CloudsView with all the relevate data
	- Check if clusters and users and displayed currectly

4) Research Profile
	
	- Click on a profile from the clouds view and check that it directs you to that users profile
	- Click "Clouds View" again and verify that you are going back to CloudsView.html as the **USER** and **NOT the Researcher** you viewed his profile. 
	- Choose another researcher and go to his profile also
	- verify that clicking on clusters in ResearchProfile.html show the cluster information
 



