$(document).ready(function () {

    var request = {
        uId: "1"
    }
    GetUserByIdFromDb(request, UpdateInformation, errorCB);
})





function UpdateInformation(results) {

    results = JSON.parse(results.d);
    var user = document.getElementById("uID");
    user.innerHTML = results.Name;
    alert(user.Name);
}



function errorCB(error) {
    alert ( "Error: " + error)
}


function success(results) {
    alert("Success CB");
}

