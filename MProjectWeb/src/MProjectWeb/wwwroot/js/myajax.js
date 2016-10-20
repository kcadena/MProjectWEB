function myajax(key, idcar, usu) {
    $.ajax({
        url: '/Projects/getLinks',
        data: { "key": key, "idcar": idcar, "usu": usu },
        type: 'GET',
        dataType: "json",
        crossDomain: true,
        //async: true,
        success: function (e) {
            //alert("OK");
            try {

                document.getElementById("aux").setAttribute("keym", key);
                document.getElementById("aux").setAttribute("idUsu", usu);
                document.getElementById("aux").setAttribute("idCar", idcar);
            } catch (err) { }
            //alert(e);
            try{
                $("#activityList").html('');
                $("#scrip").html('');
                jQuery.each(e, function (i, val) {
                    var sep = val.split("-");
                    var idcom = sep[0].split(",");
                    //alert(sep[1]);
                    var id = sep[1];
                    for (var i = 0; i < id.length; i++) {
                        id = id.replace(' ', '_');
                    }
                    for (var i = 0; i < sep[2].length; i++) {
                        sep[2] = sep[2].replace(' ', '_');
                    }

                    if (sep[1] == "Atras") {
                        var ht = '<li class="selItems" id="selBack"><a id="' + id + '">' + sep[1] + '</a></li>';
                    }
                    else {
                        var ht = '<li class="selItems" ><a id="' + id + '">' + sep[1] + '</a></li>';
                    }

                    var sc = "<script type='text/javascript'> $('#" + id + "').click(function() { myajax('" + idcom[0] + "','" + idcom[1] + "','" + idcom[2] + "');  $('#area').load('" + sep[2] + "'); }); <\/script>";
                
                    $("#activityList").append(ht);
                    $("#scrip").append(sc);

                
                
                });
            } catch (err) {
                alert(err);
            }
            
        }
    }).fail(function () {
        alert("error");
    });
}
