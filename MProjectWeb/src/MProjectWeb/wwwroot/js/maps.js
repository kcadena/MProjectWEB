//var map;
//function initMap() {
//    map = new google.maps.Map(document.getElementById('map'), {
//        center: { lat: -34.397, lng: 150.644 },
//        zoom: 8
//    });
//}


// The following example creates complex markers to indicate places near
// Sydney, NSW, Australia. Note that the anchor is set to (0,32) to correspond
// to the base of the flagpole.

function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 6,
        center: { lat: 5.070477, lng: -75.514064 }
    });

    setMarkers(map);
}

// Data for the markers consisting of a name, a LatLng and a zIndex for the
// order in which these markers should display on top of each other.
var places = [
  ['Pasto', 1.206978, -77.281415, 4," Es una ciudad de Colombia"],
  ['Popayan', 2.445379, -76.614183, 5, " Es una ciudad de Colombia"],
  ['Cali', 3.452467, -76.535744, 3, " Es una ciudad de Colombia"],
  ['Pereira', 4.808859, -75.693068, 2, " Es una ciudad de Colombia"],
  ['Manizales', 5.070477, -75.514064, 1, " Es una ciudad de Colombia"],
  ['Medellin', 6.252031, -75.568456, 1, " Es una ciudad de Colombia"]
];

function setMarkers(map) {
    // Adds markers to the map.

    // Marker sizes are expressed as a Size of X,Y where the origin of the image
    // (0,0) is located in the top left of the image.

    // Origins, anchor positions and coordinates of the marker increase in the X
    // direction to the right and in the Y direction down.
    var image = {
        url: 'c:"\"users\admi\desktop\trabajo de grado\programming\project.management\mprojectweb\mprojectweb\src\mprojectweb\wwwroot\img\point.png',
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(20, 32),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 32)
    };
    // Shapes define the clickable region of the icon. The type defines an HTML
    // <area> element 'poly' which traces out a polygon as a series of X,Y points.
    // The final coordinate closes the poly by connecting to the first coordinate.
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly'
    };

    for (var i = 0; i < places.length; i++) {
        var place = places[i];
        var pla = { lat: place[1], lng: place[2] };


        var contentString = '<div id="' + pla + '">' +

       '<h1 class="firstHeading" class="firstHeading">' + place[0] + '</h1>' +
       '<img src="http://www.argentina.travel/pics/162x122/b71a31fd3f.jpg" class="img-responsive" style="margin-top:10px">' +
       '<p>'+place[4]+'</p>'+
       '</div>';

        var infowindow = new google.maps.InfoWindow({
            content: contentString,
            maxWidth: 300
        });

        var marker = new google.maps.Marker({
            position: pla,
            map: map,
            //icon: image,
            //shape: shape,
            title: place[0],
            zIndex: place[3],
            info: contentString
        });

        // Add a click event to a marker
        google.maps.event.addListener(marker, 'click', function () {
            infowindow.close(); // Close previously opened infowindow
            infowindow.setContent(this.info);
            infowindow.open(this.getMap(), this);
        });

    }







}

