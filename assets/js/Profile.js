

$(document).ready(function () {

    //Insert uId, Summery, and uImg, articles
    var request = { Id: "1" };
    GetUserById(request, UpdateUserInfo, errorCB);

    //TODO:    
    //Insert Affiliations
    //Insert Clusters
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

