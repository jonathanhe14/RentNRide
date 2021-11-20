
    var markers = [];

    var posiciones = [];

    var map;

    var timer;

    var ubicaciones = [];


function initMap() {
        cargarUbicacionActual();

        map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: 9.932781, lng: -84.031195 },
            zoom: 15


        })


            
}


function cargarUbicacionActual() {

    navigator.geolocation.getCurrentPosition(function (position) {
        var geolocate = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
        crearMarcador(geolocate);
        map.setCenter(geolocate);
    });


}

function crearMarcador(position) {
    this.ctrActions = new ControlActions();
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


        this.ctrActions.Ubicacion(ubicaciones[0], ubicaciones[1]);




    console.log("Marcador creado");

}
function eliminarMarcadores() {
    console.log('Borrando todos los marcadores');
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

});


