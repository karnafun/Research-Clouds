
let baseX = 800, baseY = 300;
let greensX = baseX, greensY = baseY;
let reds = [];
let greens = [];
let purple = GetDiv('purple hidden');

let startX = baseX;
let startY = baseY;
let positions = [];
ConfigurePositions();
init();

function init() {
    for (var i = 0; i < 4; i++) {
        reds[reds.length] = GetDiv('red hidden');        
    }
    for (var j = 0; j < 10; j++) {
        greens[greens.length] = GetDiv('green hidden');
    }
    ShowPurple();
}
function ShowPurple() {
    anime({
        targets: purple,
        opacity: 1,
        translateX: baseX,
        translateY: baseY,
        duration: 500


    });

}
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
        duration: 500



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
        _targets[i].onclick = function () { return false; };
    }
    anime({
        targets: target,
        translateX: baseX,
        translateY: baseY
    });

    AnimateGreens();

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

function ConfigurePositions() {
    startX = baseX;
    startY = baseY;
    positions_upperLeft = [
        { x: startX-150, y: startY, open: true },
        { x: startX - 300, y: startY, open: true },
        { x: startX - 450, y: startY , open: true },
        { x: startX - 600, y: startY, open: true },
        { x: startX , y: startY - 140, open: true },
        { x: startX - 150, y: startY -140, open: true },
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
    ]

}

function AnimateGreens() {
    var counter = 0;
    for (var j = 0; j < greens.length; j++) {        
        if (counter===0) {
            _pos = positions_upperLeft[Math.floor(Math.random() * positions_upperLeft.length)];
            counter++
        } else if (counter === 1) {
            _pos = positions_lowerLeft[Math.floor(Math.random() * positions_lowerLeft.length)];
            counter++
        } else if (counter === 2) {
            _pos = positions_lowerRight[Math.floor(Math.random() * positions_lowerRight.length)];
            counter++;
        } else if (counter === 3) {
            _pos = positions_upperRight[Math.floor(Math.random() * positions_upperRight.length)];
            counter++;
        }
        if (!_pos.open) {
            j--;
            continue;
        }
        anime({
            targets: greens[j],
            opacity: 1,
            translateX: [baseX, _pos.x],
            translateY: [baseY, _pos.y]
        });

        if (counter>3) {
            counter = 0;
        }
    }
}