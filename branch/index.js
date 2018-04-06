User = { Id: 1 };
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
            init();
            purple.style.backgroundImage = "url('" + User.ImagePath + "')";
            purple.style.backgroundSize = 'cover';
        }, function (e) { return false; });
    } catch (e) {
        // RedirectToLogin();
    }

}





let baseX = 300 , baseY = 300;
let greensX = baseX, greensY = baseY;
let reds = [];
let greens = [];
let purple = GetDiv('purple hidden');

let startX = baseX;
let startY = baseY;
let positions = [];
ConfigurePositions();


function init() {
    var count = 2
    for (var i = 0; i < User.Clusters.length; i++) {
        var div = document.createElement('div');
        div.setAttribute('class', 'box russian hidden');
        div.innerHTML = "<p>" + User.Clusters[i].Name + "</p>";
        //div.setAttribute('style', 'position:absolute');
        div.id = i;
        document.getElementById("d" + count).appendChild(div);
        reds[reds.length] = div;
        count++
        if (count === 3 || count === 6 ) {
            count++;
        }
    }
   
    ShowPurple();
}
function ShowPurple(target) {
    $(target).show();
    $('.purple').show();
    p = document.getElementsByClassName("purple");
    p[0].style.opacity = 1;

}
function ShowReds() {

 

    //redo onclicks:
    for (var t = 0; t < 4; t++) {
        var _div = document.getElementById(t);
        _div.onclick = function (e) { RedClick(this); };
    }
    anime({
        targets: reds,
        opacity: {
            value: 1
        },
        translateY: [{ value: 150, transition: 800 }, { value: 0, transition: 600 }],
        
        translateX: [{ value: 150, transition: 800 }, { value: 0, transition: 600 }],
        easeeasing: 'easeInOutSine',
        delay: function (el, i, l) { return i * 1000; },



    });

    //anime.speed = .4;
}

function GetDiv(classString) {
    var div = document.createElement('div');
    div.setAttribute('class', classString);
    document.getElementById("user").appendChild(div);
    GetClickEvent(div);
    return div;
}

function PurpleClick(target) {
    ShowReds();
}

function RedClick(target) {
   
    $('.purple').hide();
    var p = $(".purple");
    var position = p.position();
    var rect = target.getBoundingClientRect();
    greensX = rect.left;
    greensY = rect.top;
    target.onclick = function (e) { BackToClusters(target); };
    _targets = AllDivsExcept(target);
    anime({
        targets: _targets,
        opacity: 0,
    });
    for (var i = 0; i < _targets.length; i++) {
        _targets[i].onclick = function () { return false; };
    }
    GenerateClusters(target.id);
    
    var centerRed = anime({
        targets: target,
        scale: 2,
        translateY: 24,
        complete: function (anime) {
            AnimateGreens();
            document.getElementById('user').appendChild(target);
            
        }
    });

    


}
function GreenClick(target) {

}

function ShowGreens() {
    anime(
    {
        targets: greens,
        opacity: 0
    });
}

function AllDivsExcept(target) {
    var all = document.querySelectorAll('.box');
    var list = [];

    for (var i = 0; i < all.length; i++)
        if (all[i] !== target)
            list[list.length] = all[i];


    return list;
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

function ConfigurePositions() {
    startX = baseX;
    startY = baseY;
    positions_upperLeft = [
        { x: startX - 150, y: startY, open: true },
        { x: startX - 300, y: startY, open: true },
        { x: startX - 450, y: startY, open: true },
        { x: startX - 600, y: startY, open: true },
        { x: startX, y: startY - 140, open: true },
        { x: startX - 150, y: startY - 140, open: true },
        { x: startX - 300, y: startY - 130, open: true },
        { x: startX - 450, y: startY - 150, open: true },
        { x: startX - 600, y: startY - 170, open: true }
    ];

    positions_upperRight = [
        { x: startX + 150, y: startY, open: true },
        { x: startX + 300, y: startY, open: true },
        { x: startX + 450, y: startY, open: true },
        { x: startX + 600, y: startY, open: true },
        { x: startX, y: startY + 140, open: true },
        { x: startX + 150, y: startY - 140, open: true },
        { x: startX + 300, y: startY - 130, open: true },
        { x: startX + 450, y: startY - 150, open: true },
        { x: startX + 600, y: startY - 170, open: true }
    ];

    positions_lowerRight = [
        { x: startX, y: startY + 140, open: true },
        { x: startX + 150, y: startY + 140, open: true },
        { x: startX + 300, y: startY + 130, open: true },
        { x: startX + 450, y: startY + 150, open: true },
        { x: startX + 600, y: startY + 170, open: true }
    ];

    positions_lowerLeft = [
        { x: startX - 150, y: startY + 140, open: true },
        { x: startX - 300, y: startY + 130, open: true },
        { x: startX - 450, y: startY + 150, open: true },
        { x: startX - 600, y: startY + 170, open: true }
    ];

}

function AnimateGreens() {

    var counter = 0;

        anime({
            targets: greens,
            opacity: 1,
            translateX: [{ value: function() {return anime.random(-200, 250) }, transition: 800 }, { value: 0, transition: 600 }],
            translateY: [{ value: function () { return anime.random(-200, 250) }, transition: 800 }, { value: 0, transition: 600 }],
            easeeasing: 'easeInOutSine',
           delay: function(el, i, l) { return i * 1000; },

    });

}

function GenerateClusters(_id) {
    var clust = User.Clusters[_id];
    greens = [];
    var counter = 1;
    for (var i = 0; i < clust.Users.length; i++) {
        var div = document.createElement('div');
        div.className = 'box green hidden';
        div.style.backgroundImage = "url('" + clust.Users[i].ImagePath + "')";
        div.style.backgroundSize = 'cover';
        div.id = 'r' + clust.Users[i];
        greens[greens.length] = div;
        document.getElementById("d" + counter).appendChild(div);
        counter++;
        if (counter > 6) {
            counter = 1;
        }
      //  document.body.appendChild(div)
    }
    ShowGreens();
}

function BackToClusters(target) {
    switch (target.id) {
        case '0':
            $(target).hide();
            document.getElementById('d2').appendChild(target);
            break;
        case '1':
            $(target).hide();
            document.getElementById('d4').appendChild(target);
            break;


        case '2':
            $(target).hide();
            document.getElementById('d5').appendChild(target);
            break;
        case '3':
            $(target).hide();
            document.getElementById('d7').appendChild(target);
            break;
    }

    //document.getElementById().appendChild(target);
    for (var i = 0; i < greens.length; i++) {
        greens[i].parentNode.removeChild(greens[i]);
    }
    PurpleClick();
    ShowPurple(target);
}