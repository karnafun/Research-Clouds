User = {};
try {

    User = localStorage.getItem("User");
    User = JSON.parse(User);
    var request = { Id: User.Id };

    //Insert uId, Summery, and uImg, articles
    GetUserForAnimationAjax(request, DisplayClusters, errorCB);

} catch (e) {
    RedirectToLogin();
}
var test = 0;
var clicked = true;
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
    var nodesarr = [{ id: 0, label: User.Name, shape: "circularImage", image: User.ImagePath,size: 50 }];
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
        var json = {id: i, label: User.Clusters[i-1].Name, shape: "circle" , size: 50, group:'clusters'};
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
        interaction: {
            hover: true,
        },
        physics: {
            enabled: true,
            barnesHut: {
                avoidOverlap: 0


            }

        },
        edges: {
            width: 5,
            length: 125
        },
        groups: {
            clusters: {
                shape: 'icon',
                icon: {
                    face: 'FontAwesome',
                    code: '\uf0c2',
                    size: 65,
                    color: 'white'
                }
                 
            }
        }
        
    };

        options.nodes = {
            color: "#37956f",

    }
         network = new vis.Network(container, data, options);
         network.on("hoverNode", function (params) {
             alet("hover");
         });
         network.on("selectNode", function (params) {
             var te = (params.nodes[0]) - 1;
             if (params.nodes[0] > 4) {
                 for (var i = 0; i < nodesarr.length; i++) {
                     if (nodesarr[i].id == params.nodes[0]) {
                         ViewResearcher(nodesarr[i].borderWidthSelected);
                         break;
                     }
                 }
                 
                 
             }
             else  {
                 if (params.nodes[0] == 0) {
                     alert("it tickles")
                 }
                 else {
                     try {
                         for (var i = 0; i <= User.Clusters[te].Users.length; i++) {
                             var notemp = { size: 35, id: nodes.length, label: User.Clusters[te].Users[i].Name, shape: "circularImage", image: User.Clusters[te].Users[i].ImagePath, borderWidthSelected: User.Clusters[te].Users[i].Id };
                             nodesarr.push(notemp)
                             nodes.add(notemp);
                             var edtemp = { from: params.nodes[0], to: edges.length + 1 };
                             edges.add(edtemp);
                         }
                     } catch (e) {
                         return null;
                     }
                 }
             }


            
            
  



            
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

function ViewResearcher(_id) {
    GetUserById({ Id: _id }, function (results) {
        localStorage.setItem('Researcher', results.d);
        window.location.replace("../html/ResearcherProfile.html");

    }, errorCB)
}
