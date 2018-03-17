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
                alert("No rows effected, user was not created");
            }
        }, function (error) {
            alert("Error callback:" + error.responseText)
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
        alert("Passwords do not match");
    } else if (User.Email.toLowerCase().indexOf("gmail") != -1) {
        alert("Please enter your university/college email address")
    } else if (User.FirstName.length < 2 || User.LastName.length < 1) {
        alert("please enter a valid first and last name")
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