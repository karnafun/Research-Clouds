User = {};
EditedUser = {};
EditingMode = false;
ClusterClickedId = -1;
try {

    User = localStorage.getItem("User");
    User = JSON.parse(User);
    var request = { Id: User.Id };

    //Insert uId, Summery, and uImg, articles
    GetUserById(request, function (results) {
        try {

            results = JSON.parse(results.d);
        } catch (e) {
            RedirectToLogin();
        }
        User = results;
        EditedUser = $.extend(true, {}, User);
        UpdateResearcherInfo();
    }, errorCB);

    //TODO:    
    //Insert Affiliations
    //Insert Clusters

} catch (e) {
    RedirectToLogin();
}


function UpdateResearcherInfo() {
    $("#uID").html(User.Name);
    $("#uImg").attr("src", User.ImagePath);
    $("#uSummery").html(User.Summery);

    BuildArticles();
    BuildAffiliations();
    BuildClusters();

}

function BuildArticles() {
    var resString = "";
    $.each(User.Articles, function (index, value) {
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

}
function BuildAffiliations() {
    var resString = "";
    $.each(User.Affiliations, function (index, value) {
        /*
        TODO:
        Build an html li article using value (it has the users in it )
        */
        resString += " <li class='media' style='border-bottom:2px solid #F8FCF7'><div class='media-body'><h5><a href='#'>" + value.Name + "</a></h5><br /></div></li>";


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
        resString += '<span onclick="ClusterClick(' + (value.Id) + ')" class="btn light-russian col-md-3 col-lg-2 col-sm-12" id="uCluster' + (index + 1) + '">' + value.Name + '</span>'
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

function ConfigureClickEvents() {
    $("#btn_logout").click(function () {
        Logout();
    });

    $("#editProfile").click(function () {
        if (!EditingMode) {
            ToggleEditingTools(true);
        }
    })

    $(".fa-check-square").click(function () {
        SaveChanges();
    })

    $(".fa-times").click(function () {
        // CancelChanges();
    })

    $("#btn_modalSave").click(function (e) {
        SaveArticle(e);
    })
}

//***************************************************************************************************//
//**************************************** Fixed Editing ********************************************//
//**************************************************************************************************//
/* Renamed the following:
file1 - file_image
btnModaleSave = btn_modalSave
Modal-articleName = articleModal_title
Modal-articleLink = articleModal_link
Modal-articleAuthors = articleModal_authors

*/
$(document).ready(function () {
    ToggleEditingTools(false);    
    ConfigureClickEvents();

});

function SaveArticle(e) {

    var title = $("#articleModal_title").val();
    var link = $("#articleModal_link").val();
    var authors = $("#articleModal_authors").val();
    var modaleArtical = document.getElementById("uArticleModale");
    var articleUl = document.getElementById("articleList");

    if (title == '' || link == '' || authors== '') {
        return null;
    }
    else {
        //articleUl.innerHTML += "<li class='media my-4' style='border-bottom:2px solid #F8FCF7'> <div class='media-body'><h5 class='mt-0 mb-1'><a href='" + articleLink.value + "'>" + articletitle.value + "</a></h5><small>" + articleAuthor.value + "<cite> PHD</cite></small></div></li>";
        ArticleDetails = { Id: User.Id, Keywords: [], Link: link, Title: title, Users: [User] };
        EditedUser.Articles.push(ArticleDetails);        
    }


}

function SaveChanges() {
    EditedUser.BirthDate = GetDateObject(EditedUser.BirthDate);
    EditedUser.RegistrationDate= GetDateObject(EditedUser.RegistrationDate);
    request = {
        userString: JSON.stringify(EditedUser)
    }
    UpdateUserAjax(request, function (results) {
        if (results.d>1) {
            alert("User Updated Successfully");
        } else {
            alert(results.d + " rows effected");
        }
    }, errorCB);

}
function ToggleEditingTools(toggleOn) {
    if (toggleOn) {
        $(".fa-edit").show();
        $(".fa-check").show();
        $(".fa-undo").show();
        $(".fa-check-square").show();
        $(".fa-times").show();
        EditingMode = true;
    } else {
        $(".fa-edit").hide();
        $(".fa-check").hide();
        $(".fa-undo").hide();
        $(".fa-check-square").hide();
        $(".fa-times").hide();
        $("#file_image").hide(); //allways hidden
        EditingMode = false;
    }
}
function GetDateObject(myDate) {
    return new Date(parseInt(myDate.substr(6)));
}



//***************************************************************************************************//
//***************************** Editing Profile (Ilya Testing) *************************************
//**************************************************************************************************//
$(document).ready(function () {
    //Running before ready 

    return;


    var userimg = document.getElementById("uImg");
    var file = document.getElementById("file1");
    $(file).hide()
    ///Catch Modale save button//////

    var Savebtn = document.getElementById("btnModalSave")

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    var div = document.getElementById('uID');
    var edit = document.getElementsByClassName('fa-edit');
    var check = document.getElementsByClassName('fa-check');
    var undo = document.getElementsByClassName('fa-undo');
    var summery = document.getElementById("uSummery");
    var editProfile = document.getElementById("editProfile");
    var doneEdit = document.getElementsByClassName("fa-check-square");
    var cancelEdite = document.getElementsByClassName("fa-times");
    $(doneEdit).hide();
    $(cancelEdite).hide();
    $(check).hide();
    $(undo).hide();
    $(edit).hide();
    var stringTemp = div.innerHTML;
    var summeryTemp = summery.innerHTML;


    //Show edit button
    editProfile.onclick = function (e) {
        $(edit).show();
        $(cancelEdite).show()
    }


    cancelEdite[0].onclick = function () {

        $(edit).hide();
        $(doneEdit).hide();
        $(cancelEdite).hide();
        $(check).hide();
        $(undo).hide();

        if (div.contentEditable == true) {
            div.contentEditable = false;
        }
        else {
            return null;
        }
    }




    ///////// Add to body article ////////////////////////////

    Savebtn.onclick = function (e) {
        var modaleArtical = document.getElementById("uArticleModale");
        var articleUl = document.getElementById("articleList");
        var articletitle = document.getElementById("Modal-articleName");
        var articleLink = document.getElementById("Modal-articleLink");
        var articleAuthor = document.getElementById("Modal-articleAuthors");
        if (articletitle.value == '' || articleLink.value == '' || articleAuthor.value == '') {
            return null;
        }
        else {

            articleUl.innerHTML += "<li class='media my-4' style='border-bottom:2px solid #F8FCF7'> <div class='media-body'><h5 class='mt-0 mb-1'><a href='" + articleLink.value + "'>" + articletitle.value + "</a></h5><small>" + articleAuthor.value + "<cite> PHD</cite></small></div></li>";
            ArticleDetails = { Id: User.Id, Keywords: [], Link: articleLink.value, Title: articletitle.value, Users: [User] };
            User.Articles.push(ArticleDetails);

        }


    }


    /////////////////////////////////////////////////////////////////////Changing User Img///////////////////////////////////////////////////////////


    edit[0].onclick = function (e) {
        $(check[0]).show();
        $(doneEdit).show();
        $(cancelEdite).show();
        $(this).hide();
        var test = file.click();
        x = file.value;
    }

    check[0].onclick = function (e) {
        $(edit[0]).show();
        $(this).hide();
        alert(file.value);
        //var tmppath = URL.createObjectURL(event.target.file[0]);
        // userimg.setAttribute("src", String(x));
    }



    ////////////////////////////End Changing User Img/////////////////////////////////////

    ////////////////////////////////////////////////////////Name changing////////////////////////////////////////////////////
    // making changes
    edit[1].onclick = function (e) {
        stringTemp = div.innerHTML;
        $(this).hide();
        $(doneEdit).show();
        $(cancelEdite).show();
        $(check[1]).show();
        $(undo[1]).show();
        div.contentEditable = true;
        div.focus();
        div.setAttribute("class", "edeting");
        $(doneEdit).show();
        $(cancelEdite).show();
    }


    //confirmiing chenges
    check[1].onclick = function (e) {
        var string = div.innerHTML;
        $(doneEdit).show();
        $(cancelEdite).show();
        $(check[1]).hide();
        $(undo[1]).hide();
        if (string == '') {
            div.innerHTML = "edit Line";
            $(edit[1]).show();
            div.contentEditable = false;
            div.className = "endEdeting";


        }
        else {
            string = string.trim();
            // div.style.backgroundColor = '#ffffff';
            div.style.border = '';
            div.contentEditable = false;
            $(edit[1]).show();
            div.className = "endEdeting";
            User.Name = string;
        }

    }

    //Undoing changes
    undo[1].onclick = function (e) {
        $(this).hide();
        $(check[1]).hide();
        $(edit[1]).show();
        $(doneEdit).show();
        $(cancelEdite).show();
        div.innerHTML = stringTemp.trim();
        div.contentEditable = false;
        div.className = "endEdeting";



    }




    /////////////////////////////////////////////////////////Canging Summery////////////////////////////////////////////////////

    edit[2].onclick = function (e) {
        summeryTemp = summery.innerHTML;
        $(this).hide();
        $(check[2]).show();
        $(undo[2]).show();
        $(doneEdit).show();
        $(cancelEdite).show();
        summery.contentEditable = true;
        summery.focus();
        summery.setAttribute("class", "edeting");
    }


    //confirmiing chenges
    check[2].onclick = function (e) {
        var summeryString = summery.innerHTML;
        $(check[2]).hide();
        $(undo[2]).hide();
        $(doneEdit).show();
        $(cancelEdite).show();
        if (summeryString == '') {
            summery.innerHTML = "Add Summery";
            $(edit[2]).show();
            summery.contentEditable = false;
            summery.className = "Endsummery text-left";


        }
        else {
            summeryString = summeryString.trim();
            //  summery.style.backgroundColor = '#ffffff';
            summery.style.border = '';
            summery.contentEditable = false;
            $(edit[2]).show();
            summery.className = "Endsummery text-left";

        }

    }

    //Undoing changes
    undo[2].onclick = function (e) {
        $(this).hide();
        $(check[2]).hide();
        $(edit[2]).show();
        summery.innerHTML = summeryTemp.trim();
        summery.contentEditable = false;
        summery.className = "Endsummery text-left";



    }






    doneEdit[0].onclick = function () {
        User.Name = div.innerHTML;
        if (ArticleDetails = ! null) {
            User.Articles.push(ArticleDetails);
        }
        var jsonString = JSON.stringify(User);
        var request = {
            userString: jsonString
        }
        UpdateUserAjax(request, UserUpdated, errorCB);

    }


    function UserUpdated(results) {
        alert("OK");
    }
    function errorCB(error) {
        console.log(error.responseText);
    }

})