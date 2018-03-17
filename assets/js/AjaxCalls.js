//---------------------------------------------------------------------------
// 
//---------------------------------------------------------------------------
function GetUserById(request, successCB, failureCB) {

    dataString = JSON.stringify(request);
    $.ajax({ // ajax call starts
        url: '../../AjaxServices.asmx/GetUserById',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        timeout: 5000, // timeout in miliseconds
        success: successCB, // 
        error: failureCB 
        })// end of ajax call
}


//---------------------------------------------------------------------------
// 
//---------------------------------------------------------------------------
function LoginAjax(request, successCB, failureCB) {

    dataString = JSON.stringify(request);
    $.ajax({ // ajax call starts
        url: '../../AjaxServices.asmx/Login',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        timeout: 5000, // timeout in miliseconds
        success: successCB, // 
        error: failureCB
    })// end of ajax call
}



//---------------------------------------------------------------------------
// 
//---------------------------------------------------------------------------
function UpdateUserAjax(request, successCB, failureCB) {

    dataString = JSON.stringify(request);
    $.ajax({ // ajax call starts
        url: '../../AjaxServices.asmx/UpdateUser',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        timeout: 5000, // timeout in miliseconds
        success: successCB, // 
        error: failureCB
    })// end of ajax call
}

//---------------------------------------------------------------------------
// 
//---------------------------------------------------------------------------
function GetClusterById(request, successCB, failureCB) {

    dataString = JSON.stringify(request);
    $.ajax({ // ajax call starts
        url: '../../AjaxServices.asmx/GetClusterById',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        timeout: 5000, // timeout in miliseconds
        success: successCB, // 
        error: failureCB
    })// end of ajax call
}



//---------------------------------------------------------------------------
// Returns list of clusters with thier users information
// request needs the Id of the user 
//---------------------------------------------------------------------------
function GetUserFullClustersAjax(request, successCB, failureCB) {

    dataString = JSON.stringify(request);
    $.ajax({ // ajax call starts
        url: '../../AjaxServices.asmx/GetUserFullClusters',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        timeout: 5000, // timeout in miliseconds
        success: successCB, // 
        error: failureCB
    })// end of ajax call
}

//---------------------------------------------------------------------------
// Returns a user with full cluster information, ready to be animated.
// request needs the Id of the user 
//---------------------------------------------------------------------------
function GetUserForAnimationAjax(request, successCB, failureCB) {

    dataString = JSON.stringify(request);
    $.ajax({ // ajax call starts
        url: '../../AjaxServices.asmx/GetUserForAnimation',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        timeout: 5000, // timeout in miliseconds
        success: successCB, // 
        error: failureCB
    })// end of ajax call
}



//---------------------------------------------------------------------------
// Insert a new user to the database, doesnt need article, clusters or institutes
// Request Includs:
// FirstName, LastName, Email, Password
//---------------------------------------------------------------------------
function InsertUserAjax(request, successCB, failureCB) {

    dataString = JSON.stringify(request);
    $.ajax({ // ajax call starts
        url: '../../AjaxServices.asmx/InsertUser',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        timeout: 5000, // timeout in miliseconds
        success: successCB, // 
        error: failureCB
    })// end of ajax call
}