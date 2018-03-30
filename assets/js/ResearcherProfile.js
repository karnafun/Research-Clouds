Researcher = {};
ClusterClickedId = -1;
try {

    Researcher = localStorage.getItem("Researcher");
    Researcher = JSON.parse(Researcher);
    var request = { Id: Researcher.Id };

    //Insert uId, Summery, and uImg, articles
    GetUserById(request, UpdateResearcherInfo, errorCB);

    //TODO:    
    //Insert Affiliations
    //Insert Clusters

} catch (e) {
    RedirectToLogin();
}

$(document).ready(function () {
    $("#btn_logout").click(function () {
        Logout();
    });
    $("#editProfile").on("click", function () {
        $("#editUl").hide();
    })
    $(document).on("click", function () {
        $("#editUl").hide();
    })

    $('.dropdown-toggle').click(function () {
        $(this).next('.dropdown-menu').slideToggle(500);
    });
});

function UpdateResearcherInfo(results) {
    /*
    Filling:
    uId, uSummery, uImg
    */
    try {

        results = JSON.parse(results.d);
    } catch (e) {
        RedirectToLogin();
    }
    Researcher = results;
    $("#uID").html(results.Name);
    $("#uImg").attr("src", results.ImagePath);
    $("#uSummery").html(results.Summery);
    var resString = "";
    $.each(results.Articles, function (index, value) {

        /*
        TODO:
        Build an html li article using value (it has the Researchers in it )
        */
        //to understand the raw results meanwhile
        var usernames = "";
        for (var i = 0; i < value.Users.length; i++) {
            usernames += value.Users[i].Name;
            if (i != value.Users.length - 1) {
                usernames += ", ";
            }
        }
        resString += "<li onclick='return ArticleClick()' class='media' style='border-bottom:2px solid #F8FCF7'><div class='media-body'><h5><a href='" + value.Link + "'>" + value.Title + "</a></h5><br /><p>" + usernames + "</p></div></li > ";

    });
    $("#articleList").empty();
    $("#articleList").append(resString);


    resString = "";
    $.each(results.Affiliations, function (index, value) {

        /*
        TODO:
        Build an html li article using value (it has the users in it )
        */
        // resString += " <li class='media' style='border-bottom:2px solid #F8FCF7'><h5><a href='#'>" + value.Name + "</a></h5><br>,<p>" + value.Users[0].Name + "</p></li>";
        resString += " <li class='media' style='border-bottom:2px solid #F8FCF7'><div class='media-body'><h5><a href='#'>" + value.Name + "</a></h5><br /></div></li>";


    });
    $("#affiliationsList").empty();
    $("#affiliationsList").append(resString);

    resString = "";
    $.each(results.Clusters, function (index, value) {

        /*
        TODO: Build cluster buttons based on value info
            
        */
        resString += '<span onclick="ClusterClick(' + (value.Id) + ')" class="btn light-russian col-xs-6" id="uCluster' + (index + 1) + '">' + value.Name + '</span>'
    });
    $("#clusters").html(resString);
}

function UpdateResearcherArticles(results) {
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
    window.location.replace("../html/Login.html");
}

function RedirectToLogin() {
    var message = "You should be redirected to login, are you fucking with me on purpose? \r\nOK to continue, \r\nCancel to go back.";
    if (!confirm(message)) {
        window.location.replace("../html/Login.html");
    }
}


function ClusterClick(_id) {
    if (ClusterClickedId == _id) {
        //Toggle back
        $("#clusterInfo").animate({ height: '0', width: '0' });
        ClusterClickedId = -1;
        return;
    } else if (ClusterClickedId == -1) {
        $("#clusterInfo").animate({ height: '100px', width: '250px' });
        ClusterClickedId = _id;
    } 

    //change the text
    for (var i = 0; i < Researcher.Clusters.length; i++) {
        if (Researcher.Clusters[i].Id == _id) {
            $("#p_clusterInfo").text(Researcher.Clusters[i].Name);
            ClusterClickedId = _id;
        }
    }


}

function ArticleClick() {
    if (!confirm('Leave Research Clouds and go to the article?')) {
        return false;
    }

}