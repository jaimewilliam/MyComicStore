

$(document).ready(function () {

    var fullurl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/';

    $("#btnsearch").on('click', function () {

        var categoryId = $("#currentcategoryname").attr('data-categoryid');
        var listid = $('#Comiclist :selected').attr('data-listId');
        var title = $("#mySearchbox").val();

        $.ajax(
            {
                url: fullurl + 'ComicStore/Browse',
                data: {
                    categoryId: categoryId,
                    listId: listid,
                    comTitle: title
                },
                type: 'GET',
                async: false,
                contentType: 'application/json; charset=utf-8',
                //cache: true,
                success: function (data) {
                    //console.log(data);
                    $('.defaultview').children().remove();
                    $('.defaultview').html(data);

                    //TODO: open the comic details if search result == 1
                    var searchResult = $('.motherdiv').children().length;
                    if (searchResult == 1) {
                        $('.motherdiv').children().children().click();
                    }


                },
                error: function (response) { console.log(response.responseText); },
                failure: function (response) { console.log(response.responseText); }
            });

        //TODO: Open a Diffrent View!
        //var title = $("#mySearchbox").val();
        //alert(title);
        //$.ajax(
        //    {
        //        //url: fullurl + 'ComicStore/SearchView',
        //        url: fullurl + 'ComicStore/Browse',
        //        data: { comTitle: title },
        //        type: 'GET',
        //        async: false,
        //        contentType: 'application/json; charset=utf-8',
        //        //cache: true,
        //        success: function (data) {
        //            //console.log(data);
        //            $('.defaultview').children().remove();
        //            $('.defaultview').html(data);
        //        },
        //        error: function (response) { console.log(response.responseText); },
        //        failure: function (response) { console.log(response.responseText); }
        //    });
    });

    $("#mySearchbox").unbind('keyup').on('keyup', function () {

        var title = $("#mySearchbox").val();

        $.ajax(
            {
                url: fullurl + 'ComicStore/AutoCompleteSearch',
                data: {
                    comTitle: title
                },
                type: 'GET',
                async: false,
                contentType: 'application/json; charset=utf-8',
                //cache: true,
                success: function (data) {
                    $('.autocompleteresult').children().remove();
                    //console.log(data[1]);
                    for (var i = 0; i < data.length; i++) {
                        var returnspan = $('<span class="suggestionbox" onclick="selectresult(this);" style="position:relative;display:block;background-color:#f5f5f5; border:1px Solid #464646;padding:10px">' + data[i] + '</span>');
                        $(returnspan).appendTo($('.autocompleteresult'));
                    }

                    $(".suggestionbox").unbind('mouseover').on('mouseover', function () {
                        $('.suggestionbox').css({ "background-color": "#f5f5f5", "color": "#000000" });
                        $(this).css({ "background-color": "#464646", "color": "#ffffff" });
                    });

                },
                error: function (response) { console.log(response.responseText); },
                failure: function (response) { console.log(response.responseText); }
            });
    });

    $(window).on('click', function () {
        $('.autocompleteresult').children().remove();
        //$('#mySearchbox').val("");
    });
});

function selectresult(span) {
    $("#mySearchbox").val($(span).text());
    $("#btnsearch").click();
    $('.autocompleteresult').children().remove();
}