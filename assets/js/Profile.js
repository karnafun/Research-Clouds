$(document).ready(function () {

    var request = {
        uId: "1"
    }
    GetUserByIdFromDb(request, UpdateInformation, errorCB);
})





function UpdateInformation(results) {
    var t = results;
}



function errorCB(error) {
    alert ( "Error: " + error)
}


function success(results) {
    alert("Success CB");
}

