$(document).ready(function () {
    $(".btn_log").click(function () {
        //alert("Has escrito: " + $("#TextBox1").val());
        $("#modal_reg").modal("hide");
        $("#modal_log").modal();
    });



    $(".btn_reg").click(function () {
        //alert("Has escrito: " + $("#TextBox1").val());
        $("#modal_log").modal("hide");
        $("#modal_reg").modal();
    });
});


