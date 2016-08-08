//permite hacer el cambio del modal login a register y viceversa
$(".btn_log").click(function () {
    $("#modal_reg").modal("hide");
    $("#modal_log").modal();
});

$(".btn_reg").click(function () {
    $("#modal_log").modal("hide");
    $("#modal_reg").modal();
});


//vriables para las opciones en side-nav
var opt = "";     //variable para JSON
var con = "";   //Controlador
var act = "";   //Accion
var act_ant = "";//Accion Anterior
//carga las optciones provenientes de archivo JSON 
function dropDownClass(op) {
    //alert(op);
    //op = op.replace(/&quot;/gi, '"');
    //op = op.trim();
    opt = op;
}
//funcion que habilita cargar AJAX en la pagina 
//op1 y op2 corresponden al controlador y accion que vienen del click de las opciones
function callOpt(op1, op2, sel) {

    if (sel == 1) {
        $("#op_" + act_ant).removeClass('in');  //cierra dropdown cuando selecciona otra opcion
        $("#" + act_ant).parent().removeClass("active");
        act_ant = op2;
    }
    //$("#op_" + act).addClass('active');
    $("#axd").removeClass('in');           //cierra dropdown cuando selecciona opcion en moviles
    con = op1;
    act = op2;
    $("#" + act).parent().addClass("active");

    //e.preventDefault();
    //e.stopPropagation();
    $.ajax({
        url: '',
        type: 'text',
        async: true,
        cache: false,
        beforeSend: function () {

            $("#content-opt").html("");
            $("#content-opt").html('<img src="/img/ajax-loader.gif" style="position:relative;margin-left:50%;margin-top:20%;height:8%;width:8%;">');
        },
        success: function succ(data) {
            //alert(con + "  " + act);
            $("#content-opt").load('/' + con + '/' + act);

        }
    });

}


function projectsClick(dat) {
    dat = dat.replace(/prj_/gi, "");
    dat = { id: dat };
    $.ajax({
        url: '/Projects/PanelProject',
        type: "POST",
        //dataType: "json",
        data: dat,
        //contentType: 'application/x-www-form-urlencoded; charset=utf-8',

        // cache: false,
        //datType: "json",
        //async: true,
        cache: false,
        beforeSend: function () {
            $("#panel_prj").html("");
            $("#panel_prj").html('<img src="/img/ajax-loader.gif" style="position:relative;margin-left:50%;margin-top:20%;height:8%;width:8%;">');
        },
        success: function succ(data) {
            $("#panel_prj").html(data);
        }
    }).fail(
       function (da) {
           alert("No se pudo cargar la pagina.");
       }
    );

}


///////////////  opciones concernientes al dropdown que poseen las actividades  //////////////////////////

function optionsActivities(id, act) {
    //id => id de la caracteristica 
    //act => Opciones  [ 1* files 2*estatistics 3* more information] => Accion en el controlador
    $.ajax({
        url: '/Projects/' + act,
        type: 'POST',
        data: { id_car: id },
        async: true,
        cache: false,
        beforeSend: function () {

            $("#content-opt").html("");
            $("#content-opt").html('<img src="/img/ajax-loader.gif" style="position:relative;margin-left:50%;margin-top:20%;height:8%;width:8%;">');
        },
        success: function succ(data) {
            $("#content-opt").html(data);

        }
    });

}


///////////////  Click de los folders  //////////////////////////

function openFolder(car,op) {
    //alert(car);
    $.ajax({
        url: '/Projects/Activity',
        type: 'POST',
        data: { id_car: car,opt:op },
        async: true,
        cache: false,
        beforeSend: function () {

           // $("#content-opt").html("");
           // $("#content-opt").html('<img src="/img/ajax-loader.gif" style="position:relative;margin-left:50%;margin-top:20%;height:8%;width:8%;">');
        },
        success: function succ(data) {
            if (data != 0) {
                $("#content-opt").html("");
                $("#content-opt").html(data);
            }
            else {
                alert("No se encontraron actividaes relacionadas!!!");
            }
        }
    }).fail(
       function (da) {
           alert("No se pudo cargar la pagina.");
       }
    );
}




$(document).ready(function () {
    $("[rel='tooltip']").tooltip();

    $('.thumbnail').hover(
        function () {
            $(this).find('.caption').slideDown(250); //.fadeIn(250)
        },
        function () {
            $(this).find('.caption').slideUp(250); //.fadeOut(205)
        }
    );
});