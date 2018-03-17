User = {};

$(document).ready(function () {
    GetUserById({ Id: 1 }, FillUserInformation, errorCB);

    $("#btn_panel_getUser").click(function () {
        var request = {
            Id: $("#txt_panel_id").val()
        }
        GetUserById(request, FillUserInformation, errorCB);
    });
});



function FillUserInformation(results) {
    User = JSON.parse(results.d);
    DisplayTableInfo();
}


function DisplayTableInfo() {
    $("#txt_id").val(User.Id);
    $("#txt_firstName").val(User.FirstName);
    $("#txt_middleName").val(User.MiddleName);
    $("#txt_lastName").val(User.LastName);
    $("#txt_degree").val(User.Degree);
    $("#txt_imagePath").val(User.ImagePath);    
    if (User.IsAdmin) {
        $("#cb_administrator").attr('checked', true);
    } else {
        $("#cb_administrator").attr('checked', false);
    }
    $("#txt_email").val(User.Email);
    $("#txt_hash").val(User.Hash);
    $("#txt_salt").val(User.Salt);
    $("#txt_summery").val(User.Summery);

    $("#txt_birthDate").val(GetNormalDate(User.BirthDate));
    $("#txt_registrationDate").val(GetNormalDate(User.RegistrationDate));

}
function UpdateUserFromTable() {
    User.Id = $("#txt_id").val();
    User.FirstName = $("#txt_firstName").val();
    User.MiddleName= $("#txt_middleName").val();
    User.LastName= $("#txt_lastName").val();
    User.Degree= $("#txt_degree").val();
    User.ImagePath= $("#txt_imagePath").val();
    User.BirthDate= $("#txt_birthDate").val();
    User.RegistrationDate = $("#txt_registrationDate").val();
    if ($('#check_id').is(":checked")) {
        User.IsAdmin = true;
    } else {
        User.IsAdmin = false;
    }
    User.Email= $("#txt_email").val();
    User.Hash= $("#txt_hash").val();
    User.Salt= $("#txt_salt").val();
    User.Summery =$("#txt_summery").val();
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
function GetNormalDate(dateInMillisec) {
    return new Date(parseInt(dateInMillisec.substr(6))).toLocaleDateString();
}

function errorCB(error) {
    console.log(error.responseText);
}
