
User = {};
EditedUser = {};
EditingMode = false;
ClusterClickedId = -1;
try {

    User = localStorage.getItem("User");
    User = JSON.parse(User);
    var request = { Id: User.Id };

    //Insert uId, Summery, and uImg, articles
    if (!User.Updated) {
        GetUserById(request, function (results) {
            try {
                results = JSON.parse(results.d);
            } catch (e) {
                RedirectToLogin();
            }
            User = results;
            EditedUser = $.extend(true, {}, User);
            UpdatePageFromUser();
        }, errorCB);
    } else {
        UpdatePageFromUser();
    }

    $(document).ready(function () {
        //ToggleEditingTools(false);
        ConfigureClickEvents();
        $("#editProfile").on("click", function () {
            $("#editUl").hide();
        })
        $("#email").attr("title", User.Email)

        $(document).on("click", function () {
            $("#editUl").hide();
        });
        $('[data-toggle="tooltip"]').tooltip();
        $('#tool').on('click', function () {
            ViewUser(User.Id);
        });

        if (User.Articles.length == 0) {
            $('#uArticleModale').modal('show');
        }
        //build the profile
        $("#buildProfile_btn").on("click", function () {

            try {
                $("#loader").attr("style", "display:block");
                User.BirthDate = GetDateObject(User.BirthDate);
                User.RegistrationDate = GetDateObject(User.RegistrationDate);
                var request = {
                    userString: JSON.stringify(User)
                };
                FindUserAutomaticallyAjax(request, function (results) {
                    GetUserById({ Id: User.Id }, function (results) {
                        try {
                            results = JSON.parse(results.d);
                            User = results;
                            EditedUser = $.extend(true, {}, User);
                            UpdatePageFromUser();
                        } catch (e) {
                            RedirectToLogin();
                        }

                        //alert("Done, configured the user");
                        $("#loader").attr("style", "display:none");
                    })
                }, errorCB);     //If RefreshUser not working, just past the code itself instead and it'll work             
            } catch (e) {
                console.log(e)
                $(this).attr("style", "display:none");
            }


        });
        $("#edit-user-profile").on("click", function () {
            $("#infoModal_firstName").val(User.FirstName);
            $("#infoModal_middleName").val(User.MiddleName);
            $("#infoModal_lastName").val(User.LastName);
            $("#edit-profile-modal").modal("show");
        })
       
       
    });

} catch (e) {
    RedirectToLogin();
}


function UpdatePageFromUser() {
    $("#uID").html(User.Name);
    $("#uImg").attr("src", User.ImagePath);
    $("#uSummery").html(User.Summery);

    BuildArticles();
    BuildAffiliations();
    BuildClusters();
}



function BuildArticles() {
    var res = "";
    $.each(User.Articles, function (index, value) {
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
    $.each(User.Affiliations, function (index, value) {
        /*
        TODO:
        Build an html li article using value (it has the users in it )
        */
        resString += " <li class='media animated fadeInRight' style='border-bottom:2px solid #F8FCF7'><span class='icon fa-university'></span><div class='media-body'><h5><a href='#'>" + value.Name + "</a></h5><br /></div> "
            + "</li>";
            


    });
    $("#affiliationsList").empty();
    $("#affiliationsList").append(resString);
}
function BuildClusters() {

    var resString = "";
    $.each(User.Clusters, function (index, value) {

        /*
        TODO: Build cluster buttons based on value info            
        */
        resString +=
            '<li onclick="ClusterClick(' + value.Id + ')"  id="uCluster' + (index + 1) + '">' +
            '<span class="icon fa-cloud animated fadeInLeft"></span>' +
            '<br/>' +
            value.Name +
            '<br/>' +
            
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
    for (var i = 0; i < User.Clusters.length; i++) {
        if (User.Clusters[i].Id == _id) {
            $("#p_clusterInfo").text(User.Clusters[i].Name);
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
    //ToggleEditingTools(false);
    ConfigureClickEvents();
    $("#editProfile").on("click", function () {
        $("#editUl").hide();
    })
    $("#email").attr("title", User.Email)

    $(document).on("click", function () {
        $("#editUl").hide();
    });
    $('[data-toggle="tooltip"]').tooltip();
    $('#tool').on('click', function () {
        ViewUser(User.Id);
    });
});

function EditArticle(_id) {
    $.each(User.Articles, function (index, value) {
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

function SaveArticle(e) {

    var title = $("#articleModal_title").val();
    var link = $("#articleModal_link").val();
    var _users = $("#articleModal_authors").val();

    if (title == '' || link == '' || _users == '') {
        PopAlert('Error', 'You MUST fill all relevant field to update an article');
        return null;
    }

    var articleId = $("#articleModal_articleId").val();
    var exists = false;
    for (var i = 0; i < User.Articles.length; i++) {
        var _article = User.Articles[i];
        if (_article.Id == articleId) {
            _article.Title = title;
            _article.Link = link;
            if (!String.prototype.includes) {
                String.prototype.includes = function (search, start) {
                    'use strict';
                    if (typeof start !== 'number') {
                        start = 0;
                    }

                    if (start + search.length > this.length) {
                        return false;
                    } else {
                        return this.indexOf(search, start) !== -1;
                    }
                };
            }

            if (_users.includes(',')) {
                _users = _users.split(',');
            } else {
                _users = [_users, '']
            }
            //New ajax:

            var request = {
                uId: User.Id,
                aId: _article.Id,
                title: _article.Title,
                link: _article.Link,
                authors: _users
            }
            UpdateArticleAjax(request, function (results) {
                User = JSON.parse(results.d);
                localStorage.setItem('User', results.d)
                BuildArticles();
            }, errorCB)

            return;
            try {

                _users = _users.split(',');



                _article.Users = [];
                for (var j = 0; j < _users.length; j++) {
                    _article.Users.push(_users[j]);
                }
            } catch (e) {
                _article.Users = [];
                _article.Users.push(_users);
                console.log("tried to split with , the variable: " + _users);
            }
            exists = true;
        }
    }
    if (!exists) {
        ArticleDetails = { Id: -1, Keywords: [], Link: link, Title: title, Users: [User.Name] };
        User.Articles.push(ArticleDetails);
        //UpdateUserInDatabase();
    }
    // BuildArticles();
    //UpdatePageFromUser();

}

function SaveChanges() {
    $("#loader").attr("style", "display:block");
    EditedUser.BirthDate = GetDateObject(EditedUser.BirthDate);
    EditedUser.RegistrationDate = GetDateObject(EditedUser.RegistrationDate);
    request = {
        userString: JSON.stringify(EditedUser)
    };
    UpdateUserAjax(request, function (results) {
        if (results.d > 1) {
            // alert("User Updated Successfully");
            $("#loader").attr("style", "display:none");
        } else {
            alert(results.d + " rows effected");
            $("#loader").attr("style", "display:none");
        }
    }, function (err) {
        $("#loader").attr("style", "display:none");
        errorCB(err);

    });

}
function CancelChanges() {
    ToggleEditingTools(false);
    UpdatePageFromUser();
}

function GetDateObject(myDate) {
    //return new Date(parseInt(myDate.substr(6)));
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
    $(".fa-edit").click(function (e) {
        EditField(e);
    });

    //$("#articleModal_btn_save").click(function (e) {
    //    SaveArticle(e);
    //    alert("wtf");
    //});
    $('#articleModal_btn_save').unbind('click').click(function (e) {
        SaveArticle(e);
    });

    $("#infoModal_btn_save").unbind('click').click(function (e) {
        $("#loader").attr("style", "display:block");
        User.FirstName = $("#infoModal_firstName").val();
        User.MiddleName = $("#infoModal_middleName").val();
        User.LastName = $("#infoModal_lastName").val();
        User.Name = User.FirstName + " " + User.MiddleName + " " + User.LastName;
        var img = $("#infoModal_imagePath")[0].files[0];

        var request = new FormData();

        if (img != null && img != undefined && img != "") { request.append("UploadedFile", img) }
        request.append("uId", User.Id)
        request.append("firstName", User.FirstName)
        request.append("middleName", User.MiddleName)
        request.append("lastName", User.LastName)
        UpdatePersonalInfoAjax(request, function (results) {

            //Get the user again from the server and update the page
            GetUserById({ Id: User.Id }, function (results) {
                try {
                    results = JSON.parse(results.d);
                    User = results;
                    EditedUser = $.extend(true, {}, User);
                    UpdatePageFromUser();
                    //alert("updated?");
                    $("#loader").attr("style", "display:none");
                    //In case of image caching: 
                    d = new Date();
                    $("#uImg").attr("src", User.ImagePath + "?" + d.getTime());

                    //setTimeout(function () { UpdatePageFromUser();  alert("another check")}, 3000);//Just in case

                } catch (e) {
                    RedirectToLogin();
                }
            })



        }, function (err) {
            $("#loader").attr("style", "display:none");
            errorCB(err)
        })

        //update articles 
        //GetUserArticlesAjax({ uId: User.Id }, function (results) {
        //    results = JSON.parse(results.d);
        //    User.Articles = results;
        //    BuildArticles();
        //}, errorCB)
    });

    $("#summeryModal_btn_save").click(function (e) {
        User.Summery = $("#summeryModal_summery").val();
        UpdatePageFromUser();
        UpdateUserInDatabase();
    });

}

function EditField(e) {
    var sender = e.currentTarget.id;
    if (sender == "edit_name") {
        $("#infoModal_firstName").val(User.FirstName);
        $("#infoModal_middleName").val(User.MiddleName);
        $("#infoModal_lastName").val(User.LastName);

    } else if (sender == "edit_summery") {
        $("#summeryModal_summery").val(User.Summery);
    } else if (sender == "edit_article") {
        //not implemented yet -> need to make one for each article.
    }
}

function UpdateUserInDatabase() {
    User.BirthDate = GetDateObject(User.BirthDate);
    User.RegistrationDate = GetDateObject(User.RegistrationDate);
    UpdateUserAjax({ userString: JSON.stringify(User) }, function (results) {
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
    GetUserById({ Id: _id }, function (results) {
        localStorage.setItem('User', results.d)
        window.location.replace("../html/CloudsView.html");
    }, errorCB)

}

function UpdateImage() {

}

function RefreshUser(results) {
    GetUserById({ Id: User.Id }, function (results) {
        try {
            results = JSON.parse(results.d);
            User = results;
        } catch (e) {
            RedirectToLogin();
        }

        EditedUser = $.extend(true, {}, User);
        UpdatePageFromUser();
        alert("Done, configured the user");
    })
}