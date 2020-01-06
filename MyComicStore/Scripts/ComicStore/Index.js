
var fullurl;

$(document).ready(function () {

    fullurl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/';

    $('.categoryitem').unbind('click').on('click', function () {

        $('.categoryitem').css('background-color', '');
        $(this).css('background-color', '#000');

        //TODO: Pass categoryname to Layout.cshtml
        $("#currentcategoryname").html($(this).find('span').text());

        var categoryId = $(this).attr('data-categoryid');
        var listid = $('#Comiclist :selected').attr('data-listId');
        var title = $("#mySearchbox").val();
        
        // TODO: pass the CategoryId to Layout.cshtml
        $("#currentcategoryname").attr('data-categoryid', categoryId);
        //alert(categoryId);
        //window.location.href = '/ComicStore/Browse?categoryId=' + categoryId;
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

        //When using ActionLink
        //var hyperLink = $(this).find('a');
        //if ($(hyperLink).length > 0) {
        //    window.location.href = $(hyperLink).get(0).href;
        //}
    });
});

function admin(adminbtn)
{
    window.location.href = '/Admin/Home';
}