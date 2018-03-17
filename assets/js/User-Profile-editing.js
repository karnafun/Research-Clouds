window.onload = function () {
    var userimg = document.getElementById("uImg");
    var file = document.getElementById("file1");
    $(file).hide()
    ///Catch Modale save button//////

    var Savebtn = document.getElementById("btnModalSave")

    ///////////////////////////////
    var div = document.getElementById('uID');
    var edit = document.getElementsByClassName('fa-edit');
    var check = document.getElementsByClassName('fa-check');
    var undo = document.getElementsByClassName('fa-undo');
    var summery = document.getElementById("uSummery");
    $(check).hide();
    $(undo).hide();
    var stringTemp = div.innerHTML;
    var summeryTemp = summery.innerHTML;
    ////////////////////////////Changing User Img/////////////////////////////////////


    edit[0].onclick = function (e) {
        $(check[0]).show();
        $(this).hide();
        file.click();
        x = file.value;
    }

        check[0].onclick = function (e) {
            $(edit[0]).show();
            $(this).hide();
            alert(file.value);
            //var tmppath = URL.createObjectURL(event.target.file[0]);
            // userimg.setAttribute("src", String(x));
        }



    ////////////////////////////End Changing User Img/////////////////////////////////////

    ////////////////////////////////////////////////////////Name changing////////////////////////////////////////////////////
    // making changes
    edit[1].onclick = function (e) {
        stringTemp = div.innerHTML;
        $(this).hide();
        $(check[1]).show();
        $(undo[1]).show();
        div.contentEditable = true;
        div.focus();
        div.setAttribute("class", "edeting");
    }


    //confirmiing chenges
    check[1].onclick = function (e) {
        var string = div.innerHTML;
        $(check[1]).hide();
        $(undo[1]).hide();
        if (string == '') {
            div.innerHTML = "edit Line";
            $(edit[1]).show();
            div.contentEditable = false;
            div.className = "endEdeting";


        }
        else {
            string = string.trim();
            // div.style.backgroundColor = '#ffffff';
            div.style.border = '';
            div.contentEditable = false;
            $(edit[1]).show();
            div.className = "endEdeting";

        }

    }

    //Undoing changes
    undo[1].onclick = function (e) {
        $(this).hide();
        $(check[1]).hide();
        $(edit[1]).show();
        div.innerHTML = stringTemp.trim();
        div.contentEditable = false;
        div.className = "endEdeting";



    }

    ////////////////////////////////////////////////////////End Name changing////////////////////////////////////////////////////


    /////////////////////////////////////////////////////////Canging Summery////////////////////////////////////////////////////

    edit[2].onclick = function (e) {
        summeryTemp = summery.innerHTML;
        $(this).hide();
        $(check[2]).show();
        $(undo[2]).show();
        summery.contentEditable = true;
        summery.focus();
        summery.setAttribute("class", "edeting");
    }


    //confirmiing chenges
    check[2].onclick = function (e) {
        var summeryString = summery.innerHTML;
        $(check[2]).hide();
        $(undo[2]).hide();
        if (summeryString == '') {
            summery.innerHTML = "Add Summery";
            $(edit[2]).show();
            summery.contentEditable = false;
            summery.className = "Endsummery text-left";


        }
        else {
            summeryString = summeryString.trim();
            //  summery.style.backgroundColor = '#ffffff';
            summery.style.border = '';
            summery.contentEditable = false;
            $(edit[2]).show();
            summery.className = "Endsummery text-left";

        }

    }

    //Undoing changes
    undo[2].onclick = function (e) {
        $(this).hide();
        $(check[2]).hide();
        $(edit[2]).show();
        summery.innerHTML = summeryTemp.trim();
        summery.contentEditable = false;
        summery.className = "Endsummery text-left";



    }

    ///////// Add to body article ////////////////////////////

    Savebtn.onclick = function (e) {
        var modaleArtical = document.getElementById("uArticleModale");
        var articleUl = document.getElementById("articLelist");
        var articletitle = document.getElementById("Modal-articleName");
        var articleSummery = document.getElementById("Modal-articleSummery");
        var articleAuthor = document.getElementById("Modal-articleAuthors");
        if (articletitle.value == '' || articleSummery.value == '' || articleAuthor.value == '') {
            return null;
        }
        else {
            articleUl.innerHTML += "<li class='media my-4' style='border-bottom:2px solid #F8FCF7'> <div class='media-body'><h5 class='mt-0 mb-1'>" + articletitle.value + "</h5><p>" + articleSummery.value + "</p><small>" + articleAuthor.value + "<cite> PHD</cite></small></div></li>";
        }


    }


}
