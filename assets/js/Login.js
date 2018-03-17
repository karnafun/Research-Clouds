


function CheckCredentials() {
    var t = $("#title");
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
        alert("Failed");
    }
    
}

function errorCB(error) {
    console.log(error);
    alert("Error:" + error.status);
}

