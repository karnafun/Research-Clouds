# Research Clouds Platform.

Research Clouds is a college project under development by [Ilya Kolker](http://github.com/ilyakolker) and [Dor Danai](http://github.com/karnafun)    
 
We're prioritizing the usage of industry coding standards and best practices in order to build a robust, scalable and well documented code, at the cost slower development time.    
If you find a bug or have any suggestions on how we can improve our work, we encourage you to [contact us](https://www.facebook.com/karnafun) and let us know.
 
 
 - [Technical Information](#technical-information)
 - [Project Wiki](https://github.com/karnafun/Research-clouds/wiki)
 - [QA Stages](#qa-stages)
 - [Next Steps](#next-steps)
 - [Running Application On Production Server](https://proj.ruppin.ac.il/bgroup62/prod)
 
### Technical Information:

*We have decided to implement a few **extra features** that were not a part of the  requirements* 
 [See Extra Features](#extra-features)


#### Data Tier:  
all of our `SQL` queries are located in our [SQL Folder](https://github.com/karnafun/Research-clouds/tree/master/sql), the server is hosted by our college under the domain ruppin.ac.il using `Microsoft SQL Server Management Studio`  

#### Logic Tier:
 **All of our logic layer is implemented in `C#`.**  
Connections to the SQL Server are being opened using the `DBServices` class that can be found at [DBServices.cs](https://github.com/karnafun/Research-clouds/blob/master/App_Code/DBServices.cs)   
`aspx` and `aspx.cs` files are currently only for testing server-side, and are not a part of the Client Tier

##### Web Services:  
`AJAX` calls are made by [AjaxCalls.js](https://github.com/karnafun/Research-clouds/blob/master/assets/js/AjaxCalls.js) to [AjaxWebService.cs](https://github.com/karnafun/Research-clouds/blob/master/App_Code/AjaxServices.cs)  

#### Client Tier:
Using mostly [Bootstrap 4](https://getbootstrap.com/) and utilizing HTML5 features.  
[jQuery 3.3.1](https://jquery.com/) is also heavily relied on.  
We might use additional JavaScript libraries like d3.js for our animations in the future. 


   
### Extra Features:
 - [SHA2Encryption class](https://github.com/karnafun/Research-clouds/wiki/SHA2Encryption), hashing with `SHA256` and generating simple random hexadecimal SALT.  
 - [LogManager class](https://github.com/karnafun/Research-clouds/wiki/Log-Manager), sending live reports and exception updates to our dedicated email address ResearchCloudsDe<span>velopment@g</span>mail.com 
 - RCEntity class, implementing OOP Inheritance for better scalability       
 - Strict usage of parameters with SqlCommand object to avoid sql injections
 - SQL Stored Procedures combined with the inheritance to create a better code


If you feel like playing around with the very basics of cryptography ideas, in our [aspx folder](https://github.com/karnafun/Research-clouds/tree/master/assets/aspx) you can find `WebFormClassDemo.aspx.cs` file which is connected to a plain web form where you can manipulate data without having to reach the client side. that page was mainly created for server-side testing
 

  ***  


## QA Stages:

1) Register as a new User
	
	- Is field validation working?
	- Are you getting redirected to UserProfile with the new User?


###### if articles and image update is working, verify it now

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
 


## Next Steps:


 - Fix collapsed Navbar for mobile. when pressing "Settings" the drop down color is "horrible white" which is worse than normal white 
 - Enable adding articles and image for users
 - Fix the way cluster information is displayed on ResearcherProfile.html
 - Start creating the main animation.
 - Get resonable database information instead of the one we made to get points from Benney. (atleast replace oren hazan..)
 - Find a way to store actual passwords not on the database, so we can manually add them to our SQL queries easily when rebuilding the database
 - Learn how to use github tickets so we can keep track of issue fixes in a better way.
 - Build a repository page so we can explain what Research Clouds is without refering non-developers to our github.


