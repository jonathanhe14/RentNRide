var markers = [];

var posiciones = [];

var map;

var timer;

var ubicaciones = [];

var latitudOriginal;

var longitudOriginal;

function initMap() {

    var latitud = $('.latitud').attr('value');
    var longitud = $('.longitud').attr('value');

    latitudOriginal = $('.latitud').attr('value');
    longitudOriginal = $('.longitud').attr('value');

    cargarUbicacionOriginal(latitud, longitud);

    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: latitud, lng: longitud },
        zoom: 15


    })
}

function volverUbicacionDefault(latitudVieja, longitudVieja) {

    eliminarMarcadores();
    cargarUbicacionOriginal(latitudVieja, longitudVieja);
    $('.latitud').val(latitudVieja);
    $('.longitud').val(longitudVieja);
    $("#q-lugar-diferente").text("0");
    var q_lugar = parseFloat($("#q-lugar-diferente").text());
    var p_lugar = parseFloat($("#precio-difer").text());
    var total = q_lugar * p_lugar;
    $("#total-difer").text(total.toString());
    var q_horas = parseFloat($("#q-horas").text());
    var p_horas = parseFloat($("#precio-tarifa").text());
    var total_horas = q_horas * p_horas;
    $("#total-tarifa").text(total_horas.toString());
    var total_maximo = total + total_horas;
    $("#total-general").text(total_maximo.toString());
}

function cargarUbicacionOriginal(latitud, longitud) {

    navigator.geolocation.getCurrentPosition(function (position) {
        var geolocate = new google.maps.LatLng(latitud, longitud);
        crearMarcador(geolocate);
        map.setCenter(geolocate);
    });


}

function crearMarcador(position) {

    posiciones.push(posiciones);

    eliminarMarcadores();
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
    $('.latitud').val(latitud);
    $('.longitud').val(longitud);


    //console.log("Marcador creado");
    var la = latitud.toString();
    var lon = longitud.toString();
    var comprobLa = la.localeCompare(latitudOriginal);
    var comprobLon = lon.localeCompare(longitudOriginal);
    if (comprobLa != 0 && comprobLon != 0) {
        $("#q-lugar-diferente").text("1");
        var q_lugar = parseFloat($("#q-lugar-diferente").text());
        var p_lugar = parseFloat($("#precio-difer").text());
        var total = q_lugar * p_lugar;
        $("#total-difer").text(total.toString());
        var q_horas = parseFloat($("#q-horas").text());
        var p_horas = parseFloat($("#precio-tarifa").text());
        var total_horas = q_horas * p_horas;
        $("#total-tarifa").text(total_horas.toString());
        var total_maximo = total + total_horas;
        $("#total-general").text(total_maximo.toString());
    }

}
function eliminarMarcadores() {
    //console.log('Borrando todos los marcadores');
    for (a in markers) {
        markers[a].setMap(null);
    }
    markers = [];
}


$(document).ready(function () {

    $('#EliminarMarcador').click(function () {
        eliminarMarcadores();
    });
    map.addListener("click", (event) => {
        if (markers.length < 1) {
            crearMarcador(event.latLng);
        } else {
            eliminarMarcadores();

        }


    });;

    $("#btn-ubic-orig").click(function () {
        $("#q-lugar-diferente").text("");
        volverUbicacionDefault(latitudOriginal, longitudOriginal)
    });

});