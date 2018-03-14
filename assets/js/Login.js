


function CheckCredentials() {
    var request = {
        email: $("#username").text,
        password: $("#password").text
    };


}

function Login(results) {

}

function erroCB(error) {
    console.log(error);
    alert("Error:" + error.status);
}