User = {};
try {

    User = localStorage.getItem("User");
    User = JSON.parse(User);
    var request = { Id: User.Id };

    //Insert uId, Summery, and uImg, articles
    GetUserForAnimationAjax(request, DisplayClusters, errorCB);

} catch (e) {
    RedirectToLogin();
}

$(document).ready(function () {
    $("#btn_logout").click(function () {
        Logout();
    });    
});

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

function errorCB(err) {
    console.log(err.responseText);
}

function DisplayClusters(results) {
    User = JSON.parse(results.d);
    var bootstrapClass = "";
    if (User.Clusters.length > 2) {
        bootstrapClass = "col-md-3";
    } else {
        bootstrapClass = "col-md-6";
    }

    var res = "";
    for (var i = 0; i < User.Clusters.length; i++) {
        //Cluster Loop
        var _cluster = User.Clusters[i];

        res += "<div class='" + bootstrapClass + "'>";
        res += "<div data-toggle='collapse' data-target='#" + _cluster.Id + "'>";
        res += "<h4 class='btn light-russian'>" + _cluster.Name + "</h4>";
        res += "</div>";

        res += "<div id='" + _cluster.Id + "' class='collapsible collapse'>"
        for (var j = 0; j < _cluster.Users.length; j++) {
            //User Loop
            var _user = _cluster.Users[j];

           
            res += "<a onclick='ViewUser(" + _user.Id + ")'><div class='animated fadeInLeft' style='border-right: 10px solid #37956f;'> <img src='" + _user.ImagePath + "' width='100' height='100' /></div>";
            res += "<h5>" + _user.Name + " </h5></a>";
           
        }
        res += "</div>";
        res += "</div>";
    }
    $("#clusters").html(res);
    $('#uArticleModale').modal('show');
    
}


function ViewUser(_id) {
    GetUserById({ Id: _id }, function (results) {
        localStorage.setItem('Researcher', results.d)
        window.location.replace("../html/ResearcherProfile.html");
    }, errorCB)

}