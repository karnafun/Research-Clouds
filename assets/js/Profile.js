


try {
    User = localStorage.getItem("User");
          User = JSON.parse(User);
          var request = { Id: User.Id };

          //Insert uId, Summery, and uImg, articles
          GetUserById(request, UpdateUserInfo, errorCB);

    //TODO:    
    //Insert Affiliations
    //Insert Clusters

} catch (e) {
          alert("No User");
}




$(document).ready(function () {
    //Running before ready 
  
})





function UpdateUserInfo(results) {
    /*
    Filling:
    uId, uSummery, uImg
    */
    results = JSON.parse(results.d);
    User = results;
    $("#uId").html(results.Name);
    $("#uImg").attr("src", results.ImgPath);
    $("#uSummery").html(results.Summery);
    var resString = "";
    $.each(results.Articles, function (index, value) {

        /*
        TODO:
        Build an html li article using value (it has the users in it )
        */
        resString += "<li class='media' style='border-bottom:2px solid #F8FCF7'><h5><a href='#'>" + value.Name + "</a></h5><small> " + value.Users[0].Name + "</small></li>";
        resString += "<br>"; //to understand the raw results meanwhile
    });

    $("#articleList").html(resString);

    resString = "";
    $.each(results.Affiliations, function (index, value) {

        /*
        TODO:
        Build an html li article using value (it has the users in it )
        " <li class='media style='border-bottom:2px solid #F8FCF7'><h5><a href='#'>" + value.Name + "</a></h5><small> " + value.Users[0].Name + "</small></li>"
        " <li><a href='#'>" + value.Name + "</a>, " + value.Users[0].Name + "</li>"
        */
        resString += " <li class='media' style='border-bottom:2px solid #F8FCF7'><h5><a href='#'>" + value.Name + "</a></h5><br/><small> " + value.Users[0].Name + "</small></li>";
        resString += "<br>"; //to understand the raw results meanwhile
    });
  
    $("#affiliationsList").html(resString);
   
    resString = "";
    $.each(results.Clusters, function (index, value) {

        /*
        TODO: Build cluster buttons based on value info
            
        */
        resString += '<span class="btn russian col-xs-6" id="uCluster'+(index+1)+'">'+value.Name+'</span>'
    });
    $("#clusters").html(resString);
}

function UpdateUserArticles(results) {
    /*
    Inserting Articles
    */
    results = JSON.parse(results.d);
}

function errorCB(error) {
    alert("Error: " + error.responseText);
}

function Logout() {
    localStorage.setItem('User', null);
    window.location.replace("../html/LoginTesting.html");
}