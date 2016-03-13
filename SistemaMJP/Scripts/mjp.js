$(document).ready(function () {
    $("#txtSubPartida").keydown(function () {
        $("#MsjErrorSubPartida").css('display', 'none');

    });

    $("#txtNumFactura").keydown(function () {
        $("#MsjErrorFactura").css('display', 'none');

    });
});
//$("input[name=Tipo]:radio").change(function () {

//    if ($("#IngresoFactura").attr("checked")) {
//        $("#formFacturas").css('display', 'block');
//    }
//    else {
//        $("#formFactura").css('display', 'none');
//    }
//});

$(document).ready(function () {
    $('input[name="Tipo"]').change(function () {
        if (this.value == '1') {
            $("#formFacturas").css('display', 'block');
        }
        else if (this.value == '2') {
            $("#formFactura").css('display', 'none');
        }
    });
});