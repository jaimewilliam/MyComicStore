

$(document).ready(function () {

    $("#btnplus").on('click', function () {
        //alert("plus");
        //TODO: Increase the value of textbox
        $('#Quantity').val(+ $('#Quantity').val() + 1);

        // using parseInt function takes a string as argument and returns its integer equivalent.
        //var qty = parseInt($('#Quantity').val()) + 1 ; 
        //$('#Quantity').val(parseInt($('#Quantity').val()) + 1);
    });

    $("#btnminus").on('click', function () {
        //alert("plus");
        //TODO: Increase the value of textbox
        var qty = + $('#Quantity').val() > 1 ? + $('#Quantity').val() - 1 : 1;
        $('#Quantity').val(qty);

        // using parseInt
        //var qty = parseInt($('#Quantity').val()) > 1 ? parseInt($('#Quantity').val()) - 1 : 1;
        //$('#Quantity').val(qty);
    });

    $("#details").on('click', function () {
        $("#reviews").css({ "background-color": "#ffffff", "color": "#464646" });
        $(this).css({ "background-color": "#464646", "color": "#ffffff" })
        $("#id_description").css("display", "block")
        $("#id_review").css("display", "none")
    });

    var fullurl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + '/';

    $("#reviews").on('click', function () {
        $("#details").css({ "background-color": "#ffffff", "color": "#464646" });
        $(this).css({ "background-color": "#464646", "color": "#ffffff" })
        $("#id_description").css("display", "none")
        $("#id_review").css("display", "block")


        var comicId = $(".maindiv").attr('data-comicid');
        $.ajax(
            {
                url: fullurl + 'ComicStore/Reviews',
                data: { id: comicId },
                type: 'GET',
                async: false,
                contentType: 'application/json; charset=utf-8',
                //cache: true,
                success: function (data) {

                    $('.modelreview').children().remove();
                    $('.modelreview').html(data);
                },
                error: function (response) { console.log(response.responseText); },
                failure: function (response) { console.log(response.responseText); }
            });
    });

    $("#btnCart").on('click', function () {

        var comicTitle = $(".comicTitle").text();
        var comicId = $(".comicId").text();
        var comicPrice = $(".comicPrice").text();
        var qty = $("#Quantity").val();
        var newcomicPrice = comicPrice * qty;

        if ($("#selectedComics").find($('*[data-comictitle="' + comicTitle.split(' ').join('') + '"]')).length > 0) {
            var existingOrder = $("#selectedComics").find($('*[data-comictitle="' + comicTitle.split(' ').join('') + '"]'));
            $(existingOrder).find('.quantity').text(parseFloat(existingOrder.find('.quantity').text()) + parseInt(qty));

            var newPrice = comicPrice * parseInt($(existingOrder).find('.quantity').text());
            $(existingOrder).find('.qtyprice').text(newPrice);

        } else {
            var selectedItem = $('<div id="' + comicId + '" class="itemtray" style="position:relative; width:100%; height:10%; cursor:pointer" data-comictitle="' + comicTitle.split(' ').join('') + '" data-comicid="' + comicId + '">' +
                '<div id="delete" style="position:absolute; width:15px; height:15px; cursor:pointer; z-index:1; top:0; left: 95%; text-align:center">'+'<span>'+ "X" + '</span>'+'</div>' +
                '<div style="position:relative; width:70%; height:100%; display:inline-block; vertical-align:top; ">' +
                '<div style="position:relative; width:100%; height:60%">' +
                '<div style="position:relative; width:100%; height:auto; padding-left:10px; top:50%; left:50%; transform:translate(-50%,-50%); font-size:large; font-family:"Century Gothic"; font-weight:bold">' +
                '<span>' + comicTitle + '</span>' +
                '</div>' +
                '</div>' +
                '<div style="position:relative; width:100%; height:40%">' +
                '<div style="position:relative; width:100%; height:auto; padding-left:10px; top:50%; left:50%; transform:translate(-50%,-50%); font-size:small; font-family:"Century Gothic"; font-weight:bold">' +
                '<span>' + comicPrice + ' x <span class="quantity">' + qty + '</span></span>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '<div style="position:relative; width:30%; height:100%; display:inline-block; vertical-align:top">' +
                '<div style="position:relative; width:100%; height:auto; padding-right:10px; top:50%; left:50%; transform:translate(-50%,-50%); font-size:large; font-weight:bold; text-align:right">' +
                '<span class="qtyprice">' + newcomicPrice + '</span>' +
                '</div>' +
                '</div>' +
                '</div>');

            var $div = $(selectedItem);

            // DELETE BUTTON 
            $div.find("#delete").on("click",function () {
                $('#' + comicId).remove();

                if ($("#selectedComics").find('.itemtray').length > 0) {
                    computeTotalPrice();
                } else {
                    $("#redAlert").css("display", "none");
                    $("#div_totalAmount").find("#totalAmount").text("Php " + "0.00");
                }
            });

            $(selectedItem).appendTo($("#selectedComics"));
            $("#redAlert").css("display", "block");

            $(".itemtray").unbind('mouseover').on('mouseover', function () {
                $('.itemtray').css({ "background-color": "#ffffff", "color": "#000000" });
                $(this).css({ "background-color": "#EEEEEE", "color": "#000000" });
            });


        }

        computeTotalPrice();
        $(".eks").click();
    })

    
});



var stars = $(".maindiv").attr('data-totalratings');

$(".stars").rateYo({
    starWidth: "10px",
    rating: stars,
    normalFill: "#A0A0A0",
    ratedFill: "#F39C12",
    numStars: 5,
    maxValue: 5,
    halfStar: true,
    readOnly: true

});

//TODO: Allow only numeric (0-9) in HTML inputbox using jQuery!
//$('#Quantity[name="Quantity"]').keyup(function (e) {
//    if (/\D/g.test(this.value)) {
//        // Filter non-digits from input value.
//        this.value = this.value.replace(/\D/g, '');
//    }
//});
$('#Quantity').bind('keyup paste', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

    //onclick = "addtoCart($(this));"
    //function addtoCart(cart) {}

function computeTotalPrice() {

    var totalamount = 0;
    var subtotalamount = $("#selectedComics").find(".qtyprice");
    //alert(subtotalamount)
    for (var i = 0; i < subtotalamount.length; i++) {
        totalamount = totalamount + parseFloat($(subtotalamount[i]).text());
        $("#div_totalAmount").find("#totalAmount").text("Php " + totalamount);
    }

    //alert(subtotalamount.length + ', ' + totalamount);
}