

$(document).ready(function () {

    var fullurl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/';

    $("#Comiclist").on('change', function () {

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
                },
                error: function (response) { console.log(response.responseText); },
                failure: function (response) { console.log(response.responseText); }
            });


        //$(".listname").children("option").filter(":selected").attr('data-listId')
        //TODO: Get selected value of a dropdown's item using jQuery... $('#dropDownId :selected')
        //alert($('#Comiclist :selected').attr('data-listId'));
        //var listid = $('#Comiclist :selected').attr('data-listId');

        //$.ajax(
        //    {
        //        //url: fullurl + 'ComicStore/ComicList',
        //        url: fullurl + 'ComicStore/Browse',
        //        data: { listId: listid },
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

    $("#btnLogin_out").on("click", function () {

        var inout = $("#btnLogin_out").text();

        if (inout == "LOGIN") {

            $('#modallogin').css("display", "block", );
            setTimeout(function () {
                $("#modallogin").css('opacity', '1');
                $("#modallogin").css('transition', '0.5s ease-in');

                setTimeout(function () {
                    $('#modallogin_1').css({
                        'left': '0',
                        'transition': '0.5s ease-in'
                    });
                }, 500, )

            }, 200);

        } else {

            $.ajax(
                {
                    url: fullurl + 'ComicStore/LogOff',
                    data: {},
                    type: 'POST',
                    async: false,
                    contentType: 'application/json; charset=utf-8',
                    cache: true,
                    success: function (data) {

                        $("#btnLogin_out").text("LOGIN");
                        $("#btnadmin").remove();

                        //window.location.href = fullurl + 'Home/DefaultView';
                    },
                    error: function (response) { console.log(response.responseText); },
                    failure: function (response) { console.log(response.responseText); }
                });
        }
    })

});

