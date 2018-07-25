$(document).ready(function () {
    $(document).on('keypress', function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            document.getElementById("login-btn").click();
        }
    });
})


function CheckCredentials() {
    if ($("#username").val().trim() == "" || $("#username").val().trim().length <=3 ) {
        $("#p_error").text("Enter Email Address");
        return;
    }
    if ($("#password").val() == "") {
        $("#p_error").text("Enter Password");
        return;
    }
    var request = {
        email: $("#username").val(),
        password: $("#password").val()
    };
    LoginAjax(request,Login,errorCB)

}

function Login(results) {
    results = JSON.parse(results.d);
    if (results!=null && results.Name!=null) {
        localStorage.setItem('User', JSON.stringify(results))
        window.location.replace("../html/UserProfile.html");        
    } else {
        // TODO: Implement login fail logic
        $("#p_error").text("Invalid Username or Password");
    }
    
}

function errorCB(error) {
    console.log(error);
    $("#p_error").text("Connection Error");
}

