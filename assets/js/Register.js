User = EmptyUserJSON();

$(document).ready(function () {
    $("#btn_submit").click(function (event) {
        RegisterUser();
    });
});


function RegisterUser() {
    if (CheckInformation()) {
        request = {
            userString: JSON.stringify(User)
        }
        InsertUserAjax(request, function (results) {
            results = JSON.parse(results.d)
            if (results.id == null) {
                localStorage.setItem('User', JSON.stringify(results));
                window.location.replace("../html/UserProfile.html");
            } else {
                consloe.log("No rows effected, user was not created");
            }
        }, function (error) {
            consloe.log("Error callback:" + error.responseText)
        });
    }


}
function CheckInformation() {
    User.FirstName = $('#txt_firstName').val();
    User.LastName = $('#txt_lastName').val();
    User.Email = $('#txt_email').val();
    User.Password = $('#txt_password').val();



    var confirmPassword = $('#txt_confirmPassword').val();
    if (User.Password != confirmPassword) {
        $("#p_error").text("Passwords do not match");
    } else if (User.Password.length < 3){
        $("#p_error").text("Password too short");
    }
    else if (VerifyEmail(User.Email)) {
        $("#p_error").text("Please enter your university/college email address")
    } else if (User.FirstName.length < 2 || User.LastName.length < 1) {
        
        $("#p_error").text('please enter a valid first and last name')
    } else {
        return true;
    }
    return false;
}

function EmptyUserJSON() {
    var res = JSON.parse('{"FirstName":null,"MiddleName":"","LastName":null,"Name":"  ","ImagePath":"","Degree":"","IsAdmin":false,"Email":"","Summery":"","BirthDate":"\/Date(-62135596800000)\/","RegistrationDate":"\/Date(-62135596800000)\/","Hash":null,"Salt":null,"Articles":[],"Clusters":[],"Affiliations":[],"Id":0}');
    res.BirthDate = GetDateObject(res.BirthDate);
    res.RegistrationDate = GetDateObject(res.RegistrationDate);
    return res;
}
function GetDateObject(myDate) {
    return new Date(parseInt(myDate.substr(6)));
}

 
function VerifyEmail(emailValue) {
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

    //if (emailValue.toLowerCase().includes('ruppin') || emailValue.toLowerCase().includes('haifa')
    //    || emailValue.toLowerCase().includes('@idc') || emailValue.toLowerCase().includes('harvard')) {
    //    return true;
    //} else { 
    //    return false;
    //}

    return emailValue.length <= 5;
}