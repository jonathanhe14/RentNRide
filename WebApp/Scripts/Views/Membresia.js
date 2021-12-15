function Membresia() {

    this.ctrlActions = new ControlActionsAdmin();
    this.correo = "";
    this.membresia = "";

    this.getURLParameter = function (sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
        return false;
    }

    this.getParams = function () {
        var correo = this.getURLParameter('correo');
        var membresia = this.getURLParameter('membresia');

        

        this.correo = atob(correo);
        this.membresia = atob(membresia);
        localStorage.setItem("Correo", atob(correo))
        localStorage.setItem("IdMembresia", atob(membresia))

        this.ctrlActions.GetToApi("administrador/GetMembresia/" + this.membresia, function (resp) {
            $('#lblNombre').append(resp["Nombre"]);
            $('#lblMonto').append(resp["MontoMensual"]);
            $('#lblComision').append(resp["ComisionTransaccion"]);
            $('#lblNum').append(resp["NumDias"]);
        });

        
    }

    this.Aceptar = function() {
        window.location.href = "https://localhost:44383/Home/PagoMembresia";

    }

    this.Rechazar = function () {
        window.location.href = "https://localhost:44383/Home/InicioSesion";
        Storage.clear();
    }

}

$(document).ready(function () {
    var membresia = new Membresia();
    membresia.getParams();

});