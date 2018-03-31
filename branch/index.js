
var red = document.querySelector('.red');
var green = document.querySelector('.green');
var blue = document.querySelector('.blue');
var purple = document.querySelector('.purple');
let boxes = [red, green, blue, purple];
anime({
    targets: boxes,
    translateY: [
        { value: 200, duration: 1200 },
        { value: 0, duration: 800 }
    ],
    rotate: '1turn',
    //backgroundColor: '#FFF',
    duration: 2000
    //loop: true
});