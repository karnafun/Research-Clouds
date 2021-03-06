﻿User = {};
Researcher = {};
EditedUser = {};
EditingMode = false;
ClusterClickedId = -1;





try {
  
    User = localStorage.getItem("User");
    User = JSON.parse(User);
    Researcher = localStorage.getItem("Researcher");
    Researcher = JSON.parse(Researcher);
    var request = { Id: Researcher.Id };

     //Insert uId, Summery, and uImg, articles
    if (!Researcher.Updated) {
        GetUserById(request, function (results) {
            try {
                results = JSON.parse(results.d);
            } catch (e) {
                RedirectToLogin();
            }
            Researcher = results;
            EditedUser = $.extend(true, {}, Researcher);
            UpdatePageFromUser();
            $("#loader").attr("style", "display:none");
        }, errorCB);
    } else {
        UpdatePageFromUser();
        $("#loader").attr("style", "display:none");
    }


    //TODO:    
    //Insert Affiliations
    //Insert Clusters

} catch (e) {
    RedirectToLogin();
}


function UpdatePageFromUser() {
    $("#uID").html(Researcher.Name);
    $("#uImg").attr("src", Researcher.ImagePath);
    $("#uSummery").html(Researcher.Summery);

    BuildArticles();
    BuildAffiliations();
    BuildClusters();

}


function BuildArticles() {
    var res = "";
    $.each(Researcher.Articles, function (index, value) {
        var usernames = "";
        for (var i = 0; i < value.Users.length; i++) {
            var _name = value.Users[i].Name;
            if (value.Users[i].Name == undefined) {
                _name = value.Users[i];
            }
            usernames += "<span class='article_user'>" + _name + "</span>";
            if (i != value.Users.length - 1) {
                usernames += ", ";
            }

        }
        res += "<li class='media animated fadeInLeft' style='border-bottom:2px solid #F8FCF7'>" +
            "<span class='icon fa-graduation-cap'></span>" +
            "<div class='media-body'><br/>" +

            //"<h5 onclick='return ArticleClick()>" +
            "<a onclick='return ArticleClick()' href= '" + value.Link + "'>" +
            value.Title +
            "</a>" +
            // "</h5>"+
            "<p>" + usernames + "</p>" +
            "</div>" +
            "</li>";

    });
    $("#articleList").empty();
    $("#articleList").append(res);

}
function BuildAffiliations() {
    var resString = "";
    $.each(Researcher.Affiliations, function (index, value) {
        /*
        TODO:
        Build an html li article using value (it has the users in it )
        */
        resString += " <li class='media animated fadeInRight' style='border-bottom:2px solid #F8FCF7'><span class='icon fa-university'></span><div class='media-body'><h5><a href='#'>" + value.Name + "</a></h5><br /></div> " +
            "</li>";


    });
    $("#affiliationsList").empty();
    $("#affiliationsList").append(resString);
}
function BuildClusters() {

    var resString = "";
    $.each(Researcher.Clusters, function (index, value) {

        /*
        TODO: Build cluster buttons based on value info            
        */
        resString +=
            '<li onclick="ClusterClick(' + value.Id + ')"  id="uCluster' + (index + 1) + '">' +
            '<span class="icon fa-cloud animated fadeInLeft"></span>' +
            '<br/>' +
            value.Name +
            '<br/>' +
            '<p>' +
            'Here will be a short description of the cluster' +
            '</p>' +
            '</li>'
    });
    $("#uclust").html(resString);
}

function UpdateResearcherArticles(results) {
    /*
    Inserting Articles
    */
    results = JSON.parse(results.d);
}

function errorCB(error) {
    consloe.log("Error: " + error.responseText);
    $("#loader").attr("style", "display:none");
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


//***************************************************************************************************//
//**************************************** Fixed Editing ********************************************//
//**************************************************************************************************//
/* need to clear  unused functions

*/
$(document).ready(function () {
    $("#loader").attr("style", "display:block");
    //ToggleEditingTools(false);
    ConfigureClickEvents();
    $("#editProfile").on("click", function () {
        $("#editUl").hide();
    })
    $("#email").attr("title", Researcher.Email)

    $(document).on("click", function () {
        $("#editUl").hide();
    });
    $('[data-toggle="tooltip"]').tooltip();
    $('#tool').on('click', function () {
        ViewUser(User.Id);
    });

});

function EditArticle(_id) {
    $.each(Researcher.Articles, function (index, value) {
        if (value.Id == _id) {
            $("#articleModal_title").val(value.Title);
            $("#articleModal_link").val(value.Link);
            var res = "";
            for (var i = 0; i < value.Users.length; i++) {
                var _name = value.Users[i].Name;
                if (_name == undefined) {
                    _name = value.Users[i];
                }
                res += _name;
                if (i != value.Users.length - 1) {
                    res += ", ";
                }
            }
            $("#articleModal_authors").val(res);
        }
    });
    $("#articleModal_articleId").val(_id);
}

//function SaveArticle(e) {

//    var title = $("#articleModal_title").val();
//    var link = $("#articleModal_link").val();
//    var _users = $("#articleModal_authors").val();

//    if (title == '' || link == '' || _users == '') {
//        PopAlert('Error', 'You MUST fill all relevant field to update an article');
//        return null;
//    }

//    var articleId = $("#articleModal_articleId").val();
//    var exists = false;
//    for (var i = 0; i < Researcher.Articles.length; i++) {
//        var _article = Researcher.Articles[i];
//        if (_article.Id == articleId) {

//            _article.Title = title;
//            _article.Link = link;
//            try {
//                _users = _users.split(',');
//                _article.Users = [];
//                for (var j = 0; j < _users.length; j++) {
//                    _article.Users.push(_users[j]);
//                }
//            } catch (e) {
//                _article.Users = [];
//                _article.Users.push(_users);
//                console.log("tried to split with , the variable: " + _users);
//            }
//            exists = true;
//        }
//    }
//    if (!exists) {
//        ArticleDetails = { Id: -1, Keywords: [], Link: link, Title: title, Users: [Researcher.Name] };
//        Researcher.Articles.push(ArticleDetails);
//    }
//    BuildArticles();

//}

function SaveChanges() {
    EditedUser.BirthDate = GetDateObject(EditedUser.BirthDate);
    EditedUser.RegistrationDate = GetDateObject(EditedUser.RegistrationDate);
    request = {
        userString: JSON.stringify(EditedUser)
    };
    UpdateUserAjax(request, function (results) {
        if (results.d > 1) {
            alert("User Updated Successfully");
        } else {
            alert(results.d + " rows effected");
        }
    }, errorCB);

}
function CancelChanges() {
    ToggleEditingTools(false);
    UpdatePageFromUser();
}

function GetDateObject(myDate) {
    return new Date(parseInt(myDate.substr(6)));
}


function ConfigureClickEvents() {
    $("#btn_logout").click(function () {
        Logout();
    });

    $("#editProfile").click(function () {
        if (!EditingMode) {
            ToggleEditingTools(true);
        }
    });

    $(".fa-check-square").click(function () {
        SaveChanges();
    });

    $(".fa-times").click(function () {
        // CancelChanges();
    });
    

    $("#articleModal_btn_save").click(function (e) {
        SaveArticle(e);
    });

    $("#infoModal_btn_save").click(function (e) {
        Researcher.FirstName = $("#infoModal_firstName").val();
        Researcher.MiddleName = $("#infoModal_middleName").val();
        Researcher.LastName = $("#infoModal_lastName").val();
        Researcher.Name = Researcher.FirstName + " " + Researcher.MiddleName + " " + Researcher.LastName;
        var img = $("#infoModal_imagePath").val();
        if (img != null && img != undefined && img != "") {
            Researcher.ImagePath = img;
        }
        UpdatePageFromUser();
        UpdateUserInDatabase();
    });

    $("#summeryModal_btn_save").click(function (e) {
        Researcher.Summery = $("#summeryModal_summery").val();
        UpdatePageFromUser();
        UpdateUserInDatabase();
    });

}

function EditField(e) {
    var sender = e.currentTarget.id;
    if (sender == "edit_name") {
        $("#infoModal_firstName").val(Researcher.FirstName);
        $("#infoModal_middleName").val(Researcher.MiddleName);
        $("#infoModal_lastName").val(Researcher.LastName);

    } else if (sender == "edit_summery") {
        $("#summeryModal_summery").val(Researcher.Summery);
    } else if (sender == "edit_article") {
        //not implemented yet -> need to make one for each article.
    }
}

function UpdateUserInDatabase() {
    Researcher.BirthDate = GetDateObject(Researcher.BirthDate);
    Researcher.RegistrationDate = GetDateObject(Researcher.RegistrationDate);
    UpdateUserAjax({ userString: JSON.stringify(Researcher) }, function (results) {
        PopAlert('Success', 'You updated your profile');
    }, errorCB);
}

function PopAlert(type, message) {
    var popup = [
        '<div class="alert alert-info">',
        '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>',
        '<strong>' + type + '!</strong> ' + message,
        '</div>'
    ];
    $('#userProfileCon').prepend(popup.join(''));
}

function ViewUser(_id) {
    $("#loader").attr("style", "display:block");
    GetUserById({ Id: _id }, function (results) {
        window.location.replace("../html/CloudsView.html");
        $("#loader").attr("style", "display:none");
    }, errorCB)

}

function RefreshUser() {

}