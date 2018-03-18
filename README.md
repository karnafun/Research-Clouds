 
###Todo:

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


#####if articles and image update is working, verify it now

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
 



