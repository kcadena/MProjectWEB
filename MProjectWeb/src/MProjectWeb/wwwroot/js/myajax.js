function myajax(key, idcar, usu) {
    $.ajax({
        url: 'http://172.16.10.248/MProjectService.svc/getLinks',
        data: { "key": key, "idcar": idcar, "usu": usu },
        type: 'GET',
        dataType: 'json',
        success: function (e) {
            $("#activityList").html('');
            $("#scrip").html('');
            jQuery.each(e["d"], function (i, val) {
                var sep = val.split("-");
                var idcom = sep[0].split(",");
                var ht = '<li><a id="' + sep[1] + '">' + sep[1] + '</a></li>';
                var sc = "<script type='text/javascript'> $('#" + sep[1] + "').click(function() { myajax('" + idcom[0] + "','" + idcom[1] + "','" + idcom[2] + "');  $('#area').load('http://172.16.10.248/prueba%20web/" + sep[1] + ".html'); }); <\/script>";
                $("#activityList").append(ht);
                $("#scrip").append(sc);
            });
        },
        error: function () { alert('Failed!'); }
    });
}
