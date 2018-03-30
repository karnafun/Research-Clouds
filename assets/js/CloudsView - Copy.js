User = {};
var elements = [];
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

    for (var i = 0; i < User.Clusters.length; i++) {
        //Cluster Loop
        var _cluster = User.Clusters[i];
        var divBootclass = document.createElement("div");
        var divToggle = document.createElement("div");
        var h4Cluster = document.createElement("h4");
        var divClusterId = document.createElement("div");
        var h4t = document.createTextNode(_cluster.Name); 

        //adding animation div to array
        elements.push(divBootclass);

        divBootclass.setAttribute("class", bootstrapClass);
        divToggle.setAttribute("data-toggle", "collapse");
        divToggle.setAttribute("data-target", "#" + _cluster.Id);
        h4Cluster.setAttribute("class", "p1 btn light-russian");
        h4Cluster.appendChild(h4t);
        divClusterId.setAttribute("id", _cluster.Id);
        divClusterId.setAttribute("class", "collapsible collapse");
        divToggle.appendChild(h4Cluster);
        
        divBootclass.appendChild(divToggle);



        for (var j = 0; j < _cluster.Users.length; j++) {
            //User Loop
            var _user = _cluster.Users[j];

            //Creating html elements
            var aToreasearcher = document.createElement("a");
            var divAnimation = document.createElement("div");
            var imgresearcher = document.createElement("img");
            var h5researcherName = document.createElement("h5");
            var h5t = document.createTextNode(_user.Name); 

            //Setting attributes
            aToreasearcher.setAttribute("onclick", 'ViewUser(' + _user.Id + ')');
            divAnimation.setAttribute("class", "p2 divAnime");
            imgresearcher.setAttribute("src", _user.ImagePath);
            imgresearcher.setAttribute("width", "100");
            imgresearcher.setAttribute("height", "100");
            h5researcherName.appendChild(h5t);

            //Appending elements 
            divAnimation.appendChild(imgresearcher);
            aToreasearcher.appendChild(divAnimation);
            aToreasearcher.appendChild(h5researcherName);
            divClusterId.appendChild(aToreasearcher);
            divBootclass.appendChild(divClusterId);


           
        }

        //Appending all elements into the DOM
        document.getElementById("clusters").appendChild(divBootclass);

    }
    
   // $('#uArticleModale').modal('show');
    //alert(elements[0].className);
    var playPause = anime({
        targets: '.p2',
        rotate: '1turn',
        delay: function (el, i, l) { return i * 100; },
        direction: 'alternate',
        loop: true,
        autoplay: false
    });



    document.querySelector(elements[0]).onclick = playPause.play;
}


function ViewUser(_id) {
    GetUserById({ Id: _id }, function (results) {
        localStorage.setItem('Researcher', results.d)
        window.location.replace("../html/ResearcherProfile.html");
    }, errorCB)

}


//animated fadeInLeft