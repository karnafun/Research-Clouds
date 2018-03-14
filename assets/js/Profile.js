
User = localStorage.getItem("User");

      try {
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
    $("#uId").text(results.Name);
    $("#uImg").attr("src", results.ImgPath);
    $("#uSummery").text(results.Summery);

    var resString = "";
    $.each(results.Articles, function (index, value) {

        /*
        TODO:
        Build an html li article using value (it has the users in it )
        */
        resString += " <li><a href='#'>" + value.Title + "</a>, " + value.Users[0].Name + "</li>";
        resString += "<br>"; //to understand the raw results meanwhile
    });

    $("#articleList").html(resString);

    resString = "";
    $.each(results.Affiliations, function (index, value) {

        /*
        TODO:
        Build an html li article using value (it has the users in it )
        */
        resString += " <li><a href='#'>" + value.Name + "</a>, " + value.Users[0].Name + "</li>";
        resString += "<br>"; //to understand the raw results meanwhile
    });

    $("#affiliationsList").html(resString);
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