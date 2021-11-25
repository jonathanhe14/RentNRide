function IniciarSesion() {

}
$(document).ready(function () {

    $('#IniciarSesion').click(function () {
        var correo = $('#UserName').val()
        localStorage.setItem("Correo", correo)
    })

});