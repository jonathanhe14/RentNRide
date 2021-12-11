
var markers = [];

var posiciones = [];

var map;

var timer;

var ubicaciones = [];




function initMap() {
    var latitud = $('.latitud').attr('value');
    var longitud = $('.longitud').attr('value');

    cargarUbicacionActual(latitud, longitud);

    //Poner aquí dónde va en el mapa
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: latitud, lng: longitud },
        zoom: 15


    })



}


function cargarUbicacionActual(latitud, longitud) {

    navigator.geolocation.getCurrentPosition(function (position) {
        var geolocate = new google.maps.LatLng(latitud, longitud);
        crearMarcador(geolocate);
        map.setCenter(geolocate);
    });


}

function crearMarcador(position) {

    posiciones.push(posiciones);


    markers = [];
    const marker = new google.maps.Marker({
        position,
        map,
    });
    markers.push(marker);
    var latitud = marker.getPosition().lat();
    var longitud = marker.getPosition().lng();
    ubicaciones.push(latitud);
    ubicaciones.push(longitud);

    console.log("Marcador creado");
}


