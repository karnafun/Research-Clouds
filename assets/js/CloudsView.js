User = {Id: 1};
try {

    User = localStorage.getItem("User");
    User = JSON.parse(User);
    var request = { Id: 1 };

    //Insert uId, Summery, and uImg, articles
    GetUserForAnimationAjax(request, DisplayClusters, errorCB);

} catch (e) {
    RedirectToLogin();
}
var test = 0;
$(document).ready(function () {
    $("#btn_logout").click(function () {
        Logout();
    });    

    $("#editProfile").on("click", function () {
        $("#editUl").hide();
    })
    $(document).on("click", function () {
        $("#editUl").hide();
    })
    $('.fa').on('click', function () {
        ViewUserClouds(User.Id);
    })
});

function Logout() {
    localStorage.setItem('User', null);
    window.location.replace("../html/Login.html");
}

function RedirectToLogin() {
    var message = "You should be redirected to login, are you fucking with me on purpose? \r\nOK to continue, \r\nCancel to go back.";
    if (!confirm(message)) {
        window.location.replace("../html/Login.html");
    }
}

function errorCB(err) {
    console.log(err.responseText);
}

function DisplayClusters(results) {
    var nodesarr = [{ size: 50, id: 0, label: User.Name, shape: "circularImage", image: User.ImagePath }];
    var edgesarr = [];
    User = JSON.parse(results.d);
    var bootstrapClass = "";
    if (User.Clusters.length > 2) {
        bootstrapClass = "col-md-3";
    } else {
        bootstrapClass = "col-md-6";
    }

    var res = "";
    for (var i = 1; i <= User.Clusters.length; i++) {
        //Cluster Loop
        var json = { size: 50, id: i, label: User.Clusters[i-1].Name, shape: "circle" };
        nodesarr.push(json);
            
        
       

        // create an array with edges
        var edgesjson = { from: 0, to: i };
        edgesarr.push(edgesjson);
        

        // create a network

    }
    nodes = new vis.DataSet(nodesarr);
    edges = new vis.DataSet(edgesarr);

    var container = document.getElementById('mynetwork');
    var data = {
        nodes: nodes,
        edges: edges,
    };
    var options = {
        physics: {
            enabled: true,
            barnesHut: {
                avoidOverlap: 0


            }

        },
        edges: {
            width: 8
        },
        nodes: {

        }
        
    };

        options.nodes = {
            color: "#37956f",

    }
         network = new vis.Network(container, data, options);
        //network.fit();
         network.on("selectNode", function (params) {
             if (params.nodes[0] == 0) {
                 alert("it tickles")
             }
             else {
                 var te = (params.nodes[0]) - 1;
                 try {
                     for (var i = 0; i <= User.Clusters[te].Users.length; i++) {
                         nodes.add({ size: 50, id: nodes.length, label: User.Clusters[te].Users[i].Name, shape: "circularImage", image: User.Clusters[te].Users[i].ImagePath });
                         edges.add({ from: params.nodes[0], to: edges.length+1 });
                     }
                 } catch (e) {
                     return null;
                 } 
             }
            
            //User.Clusters[params.nodes[0]].Name
            
  



            
        });

    
}


function showanimation() {
    if (test == 0) {
        var net = document.getElementById("mynetwork");
        net.setAttribute('class', 'mynetwork1 col-lg-8 col-md-8 col-sm-8');
        var main = document.getElementById("main");
        main.setAttribute('class', 'main1');
        test = 1;
        
    }
    else if (test == 1) {
        var net = document.getElementById("mynetwork");
        net.setAttribute('class', 'mynetwork2 col-lg-8 col-md-8 col-sm-8');
        var main = document.getElementById("main");
        main.setAttribute('class', 'main2');
        test = 0;
        network.destroy();
        var request = { Id: User.Id }
        try {
            GetUserForAnimationAjax(request, DisplayClusters, errorCB);
        } catch (e) {

        }
    }

}


function ViewUserClouds(_id) {
    GetUserById({ Id: _id }, function (results) {
        localStorage.setItem('User', results.d)
        window.location.replace("../html/UserProfile.html");
    }, errorCB)

}