﻿User = {};

$(document).ready(function () {
    
    var div = document.getElementById('uID');
    var edit = document.getElementsByClassName('fa-edit');
    var check = document.getElementsByClassName('fa-check');
    var undo = document.getElementsByClassName('fa-undo');
    var summery = document.getElementById("uSummery");
    $("#file1").hide();
    $(check).hide();
    $(undo).hide();
   // GetUserById({ Id: 1 }, FillUserInformation, errorCB);
    $("#Ilya").click(function () {
        var request = {
            //Id: $("#txt_panel_id").val()
            Id: 2
        }
        GetUserById(request, FillUserInformation, errorCB);
        log("GetUserById() with Id:" + request.Id);
    });


    $("#btn_panel_updateUser").click(function () {
        log("Updating JSON user from the table");
        UpdateUserFromTable();
        //{"FirstName":"Lionel",
        var jsonString = JSON.stringify(User);       
        var request = {
            userString: jsonString
        }
        UpdateUserAjax(request, UserUpdated, errorCB);
        log("UpdateUserAjax() with user Id:" + User.Id);
    });
});

function UserUpdated(results) {
    var effected = results.d;
    if (effected<0) {
       // alert(effected + " Rows Effected");
        return;
    }
    GetUserById({ Id: User.Id }, FillUserInformation, errorCB);
    log("GetUserById() with Id:" + User.Id);
}

function FillUserInformation(results) {
    User = JSON.parse(results.d);
    log("Recivied User Information for Id: "+User.Id);
    DisplayTableInfo();
}


function DisplayTableInfo() {
    //  $("#txt_id").val(User.Id);
    $("#uID").html(User.FirstName + " " + User.LastName);
    //$("#txt_middleName").val(User.MiddleName);
    //$("#txt_lastName").val(User.LastName);
    //$("#txt_degree").val(User.Degree);
    $("#uImg").attr("src", String(User.ImagePath));
    //if (User.IsAdmin) {
    //    $("#cb_administrator").attr('checked', true);
    //} else {
    //    $("#cb_administrator").attr('checked', false);
    //}
    //$("#txt_email").val(User.Email);
    //$("#txt_hash").val(User.Hash);
    //$("#txt_salt").val(User.Salt);
    $("#uSummery").html(User.Summery);

    //$("#txt_birthDate").val(GetDisplayDate(User.BirthDate));
    //$("#txt_registrationDate").val(GetDisplayDate(User.RegistrationDate));

}
function UpdateUserFromTable() {
    User.Id = $("#txt_id").val();
    User.FirstName = $("#txt_firstName").val();
    User.MiddleName= $("#txt_middleName").val();
    User.LastName= $("#txt_lastName").val();
    User.Degree= $("#txt_degree").val();
    User.ImagePath= $("#txt_imagePath").val();
    if ($('#check_id').is(":checked")) {
        User.IsAdmin = true;
    } else {
        User.IsAdmin = false;
    }
    User.Email= $("#txt_email").val();
    User.Hash= $("#txt_hash").val();
    User.Salt= $("#txt_salt").val();
    User.Summery = $("#txt_summery").val();

    //alert(GetDateObject(User.BirthDate));
    User.BirthDate = GetDateObject(User.BirthDate)
    User.RegistrationDate = GetDateObject(User.RegistrationDate);
   // User.BirthDate=  $("#txt_birthDate").val();
   // User.RegistrationDate = $("#txt_registrationDate").val();
}
function StringInformation() {
    var info = "Id: " + User.Id;
    info += "<br>Name: " + User.Name;
    info += "<br>Image Path: " + User.ImgPath;
    info += "<br>Birth Date: " + User.BirthDate;
    info += "<br>Registration: " + User.RegistrationDate;
    info += "<br>Summery: " + User.Summery;
    return info;
}



//Utilities
function GetDisplayDate(myDate) {
    return new Date(parseInt(myDate.substr(6))).toLocaleDateString();
}
function GetDateObject(myDate) {
    return new Date(parseInt(myDate.substr(6)));
}

function errorCB(error) {
    console.log(error.responseText);
}

function log(str) {   
    $("#logs").html(function (i, val) {
        return val + str + "<br>";
    });
    
}
