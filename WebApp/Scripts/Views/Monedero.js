dataM = {};
dataMonedero = {};
function Monedero() {

    this.URL_API = "http://localhost:52125/api/";
    this.usersService = 'usuarios';
    this.GetUrlApiService = function (service) {
        return this.URL_API + service;
    }


    this.GetToApiMonedero = function (service, callbackFunction) {

        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            dataM = response.Data;
            //data = JSON.stringify(response.Data);
            console.log(dataM);
            $("#saldo").html(dataM["Saldo"] +"₡");
        });
    }
    this.Monedero = function () {
        console.log(localStorage.getItem('Correo'))
        this.GetToApiMonedero(this.usersService + "/GetMonedero?correo=" + localStorage.getItem('Correo'))

    }

    this.PutToAPI = function (data) {
        var jqxhr = $.post('http://localhost:52125/api/usuarios/ActualizarMonedero', data, function (response) {

        })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
            })
    };
    this.Recarga = function () {
        this.PutToAPI(dataMonedero);
    }
    this.Actualizar = function () {
        dataMonedero["Saldo"] = dataM["Saldo"] + parseInt($("#montoRecarga").val())
        dataMonedero["IdUsuario"] = dataM["IdUsuario"];
        dataMonedero["FechaExpiracion"] = dataM["FechaExpiracion"];
        dataMonedero["FechaCorte"] = dataM["FechaCorte"];
        dataMonedero["InfoMonedero"] = dataM["InfoMonedero"];
        console.log("Data Monedero" + dataMonedero);
        console.log(dataMonedero)
        this.Recarga()
    }
}
paypal.Buttons({
    style: {
        color: "blue",
        shape: "pill"
    },
    createOrder: function (data, actions) {
        return actions.order.create({
            purchase_units: [{


                amount: {
                    value: parseInt($("#montoRecarga").val())
                }
            }]
        });
    },
    onApprove: function (data, actions) {
        return actions.order.capture().then(function (details) {
            this.crtMonedero = new Monedero();
            this.crtMonedero.Actualizar();
        })
    }


}).render('#paypal-payment-button');

$(document).ready(function () {
    this.ctrMoneder = new Monedero();
    this.ctrMoneder.Monedero();
    $("#montoRecarga").val(10)


});