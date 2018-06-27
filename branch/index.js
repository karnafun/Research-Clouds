User = { Id: 2 };
LoadUser();
function LoadUser() {
    try {
        GetUserForAnimationAjax({ Id: User.Id }, function (results) {
            try {
                results = JSON.parse(results.d);
            } catch (e) {
                RedirectToLogin();
            }
            User = results;
            ShowPurple();
           // init();
            purple.style.backgroundImage = "url('" + User.ImagePath + "')";
            purple.style.backgroundSize = 'cover';
        }, function (e) { return false; });
    } catch (e) {
        // RedirectToLogin();
    }

}
$(document).ready(function () {
    var aniTrue = true;
    redbool = true;
    window.onresize = function () {
        $("#widthNum").text($(window).width());
        

        if (redbool) {
            if ($(window).width() < 480 && aniTrue == true) {
                SetMobileDiv();
                aniTrue = false
            }
            else if ($(window).width() > 480 && aniTrue == false) {
                BigScreenAnimation();
                aniTrue = true;
            }
        }


    }
})
    

var arrClusterImge = ["../../assets/img/winning-runner.svg", "../../assets/img/kicking-a-footbal-ball.svg", "../../assets/img/soccer-ball-silhouette.svg", "../../assets/img/dumbbells-exercise.svg"];
var window_width = $(window).width();
var window_height = $(window).height();
var go = true; // to not run init() many times
mobilebool = true;
let baseX = 300 , baseY = 300;
let greensX = baseX, greensY = baseY;
let reds = [];
let greens = [];
let purple = GetDiv('purple hidden');
let startX = baseX;
let startY = baseY;
let positions = [];
//ConfigurePositions();

function ShowPurple() {

    p = document.getElementsByClassName("purple");
    p[0].style.opacity = 1;
    
}
function PurpleClick(target) {
    if (go) {
        init();
    }
    ShowReds($(window).width());
    go = false;
}

function init() {
    var count = 2
    for (var i = 0; i < User.Clusters.length; i++) {
        var div = document.createElement('div');
        if (window_width > 480) {
            div.setAttribute('class', 'box russian elem');
        }
        else {
            div.setAttribute('class', 'box russian mobileelem');

        }
       // div.setAttribute('class', 'box russian elem');
        //div.innerHTML = "<p>" + User.Clusters[i].Name + "</p>";
        div.id = i;
        div.style.backgroundImage = "url('" + arrClusterImge[i] + "')";
        div.style.backgroundSize = 'cover';
        div.style.backgroundRepeat = "no-repeat";
        document.getElementById("bd").appendChild(div);
        reds[reds.length] = div;
        count++
        if (count === 3 || count === 6 ) {
            count++;
        }
    }
   
}
function ShowReds(ww) {  
    //red onclicks:
    if (ww > 480) {
        BigScreenAnimation();

    }
    else {
        SetMobileDiv();
       
    }
    


}

function GetDiv(classString) {
    var div = document.createElement('div');
    div.setAttribute('class', classString);
    div.setAttribute('id', "u" + User.Id);
    document.getElementById("bd").appendChild(div);
    GetClickEvent(div);
    return div;
}

function GetClickEvent(target) {
    target.onclick = function (e) {
        if (target.className.search('purple') !== -1) {
            PurpleClick(target);
        } else if (target.className.search('red') !== -1) {
            RedClick(target);
        } else if (target.className.search('green') !== -1) {
            GreenClick(target);
        }
    };
}
function RedClick(target) {
    redbool = false;
    $('.purple').hide();
    target.onclick = function (e) { BackToClusters(target); };
    _targets = AllDivsExcept(target);
    $(_targets).hide();

    $("#"+target.id).animate({ top: 50 + "%", left: 50 + "%", height: 0, width: 10 + "%",'padding-bottom': 10+"%" }, 1000);
    for (var i = 0; i < _targets.length; i++) {
        _targets[i].onclick = function () { return false; };
    }
    GenerateClusters(target.id);
    target.onclick = function (e) { BackToClusters(target, _targets); };

}
function GreenClick(target) {

    target.onclick = function (e) {
        GetUserById({ Id: 2 }, function (results) {
            localStorage.setItem('Researcher', results.d)
            window.location.replace("../assets/html/ResearcherProfile.html");
        }, errorCB)
    };


}

function ShowGreens() {

    var h = 30;
    var w = 30;
    countP = 0;
    countPX = 0;
    if (greens.length % 2 == 0) {
        var ww = $(window).width();
        var up = greens.length / 2;
        AnimateGreens(up, h, w,ww)
    }
    else {
        var up = ((greens.length + 1) / 2);
        var ww = $(window).width();
        AnimateGreens(up,h,w,ww)
    }

}

function AllDivsExcept(target) {
    var all = document.querySelectorAll('.box');
    var list = [];

    for (var i = 0; i < all.length; i++)
        if (all[i] !== target)
            list[list.length] = all[i];


    return list;
}



function AnimateGreens(up, h, w,ww) {
    if (ww > 480) {
        for (var i = 0; i < greens.length; i++) {
            wTest = (w + countPX);
            hTest = (h + countP);
            if (hTest == 50 && wTest == 50) {
                
                $("#" + greens[i].id).animate({ top: (h + 12) + "%", left: (w + countPX) + "%", height: 0, width: 6 + "%", 'padding-bottom': 6 + "%", opacity: 1 }, 1000);
            }
            else {
                if (up <= 0) {
                    countP += 10;
                    $("#" + greens[i].id).animate({ top: (h + countP) + "%", left: (w + countPX) + "%", height: 0, width: 6 + "%", 'padding-bottom': 6 + "%", opacity: 1 }, 1000);
                    
                }
                else {
                    $("#" + greens[i].id).animate({ top: (h + countP) + "%", left: (w + countPX) + "%", height: 0, width: 6 + "%", 'padding-bottom': 6 + "%", opacity: 1  }, 1000);
                    countP -=5;
                }
            }

            countPX += 10;
            up--;
        }
    }
    else {
        for (var i = 0; i < greens.length; i++) {
            wTest = (w + countPX);
            hTest = (h + countP);
            if (hTest == 50 || wTest == 50) {
                countP += 10;
                $("#" + greens[i].id).animate({ top: (h + countP) + "%", left: (w + countPX) + "%", height: 0, width: 6 + "%", 'padding-bottom': 6 + "%", opacity: 1 }, 1000);
            }
            else {
                if (up <= 0) {
                    countP += 10;
                    $("#" + greens[i].id).animate({ top: (h + countP) + "%", left: (w + countPX) + "%", height: 0, width: 6 + "%", 'padding-bottom': 6 + "%", opacity: 1 }, 1000);

                }
                else {
                    $("#" + greens[i].id).animate({ top: (h + countP) + "%", left: (w + countPX) + "%", height: 0, width: 6 + "%", 'padding-bottom': 6 + "%", opacity: 1 }, 1000);
                    countP -= 10;
                }
            }

            countPX += 10;
            up--;
        }
    }
        
    
    
}


function GenerateClusters(_id) {
    var clust = User.Clusters[_id];
    greens = [];
     counter = 1;
    for (var i = 0; i < clust.Users.length; i++) {
        var div = document.createElement('div');
        div.className = 'green elem';
        div.style.backgroundImage = "url('" + clust.Users[i].ImagePath + "')";
        div.style.backgroundSize = 'cover';
        div.style.backgroundRepeat = "no-repeat";
        div.style.zIndex = "-2";
        div.id = 'r' + clust.Users[i].Id;
        GetClickEvent(div);
        greens[greens.length] = div;
        document.getElementById("bd").appendChild(div);
        counter++;
        if (counter > 6) {
            counter = 1;
        }

      //  document.body.appendChild(div)
    }

    ShowGreens();
}

function BackToClusters(target, targets) {
    init();
    target.parentNode.removeChild(target);
    for (var i = 0; i < targets.length; i++) {
        targets[i].parentNode.removeChild(targets[i]);
    }



    for (var i = 0; i < greens.length; i++) {
        $(greens[i]).animate({ top: 50 + "%", left: 50 + "%", height: 0, width: 6 + "%", 'padding-bottom': 6 + "%", opacity: 0 }, 1000, function () {
            for (var i = 0; i < greens.length; i++) {
                greens[i].parentNode.removeChild(greens[i]);
            }});
        
    }

    $('.purple').show();
    PurpleClick();
    ShowPurple();
    redbool = true;
}

/////////////////////////////////////Mobile functions/////////////////////////////////////////////////////
function BigScreenAnimation() {
    for (var t = 0; t < 4; t++) {
        var _div = document.getElementById(t);
        if (mobilebool == false) {
            $("#u" + User.Id).show();
        }
        _div.onclick = function (e) { RedClick(this); };
        switch (t) {
            case 0:
                $("#" + t).animate({ top: 32 + "%", left: 50 + "%", width: 6 + "%", height: 0, 'padding-bottom': 6 + "%", opacity: 1 }, 1000);
                break;

            case 1:
                $("#" + t).animate({ top: 50 + "%", left: 60 + "%", width: 6 + "%", height: 0, 'padding-bottom': 6 + "%", opacity: 1  }, 1000);
                break;
            case 2:
                $("#" + t).animate({ top: 68 + "%", left: 50 + "%", width: 6 + "%", height: 0, 'padding-bottom': 6 + "%", opacity: 1 }, 1000);
                break;
            case 3:
                $("#" + t).animate({ top: 50 + "%", left: 40 + "%", width: 6 + "%", height: 0, 'padding-bottom': 6 + "%", opacity: 1 }, 1000);
                break;

        }
    }
    mobilebool = true;
}

function SetMobileDiv() {
    for (var t = 0; t < 4; t++) {
        var _div = document.getElementById(t);
        if (mobilebool == true) {
            $("#u" + User.Id).hide();
        }
        
        _div.onclick = function (e) { RedClick(this); };
        switch (t) {
            case 0:
                $("#" + t).animate({ top: 35 + "%", left: 35 + "%", width: 15 + "%", height: 0, 'padding-bottom': 15 + "%", opacity: 1 }, 1000);
                break;

            case 1:
                $("#" + t).animate({ top: 35 + "%", left: 65 + "%", width: 15 + "%", height: 0, 'padding-bottom': 15 + "%", opacity: 1  }, 1000);
                break;
            case 2:
                $("#" + t).animate({ top: 65 + "%", left: 65 + "%", width: 15 + "%", height: 0, 'padding-bottom': 15 + "%", opacity: 1 }, 1000);
                break;
            case 3:
                $("#" + t).animate({ top: 65 + "%", left: 35 + "%", width: 15 + "%", height: 0, 'padding-bottom': 15 + "%", opacity: 1 }, 1000);
                break;

        }
    }
    mobilebool = false;
}

