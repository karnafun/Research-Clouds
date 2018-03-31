
let baseX = 800, baseY = 300;
let greensX = baseX, greensY = baseY;
var reds = [];
var greens = [];
var purple = GetDiv('purple hidden');

for (var i = 0; i < 4; i++) {
    reds[reds.length] = GetDiv('red hidden');
    greens[greens.length] = GetDiv('green hidden');
}
anime({
    targets: purple,
    opacity: 1,
    translateX: baseX,
    translateY: baseY,
    duration: 800


});


function ShowReds() {
    anime({
        targets: reds,
        opacity: {
            value: 1
        },
        translateY: {
            value: function (target, index) {
                if (index === 0) {
                    return [baseY, baseY + 150];
                } else if (index === 2) {
                    return [baseY, baseY - 150];
                } else {
                    return [baseY, baseY];
                }
            }
        },
        translateX: {
            value: function (target, index) {
                if (index === 1) {
                    return [baseX, baseX + 150];
                } else if (index === 3) {
                    return [baseX, baseX - 150];
                } else {
                    return [baseX, baseX];
                }
            }
        },
        duration: 800



    });
}

function GetDiv(classString) {
    var div = document.createElement('div');
    div.className = 'box ' + classString;
    document.body.appendChild(div);
    GetClickEvent(div);
    //div.addEventListener('click', function (event) {
    //    if (classString.search('purple')!==-1) {
    //        PurpleClick(div);
    //    } else if (classString.search('red')!==-1)
    //    {
    //        RedClick(div);
    //    } else if (classString.search('green') !== -1){
    //        GreenClick(div);
    //    }
    //});
    return div;
}

function PurpleClick(target) {
    ShowReds();
}
function RedClick(target) {
    var rect = target.getBoundingClientRect();
    greensX = rect.left;
    greensY = rect.top;
    _targets = AllDivsExcept(target);
    anime({
        targets: _targets,
        opacity: 0,
        duration: 100
    });
    for (var i = 0; i < _targets.length; i++) {
        _targets[i].onclick = function () { return false;}
    }
    anime({
        targets: target,
        translateX: baseX,
        translateY:baseY
    })

}
function GreenClick(target) {

}

function ShowGreens() {
    var timeline = anime.timeline();
    timeline.add({
        targets: greens,
        opacity: 1,
        translateX: function (target, index) {
            return [greensX, greensX + index * 150];
        },
        translateY: [greensY, greensY + 1]
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

