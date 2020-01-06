


function loadDetails(div) {

    var fullurl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/';

    var comicId = $(div).attr('data-comicid');
    // TODO: GET the CategoryId from Layout.cshtml
    var catId = $("#currentcategoryname").attr('data-categoryid');

    //window.location.href = '/ComicStore/Browse?categoryId=' + categoryId;

    $.ajax(
        {
            url: fullurl + 'ComicStore/Details',
            data: { id: comicId, categoryId: catId },
            type: 'GET',
            async: false,
            contentType: 'application/json; charset=utf-8',
            //cache: true,
            success: function (data) {
                //alert("comicId");

                $('.modaldetails').css("display", "block", );
                setTimeout(function () {
                    $(".modaldetails").css('opacity', '1');
                    $(".modaldetails").css('transition', '0.5s');

                    setTimeout(function () {
                        $('.modaldetails_1').css({
                            'opacity': '1',
                            'transition': '0.5s'
                        });
                    }, 500, )

                }, 200);
                //$('.modaldetails_1').children().remove();
                $('.modaldetails_1').children().not('.eks').remove();

                //TODO: Convert object to HTML
                var olddata = $('.eks').get(0).outerHTML;
                $('.modaldetails_1').html(olddata + data);
            },
            error: function (response) { console.log(response.responseText); },
            failure: function (response) { console.log(response.responseText); }
        });
}

var ratingshere = $('.ratinghere');

for (var i = 0; i < $(ratingshere).length; i++) {
    var thisRating = $(ratingshere[i]);
    var comicrates = $(ratingshere[i]).attr('data-comicrates');

    $(thisRating).rateYo({
        starWidth: "20px",
        rating: comicrates,
        normalFill: "#A0A0A0",
        ratedFill: "#F39C12",
        numStars: 5,
        maxValue: 5,
        halfStar: true,
        readOnly: true
    });
}


//$(document).ready(function () {

//    var fullurl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/';

//    $('.imgdiv').unbind('click').on('click', function () {
//        var comicId = $(this).attr('data-comicid');

//        // TODO: GET the CategoryId from Layout.cshtml
//        var catId = $("#currentcategoryname").attr('data-categoryid');

//        //alert(comicId);
//        //window.location.href = '/ComicStore/Browse?categoryId=' + categoryId;
//        $.ajax(
//            {
//                url: fullurl + 'ComicStore/Details',
//                data: { id: comicId, categoryId: catId },
//                type: 'GET',
//                async: false,
//                contentType: 'application/json; charset=utf-8',
//                //cache: true,
//                success: function (data) {
//                    //alert("comicId");

//                    $('.modaldetails').css("display", "block", );
//                    setTimeout(function () {
//                        $(".modaldetails").css('opacity', '1');
//                        $(".modaldetails").css('transition', '0.5s');

//                        setTimeout(function () {
//                            $('.modaldetails_1').css({
//                                'opacity': '1',
//                                'transition': '0.5s'
//                            });
//                        }, 500, )

//                    }, 200);
//                    //$('.modaldetails_1').children().remove();
//                    $('.modaldetails_1').children().not('.eks').remove();

//                    //TODO: Convert object to HTML
//                    var olddata = $('.eks').get(0).outerHTML;
//                    $('.modaldetails_1').html(olddata + data);
//                },
//                error: function (response) { console.log(response.responseText); },
//                failure: function (response) { console.log(response.responseText); }
//            });
//    });

//});